using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Shapes;
using System.Xml.Serialization;
using Test.Model.Interfaces;

namespace Test.Model.Implementations
{
    internal class FileOperationXML : IFileOperation
    {
        public ObservableCollection<Article> Open(string filename)
        {
            ObservableCollection<Article> articleListe;

            XmlSerializer xs = new XmlSerializer(typeof(List<Article>));
            /// necessary to propperly read the pathing
            TextReader textReader = new StreamReader(filename);
            /// Deserialization
            var articleListeListe = (List<Article>)xs.Deserialize(textReader)!;
            /// change from List<Artikel> to ObservableCollection<Artikel>
            articleListe = new ObservableCollection<Article>(articleListeListe!);    

            Debug.WriteLine("Now loading the format XML!");
            return articleListe;
        }

        public bool Save(string filename, IList<Article> articleList)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Article>));
            using (FileStream stream = File.Create(filename))
            {
                /// Serialization
                xs.Serialize(stream, articleList);                       
            }

            Debug.WriteLine("we are now safing the format XML");
            return true;
        }
    }
}
