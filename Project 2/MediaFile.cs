using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2
{
    //  Derived class to parent class
    class MediaFile : GenericFile
    {
        public string Title { get; set; }
        public TimeSpan Length { get; set; }
        public int Rating { get; set; }
        public MediaFile() : base()
        {
            //  Default values declared as empty string and 0
            Title = "";
            Rating = 0;
        }
    }
}
