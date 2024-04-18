using System.ComponentModel.DataAnnotations;

namespace eKhaya.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}