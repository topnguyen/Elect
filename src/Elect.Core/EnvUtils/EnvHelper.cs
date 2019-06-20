#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> EnvHelper.cs </Name>
//         <Created> 15/03/2018 4:52:43 PM </Created>
//         <Key> 0d965c63-3413-4da6-adea-6dec13fe5819 </Key>
//     </File>
//     <Summary>
//         EnvHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;

namespace Elect.Core.EnvUtils
{
    public static class EnvHelper
    {
        public const string AspNetCoreEnvironmentVariable = "ASPNETCORE_ENVIRONMENT";

        public static string CurrentEnvironment => Environment.GetEnvironmentVariable(AspNetCoreEnvironmentVariable);

        public static readonly string MachineName = Environment.MachineName;

        public const string EnvDevelopmentName = "Development";
        public const string EnvStagingName = "Staging";
        public const string EnvProductionName = "Production";

        public static bool IsDevelopment()
        {
            return Is(EnvDevelopmentName);
        }

        public static bool IsStaging()
        {
            return Is(EnvStagingName);
        }

        public static bool IsProduction()
        {
            return Is(EnvProductionName);
        }

        public static bool Is(string environment)
        {
            return string.Equals(CurrentEnvironment, environment, StringComparison.OrdinalIgnoreCase);
        }
    }
}