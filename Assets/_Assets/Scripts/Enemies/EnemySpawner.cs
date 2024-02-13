using System;
using System.Collections.Generic;
using _Assets.Scripts.WordsList;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Assets.Scripts.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private List<WordsListSo> _wordsListSOs;
        void Start()
        {
            SpawnNextEnemy();
        }

        private async UniTaskVoid SpawnNextEnemy()
        {
            //TODO: refactor this, change naming and everything
            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1));

                var whichSO = Random.Range(0, 8);
                var list = _wordsListSOs[whichSO].words;
                var whichword = Random.Range(0, list.Count);
                var word = list[whichword];

                var enemyGO = Instantiate(_enemyPrefab);
                var component = enemyGO.GetComponent<Enemy>();
                enemyGO.name = word;
                component.Word = word;
            }
        }
    }
}
