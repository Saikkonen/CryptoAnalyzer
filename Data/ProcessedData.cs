using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAnalyzer.Data
{
    internal class ProcessedData
    {
        public DateTime Date { get; set; }
        public double FirstPrice { get; set; }
        public double VolumeSum { get; set; }
    }
}
