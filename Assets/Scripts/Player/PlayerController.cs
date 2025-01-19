using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Third Person Controller References
    [SerializeField]
    private Animator playerAnim;

    //Blocking Parameters
    public bool isBlocking;

    //SpecialAttack Parameters
    public bool isJumping;

    //Attack Parameters
    public bool isAttacking;
    private float timeSinceAttack;
    public int currentAttack = 0;




    private void Update()
    {
        timeSinceAttack += Time.deltaTime;

        Attack();

        Block();
        SpecialAttack();
    }


    private void Block()
    {
        if (Input.GetKey(KeyCode.Mouse1) && playerAnim.GetBool("Grounded"))
        {
            playerAnim.SetBool("Block", true);
            isBlocking = true;
        }
        else
        {
            playerAnim.SetBool("Block", false);
            isBlocking = false;
        }
    }

    public void SpecialAttack()
    {
        if (Input.GetKey(KeyCode.Mouse2) && playerAnim.GetBool("Grounded"))
        {
            playerAnim.SetBool("SpecialAttack", true);
            isJumping = true;
        }
        else
        {
            playerAnim.SetBool("SpecialAttack", false);
            isJumping = false;
        }
    }

    private void Attack()
    {

        if (Input.GetMouseButtonDown(0) && playerAnim.GetBool("Grounded") && timeSinceAttack > 0.7f)
        {

            currentAttack++;
            isAttacking = true;

            if (currentAttack > 3)
                currentAttack = 1;

            //Reset
            if (timeSinceAttack > 1.0f)
                currentAttack = 1;

            //Call Attack Triggers
            playerAnim.SetTrigger("Attack" + currentAttack);

            //Reset Timer
            timeSinceAttack = 0;
        }
    }

    //This will be used at animation event
    public void ResetAttack()
    {
        isAttacking = false;
    } 
}   