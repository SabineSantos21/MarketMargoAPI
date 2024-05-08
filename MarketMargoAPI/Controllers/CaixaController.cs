using MarketMargoAPI.Models;
using MarketMargoAPI.Models.Enum;
using MarketMargoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketMargoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CaixaController: ControllerBase
    {
        private readonly ConnectionDB _dbContext;

        public CaixaController(ConnectionDB dbContext)
        {
            _dbContext = dbContext;
        }

        
        [HttpPost]
        public async Task<ActionResult<List<Caixa>>> CriarTransacao(NovaTransacao novaTransacao)
        {
            try
            {
                CaixaService caixaService = new CaixaService(_dbContext);
                PrecoService precoService = new PrecoService(_dbContext);

                var transacaoCode = caixaService.GenerateTransactionCode();

                foreach (var novaTransacaoItem in novaTransacao.ProdutosTransacao)
                {
                    Caixa caixa = new Caixa();
                    caixa.Id_Produto = novaTransacaoItem.IdProduto;
                    caixa.Quantidade = novaTransacaoItem.Quantidade;
                    caixa.Cod_Barras = caixaService.GenerateRandomBarcode();
                    caixa.Status = StatusTransacao.SUCESSO.GetHashCode();
                    caixa.Transacao_Code = transacaoCode;
                    caixa.Preco_Produto = precoService.GetPrecoByProdutoId(novaTransacaoItem.IdProduto).Valor;
                    caixa.Data_criacao = DateTime.Now;
                    caixa.Data_modificacao = DateTime.Now;
                    caixa.Ativo = true;
                    
                    await caixaService.CriarCaixa(caixa);
                }

                List<Caixa> transactions = await caixaService.GetTransacaoByCode(transacaoCode);

                return Ok(transactions);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{cod_transacao}")]
        public async Task<ActionResult<List<Caixa>>> GetTrasacoesByCode(string cod_transacao)
        {
            CaixaService caixaService = new CaixaService(_dbContext);

            List<Caixa> transactions = await caixaService.GetTransacaoByCode(cod_transacao);

            ProdutoService produtoService = new ProdutoService(_dbContext);

            foreach (var item in transactions)
            {
                item.Produto = produtoService.GetProdutoById(item.Id_Produto).Result;
            }

            if (transactions == null)
            {
                return NotFound();
            }

            return Ok(transactions);
        }
    }
}
