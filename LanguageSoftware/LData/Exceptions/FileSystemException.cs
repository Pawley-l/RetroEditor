namespace LData.Exceptions
{
    /**
     * Exception for anything related to the filesystem. Exists to be cross platform.
     */
    public class FileSystemException
    {
        public string Error;
        
        public FileSystemException(string error)
        {
            Error = error;
        }
    }
}