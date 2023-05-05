using UnityEngine;

namespace Hive
{
    public class HoneyStorage : HiveBuilding, IHoneyContainer
    {
        public Sprite Picture;
        public int Honey { get; set; }
        public override string GetName() => "Хранилище мёда";

        public override string GetDescription() => "Это хранилище мёда";

        public override Sprite GetImage() => Picture;

        public override GameObject ShowSpecialInformation()
        {
            throw new System.NotImplementedException();
        }
    }
}