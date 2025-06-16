using Microsoft.AspNetCore.Identity;

namespace ControleDeCinema.Domínio;

public class Usuario : IdentityUser<int>
{
    public Usuario()
    {
        EmailConfirmed = true;
    }
}
