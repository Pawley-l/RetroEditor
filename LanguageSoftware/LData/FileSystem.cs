using System.Collections.Generic;
using LData.FileSystemItems;

namespace LData
{
    /**
     * <summary>
     * Static FileSystem Management class designed to work with windows.
     * </summary>
     */
    public static class FileSystem
    {
        /**
         * <summary>
         * Gets all Drives and their contents
         * </summary>
         */
        public static List<IFileSystemItem> GetFileSystemRoot()
        {
            return null;
        }
        
        /**
         * <summary>
         * Gets the contents of a drive
         * </summary>
         */
        public static List<IFileSystemItem> GetDriveContents(string path)
        {
            return null;
        }
        
        /**
         * <summary>
         * Gets the contents of a folder
         * </summary>
         */
        public static List<IFileSystemItem> GetFolderContents(string path)
        {
            return null;
        }
        
        /**
         * <summary>
         * Gets the contents of a file
         * </summary>
         */
        public static List<string> GetFileContents(string file)
        {
            return null;
        }

        /**
         * <summary>
         * Sets the contents of a file
         */
        public static void SetFileContents(string file, List<string> new_contents)
        {
            
        }
    }
}