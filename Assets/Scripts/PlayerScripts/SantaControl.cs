using System;
using System.Collections;
using UnityEngine;

namespace PlayerScripts
{
    public class SantaControl : MonoBehaviour
    {
        public float speed;
        public float maxSpeed;
        public Vector2 dirVec;
        public SantaState state;
        public bool isAlive = true;
        
        private Rigidbody2D _rigid;
        private Animator _anim;
        private SpriteRenderer _sr;
        private static readonly int AnimSpeed = Animator.StringToHash("Speed");
        private static readonly int AnimDead = Animator.StringToHash("Dead");

        private float _ac;
        
        private void Awake()
        {
            if (_rigid == null) _rigid = GetComponent<Rigidbody2D>();
            if (_anim == null) _anim = GetComponent<Animator>();
            if (_sr == null) _sr = GetComponent<SpriteRenderer>();

            
        }

        private void Start()
        {
            StartCoroutine(StateSwitch());
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.GetComponent<SantaControl>().state == SantaState.MainSanta &&
                other.transform.CompareTag("FollowArea"))
                state = SantaState.FollowMain;
        }

        private void OnTriggerEnter(Collider other)
        {
            
            
                state = SantaState.Idle;
                Debug.Log("충돌!");
            
        }
        
        private void FixedUpdate()
        {
            
        }

        private IEnumerator StateSwitch()
        {
            while (isAlive)
            {
                switch (state)
                {
                    case SantaState.Idle:
                        yield return SantaIdle();
                        break;
                    case SantaState.MainSanta:
                        break;
                    case SantaState.FollowMain:
                        break;
                    case SantaState.Attacking:
                        break;
                    case SantaState.Dead:
                        
                        break;
                }
            }
        }

        private IEnumerator SantaIdle()
        {
            yield break;
        }
        private IEnumerator SantaMain()
        {
            yield break;

        }
        private IEnumerator SantaFollow()
        {
            yield break;

        }
        private IEnumerator SantaAttack()
        {
            yield break;

        }
        private IEnumerator SantaSkill()
        {
            yield break;

        }
        private IEnumerator SantaDead()
        {
            yield break;

        }
    }
}
