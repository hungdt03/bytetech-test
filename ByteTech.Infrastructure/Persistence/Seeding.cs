using System;
using ByteTech.Domain.Entities;
using ByteTech.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace ByteTech.Infrastructure.Persistence;

public class Seeding
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<MongoDbContext>();

        var adminUser = await context.Users.FindAsync(u => u.Role == EUserRole.Admin);
        if (await adminUser.AnyAsync())
        {
            return;
        }

        var hasher = new PasswordHasher<object>();
        var admin = new User
        {
            FullName = "Administrator",
            Email = "admin@gmail.com",
            PasswordHash = hasher.HashPassword(null!, "Admin@123"),
            Role = EUserRole.Admin,
            IsLocked = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await context.Users.InsertOneAsync(admin);  
    }
}
