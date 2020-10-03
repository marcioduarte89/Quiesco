using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Products.Infrastructure.Data;
using System.Linq;

namespace Products.API.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase {

        private readonly ILogger<TestController> _logger;
        private readonly ProductsContext _context;

        public TestController(ILogger<TestController> logger, ProductsContext context) {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get() {

            var test = _context.Properties.Include(x => x.Rooms).FirstOrDefault();

            return Ok();
        }
    }
}
