using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SynTextDataManager.Library.Internal.DataAccess;
using SynTextDataManager.Library.Models;

namespace SynTextDataManager.Library.DataAccess
{
    public class TextData
    {
        private readonly ISqlDataAccess sqlDataAccess;

        public TextData(ISqlDataAccess sqlDataAccess)
        {
            this.sqlDataAccess = sqlDataAccess;
        }

        public List<WordModel> GetComplexWords()
        {
            var output = sqlDataAccess.LoadData<WordModel, dynamic>("spComplexWordLookup", new { }, "DefaultConnection");
            return output;
        }
    }
}
