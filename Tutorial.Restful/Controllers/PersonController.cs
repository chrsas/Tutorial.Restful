using System;
using Microsoft.AspNetCore.Mvc;

namespace Tutorial.Restful.Controllers
{
    [Route("api/[controller]s")]
    public class PersonController : Controller
    {
        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<string> Index(int id)
        {
            return Ok("person");
        }

        [NonAction]
        public ActionResult<DateTime> GetTime()
        {
            return Ok(DateTime.Now);
        }
    }
}