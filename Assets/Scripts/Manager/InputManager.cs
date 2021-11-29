using System;
using UnityEngine;

namespace Manager
{
    public class InputManager : Singleton<InputManager>
    {
        public Joystick joystick;
        public ButtonHandler buttonFire;
    
        public static float MoveHorizontal;
        public static float MoveVertical;

        public static bool IsPressFire;

        private void Start()
        {
#if !(UNITY_ANDROID || UNITY_IOS)
            Destroy(joystick.gameObject);
            Destroy(buttonFire.gameObject);
#else
            joystick.gameObject.SetActive(true);
            buttonFire.gameObject.SetActive(true);
#endif
        }

        private void Update()
        {
#if UNITY_ANDROID || UNITY_IOS
        MoveHorizontal = joystick.Horizontal;
        MoveVertical = joystick.Vertical;
        IsPressFire = buttonFire.IsPressed;
#else
            MoveHorizontal = Input.GetAxis("Horizontal");
            MoveVertical = Input.GetAxis("Vertical");
            IsPressFire = Input.GetButtonDown("Fire1");
            
            if (Input.GetKeyUp(KeyCode.Escape))
                GameManager.Instance.Pause();
            
#endif
        }

        private void LateUpdate()
        {
#if UNITY_ANDROID || UNITY_IOS
#else
            IsPressFire = false;
#endif
        }
    }
}
