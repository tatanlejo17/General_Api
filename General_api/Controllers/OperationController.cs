using Microsoft.AspNetCore.Mvc;

namespace General_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        [HttpGet]
        public decimal add(decimal num1, decimal num2)
        {
            return num1 + num2;
        }

        [HttpPost]
        public decimal sub(Numbers numbers, [FromHeader] string Host, 
            [FromHeader(Name = "Content-length")] string contentLength,
            [FromHeader(Name = "x-some")] string some)
        {
            Console.WriteLine(Host);
            Console.WriteLine(contentLength);
            Console.WriteLine(some);
            return numbers.A - numbers.B;
        }
    }

    public class Numbers
    {
        public int A { get; set; }
        public int B { get; set; }
    }
}