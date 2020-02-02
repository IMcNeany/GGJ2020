using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.HasComponent<Door>())
        {
            if (other.GetComponent<Door>().isUnlocked)
            {
                var animator = other.GetComponent<Animator>();

                animator.SetBool("IsPlayerStanding", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.HasComponent<Door>())
        {
            if (other.GetComponent<Door>().isUnlocked)
            {
                var animator = other.GetComponent<Animator>();

                animator.SetBool("IsPlayerStanding", false);
            }

        }
    }
}