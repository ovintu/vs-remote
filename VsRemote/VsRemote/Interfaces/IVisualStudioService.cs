using EnvDTE;
using System.Collections.Generic;
using System.Threading.Tasks;
using VsRemote.Models;

namespace VsRemote.Interfaces
{
    public interface IVisualStudioService
    {
        Task<IEnumerable<VisualStudioInstance>> GetRunningInstancesAsync();
        Task<VsSolution> GetSolutionDetails(int id);
        void StartBuild();
    }
}
