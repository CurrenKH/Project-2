using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2
{
    //  Derived class to parent class
    class Image : GenericFile
    {
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public string Resolution { get; set; }

        public Image() : base()
        {
            //  Default values declared as empty string and 0
            Width = 0;
            Height = 0;
            Resolution = "";
        }
    }
}
