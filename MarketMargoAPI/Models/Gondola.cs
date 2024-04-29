using System.Text.Json.Serialization;

namespace MarketMargoAPI.Models
{
    public class Gondola
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public required string Nome { get; set; }

        [JsonPropertyName("categoria")]
        public int Capacidade { get; set; }

        [JsonPropertyName("id_categoria")]
        public int Id_Categoria { get; set; }

        [JsonPropertyName("data_criacao")]
        public DateTime Data_criacao { get; set; }

        [JsonPropertyName("data_modificacao")]
        public DateTime Data_modificacao { get; set; }

        [JsonPropertyName("ativo")]
        public bool Ativo { get; set; }
    }
}
