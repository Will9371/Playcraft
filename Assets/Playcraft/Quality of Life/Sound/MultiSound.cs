using UnityEngine;
using System.Collections;

namespace Playcraft
{
    public class MultiSound : MonoBehaviour
    {
        [SerializeField] AudioClipArray clips;
        AudioSource source;

        void Awake()
        {
            source = GetComponent<AudioSource>();
        }

        public void SetClips(AudioClipArray clips) { this.clips = clips; }

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
            PlayClip(index, source.volume);
        }
        
        public void PlayRandom(float volume)
        {
            var index = Random.Range(0, clips.values.Length);
            PlayClip(index, volume);
        }
        
        void PlayClip(int index, float volume)
        {
            //source.clip = clips.values[index];
            source.PlayOneShot(clips.values[index], volume);
        }
    }
}