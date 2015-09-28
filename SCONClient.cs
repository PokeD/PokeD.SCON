using System;
using System.Collections.Generic;

using PokeD.Core.Data;
using PokeD.Core.Interfaces;
using PokeD.Core.Packets;

using PokeD.Core.Packets.SCON.Authorization;
using PokeD.Core.Packets.SCON.Chat;
using PokeD.Core.Packets.SCON.Logs;
using PokeD.Core.Packets.SCON.Status;
using PokeD.Core.Wrappers;

using PokeD.SCON.IO;
using PokeD.SCON.UILibrary;

namespace PokeD.SCON
{
    public partial class SCONClient : IClient
    {
        BasicUIViewModel BasicUIVM { get; set; }

        PasswordStorage Password { get; set; }

        INetworkTCPClient Client { get; set; }
        IPacketStream Stream { get; set; }

#if DEBUG
        // -- Debug -- //
        List<ProtobufPacket> Received { get; } = new List<ProtobufPacket>();
        List<ProtobufPacket> Sended { get; } = new List<ProtobufPacket>();
        // -- Debug -- //
#endif

        public SCONClient(BasicUIViewModel basicUIvm)
        {
            BasicUIVM = basicUIvm;
            BasicUIVM.OnConnect += BasicUIViewModel_OnConnect;
            BasicUIVM.OnDisconnect += BasicUIViewModel_OnDisconnect;

            BasicUIVM.TabChanged += BasicUIViewModel_TabChanged;

            BasicUIVM.OnGetLog += BasicUIViewModel_GetLog;
            BasicUIVM.OnGetCrashLog += BasicUIViewModel_GetCrashLog;

            Client = NetworkTCPClientWrapper.NewInstance();
            Stream = new ProtobufStream(Client);
        }
        private bool BasicUIViewModel_OnConnect(string ip, ushort port, string password, bool autoReconnect)
        {
            try
            {
                Password = new PasswordStorage(password);
                if (Stream.Connected)
                    Disconnect();
                
                Stream.Connect(ip, port);

                SendPacket(new AuthorizationRequestPacket());

                return true;
            }
            catch (Exception) { return false; }
        }
        private bool BasicUIViewModel_OnDisconnect()
        {
            try
            {
                if (Stream.Connected)
                    Disconnect();
                
                return true;
            }
            catch (Exception) { return false; }
        }
        private void BasicUIViewModel_TabChanged(string tabName)
        {
            switch (tabName)
            {
                case "Players":
                    break;

                case "Bans":
                    break;

                case "Player Database":
                    break;

                case "Logs":
                    SendPacket(new LogListRequestPacket());
                    break;

                case "Crash Logs":
                    SendPacket(new CrashLogListRequestPacket());
                    break;

                case "Settings":
                    break;
            }
        }
        private void BasicUIViewModel_GetLog(int index)
        {
            SendPacket(new LogFileRequestPacket { LogFilename = BasicUIVM.CrashLogsGridDataList[index].LogFilename });
        }
        private void BasicUIViewModel_GetCrashLog(int index)
        {
            SendPacket(new CrashLogFileRequestPacket { CrashLogFilename = BasicUIVM.CrashLogsGridDataList[index].LogFilename });
        }


        public IClient Initialize(PasswordStorage password)
        {
            Password = password;

            return this;
        }


        public void Update()
        {
            if (!Stream.Connected)
            {
                Dispose();
                return;
            }

            if (Stream.Connected && Stream.DataAvailable > 0)
            {
                //try
                //{
                    var dataLength = Stream.ReadVarInt();
                    if (dataLength == 0)
                    {
                        Logger.Log(LogType.GlobalError, $"Protobuf Reading Error: Packet Length size is 0. Disconnecting.");
                        SendPacket(new AuthorizationDisconnectPacket { Reason = "Packet Length size is 0!" });
                        Dispose();
                        return;
                    }

                    var data = Stream.ReadByteArray(dataLength);

                    HandleData(data);
                //}
                //catch (ProtobufReadingException ex) { Logger.Log(LogType.GlobalError, $"Protobuf Reading Exeption: {ex.Message}. Disconnecting IClient {Name}."); }
            }
        }

        private void HandleData(byte[] data)
        {
            if (data == null)
            {
                Logger.Log(LogType.GlobalError, $"SCON Reading Error: Packet Data is null.");
                return;
            }

            using (var reader = new ProtobufDataReader(data))
            {
                var id = reader.ReadVarInt();
                var origin = reader.ReadVarInt();

                if (id >= SCONResponse.Packets.Length)
                {
                    Logger.Log(LogType.GlobalError, $"SCON Reading Error: Packet ID {id} is not correct, Packet Data: {data}. Disconnecting.");
                    SendPacket(new AuthorizationDisconnectPacket { Reason = $"Packet ID {id} is not correct!" });
                    Dispose();
                    return;
                }

                var packet = SCONResponse.Packets[id]().ReadPacket(reader);
                packet.Origin = origin;

                HandlePacket(packet);

#if DEBUG
                Received.Add(packet);
#endif
            }
        }
        private void HandlePacket(ProtobufPacket packet)
        {
            switch ((SCONPacketTypes) packet.ID)
            {
                case SCONPacketTypes.AuthorizationResponse:
                    HandleAuthorizationResponse((AuthorizationResponsePacket) packet);
                    break;


                case SCONPacketTypes.EncryptionRequest:
                    HandleEncryptionRequest((EncryptionRequestPacket) packet);
                    break;


                case SCONPacketTypes.AuthorizationComplete:
                    HandleAuthorizationComplete((AuthorizationCompletePacket) packet);
                    break;

                case SCONPacketTypes.AuthorizationDisconnect:
                    HandleAuthorizationDisconnect((AuthorizationDisconnectPacket) packet);
                    break;


                case SCONPacketTypes.ChatMessage:
                    HandleChatMessage((ChatMessagePacket) packet);
                    break;


                case SCONPacketTypes.PlayerInfoListResponse:
                    HandlePlayerInfoListResponse((PlayerInfoListResponsePacket) packet);
                    break;


                case SCONPacketTypes.LogListResponse:
                    HandleLogListResponse((LogListResponsePacket) packet);
                    break;

                case SCONPacketTypes.LogFileResponse:
                    HandleLogFileResponse((LogFileResponsePacket) packet);
                    break;


                case SCONPacketTypes.CrashLogListResponse:
                    HandleCrashLogListResponse((CrashLogListResponsePacket) packet);
                    break;

                case SCONPacketTypes.CrashLogFileResponse:
                    HandleCrashLogFileResponse((CrashLogFileResponsePacket) packet);
                    break;


                case SCONPacketTypes.PlayerDatabaseListResponse:
                    HandlePlayerDatabaseListResponse((PlayerDatabaseListResponsePacket) packet);
                    break;
            }
        }


        private void SendPacket(ProtobufPacket packet)
        {
            if (Stream.Connected)
            {
                Stream.SendPacket(ref packet);

#if DEBUG
                Sended.Add(packet);
#endif
            }
        }


        private void DisplayMessage(string message)
        {
            BasicUIVM.DisplayMessage(message);
        }


        private void Disconnect()
        {
            Stream.Disconnect();
        }


        public void Dispose()
        {
            
        }
    }
}
