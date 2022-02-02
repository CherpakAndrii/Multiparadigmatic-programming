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
    string text = new StreamReader(path, Encoding.Default).ReadToEnd();

List<string> useless1 = new() { "The ", "For ", "On ", "In ", "At ", "To " };
List<string> useless2 = new() { ", ", ". ", " - ", ": ", "\r\n", " the ", " for ", " on ", " in ", " at ", " to " };
int ctr = 0;

RemoveUseless1:
    text = text.Replace(useless1[ctr], "");
    ctr++;
    if (ctr<useless1.Count) goto RemoveUseless1;

text = text.ToLower();
ctr = 0;

RemoveUseless2:
    text = text.Replace(useless2[ctr], " ");
    ctr++;
    if (ctr<useless2.Count) goto RemoveUseless2;

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
