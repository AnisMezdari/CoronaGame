using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimator : MonoBehaviour
{
    zombieBehavior zombieBehavior;
    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        zombieBehavior = this.GetComponent<zombieBehavior>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        animator.SetBool("moving", zombieBehavior.errant);
        animator.SetBool("chasing", !zombieBehavior.errant);

    }
}
