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
        [SerializeField] private WordsListSo _wordsListSo;
        [SerializeField] private float _timeToSpawnEnemy;
        [Space]
        [SerializeField] private float _heightToSpawn;
        [SerializeField] private float _maxWidthToSpawn;
        
        //TODO: find out which word is the lowest or dont create word with letter that is currently on the screen
        private HashSet<Enemy> enemiesList = new HashSet<Enemy>();
        private Enemy _currectEnemy;
            
        void Start()
        {
            SpawnNextEnemy();
        }

        private async UniTaskVoid SpawnNextEnemy()
        {
            //TODO: refactor this, change naming and everything
            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_timeToSpawnEnemy));

                var randomWidth = Random.Range(-_maxWidthToSpawn, _maxWidthToSpawn);
                var spawnPoint = new Vector2(randomWidth, _heightToSpawn);
                
                var storages = _wordsListSo.WordsLists;
                var whichSo = Random.Range(0, storages.Count);
                var list = storages[whichSo].words;
                var whichWord = Random.Range(0, list.Count);
                var word = list[whichWord];

                var enemyGo = Instantiate(_enemyPrefab,spawnPoint, Quaternion.identity);
                var component = enemyGo.GetComponent<Enemy>();
                enemyGo.name = word;
                component.Word = word;

                enemiesList.Add(component);
            }
        }

        public void CheckInputLetter(char character)
        {
            //TODO: refactor this
            if (_currectEnemy != null)
            {
                if (_currectEnemy.CheckLetter(character))
                {
                    if (_currectEnemy.ShouldBeDestroyed)
                    {
                        Destroy(_currectEnemy.gameObject);
                        enemiesList.Remove(_currectEnemy);
                        _currectEnemy = null;
                    }
                }

                return;
            }
            
            foreach (var enemy in enemiesList)
            {
                if (enemy.CheckLetter(character))
                {
                    _currectEnemy = enemy;
                    if (_currectEnemy.ShouldBeDestroyed)
                    {
                        Destroy(_currectEnemy.gameObject);
                        enemiesList.Remove(_currectEnemy);
                        _currectEnemy = null;
                    }
                    //TODO: usuwanie currentEnemy kiedy napisany zostało całe słowo
                    break;
                }
            }
        }
    }
}
