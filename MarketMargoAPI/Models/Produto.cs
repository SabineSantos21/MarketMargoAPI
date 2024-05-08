using System.Text.Json.Serialization;

namespace MarketMargoAPI.Models
{
    public class Produto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string? Nome { get; set; }

        [JsonPropertyName("setor")]
        public string? Setor { get; set; }

        [JsonPropertyName("quantidade")]
        public int Quantidade { get; set; }
        
        [JsonPropertyName("id_categoria")]
        public int Id_Categoria { get; set; }

        [JsonPropertyName("data_criacao")]
        public DateTime Data_criacao { get; set; }

        [JsonPropertyName("data_modificacao")]
        public DateTime Data_modificacao { get; set; }

        [JsonPropertyName("ativo")]
        public bool Ativo { get; set; }
    }

    public class NovoProduto
    {
        [JsonPropertyName("nome")]
        public string? Nome { get; set; }

        [JsonPropertyName("setor")]
        public string? Setor { get; set; }

        [JsonPropertyName("quantidade")]
        public int Quantidade { get; set; }

        [JsonPropertyName("id_categoria")]
        public int Id_Categoria { get; set; }
    }

    public class AtualizarProduto
    {
        [JsonPropertyName("nome")]
        public string? Nome { get; set; }

        [JsonPropertyName("setor")]
        public string? Setor { get; set; }

        [JsonPropertyName("quantidade")]
        public int Quantidade { get; set; }

        [JsonPropertyName("ativo")]
        public bool Ativo { get; set; }
    }
}
