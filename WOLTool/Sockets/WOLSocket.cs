using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace WOLTool.Sockets
{
    /// <summary>
    /// A class that provides methods for sending Wake-on-LAN (WOL) magic packets to wake up computers on a network.
    /// Encapsulates a Socket object for sending UDP packets.
    /// </summary>
    public class WOLSocket : Socket
    {
        /// <summary>
        /// The IP Endpoints to which the magic packet(s) will be sent.
        /// </summary>
        protected virtual IEnumerable<IPEndPoint> Endpoints { get; } = new IPEndPoint[]
        {
            new IPEndPoint(IPAddress.Broadcast, 0), // legacy
            new IPEndPoint(IPAddress.Broadcast, 7), // echo
            new IPEndPoint(IPAddress.Broadcast, 9) // discard
        };

        public WOLSocket() : base(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
        {
            EnableBroadcast = true; // Enable broadcast, required for macOS compatibility
        }

        /// <summary>
        /// Broadcasts a Wake-on-LAN magic packet to the specified MAC address.
        /// </summary>
        /// <param name="macAddress">MAC Address to wake in string format (will be parsed).</param>
        public void Broadcast(string macAddress)
        {
            var physicalAddress = PhysicalAddress.Parse(macAddress);
            Broadcast(physicalAddress);
        }

        /// <summary>
        /// Broadcasts a Wake-on-LAN magic packet to the specified MAC addresses.
        /// </summary>
        /// <param name="macAddresses">MAC Addresses to wake in string format (will be parsed).</param>
        public void Broadcast(IEnumerable<string> macAddresses)
        {
            var physicalAddresses = macAddresses
                .Select(mac => PhysicalAddress.Parse(mac));
            Broadcast(physicalAddresses);
        }

        /// <summary>
        /// Asynchronously Broadcasts a Wake-on-LAN magic packet to the specified MAC address.
        /// </summary>
        /// <param name="macAddress">MAC Address to wake in string format (will be parsed).</param>
        /// <returns>Task</returns>
        public async Task BroadcastAsync(string macAddress)
        {
            var physicalAddress = PhysicalAddress.Parse(macAddress);
            await BroadcastAsync(physicalAddress).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously Broadcasts a Wake-on-LAN magic packet to the specified MAC address.
        /// </summary>
        /// <param name="macAddress">MAC Address to wake.</param>
        /// <returns>Task</returns>
        public async Task BroadcastAsync(PhysicalAddress macAddress)
        {
            await Task.Run(() =>
            {
                Broadcast(macAddress);
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously Broadcasts a Wake-on-LAN magic packet to the specified MAC addresses.
        /// </summary>
        /// <param name="macAddresses">MAC Addresses to wake in string format (will be parsed).</param>
        /// <returns>Task</returns>
        public async Task BroadcastAsync(IEnumerable<string> macAddresses)
        {
            var physicalAddresses = macAddresses
                .Select(mac => PhysicalAddress.Parse(mac));
            await BroadcastAsync(physicalAddresses).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously Broadcasts a Wake-on-LAN magic packet to the specified MAC addresses.
        /// </summary>
        /// <param name="macAddresses">MAC Addresses to wake.</param>
        /// <returns>Task</returns>
        public async Task BroadcastAsync(IEnumerable<PhysicalAddress> macAddresses)
        {
            await Task.Run(() =>
            {
                Broadcast(macAddresses);
            }).ConfigureAwait(false);
        }

        #region Core Functionality

        /// <summary>
        /// Broadcasts a Wake-on-LAN magic packet to the specified MAC address.
        /// </summary>
        /// <param name="macAddress">MAC Address to wake.</param>
        public void Broadcast(PhysicalAddress macAddress) =>
            Broadcast_Internal(macAddress);

        /// <summary>
        /// Broadcasts a Wake-on-LAN magic packet to the specified MAC addresses.
        /// </summary>
        /// <param name="macAddresses">MAC Addresses to wake.</param>
        public void Broadcast(IEnumerable<PhysicalAddress> macAddresses)
        {
            foreach (var macAddress in macAddresses)
            {
                Broadcast_Internal(macAddress);
            }
        }

        private void Broadcast_Internal(PhysicalAddress mac)
        {
            byte[] magicPacket = BuildMagicPacket(mac); // Get magic packet byte array based on MAC Address
            foreach (var ep in this.Endpoints) // Broadcast to *all* WOL Endpoints
            {
                this.SendTo(magicPacket, ep); // Broadcast magic packet
            }
        }

        private static byte[] BuildMagicPacket(PhysicalAddress macAddress)
        {
            byte[] macBytes = macAddress.GetAddressBytes(); // Convert 48 bit MAC Address to array of bytes
            byte[] magicPacket = new byte[102];
            for (int i = 0; i < 6; i++) // 6 times 0xFF
            {
                magicPacket[i] = 0xFF;
            }
            for (int i = 6; i < 102; i += 6) // 16 times MAC Address
            {
                Buffer.BlockCopy(macBytes, 0, magicPacket, i, 6);
            }
            return magicPacket; // 102 Byte Magic Packet
        }

        #endregion
    }
}
