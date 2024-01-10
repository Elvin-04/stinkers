using System.Xml.Schema;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Spray : MonoBehaviour
{
    public bool enemyInRange;

    [Header("References")]
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private Image sprayBar;
    [SerializeField] private Image reloadCircle;
    [SerializeField] private GameObject reloadButton;

    [Header("Parameters")]
    [SerializeField] private float sprayTime = 20f;
    [SerializeField] private float reloadTime = 3f;

    private float currentSprayTime = 0.0f;
    private float currentReloadTime = 0.0f;
    private bool isReloading = false;

    private void Start()
    {
        sprayBar.transform.parent.gameObject.SetActive(true);
        reloadCircle.transform.gameObject.SetActive(false);
        reloadButton.SetActive(false);

        if (particles != null)
            particles.Stop();
    }


    private void FixedUpdate()
    {
        if(isReloading)
        {
            reloadCircle.transform.gameObject.SetActive(true);
            currentReloadTime += Time.deltaTime;
            reloadCircle.fillAmount = currentReloadTime / reloadTime;
            if (currentReloadTime > reloadTime)
            {
                currentReloadTime = 0.0f;
                currentSprayTime = 0.0f;
                isReloading = false;
                reloadCircle.transform.gameObject.SetActive(false);
                sprayBar.transform.parent.gameObject.SetActive(true);
                reloadCircle.fillAmount = 0;
            }
        }


        if (particles != null)
        {
            if (enemyInRange && !isReloading)
            {
                if(currentSprayTime < sprayTime && !isReloading)
                {
                    particles.Play();
                    currentSprayTime += Time.deltaTime;
                    sprayBar.fillAmount = 1f - currentSprayTime / sprayTime;
                }
                else
                {
                    reloadButton.SetActive(true);
                    sprayBar.transform.parent.gameObject.SetActive(false);
                    particles.Stop();
                }
            }
            else
            {
                particles.Stop();
            }
        }
        

        if(Input.GetKeyDown(KeyCode.V))
        {
            Reload();
        }

    }

    public void Reload()
    { 
        reloadButton.SetActive(false);
        isReloading = true;
    }
}
