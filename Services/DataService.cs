using DevBlog5.Data;
using DevBlog5.Enums;
using DevBlog5.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DevBlog5.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BlogUser> _userManager;

        public DataService(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<BlogUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task ManageDataAsync()
        {
            //runs the migrations
            await _dbContext.Database.MigrateAsync();

            // seeds roles in task below
            await SeedRolesAsync();

            // seeds users in task below
            await SeedUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            // if roles exist do nothing
            if (_dbContext.Roles.Any())
            {
                return;
            }

            foreach (var role in Enum.GetNames(typeof(BlogRole)))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
        private async Task SeedUsersAsync()
        {
            if (_dbContext.Users.Any())
            {
                return;
            }

            var adminUser = new BlogUser()
            {
                Email = "sunday@sleepycafe.com",
                UserName = "sunday@sleepycafe.com",
                FirstName = "David",
                LastName = "Bellerose",
                DisplayName = "David Bellerose",
                PhoneNumber = "1234567890",
                EmailConfirmed = true,
            };

            await _userManager.CreateAsync(adminUser, "Abc&123!");
            await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());

            var modUser = new BlogUser()
            {
                Email = "david@sleepycafe.com",
                UserName = "david@sleepycafe.com",
                FirstName = "Moderator",
                LastName = "Smith",
                DisplayName = "Moderator",
                PhoneNumber = "1234567890",
                EmailConfirmed = true,
            };
            await _userManager.CreateAsync(modUser, "Abc&123!");
            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());
        }
    }
}
