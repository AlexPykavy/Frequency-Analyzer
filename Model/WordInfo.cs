using System;

namespace FrequencyAnalyzer.Model
{
    [Serializable]
    public class WordInfo
    {
        public string Word { get; set; }

        public int Amount { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;

            if (obj == null)
                return false;

            WordInfo wordInfo = obj as WordInfo;

            if (wordInfo == null)
                return false;

            return Word.Equals(wordInfo.Word);
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + (Word == null ? 0 : Word.GetHashCode());
            return hash;
        }
    }
}
