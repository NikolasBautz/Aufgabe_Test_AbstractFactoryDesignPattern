using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Test.Factory;
using Test.Model;
using Test.Model.Factory;
using Test.Model.Interfaces;
using Test.MVVM;

namespace Test.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// automatic detection when something is changed, and changes it in the GUI
        /// ObservableCollection<Artikel> is a collection of Item objects that
        /// Sends notifications to the user interface when the collection is updated
        /// (items are added, removed, or updated). This ensures that
        /// the user interface is updated automatically.
        /// </summary>
        public ObservableCollection<Article> Items { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MainWindowViewModel()
        {
            Items = new ObservableCollection<Article>();

            /// call the ArticleFactory
            ArticleFactory articleFactory = new ArticleFactory();            
            Items = articleFactory.CreateInitialArticle();


            AddingButtonCommand = new RelayCommand(AddButtonClick);
            SelectingButtonCommand = new RelayCommand(SelectButtonClick);
            EditingButtonCommand = new RelayCommand(EditButtonClick);
            DeletingButtonCommand = new RelayCommand(DeleteButtonClick);
            LoadingButtonCommand = new RelayCommand(OpenButtonClick);
            SavingButtonCommand = new RelayCommand(SaveButtonClick);

        }

        private Article selectedItem;

        /// <summary>
        /// Update-Method bzw selectedItem to pull from DataGrid
        /// </summary>
        public Article SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(selectedItem));
            }
        }

        private string textBoxname;

        /// <summary>
        /// Textbox-Binding
        /// </summary>
        public string TextBoxname
        {
            get { return textBoxname; }
            set
            {
                textBoxname = value;
                /// important to update the GUI
                OnPropertyChanged();        
            }
        }
        private string textBoxarticlenumber;

        public string TextBoxarticlenumber
        {
            get { return textBoxarticlenumber; }
            set
            {
                textBoxarticlenumber = value;
                OnPropertyChanged();
            }
        }
        private string textBoxpiece;

        public string TextBoxpiece
        {
            get { return textBoxpiece; }
            set
            {
                textBoxpiece = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Button-Function with RelayCommand 
        /// </summary>
        public ICommand AddingButtonCommand { get; }

        private void AddButtonClick(object parameter)
        {
            /// added an item with parameters from the text boxes
            Items.Add(new Article                       
            {
                name = textBoxname,
                articlenumber = textBoxarticlenumber,
                piece = textBoxpiece
            });
        }

        public ICommand SelectingButtonCommand { get; }

        private void SelectButtonClick(object parameter)
        {
            /// Only works if the public string TextBox values are set {OnPropertyChanged();}
            if (SelectedItem != null)                   
            {
                TextBoxname = SelectedItem.name!;
                TextBoxarticlenumber = SelectedItem.articlenumber!;
                TextBoxpiece = SelectedItem.piece!;
            }
        }

        public ICommand EditingButtonCommand { get; }

        private void EditButtonClick(object parameter)
        {
            /// Removed the selected item(line) from the list
            Items.Remove(selectedItem);
            /// Added an item with parameters from the text boxes
            Items.Add(new Article                       
            {
                name = textBoxname,
                articlenumber = textBoxarticlenumber,
                piece = textBoxpiece
            });
        }

        public ICommand DeletingButtonCommand { get; }

        private void DeleteButtonClick(object parameter)
        {
            /// Removed the selected item(line) from the list
            Items.Remove(selectedItem);                 
        }

        public ICommand LoadingButtonCommand { get; }

        [STAThread]
        private void OpenButtonClick(object parameter)
        {
            /// Open-Window
            OpenFileDialog fileDialog = new()
            {
                /// opens only file with an ending .csv .xml .json
                Filter = "CSV-Dateien (*.csv)|*.csv|JSON-Dateien (*.json)|*.json|XML-Dateien (*.xml)|*.xml",   
                Title = "Please select file",
                Multiselect = false
            };

            /// trigger if use clicks open
            bool? success = fileDialog.ShowDialog();                                
            if (success == true)
            {                
                /// Input
                IFileOperation operation = new OperationFactory().GetOperation(fileDialog.FileName);
                var articlesList = operation.Open(fileDialog.FileName);

                /// Output on WPF
                if (articlesList != null)
                {
                    Items.Clear();
                    foreach (var article in articlesList)       
                    {
                        Items.Add(article);
                    }
                }
                else
                {
                    MessageBox.Show("File Error!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                /// Output in Debuggingline
                Debug.WriteLine("Done Loading!");
            }
            else
            {
                MessageBox.Show("File not slected!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }                    
        }
                
        public ICommand SavingButtonCommand { get; }

        [STAThread]
        private void SaveButtonClick(object parameter)    
        {
            /// Convert DataGrid to List
            List<Article> ItemsListe = Items.ToList();

            /// Open-Window
            SaveFileDialog savefileDialog = new()
            {
                Filter = "CSV-Dateien (*.csv)|*.csv|JSON-Dateien (*.json)|*.json|XML-Dateien (*.xml)|*.xml", 
                DefaultExt = "csv",                                                                         
                AddExtension = true                                                                         
            };

            /// trigger if use clicks open
            bool? success = savefileDialog.ShowDialog();
            if (success == true)
            {
                /// Input
                IFileOperation operation = new OperationFactory().GetOperation(savefileDialog.FileName);
                operation.Save(savefileDialog.FileName, ItemsListe);

                /// Output in Debuggingline
                Debug.WriteLine("Done Saving!");
            }
            else
            {
                MessageBox.Show("File not saved!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

            ///
    }
}
