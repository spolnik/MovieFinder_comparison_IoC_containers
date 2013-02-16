using System;
using System.IO;
using Ninject;
using Ninject.Extensions.Interception.Infrastructure.Language;

namespace MovieFinder.NInject
{
    class Program
    {
        static void Main()
        {
            try
            {
                IMovieLister lister;

                using (var kernel = InitializeKernel())
                {
                    lister = kernel.Get<IMovieLister>();
                }

                var movies = lister.MoviedDirectedBy("Roberto Benigni");
                Console.WriteLine("\nSearching for movie...\n");
                foreach (var movie in movies)
                {
                    Console.WriteLine(string.Format("Movie Titile ='{0}', Director = '{1}'.",
                                                    movie.Titile, movie.Director));
                }

                Console.WriteLine("\nNInject MovieApp Done.\n\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static StandardKernel InitializeKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<LoggingInterceptor>().ToSelf();
            kernel.Bind<IMovieFinder>().To<ColonDelimitedMovieFinder>().WithConstructorArgument("movieFile", new FileInfo("movies.txt"));

            kernel.Bind<IMovieLister>().To<MovieLister>();
            //TODO: it doesn't work, I don't know why
//                .Intercept().With<LoggingInterceptor>();

            return kernel;
        }
    }
}
