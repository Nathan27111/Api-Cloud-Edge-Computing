using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Airhockey.Models;
using Airhockey.Repositories;
using Airhockey.DTO.Tournaments;
using Airhockey.Wrapper;

namespace Airhockey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class TournamentsController: ControllerBase
    {
        private readonly ITournamentRepository _tournamentRepository;
        private readonly IMapper _mapper;

        public TournamentsController(ITournamentRepository tournamentRepository, IMapper mapper)
        {
            _tournamentRepository = tournamentRepository;
            _mapper = mapper;
        }

        #region GET TOURNAMENT BY CODE
        [HttpGet("{code}", Name = "GetTournament")]
        [ProducesResponseType(typeof(TournamentReadDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<Response<TournamentReadDTO>> GetTournament(string code)
        {
            try
            {
                Tournaments tournaments = _tournamentRepository.GetTournament(code);
                if(tournaments == null)
                {
                    return NotFound(new Response<TournamentReadDTO>(null)
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status404NotFound}" },
                        Message = $"No tournament was found with code {code}."
                    });
                }
                return Ok(new Response<TournamentReadDTO>(_mapper.Map<TournamentReadDTO>(tournaments)));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<TournamentReadDTO>(null)
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status500InternalServerError}" },
                        Message = $"({e.Message})"
                    });
            }
        }
        #endregion

        #region ADD TOURNAMENT
        [HttpPost()]
        [ProducesResponseType(typeof(TournamentReadDTO), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<TournamentReadDTO> CreateTournament([FromBody] TournamentWriteDTO newTournament)
        {
            try
            {
                if(newTournament != null)
                {
                    Tournaments createdTournament = _tournamentRepository.CreateTournament(_mapper.Map<Tournaments>(newTournament));
                    if(createdTournament == null)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<TournamentReadDTO>(null)
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status500InternalServerError}" },
                        Message = $"Something went wrong while creating the tournament."
                    });
                    }
                    else
                    {
                        return Created(Url.Link("GetTournament", new { code = createdTournament.Code }),
                            new Response<TournamentReadDTO>(_mapper.Map<TournamentReadDTO>(createdTournament)));
                    }
                }
                else
                {
                    return BadRequest(new Response<TournamentReadDTO>(null)
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status400BadRequest}" },
                        Message = $"No tournament data was received"
                    });
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<TournamentReadDTO>(null)
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status500InternalServerError}" },
                        Message = $"({e.Message})"
                    });
            }
        }
        #endregion

        #region UPDATE TOURNAMENT ACTIVENESS
        [HttpPut("{code}")]
        [ProducesResponseType(typeof(TournamentReadDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<Response<TournamentReadDTO>> ChangeActiveness(string code)
        {
            try
            {


                if (code != null)
                {
                    Tournaments tournament = _tournamentRepository.UpdateActive(code);
                    if (tournament != null)
                    {
                        return Ok(new Response<TournamentReadDTO>(_mapper.Map<TournamentReadDTO>(tournament)));
                    }
                    else
                    {
                        return NotFound(new Response<TournamentReadDTO>(null)
                        {
                            Succeeded = false,
                            Errors = new string[] { $"Status code: {StatusCodes.Status404NotFound}" },
                            Message = $"No tournament was found with this code"
                        });
                    }
                }
                else
                {
                    return BadRequest(new Response<TournamentReadDTO>(null)
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status400BadRequest}" },
                        Message = $"No code was given"
                    });
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<TournamentReadDTO>(null)
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