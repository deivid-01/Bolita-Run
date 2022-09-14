using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    // Start is called before the first frame update
    public float increase;
    public bool gameStart;
    public Animator anim;
    
    void Start()
    {
        gameStart = false;
        increase = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //ControlAnimation
        if (Input.GetKeyDown(KeyCode.Space) && gameStart==false)
        {
            anim.SetInteger("Run", 0);
            gameStart = true;

        }
        if (gameStart == true)
        { if (Input.GetKey(KeyCode.LeftArrow))
            {
                anim.SetInteger("Run", -1);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                anim.SetInteger("Run", 1);
            }
            else 
                anim.SetInteger("Run", 0);
        }
        anim.SetFloat("speedPlus",increase);
        increase += 0.001f;
        }
    
}
