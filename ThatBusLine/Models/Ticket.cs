using System.ComponentModel;

namespace ThatBusLine.Models
{
    public class Ticket
    {
        public int ID { get; set; }

        //User id (Foreign Key)
        public string TicketOwner { get; set; }

        [DisplayName("Is Ticket Used")]
        public bool IsUsed { get; set; }

        public double Price { get; set; }

        [DisplayName("Bought At")]
        public DateTime CreatedAt { get; set; }

        [DisplayName("Used At")]
        public DateTime UsedAt { get; set; }
    }
}
