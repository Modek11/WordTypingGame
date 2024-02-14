using System.Collections.Generic;
using UnityEngine;

namespace _Assets.Scripts.WordsList
{
    [CreateAssetMenu(fileName = "WordsList", menuName = "ScriptableObjects/WordsList")]
    public class WordsListSo : ScriptableObject
    {
        public List<WordsListType> WordsLists;
    }
}
