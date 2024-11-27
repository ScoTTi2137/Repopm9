namespace podstawy3g2.Models
{
    public class Car
    {
        public static List<Car> cars = new List<Car>();
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int EngineId { get; set; }
        public Engine Engine { get; set; }

        public Car() { }
        public Car(string brand, string model)
        {
            Id = NextId();
            Brand = brand;
            Model = model;
        }
        public int NextId()
        {
            if (Car.cars.Count == 0)
                return 1;
            else
                return Car.cars.Max(car => car.Id) + 1;
        }
    }
}