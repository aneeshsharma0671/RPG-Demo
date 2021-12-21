using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIBehaviour : MonoBehaviour
{
    public Transform player;
    public bool playerInRange;
    public float visionRadius = 3f;
    public float speed = 1f;

    private void Update()
    {
        CheckPlayerInRange();
        if(playerInRange && player.GetComponent<PlayerMovement>().moving)
        {
            CombatMovment();
        }
        else
        {
            NormalMovment();
        }
    }

    private void CheckPlayerInRange()
    {
        if(Vector3.Distance(player.position,transform.position) >= visionRadius || Vector3.Distance(player.position, transform.position) < 1.5f)
        {
            playerInRange = false;
        }
        else
        {
            playerInRange = true;
        }
    }

    public virtual void NormalMovment()
    {
    }

    public virtual void CombatMovment()
    {
    }
}
