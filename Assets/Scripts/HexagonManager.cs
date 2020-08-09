using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonManager : MonoBehaviour {

    private GameObject[] hexagons = new GameObject[6];
    private byte[] randomNumbersList = new byte[4];
    private byte[] randomNumbersListSorted = new byte[4];

    private int[] hexagonsHPNumbersList = new int[4]; 

    private GameObject spyMidPoint;

    private byte hexagonSpeed = 3;
    HexagonScript hexagonScript;

    public float hexagonSizeConstant;
    public float halfScreenWidth;


    void Start() {
        hexagons = GameObject.FindGameObjectsWithTag("hexagon"); 
        spyMidPoint = GameObject.FindGameObjectWithTag("middlepoint");
        Vector2 bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Debug.Log("Width:" + bounds.x);
        halfScreenWidth = bounds.x;
        hexagonSizeConstant = 2 * bounds.x / 8;
        ChangeHexagonSizesAccordingToResolution(hexagons);
        CreateHexagonSeries();

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Time.timeScale = 1;
        }
    }

    void ChangeHexagonSizesAccordingToResolution(GameObject[] objects)
    {
        for(int i = 0; i < 6; i++)
        {
            objects[i].transform.localScale = new Vector2(hexagonSizeConstant,hexagonSizeConstant);
        }
    }

    void GiveHPNumberToHexagon(GameObject g,int index)
    {
        hexagonScript = g.GetComponent<HexagonScript>();
        hexagonsHPNumbersList[3-index] = Random.Range(1, 10); //later this will be improved to 3 leaf tree algorithm model
                                                       //we might also need 2d array for storing old hp informations
        hexagonScript.hitHP = hexagonsHPNumbersList[3-index];

    }

    void CreateHexagonSeries()
    {
  
        for (int i = 0; i < 4; i++)
        {
            randomNumbersList[i] = (byte)Random.Range(0, 6);
        }

        RNGColorBalancer();

        for (int i = 0; i < 4; i++)
        {
            //-2.25f + (i * 1.5f) this is only for 480x800
            GameObject hexagonTile = Instantiate(hexagons[randomNumbersList[i]], new Vector2(halfScreenWidth - (hexagonSizeConstant * (2*i+1)), 7f), Quaternion.identity); //j outer loop and j*2 on Vec2.y for test
            GiveHPNumberToHexagon(hexagonTile,i);
            hexagonTile.GetComponent<Rigidbody2D>().velocity = Vector2.down * hexagonSpeed;
        }
        PrintArray(hexagonsHPNumbersList); //debugging purpose

        GameObject tempSpyMidPoint = Instantiate(spyMidPoint, new Vector2(0, 7f), Quaternion.identity);
        tempSpyMidPoint.GetComponent<Rigidbody2D>().velocity = Vector2.down * hexagonSpeed;
        
    }

    public void RNGColorBalancer()
    {
        //we have 4 hexagons and we dont want 3 same colors in group of 4 hexagons

        randomNumbersListSorted = BubbleSort(randomNumbersList);

        while( (randomNumbersListSorted[0]==randomNumbersListSorted[1] && randomNumbersListSorted[1] == randomNumbersListSorted[2]) || (randomNumbersListSorted[1] == randomNumbersListSorted[2] && randomNumbersListSorted[2] == randomNumbersListSorted[3]))
        {
            //-xxx or xxx-

            randomNumbersListSorted[1] = (byte)Random.Range(0, 6); // -xxx,xxx-
            randomNumbersListSorted[2] = (byte)Random.Range(0, 6); //-xxx , xxx-

            BubbleSort(randomNumbersListSorted);
        }

        randomNumbersList = FisherShuffle(randomNumbersListSorted);
    }
   
    public byte[] BubbleSort(byte[] arr)
    {
        //utility method
        byte[] b = new byte[4];
        byte n = 4; //length of randomNumbersList to just remember

        for (int i = 0; i < n; i++)
        {
            for (int j = 1; j < (n - i); j++)
            {

                if (arr[j - 1] > arr[j])
                {
                    byte temp = arr[j - 1];
                    arr[j - 1] = arr[j];
                    arr[j] = temp;
                }

            }
        }

        for(int i = 0; i < 4; i++)
        {
            b[i] = arr[i];
        }

        return b;
    }

    public byte[] FisherShuffle(byte[] arr)
    {
        //utility method
        byte[] y = new byte[4];
        byte n = 4; //length of randomNumbersListSorted to just remember
        
        for (int i = 0; i < n; i++)
        {
            int k = Random.Range(0, i + 1);
            byte temp = arr[k];
            arr[k] = arr[i];
            arr[i] = temp;


        }

        for (int i = 0; i < 4; i++)
        {
            y[i] = arr[i];
        }

        return y;

    }

    public void PrintArray(int[] arr)
    {
        //For debugging purposes
        Debug.Log("First term:"+arr[0] +" Second term:" + arr[1] + " Third term:" + arr[2] +" Forth term:"+ arr[3]);
        
    }
    
    public int[] ReturnHPListOfHexagons()
    {
        return hexagonsHPNumbersList;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "middlepoint")
        {
            Destroy(col.gameObject);
            CreateHexagonSeries();
        }
        if (col.gameObject.tag == "hexagon")
        {
            Destroy(col.gameObject);
        }
    }



}
