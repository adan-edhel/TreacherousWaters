using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreacherousWaters
{
    [RequireComponent(typeof(ParticleSystem))]
    public class Particle : MonoBehaviour
    {
        [SerializeField] float delay;

        void Start()
        {
            float particleDuration = GetComponent<ParticleSystem>().main.duration;
            Destroy(gameObject, particleDuration + delay);
        }
    }
}
