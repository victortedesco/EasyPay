using EasyPay.Data;
using EasyPay.Models;

namespace EasyPay.Repositories;

public class UserRepository(DataContext dataContext) : Repository<User>(dataContext, dataContext.Users) { }