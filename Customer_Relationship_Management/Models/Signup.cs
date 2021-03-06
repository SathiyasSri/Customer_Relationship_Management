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

    public partial class Signup
    {
        public int id { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        [Required]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        public Nullable<System.DateTime> Createddate { get; set; }
        [Required]
        public string RoleName { get; set; }

        public virtual AdminEmployee AdminEmployee { get; set; }
        public Signup()
        {
            this.Createddate = DateTime.UtcNow;
        }
    }
}
