using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet d'utiliser la struct comme variable multiple.
[System.Serializable]
public struct BackGroundLayer
{
    public GameObject _visual;
    public float _layerDistance;
}

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private List<BackGroundLayer> _backgrounds = new List<BackGroundLayer>();
    [SerializeField] private float _smoothSpeed = 1f;

    private Vector3 _previousCamPos = new Vector3();
    private Vector3 _backgroundTargetPos = new Vector3();

    private void Start()
    {
        _previousCamPos = _camera.position;
    }

    private void Update()
    {
        float parallaxValue;
        BackGroundLayer currentBackground;

        for(int i = 0; i < _backgrounds.Count; i++)
        {
            currentBackground = _backgrounds[i];
            parallaxValue = (_previousCamPos.x - _camera.position.x) * -currentBackground._layerDistance;

            _backgroundTargetPos = currentBackground._visual.transform.position;

            _backgroundTargetPos.x += parallaxValue;

            currentBackground._visual.transform.position = Vector3.Lerp(currentBackground._visual.transform.position, _backgroundTargetPos, _smoothSpeed * Time.deltaTime);
        }

        _previousCamPos = _camera.position;
    }
}
