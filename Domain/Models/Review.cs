namespace Domain.Models
{
    public class Review : BaseEntity
    {
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int FilmId { get; set; }
        public virtual Film Film { get; set; }
    }
}
