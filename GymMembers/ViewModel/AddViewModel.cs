﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GymMembers.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

// KEIRA: (ErrorWindow.xaml Pop-Up) Add namespaces.
using GymMembers.View;
using Prism.Mvvm;
using Prism.Commands;
using System.Windows.Data;

namespace GymMembers.ViewModel
{
    /// <summary>
    /// The VM for adding users to the list.
    /// </summary>
    public class AddViewModel : ViewModelBase
    {
        /// <summary>
        /// The currently entered first name in the add window.
        /// </summary>
        private string enteredFName;

        /// <summary>
        /// The currently entered last name in the add window.
        /// </summary>
        private string enteredLName;

        /// <summary>
        /// The currently entered email in the add window.
        /// </summary>
        private string enteredEmail;

        /// <summary>
        /// Initializes a new instance of the AddViewModel class.
        /// </summary>
        public AddViewModel()
        {
            SaveCommand = new RelayCommand<IClosable>(SaveMethod);
            // KEIRA: (ErrorWindow.xaml Pop-Up) Attach ShowCommand to ShowMethod to act as an event.
            ShowCommand = new RelayCommand<IClosable>(ShowMethod);
            // KEIRA: (CancelCommand) Attach CancelCommand to CancelMethod to act as an event.
            CancelCommand = new RelayCommand<IClosable>(CancelMethod);
        }

        /// <summary>
        /// The command that triggers saving the filled out member data.
        /// </summary>
        public ICommand SaveCommand { get; private set; }

        /// <summary>
        /// The command that triggers closing the add window.
        /// </summary>
        public ICommand CancelCommand { get; private set; } // REVIEW: Equivalent to "public RelayCommand<IClosable> ExitCommand { get; private set; }" ?

        // KEIRA: (ErrorWindow.xaml Pop-Up) Add ShowCommand.
        public ICommand ShowCommand { get; private set; }

        /// <summary>
        /// Sends a valid member to the Main VM to add to the list, then closes the window.
        /// </summary>
        /// <param name="window">The window to close.</param>
        public void SaveMethod(IClosable window)
        {
            try
            {
                if (window != null)
                {
                    var addViewModelMessage = new MessageMember(EnteredFName, EnteredLName, EnteredEmail, "Add");
                    Messenger.Default.Send(addViewModelMessage); // sends "Add" message to MainViewModel.ReceiveMember(MessageMember m)
                    window.Close();
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Fields must be under 25 characters.", "Entry Error");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Fields cannot be empty.", "Entry Error");
            }
            catch (FormatException)
            {
                MessageBox.Show("Must be a valid e-mail address.", "Entry Error");
            }
        }

        /// <summary>
        /// Closes the window.
        /// </summary>
        /// <param name="window">The window to close.</param>
        /// 
        // KEIRA: (CancelCommand) Add CancelMethod().
        public void CancelMethod(IClosable window)
        {
            if (window != null)
            {
                window.Close();
            }
        }

        // KEIRA: (ErrorWindow.xaml Pop-Up) Add ShowMethod().
        public void ShowMethod(IClosable window) {
            ErrorWindow errorWindow = new ErrorWindow();
            errorWindow.Show();
        }

        /// <summary>
        /// The currently entered first name in the add window.
        /// </summary>
        public string EnteredFName
        {
            get { return enteredFName; }
            set
            {
                enteredFName = value;
                RaisePropertyChanged("EnteredFName");
            }
        }

        /// <summary>
        /// The currently entered last name in the add window.
        /// </summary>
        public string EnteredLName
        {
            get { return enteredLName; }
            set
            {
                enteredLName = value;
                RaisePropertyChanged("EnteredLName");
            }
        }

        /// <summary>
        /// The currently entered e-mail in the add window.
        /// </summary>
        public string EnteredEmail
        {
            get { return enteredEmail; }
            set
            {
                enteredEmail = value;
                RaisePropertyChanged("EnteredEmail");
            }
        }
    }
}
