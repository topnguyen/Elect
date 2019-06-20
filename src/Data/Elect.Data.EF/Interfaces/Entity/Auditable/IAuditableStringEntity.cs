#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IAuditableStringEntity.cs </Name>
//         <Created> 24/03/2018 9:30:55 PM </Created>
//         <Key> e20c4a5b-45af-4d3c-aeec-efcf907ede9f </Key>
//     </File>
//     <Summary>
//         IAuditableStringEntity.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Data.EF.Interfaces.Entity.Auditable
{
    public interface IAuditableStringEntity : IAuditableEntity
    {
        string CreatedBy { get; set; }

        string LastUpdatedBy { get; set; }
    }
}