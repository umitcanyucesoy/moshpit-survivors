using Datas;
using PlayerScripts;
using UnityEngine;
using VContainer;

namespace EnemyScripts
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyData enemyData;
        
        public float knockBackTime = .5f;
        public float knockBackCounter;

        private Transform _target;
        private Rigidbody2D _rb2D;
        private EnemyHealthController _enemyHealthController;
        private float _currentMoveSpeed;

        [Inject]
        public void Construct(PlayerController playerTransform)
        {
            _target = playerTransform.transform;
        }

        private void Awake()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            _enemyHealthController = GetComponent<EnemyHealthController>();
        }
        
        private void Start()
        {
            _currentMoveSpeed = enemyData.moveSpeed;
        }

        private void Update()
        {
            EnemyMovement();
            KnockBack();
        }

        private void EnemyMovement()
        {
            if (_target.gameObject.activeSelf == true)
                _rb2D.velocity = (_target.position - transform.position).normalized * _currentMoveSpeed;
            else
                _rb2D.velocity = Vector3.zero;
        }

        private void KnockBack()
        {
            if (knockBackCounter > 0)
            {
                knockBackCounter -= Time.deltaTime;

                if (_currentMoveSpeed > 0)
                {
                    _currentMoveSpeed = -_currentMoveSpeed * 1.5f;
                }

                if (knockBackCounter <= 0)
                {
                    _currentMoveSpeed = Mathf.Abs(_currentMoveSpeed * .5f);
                }
            }
        }
    }
}
