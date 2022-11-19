using PlotManager.Contracts.Feature;
using System;
using System.Collections.Generic;

namespace PlotManager.Contracts.Plot
{
    public class PlotDTO
    {
        public Guid Id { get; set; }
        public int OrdinalNumber { get; set; }
        public List<FeatureDTO> Features { get; set; }
    }
}