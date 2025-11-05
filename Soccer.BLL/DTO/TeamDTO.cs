using System.ComponentModel.DataAnnotations;

namespace Soccer.BLL.DTO
{
    // Data Transfer Object - специальная модель для передачи данных
    // Класс TeamDTO должен содержать только те данные, которые нужно передать 
    // на уровень представления или, наоборот, получить с этого уровня.
    public class TeamDTO
    {
        public int Id { get; set; }

        [Display(Name = "Название клуба")]
        [Required(ErrorMessage = "Поле должно быть установлено.")]
        public string? Name { get; set; }

        [Display(Name = "Тренер клуба")]
        [Required(ErrorMessage = "Поле должно быть установлено.")]
        public string? Coach { get; set; }
    }
}
