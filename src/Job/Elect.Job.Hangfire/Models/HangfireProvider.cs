#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> HangfireProvider.cs </Name>
//         <Created> 02/04/2018 5:26:48 PM </Created>
//         <Key> 9173e95a-797e-4c46-80dd-1277224a2caf </Key>
//     </File>
//     <Summary>
//         HangfireProvider.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Job.Hangfire.Models
{
    public enum HangfireProvider
    {
        Memory,
        SqlServer
    }
}