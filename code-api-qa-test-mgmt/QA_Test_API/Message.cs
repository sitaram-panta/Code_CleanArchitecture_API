namespace QA_Test_Log
{
    public class DeleteResult<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T DeletedEntity { get; set; }
    }

    public class EditResult<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T EditedEntity { get; set; }
    }
}
