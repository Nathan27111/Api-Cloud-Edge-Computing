using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airhockey.Repositories;
using Airhockey.Models;
using Airhockey.DTO;
using Airhockey.Wrapper;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;

namespace Airhockey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerRepository _playerRepo;
        private readonly IMapper _mapper;

        public PlayersController(IPlayerRepository playerRepository, IMapper mapper)
        {
            _playerRepo = playerRepository;
            _mapper = mapper;
        }
        #region GET PLAYER BY ID
        [HttpGet("{id:int}", Name = "GetPlayer")]
        [ProducesResponseType(typeof(PlayerReadDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<Response<PlayerReadDTO>> GetPlayer(int id)
        {
            try
            {
                Player player = _playerRepo.GetPlayer(id);
                if (player == null)
                {
                    return NotFound(new Response<PlayerReadDTO>(null)
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status404NotFound}" },
                        Message = $"Player with id {id} is not found..."
                    });
                }

                return Ok(new Response<PlayerReadDTO>(_mapper.Map<PlayerReadDTO>(player)));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<PlayerReadDTO>(null)
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status500InternalServerError}" },
                        Message = $"({e.Message})"
                    });
            }
        }
        #endregion

        #region CREATE PLAYER
        [HttpPost()]
        [ProducesResponseType(typeof(PlayerReadDTO), 201)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<PlayerReadDTO> CreatePlayer([FromBody] PlayerWriteDTO newPlayer)
        {
            try
            {
                if (newPlayer != null)
                {
                    Player createdPlayer = _playerRepo.CreatePlayer(_mapper.Map<Player>(newPlayer), newPlayer.Code);
                    if (createdPlayer == null)
                    {
                        return NotFound(new Response<PlayerReadDTO>(null)
                        {
                            Succeeded = false,
                            Errors = new string[] { $"Status code: {StatusCodes.Status404NotFound}" },
                            Message = $"No tournament was found"
                        });
                    } else
                    {
                        return Created(Url.Link("GetPlayer",
                                    new { id = createdPlayer.Id }),
                                    new Response<PlayerReadDTO>(_mapper.Map<PlayerReadDTO>(createdPlayer)));
                    }
                }
                else
                {
                    return NotFound(new Response<PlayerReadDTO>(null)
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status404NotFound}" },
                        Message = $"No player data was received"
                    });
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<PlayerReadDTO>(null)
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status500InternalServerError}" },
                        Message = $"({e.Message})"
                    });
            }
        }
        #endregion

        #region GET PLAYERS BY TOURNAMENT ID
        [HttpGet("tournaments/{tournamentsId:int}")]
        [ProducesResponseType(typeof(PlayerReadDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<Response<IEnumerable<PlayerReadDTO>>> GetPlayersByTournamentId(int tournamentId)
        {
            try
            {
                IEnumerable<PlayerReadDTO> players = _playerRepo.GetPlayersByTournamentId(tournamentId).ProjectTo<PlayerReadDTO>(_mapper.ConfigurationProvider);
                if (players.Any())
                {
                    return Ok(new Response<IEnumerable<PlayerReadDTO>>(players.ToList()));
                }
                else
                {
                    return NotFound(new Response<PlayerReadDTO>(null)
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status404NotFound}" },
                        Message = $"No players found in this tournament"
                    });
                }

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<PlayerReadDTO>(null)
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status500InternalServerError}" },
                        Message = $"({e.Message})"
                    });
            }
        }
        #endregion

        #region UPDATE PLAYER RANKING
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(PlayerReadDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<Response<PlayerReadDTO>> UpdatePlayer(int id, [FromBody] PlayerUpdateDTO updatedPlayer)
        {
            try
            {
                if (id == updatedPlayer.Id)
                {
                    if (_playerRepo.GetPlayer(id) != null)
                    {
                        return Ok(new Response<PlayerReadDTO>(_mapper.Map<PlayerReadDTO>(_playerRepo.UpdatePlayer(_mapper.Map<Player>(updatedPlayer)))));
                    }
                    else
                    {
                        return NotFound(new Response<PlayerReadDTO>(null)
                        {
                            Succeeded = false,
                            Errors = new string[] { $"Status code: {StatusCodes.Status404NotFound}" },
                            Message = $"Player with id {id} is not found..."
                        });
                    }
                }
                else
                {
                    return BadRequest(new Response<PlayerReadDTO>(null)
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status400BadRequest}" },
                        Message = $"Player Id did not match"
                    });
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<PlayerReadDTO>(null)
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
