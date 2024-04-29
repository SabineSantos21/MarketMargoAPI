using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System;
using MarketMargoAPI.Models.Enum;

namespace MarketMargoAPI.Models
{
    public class Usuario
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Nome")]
        public string? Nome { get; set; }

        [JsonPropertyName("Email")]
        public string? Email { get; set; }

        [JsonPropertyName("Senha")]
        public string? Senha { get; set; }

        [JsonPropertyName("Funcao")]
        public Funcao Funcao { get; set; }

        [JsonPropertyName("Data_criacao")]
        public DateTime Data_criacao { get; set; }

        [JsonPropertyName("Data_modificacao")]
        public DateTime Data_modificacao { get; set; }

        [JsonPropertyName("Ativo")]
        public bool Ativo { get; set; }

        [NotMapped]
        [JsonPropertyName("Token")]
        public string? Token { get; set; }
    }

    public class NovoUsuario
    {
        [JsonPropertyName("nome")]
        public string? Nome { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("senha")]
        public string? Senha { get; set; }
    }

    public class AtualizarUsuario
    {
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("senha")]
        public string? Senha { get; set; }
    }
}
