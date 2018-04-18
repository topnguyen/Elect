﻿#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectHangfireOptions.cs </Name>
//         <Created> 02/04/2018 5:25:42 PM </Created>
//         <Key> 0d38ac4b-4faa-439a-9d26-34072398ada5 </Key>
//     </File>
//     <Summary>
//         ElectHangfireOptions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.Interfaces;
using Hangfire;
using System;

namespace Elect.Job.Hangfire.Models
{
    public class ElectHangfireOptions : IElectOptions
    {
        /// <summary>
        ///     Disable or Enable Job Dashboard, default is false. 
        /// </summary>
        public bool IsDisableJobDashboard { get; set; }

        /// <summary>
        ///     Job Dashboard url, default is "/developers/job". 
        /// </summary>
        /// <remarks> Must start with "/" </remarks>
        public string Url { get; set; } = "/developers/job";

        /// <summary>
        ///     URL for back button in Job Dashboard. Set to <see langword="null" /> to hide the Back
        ///     To Site link, default is "/".
        /// </summary>
        public string BackToUrl { get; set; } = "/";

        /// <summary>
        ///     Access Key via uri param "key", default is "" - allow anonymous. 
        /// </summary>
        public string AccessKey { get; set; } = string.Empty;

        /// <summary>
        ///     Un-authorize message when user access Job Dashboard with not correct key. Default is
        ///     "You don't have permission to view API Document, please contact your administrator."
        /// </summary>
        public string UnAuthorizeMessage { get; set; } = "You don't have permission to access Job Dashboard, please contact your administrator.";

        /// <summary>
        ///     Storage provider, default is Memory. 
        /// </summary>
        public HangfireProvider Provider { get; set; } = HangfireProvider.Memory;

        /// <summary>
        ///     Database Connection if <see cref="Provider " /> is <see cref="HangfireProvider.SqlServer" /> 
        /// </summary>
        public string HangfireDatabaseConnectionString { get; set; }

        /// <summary>
        ///     The interval the /stats endpoint should be polled with (milliseconds), default is 2000.
        /// </summary>
        public int StatsPollingInterval { get; set; } = 3000;

        /// <summary>
        ///     Additional Options if you want to add your customize after Elect add Hangfire Global Config.
        /// </summary>
        public Action<IGlobalConfiguration, ElectHangfireOptions> ExtendOptions { get; set; }
    }
}