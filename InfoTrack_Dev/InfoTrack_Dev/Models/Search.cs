using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfoTrack_Dev.Models
{
    public class Search
    {
        public string keyword { get; set; }
        public string filename { get; set; }
        public int urlCount { get; set; }
        public int textCount { get; set; }
        public LinkedList<int> lines { get; set; }
        public string error { get; set; }
    }
}