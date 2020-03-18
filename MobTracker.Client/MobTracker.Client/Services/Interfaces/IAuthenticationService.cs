using MobTracker.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobTracker.Client.Services.Interfaces
{
    public interface IAuthenticationService
    {

        Task<AuthenticationResult> Authenticate();
        bool IsAuthenticated { get; }
        AuthenticationResult AuthenticationResult { get; }
    }
}
