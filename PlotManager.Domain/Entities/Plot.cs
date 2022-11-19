using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlotManager.Domain.Entities
{
    public class Plot
    {
        [Key]
        public Guid Id { get; set; }        
        public int OrdinalNumber { get; set; }
        public virtual ICollection<PlotFeatures> PlotFeatures { get; set; }
        public Guid PlotComplexId { get; set; }
        public virtual PlotComplex PlotComplex { get; set; }
    }
}