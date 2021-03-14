using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineExample : MonoBehaviour
{
    private Coroutine _waitRoutine;
    private float _moveTime = 2f;

    [SerializeField] private Transform _startPos;
    [SerializeField] private Transform _endPos;


    private void Start()
    {
        //_waitRoutine = StartCoroutine(WaitSomeTime());
        StartCoroutine(TimerWait());

    }

    private IEnumerator WaitSomeTime()
    {
        Debug.Log("Started");

        yield return new WaitForEndOfFrame();
        Debug.Log("1 frame has passed");

        yield return null;
        Debug.Log("Another grame has passed");

        yield return new WaitForSeconds(2f);
        Debug.Log("2 seconds have gone away!");

        yield return new WaitForSeconds(4f);
        Debug.Log("Total of 6 seconds has passed");
    }

    private IEnumerator TimerWait()
    {
        float timer = 0f;
        while(timer < 5f)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Timer of 5 secs done");
    }

    private IEnumerator LerpToPosition()
    {
        float timer = 0f;

        while(timer < _moveTime)
        {
            timer += Time.deltaTime;

            float lerpValue = timer / _moveTime;
            transform.position = Vector3.Lerp(_startPos.position, _endPos.position, lerpValue);

            yield return new WaitForEndOfFrame();
        }
    }


    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //StopCoroutine(_waitRoutine);
            StopAllCoroutines();
            Debug.Log("CoroutineStopped");

            StartCoroutine(LerpToPosition());
        }
    }
}
