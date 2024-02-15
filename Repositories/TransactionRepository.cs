using EasyPay.Data;
using EasyPay.Models;

namespace EasyPay.Repositories;

public class TransactionRepository(DataContext dataContext) : Repository<Transaction>(dataContext, dataContext.Transactions) { }