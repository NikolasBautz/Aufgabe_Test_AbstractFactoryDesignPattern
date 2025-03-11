using System.Collections.ObjectModel;

namespace Test.Model.Interfaces
{
    public interface IFileOperation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="articleList"></param>
        /// <returns></returns>
        bool Save(string filename, IList<Article> articleList);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        ObservableCollection<Article> Open(string filename);
    }
}
