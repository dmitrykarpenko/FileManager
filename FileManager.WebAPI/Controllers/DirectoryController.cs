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
    //[EnableCorsAttribute("http://localhost:50202", "*", "*")]
    public class DirectoryController : ApiController
    {
        public DirectoryM Get()
        {
            return Get("F:\\Video");
        }

        public DirectoryM Get(string id)
        {
            var dirM = DirectoryHelper.GetDirectoryModelByPath(id);

            //var encodedPath = Uri.EscapeUriString("C:\\ololo as;ojf;sdfj\\oasjdfsj");

            var dummyDir = new DirectoryM() { Name = "Dummy", Subdirectories =
                new List<DirectoryM>() { new DirectoryM() { Name = "Dummy subdir" },
                                         new DirectoryM() { Name = "Dummy subdir 2" } } };

            return dirM;
        }
    }
}
