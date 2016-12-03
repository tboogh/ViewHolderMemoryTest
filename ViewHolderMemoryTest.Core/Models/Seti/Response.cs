using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewHolderMemoryTest.Core.Models.Seti
{
    public class Response
    {
        public string path { get; set; }
        public List<Datum> data { get; set; }
        public string order { get; set; }
    }
}