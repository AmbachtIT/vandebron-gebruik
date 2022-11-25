using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vandebron.Verbruik.Console.App.Api.Usage
{
    public class Entry
    {

        public DateTime Time { get; set; }

        public double Consumption { get; set; }

        public double Production { get; set; }

        public double ConsumptionPeak { get; set; }

        public double ConsumptionOffPeak { get; set; }

        public double ProductionPeak { get; set; }

        public double ProductionOffPeak { get; set; }

    }
}
