using System.ComponentModel.DataAnnotations;

namespace Practico_9_APIs.Modelos.Dto
{
    public class EPDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Nombre { get; set; }

        public int Opcupantes {  get; set; }    
        public int MetrosCuadrados { get; set; }
    }
}
