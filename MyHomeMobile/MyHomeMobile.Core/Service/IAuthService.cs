using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyHomeMobile.Core.Model;

namespace MyHomeMobile.Core.Service
{
    public interface IAuthService
    {
        bool IsAuthenticated { get; }

        Account CurentUser { get; }

        Task<bool> LoginAsync(string user, string password);
    }
}
