using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Aragas.Network;

using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

using PokeD.Core.Data.SCON;
using PokeD.Core.Packets.SCON.Authorization;
using PokeD.Core.Packets.SCON.Chat;
using PokeD.Core.Packets.SCON.Logs;
using PokeD.Core.Packets.SCON.Status;
using PokeD.SCON.Exceptions;
using PokeD.SCON.UILibrary.GridModels;

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

            if(!AuthorizationStatus.HasFlag(AuthorizationStatus.EncryprionEnabled))
                SendPacket(new AuthorizationPasswordPacket { PasswordHash = Password.Hash });
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

                SendPacket(new AuthorizationPasswordPacket { PasswordHash = Password.Hash });
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
                BasicUIVM.PlayersGridDataList.Clear();
                for (var i = 0; i < packet.PlayerInfos.Length; i++)
                {
                    BasicUIVM.PlayersGridDataList.Add(new PlayersDataGridModel
                    {
                        Number = i,
                        Name = packet.PlayerInfos[i].Name,
                        IP = packet.PlayerInfos[i].IP,
                        Ping = packet.PlayerInfos[i].Ping,
                        Position = packet.PlayerInfos[i].Position,
                        LevelFile = packet.PlayerInfos[i].LevelFile,
                        PlayTime = packet.PlayerInfos[i].PlayTime
                    });
                }

            }
            else
                DisplayMessage("You are not authorized.");
        }

        private void HandleChatMessage(ChatMessagePacket packet)
        {
            if (Authorized)
            {
                var builder = new StringBuilder();
                foreach (var line in BasicUIVM.ConsoleOutput.Split(new [] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                    builder.AppendLine(line);

                builder.AppendLine($"{packet.Player}: {packet.Message}");


                var reversed = builder.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Reverse();
                builder.Clear();
                foreach (var line in reversed)
                    builder.AppendLine(line);


                BasicUIVM.ConsoleOutput = builder.ToString();
            }
            else
                DisplayMessage("You are not authorized.");
        }

        private void HandleLogListResponse(LogListResponsePacket packet)
        {
            if (Authorized)
            {
                BasicUIVM.LogsGridDataList.Clear();
                for (var i = 0; i < packet.Logs.Length; i++)
                {
                    BasicUIVM.LogsGridDataList.Add(new LogsDataGridModel
                    {
                        Number = i,
                        LogFilename = packet.Logs[i].LogFileName
                    });
                }
            }
            else
                DisplayMessage("You are not authorized.");
        }

        private void HandleLogFileResponse(LogFileResponsePacket packet)
        {
            if (Authorized)
                BasicUIVM.DisplayLog(packet.LogFilename, packet.LogFile);
            else
                DisplayMessage("You are not authorized.");
        }

        private void HandleCrashLogListResponse(CrashLogListResponsePacket packet)
        {
            if (Authorized)
            {
                BasicUIVM.CrashLogsGridDataList.Clear();
                for (var i = 0; i < packet.CrashLogs.Length; i++)
                {
                    BasicUIVM.CrashLogsGridDataList.Add(new LogsDataGridModel
                    {
                        Number = i,
                        LogFilename = packet.CrashLogs[i].LogFileName
                    });
                }
            }
            else
                DisplayMessage("You are not authorized.");
        }

        private void HandleCrashLogFileResponse(CrashLogFileResponsePacket packet)
        {
            if (Authorized)
            {
                BasicUIVM.DisplayLog(packet.CrashLogFilename, packet.CrashLogFile);
            }
            else
                DisplayMessage("You are not authorized.");
        }

        private void HandlePlayerDatabaseListResponse(PlayerDatabaseListResponsePacket packet)
        {
            if (Authorized)
            {
                BasicUIVM.PlayersDatabaseGridDataList.Clear();
                for (var i = 0; i < packet.PlayerDatabases.Length; i++)
                {
                    BasicUIVM.PlayersDatabaseGridDataList.Add(new PlayersDatabaseDataGridModel
                    {
                        Number = i,
                        Name = packet.PlayerDatabases[i].Name,
                        LastIP = packet.PlayerDatabases[i].LastIP,
                        LastSeen = packet.PlayerDatabases[i].LastSeen,
                    });
                }
            }
            else
                DisplayMessage("You are not authorized.");
        }

        private void HandleBanListResponse(BanListResponsePacket packet)
        {
            if (Authorized)
            {
                BasicUIVM.BansGridDataList.Clear();
                for (var i = 0; i < packet.Bans.Length; i++)
                {
                    BasicUIVM.BansGridDataList.Add(new BansDataGridModel
                    {
                        Number = i,
                        Name = packet.Bans[i].Name,
                        IP = packet.Bans[i].IP,
                        BanTime = packet.Bans[i].BanTime,
                        UnBanTime = packet.Bans[i].UnBanTime,
                        Reason = packet.Bans[i].Reason,
                    });
                }
            }
            else
                DisplayMessage("You are not authorized.");
        }
    }
}
