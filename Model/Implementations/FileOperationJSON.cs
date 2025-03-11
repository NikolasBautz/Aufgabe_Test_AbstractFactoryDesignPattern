using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows.Shapes;
using Test.Model.Interfaces;

namespace Test.Model.Implementations
{
    class FileOperationJSON : IFileOperation
    {
        public ObservableCollection<Article> Open(string filename)
        {
            ObservableCollection<Article> articleListe;
            string json = File.ReadAllText(filename);

            /// Deserialization
            articleListe = JsonSerializer.Deserialize<ObservableCollection<Article>>(json)!;     
            Debug.WriteLine("Now loading the format JSON!");
            return articleListe!;
        }

        public bool Save(string filename, IList<Article> articleList)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            string json = JsonSerializer.Serialize(articleList, options);
            File.WriteAllText(filename, json);

            Debug.WriteLine("we are now safing the format JSON");
            return true;
        }
    }
}
