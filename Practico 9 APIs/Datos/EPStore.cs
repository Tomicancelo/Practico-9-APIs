using Practico_9_APIs.Modelos.Dto;

namespace Practico_9_APIs.Datos
{
    public class EPStore
    {
        public static List<EPDto> EPList = new List<EPDto>
        {
            new EPDto{Id=1, Nombre="Vista a a Pisina", Opcupantes=3, MetrosCuadrados=50},
            new EPDto{Id=2, Nombre="Vista a la Playa", Opcupantes=2, MetrosCuadrados=100 }
        };
    }
}