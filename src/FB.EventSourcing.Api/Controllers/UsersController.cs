using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FB.EventSourcing.Application;
using FB.EventSourcing.Application.Contracts.Users.Commands;
using FB.EventSourcing.Application.Contracts.Users.Dtos.Request;
using FB.EventSourcing.Application.Contracts.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FB.EventSourcing.Api.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet, Route("{email}")]
        [ProducesResponseType(typeof(bool), (int) HttpStatusCode.OK)]
        public async Task<ApiResponse> Get(string email = "furkan@bozdag.dev")
        {
            var response = await _mediator.Send(new GetUserByEmailQuery(email));
            return ApiResponse(response);
        }

        [HttpPost, Route("create")]
        [ProducesResponseType(typeof(bool), (int) HttpStatusCode.OK)]
        public async Task<ApiResponse> Create([FromBody] CreateUserRequestDto request)
        {
            var command = _mapper.Map<CreateUserCommand>(request);
            await _mediator.Send(command);

            return ApiResponse();
        }

        [HttpPut, Route("change-email")]
        [ProducesResponseType(typeof(bool), (int) HttpStatusCode.OK)]
        public async Task<ApiResponse> ChangeEmail([FromBody] ChangeEmailRequestDto request)
        {
            var command = _mapper.Map<ChangeEmailCommand>(request);
            await _mediator.Send(command);

            return ApiResponse();
        }
    }
}