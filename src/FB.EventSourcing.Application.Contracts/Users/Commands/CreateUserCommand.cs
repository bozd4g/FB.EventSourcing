using System;
using AutoMapper;
using FB.EventSourcing.Application.Contracts.Users.Dtos.Request;
using MediatR;

namespace FB.EventSourcing.Application.Contracts.Users.Commands
{
    [AutoMap(typeof(CreateUserRequestDto))]
    public class CreateUserCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
    }
}