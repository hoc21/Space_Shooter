using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] float moveSpeed =  5f;

    Animator  animator;
    void Start()
    {
        animator = FindObjectOfType<Animator>();
    }

    public void Up()
    {
        
        animator.SetBool("up",true);
    }
    public void Down(){
        animator.SetBool("up",false);
    } 
}

    

