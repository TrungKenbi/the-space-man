using System;
using UnityEngine;

namespace UI
{
    public class FollowCursor : MonoBehaviour
    {
        [HideInInspector]
        public Animator animClick;
        
        private Vector2 _currentMousePos;
        private static readonly int LeftMouseClick = Animator.StringToHash("LeftMouseClick");

        private void OnValidate()
        {
            if (animClick == false)
                animClick = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                animClick.SetBool(LeftMouseClick, true);

            _currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = _currentMousePos;
        }
    }
}
