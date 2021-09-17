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
        public string Extension { get; set; }
        public string UserComment { get; set; }
        public int Size { get; set; }
        public string LastModified { get; set; }
        public string CreationDate { get; set; }

        public GenericFile()
        {
            //  Default values declared as empty strings
            Name = "";
            Extension = "";
            UserComment = "";
            Size = 0;
            LastModified = "";
            CreationDate = "";
        }
    }
}
