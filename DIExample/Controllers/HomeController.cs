using Autofac;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace DIExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICitiesService _citiesService1;
        private readonly ICitiesService _citiesService2;
        private readonly ICitiesService _citiesService3;
        //private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILifetimeScope _lifeTimeScope; // by Autofac

        // Constructor Injected
        public HomeController(ICitiesService citiesService1, 
                              ICitiesService citiesService2, 
                              ICitiesService citiesService3,
                              ILifetimeScope lifeTimeScope)
        {
            _citiesService1 = citiesService1;
            _citiesService2 = citiesService2;
            _citiesService3 = citiesService3;
            //_serviceScopeFactory = serviceScopeFactory;
            _lifeTimeScope  = lifeTimeScope;
        }

        [Route("/")]
        //public IActionResult Index([FromServices] ICitiesService citiesService)
        public IActionResult Index()
        {
            List<string> cities = _citiesService1.GetCities();

            ViewBag.InstanceId_CitiesService_1 = _citiesService1.ServiceInstanceId;
            
            ViewBag.InstanceId_CitiesService_2 = _citiesService2.ServiceInstanceId;

            ViewBag.InstanceId_CitiesService_3 = _citiesService3.ServiceInstanceId;

            // Entity FW is managed so U can diposed this way
            // You can normally create scope by this for connecting DB like ADO.NET
            //using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            using (ILifetimeScope scope = _lifeTimeScope.BeginLifetimeScope()) 
            {
                // Inject CitiesService 
                ICitiesService citiesService = scope.Resolve<ICitiesService>();

                // DB work

                ViewBag.InstanceId_CitiesService_InScope = citiesService.ServiceInstanceId;

            } // end of scope; it calls CitiesService.Dispose()

            return View(cities);
        }
    }
}
