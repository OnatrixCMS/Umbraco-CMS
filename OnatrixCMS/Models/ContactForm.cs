namespace OnatrixCMS.Models;

public class ContactForm
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Question { get; set; }
    public string? SelectedOption { get; set; }
}
