namespace MenuSiparis.Core.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BookingDate { get; set; }
        public int PersonCount { get; set; }
    }
}
