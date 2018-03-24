#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IAuditableEntity_TKey_.cs </Name>
//         <Created> 24/03/2018 9:30:04 PM </Created>
//         <Key> ce41c03e-761d-4872-9bb4-714950647f7e </Key>
//     </File>
//     <Summary>
//         IAuditableEntity_TKey_.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Data.EF.Interfaces.Entity.Auditable
{
    public interface IAuditableEntity<TKey> : IAuditableEntity where TKey : struct
    {
        TKey? CreatedBy { get; set; }

        TKey? LastUpdatedBy { get; set; }
    }
}