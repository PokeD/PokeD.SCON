using System;

using PokeD.Core.Data;
using PokeD.Core.Interfaces;

namespace PokeD.SCON
{
    public interface IClient : IUpdatable, IDisposable
    {
        IClient Initialize(PasswordStorage password);
    }
}
