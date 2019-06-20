#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ISoftDeletableStringEntity.cs </Name>
//         <Created> 24/03/2018 9:42:27 PM </Created>
//         <Key> b591367a-b248-41d4-8ad8-12daf77052a7 </Key>
//     </File>
//     <Summary>
//         ISoftDeletableStringEntity.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Data.EF.Interfaces.Entity.SoftDelete
{
    public interface ISoftDeletableStringEntity : ISoftDeletableEntity
    {
        string DeletedBy { get; set; }
    }
}