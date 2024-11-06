namespace podstawy3g2.Models
{
    public class Car
    {
        public static List<Car> cars = new List<Car>();
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public Car() { }
        public Car(string brand, string model)
        {
            Id = NextId();
            Brand = brand;
            Model = model;
        }
        public int NextId()
        {
            return cars.Count == 0 ? 1 : cars.Max(x => x.Id) + 1;
        }
    }
}
