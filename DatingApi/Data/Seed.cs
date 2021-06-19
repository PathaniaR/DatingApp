using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DatingApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApi.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DatingDataContext datacontext)
        {
            if(await datacontext.AppUsers.AnyAsync()) return;

            var userData= await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users=JsonSerializer.Deserialize<List<AppUser>>(userData);
            users.ForEach(userData=>{
                using var hmac = new HMACSHA512();
                userData.UserName=userData.UserName.ToLower();
                userData.PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes("Password"));
                userData.PasswordSalt=hmac.Key;

                 datacontext.AppUsers.Add(userData);
            });

            await datacontext.SaveChangesAsync();
        }
    } 
}