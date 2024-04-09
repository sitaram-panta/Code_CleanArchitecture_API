using Microsoft.EntityFrameworkCore;
using QA_Test_Log.Data;
using QA_Test_Log.Interface;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace QA_Test_Log.Services
{
    public abstract class _AbsGenericRepo<MainEntity, PKType> : _IAbsGenericRepo<MainEntity, PKType>
        where MainEntity : class
    {
        private readonly AppDbContext dbContext;

        public _AbsGenericRepo(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public virtual IQueryable<MainEntity> GetList()
        {
            return dbContext.Set<MainEntity>();
        }

        public virtual async Task<MainEntity> GetDetails(PKType id)
        {
            return await dbContext.Set<MainEntity>().FindAsync(id);
        }

        public virtual async Task<MainEntity> Create(MainEntity Data)
        {
            dbContext.Set<MainEntity>().Add(Data);
            await dbContext.SaveChangesAsync();
            return Data;
        }

        public virtual async Task<EditResult<MainEntity>> Update(PKType id, MainEntity Data)
        {
            var existingEntity = await dbContext.Set<MainEntity>().FindAsync(id);

            if (existingEntity != null)
            {
                // Check if the foreign key value is being changed
                if (!ValidateForeignKey(existingEntity, Data))
                {
                    return new EditResult<MainEntity>
                    {
                        IsSuccess = false,
                        Message = "Foreign key value cannot be changed.",
                        EditedEntity = null
                    };
                }

                // Copy the values from the 'Data' parameter to the 'existingEntity'
                dbContext.Entry(existingEntity).CurrentValues.SetValues(Data);

                await dbContext.SaveChangesAsync();

                return new EditResult<MainEntity>
                {
                    IsSuccess = true,
                    Message = "Entity updated successfully",
                    EditedEntity = existingEntity
                };
            }
            else
            {
                return new EditResult<MainEntity>
                {
                    IsSuccess = false,
                    Message = "Entity not found.",
                    EditedEntity = null
                };
            }
        }

        protected virtual bool ValidateForeignKey(MainEntity existingEntity, MainEntity newData)
        {
            var properties = typeof(MainEntity).GetProperties();

            foreach (var property in properties)
            {
                var foreignKeyAttribute = property.GetCustomAttribute<ForeignKeyAttribute>();
                if (foreignKeyAttribute != null)
                {
                    var originalValue = property.GetValue(existingEntity);
                    var newValue = property.GetValue(newData);

                    if (!object.Equals(originalValue, newValue))
                    {
                        return false;
                    }
                }
            }

            return true;
        }



        public virtual async Task<DeleteResult<MainEntity>> Delete(PKType id)
        {
            // Find the entity to delete by its ID
            var entityToDelete = await dbContext.Set<MainEntity>().FindAsync(id);

            if (entityToDelete != null)
            {
                // Remove the entity from the context
                dbContext.Set<MainEntity>().Remove(entityToDelete);

                // Save the changes to the database
                await dbContext.SaveChangesAsync();

                // Return success result with the deleted entity
                return new DeleteResult<MainEntity>
                {
                    IsSuccess = true,
                    Message = "Entity deleted successfully.",
                    DeletedEntity = entityToDelete
                };
            }
            else
            {
                // Return failure result indicating that the entity was not found
                return new DeleteResult<MainEntity>
                {
                    IsSuccess = false,
                    Message = "Entity not found.",
                    DeletedEntity = null
                };
            }
        }
    }
}
