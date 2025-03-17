using System.ComponentModel.DataAnnotations;

namespace MarkRent.Domain.Entities
{
    public class Vehicle
    {
        public Vehicle()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public Guid FutureEventId { get; set; }
        public FutureEvent FutureEvent { get; set; }
    }
}
