using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bart/Bullet")]
public class BulletData : ScriptableObject
{
    [System.Serializable]
    public struct Bullet
    {
        public float _speed;
        public float _size;
        public float _lifeTime;
        public float _damage;
        public Color _color;
    }


    public List<Bullet> _bullets = new List<Bullet>();

}
