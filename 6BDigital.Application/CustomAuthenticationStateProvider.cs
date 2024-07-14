using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Claim = System.Security.Claims.Claim;
using System.Text;
using _6BDigital.Domain;
using System.IdentityModel.Tokens.Jwt;
using _6BDigital.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace _6BDigital.Application
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private string _Key = "5d081fbe-458a-4c59-9ff0-6f8e8b912d75";

        public ILocalStorageService _localStorageService { get; }
        public IUserApplication _userService { get; set; }

        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService, IUserApplication userService)
        {
            _localStorageService = localStorageService;
            _userService = userService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity identity = new ClaimsIdentity();

            string token = await _localStorageService.GetItemAsync<string>("AccessToken");

            if (!string.IsNullOrWhiteSpace(token))
            {
                User user = await GetUserFromAccessToken(token);

                if (user != null)
                {
                    identity = GetClaimsIdentity(user);
                }
            }

            var claimsPrincipal = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        public async Task MarkUserAsAuthenticated(User user)
        {
            await _localStorageService.SetItemAsync("AccessToken", GenerateAccessToken(user));

            var identity = GetClaimsIdentity(user);

            var claimsPrincipal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorageService.RemoveItemAsync("AccessToken");

            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GetClaimsIdentity(user),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<User> GetUserFromAccessToken(string accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_Key);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                SecurityToken securityToken;
                var principle = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);

                JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

                if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var name = principle.FindFirst(ClaimTypes.Name)?.Value;
                    var userName = principle.FindFirst(ClaimTypes.Email)?.Value;                    

                    return new User { UserName = userName, Name = name };
                }
            }
            catch (Exception)
            {
                return new User();
            }

            return new User();
        }

        public ClaimsIdentity GetClaimsIdentity(User user)
        {
            var claimsIdentity = new ClaimsIdentity();

            if (user.UserName != null)
            {
                claimsIdentity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Email, user.UserName ?? ""),
                        new Claim(ClaimTypes.Name, user.Name)                       
                    },
                    "apiauth_type");
            }

            return claimsIdentity;
        }
    }
}
