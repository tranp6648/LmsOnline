using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSOnline.Models
{
    [Table("Account")]
    public class Account
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName ="varchar(200)")]
        public string UserCode { get; set; }
        [Required]
        public bool gender { get; set; }
        [Required]
        public int AccountType { get; set; }
        [Required]
        [Column(TypeName ="varchar(150)")]
        public string Email { get; set; }
        [Column(TypeName ="varchar(200)")]
        [Required]
        public string Username { get; set; }
        [Column(TypeName ="varchar(200)")]
        [Required]
        public string Password { get; set; }
        [Column(TypeName ="varchar(12)")]
        [Required]
        public string Phone {  get; set; }
        [Column(TypeName ="nvarchar(150)")]
        [Required]

        public string Address { get; set; }

        [Column(TypeName ="nvarchar(200)")]
        [Required]
        public string FullName {  get; set; }
        [Column(TypeName ="varchar(200)")]
        [Required]
        public string Avatar {  get; set; }
    }
}
