using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elect.Test.Core
{
    [TestClass]
    public class JobStartTimeCalculationUnitTest
    {
        [TestMethod]
        public void JobStartTimeCalculationCase0()
        {
            // Provided Information

            var shifts = new List<ShiftInfoModel>
            {
                new ShiftInfoModel
                {
                    ShiftName = "Ca 1",
                    Start = new TimeSpan(7, 30, 0),
                    End = new TimeSpan(11, 30, 0)
                },

                new ShiftInfoModel
                {
                    ShiftName = "Ca 2",
                    Start = new TimeSpan(12, 30, 0),
                    End = new TimeSpan(16, 30, 0)
                }
            };
            
            var jobEndTime = new DateTime(2020, 01, 03, 8, 0, 0);

            var jobDuration = new TimeSpan(6, 0, 0);

            // Calculation

            var jobStartTime = FindJobStartTime(shifts, jobEndTime, jobDuration);

            // Testing Time

            var correctJobStartTime = new DateTime(2020, 01, 02, 10, 0, 0);

            Assert.AreEqual(correctJobStartTime, jobStartTime);
        }
        
        [TestMethod]
        public void JobStartTimeCalculationCase1()
        {
            // Provided Information

            var shifts = new List<ShiftInfoModel>
            {
                new ShiftInfoModel
                {
                    ShiftName = "Ca 1",
                    Start = new TimeSpan(7, 30, 0),
                    End = new TimeSpan(11, 30, 0)
                },

                new ShiftInfoModel
                {
                    ShiftName = "Ca 2",
                    Start = new TimeSpan(16, 30, 0),
                    End = new TimeSpan(20, 30, 0)
                }
            };
            
            var jobEndTime = new DateTime(2020, 01, 03, 16, 30, 0);

            var jobDuration = new TimeSpan(7, 39, 0);

            // Calculation

            var jobStartTime = FindJobStartTime(shifts, jobEndTime, jobDuration);

            // Testing Time

            var correctJobStartTime = new DateTime(2020, 01, 02, 16, 51, 0);

            Assert.AreEqual(correctJobStartTime, jobStartTime);
        }
        
        [TestMethod]
        public void JobStartTimeCalculationCase2()
        {
            // Provided Information

            var shifts = new List<ShiftInfoModel>
            {
                new ShiftInfoModel
                {
                    ShiftName = "Ca 1",
                    Start = new TimeSpan(7, 30, 0),
                    End = new TimeSpan(11, 30, 0)
                },

                new ShiftInfoModel
                {
                    ShiftName = "Ca 2",
                    Start = new TimeSpan(12, 30, 0),
                    End = new TimeSpan(17, 00, 0)
                }
            };
            
            var jobEndTime = new DateTime(2020, 01, 03, 9, 30, 0);

            var jobDuration = new TimeSpan(2, 0, 0);

            // Calculation

            var jobStartTime = FindJobStartTime(shifts, jobEndTime, jobDuration);

            // Testing Time

            var correctJobStartTime = new DateTime(2020, 01, 03, 13, 00, 0);

            Assert.AreEqual(correctJobStartTime, jobStartTime);
        }
        
        [TestMethod]
        public void JobStartTimeCalculationCase3()
        {
            // Provided Information

            var shifts = new List<ShiftInfoModel>
            {
                new ShiftInfoModel
                {
                    ShiftName = "Ca 1",
                    Start = new TimeSpan(7, 30, 0),
                    End = new TimeSpan(11, 30, 0)
                },

                new ShiftInfoModel
                {
                    ShiftName = "Ca 2",
                    Start = new TimeSpan(17, 00, 0),
                    End = new TimeSpan(19, 20, 0)
                }
            };
            
            var jobEndTime = new DateTime(2020, 01, 03, 7, 50, 0);

            var jobDuration = new TimeSpan(13, 50, 0);

            // Calculation

            var jobStartTime = FindJobStartTime(shifts, jobEndTime, jobDuration);

            // Testing Time

            var correctJobStartTime = new DateTime(2019, 12, 31, 18, 30, 0);

            Assert.AreEqual(correctJobStartTime, jobStartTime);
        }
        
        //--------------------------------------- Calculation Methods ---------------------------------------

        public DateTime FindJobStartTime(List<ShiftInfoModel> shifts, DateTime jobEndTime, TimeSpan jobDuration)
        {
            var jobStartTime = new DateTime();

            var shiftIndexContainJobEndTime = FindClosestIndexShift(shifts, jobEndTime);

            var totalDuration = CalculateDuration(shiftIndexContainJobEndTime, shifts, ref jobEndTime, out var date, out var shiftIndex);

            if (totalDuration >= jobDuration)
            {
                date = date.Add(jobEndTime.TimeOfDay);

                jobStartTime = date - jobDuration;

                return jobStartTime;
            }

            jobDuration -= totalDuration;

            jobEndTime = date.Add(shifts[shiftIndex].Start);
            
            jobStartTime = FindJobStartTime(shifts, jobEndTime, jobDuration);
            
            return jobStartTime;
        }

        /// <summary>
        ///     Find Shift contain Job End Time
        /// </summary>
        /// <param name="shifts"></param>
        /// <param name="jobEndTime"></param>
        /// <returns>Index of the shift, -1 for the job end before start any shift</returns>
        public static int FindClosestIndexShift(List<ShiftInfoModel> shifts, DateTime jobEndTime)
        {
            for (int i = 0; i < shifts.Count; i++)
            {
                var shift = shifts[i];

                if (shift.Start < jobEndTime.TimeOfDay)
                {
                    return i;
                }
            }
                
            return -1;
        }

        public static TimeSpan CalculateDuration(int shiftIndexContainJobEndTime,
            List<ShiftInfoModel> shifts,
            ref DateTime jobEndTime,
            out DateTime date,
            out int shiftIndex)
        {
            // Initial

            TimeSpan duration;

            date = jobEndTime.Date;

            shiftIndex = shiftIndexContainJobEndTime;

            if (shiftIndexContainJobEndTime < 0)
            {
                // This case mean the job end time before any shift in the day
                // So total duration will be previous shift in yesterday

                shiftIndex = shifts.Count - 1;

                var lastShiftInfo = shifts.LastOrDefault();

                jobEndTime = jobEndTime.Date.AddDays(-1).Add(lastShiftInfo.End);

                date = jobEndTime.Date;

                duration = lastShiftInfo.End - lastShiftInfo.Start;

                return duration;
            }
            
            var shiftInfo = shifts[shiftIndexContainJobEndTime];

            if (shiftInfo.End < jobEndTime.TimeOfDay)
            {
                duration = shiftInfo.End - shiftInfo.Start;

                return duration;
            }

            duration = jobEndTime.TimeOfDay - shiftInfo.Start;

            return duration;
        }
    }

    public class ShiftInfoModel
    {
        public string ShiftName { get; set; }

        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }
    }
}