using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Airhockey.Repositories;
using Airhockey.Models;
using Airhockey.DTO;
using Airhockey.Wrapper;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;

namespace Airhockey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class GamesController : ControllerBase
    {
        
        private readonly IGameRepository _gameRepo;
        private readonly IMapper _mapper;

        public GamesController(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepo = gameRepository;
            _mapper = mapper;
        }

        #region GET Game BY ID
        [HttpGet("{id:int}", Name = "GetGame")]
        [ProducesResponseType(typeof(GameReadDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<Response<GameReadDTO>> GetGame(int id)
        {
            try
            {
                Games Game = _gameRepo.GetGame(id);
                if (Game == null)
                {
                    return NotFound(new Response<GameReadDTO>(null)
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status404NotFound}" },
                        Message = $"Game with id {id} is not found..."
                    });
                }

                return Ok(new Response<GameReadDTO>(_mapper.Map<GameReadDTO>(Game)));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<GameReadDTO>(null)
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status500InternalServerError}" },
                        Message = $"({e.Message})"
                    });
            }
        }
        #endregion

        #region GET GAMES BY TOURNAMENT ID
        [HttpGet("tournaments/{tournamentsId:int}")]
        [ProducesResponseType(typeof(GameReadDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<Response<IEnumerable<GameReadDTO>>> GetGamesByTournamentId(int tournamentId)
        {
            try
            {
                IEnumerable<GameReadDTO> Games = _gameRepo.GetGamesByTournamentId(tournamentId).ProjectTo<GameReadDTO>(_mapper.ConfigurationProvider);
                if (Games.Any())
                {
                    return Ok(new Response<IEnumerable<GameReadDTO>>(Games.ToList()));
                }
                else
                {
                    return NotFound(new Response<GameReadDTO>(null)
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status404NotFound}" },
                        Message = $"No games found in this tournament"
                    });
                }

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<GameReadDTO>(null)
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status500InternalServerError}" },
                        Message = $"({e.Message})"
                    });
            }
        }
        #endregion

        #region UPDATE GAME
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(GameReadDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<Response<GameReadDTO>> UpdateGame(int id, [FromBody] GameUpdateDTO updatedGame)
        {
            try
            {
                if (id == updatedGame.Id)
                {
                    if (_gameRepo.GetGame(id) != null)
                    {
                        return Ok(new Response<GameReadDTO>(_mapper.Map<GameReadDTO>(_gameRepo.UpdateGame(_mapper.Map<Games>(updatedGame)))));
                    }
                    else
                    {
                        return NotFound(new Response<GameReadDTO>(null)
                        {
                            Succeeded = false,
                            Errors = new string[] { $"Status code: {StatusCodes.Status404NotFound}" },
                            Message = $"Game with id {id} is not found..."
                        });
                    }
                }
                else
                {
                    return BadRequest(new Response<GameReadDTO>(null)
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status400BadRequest}" },
                        Message = $"Game Id did not match"
                    });
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<GameReadDTO>(null)
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status500InternalServerError}" },
                        Message = $"({e.Message})"
                    });
            }
        }
        #endregion
    }
}
