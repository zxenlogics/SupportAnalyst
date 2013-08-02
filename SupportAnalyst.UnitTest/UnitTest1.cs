using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportAnalyst.Model;
using SupportAnalyst.Repository;

namespace SupportAnalyst.UnitTest
{
    public class QueryResult
    {
        public int Count { get; set; }
        public string Msg { get; set; }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var samplesDb = new ESamplesDbContext();
            var pacDb = new PACDbContext();
            var samplesRepository = new LogRepository<ESamplesDbContext>(new ESamplesDbContext());

            Stopwatch timer = Stopwatch.StartNew();

            //var samplesCount = samplesDb.LogEntries.Where(w => w.LogType == "Error").ToList();
            var logEntry = samplesRepository.FindById(56984);

            //var samplesCount =
            //    samplesDb.LogEntries.Count(
            //        w =>
            //            w.Message.Contains("Performance") && w.TimeStamp >= new DateTime(2013, 5, 1, 0, 1, 0) &&
            //            w.TimeStamp <= new DateTime(2013, 7, 30, 23, 59, 59));

            //var entCount =
            //    pacDb.LogEntries.Count(
            //        w =>
            //            w.Message.Contains("Login") && w.TimeStamp >= new DateTime(2013, 7, 27, 0, 1, 0) &&
            //            w.TimeStamp <= new DateTime(2013, 7, 30, 23, 59, 59));

            timer.Stop();
            //Console.WriteLine("Stop: {0}, eSamples.Message: {1} - {2} - {3}", timer.Elapsed.Seconds, samplesCount[2].LogType, samplesCount[2].TimeStamp, samplesCount[2].Message);
            Console.WriteLine("Stop: {0}, eSamples.Message: {1}", timer.Elapsed.Seconds, logEntry.Message);
            //Console.WriteLine("Stop: {0}, Pac.Message: {1}", timer.Elapsed.Seconds, entCount);
        }

        [TestMethod]
        public void TestMethodAsync()
        {
            var samplesDb = new ESamplesDbContext();
            var pacDb = new PACDbContext();

            Stopwatch timer = Stopwatch.StartNew();

            Task<QueryResult> t1 = Task<QueryResult>.Factory.StartNew(() =>
            {
                var log4Count = samplesDb.LogEntries.Count(
                    w =>
                        w.Message.Contains("Performance") && w.TimeStamp >= new DateTime(2013, 7, 20, 0, 1, 0) &&
                        w.TimeStamp <= new DateTime(2013, 7, 30, 23, 59, 59));
                //timer.Stop();
                return new QueryResult() { Count = log4Count, Msg = "eSample Query returned: " };
            });


            Task<QueryResult> t2 = Task<QueryResult>.Factory.StartNew(() =>
            {
                //timer.Restart();
                var entCount =
                    pacDb.LogEntries.Count(
                        w =>
                            w.Message.Contains("Login") && w.TimeStamp >= new DateTime(2013, 7, 20, 0, 1, 0) &&
                            w.TimeStamp <= new DateTime(2013, 7, 30, 23, 59, 59));
                return new QueryResult() { Count = entCount, Msg = "Pac Query returned: " };
            });

            //Task t3 = t1.ContinueWith((prevTask) =>
            //    Console.WriteLine("Stop: {0}, eSamples.Message: {1}", timer.Elapsed.Seconds, prevTask.Result));

            //Task t4 = t2.ContinueWith((prevTask) =>
            //    Console.WriteLine("Stop: {0}, Pac.Message: {1}", timer.Elapsed.Seconds, prevTask.Result));

            //Task.WaitAll(t1, t2);
            Task<QueryResult>[] tasks = { t1, t2 };
            int index = Task.WaitAny(tasks);
            //int first = tasks[index].Result;

            timer.Stop();
            //Console.WriteLine("Stop: {0}, eSamples.Message: {1}", timer.Elapsed.Seconds, t1.Result);
            //Console.WriteLine("Stop: {0}, Pac.Message: {1}", timer.Elapsed.Seconds, t2.Result);

            Console.WriteLine("Elapsed time: {0}s, {1}{2}", timer.Elapsed.Seconds, tasks[index].Result.Msg, tasks[index].Result.Count);
        }

    }
}
