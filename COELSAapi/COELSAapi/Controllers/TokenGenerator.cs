using COELSAapi.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace COELSAapi.Controllers
{
    /// <summary>
    /// JWT Token generator class using "secret-key"
    /// more info: https://self-issued.info/docs/draft-ietf-oauth-json-web-token.html
    /// </summary>
    internal static class TokenGenerator
    {
        //public static string GenerateTokenJwt(User user)
        //{
        //    // appsetting for Token JWT
        //    var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
        //    var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
        //    var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
        //    var expireTime = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];

        //    var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
        //    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        //    // create a claimsIdentity
        //    ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) });
            

        //    // create token to the user
        //    var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        //    var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
        //        audience: audienceToken,
        //        issuer: issuerToken,
        //        subject: claimsIdentity,
        //        notBefore: DateTime.UtcNow,
        //        expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)),
        //        signingCredentials: signingCredentials);

        //    var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
        //    return jwtTokenString;
        //}

        // GENERAMOS EL TOKEN CON LA INFORMACIÓN DEL USUARIO
        public static string GenerarTokenJWT(User usuarioInfo)
        {
            // RECUPERAMOS LAS VARIABLES DE CONFIGURACIÓN
            var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
            var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
            var expireTime = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];


            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var _Header = new JwtHeader(signingCredentials);

            // CREAMOS LOS CLAIMS //
            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, usuarioInfo.Id.ToString()),
                new Claim("Nombre", usuarioInfo.Name),                
                new Claim(JwtRegisteredClaimNames.Email, usuarioInfo.Email),
                new Claim(ClaimTypes.Role, usuarioInfo.RoleName)
            };

            // CREAMOS EL PAYLOAD //
            var _Payload = new JwtPayload(
                    issuer: issuerToken,
                    audience: audienceToken,
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddHours(Convert.ToInt32(expireTime))
                );

            // GENERAMOS EL TOKEN //
            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }


    }
}