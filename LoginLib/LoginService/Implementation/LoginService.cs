using Dapper;
using LoginLib.Login.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginLib.Login.Implementation
{
    public class LoginService : ILoginService
    {
        public async Task<bool> Authenticate(string username, string password)
        {

            bool result = false;

            using (SqlConnection con = new SqlConnection()) {

                var parameters = new DynamicParameters();
                parameters.Add("@username", username);
                parameters.Add("@password", password);

                result = (await con.QueryAsync<bool>("UserCheck",parameters,commandType:CommandType.StoredProcedure)).ToList()[0];
            
            }

            return result;
            
        }

        
        



    }
}
