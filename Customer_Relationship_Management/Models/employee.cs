//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Customer_Relationship_Management.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public employee()
        {
            this.PayRolls = new HashSet<PayRoll>();
        }
        [Required]
        public int Employee_Id { get; set; }
        [ForeignKey("Employee_Id")]
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string MailId { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public Nullable<System.DateTime> DateofBirth { get; set; }
        [Required]
        public string Username { get; set; }

        public virtual AdminEmployee AdminEmployee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PayRoll> PayRolls { get; set; }
    }
}
