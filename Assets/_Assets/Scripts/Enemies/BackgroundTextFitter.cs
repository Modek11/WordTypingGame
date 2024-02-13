using TMPro;
using UnityEngine;

namespace _Assets.Scripts.Enemies
{
    [ExecuteInEditMode]
    public class BackgroundTextFitter : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _textMeshPro;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _paddingX;
        [SerializeField] private float _paddingY;


        private void Update()
        {
            SetBackgroundSize();
        }

        public void SetBackgroundSize()
        {
            var backgroundSize = new Vector2(_textMeshPro.preferredWidth + _paddingX, _textMeshPro.preferredHeight + _paddingY);
            _spriteRenderer.size = backgroundSize;
        }
    }
}
