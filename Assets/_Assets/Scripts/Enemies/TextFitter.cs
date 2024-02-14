using TMPro;
using UnityEngine;

namespace _Assets.Scripts.Enemies
{
    [ExecuteInEditMode]
    public class TextFitter : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _textMeshPro;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _textSize;
        [SerializeField] private float _paddingX;
        [SerializeField] private float _paddingY;


        //TODO: change this Update() to Start(), when is done, and delete ExecuteInEditMode above
        private void Update()
        {
            SetBackgroundSize();
            SetTextSize();
        }

        private void SetTextSize()
        {
            _textMeshPro.fontSize = _textSize;
        }

        public void SetBackgroundSize()
        {
            var backgroundSize = new Vector2(_textMeshPro.preferredWidth + _paddingX, _textMeshPro.preferredHeight + _paddingY);
            _spriteRenderer.size = backgroundSize;
        }
    }
}
