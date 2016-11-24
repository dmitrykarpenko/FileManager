using FileManager.WebAPI.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.WebAPI.Models
{
    public class DirectoryM
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public IEnumerable<DirectoryM> Subdirectories { get; set; }
        public IEnumerable<FileM> Files { get; set; }
        public Dictionary<FileSizeSpan, int> CountOf { get; set; }
    }
}
