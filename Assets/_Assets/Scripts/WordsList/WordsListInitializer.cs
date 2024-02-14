using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace _Assets.Scripts.WordsList
{
    public abstract class WordsListInitializer
    {
        private const string WORDS_TXT_FILE_NAME = "WordsList";
        private const string WORDS_TXT_FILE_TYPE = "t:TextAsset";
        private const string WORDS_TXT_FILE_EXTENSION = "txt";
        private const string WORDS_LIST_SO_NAME = "WordsListSO";
        private const string WORDS_LIST_SO_TYPE = "t:WordsListSO";
        private const string WORDS_LIST_SO_EXTENSION = "asset";
        private const int STARTING_ASCII_LETTER_NUMBER = 97;
        private const int LETTERS_IN_ALPHABET = 26;
        
        [MenuItem("Tools/Generate Words Lists")]
        static void GenerateWordsLists()
        {
            var wordsArray = GetWordsArray();
            var wordsListSo = GetWordsListSo();
            wordsListSo.WordsLists.Clear();
            InitLettersWordsListSo(wordsListSo);
            
            foreach (var word in wordsArray)
            {
                var storageNumber = word.Length - 1;
                if(storageNumber <= 0)
                {
                    continue;
                }
                
                while (wordsListSo.WordsLists.Count <= storageNumber) 
                {
                    wordsListSo.WordsLists.Add(new WordsListType());
                }
                
                wordsListSo.WordsLists[storageNumber].words.Add(word);
            }
        }

        static void InitLettersWordsListSo(WordsListSo wordsListSo)
        {
            wordsListSo.WordsLists.Add(new WordsListType());
            for (int i = 0; i < LETTERS_IN_ALPHABET; i++)
            {
                var letter = (char)(STARTING_ASCII_LETTER_NUMBER + i);
                wordsListSo.WordsLists[0].words.Add(letter+"");
            }
        }
        
        static WordsListSo GetWordsListSo()
        {
            var paths = GetPathsToFiles(WORDS_LIST_SO_NAME, WORDS_LIST_SO_TYPE, WORDS_LIST_SO_EXTENSION, true);
            var path = paths[0];
            return AssetDatabase.LoadAssetAtPath<WordsListSo>(path);
        }

        static string[] GetWordsArray()
        {
            var paths = GetPathsToFiles(WORDS_TXT_FILE_NAME, WORDS_TXT_FILE_TYPE, WORDS_TXT_FILE_EXTENSION, true);
            var path = paths[0];
            return File.ReadAllLines(path);
        }
        
        static List<string> GetPathsToFiles(string fileName, string fileType, string fileExtension, bool shouldReturnSinglePath)
        {
            var paths = new List<string>();
            var guids = AssetDatabase.FindAssets($"{fileName} {fileType}");
            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                if (!assetPath.EndsWith($"{fileName}.{fileExtension}")) continue;
                
                paths.Add(assetPath);
            }

            if (paths.Count > 1 && shouldReturnSinglePath)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Found more than one asset of name: {fileName}.{fileExtension}. Files paths:");
                foreach (var path in paths)
                {
                    sb.Append($"\n\t{path}");
                }
                
                Debug.LogError(sb.ToString());
            }
            
            return paths;
        }
    }
}