using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebRest.EF.Models;

[Table("STUDENT")]
public partial class Student
{
    [Key]
    [Column("STUDENT_GUID")]
    [StringLength(32)]
    [Unicode(false)]
    public string StudentGuid { get; set; } = null!;

    [Column("SALUTATION")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Salutation { get; set; }

    [Column("FIRST_NAME")]
    [StringLength(25)]
    [Unicode(false)]
    public string? FirstName { get; set; }

    [Column("LAST_NAME")]
    [StringLength(25)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [Column("PHONE")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [Column("REGISTRATION_DATE", TypeName = "DATE")]
    public DateTime RegistrationDate { get; set; }

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

    [InverseProperty("Student")]
    public virtual ICollection<Enrollment> Enrollment { get; set; } = new List<Enrollment>();

    [InverseProperty("Student")]
    public virtual ICollection<StudentAddress> StudentAddress { get; set; } = new List<StudentAddress>();

    [InverseProperty("Student")]
    public virtual ICollection<StudentEmployer> StudentEmployer { get; set; } = new List<StudentEmployer>();
}
