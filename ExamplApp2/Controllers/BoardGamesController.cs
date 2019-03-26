using System.Collections.Generic;
using System.Threading.Tasks;
using Games.Models;
using Games.Services;
using Microsoft.AspNetCore.Mvc;

namespace Games.Controllers
{
    [Route("api/[controller]")]
    public class BoardGamesController : Controller
    {
        private BoardGameService _boardGameService;

        public BoardGamesController(BoardGameService service)
        {
            _boardGameService = service;
            _boardGameService.Initialize();
        }

        // GET: api/<controller >
        [HttpGet]
        public Task<List<BoardGame>> GetBoardGamesAsync()
        {
            return _boardGameService.GetBoardGamesAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<BoardGame> GetBoardGame(int id)
        {
            return await _boardGameService.GetBoardGameAsync(id);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]BoardGame boardGame)
        {
            _boardGameService.AddBoardGame(boardGame);
        }

        //// PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
