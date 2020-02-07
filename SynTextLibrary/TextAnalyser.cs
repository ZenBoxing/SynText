using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataAccess;

namespace SynTextLibrary
{
    public class TextAnalyser
    {
        //public Dictionary<int, string> GunningFoxValues = new Dictionary<int, string>();
        private List<GunningFoxValue> GunningFoxValues = new List<GunningFoxValue>();

        public TextAnalyser()
        {
            GunningFoxValues.Add(new GunningFoxValue(6, "Sixth Grade"));
            GunningFoxValues.Add(new GunningFoxValue(7, "Seventh Grade"));
            GunningFoxValues.Add(new GunningFoxValue(8, "Eigth Grade"));
            GunningFoxValues.Add(new GunningFoxValue(9, "High School Freshman"));
            GunningFoxValues.Add(new GunningFoxValue(10, "High School Sophmore"));
            GunningFoxValues.Add(new GunningFoxValue(11, "High School Junior"));
            GunningFoxValues.Add(new GunningFoxValue(12, "High School Senior"));
            GunningFoxValues.Add(new GunningFoxValue(13, "College Freshman"));
            GunningFoxValues.Add(new GunningFoxValue(14, "College Sophmore"));
            GunningFoxValues.Add(new GunningFoxValue(15, "College Junior"));
            GunningFoxValues.Add(new GunningFoxValue(16, "College Senior"));
            GunningFoxValues.Add(new GunningFoxValue(17, "College Graduate"));
        }

        /// <summary>
        ///  Gets the reading level of the sample text according
        ///  to the Gunning Fox Index
        /// </summary>
        /// <param name="SampleText">Text to Analyze</param>
        /// <returns>The Reading level of the sample text</returns>
        public string GetReadabilityLevel(string SampleText)
        {
            //----Split sample text into word array

            //matches all non-letter characters
            Regex wordRegex = new Regex("[^a-zA-Z0-9 -]");


            char[] wordSeparators = { ' ' };

            //Replaces non-letter characters with space
            string lettersOnlyText = wordRegex.Replace(SampleText, " ");
            //Splits sample text into array
            string[] separatedWordArray = lettersOnlyText.Split(wordSeparators, StringSplitOptions.RemoveEmptyEntries);
            //Remove duplicate words from array
            string[] uniqueWordArray = new HashSet<string>(separatedWordArray).ToArray();

            //----Split sample text into sentance array 

            //Matches question mark and exclam characters
            Regex sentanceRegex = new Regex("[?!]");


            char[] sentanceSeparators = { '.' };
            //Replaces matches with period charcter
            string periodOnlyText = sentanceRegex.Replace(SampleText, ".");
            //splits text into sentance array
            string[] separatedSentanceArray = periodOnlyText.Split(sentanceSeparators, StringSplitOptions.RemoveEmptyEntries);

            //---

            int NumOfTotalWords = separatedWordArray.Length;
            //Get the number of complex words by retrieve word frequency data from database
            int NumOfComplexWords = GetComplexWordCount(GetRevisedWordArray(uniqueWordArray));
            int NumOfSentences = separatedSentanceArray.Length;

            int GIndex = GetGunningFoxIndex(NumOfSentences, NumOfTotalWords, NumOfComplexWords);

            GunningFoxValue GFoxValue = GunningFoxValues.Find(x => x.FoxIndex == GIndex);

            if (NumOfComplexWords == -1)
            {
                return "Error";
            }
            else
            {
                return GFoxValue.ReadingLevelByGrade;
            }
        }

        private int GetComplexWordCount(string[] WordArray)
        {
            try
            {
                using (SynTextDBEntities db = new SynTextDBEntities())
                {
                    List<string> FoundCompWords = new List<string>();
                    List<string> UnFoundCompWords = new List<string>();
                    int complexWordCount = 0;
                    foreach (var word in WordArray)
                    {
                        //2058885 

                        var record = db.words.Where(i => i.word1 == word).FirstOrDefault();

                        //removes plural
                        if (record == null)
                        {
                            record = db.words.Where(i => i.word1 == word.Remove((word.Length - 1), 1)).FirstOrDefault();
                        }

                        if (record == null)
                        {
                            complexWordCount++;
                            UnFoundCompWords.Add(word);
                        }
                        else
                        {
                            if (record.wordrank < 2058885)
                            {
                                complexWordCount++;
                                FoundCompWords.Add(word);
                            }
                        }
                    }
                    return complexWordCount;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return -1;
            }
        }

        //Removes proper nouns and single letter words from word array
        private string[] GetRevisedWordArray(string[] WordArray)
        {
            List<string> RevisedWordArray = new List<string>();

            Regex RegEx = new Regex("[A-Z]\\w+");

            foreach (var word in WordArray)
            {
                if (!RegEx.IsMatch(word) && word.Length > 1)
                {
                    RevisedWordArray.Add(word);
                }
            }
            return RevisedWordArray.ToArray();
        }

        public static int GetGunningFoxIndex(int NumOfSentences, int NumOfTotalWords, int NumOfComplexWords)
        {
            return (int)(0.4 * ((NumOfTotalWords / NumOfSentences) + 100 * (NumOfComplexWords / NumOfTotalWords)));
        }
    }
}
