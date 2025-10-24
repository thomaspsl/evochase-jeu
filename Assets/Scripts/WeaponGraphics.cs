using UnityEngine;

public class WeaponGraphics : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    public GameObject hitEffectPref;

    public static WeaponGraphics currentGraphics;

    public static WeaponGraphics GetCurrentGraphics(WeaponGraphics weapon)
    {
        // Récupération des éléments graphiques liés à l'arme
        currentGraphics = weapon.GetComponent<WeaponGraphics>();
        return currentGraphics;
    }

}
