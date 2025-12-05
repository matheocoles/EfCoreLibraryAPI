using System.ComponentModel.DataAnnotations; // Nécessaire pour les validations

namespace EfCoreLibraryAPI.DTO.Login.Request;

public class CreateLoginDto
{
    [Required(ErrorMessage = "Le nom d'utilisateur est requis.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "L'identifiant doit faire entre 3 et 50 caractères.")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le nom complet est requis.")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le mot de passe est requis.")]
    [MinLength(6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères.")]
    public string Password { get; set; } = string.Empty;

    // Ajout du champ Rôle (Optionnel, par défaut "User")
    // Cela te permet d'envoyer "Admin" via Swagger
    public string Role { get; set; } = "User";
}