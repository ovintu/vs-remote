using System.Collections.Generic;
using System.IO;
using System.Linq;
using VsRemote.Interfaces;
using VsRemote.Models;

namespace VsRemote.Services
{
    public class SolutionParserService : ISolutionParserService
    {
        public VsSolution ParseSolution(EnvDTE.Solution dteSolution)
        {
            var vsSolution = new VsSolution
            {
                Name = Path.GetFileName(dteSolution.FullName)
            };

            var dteProjectsCollection = dteSolution.Projects;
            if (dteProjectsCollection == null || dteProjectsCollection.Count == 0)
                return vsSolution;

            vsSolution.VsProjects = new List<VsProject>();
            for (int index = 1; index <= dteProjectsCollection.Count; index++)
            {
                var dteProject = dteProjectsCollection.Item(index);
                if (dteProject != null)
                {
                    vsSolution.VsProjects.Add(new VsProject
                    {
                        Name = dteProject.Name,
                        Files = this.GetFilesForProject(dteProject)
                    });
                }
            }

            return vsSolution;
        }

        private List<VsFile> GetFilesForProject(EnvDTE.Project dteProject)
        {
            var csFilesInTheProject = new List<VsFile>();
            var dteProjectItems = this.GetDteProjectItems(dteProject.ProjectItems);
            foreach (var dteProjectItem in dteProjectItems)
            {
                csFilesInTheProject.Add(new VsFile
                {
                    Name = Path.GetFileName(dteProjectItem.FileNames[1])
                });
            }
            return csFilesInTheProject;
        }

        private IEnumerable<EnvDTE.ProjectItem> GetDteProjectItems(EnvDTE.ProjectItems envDteProjectItems)
        {
            var dteProjectItems = this.GetAllDteProjectItems(envDteProjectItems);
            if (dteProjectItems != null)
            {
                var dteProjectItemsList = new List<EnvDTE.ProjectItem>();
                foreach (EnvDTE.ProjectItem dteProjectItem in dteProjectItems)
                {
                    if (dteProjectItem.Kind != EnvDTE.Constants.vsProjectItemsKindMisc &&
                        dteProjectItem.Name.ToLower().EndsWith(".cs"))
                    {
                        dteProjectItemsList.Add(dteProjectItem);
                    }
                }

                return dteProjectItemsList;
            }

            return Enumerable.Empty<EnvDTE.ProjectItem>();
        }

        private IEnumerable<EnvDTE.ProjectItem> GetAllDteProjectItems(EnvDTE.ProjectItems dteProjectItems)
        {
            foreach (EnvDTE.ProjectItem dteProjectItem in dteProjectItems)
            {
                yield return dteProjectItem;
                if (dteProjectItem.SubProject != null && dteProjectItem.SubProject is EnvDTE.Project)
                {
                    foreach (EnvDTE.ProjectItem childDteItem in this.GetAllDteProjectItems(dteProjectItem.SubProject.ProjectItems))
                    {
                        yield return childDteItem;
                    }
                }
                else
                {
                    foreach (EnvDTE.ProjectItem childDteItem in GetAllDteProjectItems(dteProjectItem.ProjectItems))
                    {
                        yield return childDteItem;
                    }
                }
            }
        }
    }
}
