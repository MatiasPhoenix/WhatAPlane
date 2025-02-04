using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Parameters")]
    public GameObject bulletPrefab;       // Prefab del proiettile
    public Transform firePoint;           // Punto di spawn (EmptyObject)
    public float bulletSpeed = 20f;       // Velocità della pallina
    public float fireRate = 0.2f;         // Timer per il fuoco (intervallo tra i colpi)

    private float nextFireTime = 0f;      // Tempo prossimo per sparare
    private bool isFiring;                // Controllo del fuoco (tenendo premuto il tasto)

    [Header("Input")]
    public InputAction ShootAction;       // Input per sparare

    //Private elements
    

    private void OnEnable()
    {
        ShootAction.Enable();
    }

    private void OnDisable()
    {
        ShootAction.Disable();
    }

    void Update()
    {
        // Controllo se il tasto di fuoco è premuto
        isFiring = ShootAction.ReadValue<float>() > 0;

        if (isFiring && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        SoundsManager.instance.PlayShoot();
        
        // Istanzia il proiettile nel firePoint e lo spara nella direzione corrente del muso
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.right * bulletSpeed; // Spara nella direzione del "firePoint"
        }

        // Optional: Distrugge il proiettile dopo 5 secondi per evitare troppi oggetti nella scena
        Destroy(bullet, 5f);
    }
}
