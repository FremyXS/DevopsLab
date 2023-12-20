using Laborotor.Database.Entity.Common;

namespace Laborotor.Database.Entity
{
    public class Link: EntityBase
    {
        public string Url { get; set; }
        public bool Status { get; set; }
        public int Code { get; set; }
    }
}
