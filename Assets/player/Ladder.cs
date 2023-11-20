using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float climbSpeed = 5f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FPSController playerController = other.GetComponent<FPSController>();

            if (playerController != null && Input.GetKey(KeyCode.W))
            {
                playerController.ClimbLadder(climbSpeed);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FPSController playerController = other.GetComponent<FPSController>();

            if (playerController != null)
            {
                playerController.StopClimbing();
            }
        }
    }
}
