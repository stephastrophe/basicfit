

class CountYourWords
{
    //normally I would prefer to use a pathname instead of a Streamreader/writer output, so that the caller does not need to rely on those methods, But I do not know the file structure of the entire project so I rely on SR and SW for now
    public static void CountWords(StreamReader input, StreamWriter? output) //if you want to write to console, output should be null
    {

        var wordCount = ReadWords.readLinesIntoDictionary(input);
        wordCount = sortDictionary.sortStringIntAZ(wordCount);
        int total = 0;
        foreach (KeyValuePair<string, int> word in wordCount)
        {
            total += word.Value;
        }
        PrintYourWords.PrintWords(output, total, wordCount);
        
        
    }

    
}

class PrintYourWords{
    public static void PrintWords (StreamWriter? output, int total, Dictionary<string,int> wordCount){ 
        if (output == null) Console.WriteLine("Number of words: {0}\n", total);
        else output.WriteLine("Number of words: {0}\n", total);
        foreach (KeyValuePair<string, int> word in wordCount)
        {
            if (output == null) Console.WriteLine("{0} {1}", word.Key, word.Value);
            else output.WriteLine("{0} {1}", word.Key, word.Value);
        }
    }
}

class ReadWords{
    public static Dictionary<string, int> readLinesIntoDictionary(StreamReader sr){
        var wordCount = new Dictionary<string, int>();
        var line = sr.ReadLine();
        while (line != null)
        {
            string filteredLine = filterLine(line);
            string[] wordsInLine = filteredLine.Split(" ");
            if (filteredLine != null)
            {
                
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
        return wordCount;
    }

    public static string filterLine(string line){
            string filteredLine = new(line.Where(c => Char.IsLetter(c) || Char.IsWhiteSpace(c)).ToArray());
            filteredLine = filteredLine.ToLower();

            return filteredLine;
    }
}

class sortDictionary{
    public static Dictionary<string, int> sortStringIntAZ(Dictionary<string, int> inputDict) //rigid and inefficient implementation, with more time I would like to expand this to more than A-Z, String-Int and a better sorting algorithm 
    {
        if (inputDict.Count == 0)
        {
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
        inputDict = sortDictionary.sortStringIntAZ(inputDict);
        foreach (KeyValuePair<string, int> pair in inputDict)
        {
            outputDict.Add(pair.Key, pair.Value);
        }
        ;
        return outputDict;
    }
}