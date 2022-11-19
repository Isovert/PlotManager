using System;
using System.ComponentModel.DataAnnotations;

namespace PlotManager.Contracts.Feature
{
    public class FeatureForUpdateDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, ErrorMessage = "Name cannot be logner than 20 characters")]
        public string Name { get; set; }
    }
}