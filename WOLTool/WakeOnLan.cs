using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using WOLTool.Sockets;

namespace WOLTool
{
    /// <summary>
    /// A static class that provides one-and-done methods for sending Wake-on-LAN (WOL) magic packets to wake up computers on a network.
    /// If you are using these methods in quick succession, consider using the WOLSocket class instead, to avoid Socket Exhaustion.
    /// </summary>
    public static class WakeOnLan
    {
        /// <summary>
        /// Broadcasts a Wake-on-LAN magic packet to the specified MAC addresses.
        /// </summary>
        /// <param name="macAddresses">MAC Addresses to wake in string format (will be parsed).</param>
        public static void Broadcast(IEnumerable<string> macAddresses)
        {
            using (var socket = new WOLSocket())
            {
                socket.Broadcast(macAddresses);
            }
        }

        /// <summary>
        /// Asynchronously Broadcasts a Wake-on-LAN magic packet to the specified MAC addresses.
        /// </summary>
        /// <param name="macAddresses">MAC Addresses to wake in string format (will be parsed).</param>
        /// <returns>Task</returns>
        public static async Task BroadcastAsync(IEnumerable<string> macAddresses)
        {
            using (var socket = new WOLSocket())
            {
                await socket.BroadcastAsync(macAddresses);
            }
        }

        /// <summary>
        /// Broadcasts a Wake-on-LAN magic packet to the specified MAC address.
        /// </summary>
        /// <param name="macAddress">MAC Address to wake in string format (will be parsed).</param>
        public static void Broadcast(string macAddress)
        {
            using (var socket = new WOLSocket())
            {
                socket.Broadcast(macAddress);
            }
        }

        /// <summary>
        /// Asynchronously Broadcasts a Wake-on-LAN magic packet to the specified MAC address.
        /// </summary>
        /// <param name="macAddress">MAC Address to wake in string format (will be parsed).</param>
        /// <returns>Task</returns>
        public static async Task BroadcastAsync(string macAddress)
        {
            using (var socket = new WOLSocket())
            {
                await socket.BroadcastAsync(macAddress);
            }
        }

        /// <summary>
        /// Broadcasts a Wake-on-LAN magic packet to the specified MAC address.
        /// </summary>
        /// <param name="macAddress">MAC Address to wake.</param>
        public static void Broadcast(PhysicalAddress macAddress)
        {
            using (var socket = new WOLSocket())
            {
                socket.Broadcast(macAddress);
            }
        }

        /// <summary>
        /// Broadcasts a Wake-on-LAN magic packet to the specified MAC addresses.
        /// </summary>
        /// <param name="macAddresses">MAC Addresses to wake.</param>
        public static void Broadcast(IEnumerable<PhysicalAddress> macAddresses)
        {
            using (var socket = new WOLSocket())
            {
                socket.Broadcast(macAddresses);
            }
        }

        /// <summary>
        /// Asynchronously Broadcasts a Wake-on-LAN magic packet to the specified MAC address.
        /// </summary>
        /// <param name="macAddress">MAC Address to wake.</param>
        /// <returns>Task</returns>
        public static async Task BroadcastAsync(PhysicalAddress macAddress)
        {
            using (var socket = new WOLSocket())
            {
                await socket.BroadcastAsync(macAddress);
            }
        }

        /// <summary>
        /// Asynchronously Broadcasts a Wake-on-LAN magic packet to the specified MAC addresses.
        /// </summary>
        /// <param name="macAddresses">MAC Addresses to wake.</param>
        /// <returns>Task</returns>
        public static async Task BroadcastAsync(IEnumerable<PhysicalAddress> macAddresses)
        {
            using (var socket = new WOLSocket())
            {
                await socket.BroadcastAsync(macAddresses);
            }
        }
    }
}
