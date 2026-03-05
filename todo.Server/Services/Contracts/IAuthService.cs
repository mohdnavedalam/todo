using todo.Server.Models.Auth;

namespace todo.Server.Services.Contracts
{
    public interface IAuthService
    {
        Task<AuthResponse?> SignUp(SignUpRequest request);
        Task<AuthResponse?> SignIn(SignInRequest request);
    }
}
