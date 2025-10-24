using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSoundScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    //Play Global
    private static BGSoundScript instance = null;
    public static BGSoundScript Instance
    {
        get { return instance; }
    }

    void Awake()
    { 
        DontDestroyOnLoad(this.gameObject);
    }
    //Play Gobal End

    // Update is called once per frame
    void Update () {
		
	}

    public void ChangeState()
    {
        if (true == gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
        else if (false == gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
    }
}
