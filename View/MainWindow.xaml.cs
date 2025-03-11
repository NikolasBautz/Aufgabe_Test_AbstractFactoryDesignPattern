using System.Windows;
using Test.ViewModel;

namespace Test.View
{    
    public partial class MainWindow : Window        //Dies ist die Definition der MainWindow-Klasse, die von der Window-Klasse erbt. 
    {                                               //partial bedeutet, dass die Klasse in mehrere Dateien unterteilt sein kann.
        public MainWindow()                         //Dies ist der Konstruktor der MainWindow-Klasse
        {
            InitializeComponent();                                  //Diese Methode wird aufgerufen, um die Benutzeroberfläche zu initialisieren und alle XAML-Komponenten zu laden.
            MainWindowViewModel vm = new MainWindowViewModel();     //Hier wird eine neue Instanz des MainWindowViewModel erstellt. Dies ist die Schicht, die zwischen der View (Benutzeroberfläche) und dem Model vermittelt.
            DataContext = vm;                                       //Der DataContext der MainWindow-Instanz wird auf die erstellte Instanz des MainWindowViewModel gesetzt. Dies ermöglicht Datenbindungen zwischen der Benutzeroberfläche (XAML) und dem ViewModel. 
        }                                                           //Dies ermöglicht Datenbindungen zwischen der Benutzeroberfläche (XAML) und dem ViewModel.

    }
}