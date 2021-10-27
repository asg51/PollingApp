namespace PollingApp.Entities
{
    public class Chosen
    {
        public string ChosenName { get; set; }
        public int Index { get; set; }
        public Chosen(string chosenName, int index)
        {
            ChosenName = chosenName;
            Index = index;
        }
    }
}
