using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elect.Test.Core
{
    [TestClass]
    public class MixUnitTest
    {
        [TestMethod]
        public void BuildCategoryOptionMarix()
        {
            List<string> categoryOptions1 = new List<string> {"c1o1", "c1o2", "c1o3"};

            List<string> categoryOptions2 = new List<string> {"c2o1", "c2o2", "c2o3", "c2o4", "c2o5"};

            List<string> categoryOptions3 = new List<string> {"c3o1", "c3o2", "c3o3"};

            List<string> categoryOptions4 = new List<string> {"c4o1", "c4o2", "c4o3"};

            List<string> categoryOptions5 = new List<string> {"c5o1", "c5o2", "c5o3", "c5o4"};

            List<string> categoryOptions6 = new List<string> {"c6o1", "c6o2", "c6o3"};

            var categoryOptions = new List<List<string>>
            {
                categoryOptions1, categoryOptions2, categoryOptions3, categoryOptions4, categoryOptions5,
                categoryOptions6
            };

            // Build Nested Options

            var listOptionModels = BuildListOptionModel(categoryOptions, 0);

            // Build list Key

            var listKeys = BuildListKey("", listOptionModels);

            Assert.AreEqual(listKeys.Count,
                categoryOptions1.Count * categoryOptions2.Count * categoryOptions3.Count * categoryOptions4.Count *
                categoryOptions5.Count * categoryOptions6.Count);
        }

        public List<OptionModel> BuildListOptionModel(List<List<string>> categoryOptions, int iCategoryOption)
        {
            var listOptionModels = new List<OptionModel>();

            var categoryOption = categoryOptions[iCategoryOption];

            foreach (var option in categoryOption)
            {
                var optionModel = new OptionModel
                {
                    Id = option
                };

                var iNextCategoryOption = iCategoryOption + 1;

                if (iNextCategoryOption < categoryOptions.Count)
                {
                    optionModel.OptionModels = BuildListOptionModel(categoryOptions, iNextCategoryOption);
                }

                listOptionModels.Add(optionModel);
            }

            return listOptionModels;
        }

        public List<string> BuildListKey(string key, List<OptionModel> listOptionModels)
        {
            var keys = new List<string>();

            foreach (var optionModel in listOptionModels)
            {
                var newKey = key + "_" + optionModel.Id;

                if (optionModel.OptionModels.Any())
                {
                    var subKeys = BuildListKey(newKey, optionModel.OptionModels);

                    keys.AddRange(subKeys);
                }
                else
                {
                    newKey = newKey.Trim('_');
                    keys.Add(newKey);
                }
            }

            return keys;
        }

        public class OptionModel
        {
            public string Id { get; set; }

            public List<OptionModel> OptionModels { get; set; } = new List<OptionModel>();
        }
    }
}