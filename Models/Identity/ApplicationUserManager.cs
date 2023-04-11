using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NGOAbhiudai2023.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NGOAbhiudai2023.Models.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private readonly IDapperRepository _dapperRepository;
        public ApplicationUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger, IDapperRepository dapperRepository) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _dapperRepository = dapperRepository;
        }

        public async Task<ApplicationUser> FindByMobileNoAsync(string mobileNo)
        {
            await Task.Delay(0);
            return new ApplicationUser();
        }

        public Task FindByMobileNoAsync()
        {
            throw new NotImplementedException();
        }
        public override async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword)
        {
            var result = IdentityResult.Failed();
            try
            {
                var res = await base.ResetPasswordAsync(user, token, newPassword);
                if (!string.IsNullOrEmpty(user.PasswordHash))
                {
                    string sqlQuery = @"update [dbo].[Users] set PasswordHash=@PasswordHash where Id=@Id";
                    int i = await _dapperRepository.ExecuteAsync(sqlQuery, new { user.Id, user.PasswordHash }, commandType: CommandType.Text);
                    if (i > 0)
                    {
                        result = IdentityResult.Success;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
