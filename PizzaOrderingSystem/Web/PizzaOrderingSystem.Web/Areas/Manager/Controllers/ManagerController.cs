namespace PizzaOrderingSystem.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PizzaOrderingSystem.Common;
    using PizzaOrderingSystem.Web.Controllers;

    [Authorize(Roles = GlobalConstants.ManagerRoleName)]
    [Area("Manager")]
    public class ManagerController : BaseController
    {

    }
}
