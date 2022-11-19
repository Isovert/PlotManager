using System.ComponentModel.DataAnnotations;

namespace PlotManager.Contracts.PlotComplex
{
    public class PlotComplexForCreationDTO
    {
        [Required]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }
    }
}