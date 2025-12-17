using Dapper;
using LoginLib.Login.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
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

        private IConfiguration _config;

        public LoginService(IConfiguration configuration) { 
        
              _config = configuration;
        
        }


        public async Task<bool> Authenticate(string username, string password)
        {
            try {



                bool result = false;


                using (SqlConnection con = new SqlConnection(_config.GetConnectionString("ApplicationDB")))
                {

                    var parameters = new DynamicParameters();
                    parameters.Add("@username", username);
                    parameters.Add("@password", password);

                    result = (await con.QueryAsync<bool>("CheckUser", parameters, commandType: CommandType.StoredProcedure)).ToList()[0];

                }

                return result;



            }
            catch (Exception ex) {

                throw ex;
            
            }
            
        }

        
        



    }
}
