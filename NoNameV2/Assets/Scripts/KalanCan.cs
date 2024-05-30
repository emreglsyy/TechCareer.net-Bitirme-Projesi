using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KalanCan : MonoBehaviour
{
    public static int oyuncuCan = 100;
    public int icCan;
    public GameObject KalanCanText;


    private void Update()
    {
        icCan = oyuncuCan;
        KalanCanText.GetComponent<Text>().text = "Kalan Can : " + icCan;  

        if (icCan == 0)
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}
