using System.ComponentModel.DataAnnotations;

namespace Soccer.BLL.DTO
{
    // Data Transfer Object - специальная модель для передачи данных
    // Класс PlayerDTO должен содержать только те данные, которые нужно передать 
    // на уровень представления или, наоборот, получить с этого уровня.
    public class PlayerDTO
    {
        public int Id { get; set; }

        [Display(Name = "Имя игрока")]
        [Required(ErrorMessage = "Поле должно быть установлено.")]
        public string? Name { get; set; }

        [Display(Name = "Год рождения игрока")]
        [Required(ErrorMessage = "Поле должно быть установлено.")]
        public int BirthYear { get; set; }

        [Display(Name = "Позиция на поле")]
        [Required(ErrorMessage = "Поле должно быть установлено.")]
        public string? Position { get; set; }

        public int? TeamId { get; set; }

        [Display(Name = "Название клуба")]
        public string? Team { get; set; }
    }
}
