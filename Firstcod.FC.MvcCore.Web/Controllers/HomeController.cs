using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firstcod.FC.MvcCore.Web.Connector;
using Firstcod.FC.Provider;
using Firstcod.FC.Provider.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Firstcod.FC.MvcCore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<HubConnector, IHubConnector> _hub;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IUnitOfWork unitOfWork, IHubContext<HubConnector, IHubConnector> hub, ILogger<HomeController> logger)
        {
            _unitOfWork = unitOfWork;
            _hub = hub;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post(string account, string amount)
        {
            try
            {
                Transaction transaction = new Transaction()
                {
                    Account = account,
                    Amount = decimal.Parse(amount),
                    TransactionHash = Guid.NewGuid().ToString(),
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    StateId = 1
                };

                _unitOfWork.Transaction.Add(transaction);
                _unitOfWork.Transaction.Save();

                await _hub.Clients.All.Notify("Insert");

                return Json(new { message = "Insert Form!" });
            }
            catch(Exception e)
            {
                _logger.LogError("Error Web: " + e.Message);
                return Json(new { message = e.Message });
            }
        }

        public IActionResult Transaction()
        {
            return View(_unitOfWork.Transaction.GetAll().ToList());
        }

        public IActionResult Edit(long id)
        {
            return View(_unitOfWork.Transaction.FindById(s => s.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long id, string account, string amount)
        {
            var response = _unitOfWork.Transaction.FindById(s => s.Id == id);

            if (response != null)
            {
                response.Account = account;
                response.Amount = decimal.Parse(amount);
                response.UpdateDate = DateTime.Now;

                _unitOfWork.Transaction.Update(response);
                _unitOfWork.Transaction.Save();

                await _hub.Clients.All.Notify("Update completed!");

                return Redirect("~/home/transaction");
            }
            else
                return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var response = _unitOfWork.Transaction.FindById(s => s.Id == id);

            if (response != null)
            {
                _unitOfWork.Transaction.Delete(response);
                _unitOfWork.Transaction.Save();

                await _hub.Clients.All.Notify("Delete completed!");

                return Json(new { value = 1, message = "Delete completed!" });
            }
            else
                return Json(new { value = 0, message = "?" });
        }

        public PartialViewResult _transaction()
        {
            return PartialView("_transaction", _unitOfWork.Transaction.GetAll().ToList());
        }
    }
}