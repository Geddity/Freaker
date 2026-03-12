using Spine.Unity;
using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.CullingGroup;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] bool move;
    [SerializeField] bool attack;

    public enum CharacterState
    {
        None,
        Idle,
        Walk,
        Run,
        Crouch,
        Attack,
        TakeDamage,
        Defeated
    }

    [Header("Moving")]
    public float walkSpeed = 1.5f;
    public float runSpeed = 7f;
    

    [Header("Animation")]
    public AnimationHandle animationHandle;

    CharacterState previousState, currentState;

    private void Start()
    {
        currentState = CharacterState.Idle;
    }

    void Update()
    {
        bool stateChanged = previousState != currentState;
        previousState = currentState;


        if (stateChanged) 
        { 
            HandleStateChanged();

            //Debug.Log(GetComponentInParent<CharacterObject>().Name + " " + currentState);
        }
    }

    void HandleStateChanged()
    {
        // When the state changes, notify the animation handle of the new state.
        string stateName = null;
        switch (currentState)
        {
            case CharacterState.Idle:
                stateName = "idle";
                break;
            case CharacterState.Walk:
                stateName = "walk";
                break;
            case CharacterState.Run:
                stateName = "run";
                break;
            case CharacterState.Crouch:
                stateName = "crouch";
                break;
            case CharacterState.TakeDamage:
                stateName = "take_damage";
                break;
            case CharacterState.Defeated:
                stateName = "defeated";
                break;
            case CharacterState.Attack:
                stateName = "attack";
                break;
            default:
                break;
        }

        if (currentState == CharacterState.Attack)
        {
            //animationHandle.PlayOneShotForState(stateName, 0);
            Invoke("BackToIdle", 1);
        }
        else if(currentState == CharacterState.Defeated || currentState == CharacterState.TakeDamage)
        {
            StartCoroutine(AnimDelay());
        }
        else
        {
            animationHandle.PlayAnimationForState(stateName, 0);
        }

        IEnumerator AnimDelay()
        {
            yield return new WaitForSeconds(0.5f);
            if(currentState == CharacterState.TakeDamage) 
            {
                //animationHandle.PlayOneShotForState(stateName, 0);
                yield return new WaitForSeconds(0.5f);
                currentState = CharacterState.Idle;
            }
            if(currentState == CharacterState.Defeated)
            {
                //animationHandle.PlayOneShotForStateNoLoop(stateName, 0);
            }
        }
    }

    void BackToIdle()
    {
        currentState = CharacterState.Idle;
    }
    

    public void StartMoving()
    {
        move = true;
        currentState = CharacterState.Run;
    }

    public void StopMoving()
    {
        move = false;
        currentState = CharacterState.Idle;
    }

    public void Attack()
    {
        attack = true;
        
        currentState = CharacterState.Attack;
      
    }

    public void TakeDamage()
    {
        currentState = CharacterState.TakeDamage;
    }

    public void Defeated()
    {
        currentState = CharacterState.Defeated;
    }

    private void LateUpdate()
    {
        if(attack == true)
        {
            attack = false;
        }

        //Debug.Log(GetComponentInParent<CharacterObject>().Name + " " + currentState);
    }
}
