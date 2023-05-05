using UnityEngine;
using UnityEngine.UI;

namespace Hive
{
    public class Barracks: HiveBuilding
    {
        public Sprite Picture;
        
        public override string GetName() => "Barracks";

        public override string GetDescription() => "Это описание казармы";

        public override Sprite GetImage() => Picture;
        
        public override GameObject ShowSpecialInformation()
        {
            throw new System.NotImplementedException();
        }
    }
}