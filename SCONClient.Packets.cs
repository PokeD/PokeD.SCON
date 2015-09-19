using PokeD.Core.Packets.SCON.Authorization;
using PokeD.Core.Packets.SCON.Status;

using PokeD.SCON.Exceptions;

namespace PokeD.SCON
{
    public partial class SCONClient
    {
        AuthorizationStatus AuthorizationStatus { get; set; }

        private void HandleAuthorizationResponse(AuthorizationResponsePacket packet)
        {
            AuthorizationStatus = packet.AuthorizationStatus;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="packet"></param>
        private void HandleEncryptionResponse(EncryptionResponsePacket packet)
        {
            if ((AuthorizationStatus & AuthorizationStatus.EncryprionEnabled) != AuthorizationStatus.EncryprionEnabled)
                throw new SCONException("Encryption was not enabled!");
            else
            {

            }
        }

        private void HandlePlayerListResponse(PlayerListResponsePacket packet)
        {

        }
    }
}
