using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public bool isAlive; //????????????? use game over from hexagon script
    public bool shackledPlayer; //this is for playing too much elder scrolls.God damn imagination

    private byte playerSpeed = 2;
    private byte beamSpeed = 4;
    private float initialTime = 0.0f;
    private float repeatTime = 0.5f;
    bool once = true; //? this is weird but works for now
    public GameObject laserBeam;
    MenuManager MM;

    void Start () {
    
        isAlive = true; //not good pratice here we might need to set inactive player while on menu
        laserBeam = GameObject.FindGameObjectWithTag("laser_37");

	}
	
	void Update () {
        
        if (isAlive && !shackledPlayer)
        {
            MoveForKeyboard(); //move
            AllowShooting(); //shoot      
        }

	} 

    public void MoveForKeyboard() {

        float inputX = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * inputX * playerSpeed * Time.deltaTime);
        //Movement for mobile has been added to Touch Controller

    }

    public void ShootForKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject copyBeam = Instantiate(laserBeam, new Vector2(transform.position.x , transform.position.y + 1 ) ,Quaternion.identity);
            copyBeam.GetComponent<Rigidbody2D>().velocity = Vector2.up * beamSpeed;
        }
    }

    public void Shoot()
    {
        GameObject copyBeam = Instantiate(laserBeam, new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);
        copyBeam.GetComponent<Rigidbody2D>().velocity = Vector2.up * beamSpeed;
    }

    public void AllowShooting()
    {
        MenuManager MM = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>();
        if (MM.gameStarted && !shackledPlayer && laserBeam != null)
        {
            if(once == true)
            {   Debug.Log("Shooting activity has started");
                InvokeRepeating("Shoot", initialTime, repeatTime);
                once = false;
            }
            //CancelInvoke(); for powerups we will need this function later
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "hexagon")
        {
            Debug.Log("GAME OVER");
            MenuManager MM = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>();
            MM.updateHighScoreTextGUI();
            Time.timeScale = 0; //we stop the time right here
            //until i get FX for death i will use time stopper that works for sure but feels poor man jeez
        }
    }

    IEnumerator JustWaitLittleBit()
    {
        //debugging purposes
        yield return new WaitForSeconds(0.2f);
    }







}
