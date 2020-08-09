using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

    GameObject myPlayer;
    PlayerScript PS;

    public static float sensivity = 0.8f; //?0.6 slowest, 0.8 is default, 1.2 is fastest.Conclusion = 7 points on slider
                                          //enable or disable touch later ?? for controllers
    private float maxX;

	void Start () {
        myPlayer = GameObject.FindGameObjectWithTag("Player");
        Vector2 bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        maxX = bounds.x; //optimization const for screen
	}
    
    void Update()
    {
 
        if (myPlayer != null)
        {
            PlayerScript PS = myPlayer.GetComponent<PlayerScript>();
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && !PS.shackledPlayer)
            {
                // Get movement of the finger since last frame
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                //Move object on x axis
                myPlayer.transform.Translate(touchDeltaPosition.x * Time.deltaTime * sensivity, 0 , 0);
            }
            //is this code weird or am i so high in this midnight
            //this is for optimizing which lets us remove 2 box colliders
            if (myPlayer.transform.position.x > maxX)
            {
                Vector2 temp = myPlayer.transform.position;
                temp.x = maxX;
                myPlayer.transform.position = temp;
            }
            if (myPlayer.transform.position.x < -maxX)
            {
                Vector2 temp = myPlayer.transform.position;
                temp.x = -maxX;
                myPlayer.transform.position = temp;
            }
        }
        
    }

}
