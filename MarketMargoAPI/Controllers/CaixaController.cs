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
        public async Task<ActionResult> CriarTransacao(NovaTransacao novaTransacao)
        {
            try
            {
                CaixaService caixaService = new CaixaService(_dbContext);

                var transacaoCode = caixaService.GenerateTransactionCode();

                foreach (var novaTransacaoItem in novaTransacao.ProdutosTransacao)
                {
                    Caixa caixa = new Caixa();
                    caixa.Id_Produto = novaTransacaoItem.IdProduto;
                    caixa.Quantidade = novaTransacaoItem.Quantidade;
                    caixa.Cod_Barras = caixaService.GenerateRandomBarcode();
                    caixa.Status = StatusTransacao.SUCESSO.GetHashCode();
                    caixa.Transacao_Code = transacaoCode;
                    caixa.Data_criacao = DateTime.Now;
                    caixa.Data_modificacao = DateTime.Now;
                    caixa.Ativo = true;
                    
                    await caixaService.CriarCaixa(caixa);
                }

                return Ok();
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

            if (transactions == null)
            {
                return NotFound();
            }

            return Ok(transactions);
        }
    }
}
