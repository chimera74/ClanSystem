using ClanSystem.Controllers.ClientApi;
using ClanSystem.Models;
using ClanSystem.Models.DAL;
using ClanSystem.Models.WebApiModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace ClanSystem.Controllers
{
    /*
    Ресурс для управления токенами.
    */
    public class LoginController : ApiController
    {       

        private ApplicationDbContext db;
        private UserManager<ApplicationUser> UserManager { get; set; }

        public LoginController()
        {            
            db = new ApplicationDbContext();            
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        /*
        Получение токена (вход). 
        */
        [HttpPost]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> PostLogin([FromBody] LoginDetails logindDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var user = await UserManager.FindAsync(logindDetails.UserName, logindDetails.Password);
            if (user != null)
            {
                var token = db.Tokens
                    .Where(t => (t.UserID == user.Id) && (t.DeviceID == logindDetails.DeviceID))
                    .FirstOrDefault();
                if (token == null)
                {
                    token = new AuthorizationToken() { User = user, DeviceID = logindDetails.DeviceID, Value = Guid.NewGuid() };
                    db.Tokens.Add(token);
                    await db.SaveChangesAsync();
                }
                return Ok(token.Value.ToString());
            }

            return StatusCode(HttpStatusCode.Forbidden);
        }

        [HttpGet]
        [ResponseType(typeof(LoginDetails))]
        public LoginDetails Get()
        {
            LoginDetails result = new LoginDetails() { UserName = "example", DeviceID = new Guid(), Password = "example" };            
            return result;
        }

        /*
        Удаление токена (выход).
        */
        [HttpDelete]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> DeleteLogin()
        {
            AuthorizationToken token = AuthHelper.CheckAuthorization(Request, db);
            if (token != null)
            {
                db.Tokens.Remove(token);
                await db.SaveChangesAsync();
                return Ok();
            }

            return StatusCode(HttpStatusCode.Forbidden);

        }

    }
}
