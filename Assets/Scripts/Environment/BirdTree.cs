using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdTree : MonoBehaviour
{
    public List<AudioClip> birdsongs;
    public float minInterval = 5f;

    private AudioSource audioSource;

    private void Start()
    {
        StartCoroutine(PlayBirdSong());
        audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator PlayBirdSong()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.value * 40f + minInterval);
            audioSource.clip = birdsongs[(int)(Random.value * birdsongs.Count)];
            audioSource.Play();
        }
    }
}
