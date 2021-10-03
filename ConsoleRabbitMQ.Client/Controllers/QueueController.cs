using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConsoleRabbitMQ.Client.Controllers
{
    [Route("api/[controller]")]

    public class QueueController:ControllerBase
    {
        [HttpPost]
        public async Task<string> AcceptMessageHello([FromBody]string message)
        {
            return await Task.Run(()=>$"Message - {message}");
        }

        [HttpPut]
        public async Task<string> AcceptMessageBye([FromBody] string message)
        {
            return await Task.Run(() => $"Message - {message}");
        }
    }
}
