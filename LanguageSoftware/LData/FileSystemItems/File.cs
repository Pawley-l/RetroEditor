using System.Collections.Generic;

namespace LData.FileSystemItems
{
    public class File : IFileSystemItem
    {
        private string _filename;
        private string _path;
        private string _drive;
        private List<string> _contents;
        
        /**
         * <summary> Loads the file with the file path, optionally can create if doesn't exist </summary>
         * <param name="file_path"> Path to the file </param>
         * <param name="create"> Optional. Create file if doesn't exist </param>
         */
        internal File(string file_path, bool create = false)
        {
            _path = file_path;
            
            // TODO: load file contents & create
        }
        
        /**
         * <summary> Gets the path of the file including the file </summary>
         */
        public string Path
        {
            get => _path;
        }
        
        /**
         * <summary> Gets the name of the file </summary>
         */
        public string FileName
        {
            get => _filename;
        }
        
        /**
         * <summary> Gets the drive that the file is inside </summary>
         */
        public string Drive
        {
            get => _drive;
            set => _drive = value;
        }
        
        /**
         * <summary> Gets the contents of the file </summary>
         */
        public List<string> Contents
        {
            get => _contents;
            set => _contents = value;
        }

        private void CreateFile()
        {
            // TODO: Create File
        }
    }
}