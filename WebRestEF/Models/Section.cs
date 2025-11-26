using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebRest.EF.Models;

[Table("SECTION")]
public partial class Section
{
    [Key]
    [Column("SECTION_GUID")]
    [StringLength(32)]
    [Unicode(false)]
    public string SectionGuid { get; set; } = null!;

    [Column("COURSE_GUID")]
    [StringLength(32)]
    [Unicode(false)]
    public string CourseGuid { get; set; } = null!;

    [Column("SECTION_NO")]
    [Precision(3)]
    public byte SectionNo { get; set; }

    [Column("START_DATE_TIME", TypeName = "DATE")]
    public DateTime? StartDateTime { get; set; }

    [Column("INSTRUCTOR_GUID")]
    [StringLength(32)]
    [Unicode(false)]
    public string InstructorGuid { get; set; } = null!;

    [Column("CAPACITY")]
    [Precision(3)]
    public byte? Capacity { get; set; }

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

    [ForeignKey("CourseGuid")]
    [InverseProperty("Section")]
    public virtual Course Course { get; set; } = null!;

    [InverseProperty("Section")]
    public virtual ICollection<Enrollment> Enrollment { get; set; } = new List<Enrollment>();

    [InverseProperty("Section")]
    public virtual ICollection<SectionLocation> SectionLocation { get; set; } = new List<SectionLocation>();
}
