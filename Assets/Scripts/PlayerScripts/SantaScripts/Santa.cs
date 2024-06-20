using UnityEngine;

namespace PlayerScripts.SantaScripts
{
    public class Santa : MonoBehaviour, IDamageable, ISantaMoveable
    {
        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }
        public Rigidbody2D RB { get; set; }
        public Animator ANIM { get; set; }
        public SpriteRenderer SR { get; set; }
        public bool IsFacingRight { get; set; } = false;
    
        private void Start()
        {
            CurrentHealth = MaxHealth;
            RB = GetComponent<Rigidbody2D>();
            ANIM = GetComponent<Animator>();
            SR = GetComponent<SpriteRenderer>();
        }

        public void Damage(float damageAmount)
        {
            CurrentHealth -= damageAmount;

            if (CurrentHealth <= 0f)
            {
                Dead();
            }
        }

        public void Dead()
        {
            throw new System.NotImplementedException();
        }

        public void MoveSanta(Vector2 velocity)
        {
            RB.velocity = velocity;
            CheckForLeftOrRightFacing(velocity);
        }

        public void CheckForLeftOrRightFacing(Vector2 velocity)
        {
            SR.flipX = velocity.x != 0 ? velocity.x < 0 : SR.flipX;
        }
    }
}
