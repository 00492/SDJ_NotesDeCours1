using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveTime = 1f;
    [SerializeField] private float _hp = 100f;

    private Vector2 _startPos;
    private Vector2 _endPos;

    private bool _goingUp = true;


    private void Start()
    {
        _startPos = transform.position;
        _endPos = _startPos + new Vector2(0, 3f);

        StartCoroutine(PatrolRoutine());
    }

    private IEnumerator PatrolRoutine()
    {
        float timer = 0f;

        while (timer < _moveTime)
        {
            timer += Time.deltaTime;
            float lerpValue = timer / _moveTime;

            if (_goingUp)
            {
                transform.position = Vector2.Lerp(_startPos, _endPos, lerpValue);
            }
            else
            {
                transform.position = Vector2.Lerp(_endPos, _startPos, lerpValue);
            }

            yield return null;
        }

        _goingUp = !_goingUp;
        StartCoroutine(PatrolRoutine());
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;

        Debug.Log("HP : " + _hp);

        if(_hp <= 0)
        {
            PlayerController._onKill?.Invoke(10 /* represente l'xp value de l'enemy*/);
            Destroy(gameObject);
        }
    }
}
