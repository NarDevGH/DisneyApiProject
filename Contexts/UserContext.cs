using DisneyApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DisneyApi.Contexts
{
    public class UserContext : IdentityDbContext<User>
    {
        // no hace falta un dbset, ya que se indica al heredar. IdentityDbContext<USER> 

        private const string Schema = "users";

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema(Schema);
        }
    }
}
