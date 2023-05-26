using System;
using DefaultNamespace;
using UnityEngine;

namespace Global
{
    public class Audio : MonoBehaviour
    {
        private AudioSource _hitOnHive;

        public void Start()
        {
            _hitOnHive = GetComponents<AudioSource>()[1];
            BeeEnemy.OnHiveDamage += () => _hitOnHive.Play();
        }
    }
}