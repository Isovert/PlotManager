using PlotManager.Contracts.Feature;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlotManager.Contracts.Plot
{
    public class PlotForUpdateDTO
    {
        [Required(ErrorMessage = "Ordinal Number is required")]
        public int OrdinalNumber { get; set; }
        public List<FeatureDTO> Features { get; set; }
    }
}