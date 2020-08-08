using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SynTextDataManager.Library.Models;
using SynTextDataManager.Library.DataAccess;
using SynTextDataManager.Library.Exceptions;

namespace SynTextDataManager.Library.Logic
{
    public class TextAnalyser               
    {
        private List<GunningFoxValue> GunningFoxValues;
        private List<WordModel> WordDictionary;
        public TextData TextData { get; }

        public TextAnalyser(TextData TextData)
        {
            this.TextData = TextData;
            this.WordDictionary = TextData.GetAllWords();
            this.GunningFoxValues = TextData.GetGunningFoxValues();
        }

        /// <summary>
        ///  Gets the reading level of the sample text according
        ///  to the Gunning Fox Index
        /// </summary>
        /// <param name="SampleText">Text to Analyze</param>
        /// <returns>The Reading level of the sample text</returns>
        public string GetReadabilityLevel(string sampleText)
        {
            //Separate text into array of individual words
            string[] separatedWordArray = GetSeparatedWordArray(sampleText);

            try
            {
                if (isTextInEnglish(separatedWordArray))
                {
                    //----Split sample text into sentance array 

                    string[] separatedSentanceArray = GetSeparatedSentenceArray(sampleText);

                    //---Get GunningFoxValue
                    int GIndex = GetGunningFoxIndex(separatedWordArray, separatedSentanceArray);

                    GunningFoxValue GFoxValue = GunningFoxValues.Find(x => x.Id == GIndex);


                    if (GIndex >= 5)
                    {
                        return GFoxValue.ReadingLevelByGrade;
                    }
                    else if (GIndex == -2)
                    {
                        throw new DataAccessException("Unable to access database");
                    }
                    else
                    {
                        throw new InvalidSampleException("Invalid Sample");
                    }

                }   
                else
                {
                    throw new InvalidSampleException("English Text Only");
                }
            }
            catch (InvalidSampleException Ex)
            {
                return Ex.Message;
            }
            catch (DataAccessException Ex)
            {
                return Ex.Message;
            }
            
        }

        private string[] GetSeparatedSentenceArray(string sampleText)
        {

            //Matches question mark and exclam characters
            Regex sentanceRegex = new Regex("[?!]");


            char[] sentanceSeparators = { '.' };
            //Replaces matches with period charcter
            string periodOnlyText = sentanceRegex.Replace(sampleText, ".");
            //splits text into sentance array

            string[] separatedSentanceArray = periodOnlyText.Split(sentanceSeparators, StringSplitOptions.RemoveEmptyEntries);

            return separatedSentanceArray;
        }

        private string[] GetSeparatedWordArray(string sampleText)
        {
            //matches all non-letter characters
            Regex wordRegex = new Regex("[^a-zA-Z]");

            char[] wordSeparators = { ' ' };

            //Replaces non-letter characters with space
            string lettersOnlyText = wordRegex.Replace(sampleText, " ");
            //Splits sample text into array
            string[] separatedWordArray = lettersOnlyText.Split(wordSeparators, StringSplitOptions.RemoveEmptyEntries);

            return separatedWordArray;
        }

        private int GetComplexWordCount(string[] WordArray)
        {
            try
            {
                    List<string> FoundCompWords = new List<string>();
                    List<string> UnFoundCompWords = new List<string>();
                    int complexWordCount = 0;
                    foreach (var word in WordArray)
                    {
                        //2058885 

                        //var record = db.words.Where(i => i.word1 == word).FirstOrDefault();
                        var record = WordDictionary.Where(i => i.Word == word).FirstOrDefault();
                        //removes plural
                        if (record == null)
                        {
                            record = WordDictionary.Where(i => i.Word == word.Remove(word.Length - 1, 1)).FirstOrDefault();
                        }

                        if (record == null)
                        {
                            complexWordCount++;
                            UnFoundCompWords.Add(word);
                        }
                        else
                        {
                            if (record.WordRank < 2058885)
                            {
                                complexWordCount++;
                                FoundCompWords.Add(word);
                            }
                        }
                    }

                    return complexWordCount;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return -2;
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

        private int GetGunningFoxIndex(string[] sepWordArray, string[] sepSentanceArray)
        {
            string[] uniqueWordArray = new HashSet<string>(sepWordArray).ToArray();

            double NumOfTotalWords = sepWordArray.Length;

            //Get the number of complex words by retrieving word frequency data from database
            double NumOfComplexWords = GetComplexWordCount(GetRevisedWordArray(uniqueWordArray));

            double NumOfSentences = sepSentanceArray.Length;

            int GFoxIndex;

            //Handling this exception could be better
            if (NumOfComplexWords == -2)
            {
                GFoxIndex = -2;
            }
            else
            {
                GFoxIndex = CalculateGunningFoxIndex(NumOfSentences, NumOfTotalWords, NumOfComplexWords);
            }

            if (GFoxIndex < 6 && GFoxIndex > 0)
            {
                GFoxIndex = 5;
            }

            if (GFoxIndex > 17)
            {
                GFoxIndex = 18;
            }


            return GFoxIndex;
        }

        private int CalculateGunningFoxIndex(double NumOfSentences, double NumOfTotalWords, double NumOfComplexWords)
        {
            try
            {
                return (int)(0.4 * (NumOfTotalWords / NumOfSentences + 100 * (NumOfComplexWords / NumOfTotalWords)));
            }
            catch (DivideByZeroException)
            {
                return -1;
            }
        }

        private bool isTextInEnglish(string[] text)
        {
            bool outcome = false;

            try
            {
                List<string> foundWords = new List<string>();

                    foreach (var word in text)
                    {
                        var record = WordDictionary.Where(i => i.Word == word).FirstOrDefault();
                        if (record != null)
                        {
                            foundWords.Add(word);
                        }
                    }

                double foundCount = foundWords.Count;
                double textCount = text.Length;

                double portion = foundCount / textCount;


                if (portion > .50)
                {
                    outcome = true;
                }
            }
            catch (DivideByZeroException)
            {
                outcome = false;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                string trace = ex.StackTrace;
            }


            return outcome;
        }
    }
}
