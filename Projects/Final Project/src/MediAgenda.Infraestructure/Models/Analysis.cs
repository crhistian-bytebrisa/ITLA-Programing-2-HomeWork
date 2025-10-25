﻿using MediAgenda.Infraestructure.Models.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Models
{
    [Table("Analysis")]
    public class Analysis
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }

        //Map
        public List<PrescriptionAnalysis> PrescriptionAnalyses { get; set; } = new();
        public Analysis() { }
    }
}
