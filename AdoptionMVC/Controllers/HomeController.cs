using AdoptionMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdoptionMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AdoptionDbContext _adoptionDbContext;

        public HomeController(ILogger<HomeController> logger, AdoptionDbContext context)
        {
            _logger = logger;
            _adoptionDbContext = context;
        }

        public IActionResult Index()
        {
            List<Animal> result = _adoptionDbContext.Animals.ToList();
            return View(result);
        }

        public IActionResult Results()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult AddAnimal(Animal a)
        {
            _adoptionDbContext.Animals.Add(a);
            _adoptionDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult AdoptAnimal(int id)
        {
            Animal a = _adoptionDbContext.Animals.FirstOrDefault(e => e.Id == id);
            _adoptionDbContext.Animals.Remove(a);
            _adoptionDbContext.SaveChanges();

            return RedirectToAction("Results");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}