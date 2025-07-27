using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.JWT
{
    public class JwtConfig
    {
        public class JWT
        {
            private String _validAudience;
            private String _validIssuer;
            private String _secret;
            public string ValidAudience { get => _validAudience; set => _validAudience = value; }
            public string ValidIssuer { get => _validIssuer; set => _validIssuer = value; }
            public string Secret { get => _secret; set => _secret = value; }
        }
    }
}
