using System;
using System.Collections.Generic;

using Aragas.Network.IO;
using Aragas.Network.Data;
using Aragas.Network.Packets;

using PCLExt.Network;

using PokeD.Core;
using PokeD.Core.Packets.SCON;
using PokeD.Core.Packets.SCON.Authorization;
using PokeD.Core.Packets.SCON.Chat;
using PokeD.Core.Packets.SCON.Logs;
using PokeD.Core.Packets.SCON.Status;
using PokeD.SCON.Extensions;
using PokeD.SCON.UILibrary;

namespace PokeD.SCON
{
    public partial class SCONClient : IUpdatable, IDisposable
    {
        BasicUIViewModel BasicUIVM { get; }


        ISocketClient Client { get; }
        ProtobufStream Stream { get; }


        PasswordStorage Password { get; set; }
        

#if DEBUG
        // -- Debug -- //
        List<ProtobufPacket> Received { get; } = new List<ProtobufPacket>();
        List<ProtobufPacket> Sended { get; } = new List<ProtobufPacket>();
        public object FileStorageExtension { get; private set; }

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

            BasicUIVM.OnChatStateChanged += BasicUIViewModel_OnChatStateChanged;

            Client = SocketClient.CreateTCP();
            Stream = new ProtobufStream(Client);
        }
        private bool BasicUIViewModel_OnConnect(string ip, ushort port, string password, bool autoReconnect)
        {
            try
            {
                Password = new PasswordStorage(password);
                if (Stream.IsConnected)
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
                if (Stream.IsConnected)
                    Disconnect();
                
                return true;
            }
            catch (Exception) { return false; }
        }
        private void BasicUIViewModel_TabChanged(string tabName)
        {
            if(!Stream.IsConnected)
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
            if (!Stream.IsConnected)
                DisplayMessage("Not connected to server!");
            else
                SendPacket(new LogFileRequestPacket { LogFilename = BasicUIVM.LogsGridDataList[index].LogFilename });
        }
        private void BasicUIViewModel_GetCrashLog(int index)
        {
            if (!Stream.IsConnected)
                DisplayMessage("Not connected to server!");
            else
                SendPacket(new CrashLogFileRequestPacket { CrashLogFilename = BasicUIVM.CrashLogsGridDataList[index].LogFilename });
        }
        private void BasicUIViewModel_SaveLog(string logname, string log)
        {
            FileStorageExtensions.SaveLog(logname, log);
        }

        private void BasicUIViewModel_OnChatStateChanged(bool value)
        {
            if (value)
                SendPacket(new StartChatReceivingPacket());
            else
                SendPacket(new StopChatReceivingPacket());
        }


        public void Update()
        {
            if (Stream.IsConnected && Stream.DataAvailable > 0)
            {
                var dataLength = Stream.ReadVarInt();
                if (dataLength > 0)
                {
                    var data = Stream.Receive(dataLength);

                    HandleData(data);
                }
                else
                {
                    Logger.Log(LogType.Error, $"Protobuf Reading Error: Packet Length size is 0. Disconnecting.");
                    SendPacket(new AuthorizationDisconnectPacket { Reason = "Packet Length size is 0!" });
                    Dispose();
                }
            }
        }

        private void HandleData(byte[] data)
        {
            if (data != null)
            {
                using (var reader = new ProtobufDataReader(data))
                {
                    var id = reader.Read<VarInt>();

                    Func<SCONPacket> func;
                    if (SCONPacketResponses.TryGetPacketFunc(id, out func))
                    {
                        if (func != null)
                        {
                            var packet = func().ReadPacket(reader);
                            if (packet != null)
                            {
                                HandlePacket(packet);

#if DEBUG
                                Received.Add(packet);
#endif
                            }
                            else
                            {
                                Logger.Log(LogType.Error, $"SCON Reading Error: packet is null. Packet ID {id}");
                                SendPacket(new AuthorizationDisconnectPacket { Reason = $"Packet ID {id} is not correct!" });
                                Dispose();
                            }
                        }
                        else
                        {
                            Logger.Log(LogType.Error, $"SCON Reading Error: Packet ID {id} is not correct, Packet Data: {data}. Disconnecting.");
                            SendPacket(new AuthorizationDisconnectPacket {Reason = $"Packet ID {id} is not correct!"});
                            Dispose();
                        }
                    }
                    else
                    {
                        Logger.Log(LogType.Error, $"SCON Reading Error: Packet ID {id} is not correct, Packet Data: {data}. Disconnecting.");
                        SendPacket(new AuthorizationDisconnectPacket {Reason = $"Packet ID {id} is not correct!"});
                        Dispose();
                    }
                }
            }
            else
                Logger.Log(LogType.Error, $"SCON Reading Error: Packet Data is null.");
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


        private void SendPacket(SCONPacket packet)
        {
            if (Stream.IsConnected)
            {
                Stream.SendPacket(packet);

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
            BasicUIVM.Dispose();
        }
    }
}
