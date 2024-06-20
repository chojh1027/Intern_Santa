using System;
using UnityEngine;

namespace PlayerScripts
{
    public enum SantaState
    {
        Idle,
        MainSanta,
        FollowMain,
        Attacking,
        Dead
    }
    public class PlayerControl : MonoBehaviour
    {
        public static PlayerControl Instance;
        public PlayerControls inputActions;

        //public GameObject mainCam;
        
        public GameObject[] santa;
        public int mainSantaIndex;

        public Vector2 inputVec;
        
        private void OnEnable()
        {
            Instance = this;
            
            //입력이 일어날 때 inputVec에 값 넣어주기
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.MoveInput.performed += inputActions => inputVec = inputActions.ReadValue<Vector2>();
            }
            inputActions.Enable();

            mainSantaIndex = 0;
            
            //메인 산타 전환 후 카메라 follow 설정 로직
            //if(mainCam == null) mainCam = GameObject.FindWithTag("MainCamera");
        }

        private void Start()
        {
            ChangeMainSanta(0);
        }

        private void ChangeMainSanta(int index)
        {
            santa[mainSantaIndex].GetComponent<SantaControl>().state = SantaState.Idle;
            santa[index].GetComponent<SantaControl>().state = SantaState.MainSanta;
        }
    }
}
