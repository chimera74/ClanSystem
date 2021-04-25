using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ClanSystem.Models;
using ClanSystem.Models.DAL;
using ClanSystem.Models.WebApiModels;

namespace ClanSystem.Controllers.ClientApi
{
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //Поучение оповещений 
        // GET: api/Notifications/?dateTime=2015-10-01T12:40:55.5526033Z
        [ResponseType(typeof(List<JsonNotification>))]
        [HttpGet]
        public async Task<IHttpActionResult> GetNotification(DateTime dateTime)
        {
            var token = AuthHelper.CheckAuthorization(Request, db);
            if (token != null)
            {                
                var notifications =
                    from n in db.Notifications
                    from ur in n.UserRecipients
                    from gr in n.GroupRecipients
                    from gur in gr.Users
                    where (ur.Id == token.UserID
                        || gur.Id == token.UserID)
                        && n.SendTime > dateTime
                        && n.SendTime < DateTime.UtcNow
                    select n;

                if (notifications.Count() == 0)
                {
                    return NotFound();
                }

                List<JsonNotification> results = new List<JsonNotification>();
                foreach (var n in notifications)
                {
                    results.Add(new JsonNotification(n));
                }

                return Ok(results);
            }

            return StatusCode(HttpStatusCode.Forbidden);
            
        }

        // GET: api/Notifications        
        public List<JsonNotification> GetNotifications()
        {
            List<JsonNotification> results = new List<JsonNotification>();
            results.Add(new JsonNotification() { SendTime = DateTime.UtcNow, Author = "example", Title = "example", Content = "example content", ID = 123, Importance = Importance.Default });
            results.Add(new JsonNotification() { SendTime = DateTime.UtcNow, Author = "example", Title = "example", Content = "example content", ID = 123, Importance = Importance.Default });
            return results;
        }
    }
}