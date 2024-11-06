using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioFrete.Models
{
    public record class JwtOptions
    (
        string Issuer,
        string Audience,
        string SigningKey,
        int ExpirationSeconds
    );
        
}