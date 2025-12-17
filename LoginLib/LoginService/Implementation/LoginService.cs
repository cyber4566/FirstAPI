using Dapper;
using LoginLib.Login.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private ILogger<LoginService> _logger;

        public LoginService(IConfiguration configuration,ILogger<LoginService> logger) { 
        
              _config = configuration;
            _logger = logger;
        
        }


        public async Task<bool> Authenticate(string username, string password)
        {
            try {



                bool result = false;


                using (SqlConnection con = new SqlConnection(_config.GetConnectionString("ApplicationDB")))
                {
                    _logger.LogInformation("Info logged");
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
