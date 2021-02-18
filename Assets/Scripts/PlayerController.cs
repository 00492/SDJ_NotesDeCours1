using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private float _moveSpeed = 400f;
    [SerializeField] private Animator _animator;

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private BulletData _bulletData;

    [SerializeField] private AudioSource _audioFootsteps;

    private int _direction = 0;
    private Vector2 _velocity;
    private bool _isShooting = false;

    public int _bulletIndex = 0;

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
            if (!_isShooting)
            {
                _isShooting = true;
                _animator.SetTrigger("Shoot");
                _rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
                ShootProjectile();
            }
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
        GameObject go = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
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
}
