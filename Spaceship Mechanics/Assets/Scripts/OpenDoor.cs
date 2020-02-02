using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private bool do_once = false;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.HasComponent<Door>())
        {
            if (other.GetComponent<Door>().isUnlocked)
            {
                var animator = other.GetComponent<Animator>();

                animator.SetBool("IsPlayerStanding", true);
                if(do_once == false)
                {
                    do_once = true;
                    GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
                }
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
                do_once = false;
            }

        }
    }
}