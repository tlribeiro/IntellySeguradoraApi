using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntellySeguradoraApi.Configurations
{
    /// <summary>
    /// Configurações para JSon Web Token.
    /// </summary>
    public class JWTConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Minutes { get; set; }
    }
}
