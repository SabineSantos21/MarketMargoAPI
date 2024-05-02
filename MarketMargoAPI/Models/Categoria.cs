using System.Text.Json.Serialization;

namespace MarketMargoAPI.Models
{
    public class Categoria
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string? Nome { get; set; }

        [JsonPropertyName("data_criacao")]
        public DateTime Data_criacao { get; set; }

        [JsonPropertyName("data_modificacao")]
        public DateTime Data_modificacao { get; set; }

        [JsonPropertyName("ativo")]
        public bool Ativo { get; set; }
    }

    public class NovaCategoria
    {
        [JsonPropertyName("nome")]
        public string? Nome { get; set; }
    }
    
    public class AtualizarCategoria
    {
        [JsonPropertyName("nome")]
        public string? Nome { get; set; }

        [JsonPropertyName("ativo")]
        public bool Ativo { get; set; }
    }
}
