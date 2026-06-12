using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Holds all projectile related data.
    /// </summary>
    [CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjects/Projectile")]
    public class ProjectileScriptableObject : ScriptableObject
    {
        /// <summary>
        /// Projectile object prefab.
        /// </summary>
        public GameObject prefab;
        /// <summary>
        /// Cannon shot particle.
        /// </summary>
        public GameObject particleShot;
        /// <summary>
        /// Ship impact particle.
        /// </summary>
        public GameObject particleShipImpact;
        /// <summary>
        /// Water impact particle.
        /// </summary>
        public GameObject particleWaterImpact;
        /// <summary>
        /// Default particle for every other surface.
        /// </summary>
        public GameObject particleDefaultImpact;
        /// <summary>
        /// Reference for the type of ammunition this projectile is.
        /// </summary>
        public AmmunitionType type;
        /// <summary>
        /// Amount of damage the projectile would deliver.
        /// </summary>
        public float damage = 5;
        /// <summary>
        /// Time it takes to reload this ammunition.
        /// </summary>
        public float loadTime = 3;
        /// <summary>
        /// How long the projectile can survive before being destroyed.
        /// </summary>
        public float lifeTime = 5;
        /// <summary>
        /// The force with which the projectile is shot forwards.
        /// </summary>
        public float forwardForce = 15;
        /// <summary>
        /// The force with which the projectile is shot upwards.
        /// </summary>
        public float upForce = 5;
        /// <summary>
        /// How many impacts the projectile can survive before being destroyed.
        /// </summary>
        public float survivableImpactCount = 2;
    }
}

