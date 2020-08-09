using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexagonScript : MonoBehaviour {

    public int hitHP; //we will set private later on currently Hexagon Manager is using this directly.Apply algorithm there then do encapsulation here
    bool once;

    ParticleSystem parSys;
    SpriteRenderer spRen;
    PolygonCollider2D polyCol2D;
    public AudioSource auSo; //this holds explosion sound
    TextMesh textMesh; //stores HP

    MenuManager MM;

	void Start () {
        MM = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>();
        //hitHP = (int)Random.Range(2, 4);//beginner level sample for the player.Design algorithm for there[ALGORITHM DESIGN] inside HexagonManager
        textMesh = GetComponentInChildren<TextMesh>();
        textMesh.text = hitHP.ToString();
        parSys = GetComponentInChildren<ParticleSystem>();
        spRen = GetComponent<SpriteRenderer>();
        polyCol2D = GetComponent<PolygonCollider2D>();
	}

	void Update () {    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "laser_37" || col.gameObject.tag == "laser_18" || col.gameObject.tag == "laser_20" || col.gameObject.tag == "laser_22" || col.gameObject.tag == "laser_39" || col.gameObject.tag == "laser_41")
        {
            MM.updateScoreTextGUI(1); //each hit increases score by one
            hitHP--; //this will change later depending on laser's power i also think hitHP is a shitty name.Make it hexagonHP to show it is for singular?
            textMesh.text = hitHP.ToString(); //update hp when hit
            Destroy(col.gameObject); //destroys laser in contact
            if(hitHP <= 0)
            {
                StartCoroutine("PlayParticleFXThenVanish");
            }
        }
    }

    IEnumerator PlayParticleFXThenVanish()
    {
        if(spRen != null && polyCol2D != null && parSys != null && textMesh != null)
        {
            parSys.Play();
            auSo.Play();
            spRen.sprite = null; 
            polyCol2D.isTrigger = false;
            polyCol2D.enabled = false;
            textMesh.text = null;
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
    }

}
