using System.Collections;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class SoundHelper : MonoBehaviour {

    public AudioClip[] clips;
    public float pitchVariance;

    new AudioSource audio;

	void Start () {
        audio = GetComponent<AudioSource>();
	}

    public void Play()
    {
        if (clips.Length <= 0) return;
        int index = Random.Range(0,clips.Length-1);
        audio.pitch = Random.Range(1-pitchVariance,1+pitchVariance);
        audio.PlayOneShot(clips[index]);
    }

    public void Play(AudioClip clip)
    {
        audio.pitch = 1;
        audio.PlayOneShot(clip);
    }
}
