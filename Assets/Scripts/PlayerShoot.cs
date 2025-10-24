using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject weaponGFX;

    [SerializeField]
    private string weaponLayerName = "Weapon";
    [SerializeField]
    private AudioClip shootSound;


    public WeaponGraphics weaponG;

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private LayerMask mask;

    void Start()
    {
        if (cam == null)
        {
            Debug.LogError("Aucune caméra renseignée sur le système de tir.");
            this.enabled = false;
        }

        weaponGFX.layer = LayerMask.NameToLayer(weaponLayerName);
    }

    private void Update()
    {
        // Si clic gauche cliqué
        if (Input.GetButtonDown("Fire1")) //arme semi-automatique 1 clic = une balle
        {
            // Déclenchement d'un tir
            Shoot();
        }
    }

    private void Shoot()
    {
        // Initialisation du tir
        RaycastHit hit;

        // Déclenchement de la fonction particules d'impact
        DoShootEffects();

        GameObject.Find("shootSound").GetComponent<AudioSource>().PlayOneShot(shootSound, 0.4f);
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            if (hit.collider.tag == "Enemy")
            {
                IEnemy enemy1 = hit.collider.GetComponent<IEnemy>();
                enemy1.CurrentHealth -= 50;

                /*if (enemy1 is IEnemy)
                    Debug.Log("Objet touché :" + hit.collider.name);*/
            }
            // Déclenchement de la fonction particules d'impact
            DoHitEffects(hit.point, hit.normal);
        }
    }

    // Fonction appelée quand le joueur tire
    // Fait apparaitre les effets de tir
    public void DoShootEffects()
    {
        WeaponGraphics.GetCurrentGraphics(weaponG).muzzleFlash.Play();
    }

    // Fonction appelée quand le joueur tire
    // Fait apparaitre les impacts de tir sr la surface touchée
    public void DoHitEffects(Vector3 pos, Vector3 normal)
    {
        // Instanciation d'un objet hitEffect sur la Scene de jeu
        GameObject hitEffect = Instantiate(WeaponGraphics.GetCurrentGraphics(weaponG).hitEffectPref, pos, Quaternion.LookRotation(normal));
        // Destruction de cet item 
        Destroy(hitEffect, 1.5f);
    }
}
