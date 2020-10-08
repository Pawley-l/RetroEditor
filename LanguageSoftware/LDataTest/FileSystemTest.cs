using System;
using NUnit.Framework;
using LData;
using LData.FileSystemItems;

namespace LDataTest
{
    /**
     * NOTE: This only works on windows!
     * TODO: Add file saving and folder moving testing
     */
    [TestFixture]
    public class FileSystemTest
    {
        /**
         * Checks for C Drive since it has to exist on every windows computer
         */
        [Test]
        public void GetFileSystemRootTest()
        {
            // Get the C Drive
            var DriveExists = false;

            foreach (var item in FileSystem.GetFileSystemRoot())
            {
                
                if (item is Drive drive && !DriveExists)
                {
                    DriveExists = (drive.Id == @"C:\");
                }
            }
            Assert.IsTrue(DriveExists);
        }
        
        /**
         * Gets the ReadTest.txt file from the example folder in the root of this project
         */
        [Test]
        public void GetFile()
        {
            var file = FileSystem.GetFileContents(
                System.IO.Directory.GetCurrentDirectory() + "../../../../../../Examples/Tests/ReadTest.txt");
            
            Assert.AreEqual("TRUE", file[0]);
        }
        
        /**
         * Looks for ReadTest.txt from the example folder in the root of this project
         */
        [Test]
        public void GetFolder()
        {
            // Gets an project folder
            var FileExists = false;
            var folder = System.IO.Directory.GetCurrentDirectory() + "../../../../../../Examples/Tests/";
            
            foreach (var item in FileSystem.GetFolderContents(folder))
            {
                if (item is File file && !FileExists)
                {
                    Console.WriteLine(file.FileName);
                    
                    FileExists = (file.FileName == "ReadTest.txt");
                }
            }
            Assert.IsTrue(FileExists);
        }
    }
}