using UnityEngine;

namespace TreacherousWaters
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjects/Projectile")]
    public class ProjectileScriptableObject : ScriptableObject
    {
        public GameObject prefab;
        public GameObject particleShot;
        public GameObject particleShipImpact;
        public GameObject particleWaterImpact;
        public GameObject particleDefaultImpact;
        public AmmunitionType type;
        public float damage = 5;
        public float loadTime = 3;
        public float lifeTime = 5;
        public float forwardForce = 15;
        public float upForce = 5;
        public float survivableImpactCount = 2;
    }
}

