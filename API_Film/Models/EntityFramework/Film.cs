using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace API_Film.Models.EntityFramework
{
    [Table("t_e_film_flm")]
    public partial class Film
    {
        [Key]
        [Column("flm_id")]
        public int Filmid { get; set; }

        [Column("flm_titre")]
        [Required]
        [StringLength(100)]
        public string Titre { get; set; }

        [Column("flm_resume")]
        public string? Resume { get; set; }

        [Column("flm_datesortie", TypeName ="date")]
        public DateTime? DateSortie { get; set; } = null!;

        [Column("flm_duree")]
        public decimal? Duree { get; set; }

        [Column("flm_genre")]
        [StringLength(30)]
        public string? Genre { get; set; }
        
        [InverseProperty("FilmNavigation")]
        public virtual ICollection<Notation> NotationFilm { get; } = new List<Notation>();
    }
}
