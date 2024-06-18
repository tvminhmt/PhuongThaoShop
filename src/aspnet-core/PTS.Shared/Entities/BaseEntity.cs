using System;
using System.Collections.Generic;
using System.Text;

namespace App.Shared.Core.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Id = 0;
            Code = "";
        }

        public long Id { get; set; }
        public string Code { get; set; }

    }
}
