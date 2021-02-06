using System.Collections.Generic;
using System.Threading.Tasks;
using VSRemote.Interfaces;
using VSRemote.Models;

namespace VSRemote.Services
{
    public class VisualStudioService : IVisualStudioService
    {
        public async Task<IEnumerable<VisualStudioInstance>> GetRunningInstancesAsync()
        {
            await Task.Delay(1);
            return new List<VisualStudioInstance>();
        }

        public void StartBuild()
        {

        }
    }
}