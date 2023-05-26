using System;
using UnityEngine;

namespace Global
{
    public class ButtonSound : MonoBehaviour
    {
        private void OnMouseDown()
        {
            GetComponent<AudioSource>().Play();
        }
    }
}