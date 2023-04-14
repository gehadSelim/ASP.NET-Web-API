using WebApplication1.Validations;

namespace WebApplication1.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }

        [ProductionDateInPast]
        public DateTime ProductionDate { get; set; }
        public string Type { get; set; }

    }
}
