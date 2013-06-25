using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTestProject1
{
    public class TestClassBase
    {
        public static void checkDoTest()
        {
            var value = System.Configuration.ConfigurationManager.AppSettings["DoTests"];
            if (!String.IsNullOrWhiteSpace(value))
            {
                Assert.IsTrue(value == "0");
            }
        }
    }
}
