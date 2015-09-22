using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using PokeD.Core;
using PokeD.Core.Packets.SCON.Authorization;
using PokeD.Core.Packets.SCON.Chat;
using PokeD.Core.Packets.SCON.Logs;
using PokeD.Core.Packets.SCON.Status;

using PokeD.SCON.Exceptions;

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

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="packet"></param>
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

        private void HandlePlayerListResponse(PlayerListResponsePacket packet)
        {
            if (Authorized)
            {
                
            }
            else
                throw new SCONException("You wasn't authorized!!");
        }

        private void HandleChatMessage(ChatMessagePacket packet)
        {
            if (Authorized)
            {

            }
            else
                throw new SCONException("You wasn't authorized!!");
        }

        private void HandlePlayerLocationResponse(PlayerLocationResponsePacket packet)
        {
            if (Authorized)
            {

            }
            else
                throw new SCONException("You wasn't authorized!!");
        }

        private void HandleLogListResponse(LogListResponsePacket packet)
        {
            if (Authorized)
            {

            }
            else
                throw new SCONException("You wasn't authorized!!");
        }

        private void HandleLogFileResponse(LogFileResponsePacket packet)
        {
            if (Authorized)
            {

            }
            else
                throw new SCONException("You wasn't authorized!!");
        }

        private void HandleCrashLogListResponse(CrashLogListResponsePacket packet)
        {
            if (Authorized)
            {

            }
            else
                throw new SCONException("You wasn't authorized!!");
        }

        private void HandleCrashLogFileResponse(CrashLogFileResponsePacket packet)
        {
            if (Authorized)
            {

            }
            else
                throw new SCONException("You wasn't authorized!!");
        }
    }
}
