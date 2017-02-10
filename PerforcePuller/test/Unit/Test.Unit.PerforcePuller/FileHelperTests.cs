using System.Collections.Generic;
using NUnit.Framework;
using PerforcePuller.Helpers;

namespace Test.Unit.PerforcePuller
{
    [TestFixture]
    internal class FileHelperTests
    {

        private static IEnumerable<TestCaseData> FormattingPathCases()
        {
            yield return new TestCaseData("//folder/subfolder", "//folder/subfolder/...");
            yield return new TestCaseData("//folder/subfolder/", "//folder/subfolder/...");
            yield return new TestCaseData("//folder/subfolder/...", "//folder/subfolder/...");
            yield return new TestCaseData("//FOLDER/SUBFOLDER/...", "//folder/subfolder/...");
        }

        [Test, TestCaseSource("FormattingPathCases")]
        public void FileNameHelper_PerforcePathFormatter_ReturnsFormattedPath(string testPath, string expectedPath)
        {
            var results = FileNameHelper.PerforcePathFormatter(testPath);
            Assert.That(results, Is.EqualTo(expectedPath));
        }

        private static IEnumerable<TestCaseData> SamePathCases()
        {
            yield return new TestCaseData("//folder/subfolder/...", "//folder/subfolder/...");
            yield return new TestCaseData("//folder/subfolder/file.cs", "//folder/subfolder/file.cs");
        }

        [Test, TestCaseSource("SamePathCases")]
        public void FileNameHelper_PerforcePathFormatter_ReturnsSamePath(string testPath, string expectedPath)
        {
            var results = FileNameHelper.PerforcePathFormatter(testPath);
            Assert.That(results, Is.EqualTo(expectedPath));
        }

        private static IEnumerable<TestCaseData> RootPathCases()
        {
            yield return new TestCaseData("//folder/subfolder/subsubfolder/...", "//folder/subfolder/");
            yield return new TestCaseData("//folder/subfolder/file.cs", "//folder/subfolder/");
        }

        [Test, TestCaseSource("RootPathCases")]
        public void FileNameHelper_PerforceRootFolder_ReturnsParentFolder(string testPath, string expectedPath)
        {
            var results = FileNameHelper.PerforceGetParentFolder(testPath);
            Assert.That(results, Is.EqualTo(expectedPath));
        }

    }
}
