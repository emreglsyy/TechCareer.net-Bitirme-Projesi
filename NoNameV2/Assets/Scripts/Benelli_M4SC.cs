using System.Collections;
using TMPro;
using UnityEngine;

public class Benelli_M4SC : MonoBehaviour
{
    public static Benelli_M4SC instance { get; set; }

    [Header("Ateþleme")]
    [SerializeField] private GameObject trigger;
    [SerializeField] private int damage = 5;
    public bool isFired = false;
    public bool canFire = true;
    private bool isReloading = false; // Yeni eklenen flag
    [SerializeField] private float fireRange = 100.0f; // Raycast mesafesi

    [Header("AnimasyonKontrol")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject gunUzi;

    [Header("SoundKontrol")]
    [SerializeField] private AudioSource fireSound;
    [SerializeField] private AudioSource reloadSound;

    [Header("SilahCanvas")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI mermiText;
    private int icCephane = 4;

    private void Update()
    {
        fireSettings();
        AmmoCount();
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void fireSettings()
    {
        if (Input.GetButton("Fire1") && !isReloading)
        {
            if (canFire)
            {
                StartCoroutine(fireBreak());

                if (icCephane == 0)
                {
                    StartCoroutine(reloadBreak());
                }
            }
        }
    }

    public void AmmoCount()
    {
        mermiText.text = " " + icCephane;
    }

    IEnumerator fireBreak()
    {
        canFire = false;
        isFired = true;

        fireSound.Play();
        animator.SetBool("isTriggered", true);

        // Kamera referansý al
        Camera mainCamera = Camera.main;

        // Ekranýn ortasýndaki noktayý belirle
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        // Kamera ve ekran ortasýndan Raycast fýrlat
        Ray ray = mainCamera.ScreenPointToRay(screenCenter);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, fireRange))
        {
            
            
            hit.transform.SendMessage("Enemyy", damage, SendMessageOptions.DontRequireReceiver);
        }
        
        icCephane--;
        yield return new WaitForSeconds(.8f);

        canFire = true;
        animator.SetBool("isTriggered", false);
        isFired = false;
    }

    IEnumerator reloadBreak()
    {
        isReloading = true; // Reload iþlemi baþladý
        reloadSound.Play();
        animator.SetBool("canReload", true);
        yield return new WaitForSeconds(1f);

        animator.SetBool("canReload", false);
        icCephane = 4;
        isReloading = false; // Reload iþlemi bitti
    }
}
