using System;
using Xunit;
using Xunit.Abstractions;
using System.Threading;

namespace TeamHobby.UserManagement.xTests
{
    public class UnitTest1
    {
        
        ITestOutputHelper output;
        public Example(ITestOutputHelper output)
        {
            this.output = output;  
        }
        [Fact]
        public void SingleOperationinFiveSec() 
        {
            DateTime start = DateTime.Now;
            Thread.Sleep(5000);
            // create a file
            DateTime end = DateTime.Now;  
            TimeSpan timeSpan = (end - start);
            var sec = timeSpan.TotalSeconds;
            //throw new Exception(sec.ToString());
            if (sec > 1)
            {
                throw new Exception("Greater than 5 seconds");
            }
            throw new Exception("Less than 5 seconds");
            //output.WriteLine("check");
            //output.WriteLine("Elapsed time is {0} ms", timeSpan.TotalSeconds); 
            
            var s = DateTime.Now;
            output.WriteLine(s);
            output.WriteLine("Time is ", DateTime.Now);
            aTimer.Start();

            Assert.True(false);
            aTimer.Stop(); 
            var e = DateTime.Now;
            var diff = s - e;
            output.WriteLine("Diff time ", diff);
            Assert.True(true);
        }
    }
}