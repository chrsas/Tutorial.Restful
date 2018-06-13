using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Tutorial.Restful.Controllers.Dto;

namespace Tutorial.Restful.Host.Controllers
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
        public ActionResult<string> Get(int id)
        {
            return Ok("person");
        }

        [HttpGet]
        public List<PersonDto> Get(string name)
        {
            return new List<PersonDto>() {
                new PersonDto
                {
                    Id = 1,
                    Name = "王二",
                }, new PersonDto
                {
                    Id = 2,
                    Name = "张三",
                } };
        }

        public IActionResult Post([FromBody]PersonDto person)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok();
        }
    }
}