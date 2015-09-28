using System.Collections.Generic;
using System.Collections.ObjectModel;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

using PokeD.Core;
using PokeD.Core.Packets.SCON.Authorization;
using PokeD.Core.Packets.SCON.Chat;
using PokeD.Core.Packets.SCON.Logs;
using PokeD.Core.Packets.SCON.Status;

using PokeD.SCON.Exceptions;
using PokeD.SCON.UILibrary;

namespace PokeD.SCON
{
    public partial class SCONClient
    {
        AuthorizationStatus AuthorizationStatus { get; set; }
        bool Authorized { get; set; }

        private void HandleAuthorizationResponse(AuthorizationResponsePacket packet)
        {
            if (Authorized)
                return;

            AuthorizationStatus = packet.AuthorizationStatus;
        }

        private void HandleEncryptionRequest(EncryptionRequestPacket packet)
        {
            if(Authorized)
                return;

            if (AuthorizationStatus.HasFlag(AuthorizationStatus.EncryprionEnabled))
            {
                var generator = new CipherKeyGenerator();
                generator.Init(new KeyGenerationParameters(new SecureRandom(), 16 * 8));
                var sharedKey = generator.GenerateKey();

                var pkcs = new PKCS1Signer(packet.PublicKey);
                var signedSecret = pkcs.SignData(sharedKey);
                var signedVerify = pkcs.SignData(packet.VerificationToken);

                SendPacket(new EncryptionResponsePacket { SharedSecret = signedSecret, VerificationToken = signedVerify });

                Stream.InitializeEncryption(sharedKey);
            }
            else
                throw new SCONException("Encryption was not enabled!");
        }

        private void HandleAuthorizationComplete(AuthorizationCompletePacket packet)
        {
            Authorized = true;
        }

        private void HandleAuthorizationDisconnect(AuthorizationDisconnectPacket packet)
        {
            Authorized = false;
        }

        private void HandlePlayerInfoListResponse(PlayerInfoListResponsePacket packet)
        {
            if (Authorized)
            {
                BasicUIVM.PlayersGridDataList = new ObservableCollection<PlayersDataGridModel>();

                for (var i = 0; i < packet.PlayerInfoList.Length; i++)
                {
                    var player = packet.PlayerInfoList[i];
                    var model = new PlayersDataGridModel
                    {
                        Number = i,
                        Name = player.Name,
                        GameJoltID = player.GameJoltID,
                        IP = player.IP,
                        Ping = player.Ping,
                        Online = true
                    };
                    BasicUIVM.PlayersGridDataList.Add(model);
                }

            }
            else
                DisplayMessage("You are not authorized.");
        }

        private void HandleChatMessage(ChatMessagePacket packet)
        {
            if (Authorized)
            {

            }
            else
                DisplayMessage("You are not authorized.");
        }

        private void HandleLogListResponse(LogListResponsePacket packet)
        {
            if (Authorized)
            {
                BasicUIVM.LogsGridDataList = new ObservableCollection<LogsDataGridModel>();

                for (var i = 0; i < packet.LogList.Length; i++)
                {
                    var log = packet.LogList[i];
                    var model = new LogsDataGridModel
                    {
                        Number = i,
                        LogFilename = log.LogFileName
                    };
                    BasicUIVM.LogsGridDataList.Add(model);
                }
            }
            else
                DisplayMessage("You are not authorized.");
        }

        private void HandleLogFileResponse(LogFileResponsePacket packet)
        {
            if (Authorized)
            {
                BasicUIVM.DisplayLog(packet.LogFile);
            }
            else
                DisplayMessage("You are not authorized.");
        }

        private void HandleCrashLogListResponse(CrashLogListResponsePacket packet)
        {
            if (Authorized)
            {
                BasicUIVM.CrashLogsGridDataList = new ObservableCollection<LogsDataGridModel>();

                for (var i = 0; i < packet.CrashLogList.Length; i++)
                {
                    var log = packet.CrashLogList[i];
                    var model = new LogsDataGridModel
                    {
                        Number = i,
                        LogFilename = log.LogFileName
                    };
                    BasicUIVM.CrashLogsGridDataList.Add(model);
                }
            }
            else
                DisplayMessage("You are not authorized.");
        }

        private void HandleCrashLogFileResponse(CrashLogFileResponsePacket packet)
        {
            if (Authorized)
            {
                BasicUIVM.DisplayLog(packet.CrashLogFile);
            }
            else
                DisplayMessage("You are not authorized.");
        }

        private void HandlePlayerDatabaseListResponse(PlayerDatabaseListResponsePacket packet)
        {
            if (Authorized)
            {
                
            }
            else
                DisplayMessage("You are not authorized.");
        }
    }
}
