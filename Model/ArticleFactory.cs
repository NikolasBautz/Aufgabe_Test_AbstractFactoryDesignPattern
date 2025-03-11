using System.Collections.ObjectModel;
using Test.Model;

namespace Test.Factory
{
    public class ArticleFactory
    {
        /// <summary>
        /// hier wird ein Artikel-Objekt erstellt und zurückgegeben also eine Instanz der Artikel-Klasse
        /// </summary>
        /// <param name="name"></param>
        /// <param name="articlenumber"></param>
        /// <param name="stück"></param>
        /// <returns></returns>
        public Article CreateArticle(string name, string articelnumber, string stück) 
        {
            return new Article
            {
                name = name,
                articlenumber = articelnumber,
                piece = stück
            };
        }

        public ObservableCollection<Article> CreateInitialArticle()
        {
            var articleListe = new ObservableCollection<Article>
            {
                CreateArticle("article1", "0001", "5"),
                CreateArticle("article2", "0002", "10"),
                CreateArticle("article3", "0003", "15")
            };
            return articleListe;
        }
    }
}
