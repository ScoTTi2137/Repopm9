using Microsoft.AspNetCore.Mvc;
using podstawy3g2.Models;
using System.Diagnostics;

namespace podstawy3g2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            if(Car.cars.Count == 0)
            {
                Car.cars.Add(new Car { Id = 1, Brand = "BMW", Model = "x7" });
                Car.cars.Add(new Car { Id = 2, Brand = "Audi", Model = "a6" });
                Car.cars.Add(new Car { Id = 3, Brand = "Fiat", Model = "tipo" });
            }
        }

        public IActionResult Index()
        {
            //Car car = new Car();
            //car.Id = 0;
            //car.Brand = "BMW";
            //car.Model = "x7";

            //List<Car> cars = new List<Car>();
            

            return View(Car.cars);
        }

        public IActionResult AddCar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCar(Car car)
        {
            car.Id = car.NextId();
            Car.cars.Add(car);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult DeleteCar(int id)
        {
            Car car = Car.cars.FirstOrDefault(x => x.Id == id);
            if(car != null)
                Car.cars.Remove(car);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult UpdateCar(int id)
        {
            Car car = Car.cars.FirstOrDefault(x => x.Id == id);
            if (car != null)
                return View(car);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult UpdateCar(int id, Car car)
        {
            Car updateCar = Car.cars.FirstOrDefault(x => x.Id == car.Id);
            if (updateCar != null)
            {
                updateCar.Brand = car.Brand;
                updateCar.Model = car.Model;
            }

            return RedirectToAction("Index", "Home");   
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
