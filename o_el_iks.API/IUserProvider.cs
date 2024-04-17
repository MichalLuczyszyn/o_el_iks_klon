﻿using o_el_iks.API.Entities;

namespace o_el_iks.API;

public interface IUserProvider
{
    void Register(RegistrationData data, IAuctionsProvider auctionsProvider);
    void SignIn(SignInData data, ITokenProvider tokenProvider, HttpContext httpContext);
    List<User> GetUsers();
}