using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioFrete.Models
{
    public class RefreshTokenModel
    {
        public string AccessToken {get; set;}
        public string RefreshToken {get; set;}
    }
}