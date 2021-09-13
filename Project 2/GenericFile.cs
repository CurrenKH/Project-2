using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2
{
    class GenericFile
    {
        public string Name { get; set; }
        public string UserComment { get; set; }
        public string Size { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime CreationDate { get; set; }

        public GenericFile()
        {
            //  Default values declared as empty strings
            Name = "";
            UserComment = "";
            Size = "";
            LastModified = default;
            CreationDate = default;
        }
    }
}
