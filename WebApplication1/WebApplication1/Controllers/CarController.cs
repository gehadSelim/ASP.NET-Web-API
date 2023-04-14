using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Filters;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;

        private static List<Car> cars= new List<Car>() ;

        public CarController(ILogger<CarController> logger)
        {
            _logger = logger;
           
        }

        [HttpGet]
        public ActionResult<List<Car>> Get()
        {
            _logger.LogInformation($"Get All - {DateTime.Now}");
            
            return cars;
            
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetById(int id)
        {
            Car car = cars.FirstOrDefault(c => c.Id == id);
            if(car == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(car);
            }
            
        }

        [HttpPost]
        [Route("v1")]
        public ActionResult Add_v1(Car car)
        {

            car.Id = cars.Count()+1;
            car.Type = "Gas";
            cars.Add(car);
            return CreatedAtAction(actionName: nameof(GetById), routeValues: new {id = car.Id},new GeneralResponse { Message = "Entity has been added successfully"});

        }

        [HttpPost]
        [Route("v2")]
        [ValidateCarType]
        public ActionResult Add_v2(Car car)
        {
           
            car.Id = cars.Count()+1;
            cars.Add(car);
            return CreatedAtAction(actionName: nameof(GetById), routeValues: new { id = car.Id }, new GeneralResponse { Message = "Entity has been added successfully" });

        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Put(int id, Car car)
        {
            
            if (car.Id != id)
            {
                return BadRequest();
            }
            Car existedCar = cars.FirstOrDefault(c => c.Id == id);
            if(car == null)
            {
                return NotFound();
            }
            existedCar.Model = car.Model;
            existedCar.Price = car.Price;
            existedCar.ProductionDate = car.ProductionDate;

            return CreatedAtAction(actionName: nameof(GetById), routeValues: new { id = car.Id }, new GeneralResponse { Message = "the entity has been updated successfully" });
            
        }

        
        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
           
            Car car = cars.FirstOrDefault(c=> c.Id == id);
            cars.Remove(car);
        }

        [HttpGet]
        [Route("count")]
        public ActionResult<int> GetCount()
        {         
            return Counter.count;
        }
    }
}
