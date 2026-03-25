using DSO_Email_OTP.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DSO_Email_OTP.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<EmailOtpModel> EmailOtpModels { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
