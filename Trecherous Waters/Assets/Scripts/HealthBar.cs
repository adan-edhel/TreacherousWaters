using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace TreacherousWaters
{
    [RequireComponent(typeof(ShipBase))]
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] GameObject HealthBarPrefab;
        [SerializeField] float HealthBarHeight = 28;

        [SerializeField] float distanceToDisappear = 140;

        GameObject healthBarObject;
        Image healthBarImage;
        ShipBase ship;

        void Start()
        {
            SetupHealthbar();
        }

        void Update()
        {
            if (healthBarObject != null)
            {
                HandleMovement();
                UpdateBar();
                if (ship.integrity <= 0)
                {
                    Destroy(healthBarObject);
                }
            }
        }

        private void SetupHealthbar()
        {
            ship = GetComponent<ShipBase>();
            healthBarObject = Instantiate(HealthBarPrefab, transform.position, Quaternion.identity);
            healthBarObject.transform.SetParent(transform);

            healthBarObject.transform.localPosition = new Vector3(0, HealthBarHeight, 0);
            healthBarImage = healthBarObject.transform.Find("Canvas/Bar/Slider").GetComponent<Image>();
        }

        private void HandleMovement()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, PlayerShip.Instance.transform.position);
            bool inRange = distanceToPlayer > distanceToDisappear ? false : true;
            healthBarObject.SetActive(inRange);

            if (inRange)
            {
                Vector3 posToLookAt = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
                healthBarObject.transform.LookAt(posToLookAt);
            }
        }

        private void UpdateBar()
        {
            healthBarImage.fillAmount = ship.integrity / ship.maxIntegrity;
        }
    }
}
