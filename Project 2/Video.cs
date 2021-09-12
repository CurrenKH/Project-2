using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2
{
    //  Derived class to parent class
    class Video : MediaFile
    {
        public string Director { get; set; }
        public string Producer { get; set; }

        public Video() : base()
        {
            //  Declared as empty strings
            Director = "";
            Producer = "";
        }
    }
}
