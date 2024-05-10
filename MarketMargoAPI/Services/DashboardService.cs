using MarketMargoAPI.Models;
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

        
    }
}
