using System.Collections.Generic;
using JetBrains.ProjectModel;

namespace GoToRelatedFiles
{
    public interface IFindProjectFiles
    {
        IEnumerable<IProjectFile> Find(IProjectFile projectFile);
    }
}