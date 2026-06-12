using UnityEngine;

namespace TreacherousWaters
{
    /// <summary>
    /// Handles animation of humanoid characters.
    /// </summary>
    public class AnimatedCharacter : MonoBehaviour
    {
        /// <summary>
        /// If true, randomizes the animations the character switches to.
        /// </summary>
        [SerializeField] bool randomizeAnimation = true;
        /// <summary>
        /// Preferred animation index to switch to.
        /// </summary>
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

        /// <summary>
        /// Changes animation to random character animation.
        /// </summary>
        private void ChangeAnimation()
        {
            if (randomizeAnimation)
            {
                anim.SetInteger("Index", Random.Range(1, 4));
            }
            else
            {
                anim.SetInteger("Index", preferredAnimIndex);
            }
        }

        /// <summary>
        /// Destroys the character.
        /// </summary>
        public void HandleDeath()
        {
            Destroy(gameObject);
        }
    }
}
