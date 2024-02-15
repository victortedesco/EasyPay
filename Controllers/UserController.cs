using EasyPay.Models;
using EasyPay.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EasyPay.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController(UserRepository repository) : Controller<User>(repository)
    {
    }
}
