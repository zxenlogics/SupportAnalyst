using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportAnalyst.Model;
using SupportAnalyst.Data;

namespace SupportAnalyst.UnitTest
{
    public class QueryResult
    {
        public int Count { get; set; }
        public string Msg { get; set; }
    }

    [TestClass]
    public class DataLayerTest
    {
        ILogRepository samplesRepository;
        ILogRepository pacRepository;

        public DataLayerTest()
        {
            //Database.SetInitializer<ESamplesDbContext>(null);
            //Database.SetInitializer<PACDbContext>(null);
            samplesRepository = new LogRepository(new ESamplesDbContext());
            pacRepository = new LogRepository(new PACDbContext());
        }

        [TestMethod]
        public void FluentTest()
        {
            var criteria = new QueryCriteria();

            Criteria d = criteria.EventType("Error")
                .EventStartTime(DateTime.Now)
                .EventEndTime(DateTime.Now)
                .MessageKeyword("SessionId").Instance();

            Console.WriteLine(d.GetExpression().ToString());
        }

        #region << LogRepository.FindByCriteria() >>

        [TestMethod]
        public void FindByCriteriaTest()
        {
            Criteria criteria = new QueryCriteria()
                .EventType("INFO")
                .EventStartTime(DateTime.Now)
                .EventEndTime(DateTime.Now.AddDays(-10))
                .MessageKeyword("Method").Instance();

            Stopwatch timer = Stopwatch.StartNew();

            var samplesLogEntry = samplesRepository.FindByCriteria(criteria).ToList();
            //var pacLogEntry = pacRepository.FindByCriteria("Data").ToList();

            timer.Stop();

            Console.WriteLine("Elapsed: {0}, eSamples.Misc: {1}\n eSamples.Message: {2}\n", timer.Elapsed.Seconds, samplesLogEntry.Count, samplesLogEntry[0].Message);
            //Console.WriteLine("Elapsed: {0}, Pac.MachineName: {1}\n Pac.Message: {2},", timer.Elapsed.Seconds, pacLogEntry.Count(), pacLogEntry[0].Message);
        }
        #endregion

        #region << GenericRepositoryTest >>

        [TestMethod]
        [TestCategory("Synchronous")]
        public void GenericRepositoryTest()
        {
            var samplesRepository = new GenericRepository<LogEntry>(new ESamplesDbContext());
            var pacRepository = new GenericRepository<LogEntry>(new PACDbContext());

            Stopwatch timer = Stopwatch.StartNew();

            var samplesLogEntry = samplesRepository.FindById(56985);
            var pacLogEntry = pacRepository.FindById(56985);

            timer.Stop();

            Console.WriteLine("Stop: {0}, eSamples.Message: {1}", timer.Elapsed.Seconds, samplesLogEntry.Message);
            Console.WriteLine("Stop: {0}, Pac.Message: {1}", timer.Elapsed.Seconds, samplesLogEntry.Message);
        }
        #endregion

        #region << LogRepository.Get() >>

        [TestMethod]
        [TestCategory("Synchronous")]
        public void LogRepositoryGetTest()
        {
            //ILogRepository samplesRepository = new LogRepository(new ESamplesDbContext());
            //ILogRepository pacRepository = new LogRepository(new PACDbContext());

            Stopwatch timer = Stopwatch.StartNew();

            var samplesLogEntry = samplesRepository.FindByKeyword("OnLoad").ToList();
            var pacLogEntry = pacRepository.FindByKeyword("Data").ToList();

            timer.Stop();

            Console.WriteLine("Elapsed: {0}, eSamples.Misc: {1}\n eSamples.Message: {2}\n", timer.Elapsed.Seconds, samplesLogEntry.Count, samplesLogEntry[0].Message);
            Console.WriteLine("Elapsed: {0}, Pac.MachineName: {1}\n Pac.Message: {2},", timer.Elapsed.Seconds, pacLogEntry.Count(), pacLogEntry[0].Message);
        }
        #endregion

        #region << LogRepositoryTest >>

        [TestMethod]
        [TestCategory("Synchronous")]
        public void LogRepositoryTest()
        {
            ILogRepository samplesRepository = new LogRepository(new ESamplesDbContext());
            ILogRepository pacRepository = new LogRepository(new PACDbContext());

            Stopwatch timer = Stopwatch.StartNew();

            var samplesLogEntry = samplesRepository.FindById(41373);
            var pacLogEntry = pacRepository.FindById(8);

            timer.Stop();

            Console.WriteLine("Elapsed: {0}, eSamples.Logger: {1}\n eSamples.Message: {2}\n", timer.Elapsed.Seconds, samplesLogEntry.Misc, samplesLogEntry.Message);
            Console.WriteLine("Elapsed: {0}, Pac.MachineName: {1}\n Pac.Message: {2},", timer.Elapsed.Seconds, pacLogEntry.Misc, pacLogEntry.Message);
        }

        #endregion

        #region << Old Tests >>

        [TestMethod]
        public void TestMethod1()
        {
            var samplesRepository = new GenericRepository<LogEntry>(new ESamplesDbContext());

            Stopwatch timer = Stopwatch.StartNew();

            //var samplesCount = samplesDb.LogEntries.Where(w => w.LogType == "Error").ToList();
            //var logEntry = samplesRepository.FindById(56985);
            var logEntry = samplesRepository.FindById(4);                       
            

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
        #endregion

    }
}
