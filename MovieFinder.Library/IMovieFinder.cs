using System;
using System.Collections.Generic;

namespace MovieFinder
{
    public interface IMovieFinder
    {
        IList<Movie> FindAll();
    }

    public class SimpleMovieFinder : IMovieFinder
    {
        private readonly IList<Movie> _movies = new List<Movie>();

        public SimpleMovieFinder()
        {
            InitList();
        }

        private void InitList()
        {
            _movies.Add(new Movie("La vita e bella", "Roberto Benigni"));
        }

        public IList<Movie> FindAll()
        {
            return _movies;
        }
    }
}