using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : PoolItem
{
    private Rigidbody2D _rigidBody;
    private Vector3 _moveDir = new Vector3();
    private BulletData.Bullet _bullet;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public override void Activate()
    {
        base.Activate();
    }

    public void Init(Vector3 direction, BulletData.Bullet bullet)
    {
        _bullet = bullet;
        _moveDir = direction;

        GetComponent<Renderer>().material.color = _bullet._color;
        //transform.localScale *= _bullet._size;
        transform.localScale = Vector3.one * _bullet._size;

        StartCoroutine(DestroySelf());
    }

    private void Update()
    {
        _rigidBody.velocity = _moveDir * _bullet._speed;
    }

    private IEnumerator DestroySelf()
    {
        float timer = _bullet._lifeTime;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        //Destroy(gameObject);
        Remove();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy script = other.gameObject.GetComponent<Enemy>();
            script.TakeDamage(_bullet._damage);
            //Destroy(gameObject);
            Remove();
        }
    }
}
