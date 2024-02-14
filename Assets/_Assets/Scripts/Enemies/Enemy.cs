using System.Text;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Assets.Scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        private const char EMPTY_CHAR = ' ';

        [SerializeField] private TextMeshPro _textMeshPro;
        [SerializeField] private TextFitter _backgroundTextFitter;
        [SerializeField] private float _movementSpeed = 10f;

        private StringBuilder _stringBuilder = new StringBuilder();
        private bool _shouldBeDestroyed = false;
        private string _word;
        private char _nextLetter;
        private int _index = 0;

        public string Word
        {
            get => _word; 
            set => SetWord(value);
        }
        public bool ShouldBeDestroyed => _shouldBeDestroyed;

        private void Start()
        {
            //TODO: set target to enemy instead of "new Vector2"
            transform.DOMove(new Vector2(0, -45), _movementSpeed).SetEase(Ease.Linear);
        }

        public bool CheckLetter(char letter)
        {
            if (letter != _nextLetter) return false;
            
            _stringBuilder.Replace(_nextLetter, EMPTY_CHAR, _index, 1);
            _textMeshPro.text = _stringBuilder.ToString();
            _index++;

            if (_index < _word.Length)
            {
                _nextLetter = _word[_index];
            }
            else
            {
                _shouldBeDestroyed = true;
            }

            return true;
        }

        private void SetWord(string value)
        {
            _word = value;
            _stringBuilder.Append(_word);
            _textMeshPro.text = _word;
            _nextLetter = _word[_index];
            _backgroundTextFitter.SetBackgroundSize();
        }
    }
}
