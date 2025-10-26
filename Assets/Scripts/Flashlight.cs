using System.Collections;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject lightSource;
    // public AudioSource clickSound;
    private bool lightOn, failSafe = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.L) && !failSafe) {
            failSafe = true;
            lightOn = !lightOn;
            lightSource.SetActive(!lightSource.activeSelf);
            //clickSound.Play();
            StartCoroutine(FailSafe());
        }
    }

    IEnumerator FailSafe()
    {
        yield return new WaitForSeconds(0.25f);
        failSafe = false;
    }
}