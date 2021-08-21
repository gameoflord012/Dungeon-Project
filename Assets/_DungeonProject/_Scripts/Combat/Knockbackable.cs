using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockbackable : MonoBehaviour
{    
    private ActorMovement actorMovement;
    private ActorInputEvents inputEvents;

    private void Awake()
    {
        actorMovement = GetComponent<ActorMovement>();
        inputEvents = GetComponent<ActorInputEvents>();
    }

    public void Knockback(Damager damager)
    {               
        StartCoroutine(KnockbackRoutine(damager));        
    }

    private Vector3 GetSourcePosition(Damager damager)
    {
        var damageSource = damager.GetComponentInParent<ActorMovement>();
        return damageSource ? damageSource.transform.position : damager.transform.position;
    }

    private IEnumerator KnockbackRoutine(Damager damager)
    {
        actorMovement.ResetMovement();

        actorMovement.AddForce(
            (transform.position - GetSourcePosition(damager)).normalized * damager.KnockbackStrength);        

        inputEvents.enabled = false;

        yield return new WaitForSeconds(damager.WaitTimeAfterKnockingback);

        inputEvents.enabled = true;
    }
}
