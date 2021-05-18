using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private float _moveSpeed = 400f;
    [SerializeField] private Animator _animator;

    [Header("Bullets")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private BulletData _bulletData;
    [SerializeField] private Pool _bulletPool;

    [Header("Audio")]
    [SerializeField] private AudioSource _audioFootsteps;
    [SerializeField] private AudioPool _audioPool;
    [SerializeField] private AudioClip _shootClip;
        
    private int _direction = 0;
    private Vector2 _velocity;
    private bool _isShooting = false;

    public int _bulletIndex = 0;

    public static Action<int> _onKill;
    public static Action _onPlayerHit;

    private void Start()
    {
        _onPlayerHit += HitFeedback;
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _animator.SetBool("Walk", true);
            _renderer.flipX = true;
            _direction = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _animator.SetBool("Walk", true);
            _renderer.flipX = false;
            _direction = 1;
        }
        else
        {
            _animator.SetBool("Walk", false);
            _direction = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //if (!_isShooting)
            {
                _isShooting = true;
                _animator.SetTrigger("Shoot");
                _rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
                ShootProjectile();
            }
        }
        else if(Input.GetMouseButtonDown(1))
        {
            _onPlayerHit?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _velocity = _rigidBody.velocity;
        _velocity.x = _moveSpeed * _direction * Time.deltaTime;
        _rigidBody.velocity = _velocity;
    }

    private void ShootProjectile()
    {
        //GameObject go = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        // Fetch bullet from pool
        PoolItem go = _bulletPool.GetAPoolObject();
        go.transform.position = transform.position;

        // Play clip from pool
        _audioPool.PlaySFX(_shootClip, transform.position);

        // Initialize projectile
        Projectile script = go.GetComponent<Projectile>();
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.z = 0;
        script.Init(direction.normalized, _bulletData._bullets[_bulletIndex]);
    }

    public void CanShoot()
    {
        _rigidBody.constraints = RigidbodyConstraints2D.None;
        _isShooting = false;
    }

    public void Footsteps()
    {
        _audioFootsteps.Play();
    }

    private void HitFeedback()
    {
        transform.localScale /= 1.2f;
    }

    private void OnDestroy()
    {
        _onPlayerHit -= HitFeedback;
    }
}
