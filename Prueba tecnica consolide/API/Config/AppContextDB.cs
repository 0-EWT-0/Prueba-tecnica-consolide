using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Config;

public class AppContextDB : DbContext
{
    public AppContextDB(DbContextOptions<AppContextDB> options) : base(options)
    {
    }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<SessionModel> Session { get; set; }
    public DbSet<OperationModel> Operation { get; set; }

}