using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Global
{
    public class ExtractorsSpawner : MonoBehaviour
    {
        public GameObject ParentForExtractors;
        public GameObject Bee;
        public GameObject Hive;
        public GameObject Goal;
        public static Action OnBuy;

        public void Start()
        {
            ResourcesUpdater.OnUpdated += UpdateBuyPossibility;
        }

        public void OnMouseDown()
        {
            if (Hive == null
                || Goal.transform.childCount == 0
                || Globals.ExtractorPrice > Globals.GameResources["honey"].Amount)
                return;
            
            Vector3 hivePosition = Hive.transform.position;
            var bee = Instantiate(Bee, hivePosition, Quaternion.identity);
            
            Globals.GameResources["honey"].Amount -= Globals.ExtractorPrice;
            OnBuy?.Invoke();
            bee.GetComponent<Extractor>().targetsParent = Goal;
            bee.GetComponent<Extractor>().spawnObject = Hive;
            bee.transform.localScale = new Vector3(0.6f,0.6f,0.6f);
            bee.transform.SetParent(ParentForExtractors.transform);
        }

        private void UpdateBuyPossibility()
        {
            Globals.UpdateBuyPossibility(gameObject.GetComponent<Button>(), Globals.ExtractorPrice);
        }
    }
}
