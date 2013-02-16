using System;
using System.IO;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using MovieFinder.Library;

namespace MovieFinder.CastleWindsor
{
    class Program
    {
        static void Main()
        {
            try
            {
                IMovieLister lister;

                using (var container = BootstrapContainer())
                {
                    lister = container.Resolve<IMovieLister>();
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

        public static IWindsorContainer BootstrapContainer()
        {
            return new WindsorContainer()
               .Install(FromAssembly.This());
        }
    }

    public class MovieFinderInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<LoggingInterceptor>(),
                Component.For<IMovieFinder>()
                    .ImplementedBy<ColonDelimitedMovieFinder>()
                    .DependsOn(Property.ForKey<FileInfo>().Eq(new FileInfo("movies.txt")))
                    .Interceptors<LoggingInterceptor>(),
                Component.For<IMovieLister>()
                    .ImplementedBy<MovieLister>()
                    .Interceptors<LoggingInterceptor>());
        }
    }
}
