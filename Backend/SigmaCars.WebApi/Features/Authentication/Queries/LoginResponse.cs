﻿namespace SigmaCars.WebApi.Features.Authentication.Queries;

public record LoginResponse(
    string FirstName,
    string LastName,
    string Email,
    string Jwt);