using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SynTextDataManager.Library.Internal.DataAccess;
using SynTextDataManager.Library.Models;


namespace SynTextDataManager.Library.DataAccess
{
    public class TextData : ITextData
    {
        private readonly SqlDataAccess sqlDataAccess;

        public TextData(SqlDataAccess sqlDataAccess)
        {
            this.sqlDataAccess = sqlDataAccess;
        }

        public List<WordModel> GetComplexWords()
        {
            var output = sqlDataAccess.LoadData<WordModel, dynamic>("SynTextDB.dbo.spComplexWordLookup", new { }, "SynTextDatabase");
            return output;
        }

        public List<WordModel> GetAllWords()
        {
            var output = sqlDataAccess.LoadData<WordModel, dynamic>("SynTextDB.dbo.GetWords", new { }, "SynTextDatabase");
            return output;
        }
    }
}       
                