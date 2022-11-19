using System;
using System.ComponentModel.DataAnnotations;

namespace PlotManager.Contracts.PlotComplex
{
    public class PlotComplexForUpdateDTO
    {
        [Required]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }
    }
}