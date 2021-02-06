using System.Collections.Generic;
using System.Threading.Tasks;
using VSRemote.Models;

namespace VSRemote.Interfaces
{
    public interface IVisualStudioService
    {
        Task<IEnumerable<VisualStudioInstance>> GetRunningInstancesAsync();
        void StartBuild();
    }
}
