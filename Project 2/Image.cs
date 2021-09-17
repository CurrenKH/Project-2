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
        public int HRes { get; set; }
        public int VRes { get; set; }

        public Image() : base()
        {
            Width = 0;
            Height = 0;
            HRes = 0;
            VRes = 0;
        }
    }
}
