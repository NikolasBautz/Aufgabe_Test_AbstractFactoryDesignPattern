using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Shapes;
using Test.Model.Interfaces;

namespace Test.Model.Implementations
{
    internal class FileOperationCSV : IFileOperation
    {
        public ObservableCollection<Article> Open(string filename)
        {
            ObservableCollection<Article> articleListe;

            articleListe = DeserializeFromCsv(filename);
            Debug.WriteLine("Now loading the format CSV!");
            return articleListe;
        }

        /// <summary>
        /// manuel function deserialization because there is no Library
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static ObservableCollection<Article> DeserializeFromCsv(string filePath)
        {
            var articleCollection = new ObservableCollection<Article>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    reader.ReadLine();

                    // read dataline
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // separate
                        var values = line.Split(';');

                        // create object and add it
                        var article = new Article
                        {
                            name = values[0],
                            articlenumber = values[1],
                            piece = values[2]
                        };
                        articleCollection.Add(article);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while reading file: {ex.Message}");
            }

            return articleCollection;
        }

        public bool Save(string filename, IList<Article> articleList)
        {
            string csvFilePath = filename;
            SerializeToCsv(articleList, csvFilePath);

            Debug.WriteLine("Now saving the format CSV!");

            return true;
        }

        public static void SerializeToCsv(IList<Article> articleListe, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Schreibe die Kopfzeile
                writer.WriteLine("name;articlenumber;piece");                                       //writing the headline

                // Schreibe die Datenzeilen
                foreach (var article in articleListe)
                {
                    writer.WriteLine($"{article.name};{article.articlenumber};{article.piece}");    //write the single elements , separated
                    //Debug.WriteLine($"{artikel.Name},{artikel.Artikelnummer},{artikel.Stück}");
                }
            }
        }

    }
}
