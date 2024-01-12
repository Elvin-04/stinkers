using System.Collections.Generic;
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
    [SerializeField] private float damages = 0.1f;
    [SerializeField] private float timeBetweenEachshootScript = 0.15f;

    private float currentSprayTime = 0.0f;
    private float currentReloadTime = 0.0f;
    private float currentShootTimer = 0.0f;
    private bool isReloading = false;

    public List<Transform> ennemiesInRange;

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
                sprayBar.fillAmount = 1f - currentSprayTime / sprayTime;
                isReloading = false;
                reloadCircle.transform.gameObject.SetActive(false);
                sprayBar.transform.parent.gameObject.SetActive(true);
                reloadCircle.fillAmount = 0;
            }
        }


        if (enemyInRange && !isReloading)
        {
            if (currentSprayTime < sprayTime && !isReloading)
            {
                UpdateShooting();
                if(currentShootTimer < timeBetweenEachshootScript)
                {
                    currentShootTimer += Time.deltaTime;
                }
                else
                {
                    currentShootTimer = 0.0f;
                    foreach (Transform enemy in ennemiesInRange)
                    {
                        enemy.GetComponent<Stinker>().UpdateStinkPercentage(damages);
                    }
                }
            }
            else
            {
                currentShootTimer = 0.0f;
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

    private void UpdateShooting()
    {
        particles.Play();
        currentSprayTime += Time.deltaTime;
        sprayBar.fillAmount = 1f - currentSprayTime / sprayTime;
    }


    public void Reload()
    { 
        reloadButton.SetActive(false);
        isReloading = true;
    }
}
