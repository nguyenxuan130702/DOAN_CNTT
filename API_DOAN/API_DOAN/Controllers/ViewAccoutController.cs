using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using API_DOAN_Infrastructure.Interface;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace API_DOAN.Controllers
{
    [Route("api/v1/viewaccout")]
    [ApiController]
    public class ViewAccoutController : ControllerBase
    {
        IVAccoutNameRepository _nameRepository; 
        IDOANDbContext _dbContext; 
        public ViewAccoutController(IVAccoutNameRepository nameRepository, IDOANDbContext dbContext)
        {
            _nameRepository = nameRepository;
            _dbContext = dbContext; 
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var res = _nameRepository.GetAll();
            return StatusCode(200, res); 
        }
        [HttpGet]
        [Route("list")]
        public IActionResult List(string userName)
        {
            var sql = $"SELECT * FROM accoutname WHERE UserAccout != @name";
            var paramaters = new DynamicParameters();
            paramaters.Add("@name", userName);
            var res = _dbContext.Connection.Query<accoutname>(sql, paramaters, transaction: _dbContext.Transaction);
            return StatusCode(200, res); 
        }
    }
}
