using JwtApp.Front.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace JwtApp.Front.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserLoginModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var requestContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:5135/Auth/SignIn", requestContent);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var tokenModel = JsonSerializer.Deserialize<JwtResponseModel>(jsonData, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(tokenModel?.Token);
                if (token != null)
                {
                    var claims = token.Claims.ToList();
                    claims.Add(new Claim("accessToken", tokenModel?.Token == null ? "" : tokenModel.Token));

                    ClaimsIdentity identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                    var authProps = new AuthenticationProperties
                    {
                        AllowRefresh = false,
                        ExpiresUtc = tokenModel?.ExpireDate,
                        IsPersistent = true,
                    };
                    await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProps);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");

                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");

                return View(model);
            }
        }
    }
}