using BusinessManager.Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Infrastructure.Context
{
    public class BussinesContextFactory(IOptions<BusinessDbSettings> options) : IDesignTimeDbContextFactory<BusinessContext>
    {
        private readonly BusinessDbSettings _settings = options.Value;
        public BusinessContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BusinessContext>();
            optionsBuilder.UseSqlServer(_settings.ConnectionString);

            return new BusinessContext(optionsBuilder.Options);
        }
    }

}
