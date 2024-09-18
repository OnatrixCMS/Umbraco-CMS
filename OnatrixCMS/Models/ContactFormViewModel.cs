namespace OnatrixCMS.Models
{
    public class ContactFormViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Question { get; set; }
        public bool ErrorName { get; set; }
        public bool ErrorEmail { get; set; }
        public bool ErrorQuestion { get; set; }
        public string SuccessMessage { get; set; }
    }
}
