using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundGroup
{
    public string name;

    public AudioClip[] allSounds;

	[Range(0f, 1f)]
	public float volume = .75f;
	[Range(0f, 1f)]
	public float volumeVariance = .1f;

	[Range(.1f, 3f)]
	public float pitch = 1f;
	[Range(0f, 1f)]
	public float pitchVariance = .1f;

	public float minimunDistance = 1f;
	public bool loop = false;

	public AudioMixerGroup mixerGroup;

	[HideInInspector]
	public AudioSource source;

	public AudioClip GetRandomAudioClip()
    {
		if (allSounds.Length == 0)
        {
			return null;
        }
		return allSounds[Random.Range(0, allSounds.Length)];
    }
}
