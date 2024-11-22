using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace EnemyScripts
{
    public class EnemyAnimation : MonoBehaviour
    {
        [SerializeField] private Transform sprite;
        [SerializeField] private float spriteSpeed;
        [SerializeField] private float maxSize,minSize;

        private float _activeSize;

        private void Start()
        {
            _activeSize = maxSize;
            spriteSpeed *= Random.Range(.75f, 1.25f);
        }

        private void Update()
        {
            SpriteAnimation();
        }

        private void SpriteAnimation()
        {
            sprite.localScale = Vector3.MoveTowards(sprite.localScale, Vector3.one * _activeSize,
                spriteSpeed * Time.deltaTime);

            if (Mathf.Approximately(sprite.localScale.x, _activeSize))
            {
                if (Mathf.Approximately(_activeSize, maxSize))
                {
                    _activeSize = minSize;
                }
                else
                {
                    _activeSize = maxSize;
                }
            }
        }
    }
}   