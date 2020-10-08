using System.Collections.Generic;

namespace LData.FileSystemItems
{
    public class Drive : IFileSystemItem
    {
        private string _id;
        private List<IFileSystemItem> _contents;

        internal Drive(string id)
        {
            _id = id;
        }
        
        /**
         * <summary> Gets the drive ID </summary>
         */
        public string Id
        {
            get => _id;
        }

        /**
         * <summary> Gets the contents of the file </summary>
         */
        public List<IFileSystemItem> Contents
        {
            get => _contents;
        }
        
        /**
         * Gets the drives contents for 1 level
         */
        private List<IFileSystemItem> GetDriveContents()
        {
            // TODO: Get the drives contents
            return null;
        }
    }
}