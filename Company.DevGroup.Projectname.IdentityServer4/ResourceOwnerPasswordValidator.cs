using Company.DevGroup.Projectname.IdentityServer4.Repository;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Threading.Tasks;

namespace Company.DevGroup.Projectname.IdentityServer4
{
    /// <summary>
    /// 授权模式为密码模式，实现IResourceOwnerPasswordValidator
    /// </summary>
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        //repository to get user from db  
        private readonly IUserRepository _userRepository;

        public ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository; //DI  
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            string msg = "Invalid custom credential";
            try
            {
                if (context.UserName == "tracy" && context.Password == "123456") //用于测试
                {
                    context.Result = new GrantValidationResult(
                         subject: context.UserName,
                         authenticationMethod: OidcConstants.AuthenticationMethods.Password
                    );

                    return;
                }

                //根据context.UserName和context.Password与数据库的数据做校验，判断是否合法
                var user = await _userRepository.FindAsync(context.UserName);
                if (user == null)
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
                    return;
                }

                //check if password match - remember to hash password if stored as hash in db  
                if (user.Password == context.Password)
                {
                    //set the result  
                    context.Result = new GrantValidationResult(
                        subject: user.ID.ToString(),
                        authenticationMethod: OidcConstants.AuthenticationMethods.Password);

                    return;
                }

                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, msg);
            }
        }
    }
}
