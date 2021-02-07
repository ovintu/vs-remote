using VsRemote.Models;

namespace VsRemote.Interfaces
{
    public interface ISolutionParserService
    {
        VsSolution ParseSolution(EnvDTE.Solution solution);
    }
}
