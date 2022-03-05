using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.ExtensionMethods
{
    public static class ContextExtension
    {
        public static void DeleteBehaviorConvention(this ModelBuilder modelBuilder, DeleteBehavior deleteBehavior)
        {
            modelBuilder
                .EntityLoop(entityType => entityType.GetForeignKeys()
                .Where(fk => fk.DeleteBehavior != deleteBehavior)
                .ToList()
                .ForEach(fk => fk.DeleteBehavior = deleteBehavior));
        }

        // IMutableEntityType is like EntityType, but you can also modify the entityTypes
        public static void EntityLoop(this ModelBuilder modelBuilder, Action<IMutableEntityType> action)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                action(entityType);
        }

    }
}
