using DSO_Email_OTP.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DSO_Email_OTP.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<EmailOtp> EmailOtps { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
