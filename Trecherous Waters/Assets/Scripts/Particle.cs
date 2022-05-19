using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Handles destruction of particles after they've played out, with an optional delay.
    /// </summary>
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
