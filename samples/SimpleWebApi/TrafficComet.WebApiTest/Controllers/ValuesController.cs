using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TrafficComet.Abstracts.Accessors;

namespace TrafficComet.WebApiTest.Controllers
{
    [Route("api/[controller]")]
	public class ValuesController : Controller
	{
		// GET api/values
		[HttpGet]
		public IEnumerable<string> Get([FromServices]ITrafficCometMiddlewaresAccessor cometAccessor)
		{
			cometAccessor.CustomParams.Add("Test custom param", "test value");
			cometAccessor.RequestCustomParams.Add("Request test custom param", "test value");
			cometAccessor.ResponseCustomParams.Add("Response test custom param", "test value");

			return new string[] { "value1", "value2" };
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost]
		public IActionResult Post([FromBody]dynamic value)
		{
            return Json(value);
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}