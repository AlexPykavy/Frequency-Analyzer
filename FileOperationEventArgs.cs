using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrequencyAnalyzer
{
    public class FileOperationEventArgs : EventArgs
    {
        public string OldFilename { get; set; }
        public string SelectedFilename { get; set; }
    }
}
