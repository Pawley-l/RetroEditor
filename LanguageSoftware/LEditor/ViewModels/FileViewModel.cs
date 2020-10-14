using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows.Input;
using LData;
using LEditor.Models;
using Microsoft.Win32;

namespace LEditor.ViewModels
{
    public class FileViewModel
    {
        public ICommand NewFileCommand { get; }
        public ICommand SaveFileCommand { get; }
        public ICommand OpenFileCommand { get; }

        public FileViewModel(FileModel fileModel)
        {
            FileModel = fileModel;
            
            NewFileCommand = new RelayCommand(NewFile);
            SaveFileCommand = new RelayCommand(SaveFile);
            OpenFileCommand = new RelayCommand(OpenFile);
        }

        public FileModel FileModel { get; private set; }

        public void NewFile()
        {
            FileModel.Filename = "";
            FileModel.Path = "";
            FileModel.ContentsArray = new List<string>();
        }
        
        public void SaveFile()
        {
            var save = new SaveFileDialog {Filter = "Language File (*.ll)|*.ll"};
            if (save.ShowDialog() == true)
                FileSystem.SetFileContents(save.FileName, FileModel.ContentsArray, true);
        }
        
        public void OpenFile()
        {
            var open = new OpenFileDialog();
            if (open.ShowDialog() != true) return;
            FileModel.ContentsArray = FileSystem.GetFileContents(open.FileName);
            FileModel.Path = open.FileName;
            FileModel.Filename = open.SafeFileName;
        }
    }
}