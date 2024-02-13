using System;
using System.Text;
using TMPro;
using UnityEngine;

namespace _Assets.Scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        private const char EMPTY_CHAR = ' ';

        [SerializeField] private TextMeshPro _textMeshPro;
        [SerializeField] private BackgroundTextFitter _backgroundTextFitter;

        private StringBuilder _stringBuilder;
        private string _word;
        private char _nextLetter;
        private int _index = 0;

        public string Word
        {
            get => _word; 
            set => SetWord(value);
        }

        private void Start()
        {
            _stringBuilder = new StringBuilder();
        }

        public bool CheckLetter(char letter)
        {
            if (letter != _nextLetter) return false;
            
            _stringBuilder.Replace(_nextLetter, EMPTY_CHAR, _index, 1);
            _index++;
            _nextLetter = _word[_index];

            return true;
        }

        private void SetWord(string value)
        {
            _word = value;
            _textMeshPro.text = _word;
            _nextLetter = _word[_index];
            _backgroundTextFitter.SetBackgroundSize();
        }
    }
}
