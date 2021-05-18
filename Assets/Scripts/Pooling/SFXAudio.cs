using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXAudio : PoolItem
{
    [SerializeField] private AudioSource _source;

    public void PlayClip(AudioClip clip, float volume = 0.5f)
    {
        _source.clip = clip;
        _source.volume = volume;
        _source.Play();
        StartCoroutine(EndClip(clip.length));
    }

    private IEnumerator EndClip(float duration)
    {
        yield return new WaitForSeconds(duration);
        _source.Stop();
        _source.clip = null;
        Remove();
    }
}
