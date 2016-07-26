using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cloudweb.Models
{
    public class SlideShowViewModel
    {
        public List<Table> photos { get; set; }
        public List<Table> several_photos { get; set; }
        public List<bool> booleans { get; set; }
        public int Duration { get; set; }
        public string Effect { get; set; }
        public string Name { get; set; }

        public SlideShowViewModel()
        {
            booleans = new List<bool>();
            Duration = 1;
        }
    }
}

