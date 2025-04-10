class CountYourWords
{
    public static void PrintWordCount(StreamReader sr)
    {
        
        var wordCount = new Dictionary<string, int>();
        var line = sr.ReadLine();
        while (line != null)
        {
            string filteredLine = new(line.Where(c => Char.IsLetter(c) || Char.IsWhiteSpace(c)).ToArray());
            if (filteredLine != null)
            {
                filteredLine = filteredLine.ToLower();
                string[] wordsInLine = filteredLine.Split(" ");
                foreach (string word in wordsInLine)
                {
                    if (word != "")
                    {
                        wordCount.TryGetValue(word, out int count);
                        wordCount[word] = count + 1;
                    }
                }
            }
            line = sr.ReadLine();
        }
        wordCount = sortDictionary(wordCount);
        int total = 0;
        foreach (KeyValuePair<string, int> word in wordCount)
        {
            total += word.Value;
        }
        Console.WriteLine("Number of words: {0}\n", total);
        foreach (KeyValuePair<string, int> word in wordCount)
        {
            Console.WriteLine("{0} {1}", word.Key, word.Value);
        }
    }

    private static Dictionary<string, int> sortDictionary(Dictionary<string, int> inputDict)
    {
        if (inputDict.Count == 0){
            return inputDict;
        }
        string lowest = "";
        Dictionary<string, int> outputDict = new Dictionary<string, int>();
        foreach (KeyValuePair<string, int> word in inputDict)
        {
            if (string.Compare(word.Key, lowest) < 0 || lowest == "")
            {
                lowest = word.Key;
            }
        }
        if (lowest != "")
        {
            outputDict.Add(lowest, inputDict[lowest]);
            inputDict.Remove(lowest);
        }
        inputDict = sortDictionary(inputDict);
        foreach (KeyValuePair<string, int> pair in inputDict)
        {
            outputDict.Add(pair.Key, pair.Value);
        }
        ;
        return outputDict;
    }
}