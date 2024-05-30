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
        //fareyi ekranýn ortasýna sabitlemek ve görünmez yapmak için(baþlangýçtan itibaren çalýþýr.)
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {

        //mouse x ve y ye gerekli hareket atamalarý  (eksenler aracýlýðý ile)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;
        //mouse y yukarý aþþaðýyý temsil eder mouse x sað sol temsil eder

        //yukarý aþþaðý x ekseninde olur ve - deðerle çarpýlýr(pozitif olmasý için)
        xRotation -= mouseY;

        //kamerayý 90 derece yukarý ve aþaðý sýnýrlandýrma 

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);




    }
}
