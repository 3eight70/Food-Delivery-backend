﻿namespace webNET_Hits_backend_aspnet_project_1.Services;

public class TokenService: ITokenService
{
    public string? GetToken(string request)
    {
        return request.Substring("Bearer ".Length);
    }
}