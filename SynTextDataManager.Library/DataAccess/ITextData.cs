using SynTextDataManager.Library.Models;
using System.Collections.Generic;

namespace SynTextDataManager.Library.DataAccess
{
    public interface ITextData
    {
        List<WordModel> GetAllWords();
        List<WordModel> GetComplexWords();
    }
}