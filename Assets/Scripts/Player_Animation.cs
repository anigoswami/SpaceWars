using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            _anim.SetBool("Turn_Left",true);
            _anim.SetBool("Turn_Right", false);
        }
        else if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            _anim.SetBool("Turn_Left", false);
            _anim.SetBool("Turn_Right", false);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            _anim.SetBool("Turn_Right", true);
            _anim.SetBool("Turn_Left", false);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            _anim.SetBool("Turn_Right", false);
            _anim.SetBool("Turn_Left", false);
        }
    }
}
