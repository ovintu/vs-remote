namespace VsRemote.Models
{
    public class VisualStudioInstance
    {
        public string VsEdition { get; set; }
        public VsMode CurrentMode { get; set; }
        public bool SolutionLoaded { get; set; }
    }
}