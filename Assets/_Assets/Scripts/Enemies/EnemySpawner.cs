using System;
using _Assets.Scripts.WordsList;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Assets.Scripts.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private WordsListSo _wordsListSo;
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
                
                var storages = _wordsListSo.WordsLists;
                var whichSo = Random.Range(0, storages.Count);
                var list = storages[whichSo].words;
                var whichWord = Random.Range(0, list.Count);
                var word = list[whichWord];

                var enemyGo = Instantiate(_enemyPrefab);
                var component = enemyGo.GetComponent<Enemy>();
                enemyGo.name = word;
                component.Word = word;
                
                Destroy(enemyGo, 1);
            }
        }
    }
}
