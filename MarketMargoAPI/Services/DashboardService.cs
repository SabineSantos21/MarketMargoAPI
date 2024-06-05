using MarketMargoAPI.Models;
using MarketMargoAPI.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace MarketMargoAPI.Services
{
    public class DashboardService
    {
        private readonly ConnectionDB _dbContext;

        public DashboardService(ConnectionDB dbContext)
        {
            _dbContext = dbContext;
        }

        public Dashboard GetDashboard()
        {
            try
            {
                Dashboard dashboard = new Dashboard()
                {
                    ProdutosCadastradosHoje = GetCardProdutosCadastradosHoje(),
                    TransacoesHoje = GetCardTransacoesHoje(),
                    VendasComSucesso = GetCardTransacoesSucessoHoje(),
                    VendasComInsucesso = GetCardTransacoesInsucessoHoje(),
                    ChatQuantidadeProdutosVendidosPorCategoria = GetQuantidadeProdutosVendidosPorCategoria(),
                    GetQuantidadeDeVendasHojePorProduto = GetQuantidadeDeVendasHojePorProduto()
                };

                return dashboard;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Card GetCardProdutosCadastradosHoje()
        {
            try
            {
                Card card = new Card()
                {
                    Descricao = "Produtos Cadastrados (Hoje)",
                    Valor = _dbContext.TbProduto.Where(p => p.Data_criacao.Date == DateTime.Now.Date).ToList().Count
                };

                return card;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Card GetCardTransacoesHoje()
        {
            try
            {
                Card card = new Card()
                {
                    Descricao = "Transações (Hoje)",
                    Valor = _dbContext.TbCaixa.Where(p => p.Data_criacao.Date == DateTime.Now.Date).ToList().Count
                };

                return card;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Card GetCardTransacoesSucessoHoje()
        {
            try
            {
                Card card = new Card()
                {
                    Descricao = "Vendas com Sucesso (Hoje)",
                    Valor = _dbContext.TbCaixa.Where(p => p.Data_criacao.Date == DateTime.Now.Date && p.Status == StatusTransacao.SUCESSO.GetHashCode()).ToList().Count
                };

                return card;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Card GetCardTransacoesInsucessoHoje()
        {
            try
            {
                Card card = new Card()
                {
                    Descricao = "Vendas com Insucesso (Hoje)",
                    Valor = _dbContext.TbCaixa.Where(p => p.Data_criacao.Date == DateTime.Now.Date && p.Status == StatusTransacao.INSUCESSO.GetHashCode()).ToList().Count
                };

                return card;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ChatPie GetQuantidadeProdutosVendidosPorCategoria()
        {
            try
            {
                ChatPie chartPie = new ChatPie()
                {
                    Label = new List<string>(),
                    Value = new List<int>()
                };

                var produtos = _dbContext.TbCaixa
                    .Join(_dbContext.TbProduto,
                        caixa => caixa.Id_Produto,
                        produto => produto.Id,
                        (caixa, produto) => new { Caixa = caixa, Produto = produto })
                    .Where(t => t.Caixa.Data_criacao.Date == DateTime.Now.Date)
                    .Select(t => new
                    {
                        Produto = t.Produto,
                        IdCategoria = t.Produto.Id_Categoria,
                        Quantidade = t.Caixa.Quantidade
                    })
                    .ToList();

                var produtosAgrupados = produtos
                    .GroupBy(t => new { t.Produto.Id, t.IdCategoria })
                    .Select(g => new
                    {
                        IdCategoria = g.Key.IdCategoria,
                        Quantidade = g.Sum(p => p.Quantidade)
                    })
                    .ToList();

                foreach (var item in produtosAgrupados)
                {
                    var categoria = _dbContext.TbCategoria.FirstOrDefault(c => c.Id == item.IdCategoria)?.Nome;
                    if (categoria != null)
                    {
                        int index = chartPie.Label.IndexOf(categoria);
                        if (index != -1)
                        {
                            chartPie.Value[index] += item.Quantidade;
                        }
                        else
                        {
                            chartPie.Label.Add(categoria);
                            chartPie.Value.Add(item.Quantidade);
                        }
                    }
                }

                return chartPie;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ChatPie GetQuantidadeDeVendasHojePorProduto()
        {
            try
            {
                ChatPie chartPie = new ChatPie()
                {
                    Label = new List<string>(),
                    Value = new List<int>()
                };

                var produtos = _dbContext.TbCaixa
                   .Join(_dbContext.TbProduto,
                       caixa => caixa.Id_Produto,
                       produto => produto.Id,
                       (caixa, produto) => new { Caixa = caixa, Produto = produto })
                   .Where(t => t.Caixa.Data_criacao.Date == DateTime.Now.Date)
                   .Select(t => new
                   {
                       Produto = t.Produto,
                       IdCategoria = t.Produto.Id_Categoria,
                       Quantidade = t.Caixa.Quantidade
                   })
                   .ToList();

                var produtosAgrupados = produtos
                    .Select(t => new
                    {
                        Produto = t.Produto,
                        Quantidade = t.Quantidade
                    })
                    .AsEnumerable()
                    .GroupBy(t => t.Produto)
                    .ToList();

                foreach (var item in produtosAgrupados)
                {
                    if (item.Key != null)
                    {
                        int index = chartPie.Label.IndexOf(item.Key.Nome);
                        if (index != -1)
                        {
                            chartPie.Value[index] += item.Key.Quantidade;
                        }
                        else
                        {
                            chartPie.Label.Add(item.Key.Nome);
                            chartPie.Value.Add(item.Key.Quantidade);
                        }
                    }
                }

                return chartPie;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
