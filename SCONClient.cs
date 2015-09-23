using System.Collections.Generic;

using PokeD.Core.Interfaces;
using PokeD.Core.Packets;
using PokeD.Core.Packets.SCON;
using PokeD.Core.Packets.SCON.Authorization;
using PokeD.Core.Packets.SCON.Chat;
using PokeD.Core.Packets.SCON.Logs;
using PokeD.Core.Packets.SCON.Status;
using PokeD.Core.Wrappers;

using PokeD.SCON.Exceptions;
using PokeD.SCON.IO;

namespace PokeD.SCON
{
    public partial class SCONClient : IClient
    {
        string Password { get; set; }

        INetworkTCPClient Client { get; set; }
        IPacketStream Stream { get; set; }

#if DEBUG
        // -- Debug -- //
        List<ProtobufPacket> Received { get; } = new List<ProtobufPacket>();
        List<ProtobufPacket> Sended { get; } = new List<ProtobufPacket>();
        // -- Debug -- //
#endif

        public SCONClient()
        {
            Client = NetworkTCPClientWrapper.NewInstance();
            Stream = new ProtobufStream(Client);
        }

        public IClient Initialize(string password)
        {
            Password = password;

            return this;
        }


        public void Connect(string ip, ushort port)
        {
            if (Stream.Connected)
                Disconnect();

            Stream.Connect(ip, port);

            SendPacket(new AuthorizationRequestPacket());
        }
        public void Disconnect()
        {
            Stream.Disconnect();
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

        /// <summary>
        /// Data is handled here.
        /// </summary>
        /// <param name="id">Packet ID</param>
        /// <param name="data">Packet byte[] data</param>
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

        /// <summary>
        /// Packets are handled here.
        /// </summary>
        /// <param name="packet">Packet</param>
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
                    HandleAuthorizationComplete((AuthorizationCompletePacket)packet);
                    break;

                case SCONPacketTypes.AuthorizationDisconnect:
                    HandleAuthorizationDisconnect((AuthorizationDisconnectPacket)packet);
                    break;


                case SCONPacketTypes.PlayerListResponse:
                    HandlePlayerListResponse((PlayerListResponsePacket)packet);
                    break;


                case SCONPacketTypes.ChatMessage:
                    HandleChatMessage((ChatMessagePacket) packet);
                    break;


                case SCONPacketTypes.PlayerLocationResponse:
                    HandlePlayerLocationResponse((PlayerLocationResponsePacket) packet);
                    break;


                case SCONPacketTypes.LogListResponse:
                    HandleLogListResponse((LogListResponsePacket)packet);
                    break;

                case SCONPacketTypes.LogFileResponse:
                    HandleLogFileResponse((LogFileResponsePacket)packet);
                    break;


                case SCONPacketTypes.CrashLogListResponse:
                    HandleCrashLogListResponse((CrashLogListResponsePacket)packet);
                    break;

                case SCONPacketTypes.CrashLogFileResponse:
                    HandleCrashLogFileResponse((CrashLogFileResponsePacket)packet);
                    break;
            }
        }


        public void SendPacket(ProtobufPacket packet)
        {
            if (Stream.Connected)
            {
                Stream.SendPacket(ref packet);

#if DEBUG
                Sended.Add(packet);
#endif
            }
        }


        public void Authorize()
        {
            SendPacket(new AuthorizationPasswordPacket { Password = Password });
        }
        public void EnableEncryption()
        {
            SendPacket(new EncryptionRequestPacket());
        }
        public void SendPlayerListRequest()
        {
            SendPacket(new PlayerListRequestPacket());
        }
        public void ExecuteCommand(string command)
        {
            SendPacket(new ExecuteCommandPacket { Command = command });
        }

        public void q1()
        {
            SendPacket(new CrashLogListRequestPacket());
        }
        public void q2()
        {
            SendPacket(new LogListRequestPacket());
        }
        public void q3(string s)
        {
            SendPacket(new LogFileRequestPacket { LogFilename = s });
        }

        public void Dispose()
        {
            
        }
    }
}
