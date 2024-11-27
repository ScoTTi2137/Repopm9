using Microsoft.AspNetCore.Mvc;
using podstawy3g2.Models;
using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace podstawy3g2.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            if (Car.cars.Count == 0)
            {
                Car.cars.Add(new Car { Id = 1, Brand = "BMW", Model = "x7" , EngineId = 1});
                Car.cars.Add(new Car { Id = 2, Brand = "Audi", Model = "a6", EngineId = 2 });
                Car.cars.Add(new Car { Id = 3, Brand = "Fiat", Model = "tipo", EngineId = 3});
            }
            if(Engine.list.Count == 0)
            {
                Engine.list.Add(new Engine { Id = 1, Name = "Diesel" });
                Engine.list.Add(new Engine { Id = 2, Name = "Benzin" });
                Engine.list.Add(new Engine { Id = 3, Name = "Hybrid" });
            }
        }
        public IActionResult Index()
        {
            ViewBag.EngineList = Engine.list;
            ViewBag.EngId = 0;
            return View(Car.cars);
        }
        [HttpPost]
        public IActionResult Index(int EngId)
        {
            ViewBag.EngineList = Engine.list;
            ViewBag.EngId = EngId;
            var list = Car.cars.Where(x => x.EngineId == EngId).ToList();
            if (list.Count == 0)
                list = Car.cars;
            return View(list);
        }
        public IActionResult AddCar()
        {
            ViewBag.Engines = Engine.list.Select(x => new SelectListItem
            {
                Text = x.Name, 
                Value = x.Id.ToString()
            }).ToList();
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
            if(car !=null)
            {
                ViewBag.Engines = Engine.list.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = x.Id == car.EngineId
                }).ToList();
                return View(car);
            }
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
                updateCar.EngineId = car.EngineId;
            }
            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult Details(int id)
        {
            Car car = Car.cars.FirstOrDefault(x => x.Id == id);
            if (car != null)
            {
                car.Engine = Engine.list.FirstOrDefault(x => x.Id == car.EngineId);
                return View(car);
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ShowCar(int engId)
        {
            var cars = Car.cars.Where(c => c.EngineId == engId).Select(c => c.Brand).Distinct().ToList();
            ViewBag.EngineList = Engine.list;
            ViewBag.SelectedEngineId = engId;
            return View(cars);
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
