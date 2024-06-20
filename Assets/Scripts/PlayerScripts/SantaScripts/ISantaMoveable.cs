using UnityEngine;

namespace PlayerScripts.SantaScripts
{
    public interface ISantaMoveable
    {
        Rigidbody2D RB { get; set; }
        Animator ANIM { get; set; }
        SpriteRenderer SR { get; set; }
        bool IsFacingRight { get; set; }
        void MoveSanta(Vector2 velocity);
        void CheckForLeftOrRightFacing(Vector2 velocity);
    }
}
