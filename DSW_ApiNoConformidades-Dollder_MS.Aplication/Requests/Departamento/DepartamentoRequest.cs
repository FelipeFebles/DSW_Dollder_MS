using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests;
using System.Globalization;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Departamento
{
    public class DepartamentoRequest : BaseRequest
    {
        // Constructor para limpiar automáticamente las cadenas
        public DepartamentoRequest()
        {
            LimpiarCadenas();
        }

        //Elementos de la clase DepartamentoRequest

        public string? nombre { get; set; }         //Nombre del departamento
        public string? cargo { get; set; }          //Cargo en el departamento


        private void LimpiarCadenas()
        {
            nombre = LimpiarCadena(nombre);
            cargo = LimpiarCadena(cargo);
        }

        // Método para limpiar una cadena y poner la primera letra en mayúscula
        private string? LimpiarCadena(string? valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return valor;

            // Eliminar espacios innecesarios
            valor = valor.Trim();

            // Poner la primera letra en mayúscula
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(valor.ToLower());
        }
    }
}
