using ECommerce.Core.Interfaces;
using ECommerce.Core.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositories
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(IUserRepository userRepository, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Kullanıcı adı ve şifre ile kullanıcı doğrulaması yapan metot
        public async Task<User> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return null;
            }

            var passwordVerificationResult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (passwordVerificationResult != PasswordVerificationResult.Success)
            {
                return null;
            }

            return user;
        }

        // Kullanıcı oluşturma metodu
        public async Task<string> CreateUserAsync(string username, string email)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                return "Kullanıcı zaten mevcut.";
            }

            var user = new User
            {
                UserName = username,
                Email = email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };

            // Sabit şifreyi belirleyelim
            string password = "123456";  // Burada istediğiniz şifreyi belirleyebilirsiniz

            // Şifreyi hash'leyelim
            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, password); // Hash'lenmiş şifreyi atıyoruz

            // Kullanıcıyı oluştur
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                // Role ekleyelim
                var roleResult = await _userManager.AddToRoleAsync(user, "Admin");
                if (!roleResult.Succeeded)
                {
                    return "Role atama hatası: " + string.Join(", ", roleResult.Errors.Select(e => e.Description));
                }

                return "Kullanıcı başarıyla oluşturuldu ve role eklendi.";
            }
            else
            {
                return "Kullanıcı oluşturulamadı: " + string.Join(", ", result.Errors.Select(e => e.Description));
            }
        }
    }
}
