using System.Collections.Generic;
using Elect.Core.ObjUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Elect.Data.EF.Models
{
    public class EntityStateModel
    {
        public EntityState State { get; set; }

        public IEnumerable<PropertyEntry> Properties { get; set; }

        public object Entity { get; set; }

        public EntityStateModel()
        {
        }

        public EntityStateModel(EntityEntry entityEntry)
        {
            State = entityEntry.State;
            Properties = entityEntry.Properties;
            Entity = entityEntry.Entity;
        }
    }
}