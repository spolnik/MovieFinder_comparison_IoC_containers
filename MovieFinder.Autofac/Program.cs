using System;
using System.IO;
using Autofac;

namespace MovieFinder.Autofac
{
    //TODO: problems with installation via NuGet - interception extension ;/ fail
    class Program
    {
        static void Main()
        {
            try
            {
                MovieLister lister;

                using (var container = BuildContainer())
                {
                    lister = container.Resolve<MovieLister>();
                }

                var movies = lister.MoviedDirectedBy("Roberto Benigni");
                Console.WriteLine("\nSearching for movie...\n");
                foreach (var movie in movies)
                {
                    Console.WriteLine(string.Format("Movie Titile ='{0}', Director = '{1}'.",
                                                    movie.Titile, movie.Director));
                }

                Console.WriteLine("\nMovieApp Done.\n\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            var fileParameter = new NamedParameter("movieFile", new FileInfo("movies.txt"));
            
            builder.RegisterType<ColonDelimitedMovieFinder>().As<IMovieFinder>().WithParameter(fileParameter);
            builder.RegisterType<MovieLister>().AsSelf();
            
            return builder.Build();
        }
    }
}
