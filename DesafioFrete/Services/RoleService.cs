using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using DesafioFrete.Models;

namespace DesafioFrete.Services
{
    public class RoleService
    {
        const string FilePath = "../config/json/RolePermissions.json";
        private Dictionary<string, Role> _Roles;

        public RoleService()
        {
            _Roles = new Dictionary<string, Role>();
            LoadRoles();
        }

        public Dictionary<string, Role> Roles => _Roles;

        public void AddRole(Role role)
        {
            if(!_Roles.ContainsKey(role.Name))
            {
                _Roles[role.Name] = role;
                SaveRoles();
            }
        }

        public void LoadRoles()
        {
            if(File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                var roleDict = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json);
                if(roleDict != null)
                {
                    foreach (var kvp in roleDict)
                    {
                        var role = new Role(kvp.Key)
                        {
                            Permissions = kvp.Value
                        };
                        _Roles[kvp.Key] = role;
                    }
                }
            }
        }

        public void SaveRoles()
        {
            var roleDict = new Dictionary<string, List<string>>();

            foreach(var role in _Roles.Values)
            {
                roleDict[role.Name] = role.Permissions;
            }

            var json = JsonSerializer.Serialize(roleDict, new JsonSerializerOptions { WriteIndented = true});
            File.WriteAllText(FilePath, json);
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(_Roles.ToDictionary(r => r.Key, r => r.Value.Permissions));
        }
    }
}