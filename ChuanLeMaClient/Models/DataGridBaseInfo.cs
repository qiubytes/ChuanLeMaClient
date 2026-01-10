using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Models
{
    public class DataGridBaseInfo
    {
        public string Key { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Size { get; set; }  
        public List<TagInfo> Tags { get; set; } = new();
        public string Description { get; set; } = string.Empty;
    }
    public class TagInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
    }
}
