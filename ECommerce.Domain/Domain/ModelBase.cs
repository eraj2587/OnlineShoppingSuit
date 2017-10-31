using System;

namespace ECommerce.Domain
{
    public abstract class ModelBase
    {
        public DateTimeOffset CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }

    }
}