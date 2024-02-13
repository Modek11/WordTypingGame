using System;
using System.Collections.Generic;
using System.IO;
using _Assets.Scripts.ScriptableObjectsScripts;
using UnityEditor;
using UnityEngine;

namespace _Assets.Scripts.WordsList
{
    public abstract class WordsListInitializer
    {
        //TODO: compressFindAssetMethodsIntoOneSimpler
        
        private const string TXT_FILE_NAME = "WordsList";
        private const string WORDS_LIST_SO_TYPE = "letters t:WordsListSO";
        private const int STARTING_ASCII_LETTER_NUMBER = 97;
        private const int LETTERS_IN_ALPHABET = 26;
        
        [MenuItem("Tools/Generate Words Lists")]
        static void GenerateWordsLists()
        {
            var path = GetPathToFile();
            var words = File.ReadAllLines(path);
            var wordsListSOs = GetWordsListSOs();
            ClearWordsListSOs(wordsListSOs);
            InitLetters(wordsListSOs[0]);

            foreach (var word in words)
            {
                var storageNumber = word.Length - 1;
                if(storageNumber <= 0) continue;
                wordsListSOs[storageNumber].words.Add(word);
            }
        }

        static void InitLetters(WordsListSO wordsListSO)
        {
            for (int i = 0; i < LETTERS_IN_ALPHABET; i++)
            {
                var letter = (char)(STARTING_ASCII_LETTER_NUMBER + i);
                wordsListSO.words.Add(letter+"");
                Debug.Log(letter);
            }
        }

        static void ClearWordsListSOs(List<WordsListSO> wordsListSOs)
        {
            foreach (var wordsListSO in wordsListSOs)
            {
                wordsListSO.words.Clear();
            }
        }
        
        static List<WordsListSO> GetWordsListSOs()
        {
            var wordsList = new List<WordsListSO>();

            var guids = AssetDatabase.FindAssets(WORDS_LIST_SO_TYPE);
            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<WordsListSO>(assetPath);
                wordsList.Add(asset);
            }
            
            return wordsList;
        }

        static string GetPathToFile()
        {
            var path = "";
            var guids = AssetDatabase.FindAssets($"{TXT_FILE_NAME} t:TextAsset");
            
            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                if (!assetPath.EndsWith($"{TXT_FILE_NAME}.txt")) continue;
                
                if (!String.IsNullOrEmpty(path))
                {
                    Debug.LogError($"Duplicated file found at: \n {path} \n and \n {assetPath}");
                }
                
                path = assetPath;
            }
            
            return path;
        }
    }
}