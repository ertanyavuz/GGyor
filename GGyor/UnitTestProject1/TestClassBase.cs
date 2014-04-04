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
        /// <summary>
        /// Returning true means do not proceed with the current test.
        /// </summary>
        /// <returns></returns>
        public static bool checkDoTest()
        {
            var value = System.Configuration.ConfigurationManager.AppSettings["DoTests"];
            if (!String.IsNullOrWhiteSpace(value))
            {
                Assert.IsTrue(value == "0");
                return value == "0";
            }

            return false;
        }
    }
}
