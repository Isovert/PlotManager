using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlotManager.Domain.Entities
{
    public class PlotFeatures
    {
        [Key, Column(Order = 1)]
        public Guid PlotId { get; set; }

        [Key, Column(Order = 2)]
        public Guid FeatureId { get; set; }

        public virtual Plot Plot { get; set; }
        public virtual Feature Feature { get; set; }
    }
}