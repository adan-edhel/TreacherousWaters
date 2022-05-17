using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreacherousWaters
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEffect : MonoBehaviour
    {
        [SerializeField] bool variedVolume;
        [SerializeField] List<AudioClip> clip = new List<AudioClip>();

        void Start()
        {
            if (TryGetComponent<AudioSource>(out AudioSource source))
            {
                if (clip.Count < 1) return;
                float startVolume = source.volume;
                if (variedVolume) startVolume = Random.Range(startVolume / 2, startVolume);
                source.volume = startVolume;
                source.PlayOneShot(clip[Random.Range(0, clip.Count - 1)]);
            }
        }
    }
}
