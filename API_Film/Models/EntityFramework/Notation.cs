using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Film.Models.EntityFramework
{
    [Table("Notation")]
    public partial class Notation
    {
        [Key]
        [Required]
        [Column("utl_Id")]

        public int 
    }
}
