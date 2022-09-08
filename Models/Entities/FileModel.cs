using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class FileModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string? Url { get; set; }
        public string FilePath { get; set; }
        public string FileFormat { get; set; }
        public string ContentType { get; set; }
    }
}
