using System;

namespace Portal.DevTest.Date.Model
{
    public abstract class BaseModel
    {
        public bool? IsActive { get; set; }
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
