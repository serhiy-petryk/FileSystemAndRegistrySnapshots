using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using WindowsFirewallHelper;

namespace FileSystemAndRegistrySnapshots
{
    public static class ScanFirewall
    {
        public static void Temp()
        {
            var allRules = FirewallManager.Instance.Rules.ToArray();
            var r1 = allRules.Where(a => string.Equals(a.Name, "Core Networking - Router Advertisement (ICMPv6-Out)")).ToArray();
            var r2 = allRules.Where(a => string.Equals(a.Name, "BranchCache Peer Discovery (WSD-In)")).ToArray();
            var r3 = allRules.Where(a => string.Equals(a.Name, "Connect to a Network Projector (TCP-In)")).ToArray();
            var r4 = allRules.Where(a => string.Equals(a.Name, "Core Networking - IPHTTPS (TCP-Out)")).ToArray();
            var rr = allRules.Where(a => !string.Equals(a.Name, a.FriendlyName)).ToArray();
            var s1 = r3[0].Profiles.ToString().Replace(" | ", ", ");
            var a1 = r3[0].Protocol;
        }

        public static string SaveFirewallRulesIntoFile(string dataFolder, Action<string> showStatusAction)
        {
            if (!Directory.Exists(dataFolder)) throw new Exception($"ERROR! Data folder {dataFolder} doesn't exist");

            showStatusAction("Started");

            var log = new List<string> { GetHeaderString() };
            var allRules = FirewallManager.Instance.Rules.OrderBy(a => a.Direction).ThenBy(a => a.Name)
                .ThenBy(a => a.Profiles).ThenBy(a => a.Protocol.ToString()).ToArray();
            foreach(var rule in allRules)
                log.Add(GetFileString(rule));

            showStatusAction("Saving data ..");
            var zipFileName = Path.Combine(dataFolder, $"Firewall_{Helpers.GetSystemDriveLabel()}_{DateTime.Now:yyyyMMddHHmm}.zip");
            Helpers.SaveStringsToZipFile(zipFileName, log);

            showStatusAction($"Data saved into {Path.GetFileName(zipFileName)}");
            return zipFileName;
        }


        private static string GetHeaderString() =>
            $"Direction\tName\tProfile\tEnabled\tAction\tProgram\tLocal Address\tRemote Address\tProtocol\tLocal Port\tRemote Port";

        private static string GetFileString(IFirewallRule rule)
        {
            var s = rule.Protocol.ToString();
            var protocol = s.StartsWith("{") && s.EndsWith("}") ? s.Substring(1, s.Length - 2) : s;
            var direction = rule.Direction == FirewallDirection.Inbound ? "In" : "Out";
            var profiles = rule.Profiles.ToString().Replace(" | ", ", ");
            var enabled = rule.IsEnable ? "Yes" : "No";

            return
                $"{direction}\t{rule.Name}\t{profiles}\t{enabled}\t{rule.Action}\t{rule.ApplicationName}" + 
                $"\t{GetAddresses(rule.LocalAddresses)}\t{GetAddresses(rule.RemoteAddresses)}\t{protocol}"+
                $"\t{GetPorts(rule.LocalPorts)}\t{GetPorts(rule.RemotePorts)}";

            string GetAddresses(IAddress[] addresses) => (addresses.Length == 0 ? "Any" : string.Join(", ", addresses.Select(a => a.ToString()))).Replace("*", "Any");
            string GetPorts(ushort[] ports) => ports.Length == 0 ? "Any" : string.Join(", ", ports.Select(a => a.ToString()));
        }
    }
}
