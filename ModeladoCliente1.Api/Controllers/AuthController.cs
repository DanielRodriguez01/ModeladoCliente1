using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModeladoCliente1.Api.Entidades;
using ModeladoCliente1.Shared.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ModeladoCliente1.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public AuthController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro(RegistroDto dto)
        {
            var usuario = new Usuario
            {
                UserName = dto.Email,
                Email = dto.Email
            };

            var resultado = await _userManager.CreateAsync(usuario, dto.Password);

            if (!resultado.Succeeded)
            {
                return BadRequest(resultado.Errors);
            }

            string rol = dto.Email.Contains("admin") ? "Admin" : "Usuario";
            await _userManager.AddToRoleAsync(usuario, rol);

            return Ok($"Usuario creado con rol: {rol}");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var resultado = await _signInManager.PasswordSignInAsync(
                dto.Email,
                dto.Password,
                false,
                false
            );

            if (!resultado.Succeeded)
            {
                return Unauthorized("Usuario o contraseña incorrectos");
            }

            // aqui se busca el usuario...
            var usuario = await _userManager.FindByEmailAsync(dto.Email);

            //  se crea claims (datos del usuario)
            var roles = await _userManager.GetRolesAsync(usuario);

            var claims = new List<Claim>
            {
              new Claim(ClaimTypes.Name, usuario.Email),
              new Claim(ClaimTypes.NameIdentifier, usuario.Id)
             };

            //  Aqui agrego roles al token
            foreach (var rol in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

            var claveSecreta = "MI_CLAVE_SUPER_ULTRA_SEGURA_PARA_JWT_2026_123456";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveSecreta));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // aqui creamos el token,..

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = tokenString });
        }
    }
}
