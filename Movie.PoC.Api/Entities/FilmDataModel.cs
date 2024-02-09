﻿namespace Movie.PoC.Api.Entities
{
    public class FilmDataModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateOnly Year { get; set; }
        public string Rated { get; set; }
        public DateOnly Released { get; set; }
        public TimeSpan Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Awards { get; set; }
        public string Poster { get; set; }
        public Ratings[] Ratings { get; set; }
        public int Metascore { get; set; }
        public string imdbRating { get; set; }
        public string imdbVotes { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public DateOnly DVD { get; set; }
        public decimal BoxOffice { get; set; }
        public string Production { get; set; }
        public string Website { get; set; }
        public Guid FilmId { get; set; }
        public FilmModel AssociatedFilm { get; set; }    
    }
}