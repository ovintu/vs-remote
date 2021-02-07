using System.Collections.Generic;

namespace VsRemote.Models
{
    public class VsSolution
    {
        public string Name { get; set; }
        public List<VsProject> VsProjects {get; set;}
    }
}
