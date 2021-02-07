using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using VsRemote.Models;
using VsRemote.Interfaces;

namespace VsRemote.Services
{
    public class VisualStudioService : IVisualStudioService
    {
        public async Task<IEnumerable<VisualStudioInstance>> GetRunningInstancesAsync()
        {
            await Task.Delay(1);
            var vsInstances = this.GetInstances();
            if (vsInstances != null && vsInstances.Any())
                return this.GetVsInstancesDetails(vsInstances);
            return new List<VisualStudioInstance>();
        }

        public async Task<VsSolution> GetSolutionDetails(int id)
        {
            await Task.Delay(1);
            var vsInstances = this.GetInstances();
            if (vsInstances != null && vsInstances.Any())
            {
                var instance = vsInstances.FirstOrDefault(instance => instance.MainWindow.HWnd == id);
                if (instance != null)
                {
                    return this.BuildSolution(instance);
                }
            }

            return new VsSolution();
        }

        private VsSolution BuildSolution(DTE instance)
        {
            throw new NotImplementedException();
        }

        public void StartBuild()
        {

        }

        private IEnumerable<VisualStudioInstance> GetVsInstancesDetails(IEnumerable<DTE> vsInstances)
        {
            var visualStudioInstances = new List<VisualStudioInstance>();
            foreach (DTE vsInstance in vsInstances)
            {
                visualStudioInstances.Add(new VisualStudioInstance
                {
                    Id = vsInstance.MainWindow.HWnd,
                    VsEdition = vsInstance.Version,
                    CurrentMode = (VsMode)vsInstance.Debugger.CurrentMode,
                    SolutionLoaded = !string.IsNullOrEmpty(vsInstance.Solution.FullName)
                });
            }

            return visualStudioInstances;
        }

        //https://stackoverflow.com/questions/14205933/how-do-i-get-the-dte-for-running-visual-studio-instance
        private IEnumerable<DTE> GetInstances()
        {
            IRunningObjectTable rot;
            IEnumMoniker enumMoniker;
            int retVal = GetRunningObjectTable(0, out rot);

            if (retVal == 0)
            {
                rot.EnumRunning(out enumMoniker);

                IntPtr fetched = IntPtr.Zero;
                IMoniker[] moniker = new IMoniker[1];
                while (enumMoniker.Next(1, moniker, fetched) == 0)
                {
                    IBindCtx bindCtx;
                    CreateBindCtx(0, out bindCtx);
                    string displayName;
                    moniker[0].GetDisplayName(bindCtx, null, out displayName);
                    Console.WriteLine("Display Name: {0}", displayName);
                    bool isVisualStudio = displayName.StartsWith("!VisualStudio");
                    if (isVisualStudio)
                    {
                        object obj;
                        rot.GetObject(moniker[0], out obj);
                        var dte = obj as DTE;
                        yield return dte;
                    }
                }
            }
        }

        [DllImport("ole32.dll")]
        private static extern void CreateBindCtx(int reserved, out IBindCtx ppbc);

        [DllImport("ole32.dll")]
        private static extern int GetRunningObjectTable(int reserved, out IRunningObjectTable prot);
    }
}