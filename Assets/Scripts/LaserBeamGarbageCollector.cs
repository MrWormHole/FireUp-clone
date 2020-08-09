using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamGarbageCollector : MonoBehaviour {

    public AudioSource auSo; //this holds gameplay track
    bool once = true;

    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (once)
            {
                once = false;
                auSo.Play();
            }
        }
        //add here if the game is over stop the song
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "laser_37")
        {
            Destroy(col.gameObject);
        }
    }

    
    
}
