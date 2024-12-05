using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.TaskScheduler;

namespace FileSystemAndRegistrySnapshots
{
    public static class ScanTaskScheduler
    {
        public static void Test()
        {
            var aa1 = TaskService.Instance.AllTasks.OrderBy(a=>a.Folder).ThenBy(a=>a.Name).ToArray();
            var a1 = aa1.Where(a => !a.Enabled).ToArray();
            var a2 = aa1.Where(a => a.IsActive).ToArray();
            var a31 = aa1.Where(a => a.State == TaskState.Disabled).ToArray();
            var a32 = aa1.Where(a => a.State == TaskState.Queued).ToArray();
            var a33 = aa1.Where(a => a.State == TaskState.Ready).ToArray();
            var a34 = aa1.Where(a => a.State == TaskState.Running).ToArray();
            var a35 = aa1.Where(a => a.State == TaskState.Unknown).ToArray();
        }
    }
}
