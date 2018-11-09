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
    [Table("SW_OW_BASEINFO")]
    public class SW_OW_BASEINFO : SW_WELL_BASEINFO
    {
        [DataMember]
        [Display(Name = "地理位置")]
        [MaxLength(2000)]
        public string POSITION { get; set; }
        [DataMember]
        [Display(Name = "补心海拔（m）")]
        public double? KB_ELEVATION { get; set; }
        [DataMember]
        [Display(Name = "所属管理单元")]
        [MaxLength(2000)]
        public string MANAGE_UNIT { get; set; }
        [DataMember]
        [Display(Name = "所属站点")]
        [MaxLength(2000)]
        public string WF_STATION { get; set; }
        [DataMember]
        [Display(Name = "所属井组")]
        [MaxLength(2000)]
        public string WELL_GROUP { get; set; }
        [DataMember]
        [Display(Name = "集输方式")]
        [MaxLength(2000)]
        public string GATHERING_METHOD { get; set; }
    }
}
