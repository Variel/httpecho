using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HttpEcho.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HttpEcho.Services
{
    public class DatabaseContext : IdentityDbContext<HttpEchoUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<BinRequest> BinRequests { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BinRequest>()
                .Property(r => r.Headers)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, null),
                    v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, null));

            builder.Entity<BinRequest>()
                .HasOne(r => r.Endpoint)
                .WithMany(e => e.Requests)
                .HasForeignKey(r => new { r.UserId, r.EndpointId })
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Comment>()
                .HasOne(c => c.Request)
                .WithMany(r => r.Comments)
                .HasForeignKey(c => c.RequestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Comment>()
                .HasOne(c => c.Writer)
                .WithMany()
                .HasForeignKey(c => c.WriterId)
                .OnDelete(DeleteBehavior.ClientCascade);


            builder.Entity<Endpoint>()
                .HasKey(e => new {e.UserId, e.Id});

            builder.Entity<Endpoint>()
                .HasOne(e => e.User)
                .WithMany(u => u.Endpoints)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
