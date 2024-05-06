using System.ComponentModel.DataAnnotations;

namespace WebApiBackend.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
