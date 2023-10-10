namespace YugiohGame.CardLibrary
{
    public class DataSetProcessor
    {
        private List<string[]> _cards;

        public List<string[]> Cards
        { get { return _cards; } }

        public List<string[]> ReadMonstersFromCsv(string filePath)
        {
            _cards = new List<string[]>();
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');
                        _cards.Add(values);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading CSV file: " + ex.Message);
            }
            return _cards;
        }
    }
}