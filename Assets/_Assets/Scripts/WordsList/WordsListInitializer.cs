using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace _Assets.Scripts.WordsList
{
    public abstract class WordsListInitializer
    {
        private const string WORDS_TXT_FILE_NAME = "WordsList";
        private const string WORDS_TXT_FILE_TYPE = "t:TextAsset";
        private const string WORDS_TXT_FILE_EXTENSION = "txt";
        private const string WORDS_LIST_SO_NAME = "letters";
        private const string WORDS_LIST_SO_TYPE = "t:WordsListSO";
        private const string WORDS_LIST_SO_EXTENSION = "asset";
        private const int STARTING_ASCII_LETTER_NUMBER = 97;
        private const int LETTERS_IN_ALPHABET = 26;
        
        [MenuItem("Tools/Generate Words Lists")]
        static void GenerateWordsLists()
        {
            var wordsArray = GetWordsArray();
            var wordsListSOs = GetClearedWordsListSOs();
            InitLetters(wordsListSOs[0]);
            
            //TODO: delete and create other SOs
            foreach (var word in wordsArray)
            {
                var storageNumber = word.Length - 1;
                if(storageNumber <= 0) continue;
                wordsListSOs[storageNumber].words.Add(word);
            }
        }

        static void InitLetters(WordsListSo wordsListSo)
        {
            for (int i = 0; i < LETTERS_IN_ALPHABET; i++)
            {
                var letter = (char)(STARTING_ASCII_LETTER_NUMBER + i);
                wordsListSo.words.Add(letter+"");
            }
        }
        
        static List<WordsListSo> GetClearedWordsListSOs()
        {
            var wordsList = new List<WordsListSo>();

            var paths = GetPathsToFiles(WORDS_LIST_SO_NAME, WORDS_LIST_SO_TYPE, WORDS_LIST_SO_EXTENSION);
            foreach (var path in paths)
            {
                var asset = AssetDatabase.LoadAssetAtPath<WordsListSo>(path);
                asset.words.Clear();
                wordsList.Add(asset);
            }
            
            return wordsList;
        }

        static string[] GetWordsArray()
        {
            var path = GetPathsToFiles(WORDS_TXT_FILE_NAME, WORDS_TXT_FILE_TYPE, WORDS_TXT_FILE_EXTENSION)[0];
            return File.ReadAllLines(path);
        }
        
        static List<string> GetPathsToFiles(string fileName, string fileType, string fileExtension)
        {
            var paths = new List<string>();
            var guids = AssetDatabase.FindAssets($"{fileName} {fileType}");
            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                if (!assetPath.EndsWith($"{fileName}.{fileExtension}")) continue;
                
                paths.Add(assetPath);
            }
            
            return paths;
        }
    }
}