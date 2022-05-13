using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipCombat : MonoBehaviour, IFire
{
    [SerializeField] private Transform[] cannons;
    [SerializeField] ProjectileScriptableObject[] projectiles;

    private float loadCounter;
    private bool canFire => loadCounter <= 0;

    private Collider shipCollider;

    private void Start()
    {
        shipCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (loadCounter > 0) loadCounter -= Time.deltaTime;
    }

    public void Fire()
    {
        if (!canFire) return;

        foreach (Transform cannon in cannons)
        {
            // Instantiate and create reference for canonballs
            Projectile ball = Instantiate(projectiles[0].prefab, cannon.position, Quaternion.identity).GetComponent<Projectile>();
            
            // Rotate the cannonballs to match ship rotation
            ball.transform.Rotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z));

            // Check if cannon is on portside (left side of the ship)
            bool portside = cannon.localPosition.x < 0;

            // Rotate cannon balls to match side
            float rotateDegrees = portside ? -90 : 90;
            ball.transform.Rotate(ball.transform.up, rotateDegrees);

            // Initiate shot
            ball.OnShot(shipCollider, projectiles[0]);
        }

        loadCounter = projectiles[0].loadTime;
    }

    private void OnDrawGizmosSelected()
    {
        if (cannons.Length > 0)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < cannons.Length; i++)
            {
                Gizmos.DrawSphere(cannons[i].position, .5f);
            }
        }
    }
}
