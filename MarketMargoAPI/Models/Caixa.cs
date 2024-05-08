using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MarketMargoAPI.Models
{
    public class Caixa
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("id_produto")]
        public int Id_Produto { get; set; }

        [NotMapped]
        public Produto? Produto { get; set; }

        [JsonPropertyName("quantidade")]
        public int Quantidade { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("preco_produto")]
        public double Preco_Produto { get; set; }

        [JsonPropertyName("cod_barras")]
        public string? Cod_Barras { get; set; }

        [JsonPropertyName("transacao_code")]
        public string? Transacao_Code { get; set; }

        [JsonPropertyName("data_criacao")]
        public DateTime Data_criacao { get; set; }

        [JsonPropertyName("data_modificacao")]
        public DateTime Data_modificacao { get; set; }

        [JsonPropertyName("ativo")]
        public bool Ativo { get; set; }
    }

    public class NovaTransacao
    {
        [JsonPropertyName("produtos_transacao")]
        public List<ProdutosTransacao>? ProdutosTransacao { get; set; }
    }

    public class ProdutosTransacao
    {
        [JsonPropertyName("id_produto")]
        public int IdProduto { get; set; }

        [JsonPropertyName("quantidade ")]
        public int Quantidade { get; set; }
    }
}
