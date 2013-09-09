using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnitTestProject1.LibTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var str = "(x*2)+5";

            


            var list = new Dictionary<int, int>();

            var sw = new Stopwatch();

            sw.Start();
            for (int i = 0; i < 2000; i++ )
            {
                var exp = new NCalc.Expression(str);
                exp.Parameters["x"] = i;
                var result =  exp.Evaluate();

                list.Add(i, (int)result);
                break;
            }
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed.ToString());

            sw.Reset();
            list.Clear();
            sw.Start();
            for (int i = 0; i < 2000; i++)
            {
                var exp = new NCalc.Expression(str, NCalc.EvaluateOptions.NoCache);
                exp.Parameters["x"] = i;
                var result = exp.Evaluate();

                list.Add(i, (int)result);
            }
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed.ToString());

            sw.Reset();
            list.Clear();
            sw.Start();
            var exp2 = new NCalc.Expression(str);
            for (int i = 0; i < 2000; i++)
            {

                exp2.Parameters["x"] = i;
                var result = exp2.Evaluate();

                list.Add(i, (int)result);
            }
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed.ToString());


            sw.Reset();
            list.Clear();
            sw.Start();
            var exp1 = new NCalc.Expression(str, NCalc.EvaluateOptions.NoCache);
            for (int i = 0; i < 2000; i++)
            {
                
                exp1.Parameters["x"] = i;
                var result = exp1.Evaluate();

                list.Add(i, (int)result);
            }
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed.ToString());

            //System.Diagnostics.Debug.WriteLine(exp.Evaluate().ToString());

            Assert.IsTrue(true);

        }
    }
}
