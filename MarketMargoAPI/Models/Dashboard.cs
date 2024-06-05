using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MarketMargoAPI.Models
{
    
    public class Dashboard
    {
        [JsonPropertyName("cardProdutosCadastrados")]
        public Card? ProdutosCadastradosHoje { get; set; }

        [JsonPropertyName("cardTransacoesHoje")]
        public Card? TransacoesHoje { get; set; }

        [JsonPropertyName("cardVendasComSucesso")]
        public Card? VendasComSucesso { get; set; }

        [JsonPropertyName("cardVendasComInsucesso")]
        public Card? VendasComInsucesso { get; set; }

        [JsonPropertyName("chatQuantidadeProdutosVendidosPorCategoria")]
        public ChatPie? ChatQuantidadeProdutosVendidosPorCategoria { get; set; }
        
        [JsonPropertyName("chatQuantidadeDeVendasHojePorProduto")]
        public ChatPie? GetQuantidadeDeVendasHojePorProduto { get; set; }
    }
    
    public class Card
    {
        [JsonPropertyName("descricao")]
        public string? Descricao { get; set; }

        [JsonPropertyName("valor")]
        public int Valor { get; set; }
    }

    public class ChatPie
    {
        public List<string>? Label { get; set; }

        public List<int>? Value { get; set; }
    }
}
