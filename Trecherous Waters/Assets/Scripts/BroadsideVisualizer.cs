using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreacherousWaters
{
    [RequireComponent(typeof(LineRenderer))]
    public class BroadsideVisualizer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] LineRenderer lineRenderer;
        InputHandler inputHandler;
        ShipCombat shipCombat;

        [Header("Values")]
        [SerializeField] Color lineStartColor = new Color(255, 255, 255, 65);
        [SerializeField] Color lineEndColor = new Color(255, 255, 255, 0);

        [SerializeField] Color attackStartColor = new Color(255, 0, 0, 65);
        [SerializeField] Color attackEndColor = new Color(255, 0, 0, 0);

        [SerializeField] LayerMask enemyShipLayer;

        [SerializeField] float length = 90;
        [SerializeField] float width = 12;

        float shipWidthOffset = 3.2f;

        bool[] shipRayChecks = new bool[3];

        private void OnValidate()
        {
            lineRenderer = GetComponent<LineRenderer>();

            lineRenderer.alignment = LineAlignment.TransformZ;
            lineRenderer.widthMultiplier = width;
            lineRenderer.useWorldSpace = false;
            lineRenderer.startColor = lineStartColor;
            lineRenderer.endColor = lineEndColor;
        }

        private void Start()
        {
            shipCombat = GetComponentInParent<ShipCombat>();
            inputHandler = GetComponentInParent<InputHandler>();

            switch (inputHandler.currentSide)
            {
                case Broadside.port:
                    lineRenderer.SetPosition(0, new Vector3(-shipWidthOffset, 0, -2));
                    lineRenderer.SetPosition(1, new Vector3(-length, 0, 1));
                    break;
                case Broadside.starboard:
                    lineRenderer.SetPosition(0, new Vector3(shipWidthOffset, 0, -2));
                    lineRenderer.SetPosition(1, new Vector3(length, 0, -2));
                    break;
            }
        }

        void Update()
        {
            if (!inputHandler || !shipCombat) 
            { 
                Debug.Log($"Missing InputHandler & ShipCombat scripts on {gameObject.name}'s parent!");
                return;
            }

            if (shipCombat.currentAmmo == AmmunitionType.Barrel)
            {
                for (int i = 0; i < lineRenderer.positionCount; i++)
                {
                    lineRenderer.SetPosition(i, Vector3.zero);
                }
                return;
            }

            HandleLines();
        }

        private void HandleLines()
        {
            switch (inputHandler.currentSide)
            {
                case Broadside.port:
                    lineRenderer.SetPosition(0, new Vector3(-shipWidthOffset, 0, -2));
                    lineRenderer.SetPosition(1, new Vector3(-length, 0, -2));
                    break;
                case Broadside.starboard:
                    lineRenderer.SetPosition(0, new Vector3(shipWidthOffset, 0, -2));
                    lineRenderer.SetPosition(1, new Vector3(length, 0, -2));
                    break;
            }

            SetColors(CheckForEnemyShip(inputHandler.currentSide));
        }

        private bool CheckForEnemyShip(Broadside side)
        {
            float offset = side == Broadside.port ? -length : length;

            shipRayChecks[0] = Physics.Raycast(transform.root.position + (transform.forward * 7), transform.right * offset, length, enemyShipLayer);
            shipRayChecks[1] = Physics.Raycast(transform.root.position, transform.right * offset, length, enemyShipLayer);
            shipRayChecks[2] = Physics.Raycast(transform.root.position + (transform.forward * -7), transform.right * offset, length, enemyShipLayer);

            for (int i = 0; i < shipRayChecks.Length; i++)
            {
                if (shipRayChecks[i]) return true;
            }
            return false;
        }

        private void SetColors(bool combat)
        {
            bool loaded = inputHandler.currentSide == Broadside.port ? shipCombat.portLoaded : shipCombat.starboardLoaded;
            if (combat && loaded)
            {
                lineRenderer.startColor = attackStartColor;
                lineRenderer.endColor = attackEndColor;
            }
            else
            {
                lineRenderer.startColor = lineStartColor;
                lineRenderer.endColor = lineEndColor;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.root.position, transform.root.position + (transform.right * -length));
        }
    }
}
