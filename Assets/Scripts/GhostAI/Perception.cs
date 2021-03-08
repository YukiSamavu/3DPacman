using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Perception : MonoBehaviour
{
    public abstract GameObject[] GetGameObjects();

	public static GameObject GetGameObjectFromTag(GameObject[] gameObjects, string tagName)
	{
		GameObject result = null;
		foreach (GameObject gameObject in gameObjects)
		{
			if (gameObject.CompareTag(tagName))
			{
				result = gameObject;
				break;
			}
		}

		return result;
	}
}
