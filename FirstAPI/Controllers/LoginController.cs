using LoginLib.Login.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService;
        private ILogger<LoginController> _logger;


        public LoginController(ILoginService loginService, ILogger<LoginController> logger)
        {

            _loginService = loginService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<bool>> Authenticate(string username, string password) {


            try {
                _logger.LogInformation($"Entered Authenticate Controller: username = {username} and password = {password}");

                return await _loginService.Authenticate(username, password);
                 



            }
            catch (Exception ex) {

                _logger.LogError($"Error in Authenticate Controller: {ex.Message}  {ex.InnerException}");


                throw ex;
            
            }
        }




    }
}
