using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebRest.EF.Models;

[Table("ENROLLMENT")]
public partial class Enrollment
{
    [Key]
    [Column("ENROLLMENT_GUID")]
    [StringLength(32)]
    [Unicode(false)]
    public string EnrollmentGuid { get; set; } = null!;

    [Column("STUDENT_GUID")]
    [StringLength(32)]
    [Unicode(false)]
    public string? StudentGuid { get; set; }

    [Column("SECTION_GUID")]
    [StringLength(32)]
    [Unicode(false)]
    public string? SectionGuid { get; set; }

    [Column("ENROLL_DATE", TypeName = "DATE")]
    public DateTime? EnrollDate { get; set; }

    [Column("FINAL_GRADE")]
    [Precision(3)]
    public byte? FinalGrade { get; set; }

    [Column("CREATED_BY")]
    [StringLength(30)]
    [Unicode(false)]
    public string? CreatedBy { get; set; }

    [Column("CREATED_DATE", TypeName = "DATE")]
    public DateTime? CreatedDate { get; set; }

    [Column("MODIFIED_BY")]
    [StringLength(30)]
    [Unicode(false)]
    public string? ModifiedBy { get; set; }

    [Column("MODIFIED_DATE", TypeName = "DATE")]
    public DateTime? ModifiedDate { get; set; }

    [ForeignKey("SectionGuid")]
    [InverseProperty("Enrollment")]
    public virtual Section? Section { get; set; }

    [ForeignKey("StudentGuid")]
    [InverseProperty("Enrollment")]
    public virtual Student? Student { get; set; }
}
