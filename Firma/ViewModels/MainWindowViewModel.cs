using Firma.Helpers;
using Firma.ViewResources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows;
using Firma.Views;
using Firma.ViewModels;

namespace Firma.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        //Contains collection of command, that appears on side bar and collection of tabs
        #region Komendy menu i paska narzedzi

        /// <summary>
        /// This command will be use in top menu and toolbar
        /// </summary>
        public ICommand ProductsCommand
        {
            get
            {
                return new BaseCommand(() => CreateView(new ProductsViewModel()));
            }
        }
        public ICommand ItemsCommand
        {
            get
            {
                return new BaseCommand(showAllItems);
            }
        }
        public ICommand NewInvoiceCommand
        {
            get
            {
                return new BaseCommand(() => CreateView(new NewInvoiceViewModel()));
            }
        }
        public ICommand InvoicesCommand
        {
            get
            {
                return new BaseCommand(ShowAllInvoices);
            }
        }
        public ICommand NewCustomerCommand
        {
            get
            {
                return new BaseCommand(() => CreateView(new NewCustomerViewModel()));
            }
        }
        public ICommand NewProductCommand
        {
            get
            {
                return new BaseCommand(() => CreateView(new NewProductViewModel()));
            }
        }
        public ICommand UserProfileCommand
        {
            get
            {
                return new BaseCommand(() => CreateView(new UserPanelViewModel()));
            }
        }
        #endregion

        public MainWindowViewModel()
        {
            Commands = new ReadOnlyCollection<CommandViewModel>(CreateCommands());
            Workspaces = new ObservableCollection<WorkspaceViewModel>();
            Workspaces.CollectionChanged += OnWorkspacesChanged;
        }

        #region Buttons in side bar

        /// <summary>
        /// This is collection of commands in side bar
        /// </summary>
        public ReadOnlyCollection<CommandViewModel> Commands { get; set; }

        private List<CommandViewModel> CreateCommands()
        {
            var roundButtonStyle = (Style)Application.Current.Resources["RoundButtonStyle"];

            return new List<CommandViewModel>
    {
        new CommandViewModel("User Profile", new BaseCommand(showAllItems)) { ButtonStyle = roundButtonStyle },
        new CommandViewModel("Products", new BaseCommand(() => CreateView(new ProductsViewModel()))) { ButtonStyle = roundButtonStyle },
        new CommandViewModel("Invoice", new BaseCommand(() => CreateView(new NewInvoiceViewModel()))) { ButtonStyle = roundButtonStyle },
        new CommandViewModel("Invoices", new BaseCommand(ShowAllInvoices)) { ButtonStyle = roundButtonStyle },
        new CommandViewModel("Add New Product", new BaseCommand(() => CreateView(new NewProductViewModel()))) { ButtonStyle = roundButtonStyle },
        new CommandViewModel("Add New Customer", new BaseCommand(() => CreateView(new NewCustomerViewModel()))) { ButtonStyle = roundButtonStyle }
    };
        }





        #endregion

        #region Zakładki
        public ObservableCollection<WorkspaceViewModel> Workspaces { get; set; }
        private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += OnWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= OnWorkspaceRequestClose;
        }
        private void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            //workspace.Dispos();
            Workspaces.Remove(workspace);
        }
        #endregion

        #region Funkcje pomocnicze
        private void CreateView(WorkspaceViewModel workspace)
        {
            Workspaces.Add(workspace);
            SetActiveWorkspace(workspace);
        }
        private void ShowAllInvoices()
        {
            AllInvoicesViewModel workspace = Workspaces.FirstOrDefault(vm => vm is AllInvoicesViewModel) as AllInvoicesViewModel;
            if (workspace == null)
            {
                workspace = new AllInvoicesViewModel();
                Workspaces.Add(workspace);
            }
            SetActiveWorkspace(workspace);
        }
        private void showNewProducts()
        {
            NewProductViewModel workspace = Workspaces.FirstOrDefault(vm => vm is NewProductViewModel) as NewProductViewModel;
            if (workspace == null)
            {
                workspace = new NewProductViewModel();
                Workspaces.Add(workspace);
            }
            SetActiveWorkspace(workspace);
        }
        /// <summary>
        /// This is method that creates new items tab
        /// Each time it checks if such tab is opened. If yes - opens it, if no - creates new one
        /// </summary>
        private void showAllItems()
        {
            UserPanelViewModel workspace = Workspaces.FirstOrDefault(vm => vm is UserPanelViewModel) as UserPanelViewModel;
            if (workspace == null)
            {
                workspace = new UserPanelViewModel();
                Workspaces.Add(workspace);
            }
            SetActiveWorkspace(workspace);
        }

        private void showNewProduct()
        {
            NewProductViewModel workspace = Workspaces.FirstOrDefault(vm => vm is NewProductViewModel) as NewProductViewModel;
            if (workspace == null)
            {
                workspace = new NewProductViewModel();
                Workspaces.Add(workspace);
            }
            SetActiveWorkspace(workspace);
        }

        private void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }

        #endregion
    }
}

