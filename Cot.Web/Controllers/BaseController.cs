using AspNetCoreHero.ToastNotification.Abstractions;
using Cot.Data.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Cot.Web.Controllers
{
    abstract class BaseController<T> : Controller where T : BaseController<T>
    {
        private IUnitOfWork unitOfWork;
        private INotyfService notifyService;

        protected IUnitOfWork UnitOfWork => unitOfWork ??= HttpContext.RequestServices.GetService<IUnitOfWork>();
        protected INotyfService NotyfService => notifyService ??= HttpContext.RequestServices.GetService<INotyfService>();

        public BaseController(IUnitOfWork unitOfWork, INotyfService notifyService)
        {
            this.unitOfWork = unitOfWork;
            this.notifyService = notifyService;
        }

        //private ILogger<T> _logger;
        //protected ILogger<T> Logger => _logger ?? (_logger = HttpContext.RequestServices.GetService<ILogger<T>>());
    }
}
