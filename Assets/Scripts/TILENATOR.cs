using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TILENATOR : MonoBehaviour
{
    public CUBINATOR _player;
    private Renderer _renderer;

    public int _playerID = -1;

    private Vector3 _startPos;
    private Vector3 _endPos;


    private void Start()
    {
        _startPos = transform.position;
        _endPos = _startPos + new Vector3(2, 0, 0);
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && _player != null)
        {
            _renderer.material.color = _player._playerColor;
            StartCoroutine(BounceMove());
        }
    }

    private IEnumerator BounceMove()
    {
        float timer = 0;
        float duration = 0.5f;
        while(timer < duration)
        {
            timer += Time.deltaTime;
            float lerpvalue = timer / duration;

            float x = Mathf.Lerp(_startPos.x, _endPos.x, lerpvalue);
            float y = 0;
            if(lerpvalue <= 0.5f)
            {
                y = Mathf.Lerp(_startPos.y, _startPos.y + 1, lerpvalue);
            }
            else
            {
                y = Mathf.Lerp(_startPos.y + 1, _startPos.y, lerpvalue);
            }

            transform.position = new Vector3(x, y, transform.position.z);
            yield return null;
        }
    }
}
