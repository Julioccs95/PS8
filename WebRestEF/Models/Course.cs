using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebRest.EF.Models;

[Table("COURSE")]
public partial class Course
{
    [Key]
    [Column("COURSE_GUID")]
    [StringLength(32)]
    [Unicode(false)]
    public string CourseGuid { get; set; } = null!;

    [Column("PREREQUISITE_GUID")]
    [StringLength(32)]
    [Unicode(false)]
    public string? PrerequisiteGuid { get; set; }

    [Column("COURSE_NO")]
    [Precision(8)]
    public int CourseNo { get; set; }

    [Column("DESCRIPTION")]
    [StringLength(50)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [Column("COST", TypeName = "NUMBER(9,2)")]
    public decimal? Cost { get; set; }

    [Column("CREATED_BY")]
    [StringLength(30)]
    [Unicode(false)]
    public string CreatedBy { get; set; } = null!;

    [Column("CREATED_DATE", TypeName = "DATE")]
    public DateTime CreatedDate { get; set; }

    [Column("MODIFIED_BY")]
    [StringLength(30)]
    [Unicode(false)]
    public string ModifiedBy { get; set; } = null!;

    [Column("MODIFIED_DATE", TypeName = "DATE")]
    public DateTime ModifiedDate { get; set; }

    [InverseProperty("Prerequisite")]
    public virtual ICollection<Course> InversePrerequisite { get; set; } = new List<Course>();

    [ForeignKey("PrerequisiteGuid")]
    [InverseProperty("InversePrerequisite")]
    public virtual Course? Prerequisite { get; set; }

    [InverseProperty("Course")]
    public virtual ICollection<Section> Section { get; set; } = new List<Section>();
}
