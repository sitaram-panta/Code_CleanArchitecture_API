namespace QA_Test_Log.Interface
{
    public interface IDatabaseHelper
    {
        List<T> ExecuteQuery<T>(object parameters = null);
    }
}
