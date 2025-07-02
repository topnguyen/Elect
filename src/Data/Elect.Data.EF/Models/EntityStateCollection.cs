namespace Elect.Data.EF.Models
{
    public class EntityStateCollection
    {
        public List<EntityStateModel> ListAdded { get; set; } = new List<EntityStateModel>();
        public List<EntityStateModel> ListModified { get; set; } = new List<EntityStateModel>();
        public List<EntityStateModel> ListDeleted { get; set; } = new List<EntityStateModel>();
    }
}
