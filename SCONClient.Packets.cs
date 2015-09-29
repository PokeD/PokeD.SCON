﻿using Org.BouncyCastle.Crypto;
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

                for (var i = 0; i < packet.PlayerInfoList.Length; i++)
                {
                    var player = packet.PlayerInfoList[i];
                    BasicUIVM.PlayersGridDataList.Add(new PlayersDataGridModel
                    {
                        Number = i,
                        Name = player.Name,
                        GameJoltID = player.GameJoltID,
                        IP = player.IP,
                        Ping = player.Ping,
                        Online = true
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

            }
            else
                DisplayMessage("You are not authorized.");
        }

        private void HandleLogListResponse(LogListResponsePacket packet)
        {
            if (Authorized)
            {
                BasicUIVM.LogsGridDataList.Clear();

                for (var i = 0; i < packet.LogList.Length; i++)
                {
                    var log = packet.LogList[i];
                    BasicUIVM.LogsGridDataList.Add(new LogsDataGridModel
                    {
                        Number = i,
                        LogFilename = log.LogFileName
                    });
                }
            }
            else
                DisplayMessage("You are not authorized.");
        }

        private void HandleLogFileResponse(LogFileResponsePacket packet)
        {
            if (Authorized)
            {
                BasicUIVM.DisplayLog(packet.LogFilename, packet.LogFile);
            }
            else
                DisplayMessage("You are not authorized.");
        }

        private void HandleCrashLogListResponse(CrashLogListResponsePacket packet)
        {
            if (Authorized)
            {
                BasicUIVM.CrashLogsGridDataList.Clear();

                for (var i = 0; i < packet.CrashLogList.Length; i++)
                {
                    var log = packet.CrashLogList[i];
                    BasicUIVM.CrashLogsGridDataList.Add(new LogsDataGridModel
                    {
                        Number = i,
                        LogFilename = log.LogFileName
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

                for (var i = 0; i < packet.PlayerDatabaseList.Length; i++)
                {
                    var databaseEntry = packet.PlayerDatabaseList[i];
                    BasicUIVM.PlayersDatabaseGridDataList.Add(new PlayersDatabaseDataGridModel
                    {
                        Number = i,
                        Name = databaseEntry.Name,
                        GameJoltID = databaseEntry.GameJoltID,
                        LastIP = databaseEntry.LastIP,
                        LastSeen = databaseEntry.LastSeen
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

                for (var i = 0; i < packet.BanList.Length; i++)
                {
                    var ban = packet.BanList[i];
                    BasicUIVM.BansGridDataList.Add(new BansDataGridModel
                    {
                        Number = i,
                        Name = ban.Name,
                        GameJoltID = ban.GameJoltID,
                        IP = ban.IP,
                        BanTime = ban.BanTime,
                        UnBanTime = ban.UnBanTime,
                        Reason = ban.Reason
                    });
                }
            }
            else
                DisplayMessage("You are not authorized.");
        }
    }
}
