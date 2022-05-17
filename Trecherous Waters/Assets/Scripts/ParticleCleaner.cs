using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreacherousWaters
{
    public class ParticleCleaner : MonoBehaviour
    {
        [SerializeField] float delay;

        void Start()
        {
            float particleDuration = GetComponent<ParticleSystem>().main.duration;
            Destroy(gameObject, particleDuration + delay);
        }
    }
}
