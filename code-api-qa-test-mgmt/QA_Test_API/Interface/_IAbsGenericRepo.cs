
namespace QA_Test_Log.Interface
{
    public interface _IAbsGenericRepo<MainEntity, PKType>
        where MainEntity : class
    {
        public IQueryable<MainEntity> GetList();

        public Task<MainEntity> GetDetails(PKType id);

        public Task<MainEntity> Create(MainEntity table);

        public Task<EditResult<MainEntity>> Update(PKType id, MainEntity table);

        public Task<DeleteResult<MainEntity>> Delete(PKType id);

    }
}
