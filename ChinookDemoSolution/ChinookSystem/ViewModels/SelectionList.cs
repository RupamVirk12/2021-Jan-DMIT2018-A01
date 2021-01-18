  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.ViewModels
{
    //this class will be used as a generic container for the data that will load a dropdown list
    //this value will represent a integer primary key
    //the display field will represent the displayed string of the ddl
     public class SelectionList
    {
        public int ValueField { get; set; }
        public string DisplayField { get; set;  }
    }
}
