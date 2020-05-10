namespace Bdv.ScanData.ViewModel
{
    public class EditControlViewModel
    {
        public int Index { get; set; }
        public string Class { get; set; }

        public string DisplayName => $"{Class}: {Index:D2}";
    }
}
