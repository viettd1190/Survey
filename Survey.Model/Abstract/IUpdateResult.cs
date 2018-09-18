using System.Collections.Generic;

namespace Survey.Model.Abstract
{
    public interface IUpdateResult
    {
        int State { get; set; }

        string KeyLanguage { get; set; }

        List<string> KeyLanguages { get; set; }

        object Value { get; set; }
    }
}
