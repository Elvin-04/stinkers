using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Shower : MonoBehaviour
{
    [SerializeField] private ParticleSystem cloud;
    [SerializeField] private ParticleSystem water;

    [SerializeField] private GameObject useButton;
    [SerializeField] private Image reloadImage;

    [Header("parameters")]
    [SerializeField] private float showerDuration;
    [SerializeField] private float showerReload;

    public bool enemyInRange = false;

    private bool canUse = true;
    private float currentReloadTimer = 0.0f;

    private void Start()
    {
        StopShower();
        useButton.SetActive(false);
        reloadImage.transform.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        TimerReload();

        if (enemyInRange && canUse)
            useButton.SetActive(true);
        else
            useButton.SetActive(false);
    }

    private void TimerReload()
    {
        if (!canUse)
        {
            reloadImage.transform.gameObject.SetActive(true);
            currentReloadTimer += Time.deltaTime;
            reloadImage.fillAmount = currentReloadTimer / showerReload;
            if(currentReloadTimer >= showerReload)
            {
                canUse = true;
                currentReloadTimer = 0.0f;
                reloadImage.transform.gameObject.SetActive(false);
            }
        }
    }

    private void StartShower()
    {
        cloud.Play();
        water.Play();
    }

    private void StopShower()
    {
        cloud.Stop();
        water.Stop();
    }

    public void Use()
    {
        StartShower();
        canUse = false;
        //Infliger dégâts aux ennemis
        StartCoroutine(useShower());
    }

    IEnumerator useShower()
    {
        yield return new WaitForSeconds(showerDuration);
        StopShower();
    }
}
