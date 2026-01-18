using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Models
{
    /// <summary>
    /// 文件（文件夹）列表Model
    /// </summary> 
    public class FolderFileDataModel: ObservableObject
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("size")]

        public long Size { get; set; }
        /// <summary>
        /// 是否是文件夹 否则是文件
        /// </summary>
        [JsonPropertyName("Isfolder")]

        public bool IsFolder { get; set; }
        [JsonPropertyName("tags")]

        public List<TagInfo> Tags { get; set; } = new();
        [JsonPropertyName("description")]

        public string Description { get; set; } = string.Empty;
    }
    public class TagInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
    }
}
