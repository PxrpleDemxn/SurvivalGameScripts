using UnityEngine;

[System.Serializable]

public class Weapon
{
    public GameObject prefab;
    public string weaponName;
    public int maxAmmo;
    public float fireRate;
    public float minDamage;
    public float maxDamage;
    public AudioClip fireSound;
    public AudioSource audioSource;
    public GameObject playerCameraTransform;

    public int currentAmmo;
    private float _lastFireTime;

    public void Fire()
    {
        if (Time.time >= _lastFireTime + (1f / fireRate))
        {
            if (currentAmmo > 0)
            {
                currentAmmo--;
                Debug.Log($"{weaponName} fired! Ammo left: {currentAmmo}");
                _lastFireTime = Time.time;

                if (audioSource != null && fireSound != null)
                {
                    audioSource.PlayOneShot(fireSound);
                }
                
                if (playerCameraTransform != null)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(playerCameraTransform.transform.position, playerCameraTransform.transform.forward, out hit, 100f))
                    {
                        IDamageable damageable = hit.collider.GetComponent<IDamageable>();
                        if (damageable != null)
                        {
                            damageable.TakeDamage(GetRandomizedDamage());
                        }
                        // TODO - Add damage logic
                    }
                }
            }
            else
            {
                Debug.Log($"{weaponName}: Out of ammo!");
            }
        }
    }

    public void Reload()
    {
        if (currentAmmo < maxAmmo)
        {
            Debug.Log($"{weaponName} reloading...");
            currentAmmo = maxAmmo;
            Debug.Log($"{weaponName} reloaded! Ammo: {currentAmmo}");
        }
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public float GetRandomizedDamage()
    {
        float random = Random.Range(minDamage, maxDamage);
        return Mathf.Floor(random);
    }
}