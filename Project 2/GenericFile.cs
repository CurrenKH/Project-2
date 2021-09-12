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
        public string Type { get; set; }
        public string Size { get; set; }
        public DateTime LastModified { get; set; }

        public GenericFile()
        {
            //  Default values declared as empty strings
            Name = "";
            Type = "";
            Size = "";
        }
    }
}
