using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public const float TRANSITION_TIME = 1f;
    public const string RESSOURCE_NAME = "SceneTransition";
    public static Action<float> _onTransitionProgress;

    [SerializeField] private Image _blocker;
    private AsyncOperation _loadSceneOperation;

    private static SceneTransition _instance;
    public static SceneTransition Instance
    {
        get
        {
            // Si je n'existe pas, je me crée
            if(_instance == null)
            {
                GameObject prefab = Resources.Load<GameObject>(RESSOURCE_NAME);
                GameObject go = Instantiate(prefab);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        // Si j'existe déjà, je me détruit
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // L'action en paramètre est null par défault si on ne lui assigne aucune fonction lors de l'appel
    public void ChangeScene(string sceneName, Action onSceneChanged = null)
    {
        _loadSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        _loadSceneOperation.allowSceneActivation = false;

        if(onSceneChanged == null)
        {
            onSceneChanged = AllowSceneChange;
        }
        else
        {
            onSceneChanged += AllowSceneChange;
        }

        StartCoroutine(FadeRoutine(onSceneChanged));
    }

    private void AllowSceneChange()
    {
        _loadSceneOperation.allowSceneActivation = true;
    }

    private IEnumerator FadeRoutine(Action onFadeDone)
    {
        _blocker.raycastTarget = true;

        float timer = 0;
        while(timer < TRANSITION_TIME)
        {
            timer += Time.deltaTime;
            float lerpValue = timer / TRANSITION_TIME;
            _blocker.color = Color.Lerp(Color.clear, Color.black, lerpValue);
            //_onTransitionProgress?.Invoke(lerpValue); // toujours de 0 à 1
            yield return null;
        }

        onFadeDone?.Invoke();

        timer = TRANSITION_TIME;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            float lerpValue = timer / TRANSITION_TIME;
            _blocker.color = Color.Lerp(Color.clear, Color.black, lerpValue);
            //_onTransitionProgress?.Invoke(lerpValue); // toujours de 1 à 0
            yield return null;
        }

        _blocker.raycastTarget = false;
    }
}
