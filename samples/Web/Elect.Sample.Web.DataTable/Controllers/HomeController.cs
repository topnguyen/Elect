using System;
using Elect.Sample.Web.DataTable.Models;
using Elect.Web.DataTable.Models;
using Elect.Web.DataTable.Models.Request;
using Elect.Web.DataTable.Models.Response;
using Elect.Web.DataTable.Processing.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Elect.Sample.Web.DataTable.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Get Users DataTable 
        /// </summary>
        /// <returns></returns>
        [Route("datatable")]
        [HttpPost]
        public DataTableActionResult<UserModel> GetDataTable([FromForm] DataTableRequestModel model)
        {
            // 1. In Service Layer
            DataTableResponseModel<UserModel> response = GetDataTableResponse(model);

            // 2. In Controller

            var result = response.GetDataTableActionResult(x => new
            {
                IsActive = x.IsActive ? "Yes" : "No" // Transform Data before Response
            });

            return result;
        }

        private DataTableResponseModel<UserModel> GetDataTableResponse(DataTableRequestModel model)
        {
            // Sample Data
            var users = new List<UserModel>();

            for (int i = 0; i < 1000; i++)
            {
                users.Add(new UserModel
                {
                    Id = i + 1,
                    FullName = $"User {i + 1}",
                    CreatedTime = DateTimeOffset.Now,
                    IsActive = i  % 2 == 0
                });
            }

            // Queryable Data
            var query = users.AsQueryable();

            // Generate DataTable Response
            var result = query.GetDataTableResponse(model);

            return result;
        }
    }
}