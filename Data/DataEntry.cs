using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoAnalyzer.Data
{
    internal class DataEntry
    {
        [JsonPropertyName("prices")]
        public List<List<double>>? Prices { get; set; }

        [JsonPropertyName("market_caps")]
        public List<List<double>>? Market_caps { get; set; }

        [JsonPropertyName("total_volumes")]
        public List<List<double>>? Total_volumes { get; set; }
    }
}
