using ClanSystem.Models;
using ClanSystem.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ClanSystem.Controllers.ClientApi
{
    public class AuthHelper
    {
        public static string AUTH_TOKEN_HEADER = "Auth-token";
        public static string DEVICE_ID_HEADER = "DeviceID";

        public static AuthorizationToken CheckAuthorization(HttpRequestMessage request, ApplicationDbContext db) {

            try
            {
                string token = request.Headers.GetValues(AUTH_TOKEN_HEADER).FirstOrDefault();
                string deviceID = request.Headers.GetValues(DEVICE_ID_HEADER).FirstOrDefault();
                if (token != null && deviceID != null)
                {
                    Guid tokenGUID = Guid.Parse(token);
                    Guid deviceGUID = Guid.Parse(deviceID);
                    var dbToken = db.Tokens
                        .Where(t => (t.Value == tokenGUID) && (t.DeviceID == deviceGUID))
                        .FirstOrDefault();
                    return dbToken;
                }
            }
            catch (InvalidOperationException) { }
            catch (FormatException) { }


            return null;
        }

    }
}