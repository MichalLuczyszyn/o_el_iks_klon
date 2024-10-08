﻿using o_el_iks.API.Entities;

namespace o_el_iks.API.Interfaces;

public interface IUserProvider
{
    void Register(RegistrationData data);
    void SignIn(SignInData data, ITokenProvider tokenProvider, HttpContext httpContext);
    List<User> GetUsers();
}