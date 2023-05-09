using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace AccesoDatos
{
    public partial class LoginReturn
    {
        public string[] roles { get; set; }
        public string username { get; set; }
        public string token { get; set; }
        public string Nombres { get; set; }
        public string apellidos { get; set; }
    }

    public partial class Login
    {
        public string usuario { get; set; }
        public string password { get; set; }
        public string username { get; set; }
    }
}
