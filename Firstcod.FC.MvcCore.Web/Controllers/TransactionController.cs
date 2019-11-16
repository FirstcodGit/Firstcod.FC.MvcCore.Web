using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firstcod.FC.MvcCore.Web.Connector;
using Firstcod.FC.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Firstcod.FC.MvcCore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TransactionController> _logger;
        private readonly IHubContext<HubConnector, IHubConnector> _hub;

        public TransactionController(IUnitOfWork unitOfWork, ILogger<TransactionController> logger,
            IHubContext<HubConnector, IHubConnector> hub)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _hub = hub;
        }

        // GET: api/Transaction
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var count = _unitOfWork.Transaction.GetAll().Count();

            //_logger.LogInformation("Transaction count: " + count);

            await _hub.Clients.All.Notify("Transaction count: " + count);

            return new string[] { "value1", "value2" };
        }

        // GET: api/Transaction/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Transaction
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Transaction/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
