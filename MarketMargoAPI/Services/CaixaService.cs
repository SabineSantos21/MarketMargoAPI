using MarketMargoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketMargoAPI.Services
{
    public class CaixaService
    {
        private readonly ConnectionDB _dbContext;

        public CaixaService(ConnectionDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Caixa>> GetTransacaoByCode(string cod_transacao)
        {
            return await _dbContext.TbCaixa.Where(c => c.Transacao_Code == cod_transacao).ToListAsync();
        }

        public async Task<Caixa> CriarCaixa(Caixa caixa)
        {
            try
            {
                _dbContext.TbCaixa.Add(caixa);
                await _dbContext.SaveChangesAsync();

                return caixa;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GenerateRandomBarcode()
        {
            Random random = new Random();
            const string chars = "0123456789";
            char[] barcodeChars = new char[12];

            for (int i = 0; i < barcodeChars.Length; i++)
            {
                barcodeChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(barcodeChars);
        }

        public string GenerateTransactionCode()
        {
            string prefix = "TR"; // Prefixo do código da transação
            string dateTimeFormat = "yyyyMMddHHmmss"; // Formato de data e hora
            string timestamp = DateTime.Now.ToString(dateTimeFormat); // Obter data e hora atual formatada
            string randomDigits = GenerateRandomDigits(4); // Gerar 4 dígitos aleatórios
            return prefix + timestamp + randomDigits;
        }

        static string GenerateRandomDigits(int length)
        {
            Random random = new Random();
            const string digits = "0123456789";
            char[] randomChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                randomChars[i] = digits[random.Next(digits.Length)];
            }

            return new string(randomChars);
        }
    }
}
