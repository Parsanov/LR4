using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LR4.Core.Model
{
    public class DentistryPatient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Surname { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Pname { get; set; } = string.Empty;

        [Required]
        public DateOnly Dr { get; set; }

        [Required]
        public bool Gender { get; set; }

        public string? StateComments { get; set; }

        [Required]
        public string DoctorCode { get; set; } = string.Empty;

        [Required]
        public string Posluga { get; set; } = string.Empty;

        [Required]
        public DateTime TimeVisit { get; set; }

        [Required]
        public double Cost { get; set; }
    }
}
