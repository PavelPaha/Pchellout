using System;
using TMPro;
using UnityEngine;

namespace Global
{
    public class ResourcesUpdater : MonoBehaviour
    {
        public TextMeshProUGUI Honey;
        public static Action OnUpdated;
        
        public void Start()
        {
            Extractor.OnResourcesUpdated += UpdateResources;
            BuildingPlacer.OnBuildingPlaced += UpdateResources;
        }
        
        public void UpdateResources()
        {
            OnUpdated?.Invoke();
            Honey.text = Globals.GameResources["honey"].Amount.ToString();
        }
    }
}