namespace Movie.PoC.Api.Entities
{
    public class FilmDataModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ContentRating Rated { get; set; }
        public DateOnly Released { get; set; }
        public TimeSpan Runtime { get; set; }
        public List<MediaGenre> Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public MediaLanguage Language { get; set; }
        public MediaCountry Country { get; set; }
        public string Poster { get; set; }
        public int Metascore { get; set; }
        public double imdbRating { get; set; }
        public int imdbVotes { get; set; }
        public string imdbID { get; set; }
        public MediaType Type { get; set; }
        public string? Production { get; set; }
        public string? Website { get; set; }
        public FilmModel AssociatedFilm { get; set; }
    }

    public enum MediaType
    {
        Movie,
        Series,
        Episode,
        Game,
        Short,
        TVMovie,
        TVSeries,
        TVEpisode,
        TVShort,
        TVMiniSeries,
        TVSpecial,
        Video,
        Other
    }
    public enum MediaCountry
    {
        USA,
        UK,
        Canada,
        Australia,
        Germany,
        France,
        Japan,
        SouthKorea,
        India,
        China,
        Other
    }
    public enum MediaLanguage
    {
        English,
        Spanish,
        French,
        German,
        Mandarin,
        Hindi,
        Arabic,
        Japanese,
        Korean,
        Italian,
        Portuguese,
        Russian,
        Dutch,
        Swedish,
        Turkish,
        Polish,
        Other
    }
    public enum ContentRating
    {
        G,
        PG,
        PG13,
        R,
        NC17,
        NotRated,
        Unrated,
        TVY,
        TVY7,
        TVY7FV,
        TVG,
        TVPG,
        TV14,
        TVMA
    }
    public enum MediaGenre
    {
        Action,
        Adventure,
        Animation,
        Biography,
        Comedy,
        Crime,
        Documentary,
        Drama,
        Family,
        Fantasy,
        FilmNoir,
        History,
        Horror,
        Music,
        Musical,
        Mystery,
        Romance,
        SciFi,
        Sport,
        Thriller,
        War,
        Western,
        Other
    }
}
