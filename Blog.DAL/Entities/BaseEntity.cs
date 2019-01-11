using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DAL.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public Guid CreateUserId { get; set; }

        public Guid UpdateUserId { get; set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}