using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace N11Lib
{
    public class N11Service
    {
        private string serviceUrlBase = "";
        private string appKey = "67a73f10-3704-426e-ac9e-038a9a8cfcd0";
        private string appSecret = "";

        protected object getJsonData(string url)
        {
            var wr = WebRequest.Create(url);


        }

        protected object postJsonData(string url, Dictionary<string, string> paramTable, Dictionary<string, string> fileTable)
        {
            var wr = WebRequest.Create(url);


        }
    }
}
