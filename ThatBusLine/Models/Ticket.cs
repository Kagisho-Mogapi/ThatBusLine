namespace ThatBusLine.Models
{
    public class Ticket
    {
        public int ID { get; set; }

        //User id (Foreign Key)
        public int TicketOwner { get; set; }
        public bool IsUsed { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UsedAt { get; set; }
    }
}
