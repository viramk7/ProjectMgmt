using System;

namespace ProjectMgmt.Web.Data.Entities
{
    public interface IEntity<T>
    {
        T Id { get; set; }
        Guid CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        Guid? UpdatedBy { get; set; }
        DateTime? UpdatedOn { get; set; }
    }
}
