using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2
{
    //  Derived class to parent class
    class Audio : MediaFile
    {
        /// <summary>
        /// This is the media class
        /// </summary>
        public string Artist { get; set; }
        public int BitRate { get; set; }
        public Audio() : base()
        {
            //  Default values declared as empty string and 0
            Artist = "";
            BitRate = 0;
        }
    }
}
