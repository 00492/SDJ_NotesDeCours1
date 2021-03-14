using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{

    public void LoadMainScene()
    {
        SceneTransition.Instance.ChangeScene("Main");
    }
}
