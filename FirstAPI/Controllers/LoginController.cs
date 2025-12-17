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


        public LoginController(ILoginService loginService) { 
        
               _loginService = loginService;
        
        
        }


        [HttpGet]
        public async Task<ActionResult<bool>> Authenticate(string username, string password) {


            try {

                return await _loginService.Authenticate(username, password);




            }
            catch (Exception ex) {


                throw ex;
            
            }
        }




    }
}
