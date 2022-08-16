using System.Text.Json.Serialization;

namespace BlogAPI.Src.Utilidades
{
    //public class Enum
    //{
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum TipoUsuario
        {
            NORMAL,
            ADMINISTRADOR
        }
    //}
}
