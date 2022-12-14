// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CaseManagementReports.Models
{
    public partial class Hospitalpolicy
    {
        public int PolicyIdentificationId { get; set; }
        public string PolicyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int? PolicyMakerId { get; set; }
        public string DocumentPath { get; set; }
        public string Version { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Policymaker PolicyMaker { get; set; }
    }
}