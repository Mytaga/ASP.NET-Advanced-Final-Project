namespace PizzaOrderingSystem.Web.Areas.Administration.Controllers
{
    using PizzaOrderingSystem.Common;
    using PizzaOrderingSystem.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
