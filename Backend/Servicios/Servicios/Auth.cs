﻿using AccesoDatos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Servicios
{
    public class Auth : IJtAuth
    {
        private readonly IConfiguration _config;
        private readonly DataBaseContext _context;
        public Auth(IConfiguration conf, DataBaseContext ctx)
        {
            _config = conf;
            _context = ctx; 
        }

        public string Autentication(string username, string password)
        {
            if (!ValidateUser(username, password))
            {
                return null;
            }
            else
            {
                var TokenHandler = new JwtSecurityTokenHandler();
                var TokenKey = Encoding.ASCII.GetBytes(_config["JWTKey"]);
                var TokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                        new Claim(ClaimTypes.Name,username)
                        }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(TokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = TokenHandler.CreateToken(TokenDescriptor);
                return (TokenHandler.WriteToken(token));
            }
        }
        public bool ValidateUser(string usuario, string password)
        {
            string pasw = Encrypt.GetSHA256(password);
            
                var user = _context.Usuarios.Where(u => u.Username == usuario && u.Password == pasw).FirstOrDefault();

                if (user != null)
                {
                    return true;
                }
            return false;
        }
        public LoginReturn GetUser(string usuario, string password)
        {
            if (ValidateUser(usuario, password))
            {
                string[] rolList = { };
                return new LoginReturn { roles = rolList, username = usuario };
            }
            else
            {
                return new LoginReturn { roles = { }, username = "none" };
            }
        }
    }
}
