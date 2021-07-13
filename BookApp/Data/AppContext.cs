using Microsoft.EntityFrameworkCore;

namespace BookApp.Data
{
   public class AppContext : DbContext
   {
      public AppContext() { }

      public AppContext(DbContextOptions<AppContext> options) : base(options) { }
   }
}
