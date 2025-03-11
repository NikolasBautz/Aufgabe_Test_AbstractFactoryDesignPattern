using Test.Model.Implementations;
using Test.Model.Interfaces;

namespace Test.Model.Factory
{
    class OperationFactory
    {
        ///public string GetFilter() => "blaslaksla";

        public IFileOperation GetOperation(string filename)
        {
            if (filename.EndsWith(".json"))
            {
                return new FileOperationJSON();
            }
            else if (filename.EndsWith(".csv"))
            {
                return new FileOperationCSV();
            }
            else if (filename.EndsWith(".xml"))
            {
                return new FileOperationXML();
            }
            else
            {
                return null!;
            }
        }
    }
}
