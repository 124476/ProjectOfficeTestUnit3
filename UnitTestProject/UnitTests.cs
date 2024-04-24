using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WorkingTimeEfficiency;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestAllDaysWorking()
        {
            List<WorkingDay> days = new List<WorkingDay>();

            var dateNow = DateTime.Now;
            var dateTime = dateNow.AddMonths(-1);
            while (dateTime != dateNow)
            {
                days.Add(new WorkingDay()
                {
                    Date = dateTime,
                    Status = StatusDay.Working,
                    Hours = 8
                });

                dateTime = dateTime.AddDays(1);
            }

            var tasks = new List<Task>();
            tasks.Add(new Task()
            {
                BeginDateTime = dateNow.AddDays(-21),
                EndDateTime = dateNow.AddDays(-2)
            });

            var excepted = new double[] { 58.06, 892800, 374400, 0, 0 };
            var actual = WorkingTime.CalculateEfficiency(days, tasks);
            Assert.AreEqual((excepted[0], excepted[1], excepted[2], excepted[3], excepted[4]), (actual[0], actual[1], actual[2], actual[3], actual[4]));
        }

        [TestMethod]
        public void TestAllWorkingDaysTasks()
        {
            List<WorkingDay> days = new List<WorkingDay>();

            var dateNow = DateTime.Now;
            var dateTime = dateNow.AddMonths(-1);
            while (dateTime != dateNow)
            {
                StatusDay statusDay;
                if (dateTime.DayOfWeek == DayOfWeek.Sunday || dateTime.DayOfWeek == DayOfWeek.Saturday)
                {
                    statusDay = StatusDay.PreWorking;
                }
                else
                {
                    statusDay = StatusDay.Working;
                }
                days.Add(new WorkingDay()
                {
                    Date = dateTime,
                    Status = statusDay,
                    Hours = 5
                });

                dateTime = dateTime.AddDays(1);
            }

            var tasks = new List<Task>();
            tasks.Add(new Task()
            {
                BeginDateTime = dateNow.AddDays(-21),
                EndDateTime = dateNow.AddDays(-2)
            });

            var excepted = new double[] { 58.06, 558000, 234000, 0, 0 };
            var actual = WorkingTime.CalculateEfficiency(days, tasks);
            Assert.AreEqual((excepted[0], excepted[1], excepted[2], excepted[3], excepted[4]), (actual[0], actual[1], actual[2], actual[3], actual[4]));
        }


        [TestMethod]
        public void TestNoWorkingOfTasks()
        {
            List<WorkingDay> days = new List<WorkingDay>();

            var dateNow = DateTime.Now;
            var dateTime = dateNow.AddMonths(-1);
            while (dateTime != dateNow)
            {
                days.Add(new WorkingDay()
                {
                    Date = dateTime,
                    Status = StatusDay.Working,
                    Hours = 8
                });

                dateTime = dateTime.AddDays(1);
            }

            var tasks = new List<Task>();

            var excepted = new double[] { 0, 892800, 892800, 0, 0 };
            var actual = WorkingTime.CalculateEfficiency(days, tasks);
            Assert.AreEqual((excepted[0], excepted[1], excepted[2], excepted[3], excepted[4]), (actual[0], actual[1], actual[2], actual[3], actual[4]));
        }
        [TestMethod]
        public void TestMoreTasks()
        {
            List<WorkingDay> days = new List<WorkingDay>();

            var dateNow = DateTime.Now;
            var dateTime = dateNow.AddMonths(-1);
            while (dateTime != dateNow)
            {
                StatusDay statusDay;
                if (dateTime.DayOfWeek == DayOfWeek.Sunday || dateTime.DayOfWeek == DayOfWeek.Saturday)
                {
                    statusDay = StatusDay.PreWorking;
                }
                else
                {
                    statusDay = StatusDay.Working;
                }
                days.Add(new WorkingDay()
                {
                    Date = dateTime,
                    Status = statusDay,
                    Hours = 5
                });

                dateTime = dateTime.AddDays(1);
            }

            var tasks = new List<Task>();
            tasks.Add(new Task()
            {
                BeginDateTime = dateNow.AddDays(-21),
                EndDateTime = dateNow.AddDays(-2)
            });
            tasks.Add(new Task()
            {
                BeginDateTime = dateNow.AddDays(-16),
                EndDateTime = dateNow.AddDays(-2)
            });
            tasks.Add(new Task()
            {
                BeginDateTime = dateNow.AddDays(-14),
                EndDateTime = dateNow.AddDays(-5)
            });

            var excepted = new double[] { 58.06, 558000, 234000, 0, 0 };
            var actual = WorkingTime.CalculateEfficiency(days, tasks);
            Assert.AreEqual((excepted[0], excepted[1], excepted[2], excepted[3], excepted[4]), (actual[0], actual[1], actual[2], actual[3], actual[4]));
        }
        [TestMethod]
        public void TestAllLosedTasks()
        {
            List<WorkingDay> days = new List<WorkingDay>();

            var dateNow = DateTime.Now;
            var dateTime = dateNow.AddMonths(-1);
            while (dateTime != dateNow)
            {
                StatusDay statusDay;
                if (dateTime.DayOfWeek == DayOfWeek.Sunday || dateTime.DayOfWeek == DayOfWeek.Saturday)
                {
                    statusDay = StatusDay.PreWorking;
                }
                else
                {
                    statusDay = StatusDay.Working;
                }
                days.Add(new WorkingDay()
                {
                    Date = dateTime,
                    Status = statusDay,
                    Hours = 5
                });

                dateTime = dateTime.AddDays(1);
            }

            var tasks = new List<Task>();
            tasks.Add(new Task()
            {
                BeginDateTime = dateNow.AddDays(-5),
                EndDateTime = dateNow.AddDays(-2)
            });
            tasks.Add(new Task()
            {
                BeginDateTime = dateNow.AddDays(-3),
                EndDateTime = dateNow.AddDays(-2)
            });
            tasks.Add(new Task()
            {
                BeginDateTime = dateNow.AddDays(-9),
                EndDateTime = dateNow.AddDays(-5)
            });

            var excepted = new double[] { 16.13, 558000, 468000, 0, 0 };
            var actual = WorkingTime.CalculateEfficiency(days, tasks);
            Assert.AreEqual((excepted[0], excepted[1], excepted[2], excepted[3], excepted[4]), (actual[0], actual[1], actual[2], actual[3], actual[4]));
        }
    }
}
