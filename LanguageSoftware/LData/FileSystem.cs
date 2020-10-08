using System.Collections.Generic;
using System.IO;
using System.Linq;
using LData.Exceptions;
using LData.FileSystemItems;
using File = LData.FileSystemItems.File;

namespace LData
{
    /**
     * <summary>
     * Static FileSystem Management class designed to work with windows.
     * </summary>
     *
     * Class that handles all filesystem actions. Exists due to the need to separate concerns.
     */
    public static class FileSystem
    {
        /**
         * <summary> Gets all Drives and their contents </summary>
         */
        public static List<IFileSystemItem> GetFileSystemRoot()
        {
            var drives = System.IO.Directory.GetLogicalDrives();

            return drives.Select(drive => new Drive(drive)).Cast<IFileSystemItem>().ToList();
        }
        
        /**
         * <summary> Gets the contents of a drive </summary>
         * <exception cref="FileSystemException"> Throws if the drive doesnt exist </exception>
         */
        public static List<IFileSystemItem> GetDriveContents(string id)
        {
            return new Drive(id).Contents;
        }
        
        /**
         * <summary>Gets the contents of a folder</summary>
         * <param name="path"> path to the folder </param>
         * <exception cref="FileSystemException"> Throws if the folder doesnt exist </exception>
         */
        public static List<IFileSystemItem> GetFolderContents(string path)
        {
            return new Folder(path, Path.GetPathRoot(path)).Contents;
        }
        
        /**
         * <summary> Gets the contents of a file</summary>
         * <param name="path"> path to the file </param>
         * <exception cref="FileSystemException"> Throws if the file doesnt exist </exception>
         */
        public static List<string> GetFileContents(string path)
        {
            return new File(path, Path.GetPathRoot(path)).Contents;
        }

        /**
         * <summary> Sets the contents of a file </summary>
         * <param name="path"> path to the file </param>
         * <param name="new_contents"> the new files content </param>
         * <param name="create"> Optional. If the file doesnt exist, create it with the content </param>
         * <exception cref="FileSystemException"> Throws if the file doesnt exist and create isn't true </exception>
         */
        public static void SetFileContents(string path, List<string> new_contents, bool create = false)
        {
            var file = new File(path, Path.GetPathRoot(path), create) { Contents = new_contents };
        }
    }
}