using System.Linq;
using WindowsFirewallHelper;

namespace FileSystemAndRegistrySnapshots
{
    public static class ScanFirewall
    {
        public static void Temp()
        {
            var allRules = FirewallManager.Instance.Rules.ToArray();
        }
    }
}
