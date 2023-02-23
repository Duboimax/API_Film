using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace API_Film.Models.EntityFramework
{
    [Table("t_e_utilisateur_utl")]
    [Index(nameof(Mail), IsUnique = true)]

    public partial class Utilisateur
    {
        [Key]
        [Column("utl_id")]
        public int UtilisateurId { get; set; }

        [Column("utl_nom")]
        [StringLength(50)]
        public string? Nom { get; set; }

        [Column("utl_prenom")]
        [StringLength(50)]
        public string? Prenom { get; set; }

        [Column("utl_mobile")]
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Le numéro de téléphone ne doit pa dépasser 10 chiffres")]
        [MaxLength(10)]
        public string? Mobile { get;set; }

        [Column("utl_mail")]
        [Required]
        [EmailAddress]
        [StringLength(100, MinimumLength =6, ErrorMessage ="La longeur du mail doit être compris entre 6 et 100" )]
        public string? Mail { get; set; }

        [Column("utl_pwd")]
        [RegularExpression(@"^(?=.\d)(?=.[A-Z])(?=.*\W).{12,20}$", ErrorMessage = "Le pwd doit être compris entre 12 et 20 charactère et doit contenir 1 majuscule, 1chiffre, 1 caractère spécial ")]
        [Required]
        public string? Pwd { get; set; }


        [Column("utl_rue")]
        [StringLength(200)]
        public string? Rue { get; set; }

        [Column("utl_cp")]
        [Required]
        [RegularExpression(@"^\d{1,5}$", ErrorMessage = "Le code postale doit contenir 5 chiffres")]
        public string? CodePostal { get; set; }

        [Column("utl_ville")]
        [StringLength(50)]
        public string? Ville { get; set; }

        [Column("utl_pays")]
        [StringLength(50)]
        public string? Pays { get; set; } = "France";

        [Column("utl_latitude")]
        public float? Latitude { get; set; }

        [Column("utl_longitude")]
        public float? Longitude { get; set; }

        [Column("utl_datecreation", TypeName ="date")]
        [Required]
        public DateTime DateCreation { get; set; } = DateTime.Now;

        [InverseProperty("UtilisateurNaviguation")]
        public virtual ICollection<Notation> NotationUtilisateur { get; set; } = new List<Notation>();



    }
}
