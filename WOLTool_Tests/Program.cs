using System.Net.NetworkInformation;
using WOLTool;

namespace WOLTool_Tests
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            Console.WriteLine($"Running Tests for DotNet {Environment.Version}");
            const string macStr1 = "84:1B:77:4C:9F:22";
            const string macStr2 = "3C:98:23:11:A6:7B";
            const string macStr3 = "00:15:5D:8E:44:C1";
            var mac1 = PhysicalAddress.Parse(macStr1);
            var mac2 = PhysicalAddress.Parse(macStr2);
            var mac3 = PhysicalAddress.Parse(macStr3);
            var macStrList = new List<string> { macStr1, macStr2, macStr3 };
            var macList = new List<PhysicalAddress> { mac1, mac2, mac3 };
            var t1 = Test_BroadcastAsync(macStr1);
            var t2 = Test_BroadcastAsync(mac1);
            var t3 = Test_BroadcastAsync(macStrList);
            var t4 = Test_BroadcastAsync(macList);
            Test_Broadcast(macStr1);
            Test_Broadcast(mac1);
            Test_Broadcast(macStrList);
            Test_Broadcast(macList);
            await Task.WhenAll(t1, t2, t3, t4);
            Console.WriteLine("All tests passed.");
            return 0;
        }


        private static void Test_Broadcast(IEnumerable<string> macAddresses) =>
            WakeOnLan.Broadcast(macAddresses);

        private static async Task Test_BroadcastAsync(IEnumerable<string> macAddresses) =>
            await WakeOnLan.BroadcastAsync(macAddresses);

        private static void Test_Broadcast(string macAddress) =>
            WakeOnLan.Broadcast(macAddress);

        private static async Task Test_BroadcastAsync(string macAddress) =>
            await WakeOnLan.BroadcastAsync(macAddress);

        private static void Test_Broadcast(PhysicalAddress macAddress) =>
            WakeOnLan.Broadcast(macAddress);

        private static void Test_Broadcast(IEnumerable<PhysicalAddress> macAddresses) =>
            WakeOnLan.Broadcast(macAddresses);

        private static async Task Test_BroadcastAsync(PhysicalAddress macAddress) =>
            await WakeOnLan.BroadcastAsync(macAddress);

        private static async Task Test_BroadcastAsync(IEnumerable<PhysicalAddress> macAddresses) =>
            await WakeOnLan.BroadcastAsync(macAddresses);
    }
}

