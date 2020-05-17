using System.Collections.Generic;

namespace Bdv.ScanData.Services
{
    public interface IWindowService
    {
        IEnumerable<string> GetWindows();
        IEnumerable<string> GetControls(string windowTitle, string controlClass);
        void SetText(string windowTitle, string controlClass, int controlIndex, string text);
        void SetText(string windowTitle, string[] controlClasses, int[] controlIndexes, string[] texts);
        string GetText(string windowTitle, string controlClass, int controlIndex);
    }
}
