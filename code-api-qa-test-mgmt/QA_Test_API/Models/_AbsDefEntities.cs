namespace QA_Test_Log.Models
{
    public abstract class _AbsDefEntities
    {

        public bool Status { get; set; }

        public bool Deleted { get; set; }

        public string CreatedName { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string UpdatedName { get; set; }

        public DateTime UpdatedDate { get; set; } = DateTime.Now;

    }
}
