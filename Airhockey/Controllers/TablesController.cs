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
    public class TablesController : ControllerBase
    {
        private readonly ITableRepository _tableRepo;
        private readonly IMapper _mapper;

        public TablesController(ITableRepository tableRepository, IMapper mapper)
        {
            _tableRepo = tableRepository;
            _mapper = mapper;
        }

        #region CREATE TABLE
        [HttpPost()]
        [ProducesResponseType(typeof(TableReadDTO), 201)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<TableReadDTO> CreateTable([FromBody] TableWriteDTO newTable)
        {
            try
            {
                if (newTable != null)
                {
                    AirhockeyTable createdTable = _tableRepo.CreateTable(_mapper.Map<AirhockeyTable>(newTable));
                    return Created(Url.Link("GetTable",
                                new { id = createdTable.Id }),
                                new Response<TableReadDTO>(_mapper.Map<TableReadDTO>(createdTable)));
                }
                else
                {
                    return NotFound(new Response<TableReadDTO>(null)
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status404NotFound}" },
                        Message = $"No table data was recieved"
                    });
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<TableReadDTO>(null)
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status500InternalServerError}" },
                        Message = $"({e.Message})"
                    });
            }
        }
        #endregion

        #region GET TABLES BY TOURNAMENT ID
        [HttpGet("tournaments/{tournamentsId:int}")]
        [ProducesResponseType(typeof(TableReadDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<Response<IEnumerable<TableReadDTO>>> GetTablesByTournamentId(int tournamentId)
        {
            try
            {
                IEnumerable<TableReadDTO> tables = _tableRepo.GetTablesByTournamentId(tournamentId).ProjectTo<TableReadDTO>(_mapper.ConfigurationProvider);
                if (tables.Any())
                {
                    return Ok(new Response<IEnumerable<TableReadDTO>>(tables.ToList()));
                }
                else
                {
                    return NotFound(new Response<TableReadDTO>(null)
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status404NotFound}" },
                        Message = $"No tables found in this tournament"
                    });
                }

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<TableReadDTO>(null)
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
