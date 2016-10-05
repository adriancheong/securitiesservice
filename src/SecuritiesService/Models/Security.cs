using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuritiesService.Models
{
    public class Security
    {
        public string SecurityId { get; set; }
        public string Owner { get; set; }
        public SecurityType Type { get; set; }
        public double Valuation { get; set; }
        public string Currency { get; set; }
        public string CountryOfOrigin { get; set; }
    }

    public enum SecurityType
    {
        Debt,
        Equity,
        Hybrid
    }
}
