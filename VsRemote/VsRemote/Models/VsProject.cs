using System.Collections.Generic;

namespace VsRemote.Models
{
    public class VsProject
    {
        public string Name { get; set; }
        public List<VsFile> Files { get; set; }
    }
}
