using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSOnline.Models
{
    [Table("Subject")]
    public class Subject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string CodeSubject { get; set; }
        [Required]
        public string NameSubject { get; set; }
        [ForeignKey("Account")]
        public int TeacherId { get; set; }
        public virtual Account Teacher {  get; set; }
        [Column(TypeName ="ntext")]
        public string Description { get; set; }
    }
}
