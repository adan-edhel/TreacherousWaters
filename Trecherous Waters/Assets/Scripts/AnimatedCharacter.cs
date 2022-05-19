using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreacherousWaters
{
    public class AnimatedCharacter : MonoBehaviour
    {
        [SerializeField] bool randomizeAnimation = true;
        [SerializeField] int preferredAnimIndex;
        Animator anim;

        float counter;

        void Start()
        {
            anim = GetComponent<Animator>();
            ChangeAnimation();
        }

        void Update()
        {
            if (counter > 0) { counter -= Time.deltaTime; }

            if (counter <= 0)
            {
                ChangeAnimation();
                counter = Random.Range(10, 20);
            }
        
        }

        private void ChangeAnimation()
        {
            if (randomizeAnimation)
            {
                anim.SetInteger("Index", Random.Range(1, 4));
            }
        }

        public void HandleDeath()
        {
            Destroy(gameObject);
        }
    }
}
