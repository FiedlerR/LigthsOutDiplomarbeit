using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    // Awake
    void Awake()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            loadArea("Area1");
        }
    }

    public void loadArea(string Scene) {
        StartCoroutine(LoadYourAsyncScene(Scene));

    }

    IEnumerator LoadYourAsyncScene(string Scene)
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
