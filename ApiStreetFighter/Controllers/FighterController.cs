using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ApiStreetFighter.Controllers
{
    [ApiController]
    [Route("api/fighter")]
    public class FighterController : Controller
    {
        //inyecto los comandos
        private readonly IGetFightersCommand _getFightersCommand;
        private readonly IDeleteFighterCommand _deleteFighterCommand;
        private readonly IUpdateFighterCommand _updateFighterCommand;
        private readonly IInsertFighterCommand _insertFighterCommand;
        private readonly IGetFighterByIdCommand _getFighterByIdCommand;
        private readonly IValidator<CreateUpdateEntityDTO> _validator;

        private readonly IMapper _mapper;

        public FighterController(IMapper mapper , IGetFightersCommand getFightersCommand,
            IDeleteFighterCommand deleteFighterController, 
            IUpdateFighterCommand updateFighterCommand, 
            IInsertFighterCommand insertFighterCommand,
            IGetFighterByIdCommand getFighterByIdCommand,
            IValidator<CreateUpdateEntityDTO> validator)
        {
            _mapper = mapper;
            _getFightersCommand = getFightersCommand;
            _deleteFighterCommand = deleteFighterController;
            _updateFighterCommand = updateFighterCommand;
            _insertFighterCommand = insertFighterCommand;
            _getFighterByIdCommand = getFighterByIdCommand;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _getFightersCommand.Execute();
            if (_getFightersCommand.Success)
            {
                return Ok(_mapper.Map<List<EntityDTO>>(_getFightersCommand.Fighters));
            }
            else
            {
                return NotFound(_getFightersCommand.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            await _getFighterByIdCommand.Configure(id).Execute();

            if (_getFighterByIdCommand.Success)
            {
                return Ok(_mapper.Map<EntityDTO>(_getFighterByIdCommand.Fighter));
            }
            else
            {
                return NotFound(_getFighterByIdCommand.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            await _deleteFighterCommand.Configure(id).Execute();

            if (_deleteFighterCommand.Success)
            {
                return Ok();
            }
            else
            {
                return NotFound(_deleteFighterCommand.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutById(string id, [FromBody] CreateUpdateEntityDTO entityDTO)
        {
            var result = await _validator.ValidateAsync(entityDTO);

            if (!result.IsValid)
                return BadRequest(result.Errors);


            await _updateFighterCommand.Configure(id, _mapper.Map<Fighter>(entityDTO)).Execute();

            if (_updateFighterCommand.Success)
            {
                return Ok();
            }
            else
            {
                return NotFound(_updateFighterCommand.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUpdateEntityDTO entityDTO)
        {
            var result = await _validator.ValidateAsync(entityDTO);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            await _insertFighterCommand.Configure(_mapper.Map<Fighter>(entityDTO)).Execute();

            if (_insertFighterCommand.Success)
            {
                return Ok(_insertFighterCommand.NewFighterId);
            }
            else
            {
                return BadRequest(_insertFighterCommand.Message);
            }
        }
    }
}
