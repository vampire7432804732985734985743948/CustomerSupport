using System.Text;

namespace CustomerSupport.Data.Json
{
    public class JsonDataReceiver
    {
        private readonly string _fileName = "SystemConstantsContainer.json";
        private readonly string _filePath;
        private readonly string _folderName = "SystemConstants";
        private readonly string _folderPath;

        public JsonDataReceiver()
        {
            _folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _folderName);
            _filePath = Path.Combine(_folderPath, _fileName);
        }
        public string GetData()
        {
            if (_filePath == null || !File.Exists(_filePath)) 
            {
                throw new ArgumentException(new StringBuilder("Invalid _filePath or the is no such file in specific directory").ToString());
            }

            string receivedData = File.ReadAllText(_filePath);

            return receivedData ?? throw new ArgumentException(new StringBuilder("No data over here").ToString());
        }
    }
}
