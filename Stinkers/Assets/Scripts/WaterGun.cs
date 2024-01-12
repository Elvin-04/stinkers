using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaterGun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform canon;

    [Header("References")]
    [SerializeField] private Image reloadImage;
    [SerializeField] private TextMeshProUGUI bulletText;

    [Header("Parameters")]
    public float range = 5f;
    public float firerate = 3f;
    public float damages = 0.8f;
    [SerializeField] private int maxMunition = 30;
    [SerializeField] private float timeToReload;
    [SerializeField] private LayerMask layerDetection;
    

    private Transform enemy;

    private float currentTimerFirerate = 0.0f;
    private float currentTimerReload = 0.0f;
    private int actMunition;
    private bool canShoot = false;
    private bool isReloading = false;

    private void Start()
    {
        actMunition = maxMunition;
    }

    private void FixedUpdate()
    {
        if(!canShoot)
        {
            currentTimerFirerate += Time.deltaTime;
            if(currentTimerFirerate >= 1f / firerate)
            {
                canShoot = true;
                currentTimerFirerate = 0.0f;
            }
        }

        ReloadingCheck();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, layerDetection);
        
        if (hitColliders.Length > 0)
        {
            foreach (Collider collider in hitColliders)
            {
                if (enemy == null && !collider.transform.GetComponent<Stinker>().IsClean()
                    || enemy != null && Vector3.Distance(collider.transform.position, GameManager.instance.endLevel.position) <= Vector3.Distance(enemy.position, GameManager.instance.endLevel.position)
                    && !collider.transform.GetComponent<Stinker>().IsClean())
                {
                    enemy = collider.transform;
                }
            }
        }

        if (canShoot && !isReloading && enemy != null)
        {
            Shoot();
            canShoot = false;
        }

        if(enemy != null)
        {
            var targetRotation = Quaternion.LookRotation(enemy.transform.position - canon.transform.position);
            canon.transform.rotation = Quaternion.Slerp(canon.transform.rotation, targetRotation, 10f * Time.deltaTime);

            if(Vector3.Distance(enemy.transform.position, transform.position) >= range || enemy.GetComponent<Stinker>().IsClean())
            {
                enemy = null;
            }
        }
    }

    public void SetEnemy(Transform newEnemy)
    {
        enemy = newEnemy;
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = spawnPosition.position;
        bullet.GetComponent<WaterGunBullet>().SetDestination(enemy);
        bullet.GetComponent<WaterGunBullet>().gun = this;

        actMunition--;
        bulletText.text = actMunition.ToString();
        if(actMunition <= 0)
        {
            isReloading = true;
        }
    }

    private void Reload()
    {
        actMunition = maxMunition;
        reloadImage.transform.gameObject.SetActive(false);
        bulletText.transform.gameObject.SetActive(true);
        bulletText.text = actMunition.ToString();
        reloadImage.fillAmount = 0f;
    }

    public void Upgrade()
    {
        firerate += 1;
        maxMunition += 5;
        damages += 0.5f;
    }

    private void ReloadingCheck()
    {
        if (isReloading)
        {
            reloadImage.transform.gameObject.SetActive(true);
            bulletText.transform.gameObject.SetActive(false);
            currentTimerReload += Time.deltaTime;
            reloadImage.fillAmount = currentTimerReload / timeToReload;
            if (currentTimerReload >= timeToReload)
            {
                isReloading = false;
                currentTimerReload = 0.0f;
                Reload();
            }
        }
    }

}
