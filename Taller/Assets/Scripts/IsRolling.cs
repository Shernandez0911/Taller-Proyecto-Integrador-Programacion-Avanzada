using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsRolling : MonoBehaviour
    {
    public static bool IsRollingBool;
    public float verificador = 0;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void IsRollingfunction()
    { 
        if (Input.GetKey("z") && !CheckGround.IsGround|(Input.GetKey("z") && verificador == 2)) {
            IsRollingBool = true;
            animator.SetTrigger("IsRolling 0");
            animator.SetTrigger("IsJumping");
            verificador = 1;
            } 
        else if ((Input.GetKey("z") && CheckGround.IsGround)) {
               IsRollingBool = true;
               animator.SetTrigger("IsRolling 0");
               verificador = 2;
        }
        }
 }
    
     




