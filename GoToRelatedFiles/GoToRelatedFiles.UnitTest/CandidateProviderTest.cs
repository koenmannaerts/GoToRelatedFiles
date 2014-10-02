using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoToRelatedFiles.UnitTest
{
    [TestClass]
    public class CandidateProviderTest
    {
        private IEnumerable<string> Actual { get; set; }
        private RelatedFilesProviderTest relatedFilesProviderTest;

        [TestInitialize]
        public void Setup()
        {
            Actual = null;
        }

        [TestMethod]
        public void GetCandidatesShouldReturnFileTestOfFile()
        {
            CandidatesFor(Filenames("File"), "Test").ShouldReturn("FileTest", "File");
            CandidatesFor(Filenames("File", "File1"), "Test").ShouldReturn("FileTest", "File1Test", "File", "File1");
            CandidatesFor(Filenames("File", "File1"), "Test", "ViewModel").ShouldReturn("FileTest", "File1Test", "FileViewModel", "File1ViewModel", "File", "File1");
            CandidatesFor(Filenames("FileViewModel"), "Test", "ViewModel").ShouldReturn("FileTest", "FileViewModel", "File");
            CandidatesFor(Filenames("FileTest"), "Test", "ViewModel").ShouldReturn("FileTest", "FileViewModel", "File");
            CandidatesFor(Filenames("FileDummy"), "Test", "ViewModel", "Dummy").ShouldReturn("FileTest", "FileViewModel", "FileDummy", "File");
            CandidatesFor(Filenames("File"), "Test", "ViewModel", "Dummy").ShouldReturn("FileTest", "FileViewModel", "FileDummy", "File");
        }

        private static string[] Filenames(params string[] filenames)
        {
            return filenames;
        }

        private void ShouldReturn(params string[] files)
        {
            CollectionAssert.AreEquivalent(files, (ICollection) Actual);
        }

        private CandidateProviderTest CandidatesFor(IEnumerable<string> filenames, params string[] postfixs)
        {
            Actual = CandidateProvider.GetCandidates(filenames, postfixs);
            return this;
        }
    }
}