#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ISoftDeletableEntity_TKey_.cs </Name>
//         <Created> 24/03/2018 9:36:36 PM </Created>
//         <Key> ab4ff44c-81d6-4c15-acda-73b9f25e1ef1 </Key>
//     </File>
//     <Summary>
//         ISoftDeletableEntity_TKey_.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Data.EF.Interfaces.Entity.SoftDelete
{
    public interface ISoftDeletableEntity<TKey> : ISoftDeletableEntity where TKey : struct
    {
        TKey? DeletedBy { get; set; }
    }
}