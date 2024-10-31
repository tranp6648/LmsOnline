using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSOnline.Models
{
    public class Document
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [Column(TypeName ="nvarchar(100)")]
        public string Name { get; set; }
        [Required]
        public int TypeDocument {  get; set; }
        public DateOnly ApproveDate { get; set; }
        public int status { get; set; }
        [ForeignKey("Subject")]
        public int idSubject { get; set; }
        public virtual Subject Subject { get; set; }
        [Column(TypeName ="varchar(200)")]
        public string LinkDocument {  get; set; }
    }
}
