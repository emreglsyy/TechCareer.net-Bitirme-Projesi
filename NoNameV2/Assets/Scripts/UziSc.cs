using System.Collections;
using TMPro;
using UnityEngine;

public class UziSc : MonoBehaviour
{

    public static UziSc instance { get; set; }
    [Header("Ateþleme")]
    [SerializeField] private GameObject trigger;
    [SerializeField] private int damage = 1;
    public bool isFired = false;
    public bool canFire = true;
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
    private int icCephane = 30;

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
        if (Input.GetButton("Fire1"))
        {
            if (canFire)
            {
                StartCoroutine(fireBreak());
                //icCephane--;

                if (icCephane == 0)
                {
                    StartCoroutine(reloadBreak());
                    animator.SetBool("canReload", true);
                    
                }
                else if (icCephane < 0)
                {
                    fireSound.Stop();
                    animator.SetBool("isTriggered", false);
                    icCephane = 30;
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
            Debug.Log("Hit: " + hit.transform.name);
            hit.transform.SendMessage("Enemyy", damage, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            Debug.Log("Missed");
        }
        icCephane--;
        yield return new WaitForSeconds(.2f);

        canFire = true;
        animator.SetBool("isTriggered", false);
        isFired = false;
    }

    IEnumerator reloadBreak()
    {
        Debug.Log("1");
        reloadSound.Play();
        animator.SetBool("canReload", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("canReload", false);

        icCephane = 30;
        Debug.Log("2");
    }
}
