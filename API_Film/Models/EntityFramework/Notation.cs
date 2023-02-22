using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Film.Models.EntityFramework
{
    [Table("t_j_notation_not")]
    public partial class Notation
    {
        [Key]
        [Column("utl_id")]
        public int UtilisateurId { get; set; }

        [Key]
        [Column("flm_id")]
        public int FilmId { get; set; }

        [Column("not_note")]
        [Required]
        [Range(0, 5)]
        public int Note { get; set; }

        [ForeignKey("UtilisateurId")]
        [InverseProperty("NotationUtilisateur")]
        public virtual Utilisateur UtilisateurNaviguation { get; set; } = null!;

        [ForeignKey("FilmId")]
        [InverseProperty("NotationFilm")]
        public virtual Film FilmNavigation { get; set; } = null!;

    }
}

