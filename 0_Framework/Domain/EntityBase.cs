using System;

namespace _0_Framework.Domain
{
    public class EntityBase
    {
        public long Id { get; protected set; }
        public DateTime CreationDate { get; set; }
        //public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public long? CreatedBy { get; protected set; }
        public DateTime? ModifiedAt { get; protected set; }
        public long? ModifiedBy { get; protected set; }
        public bool IsDeleted { get; protected set; }

        public void MarkAsDeleted(long userId)
        {
            IsDeleted = true;
            ModifiedAt = DateTime.UtcNow;
            ModifiedBy = userId;
        }
    }


}
