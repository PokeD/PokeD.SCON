﻿using System;

using PokeD.Core.Interfaces;

namespace PokeD.SCON
{
    public interface IClient : IUpdatable, IDisposable
    {
        void Connect(string ip, ushort port);
        void Disconnect();

        IClient Initialize(string password);

        void Authorize();
        void EnableEncryption();
        void SendPlayerListRequest();
        void ExecuteCommand(string command);

    }
}
