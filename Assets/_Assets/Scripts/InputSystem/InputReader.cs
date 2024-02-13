using _Assets.Scripts._Patterns;
using UnityEngine;

namespace _Assets.Scripts.InputSystem
{
    public class InputReader : Singleton<InputReader>
    {
        void Update()
        {
            foreach (var input in Input.inputString)
            {
                Debug.Log(input);
            }
        }

    
    }
}
