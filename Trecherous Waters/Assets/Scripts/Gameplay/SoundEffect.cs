using System.Collections.Generic;
using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Handles playing of random clips in an AudioSource. Optionally can 
    /// adjust the volume to add depth.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class SoundEffect : MonoBehaviour
    {
        [SerializeField] bool randomPitch;
        [SerializeField] bool variedVolume;
        [SerializeField] List<AudioClip> clip = new List<AudioClip>();

        void Start()
        {
            if (TryGetComponent<AudioSource>(out AudioSource source))
            {
                if (clip.Count < 1) return;

                if (variedVolume) source.volume = Random.Range(0.8f, 1.0f);
                if (randomPitch) source.pitch = Random.Range(0.85f, 1.15f);

                source.PlayOneShot(clip[Random.Range(0, clip.Count - 1)]);
            }
        }
    }
}
