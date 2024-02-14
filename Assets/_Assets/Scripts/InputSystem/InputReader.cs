using _Assets.Scripts.Core;
using _Assets.Scripts.Enemies;
using UnityEngine;

namespace _Assets.Scripts.InputSystem
{
    public class InputReader : Singleton<InputReader>
    {
        [SerializeField] private EnemySpawner _enemySpawner;
        void Update()
        {
            //TODO: add escape to disable currently entering word
            foreach (var input in Input.inputString)
            {
                _enemySpawner.CheckInputLetter(input);
                Debug.Log(input);
            }
        }
    }
}
