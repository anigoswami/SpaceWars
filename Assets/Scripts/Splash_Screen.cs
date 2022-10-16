using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Splash_Screen : MonoBehaviour
{
    public bool isSplash;
    

    private void Start()
    {
        isSplash = true;
        
    }
    
    public void toggleSplash()
    {
        if (isSplash)
        {
            this.gameObject.SetActive(false);
            
        }
        else
        {
            this.gameObject.SetActive(true);
            
        }

        isSplash = !isSplash;
    } 
    
}
