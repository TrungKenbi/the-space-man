using System;
using UnityEngine;

public class AutomicDoorController : MonoBehaviour
{
    public AudioClip openDoorSound;
    public AudioClip closeDoorSound;
    
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AudioSource speaker;
    [SerializeField]
    private BoxCollider2D doorCollider;

    private bool _onSensor;
    private static readonly int OnSensor = Animator.StringToHash("OnSensor");

    private void OnValidate()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        if (speaker == null)
            speaker = GetComponent<AudioSource>();
        if (doorCollider == null)
            doorCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        
        if (_onSensor)
            return;

        ChangDoorState(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        
        if (!_onSensor)
            return;

        ChangDoorState(false);
    }

    private void ChangDoorState(bool isOpen)
    {
        animator.SetBool(OnSensor, isOpen);
        _onSensor = isOpen;
    }
    
    private void ChangeDoorCollider(bool isOpen)
    {
        // doorCollider.enabled = !isOpen;
        speaker.PlayOneShot(isOpen ? openDoorSound : closeDoorSound);
    }
    
    public void OnDoorOpened()
    {
        ChangeDoorCollider(true);
    }

    public void OnDoorClosed()
    {
        ChangeDoorCollider(false);
    }

    
}
