using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LData.FileSystemItems
{
    public class Folder : IFileSystemItem
    {
        private string _path;
        private string _drive;

        internal Folder(string folder_path, string drive, bool create = false)
        {
            _path = folder_path;
            _drive = drive;
            
            if (create)
                CreateFolder(folder_path);
        }
        
        /**
         * <summary> Gets the path of the folder including the folder. Setting moves the folder </summary>
         */
        public string Path
        {
            get => _path;
            set => MoveFolder(value, _path);
        }
        
        /**
         * <summary> Gets the drive that the folder is inside </summary>
         */
        public string Drive
        {
            get => _drive;
        }
        
        /**
         * <summary> Gets the contents of the folder. To create a file use the File Class </summary>
         */
        public List<IFileSystemItem> Contents
        {
            get => GetContents(_path, _drive);
        }
        
        /**
         * Gets the contents of a folder
         * TODO: Add mutex for multithreading support
         */
        private static List<IFileSystemItem> GetContents(string path, string drive)
        {
            var files = Directory.GetFiles(path);
            // Gets all of the files
            var return_folder = files.Select(file => new File(file, drive)).Cast<IFileSystemItem>().ToList();
            
            // Gets all of the folders
            var folders = Directory.GetDirectories(path);
            return_folder.AddRange(folders.Select(folder => new Folder(folder, drive)).Cast<IFileSystemItem>());

            return return_folder;
        }
        
        /**
         * Moves a folder
         */
        private static void MoveFolder(string new_path, string previos_path)
        {
            Directory.Move(previos_path, new_path);
        }
        
        /**
         * Creates a folder
         */
        private static void CreateFolder(string path)
        {
            Directory.CreateDirectory(path);
        }
    }
}