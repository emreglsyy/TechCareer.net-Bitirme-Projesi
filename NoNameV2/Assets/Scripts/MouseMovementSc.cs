using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovementSc : MonoBehaviour
{
    public float mouseSensivity = 300f;
    float xRotation = 0f;
    float yRotation = 0f;

    private void Start()
    {
        //fareyi ekran�n ortas�na sabitlemek ve g�r�nmez yapmak i�in(ba�lang��tan itibaren �al���r.)
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {

        //mouse x ve y ye gerekli hareket atamalar�  (eksenler arac�l��� ile)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;
        //mouse y yukar� a��a��y� temsil eder mouse x sa� sol temsil eder

        //yukar� a��a�� x ekseninde olur ve - de�erle �arp�l�r(pozitif olmas� i�in)
        xRotation -= mouseY;

        //kameray� 90 derece yukar� ve a�a�� s�n�rland�rma 

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);




    }
}
