using Firma.Models.Context;
using Firma.Models.Entities;
using Firma.ViewResources;
using System;
using System.Windows;
using System.Windows.Input;

namespace Firma.ViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }

    public class NewCustomerViewModel : WorkspaceViewModel
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phoneNumber;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged(() => FirstName);
                }
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged(() => LastName);
                }
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(() => Email);
                }
            }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged(() => PhoneNumber);
                }
            }
        }

        private ICommand _addCustomerCommand;

        public ICommand AddCustomerCommand
        {
            get
            {
                if (_addCustomerCommand == null)
                {
                    _addCustomerCommand = new RelayCommand(AddCustomerToDatabase, CanAddCustomer);
                }
                return _addCustomerCommand;
            }
        }

        private void AddCustomerToDatabase()
        {
            try
            {
                using (var dbContext = new FakturyEntities())
                {
                    using (var transaction = dbContext.Database.BeginTransaction())
                    {
                        // Sprawdź, czy użytkownik wprowadził dane
                        if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(PhoneNumber))
                        {
                            MessageBox.Show("Please enter all required information.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        // Utwórz nowego klienta
                        Customer newCustomer = new Customer
                        {
                            FirstName = FirstName,
                            LastName = LastName,
                            Email = Email,
                            PhoneNumber = PhoneNumber
                        };

                        dbContext.Customers.Add(newCustomer);
                        dbContext.SaveChanges();

                        transaction.Commit();
                        MessageBox.Show("New customer successfully added!");
                    }
                }

                ClearTextFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanAddCustomer()
        {
            // opcjonalnie: sprawdzenie warunków, czy można dodać klienta
            return true;
        }

        public void ClearTextFields()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
        }
    }
}
