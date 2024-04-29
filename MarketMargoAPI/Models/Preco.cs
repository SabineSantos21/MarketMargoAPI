using System.Text.Json.Serialization;

namespace MarketMargoAPI.Models
{
    public class Preco
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("idProduto")]
        public int Id_Produto { get; set; }

        [JsonPropertyName("porcentagemAumento")]
        public double Porcentagem_Aumento { get; set; }

        [JsonPropertyName("valor")]
        public double Valor { get; set; }

        [JsonPropertyName("data_criacao")]
        public DateTime Data_criacao { get; set; }

        [JsonPropertyName("data_modificacao")]
        public DateTime Data_modificacao { get; set; }

        [JsonPropertyName("ativo")]
        public bool Ativo { get; set; }
    }
}
