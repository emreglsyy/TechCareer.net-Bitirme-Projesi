using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChoiceManager : MonoBehaviour
{
    [Header("Guns")]
    [SerializeField] GameObject shotgun;
    [SerializeField] GameObject uzi;
    [SerializeField] GameObject ak;

    void Update()
    {
        gunChoice();
    }

    public void gunChoice()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Alpha1 pressed");
            if (UziSc.instance != null)
            {
                UziSc.instance.canFire = false;
            }
            else
            {
                Debug.LogWarning("UziSc.instance is null");
            }

            if (Benelli_M4SC.instance != null)
            {
                Benelli_M4SC.instance.canFire = true;
            }
            else
            {
                Debug.LogWarning("Benelli_M4SC.instance is null");
            }

            shotgun.SetActive(true);
            uzi.SetActive(false);
            ak.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Alpha2 pressed");
            if (Benelli_M4SC.instance != null)
            {
                Benelli_M4SC.instance.canFire = false;
            }
            else
            {
                Debug.LogWarning("Benelli_M4SC.instance is null");
            }

            if (UziSc.instance != null)
            {
                UziSc.instance.canFire = true;
            }
            else
            {
                Debug.LogWarning("UziSc.instance is null");
            }

            uzi.SetActive(true);
            shotgun.SetActive(false);
            ak.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Alpha3 pressed");
            ak.SetActive(true);
            shotgun.SetActive(false);
            uzi.SetActive(false);
        }
    }
}
