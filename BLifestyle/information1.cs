using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLifestyle
{
   public class information1
    {
        public string resultcode { get; set; }
        public string reason { get; set; }
        public index result { get; set; }
        public string error_code { get; set; }
    }
        public class index
        {
            public string area { get; set; }
            public string sex { get; set; }
            public string birthday { get; set; }
            public string vertify { get; set; }
        }
}
