using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IJtAuth
    {
        public string Autentication(string username, string password);
        public LoginReturn GetUser(string username, string password);
    }
}
