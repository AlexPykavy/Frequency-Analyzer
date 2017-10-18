using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace FrequencyAnalyzer.Model
{
    [Serializable]
    public class Vocabulary
    {
        private ISet<WordInfo> wordSet;

        public int WordCount
        {
            get => wordSet.Count;
        }

        public IEnumerable<WordInfo> Words
        {
            get => wordSet;
            set
            {
                wordSet = new HashSet<WordInfo>(value);
            }
        }

        public Vocabulary(ISet<WordInfo> wordSet)
        {
            this.wordSet = wordSet;
        }

        public Vocabulary() : this (new HashSet<WordInfo>()) { }

        public void LoadFromFile(String path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line = sr.ReadLine();

                while (line != null)
                {
                    foreach (string word in Regex.Split(line, @"\W+|\d+"))
                    {
                        if (!string.IsNullOrWhiteSpace(word))
                            AddWord(word.ToLower());
                    }

                    line = sr.ReadLine();
                }
            }
        }

        public void ClearWords()
        {
            wordSet.Clear();
        }

        private void AddWord(String wordValue)
        {
            WordInfo word = wordSet.FirstOrDefault(w => w.Word == wordValue);

            if (word == null)
            {
                wordSet.Add(new WordInfo { Word = wordValue, Amount = 1 });
            }
            else
            {
                word.Amount = word.Amount + 1;
            }
        }
    }
}
