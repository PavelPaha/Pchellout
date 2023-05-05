using UnityEngine;

namespace Hive
{
    public class HoneyStorage: HiveObject, IContainHoney
    {
        public Sprite Picture;
        public int HoneyCount;
        public override string GetName() => "Хранилище мёда";

        public override string GetDescription() => "Это хранилище мёда";

        public override Sprite GetImage() => Picture;

        public override GameObject ShowSpecialInformation()
        {
            throw new System.NotImplementedException();
        }

        public int GetHoneyCount() => HoneyCount;

        public void UpdateHoneyCount(int value)
        {
            HoneyCount = value;
        }
    }
}