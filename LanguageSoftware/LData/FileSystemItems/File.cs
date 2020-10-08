using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LData.FileSystemItems
{
    public class File : IFileSystemItem
    {
        private string _path;
        private string _drive;

        /**
         * <summary> Loads the file with the file path, optionally can create if doesn't exist </summary>
         * <param name="file_path"> Path to the file </param>
         * <param name="create"> Optional. Create file if doesn't exist </param>
         */
        internal File(string file_path, string drive, bool create = false)
        {
            _path = file_path;
            _drive = drive;
            
            if (create)
                SetFile(null, file_path, true);
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
            get => System.IO.Path.GetFileName(_path);
        }
        
        /**
         * <summary> Gets the drive that the file is inside </summary>
         */
        public string Drive
        {
            get => _drive;
        }
        
        /**
         * <summary> Gets and sets the contents of the file </summary>
         */
        public List<string> Contents
        {
            get => new List<string>(System.IO.File.ReadAllLines(_path));
            set => SetFile(value, _path,false);
        }
        
        /**
         * <summary> Gets the contents of the file in byte format </summary>
         */
        public List<byte> ContentsByte
        {
            get => new List<byte>(System.IO.File.ReadAllBytes(_path));
        }

        /**
         * Write the contents to the file
         */
        private static void SetFile(IEnumerable<string> contents, string path, bool create = false)
        {
            // TODO: Use create parameter and throw exception if file doesnt exist
            System.IO.File.WriteAllLines(path, contents.ToArray());
        }
    }
}