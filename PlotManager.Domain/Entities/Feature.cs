using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlotManager.Domain.Entities
{
    public class Feature
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PlotFeatures> PlotFeatures { get; set; }
    }
}