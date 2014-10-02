using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.ProjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GoToRelatedFiles.UnitTest
{
    [TestClass]
    public class RelatedFilesProviderTest
    {
        private RelatedFilesProvider relatedFilesProvider;

        private Mock<IFindProjectFiles> findProjectFilesMock;

        private IEnumerable<string> Actual;


        [TestInitialize]
        public void Setup()
        {
            Actual = null;
            relatedFilesProvider = new RelatedFilesProvider();

            findProjectFilesMock = new Mock<IFindProjectFiles>();
            relatedFilesProvider.FindProjectFiles = findProjectFilesMock.Object;
        }

        [TestMethod]
        public void GetRelatedFilesShouldNotThrowException()
        {
            var projectFileMock = new Mock<IProjectFile>();
            relatedFilesProvider.GetRelatedFiles(projectFileMock.Object);
        }

        [TestMethod]    
        public void GetRelatedFilesShouldReturnCorrectTuple()
        {
            var originalProjectFileMock = new Mock<IProjectFile>();
            var projectFileMock = new Mock<IProjectFile>();

            findProjectFilesMock.Setup(x => x.Find(originalProjectFileMock.Object)).Returns(new List<IProjectFile>(){projectFileMock.Object});

            var relatedFiles = relatedFilesProvider.GetRelatedFiles(originalProjectFileMock.Object);

            Assert.AreEqual(projectFileMock.Object, relatedFiles.First().A);
            Assert.AreEqual("xxxx", relatedFiles.First().B);
            Assert.AreEqual(originalProjectFileMock.Object, relatedFiles.First().C);
        }

        [TestMethod]
        public void GetRelatedFilesShouldReturnCorrectTupleWithAllFoundProjectFiles()
        {
            var originalProjectFileMock = new Mock<IProjectFile>();
            var projectFileMock1 = new Mock<IProjectFile>();
            var projectFileMock2 = new Mock<IProjectFile>();

            findProjectFilesMock.Setup(x => x.Find(originalProjectFileMock.Object)).Returns(new List<IProjectFile>(){projectFileMock1.Object, projectFileMock2.Object});

            var relatedFiles = relatedFilesProvider.GetRelatedFiles(originalProjectFileMock.Object);

            var jetTuple1 = relatedFiles.First(x => x.A == projectFileMock1.Object);
            Assert.AreEqual(projectFileMock1.Object, jetTuple1.A);
            Assert.AreEqual("xxxx", jetTuple1.B);
            Assert.AreEqual(originalProjectFileMock.Object, jetTuple1.C);

            var jetTuple2 = relatedFiles.First(x => x.A == projectFileMock2.Object);
            Assert.AreEqual(projectFileMock2.Object, jetTuple2.A);
            Assert.AreEqual("xxxx", jetTuple2.B);
            Assert.AreEqual(originalProjectFileMock.Object, jetTuple2.C);
        }


    }
}
