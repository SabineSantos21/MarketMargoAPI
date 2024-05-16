using MarketMargoAPI.Models;
using MarketMargoAPI.Models.Enum;
using MarketMargoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketMargoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DashboardController: ControllerBase
    {
        private readonly ConnectionDB _dbContext;

        public DashboardController(ConnectionDB dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("")]
        public async Task<ActionResult<Dashboard>> GetDashboard()
        {
            DashboardService dashboardService = new DashboardService(_dbContext);

            Dashboard dashboard = dashboardService.GetDashboard();

            if (dashboard == null)
            {
                return NotFound();
            }

            return Ok(dashboard);
        }
    }
}
