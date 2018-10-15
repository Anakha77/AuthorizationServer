using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AuthorizationServer.Models
{
    public class UserViewModel : IValidatableObject
    {
        public UserViewModel() { }

        public UserViewModel(Dto.User user)
        {
            SubjectId = user.SubjectId;
            Username = user.Username;
            Password = user.Password;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }
        public Guid SubjectId { get; set; }

        [Display(Name = "Addresse Email")]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [EmailAddress(ErrorMessage = "Addresse non valide")]
        [RegularExpression("^.+@.+\\..+$", ErrorMessage = "Addresse non valide")]
        public string Username { get; set; }

        [Display(Name = "Mot de passe")]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [MinLength(8, ErrorMessage = "Le mot de passe doit contenir au moins 8 caractères")]
        public string Password { get; set; }

        [Display(Name = "Confirmation mot de passe")]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Prénom")]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public string FirstName { get; set; }

        [Display(Name = "Nom")]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public string LastName { get; set; }

        public string ReturnUrl { get; set; }

        public bool RegistrationSucess { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (GetComplexityLevel(Password) < 3)
            {
                yield return new ValidationResult("Le mot de passe doit contenir au moins 3 des 4 types de caractères suivants : Majuscules, minuscules, chiffres, caractères spéciaux", new[] { nameof(Password) });
            }
            if (!Password.Equals(ConfirmPassword))
            {
                yield return new ValidationResult("Le mot de passe n'est pas identique", new[] { nameof(Password), nameof(ConfirmPassword) });
            }
        }

        private int GetComplexityLevel(string password)
        {
            int complexity = 0;

            var regExMajuscules = new Regex("[A-Z]+");
            var regExMinuscules = new Regex("[a-z]+");
            var regExChiffres = new Regex("[0-9]+");
            var regExCaracteresSpecieux = new Regex("[&#'{([-`_ç^@)]=}$*µ%ù!§²<>]+");

            if (regExMajuscules.IsMatch(password))
                complexity++;

            if (regExMinuscules.IsMatch(password))
                complexity++;

            if (regExChiffres.IsMatch(password))
                complexity++;

            if (regExCaracteresSpecieux.IsMatch(password))
                complexity++;

            return complexity;
        }
    }
}
