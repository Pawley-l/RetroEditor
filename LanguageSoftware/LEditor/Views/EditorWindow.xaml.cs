using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using LEditor.ViewModels;

namespace LEditor.Views
{
    public partial class EditorWindow : Window
    {
        private static GraphicsWindow _graphicsWindow;
        private static EditorViewModel _currentContext;

        public EditorWindow()
        {
            InitializeComponent();
            _currentContext = (EditorViewModel)this.DataContext;
            _graphicsWindow = new GraphicsWindow(_currentContext);
            _graphicsWindow.Show();
        }

        private void GraphicsWindowButton_OnClick(object sender, RoutedEventArgs e)
        {
            _currentContext = (EditorViewModel)this.DataContext;
            _graphicsWindow.Close();
            _graphicsWindow = new GraphicsWindow(_currentContext);
            _graphicsWindow.Update();
            _graphicsWindow.Show();
        }
        
        private void text_OnGotFocus(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void RunButton_OnClick(object sender, RoutedEventArgs e)
        {
            Update();
            
        }

        private void Update()
        {
            _currentContext = (EditorViewModel)this.DataContext;
            _graphicsWindow.SetContext(_currentContext);   
            _graphicsWindow.Update();
        }
    }
}