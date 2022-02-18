using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

InpFilename:
    Console.Write("Введiть назву файлу: ");
    string path = Console.ReadLine();
    if (path != null && path != "" && File.Exists(path) && path.Length>4 
        && path[^1]=='t' && path[^3]=='t' && path[^2]=='x' && path[^4]=='.') goto FileIsSelected;
    Console.Write("Некоректний ввiд!\n");
    goto InpFilename;

FileIsSelected:
    string[] wordsToIgnore = {"--", "the", "a", "an", "for", "on", "in", "at", "to", "and", "or", "as" };
    StreamReader sr = new StreamReader(path, Encoding.Default);
    Dictionary<string, int> dictionary = new();
    int ch;
    string word;

NextWord:
    word = "";
    NextChar:
    ch = sr.Read();
    if (ch == -1 || ch == 13 || ch == 9 || ch == 10 || ch == 32 || ch == 160) goto EndWord;
    if (ch is > 32 and < 65 or > 90 and < 96 or > 122 and < 128 || ch == 150 || ch == 151) goto NextChar;
    word += (char) (ch is > 64 and < 91 or > 191 and < 224 ? ch+32 : ch);
    goto NextChar;
EndWord:
    if (word != "" && !Array.Exists(wordsToIgnore, element => element == word))
    {
        if (dictionary.ContainsKey(word)) dictionary[word] += 1;
        else dictionary.Add(word, 1);
    }
    if (ch!=-1) goto NextWord;

    List<KeyValuePair<string, int>> wordCounts = new(dictionary);
    wordCounts.Sort((p1, p2)=>p2.Value.CompareTo(p1.Value));

InpRequiredWordsNumber:
    Console.Write("Введiть бажану кiлькiсть слiв: ");
    string requiredWordsNumberS = Console.ReadLine();
    int ctr = 0, requiredWordsNumber = 0;
    if (requiredWordsNumberS == null || requiredWordsNumberS == "") goto InpError;

NextDigit:
    requiredWordsNumber *= 10;
    if (requiredWordsNumberS[ctr] is <'0' or > '9') goto InpError;
    requiredWordsNumber += requiredWordsNumberS[ctr] - '0';
    ctr++;
    if (ctr<requiredWordsNumberS.Length) goto NextDigit;
    if (requiredWordsNumber < 1) goto InpError;
    ctr = 0;
    goto OutputWords;

InpError:
    Console.WriteLine("Будь ласка, введiть натуральне число!");
    goto InpRequiredWordsNumber;

OutputWords:
    Console.Write(wordCounts[ctr].Key+" - "+wordCounts[ctr].Value+"\n");
    ctr++;
    if (ctr<wordCounts.Count && ctr<requiredWordsNumber) goto OutputWords;
