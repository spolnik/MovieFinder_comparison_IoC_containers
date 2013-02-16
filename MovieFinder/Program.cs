using System;
using System.IO;
using Spring.Context;
using Spring.Context.Attributes;
using Spring.Context.Support;
using Magnum.Extensions;

namespace MovieFinder
{
    class Program
    {
        static void Main()
        {
            try
            {
                MovieLister lister;
                
                using (var ctx = CreateContainerUsingCodeConfig())
                {
                    lister = ctx.GetObject("MovieLister").CastAs<MovieLister>();
                }

                var movies = lister.MoviedDirectedBy("Roberto Benigni");
                Console.WriteLine("\nSearching for movie...\n");
                foreach (var movie in movies)
                {
                    Console.WriteLine(string.Format("Movie Titile ='{0}', Director = '{1}'.",
                                                    movie.Titile, movie.Director));   
                }

                Console.WriteLine("\nSpring MovieApp Done.\n\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static IApplicationContext CreateContainerUsingCodeConfig()
        {
            var ctx = new CodeConfigApplicationContext();
            ctx.ScanAllAssemblies();
            ctx.Refresh();
            return ctx;
        }
    }

    [Configuration]
    public class MovieFinderConfiguration
    {
        [Definition]
        public virtual MovieLister MovieLister()
        {
            return new MovieLister(MovieFinder());
        }

        [Definition]
        public virtual IMovieFinder MovieFinder()
        {
            var fileInfo = new FileInfo("movies.txt");
            return new ColonDelimitedMovieFinder(fileInfo);
        }
    }
}
