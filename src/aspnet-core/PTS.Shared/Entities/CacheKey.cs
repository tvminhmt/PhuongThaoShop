using System;
using System.Collections.Generic;
using System.Text;

namespace App.Shared.Core.Entities
{
    public class CacheKey
    { 
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ExpiredTime { get; set; }
        public int CacheSize { get; set; }
    }
}
