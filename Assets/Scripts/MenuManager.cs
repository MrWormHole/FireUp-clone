using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    //Turn them into texts only for optimazation??? maybe you will use text.enabled = true or false
    public GameObject GameTitle; //this one contains 2 seperate texts
    public GameObject ShopTitle;
    public GameObject LeaderboardTitle;
    public GameObject SettingsTitle;
    ////////////////////////////////
    
    public RectTransform ButtonGroup; //if game starts move it through right side of invisible screen
    public GameObject PlayButton;
    public GameObject FingerAndCircle; //if game starts set it inactive[OPTIONAL IF PLAYER IS PLAYING AS FIRST TIME]
    public RectTransform FingerAndCircleRectTransform; //even if you set FingerAndCircle inactive make this one stop too
    public GameObject GamePanel;
    public GameObject BackButton;
    public GameObject PlayerShip;
    public GameObject HexagonManager;
    public GameObject LaserBeamGarbageCollector;

    public Text scoreText; //instead of this use the gameobject.?It is better?        PRACTICAL WAY
    public GameObject highScoreObj; //instead of this use the text.?It is better?        OPTIMIZED WAY
    private int score;
    private int highScore;
    /// <summary>
    /// 4 FLOATS ARE NOT NECESARRY BELOW
    /// </summary>
    public int startingPositionXofButtonGroup = 360;
    public int stoppingPositionXofButtonGroup = 220;
    public int startingPositionXofFingerAndCircle = 160;
    public int stoppingPositionXofFingerAndCircle = -160;

    static float t1 = 0.0f;
    static float t2 = 0.0f;
    static float t3 = 0.0f;

    public bool gameStarted = false;
    public bool showTutorial = true;


    void Start () {
        scoreText.text = null; //you will write this line for restart too !!!but i don't like this line honestly do something different?
        score = 0; //you will write this line for restart too
        highScoreObj.GetComponent<Text>().text += " " + PlayerPrefs.GetInt("HIGHSCORE", 0).ToString(); //to clear up key PlayerPrefs.DeleteKey("");
                                                                                                        //to clear all PlayerPrefs.DeletAll();
        //ButtonGroup.localPosition = new Vector2(360, 160); //unnecessary 
        //FingerAndCircleRectTransform.localPosition = new Vector2(160,-360); //unnecessary 
         
        shacklePlayer(); //dont let player shoot and move
	}

    void Update()
    {
        if (!gameStarted)
        {
            playButtonAnimation(1);
            //LaserBeamGarbageCollector.SetActive(false);
            if (showTutorial)
            {
                playFingerAnimation();
            }  
        }
        else if (gameStarted)
        {
            playButtonAnimation(-1);
            LaserBeamGarbageCollector.SetActive(true); //because it holds the song
        }

    }

    public int returnHighScore()
    {
        return highScore;
    }

    public void updateScoreTextGUI(int i)
    {
        score += i;
        scoreText.text = score.ToString();
    }

    public void updateHighScoreTextGUI()
    {
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HIGHSCORE", highScore);
            //highScoreObj.GetComponent<Text>().text += " " + PlayerPrefs.GetInt("HIGHSCORE", 0).ToString(); we will need this later
        }
    }

    ///ANIMATIONS BELOW
    public void playButtonAnimation(int state)
    {
        //when player is entering menu
        if (state == 1)
        {
            ButtonGroup.localPosition = new Vector2(Mathf.Lerp(startingPositionXofButtonGroup, stoppingPositionXofButtonGroup, t1), 160); //i assume 160 also will make problem
            t1 += 1f * Time.deltaTime;
        }
        //when player is leaving menu
        else if(state == -1)
        {
            ButtonGroup.localPosition = new Vector2(Mathf.Lerp(stoppingPositionXofButtonGroup, startingPositionXofButtonGroup, t2), 160); //i assume 160 also will make problem
            t2 += 1f * Time.deltaTime;
        }

    }

    public void playFingerAnimation()
    {
        //simple animation for finger
        FingerAndCircleRectTransform.localPosition = new Vector2(Mathf.Lerp(startingPositionXofFingerAndCircle, stoppingPositionXofFingerAndCircle, t3), -375); // !problem on -375 and -360
        t3 += 0.6f * Time.deltaTime;
        if (t3 >= 1.0f)
        {
            int temp = stoppingPositionXofFingerAndCircle;
            stoppingPositionXofFingerAndCircle = startingPositionXofFingerAndCircle;
            startingPositionXofFingerAndCircle = temp;
            t3 = 0.0f;
        }  
    }
    ///ANIMATIONS ABOVE
 
    ///BUTTONS BELOW
    public void onClickShopButton()
    {  
        applyColorForPanelThenShowPanel("shop");

        //playButtonAnimation(-1);
        //FingerAndCircle.SetActive(false);
        //PlayButton.SetActive(false);
    }

    public void onClickLeaderboardButton()
    {
        applyColorForPanelThenShowPanel("leaderboard");
        highScoreObj.SetActive(true);
        //playButtonAnimation(-1);
        //FingerAndCircle.SetActive(false);
        //PlayButton.SetActive(false);
    }

    public void onClickSettingsButton()
    {
        applyColorForPanelThenShowPanel("settings");
        //playButtonAnimation(-1);
        //FingerAndCircle.SetActive(false);
        //PlayButton.SetActive(false);
    }

    public void onClickStartGameButton()
    {
        GameTitle.SetActive(false);
        FingerAndCircle.SetActive(false);
        PlayButton.SetActive(false);
        scoreText.text = "0";
        gameStarted = true;
        unshacklePlayer(); //let player shoot and move
        //makeItRain();
        HexagonManager.SetActive(true); //delete this from here

    } 

    public void onClickBackButton()
    {
        GamePanel.SetActive(false);
        //Lazy code below
        ShopTitle.SetActive(false);
        LeaderboardTitle.SetActive(false);
        SettingsTitle.SetActive(false);
        BackButton.SetActive(false);
        highScoreObj.SetActive(false);
        //Lazy code above
        //After coding this lazy shit code i think i need container for each section.I will make it better next time with container
        PlayerShip.SetActive(true);     
        GameTitle.SetActive(true);
        FingerAndCircle.SetActive(true);
        PlayButton.SetActive(true);
    }
    ///BUTTONS ABOVE

    public void shacklePlayer()
    {
        PlayerScript PS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        PS.shackledPlayer = true;
    }

    public void unshacklePlayer()
    {
        PlayerScript PS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        PS.shackledPlayer = false;
    }

    public void applyColorForPanelThenShowPanel(string s)
    {
        PlayerShip.SetActive(false);
        BackButton.SetActive(true);
        if(s == "shop")
        {
            Debug.Log("this is shop");
            //Color color_shop = new Color32(0x28, 0x28, 0x28, 0x28); weird
            Color color_shop = new Color32(0x9B, 0x59, 0xB6, 0xFF);
            GamePanel.GetComponent<Image>().color = color_shop;
            GamePanel.SetActive(true);
            ShopTitle.SetActive(true);
        }
        else if(s == "leaderboard")
        {
            Debug.Log("this is leaderboard");
            //Color color_leaderboard = new Color32(0x28, 0x28, 0x28, 0x28); weird
            Color color_leaderboard = new Color32(0x2E, 0xCC, 0x71, 0xFF);
            GamePanel.GetComponent<Image>().color = color_leaderboard;
            GamePanel.SetActive(true);
            LeaderboardTitle.SetActive(true);
        }
        else if(s == "settings")
        {
            Debug.Log("this is settings");
            //Color color_settings = new Color32(0x28, 0x28, 0x28, 0x28); weird
            Color color_settings = new Color32(0x34, 0x98, 0xDB, 0xFF);
            GamePanel.GetComponent<Image>().color = color_settings;
            GamePanel.SetActive(true);
            SettingsTitle.SetActive(true);
        }


    }

    //NOT needed currently but we will need in the future because RESTART
    /*public void makeItRain()
    {
        HexagonManager = GameObject.FindGameObjectWithTag("HexagonManager");
        HexagonManager.SetActive(true);
    }*/

    

}
