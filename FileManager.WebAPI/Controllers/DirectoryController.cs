using FileManager.WebAPI.Helpers;
using FileManager.WebAPI.Models;
using FileManager.WebAPI.Models.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FileManager.WebAPI.Controllers
{
    [EnableCorsAttribute("http://localhost:50202", "*", "*")]
    public class DirectoryController : ApiController
    {
        public DirectoryM Get()
        {
            return Get("D:\\");
        }

        public DirectoryM Get(string id)
        {
            var dirM = DirectoryHelper.GetDirectoryModelByPath(id);

            return dirM;
        }
    }
}
