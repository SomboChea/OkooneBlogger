using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OkooneBlogger.Helpers
{
    public class Session : Controller
    {
        private static Session _instance = null;

        public Session()
        {
        }

        public static Session Get
        {
            get
            {
                if (_instance == null)
                    _instance = new Session();

                return _instance;
            }
        }

        public void Put(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }

        public void Put(string key, int value)
        {
            HttpContext.Session.SetInt32(key, value);
        }

        public string GetString(string key)
        {
            return HttpContext.Session.GetString(key);
        }

        public int? GetInt(string key)
        {
            return HttpContext.Session.GetInt32(key);
        }

        public void InitAuthentication()
        {
            Put(OkooneConstants.AUTH_ID, "");
            Put(OkooneConstants.AUTH_USERNAME, "");
        }
    }
}
