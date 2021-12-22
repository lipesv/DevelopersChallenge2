using System;
using System.ComponentModel.DataAnnotations;

namespace OFX.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        private DateTime? _createAt;

        public DateTime? CreateAt
        {
            get { return _createAt; }
            set { _createAt = (value == null ? DateTime.Now : value); }
        }

        public DateTime? UpdateAt { get; set; }

        public override bool Equals(object obj)
        {
            return obj is BaseEntity entity &&
                   Id.Equals(entity.Id) &&
                   _createAt == entity._createAt &&
                   CreateAt == entity.CreateAt &&
                   UpdateAt == entity.UpdateAt;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, _createAt, CreateAt, UpdateAt);
        }
    }
}
