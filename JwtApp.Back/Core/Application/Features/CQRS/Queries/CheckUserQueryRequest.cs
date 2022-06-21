﻿using JwtApp.Back.Core.Application.Dto;
using MediatR;

namespace JwtApp.Back.Core.Application.Features.CQRS.Queries
{
    public class CheckUserQueryRequest:IRequest<CheckUserResponseDto>
    {

        public string Username { get; set; }=string.Empty;
        public string Password { get; set; }=string.Empty;

    }
}
