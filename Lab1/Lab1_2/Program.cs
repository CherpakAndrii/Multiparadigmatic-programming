using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

InpFilename:
    Console.Write("Введiть назву файлу: ");
    var path = Console.ReadLine();
    if (!string.IsNullOrEmpty(path) && File.Exists(path) && path.Length>4 && path.Substring(path.Length-4)==".txt") goto FileIsSelected;
    Console.WriteLine("Некоректний ввiд!");
    goto InpFilename;

FileIsSelected:
int lineCtr = 0;
StreamReader sr = new StreamReader(path, Encoding.Default);
Dictionary<string, List<int>> dictionary = new();
List<string> useless = new() { "[", "]", ",", ".", " - ", ":", ";", "?", "!", "--", " the ", " a ", " an ", " for ",
    " on ", " in ", " at ", " to ", " and ", " or ",  " as " };

NextLine:
    if (sr.EndOfStream) goto EndOfFile;
    lineCtr++;
    string line = sr.ReadLine();
    if (string.IsNullOrEmpty(line)) goto NextLine;
    line = " "+line.ToLower();
    int ctr = 0;

    RemoveUseless:
        line = line.Replace(useless[ctr], " ");
        ctr++;
        if (ctr<useless.Count) goto RemoveUseless;

    var words = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    ctr = 0;

    ForEachWord:
        if (!dictionary.ContainsKey(words[ctr])) dictionary.Add(words[ctr], new List<int>(){(int)Math.Ceiling(lineCtr/45.0)});
        else if (!dictionary[words[ctr]].Contains((int)Math.Ceiling(lineCtr/45.0))) dictionary[words[ctr]].Add((int)Math.Ceiling(lineCtr/45.0));
        ctr++;
        if (ctr<words.Length) goto ForEachWord;

    goto NextLine;

EndOfFile:
    if (dictionary.Count == 0)
    {
        Console.WriteLine("Файл порожнiй!");
        Environment.Exit(0);
    }
    List<KeyValuePair<string, List<int>>> vocabularyIndexingList = new(dictionary);
    vocabularyIndexingList.Sort((p1, p2)=>String.Compare(p1.Key, p2.Key, StringComparison.Ordinal));
    ctr = 0;

int pageCtr;

OutputWords:
    Console.Write(vocabularyIndexingList[ctr].Key+" - ");
    pageCtr = 0;

    PrintPage:
        Console.Write(vocabularyIndexingList[ctr].Value[pageCtr]);
        pageCtr++;
        if (pageCtr < vocabularyIndexingList[ctr].Value.Count)
        {
            Console.Write(", ");
            goto PrintPage;
        }

    Console.WriteLine();
    ctr++;
    if (ctr<vocabularyIndexingList.Count) goto OutputWords;