using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class NavmeshMenuHandler : MonoBehaviour
{




    [MenuItem("LightsOut GameDesign Funtions/Enable NavMesh Objects")]
    static void enableNavMesh()
    {

        foreach (GameObject NavObject in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (NavObject.CompareTag("NavMeshObject")) {
                NavObject.SetActive(true);
            }
        }
    }

    [MenuItem("LightsOut GameDesign Funtions/Disable NavMesh Objects")]
    static void disableNavMesh()
    {
        GameObject[] objs;
        objs = GameObject.FindGameObjectsWithTag("NavMeshObject");
        foreach (GameObject NavObject in objs)
        {
            NavObject.SetActive(false);
        }
    }

    /*
    [MenuItem("LightsOut GameDesign Funtions/set Window Objects")]
    static void setWindows()
    {
        GameObject[] objs;
        objs = FindGameObjectsWithName("Window_interactive");
        foreach (GameObject window in objs)
        {
            GameObject newObject;

            // Debug.Log(GameObject.Find("Fenster"));
            // newObject = Instantiate(Resources.Load("Assets/Prefabs/Fenster")) as GameObject;

            UnityEngine.Object pPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Fenster.prefab", typeof(GameObject));

            newObject = Instantiate(pPrefab) as GameObject;
            Undo.RegisterCreatedObjectUndo(newObject, "replaced static windows");
            newObject.transform.parent = window.transform.parent;


            newObject.transform.position = window.transform.position;
            Quaternion rotation = Quaternion.Euler(0,window.transform.rotation.eulerAngles.y, 0);

            newObject.transform.rotation = rotation;
            newObject.transform.localScale = new Vector3(1,1,1);
            newObject.transform.SetSiblingIndex(window.transform.GetSiblingIndex());
            Undo.DestroyObjectImmediate(window);
        }
    }*/

    static GameObject[] FindGameObjectsWithName(string name)
    {
        GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();
        GameObject[] arr = new GameObject[gameObjects.Length];
        int FluentNumber = 0;
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].name == name)
            {
                arr[FluentNumber] = gameObjects[i];
                FluentNumber++;
            }
        }
        Array.Resize(ref arr, FluentNumber);
        return arr;
    }
}
