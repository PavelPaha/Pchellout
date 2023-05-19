using UnityEngine;
using UnityEngine.Serialization;

namespace Global
{
    public class ExtractorsSpawner : MonoBehaviour
    {
        public GameObject ParentForExtractors;
        public GameObject Bee;
        public GameObject Hive;
        public GameObject Goal;

        public void OnMouseDown()
        {
            if (Hive == null || Goal.transform.childCount == 0) return;
            Vector3 hivePosition = Hive.transform.position;
            var bee = Instantiate(Bee, hivePosition, Quaternion.identity);
            bee.GetComponent<Extractor>().targetsParent = Goal;
            bee.GetComponent<Extractor>().spawnObject = Hive;
            bee.transform.localScale = new Vector3(0.6f,0.6f,0.6f);
            bee.transform.SetParent(ParentForExtractors.transform);
        }
    }
}
