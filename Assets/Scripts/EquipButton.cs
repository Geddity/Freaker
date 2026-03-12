using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;

namespace Spine.Unity.Examples {
    public class EquipButton : MonoBehaviour {

        public SkeletonDataAsset skeletonDataAsset;
        public SkinChange skinsSystem;

        [SpineSkin(dataField: "skeletonDataAsset")] public string itemSkin;
        public SkinChange.ItemType itemType;

        void Start()
        {
            var button = GetComponent<Button>();
            button.onClick.AddListener(
                delegate { skinsSystem.Equip(itemSkin, itemType); }
            );
        }
    }

}
