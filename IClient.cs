using System;
using PokeD.Core.Data;
using PokeD.Core.Interfaces;

namespace PokeD.SCON
{
    public interface IClient : IUpdatable, IDisposable
    {
        void Connect(string ip, ushort port);
        void Disconnect();

        IClient Initialize(PasswordStorage password);

        void Authorize();
        void EnableEncryption();
        void SendPlayerListRequest();
        void ExecuteCommand(string command);

        void q1();
        void q2();
        void q3(string s);
    }
}
