using CashManager.Domain.Entities;
using FlexRepo.Repositories;

namespace CashManager.Infrastructure;

public class UsersRepository(CashManagerDbContext context) : Repository<User, Guid, CashManagerDbContext>(context)
{
}
