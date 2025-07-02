namespace Elect.Data.EF.Models
{
    public class EntityStateModel
    {
        public object Entity { get; set; }
        public EntityState State { get; set; }
        /// <summary>
        ///     Fields have IsModified is true, included Field Name and Field Value
        /// </summary>
        public Dictionary<string, object> ModifiedFields { get; set; } = new Dictionary<string, object>();
        /// <summary>
        ///     Indicating whether the value of this property is considered a
        ///     temporary value which will be replaced by a value generated from the store when
        ///     <see cref="M:Microsoft.EntityFrameworkCore.DbContext.SaveChanges" />is called.
        /// </summary>
        public List<string> TempFieldNames { get; set; }
        public EntityStateModel(EntityEntry entityEntry)
        {
            Entity = entityEntry.Entity;
            State = entityEntry.State;
            TempFieldNames = entityEntry.Properties.Where(x => x.IsTemporary).Select(x => x.Metadata.Name).ToList();
            var modifiedProperties = entityEntry.Properties.Where(x => x.IsModified).ToList();
            foreach (var modifiedProperty in modifiedProperties)
            {
                if (ModifiedFields.ContainsKey(modifiedProperty.Metadata.Name))
                {
                    continue;
                }
                var property = entityEntry.Entity.GetType().GetProperty(modifiedProperty.Metadata.Name);
                if (property == null)
                {
                    continue;
                }
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entityEntry.Entity, null);
                ModifiedFields.Add(propertyName, propertyValue);
            }
        }
    }
}
