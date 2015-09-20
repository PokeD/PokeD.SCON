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
        private void HandleEncryptionResponse(EncryptionResponsePacket packet)
        {
            if(Authorized)
                return;

            if (AuthorizationStatus.HasFlag(AuthorizationStatus.EncryprionEnabled))
            {

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
