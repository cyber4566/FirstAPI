using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginLib.Login.Interface
{
    public interface ILoginService
    {

        public Task<bool> Authenticate(string username, string password);



    }
}
