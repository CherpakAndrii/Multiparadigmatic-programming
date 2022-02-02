using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

InpFilename:
    Console.Write("Введiть назву файлу: ");
    string path = Console.ReadLine();
    if (!string.IsNullOrEmpty(path) && File.Exists(path) && path.Length>4 && path.Substring(path.Length-4)==".txt") goto FileIsSelected;
    Console.WriteLine("Некоректний ввiд!");
    goto InpFilename;

FileIsSelected:
    string text = " "+new StreamReader(path, Encoding.Default).ReadToEnd();

List<string> symbolsToIgnore = new() { "\r\n", "[", "]", ",", ".", " - ", ":", ";", "?", "!", "--", " the ", " a ", " an ", " for ",
    " on ", " in ", " at ", " to ", " and ", " or ",  " as " };
text = text.ToLower();
int ctr = 0;

RemoveUseless:
    text = text.Replace(symbolsToIgnore[ctr], " ");
    ctr++;
    if (ctr<symbolsToIgnore.Count) goto RemoveUseless;

var words = text.Split(" ", StringSplitOptions.RemoveEmptyEntries);
Dictionary<string, int> dictionary = new();
ctr = 0;

ForEachWord:
    if (dictionary.ContainsKey(words[ctr])) dictionary[words[ctr]] += 1;
    else dictionary.Add(words[ctr], 1);
    ctr++;
    if (ctr<words.Length) goto ForEachWord;

List<KeyValuePair<string, int>> wordCounts = new(dictionary);
wordCounts.Sort((p1, p2)=>p2.Value.CompareTo(p1.Value));
ctr = 0;
InpRequiredWordsNumber:
    Console.Write("Введiть бажану кiлькiсть слiв: ");
    if (!Int32.TryParse(Console.ReadLine(), out var requiredWordsNumber) || requiredWordsNumber < 1)
    {
        Console.WriteLine("Будь ласка, введiть натуральне число!");
        goto InpRequiredWordsNumber;
    }

OutputWords:
    Console.WriteLine(wordCounts[ctr].Key+" - "+wordCounts[ctr].Value);
    ctr++;
    if (ctr<wordCounts.Count && ctr<requiredWordsNumber) goto OutputWords;
