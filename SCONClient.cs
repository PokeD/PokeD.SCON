using System;
using System.Collections.Generic;

using Aragas.Core.Data;
using Aragas.Core.Interfaces;
using Aragas.Core.IO;
using Aragas.Core.Packets;
using Aragas.Core.Wrappers;
using PokeD.Core.Packets;

using PokeD.Core.Packets.SCON.Authorization;
using PokeD.Core.Packets.SCON.Chat;
using PokeD.Core.Packets.SCON.Logs;
using PokeD.Core.Packets.SCON.Status;

using PokeD.SCON.UILibrary;

namespace PokeD.SCON
{
    public partial class SCONClient : IUpdatable, IDisposable
    {
        BasicUIViewModel BasicUIVM { get; }


        ITCPClient Client { get; }
        IPacketStream Stream { get; }


        PasswordStorage Password { get; set; }
        

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

            BasicUIVM.OnSaveLog += BasicUIViewModel_SaveLog;

            Client = TCPClientWrapper.CreateTCPClient();
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
            if(!Stream.Connected)
                return;

            switch (tabName)
            {
                case "Players":
                    SendPacket(new PlayerInfoListRequestPacket());
                    break;

                case "Bans":
                    SendPacket(new BanListRequestPacket());
                    break;

                case "Player Database":
                    SendPacket(new PlayerDatabaseListRequestPacket());
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
            if (!Stream.Connected)
                DisplayMessage("Not connected to server!");
            else
                SendPacket(new LogFileRequestPacket { LogFilename = BasicUIVM.LogsGridDataList[index].LogFilename });
        }
        private void BasicUIViewModel_GetCrashLog(int index)
        {
            if (!Stream.Connected)
                DisplayMessage("Not connected to server!");
            else
                SendPacket(new CrashLogFileRequestPacket { CrashLogFilename = BasicUIVM.CrashLogsGridDataList[index].LogFilename });
        }
        private void BasicUIViewModel_SaveLog(string logname, string log)
        {
            FileSystemWrapper.SaveLog(logname, log);
        }


        public void Update()
        {
            if (Stream.Connected)
            {
                if (Stream.Connected && Stream.DataAvailable > 0)
                {
                    var dataLength = Stream.ReadVarInt();
                    if (dataLength > 0)
                    {
                        var data = Stream.ReadByteArray(dataLength);

                        HandleData(data);
                    }
                    else
                    {
                        Logger.Log(LogType.GlobalError, $"Protobuf Reading Error: Packet Length size is 0. Disconnecting.");
                        SendPacket(new AuthorizationDisconnectPacket {Reason = "Packet Length size is 0!"});
                        Dispose();
                    }
                }
            }
            else
                Dispose();
        }

        private void HandleData(byte[] data)
        {
            if (data != null)
            {
                using (var reader = new ProtobufDataReader(data))
                {
                    var id = reader.Read<VarInt>();
                    var origin = reader.Read<VarInt>();

                    if (SCONPacketResponses.Packets.Length > id)
                    {
                        if (SCONPacketResponses.Packets[id] != null)
                        {
                            var packet = SCONPacketResponses.Packets[id]().ReadPacket(reader);
                            packet.Origin = origin;

                            HandlePacket(packet);

#if DEBUG
                            Received.Add(packet);
#endif
                        }
                        else
                        {
                            Logger.Log(LogType.GlobalError, $"SCON Reading Error: Packet ID {id} is not correct, Packet Data: {data}. Disconnecting.");
                            SendPacket(new AuthorizationDisconnectPacket {Reason = $"Packet ID {id} is not correct!"});
                            Dispose();
                        }
                    }
                    else
                    {
                        Logger.Log(LogType.GlobalError, $"SCON Reading Error: Packet ID {id} is not correct, Packet Data: {data}. Disconnecting.");
                        SendPacket(new AuthorizationDisconnectPacket {Reason = $"Packet ID {id} is not correct!"});
                        Dispose();
                    }
                }
            }
            else
                Logger.Log(LogType.GlobalError, $"SCON Reading Error: Packet Data is null.");
        }
        private void HandlePacket(ProtobufPacket packet)
        {
            switch ((SCONPacketTypes)(int) packet.ID)
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


                case SCONPacketTypes.BanListResponse:
                    HandleBanListResponse((BanListResponsePacket) packet);
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
