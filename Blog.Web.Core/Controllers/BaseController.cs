using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Core.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BaseController : Controller
    {
    }
}