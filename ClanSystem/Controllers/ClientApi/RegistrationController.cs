using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClanSystem.Controllers.ClientApi
{
    public class RegistrationController : ApiController
    {
        /*
        Регистрация.
        */
        [HttpPost]
        public string PostLogin()
        {
            return "test";
        }
    }
}
