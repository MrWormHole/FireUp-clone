using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeradithAI : MonoBehaviour {

    GameObject player;
    HexagonManager hexagonManager;
    PlayerScript playerScript;
    MenuManager MM;
    
    static float time_AI = 0.0f;

    bool once = true;
	
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        MM = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>();
        playerScript = player.GetComponent<PlayerScript>();
	}
	
	
	void FixedUpdate () {
        if (MM.gameStarted && once)
        {
            hexagonManager = GameObject.FindGameObjectWithTag("HexagonManager").GetComponent<HexagonManager>();
            once = false;
        }
        if (!playerScript.shackledPlayer) 
        {
            int[] copyArrForCheat = hexagonManager.ReturnHPListOfHexagons();
            int minIndex = ReturnMinIndexFromArray(copyArrForCheat);
            MoveAI(minIndex);
        }
	}
    
    int ReturnMinIndexFromArray(int[] arr)
    {
        //utility method
        int pos = 0;

        for(int i = 1; i < 4; i++)
        {
            if(arr[i] < arr[pos])
            {
                pos = i;
            }
        }
        return pos;

    }

    void MoveAI(int coordIndex)
        //coords can't be passed as variables from global scope.Runtime errors exist in my opinion
        //this coords also work properly on 480x800 well since users won't see my AI plays i see nothing wrong
    {
        if(coordIndex == 0)
        {
            player.transform.position = new Vector2(Mathf.Lerp(player.transform.position.x, -2.25f,time_AI),player.transform.position.y);
            time_AI += 0.8f * Time.deltaTime;
            if (time_AI >= 1)
            {
                time_AI = 0f;
            }
        }
        else if(coordIndex == 1)
        {
            player.transform.position = new Vector2(Mathf.Lerp(player.transform.position.x, -0.75f, time_AI), player.transform.position.y);
            time_AI += 0.8f * Time.deltaTime;
            if (time_AI >= 1)
            {
                time_AI = 0f;
            }
        }
        else if(coordIndex == 2)
        {
            player.transform.position = new Vector2(Mathf.Lerp(player.transform.position.x, 0.75f, time_AI), player.transform.position.y);
            time_AI += 0.8f * Time.deltaTime;
            if (time_AI >= 1)
            {
                time_AI = 0f;
            }
        }
        else if(coordIndex == 3)
        {
            player.transform.position = new Vector2(Mathf.Lerp(player.transform.position.x, 2.25f, time_AI), player.transform.position.y);
            time_AI += 0.8f * Time.deltaTime;
            if(time_AI >= 1)
            {
                time_AI = 0f;
            }
        }
        else
        {
            Debug.Log("Information hasn't been provided");
        }
    }
}
