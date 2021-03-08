using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{
    public Object[] scenes;

    void Awake()
    {
        foreach(Object scene in scenes)
        {
            GameObject gameObject = GameObject.Find(scene.name);
            if (gameObject == null)
            {
                SceneManager.LoadScene(scene.name, LoadSceneMode.Additive);
            }
        }
    }
}
