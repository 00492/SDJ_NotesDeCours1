using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    private float _spriteSizeX = 0f;
    private List<GameObject> _images = new List<GameObject>();
    private Vector3 _camPos;

    private void Start()
    {
        SpawnImages();
    }

    private void SpawnImages()
    {
        _images.Add(_renderer.gameObject);

        _spriteSizeX = _renderer.sprite.bounds.size.x;
        Vector3 distance = new Vector3(_spriteSizeX, 0, 0);

        GameObject newImage = new GameObject();
        newImage.transform.SetParent(transform);
        newImage.name = _renderer.gameObject.name;

        // Opérateur ternaire ----->     Condition ? Vrai : Faux
        //newImage.transform.position = i == 0 ? newImage.transform.position += distance : newImage.transform.position -= distance;
        newImage.transform.position += distance;

        SpriteRenderer renderer = newImage.AddComponent<SpriteRenderer>();
        renderer.sortingOrder = _renderer.sortingOrder;
        renderer.sprite = _renderer.sprite;

        _images.Add(newImage);
    }

    private void Update()
    {
        _camPos = Camera.main.transform.position;

        foreach (GameObject image in _images)
        {
            if (_camPos.x - image.transform.position.x > _spriteSizeX)
            {
                image.transform.position += new Vector3(_spriteSizeX * 2f, 0, 0);
            }
            else if (image.transform.position.x - _camPos.x > _spriteSizeX)
            {
                image.transform.position -= new Vector3(_spriteSizeX * 2f, 0, 0);
            }
        }
    }
}
