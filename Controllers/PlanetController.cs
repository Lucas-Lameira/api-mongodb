//hit controll . to add dependencies

namespace apidotnet.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class PlanetController : ControllerBase
    {
        Data.MongoDB _mongoDB; //essa é a class criada XD 
        IMongoCollection<Planet> _planetCollection;

        public PlanetController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _planetCollection = _mongoDB.DB.GetCollection<Planet>(typeof(Planet).Name.ToLower());
        }

        //endpoint
        [HttpPost]
        public ActionResult SavePlanet([FromBody] PlanetDto dto) //do body da req
        {
            var planet = new Planet(dto.Name, dto.Galaxy, dto.Size);

            _planetCollection.InsertOne(planet);
            
            return StatusCode(201, "Registered planet, let me know if you find a new one!");
        }

        //endpoint
        [HttpGet]
        public ActionResult GetPlanets()
        {
            var planets = _planetCollection.Find(Builders<Planet>.Filter.Empty).ToList();
            
            return Ok(planets); //status code: 200
        }
    }
}