using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vandebron.Verbruik.Console.App.Api.Usage
{
    public class Result
    {

        public string Unit { get; set; }

        public string Resolution { get; set; }

        public List<Entry> Values { get; set; } = new List<Entry>();

    }
}
