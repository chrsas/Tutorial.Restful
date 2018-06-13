using Microsoft.AspNetCore.Mvc;

namespace Tutorial.Restful.Controllers
{
    [Route("api/[controller]s")]
    public class PersonController : Controller
    {
        // GET
        public string Index()
        {
            return "person";
        }
    }
}