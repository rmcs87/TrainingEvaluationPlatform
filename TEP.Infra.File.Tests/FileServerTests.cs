using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TEP.Infra.File.Tests
{
    [TestClass]
    public class FileServerTests
    {
        [TestMethod]
        public void OnCreateFile_WithValidData_CreatesAndReturnsPath()
        {
        }
        
        [TestMethod]
        public void OnCreateFile_WithInvalidData_ThrowsFileCreationException()
        {
        }
        
        [TestMethod]
        public void OnRemoveFile_WithValidData_Removes()
        {
        }
        
        [TestMethod]
        public void OnRemoveFile_WithValidPath_ThrowsFileRemovalException()
        {
        }
        
        [TestMethod]
        public void OnCreateFile_WithInvalidPath_ThrowsFileCreationException()
        {
        }
        
        [TestMethod]
        public void OnGetFile_WithValidPath_Removes()
        {
        }
        
        [TestMethod]
        public void OnGetFile_WithInvalidPatha_ThrowsFileRetrievalException()
        {
        }
    }
}
