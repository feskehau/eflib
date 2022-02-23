using HSS.Storage.Core;
using Microsoft.EntityFrameworkCore;

    public class HssDbContext : DbContext, IDbContext
    {
        public HssDbContext (DbContextOptions<HssDbContext> options)
            : base(options)
        {
        }

    }