using System;
using System.Collections.Generic;
using System.IO;
using Magnum.Collections;

namespace MovieFinder
{
    public class ColonDelimitedMovieFinder : IMovieFinder
    {
        private static readonly char[] Delimeter = new[] {':'};

        private FileInfo _movieFile;
        private IList<Movie> _movies;

        public ColonDelimitedMovieFinder(FileInfo movieFile)
        {
            MovieFile = movieFile;
        }

        public FileInfo MovieFile
        {
            get
            {
                return _movieFile;
            }
            set
            {
                _movieFile = value;
                if (_movieFile != null && _movieFile.Exists)
                {
                    InitList();
                }
            }
        }

        private void InitList()
        {
            _movies = new List<Movie>();

            using (var reader = MovieFile.OpenText())
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(Delimeter);
                    var tuple = new Tuple<string, string> {
                                                              First = data[0],
                                                              Second = data[1]
                                                          };

                    var movie = new Movie(tuple.First, tuple.Second);
                    _movies.Add(movie);
                }
            }
        }

        //TODO: virtual keyword is needed for NInject, fail ;/
        public IList<Movie> FindAll()
        {
            Console.WriteLine("Inside ColonDelimitedMovieFinder.FindAll");
            return _movies;
        }
    }
}