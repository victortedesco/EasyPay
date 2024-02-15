using EasyPay.Models;
using EasyPay.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EasyPay.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController(TransactionRepository repository) : Controller<Transaction>(repository)
    {
    }
}
