namespace DotNetConf2016
{
    using System;
    using System.Linq;

    public static class StringExtensions
    {
        public static readonly string[] SweetWords = new string[] { "sweetie", "lovely", "pretty", "honey", "darling", "graceful", "delicious", "dear", "nice", "adorable", "sweet" };

        public static bool IsSweetie(this string source)
        {
            return SweetWords.Any(s => source.ToLowerInvariant().Contains(s.ToLowerInvariant()));
        }

        public static string Sweeten(this string source)
        {
            var r = new Random();
            var word = SweetWords[r.Next(0, SweetWords.Length - 1)];
            var sweetWord = string.Format("{0}{1}", char.IsUpper(source[0]) ? char.ToUpperInvariant(word[0]) : word[0], word.Substring(1));
            return string.Format("{0}{1}{2}", sweetWord, char.ToUpperInvariant(source[0]), source.Substring(1));
        }
    }
}
