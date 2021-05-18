using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPool : Pool
{
    public void PlaySFX(AudioClip clip, Vector3 pos, float volume = 0.5f)
    {
        SFXAudio sfx = GetAPoolObject().GetComponent<SFXAudio>();
        sfx.transform.position = pos;
        sfx.PlayClip(clip, volume);
    }
}
