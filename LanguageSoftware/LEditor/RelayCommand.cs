﻿using System;
using System.Windows.Input;

namespace LEditor
{
    public class RelayCommand : ICommand
    {

        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new NullReferenceException();
            _canExecute = canExecute;
        }
        
        public RelayCommand(Action execute) : this(execute, null)
        {
        }
        
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}