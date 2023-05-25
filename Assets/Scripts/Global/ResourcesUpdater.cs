using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Global
{
    public class ResourcesUpdater : MonoBehaviour
    {
        public TextMeshProUGUI Honey;
        public TextMeshProUGUI HoneyCapacity;
        public static Action OnUpdated;
        
        public void Start()
        {
            Extractor.OnResourcesUpdated += UpdateResources;
            BuildingPlacer.OnBuildingPlaced += UpdateResources;
            HiveMenuInformationUpdater.OnUpgradeHoney += UpdateResources;
            ExtractorsSpawner.OnBuy += UpdateResources;
            SpawnBee.OnBuy += UpdateResources;
            BuildingPlacer.OnBuy += UpdateResources;
            UpdateResources();
        }
        
        public void UpdateResources()
        {
            OnUpdated?.Invoke();
            Honey.text = Globals.GameResources["honey"].Amount.ToString();
            HoneyCapacity.text = $"/ {Globals.CurrentStorageCapacity.ToString()}";
        }
    }
}