using FarmingDatabase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Services
{
    public interface IAuthenticationService
    {
        Boolean SignUp(User user);
    }
}
