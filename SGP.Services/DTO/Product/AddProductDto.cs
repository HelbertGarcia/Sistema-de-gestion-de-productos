
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGP.Services.DTO.Product
{
    public class AddProductDto
    {
        [MaxLength(100, ErrorMessage = "La longitud del {0} no puede exeder los {1} caracteres")]
        [DisplayName("Nombre del Producto")]
        public string? Name { get; set; }

        [MaxLength(500, ErrorMessage = "La longitud del {0} no puede exeder los {1} caracteres")]
        [DisplayName("Nombre del Producto")]
        public string? Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El {0} debe ser un valor positivo")]
        [DisplayName("Precio del Producto")]
        public decimal Price { get; set; }
    }
}
