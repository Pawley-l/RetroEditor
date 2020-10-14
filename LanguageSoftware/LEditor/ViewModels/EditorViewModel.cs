using LEditor.Models;

namespace LEditor.ViewModels
{
    public class EditorViewModel
    {
        private FileModel _file;
        public FileViewModel File { get; set; }

        public EditorViewModel()
        {
            _file = new FileModel();
            File = new FileViewModel(_file);
        }
    }
}