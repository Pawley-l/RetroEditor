using System.Collections.Generic;

namespace LData.FileSystemItems
{
    public class Folder : IFileSystemItem
    {
        private string _path;
        private string _drive;
        private List<IFileSystemItem> _contents;

        internal Folder(string folder_path)
        {
            _path = folder_path;
            
            // TODO: load folder contents
        }
        
        /**
         * <summary> Gets the path of the folder including the folder. Setting moves the folder </summary>
         */
        public string Path
        {
            get => _path;
            set => _path = value;
        }
        
        /**
         * <summary> Gets the drive that the folder is inside </summary>
         */
        public string Drive
        {
            get => _drive;
        }
        
        /**
         * <summary> Gets the contents of the file </summary>
         */
        public List<IFileSystemItem> Contents
        {
            get => _contents;
        }

        private void MoveFolder(string new_path)
        {
            // TODO: move folder in filesystem
        }
    }
}