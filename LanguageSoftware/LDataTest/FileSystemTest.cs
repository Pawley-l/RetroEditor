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
                    DriveExists = (drive.Id == "C");
                }
                else 
                    Assert.Fail(); // This should return drives only
            }
            Assert.IsTrue(DriveExists);
        }
        
        /**
         * Gets the ReadTest.txt file from the example folder in the root of this project
         */
        [Test]
        public void GetFile()
        {
            Assert.AreSame("TRUE", FileSystem.GetFileContents("../../../../../Examples/ReadTest.txt")[0]);
        }
        
        /**
         * Looks for ReadTest.txt from the example folder in the root of this project
         */
        [Test]
        public void GetFolder()
        {
            // Gets an project folder
            var FileExists = false;

            foreach (var item in FileSystem.GetFileSystemRoot())
            {
                if (item is File file && !FileExists)
                {
                    FileExists = (file.FileName == "ReadTest.txt");
                }
                else 
                    Assert.Fail(); // This directory shouldn't have any folders in it
            }
            Assert.IsTrue(FileExists);

        }
        
        /**
         * Looks for the "Windows" folder that exists in every windows install (windows 10)
         */
        [Test]
        public void GetDrive()
        {
            var FolderExists = false;

            foreach (var item in FileSystem.GetFileSystemRoot())
            {
                if (item is Folder folder && !FolderExists)
                {
                    FolderExists = (folder.Path == "C:/Windows");
                }
            }
            Assert.IsTrue(FolderExists);
        }
    }
}