using System;
using System.Collections.Generic;

namespace LEditor.Models
{
    public class FileModel : ObservableObject
    {
        private string _filename;
        private string _path;
        private List<string> _contents;

        public FileModel()
        {
            _contents = new List<string>();
        }
        
        public List<string> ContentsArray
        {
            get => _contents;
            set => OnPropertyChanged(ref _contents, value, "Contents");
        }
        
        public string Contents
        {
            get => string.Join(Environment.NewLine, _contents.ToArray());
            set => OnPropertyChanged(ref _contents, new List<string>{value});
        }

        public string Filename
        {
            get => _filename;
            set => OnPropertyChanged(ref _filename, value);
        }
        
        public string Path
        {
            get => _path;
            set => OnPropertyChanged(ref _path, value);
        }
    }
}