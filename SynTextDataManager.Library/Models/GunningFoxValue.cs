using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynTextDataManager.Library.Models
{
    public class GunningFoxValue
    {
        public int Id { get; set; }
        public string ReadingLevelByGrade { get; set; }

        public GunningFoxValue()
        {
        }

        public GunningFoxValue(int Id, string ReadingLevelByGrade)
        {
            this.Id = Id;
            this.ReadingLevelByGrade = ReadingLevelByGrade;
        }
    }
}
