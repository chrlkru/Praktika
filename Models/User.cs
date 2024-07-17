using System.ComponentModel.DataAnnotations;

namespace Praktika.Models;

public class User : EntityBase
{
    [Required(ErrorMessage = "ФИО обязательно для заполнения.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "ФИО должно быть от 3 до 100 символов.")]
    public string Фио { get; set; } = null!;

    [Required(ErrorMessage = "Телефон обязателен для заполнения.")]
    [Phone(ErrorMessage = "Некорректный номер телефона.")]
    public string Телефон { get; set; } = null!;

    [Required(ErrorMessage = "Email обязателен для заполнения.")]
    [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Дата рождения обязательна для заполнения.")]
    [DataType(DataType.Date)]
    public DateTime ДатаРождения { get; set; }

    public List<DiscontCard> DiscontCards { get; set; } = new List<DiscontCard>();

    public List<OrderList> OrderLists { get; set; } = new List<OrderList>(); 
}
