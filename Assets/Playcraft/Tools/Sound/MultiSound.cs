using UnityEngine;
using System.Collections;

public class MultiSound : MonoBehaviour
{
    [SerializeField] AudioClipArray clips;
    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void SetClips(AudioClipArray clips)
    {
        this.clips = clips;
    }

    public void PlayRandomSeries(int count, float delay, float volume, float pitch)
    {
        StartCoroutine(PlayRandomRoutine(count, delay, volume, pitch));
    }

    IEnumerator PlayRandomRoutine(int count, float delay, float volume, float pitch)
    {
        float startVolume = source.volume;
        float startPitch = source.pitch;

        source.volume = volume;
        source.pitch = pitch;

        for (int i = 0; i < count; i++)
        {
            PlayRandom();
            yield return new WaitForSeconds(delay);
        }

        source.volume = startVolume;
        source.pitch = startPitch;
    }

    public void PlayRandom()
    {
        var index = Random.Range(0, clips.values.Length);
        PlayClip(index);
    }

    public void PlayClip(int index)
    {
        //Debug.Log("Playing clip: " + index);
        var randomClip = clips.values[index];
        source.clip = randomClip;
        source.PlayOneShot(randomClip);
    }
    
    // HACK
    public void PlayRandom(float volume)
    {
        var index = Random.Range(0, clips.values.Length);
        var randomClip = clips.values[index];
        source.clip = randomClip;
        source.PlayOneShot(randomClip, volume);
    }
}
