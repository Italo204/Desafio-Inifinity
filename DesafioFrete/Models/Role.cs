using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioFrete.Models
{
    public class Role
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private List<string> _Permissions;

        public List<string> Permissions
        {
            get { return _Permissions; }
            set { _Permissions = value; }
        }

        public Role(string name)
        {
            Name = name;
            Permissions = new List<string>();
        }

        public void AddPermission(string permission)
        {
            if(!Permissions.Contains(permission))
            {
                Permissions.Add(permission);
            }
        }
    }
}