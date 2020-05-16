using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KulpAssessment.Managers;
using Dto = KulpAssessment.Data.Dto;

namespace KulpAssessment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonRepository _personManager;
        
        public PersonController(ILogger<PersonController> logger, IPersonRepository personManager)
        {
            this._logger = logger;
            this._personManager = personManager;
        }

        [HttpGet]
        public async Task<IEnumerable<Dto.PersonDto>> Get(string q)
        {
            // TODO: Should install and configure NLog or other
            this._logger.Log(LogLevel.Trace, $"GET|{q}");

            // TODO: Should send events to receiver with stats or similar

            // HACK: For the purposes of this exercise, introduce errors or delays
            if( q?.Contains("slow") == true)
            {
                // Never use Thread.Sleep when we can yield to another request...
                await Task.Delay(2000);
            }

            if( q?.Contains("err") == true)
            {
                throw new Exception("This has gone poorly");
            }

            // No need to use async since it's a single query and iteration will
            // occur as part of streaming back the response.  Why materialize the
            // whole result set if connection could end up disconnected anyway?
            return _personManager.FindPeopleByName(q).Select(Dto.PersonDto.FromEntity);
        }
    }
}
