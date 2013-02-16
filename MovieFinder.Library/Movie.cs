namespace MovieFinder
{
    public class Movie
    {
        public Movie(string titile, string director)
        {
            Titile = titile;
            Director = director;
        }

        public string Titile { get; set; }
        public string Director { get; set; }
    }
}