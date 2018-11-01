using System.Collections.Generic;
using Elect.Core.ObjUtils;
using Elect.Core.SimilarityUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elect.Test.Core
{
    [TestClass]
    public class SimilarityUnitTest
    {
        [TestMethod]
        public void JaroWinklerCase()
        {
            // 1

           var listInput = new List<string>
           {
               "Williamee Nguyen Quang",
               "Williamee Nguyen",
               "Williamee Quang Nguyen",
               "Quang Williamee Nguyen",
               "Nguyen Q Williameee",
               "Nguyen Quang W",
               "Pham V Thong",
               "V Thong Pham",
               "Vang Thong Pham",
               "Thong Vang Pham",
               "Thong Pham Vang",
               "Nguyen Le Phuop Thaii",
               "Nguyen Le Thaii Phuop",
               "Nguyen Phuop Thaii Le",
               "Thaii Nguyen Le Phuop",
               "Phuop Thaii Nguyen Le",
               "Nguyen P T Le",
               "Le P T Nguyen",
               "Vietnam Best Enterpriseze",
               "Best Enterpriseze Investme",
               "Vietnam Developmentzz Enterpri",
               "Vietnam Developmentzz Enterprise",
               "Vietnam Development Enterpri",
               "Vietnam Development Enterprise",
               "Developmentzz Enterpri XYZ",
               "Development Enterprise XYZ",
               "Enterpri XYZ",
               "City Investment",
               "Citi Investment",
               "Citi Investments",
               "City Investments",
               "Bong Hoa Hong ACD",
               "Bong Hoa Hong",
               "Thuc Pham Tot Nhat TG",
               "Thuc Pham Tot Nhat",
               "Dau Tu Ngan Sao",
               "Dau Tu Ngan Sao ABC",
               "Ngan Sao ABC",
               "Mai Dao Ap",
               "Ngan Hang WNC",
               "WNC"
           };
          
            var listBlacklist = new List<string>
            {
                "Nguyen Quang Williamee"
//                "Pham Vang Thonga",
//                "Le Phuop Thaii Nguyen",
//                "Vietnam Best Enterpriseze Investme Limited",
//                "Vietnam Developmentzz Enterpri XYZ Company Limited",
//                "City Investment Pte Ltd",
//                "Cong Ty Trach Nhiem Huu Han Mot Thanh Vien Bong Hoa Hong ACD",
//                "Cong Ty Trach Nhiem Huu Han Thuc Pham Tot Nhat TG",
//                "Cong Ty Co Phan Dau Tu Ngan Sao ABC",
//                "Cong Ty TNHH Mai Dao Ap",
//                "Ngan hang Thuong Mai Co Phan WNC"
            };

            var listScore = new List<ResultScore>();
            
            var jw = new JaroWinkler();
            
            foreach (var blacklistItem in listBlacklist)
            {
                foreach (var inputItem in listInput)
                {
                    listScore.Add(new ResultScore(inputItem, blacklistItem, jw.Similarity(inputItem, blacklistItem)));
                }
            }

            var listScoreStr = listScore.ToJsonString();
        }


        private static ResultScore GetScore(string item1, string item2)
        {
            var jw = new JaroWinkler();

            var score = jw.Similarity(item1, item2);

            return new ResultScore(item1, item2, score);
        }

        public class ResultScore
        {
            public string Item1 { get; set; }
            public string Item2 { get; set; }
            public double Score { get; set; }

            public ResultScore(string item1, string item2, double score)
            {
                Item1 = item1;
                Item2 = item2;
                Score = score;
            }
        }
    }
}