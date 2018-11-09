using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WPEDMAS.Models.SW
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(SW_OW_BASEINFO))]
    [Table("SW_WELL_BASEINFO")]
    public class SW_WELL_BASEINFO
    {
        [Key]
        [Display(Name = "井名")]
        [DataMember]
        public string WELL_NAME { get; set; }
        [DataMember]
        [Display(Name = "油田")]
        [MaxLength(2000)]
        public string OIL_FIELD { get; set; }
        [DataMember]
        [Display(Name = "区块")]
        [MaxLength(2000)]
        public string BLOCK { get; set; }
        [DataMember]
        [Display(Name = "作业区")]
        [MaxLength(2000)]
        public string AREA { get; set; }
        [DataMember]
        [Display(Name = "地面坐标X")]
        public double? COORDINATE_X { get; set; }
        [DataMember]
        [Display(Name = "地面坐标Y")]
        public double? COORDINATE_Y { get; set; }
        [DataMember]
        [Display(Name = "地面海拔（m）")]
        public double? ELEVATION { get; set; }
        [DataMember]
        [Display(Name = "投用日期")]
        public DateTime? USE_DATE { get; set; }
        [DataMember]
        [Display(Name = "井别")]
        [MaxLength(2000)]
        public string TYPE { get; set; }
        [DataMember]
        [Display(Name = "井型")]
        [MaxLength(2000)]
        public string KIND { get; set; }
    }

}
