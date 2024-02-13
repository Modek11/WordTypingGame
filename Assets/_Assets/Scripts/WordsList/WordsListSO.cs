using System.Collections.Generic;
using UnityEngine;

namespace _Assets.Scripts.ScriptableObjectsScripts
{
    [CreateAssetMenu(fileName = "WordsList", menuName = "ScriptableObjects/WordsList")]
    public class WordsListSO : ScriptableObject
    {
        public List<string> words;
    }
}
