using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2
{
    class Document : GenericFile
    {
        //  Derived class to parent class
        public int NumPages { get; set; }
        public int NumWords { get; set; }
        public string DocSubject { get; set; }

        public Document() : base()
        {
            //  Default values declared as empty string and 0
            NumPages = 0;
            NumWords = 0;
            DocSubject = "";
        }
    }
}
