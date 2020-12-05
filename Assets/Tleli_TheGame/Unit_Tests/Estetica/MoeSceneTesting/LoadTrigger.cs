using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTrigger : MonoBehaviour
{
    public string loadName;
    public string unloadName;

    public void OnTriggerEnter(Collider col)
    {
        if (loadName != "")
            SceneManagement.Instance.Load(loadName);

        if (unloadName != "")
            StartCoroutine("UnloadScene");
    }

    // Update is called once per frame
    IEnumerator UnloadScene()
    {
        yield return new WaitForSeconds(.10f);
        SceneManagement.Instance.Unload(unloadName);
    }
}
