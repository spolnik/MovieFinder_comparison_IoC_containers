using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieFinder
{
    public interface IMovieLister
    {
        IList<Movie> MoviedDirectedBy(string director);
    }

    public class MovieLister : IMovieLister
    {
        private readonly IMovieFinder _movieFinder;

        public MovieLister(IMovieFinder movieFinder)
        {
            _movieFinder = movieFinder;
        }

        //TODO: virtual keyword is needed for NInject, fail ;/
        public IList<Movie> MoviedDirectedBy(string director)
        {
            Console.WriteLine("Inside MovieLister.MoviedDirectedBy");
            return _movieFinder.FindAll().Where(m => m.Director == director).ToList();
        }
    }
}