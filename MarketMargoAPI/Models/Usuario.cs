using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System;
using MarketMargoAPI.Models.Enum;

namespace MarketMargoAPI.Models
{
    public class Usuario
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public required string Nome { get; set; }

        [JsonPropertyName("email")]
        public required string Email { get; set; }

        [JsonPropertyName("senha")]
        public required string Senha { get; set; }

        [JsonPropertyName("funcao")]
        public Funcao Funcao { get; set; }

        [JsonPropertyName("data_criacao")]
        public DateTime Data_criacao { get; set; }

        [JsonPropertyName("data_modificacao")]
        public DateTime Data_modificacao { get; set; }

        [JsonPropertyName("ativo")]
        public bool Ativo { get; set; }

        [NotMapped]
        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }
}
