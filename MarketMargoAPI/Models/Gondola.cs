using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MarketMargoAPI.Models
{
    public class Gondola
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string? Nome { get; set; }

        [JsonPropertyName("capacidade")]
        public int Capacidade { get; set; }

        [NotMapped]
        [JsonPropertyName("nomeCategoria")]
        public string? NomeCategoria { get; set; }

        [JsonPropertyName("id_categoria")]
        public int Id_Categoria { get; set; }

        [JsonPropertyName("data_criacao")]
        public DateTime Data_criacao { get; set; }

        [JsonPropertyName("data_modificacao")]
        public DateTime Data_modificacao { get; set; }

        [JsonPropertyName("ativo")]
        public bool Ativo { get; set; }
    }

    public class NovaGondola
    {
        [JsonPropertyName("nome")]
        public required string Nome { get; set; }

        [JsonPropertyName("capacidade")]
        public int Capacidade { get; set; }

        [JsonPropertyName("id_categoria")]
        public int Id_Categoria { get; set; }
    }

    public class AtualizarGondola
    {
        [JsonPropertyName("nome")]
        public required string Nome { get; set; }

        [JsonPropertyName("capacidade")]
        public int Capacidade { get; set; }

        [JsonPropertyName("id_categoria")]
        public int Id_Categoria { get; set; }

        [JsonPropertyName("ativo")]
        public bool Ativo { get; set; }
    }
}
