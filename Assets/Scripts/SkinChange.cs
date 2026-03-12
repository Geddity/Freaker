using System.Collections.Generic;
using UnityEngine;
using Spine.Unity.AttachmentTools;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

namespace Spine.Unity.Examples
{

    public class SkinChange : MonoBehaviour
    {
        // character skins

        // here we use arrays of strings to be able to cycle between them easily.
        [SpineSkin] public string[] colorSkins = { "color/c1" , "color/c2" , "color/c3", "color/c4", "color/c5" };
        public int activeSkinColorIndex = 0;

        [SpineSkin] public string[] eyesSkins = { "eyecolor/c1", "eyecolor/c2", "eyecolor/c3", "eyecolor/c4", "eyecolor/c5", "eyecolor/c6", "eyecolor/c7", };
        public int activeEyesIndex = 1;

        [SpineSkin] public string[] mouthSkins = { "mouth/m1", "mouth/m2", "mouth/m3", "mouth/m4" };
        public int activeMouthIndex = 0;

        [SpineSkin] public string[] scarSkins = { "scar/s0", "scar/s1", "scar/s2", "scar/s3", "scar/s4", "scar/s5", "scar/s6", "scar/s7", };
        public int activeScarIndex = 0;

        [SpineSkin] public string[] handSkins = { "hands/c1", "hands/c2", "hands/c3", "hands/c4", "hands/c5", "hands/iron1", };
        public int activeHandColorIndex = 0;
        public int savedHandIndex = 0;

        [SpineSkin] public string[,] footSkins = { { "foot/c1/normal", "foot/c1/hh"  },
                                                   { "foot/c2/normal", "foot/c2/hh"  },
                                                   { "foot/c3/normal", "foot/c3/hh"  },
                                                   { "foot/c4/normal", "foot/c4/hh"  },
                                                   { "foot/c5/normal", "foot/c5/hh" }, };
        public int activeFootIndex = 0;
        public int activeFootColorIndex = 0;


        [SpineSkin] public string[,] noseSkins = { { "nose/c1/nose1", "nose/c1/nose2", "nose/c1/nose3", "nose/c1/nose4", "nose/c1/nose5", "nose/c1/nose6", "nose/c1/nose7" },
                                                   { "nose/c2/nose1", "nose/c2/nose2", "nose/c2/nose3", "nose/c2/nose4", "nose/c2/nose5", "nose/c2/nose6", "nose/c2/nose7" },
                                                   { "nose/c3/nose1", "nose/c3/nose2", "nose/c3/nose3", "nose/c3/nose4", "nose/c3/nose5", "nose/c3/nose6", "nose/c3/nose7" },
                                                   { "nose/c4/nose1", "nose/c4/nose2", "nose/c4/nose3", "nose/c4/nose4", "nose/c4/nose5", "nose/c4/nose6", "nose/c4/nose7" },
                                                   { "nose/c5/nose1", "nose/c5/nose2", "nose/c5/nose3", "nose/c5/nose4", "nose/c5/nose5", "nose/c5/nose6", "nose/c5/nose7" } };
        public int activeNoseIndex = 0;
        public int activeNoseColorIndex = 0;

        [SpineSkin] public string[,] fheadSkins = { { "forehead/c1/1", "forehead/c1/2", "forehead/c1/3" },
                                                    { "forehead/c2/1", "forehead/c2/2", "forehead/c2/3" },
                                                    { "forehead/c3/1", "forehead/c3/2", "forehead/c3/3" },
                                                    { "forehead/c4/1", "forehead/c4/2", "forehead/c4/3" },
                                                    { "forehead/c5/1", "forehead/c5/2", "forehead/c5/3" } };
        public int activeFheadIndex = 0;
        public int activeFheadColorIndex = 0;

        [SpineSkin] public string[,] jawSkins = { { "jaw/c1/j1", "jaw/c1/j2", "jaw/c1/j3", "jaw/c1/j4", "jaw/c1/j5", "jaw/c1/j6", "jaw/c1/j7" },
                                                  { "jaw/c2/j1", "jaw/c2/j2", "jaw/c2/j3", "jaw/c2/j4", "jaw/c2/j5", "jaw/c2/j6", "jaw/c2/j7" },
                                                  { "jaw/c3/j1", "jaw/c3/j2", "jaw/c3/j3", "jaw/c3/j4", "jaw/c3/j5", "jaw/c3/j6", "jaw/c3/j7" },
                                                  { "jaw/c4/j1", "jaw/c4/j2", "jaw/c4/j3", "jaw/c4/j4", "jaw/c4/j5", "jaw/c4/j6", "jaw/c4/j7" },
                                                  { "jaw/c5/j1", "jaw/c5/j2", "jaw/c5/j3", "jaw/c5/j4", "jaw/c5/j5", "jaw/c5/j6", "jaw/c5/j7" } };
        public int activeJawIndex = 0;
        public int activeJawColorIndex = 0;

        [SpineSkin] public string[,] eyetypeSkins = { { "eyetype/c1/1", "eyetype/c1/2", "eyetype/c1/3", "eyetype/c1/4", "eyetype/c1/5", "eyetype/c1/6", "eyetype/c1/7" },
                                                      { "eyetype/c2/1", "eyetype/c2/2", "eyetype/c2/3", "eyetype/c2/4", "eyetype/c2/5", "eyetype/c2/6", "eyetype/c2/7" },
                                                      { "eyetype/c3/1", "eyetype/c3/2", "eyetype/c3/3", "eyetype/c3/4", "eyetype/c3/5", "eyetype/c3/6", "eyetype/c3/7" },
                                                      { "eyetype/c4/1", "eyetype/c4/2", "eyetype/c4/3", "eyetype/c4/4", "eyetype/c4/5", "eyetype/c4/6", "eyetype/c4/7" },
                                                      { "eyetype/c5/1", "eyetype/c5/2", "eyetype/c5/3", "eyetype/c5/4", "eyetype/c5/5", "eyetype/c5/6", "eyetype/c5/7" } };
        public int activeETIndex = 0;
        public int activeETColorIndex = 0;

        [SpineSkin] public string[,] earSkins = { { "ear/c1/ear1", "ear/c1/ear2", "ear/c1/ear3", "ear/c1/ear4", "ear/c1/ear5", "ear/c1/ear6", "ear/c1/ear7" },
                                                  { "ear/c2/ear1", "ear/c2/ear2", "ear/c2/ear3", "ear/c2/ear4", "ear/c2/ear5", "ear/c2/ear6", "ear/c2/ear7" },
                                                  { "ear/c3/ear1", "ear/c3/ear2", "ear/c3/ear3", "ear/c3/ear4", "ear/c3/ear5", "ear/c3/ear6", "ear/c3/ear7" },
                                                  { "ear/c4/ear1", "ear/c4/ear2", "ear/c4/ear3", "ear/c4/ear4", "ear/c4/ear5", "ear/c4/ear6", "ear/c4/ear7" },
                                                  { "ear/c5/ear1", "ear/c5/ear2", "ear/c5/ear3", "ear/c5/ear4", "ear/c5/ear5", "ear/c5/ear6", "ear/c5/ear7" } };
        public int activeEarIndex = 0;
        public int activeEarColorIndex = 0;
        public int savedEarIndex;



        [SpineSkin] public string[,] hairSkins; 
        
        public int activeHairIndex;
        public int activeHairColorIndex;
        public int savedHairIndex;

        [SpineSkin] public string[,] browSkins = { { "allhair/brows/c1/1", "allhair/brows/c1/2", "allhair/brows/c1/3", "allhair/brows/c1/4", "allhair/brows/c1/5", "allhair/brows/c1/6", "allhair/brows/c1/7" },
                                                   { "allhair/brows/c2/1", "allhair/brows/c2/2", "allhair/brows/c2/3", "allhair/brows/c2/4", "allhair/brows/c2/5", "allhair/brows/c2/6", "allhair/brows/c2/7" },
                                                   { "allhair/brows/c3/1", "allhair/brows/c3/2", "allhair/brows/c3/3", "allhair/brows/c3/4", "allhair/brows/c3/5", "allhair/brows/c3/6", "allhair/brows/c3/7" },
                                                   { "allhair/brows/c4/1", "allhair/brows/c4/2", "allhair/brows/c4/3", "allhair/brows/c4/4", "allhair/brows/c4/5", "allhair/brows/c4/6", "allhair/brows/c4/7" },
                                                   { "allhair/brows/c5/1", "allhair/brows/c5/2", "allhair/brows/c5/3", "allhair/brows/c5/4", "allhair/brows/c5/5", "allhair/brows/c5/6", "allhair/brows/c5/7" },
                                                   { "allhair/brows/c6/1", "allhair/brows/c6/2", "allhair/brows/c6/3", "allhair/brows/c6/4", "allhair/brows/c6/5", "allhair/brows/c6/6", "allhair/brows/c6/7" },
                                                   { "allhair/brows/c7/1", "allhair/brows/c7/2", "allhair/brows/c7/3", "allhair/brows/c7/4", "allhair/brows/c7/5", "allhair/brows/c7/6", "allhair/brows/c7/7" }};

        public int activeBrowIndex = 0;
        public int activeBrowColorIndex = 0;

        [SpineSkin] public string[,] mustashSkins;

        public int activeMustashIndex;
        public int activeMustashColorIndex;

        [SpineSkin] public string[,] beardSkins;

        public int activeBeardIndex = 0;
        public int activeBeardColorIndex = 0;


        public void SkinsToHumanMale()
        {
            hairSkins = new string[,] { { "allhair/hair/c1/h0", "allhair/hair/c1/h1", "allhair/hair/c1/h2", "allhair/hair/c1/h3", "allhair/hair/c1/h4", "allhair/hair/c1/h5", "allhair/hair/c1/h6", "allhair/hair/c1/h7", "allhair/hair/c1/h8", "allhair/hair/c1/h9" },
                                                   {"allhair/hair/c2/h0", "allhair/hair/c2/h1", "allhair/hair/c2/h2", "allhair/hair/c2/h3", "allhair/hair/c2/h4", "allhair/hair/c2/h5", "allhair/hair/c2/h6", "allhair/hair/c2/h7", "allhair/hair/c2/h8", "allhair/hair/c2/h9"},
                                                   {"allhair/hair/c3/h0", "allhair/hair/c3/h1", "allhair/hair/c3/h2", "allhair/hair/c3/h3", "allhair/hair/c3/h4", "allhair/hair/c3/h5", "allhair/hair/c3/h6", "allhair/hair/c3/h7", "allhair/hair/c3/h8", "allhair/hair/c3/h9"},
                                                   {"allhair/hair/c4/h0", "allhair/hair/c4/h1", "allhair/hair/c4/h2", "allhair/hair/c4/h3", "allhair/hair/c4/h4", "allhair/hair/c4/h5", "allhair/hair/c4/h6", "allhair/hair/c4/h7", "allhair/hair/c4/h8", "allhair/hair/c4/h9"},
                                                   {"allhair/hair/c5/h0", "allhair/hair/c5/h1", "allhair/hair/c5/h2", "allhair/hair/c5/h3", "allhair/hair/c5/h4", "allhair/hair/c5/h5", "allhair/hair/c5/h6", "allhair/hair/c5/h7", "allhair/hair/c5/h8", "allhair/hair/c5/h9"},
                                                   {"allhair/hair/c6/h0", "allhair/hair/c6/h1", "allhair/hair/c6/h2", "allhair/hair/c6/h3", "allhair/hair/c6/h4", "allhair/hair/c6/h5", "allhair/hair/c6/h6", "allhair/hair/c6/h7", "allhair/hair/c6/h8", "allhair/hair/c6/h9"},
                                                   {"allhair/hair/c7/h0", "allhair/hair/c7/h1", "allhair/hair/c7/h2", "allhair/hair/c7/h3", "allhair/hair/c7/h4", "allhair/hair/c7/h5", "allhair/hair/c7/h6", "allhair/hair/c7/h7", "allhair/hair/c7/h8", "allhair/hair/c7/h9"}};

            mustashSkins = new string[,] { { "allhair/mustash/c1/m0", "allhair/mustash/c1/m1", "allhair/mustash/c1/m2", "allhair/mustash/c1/m3", "allhair/mustash/c1/m4", "allhair/mustash/c1/m5", "allhair/mustash/c1/m6", "allhair/mustash/c1/m7" },
                                                      { "allhair/mustash/c2/m0", "allhair/mustash/c2/m1", "allhair/mustash/c2/m2", "allhair/mustash/c2/m3", "allhair/mustash/c2/m4", "allhair/mustash/c2/m5", "allhair/mustash/c2/m6", "allhair/mustash/c2/m7" },
                                                      { "allhair/mustash/c3/m0", "allhair/mustash/c3/m1", "allhair/mustash/c3/m2", "allhair/mustash/c3/m3", "allhair/mustash/c3/m4", "allhair/mustash/c3/m5", "allhair/mustash/c3/m6", "allhair/mustash/c3/m7" },
                                                      { "allhair/mustash/c4/m0", "allhair/mustash/c4/m1", "allhair/mustash/c4/m2", "allhair/mustash/c4/m3", "allhair/mustash/c4/m4", "allhair/mustash/c4/m5", "allhair/mustash/c4/m6", "allhair/mustash/c4/m7" },
                                                      { "allhair/mustash/c5/m0", "allhair/mustash/c5/m1", "allhair/mustash/c5/m2", "allhair/mustash/c5/m3", "allhair/mustash/c5/m4", "allhair/mustash/c5/m5", "allhair/mustash/c5/m6", "allhair/mustash/c5/m7" },
                                                      { "allhair/mustash/c6/m0", "allhair/mustash/c6/m1", "allhair/mustash/c6/m2", "allhair/mustash/c6/m3", "allhair/mustash/c6/m4", "allhair/mustash/c6/m5", "allhair/mustash/c6/m6", "allhair/mustash/c6/m7" },
                                                      { "allhair/mustash/c7/m0", "allhair/mustash/c7/m1", "allhair/mustash/c7/m2", "allhair/mustash/c7/m3", "allhair/mustash/c7/m4", "allhair/mustash/c7/m5", "allhair/mustash/c7/m6", "allhair/mustash/c7/m7" }};

            beardSkins = new string[,] { { "allhair/beard/c1/b0", "allhair/beard/c1/b1", "allhair/beard/c1/b2", "allhair/beard/c1/b3", "allhair/beard/c1/b4", "allhair/beard/c1/b5", "allhair/beard/c1/b6", "allhair/beard/c1/b7" },
                                                    { "allhair/beard/c2/b0", "allhair/beard/c2/b1", "allhair/beard/c2/b2", "allhair/beard/c2/b3", "allhair/beard/c2/b4", "allhair/beard/c2/b5", "allhair/beard/c2/b6", "allhair/beard/c2/b7" },
                                                    { "allhair/beard/c3/b0", "allhair/beard/c3/b1", "allhair/beard/c3/b2", "allhair/beard/c3/b3", "allhair/beard/c3/b4", "allhair/beard/c3/b5", "allhair/beard/c3/b6", "allhair/beard/c3/b7" },
                                                    { "allhair/beard/c4/b0", "allhair/beard/c4/b1", "allhair/beard/c4/b2", "allhair/beard/c4/b3", "allhair/beard/c4/b4", "allhair/beard/c4/b5", "allhair/beard/c4/b6", "allhair/beard/c4/b7" },
                                                    { "allhair/beard/c5/b0", "allhair/beard/c5/b1", "allhair/beard/c5/b2", "allhair/beard/c5/b3", "allhair/beard/c5/b4", "allhair/beard/c5/b5", "allhair/beard/c5/b6", "allhair/beard/c5/b7" },
                                                    { "allhair/beard/c6/b0", "allhair/beard/c6/b1", "allhair/beard/c6/b2", "allhair/beard/c6/b3", "allhair/beard/c6/b4", "allhair/beard/c6/b5", "allhair/beard/c6/b6", "allhair/beard/c6/b7" },
                                                    { "allhair/beard/c7/b0", "allhair/beard/c7/b1", "allhair/beard/c7/b2", "allhair/beard/c7/b3", "allhair/beard/c7/b4", "allhair/beard/c7/b5", "allhair/beard/c7/b6", "allhair/beard/c7/b7" }};
        }

        public void SkinsToHumanFemale()
        {
            hairSkins = new string[,] { { "allhair/hair/c1/h0", "allhair/hair/c1/h1", "allhair/hair/c1/h2", "allhair/hair/c1/h3", "allhair/hair/c1/h4", "allhair/hair/c1/h5", "allhair/hair/c1/h6", "allhair/hair/c1/h7" },
                                                   {"allhair/hair/c2/h0", "allhair/hair/c2/h1", "allhair/hair/c2/h2", "allhair/hair/c2/h3", "allhair/hair/c2/h4", "allhair/hair/c2/h5", "allhair/hair/c2/h6", "allhair/hair/c2/h7"},
                                                   {"allhair/hair/c3/h0", "allhair/hair/c3/h1", "allhair/hair/c3/h2", "allhair/hair/c3/h3", "allhair/hair/c3/h4", "allhair/hair/c3/h5", "allhair/hair/c3/h6", "allhair/hair/c3/h7"},
                                                   {"allhair/hair/c4/h0", "allhair/hair/c4/h1", "allhair/hair/c4/h2", "allhair/hair/c4/h3", "allhair/hair/c4/h4", "allhair/hair/c4/h5", "allhair/hair/c4/h6", "allhair/hair/c4/h7"},
                                                   {"allhair/hair/c5/h0", "allhair/hair/c5/h1", "allhair/hair/c5/h2", "allhair/hair/c5/h3", "allhair/hair/c5/h4", "allhair/hair/c5/h5", "allhair/hair/c5/h6", "allhair/hair/c5/h7"},
                                                   {"allhair/hair/c6/h0", "allhair/hair/c6/h1", "allhair/hair/c6/h2", "allhair/hair/c6/h3", "allhair/hair/c6/h4", "allhair/hair/c6/h5", "allhair/hair/c6/h6", "allhair/hair/c6/h7"},
                                                   {"allhair/hair/c7/h0", "allhair/hair/c7/h1", "allhair/hair/c7/h2", "allhair/hair/c7/h3", "allhair/hair/c7/h4", "allhair/hair/c7/h5", "allhair/hair/c7/h6", "allhair/hair/c7/h7"}};

            mustashSkins = new string[0,0];
            beardSkins = new string[0,0];
        }

        // equipment skins
        public enum ItemType
        {
            Helm, Cloth, Bracers, Belt, Pants, Boots, MainHand, OffHand
        }
        [SpineSkin] public string clothesSkin = "";
        [SpineSkin] public string pantsSkin = "";
        [SpineSkin] public string beltSkin = "";
        [SpineSkin] public string bracersSkin = "";
        [SpineSkin] public string bootsSkin = "";
        [SpineSkin] public string helmSkin = "";
        [SpineSkin] public string mainHandSkin = "";
        [SpineSkin] public string offHandSkin = "";

        SkeletonAnimation skeletonAnimation;
        // This "naked body" skin will likely change only once upon character creation,
        // so we store this combined set of non-equipment Skins for later re-use.
        Skin characterSkin;

        // for repacking the skin to a new atlas texture
        public Material runtimeMaterial;
        public Texture2D runtimeAtlas;

        CharacterObject characterObject;

        public void SaveSkinToSO(CharacterSkin characterSkin)
        {
            characterObject.characterData.skins.skeleton = equipmentManager.activeSkeleton;

            switch (characterSkin)
            {
                case CharacterSkin.SkinColor:
                    characterObject.characterData.skins.skinColor = activeSkinColorIndex;
                    break;
                case CharacterSkin.EyeColor:
                    characterObject.characterData.skins.eyeColor = activeEyesIndex;
                    break;
                case CharacterSkin.Mouth:
                    characterObject.characterData.skins.mouth = activeMouthIndex;
                    break;
                case CharacterSkin.Scar:
                    characterObject.characterData.skins.scar = activeScarIndex;
                    break;
                case CharacterSkin.Nose:
                    characterObject.characterData.skins.nose = activeNoseIndex;
                    break;
                case CharacterSkin.Forehead:
                    characterObject.characterData.skins.forehead = activeFheadIndex;
                    break;
                case CharacterSkin.Jaw:
                    characterObject.characterData.skins.jaw = activeJawIndex;
                    break;
                case CharacterSkin.EyeType:
                    characterObject.characterData.skins.eyeType = activeETIndex;
                    break;
                case CharacterSkin.Ear:
                    characterObject.characterData.skins.ear = activeEarIndex;
                    break;
                case CharacterSkin.Hair:
                    characterObject.characterData.skins.hair = activeHairIndex;
                    break;
                case CharacterSkin.HairColor:
                    characterObject.characterData.skins.hairColor = activeHairColorIndex;
                    break;
                case CharacterSkin.Brow:
                    characterObject.characterData.skins.brow = activeBrowIndex;
                    break;
                case CharacterSkin.BrowColor:
                    characterObject.characterData.skins.browColor = activeBrowColorIndex;
                    break;
                case CharacterSkin.Mustash:
                    characterObject.characterData.skins.mustash = activeMustashIndex;
                    break;
                case CharacterSkin.MustashColor:
                    characterObject.characterData.skins.mustashColor = activeMustashColorIndex;
                    break;
                case CharacterSkin.Beard:
                    characterObject.characterData.skins.beard = activeBeardIndex;
                    break;
                case CharacterSkin.BeardColor:
                    characterObject.characterData.skins.beardColor = activeBeardColorIndex;
                    break;
            }
        }

        public void StartSkin()
        {
            equipmentManager.activeSkeleton = characterObject.characterData.skins.skeleton;

            activeSkinColorIndex = 1;

            activeHandColorIndex = activeSkinColorIndex;
            activeFootColorIndex = activeSkinColorIndex;
            activeNoseColorIndex = activeSkinColorIndex;
            activeFheadColorIndex = activeSkinColorIndex;
            activeJawColorIndex = activeSkinColorIndex;
            activeETColorIndex = activeSkinColorIndex;
            activeEarColorIndex = activeSkinColorIndex;

            activeEyesIndex = 1;
            activeMouthIndex = 0;
            activeScarIndex = 0;
            activeNoseIndex = 0;
            activeFheadIndex = 0;
            activeJawIndex = 0;
            activeETIndex = 0;
            activeEarIndex = 0;
            activeHairIndex = 1;
            activeHairColorIndex = 0;
            activeBrowIndex = 0;
            activeBrowColorIndex = 0;

            heightSlider.value = 0.96f;

            savedEarIndex = 0;
            savedHairIndex = 1;
            savedHandIndex = 1;
        }

        public void SaveSkinToSO()
        {
            characterObject.characterData.skins.skeleton = equipmentManager.activeSkeleton;

            characterObject.characterData.skins.skinColor = activeSkinColorIndex;
            characterObject.characterData.skins.eyeColor = activeEyesIndex;
            characterObject.characterData.skins.mouth = activeMouthIndex;
            characterObject.characterData.skins.scar = activeScarIndex;
            characterObject.characterData.skins.nose = activeNoseIndex;
            characterObject.characterData.skins.forehead = activeFheadIndex;
            characterObject.characterData.skins.jaw = activeJawIndex;
            characterObject.characterData.skins.eyeType = activeETIndex;
            characterObject.characterData.skins.ear = activeEarIndex;
            characterObject.characterData.skins.hair = activeHairIndex;
            characterObject.characterData.skins.hairColor = activeHairColorIndex;
            characterObject.characterData.skins.brow = activeBrowIndex;
            characterObject.characterData.skins.browColor = activeBrowColorIndex;
            characterObject.characterData.skins.mustash = activeMustashIndex;
            characterObject.characterData.skins.mustashColor = activeMustashColorIndex;
            characterObject.characterData.skins.beard = activeBeardIndex;
            characterObject.characterData.skins.beardColor = activeBeardColorIndex;

            characterObject.characterData.skins.height = heightSlider.value;
        }

        public void LoadSkinFromSO()
        {
            equipmentManager.activeSkeleton = characterObject.characterData.skins.skeleton;

            activeSkinColorIndex = characterObject.characterData.skins.skinColor;

            activeHandColorIndex = activeSkinColorIndex;
            activeFootColorIndex = activeSkinColorIndex;
            activeNoseColorIndex = activeSkinColorIndex;
            activeFheadColorIndex = activeSkinColorIndex;
            activeJawColorIndex = activeSkinColorIndex;
            activeETColorIndex = activeSkinColorIndex;
            activeEarColorIndex = activeSkinColorIndex;

            activeEyesIndex = characterObject.characterData.skins.eyeColor;
            activeMouthIndex = characterObject.characterData.skins.mouth;
            activeScarIndex = characterObject.characterData.skins.scar;
            activeNoseIndex = characterObject.characterData.skins.nose;
            activeFheadIndex = characterObject.characterData.skins.forehead;
            activeJawIndex = characterObject.characterData.skins.jaw;
            activeETIndex = characterObject.characterData.skins.eyeType;
            activeEarIndex = characterObject.characterData.skins.ear;
            activeHairIndex = characterObject.characterData.skins.hair;
            activeHairColorIndex = characterObject.characterData.skins.hairColor;
            activeBrowIndex = characterObject.characterData.skins.brow;
            activeBrowColorIndex = characterObject.characterData.skins.browColor;

            heightSlider.value = characterObject.characterData.skins.height;

            savedEarIndex = characterObject.characterData.skins.ear;
            savedHairIndex = characterObject.characterData.skins.hair;
            savedHandIndex = characterObject.characterData.skins.skinColor;

            if (characterObject.characterData.skins.skeleton == 0)
            {
                activeMustashIndex = characterObject.characterData.skins.mustash;
                activeMustashColorIndex = characterObject.characterData.skins.mustashColor;
                activeBeardIndex = characterObject.characterData.skins.beard;
                activeBeardColorIndex = characterObject.characterData.skins.beardColor;
            }
            
        }

        public EquipmentManager equipmentManager;

        void Awake()
        {
            

            skeletonAnimation = this.GetComponent<SkeletonAnimation>();
            characterObject = GetComponentInParent<CharacterObject>();
        }

        void Start()
        {
            LoadSkinFromSO();

            if (gameObject.name == "human_male")
            {
                SkinsToHumanMale();

                UpdateCharacterSkin();
                UpdateCombinedSkin();
            }      

            if (gameObject.name == "human_female")
            {
                SkinsToHumanFemale();

                UpdateCharacterSkin();
                UpdateCombinedSkin();
            }
        }

        public Slider heightSlider;
        private float heightSliderNumber;
        Vector3 scale;

        public void ColorSkin(int colorIndex)
        {
            activeSkinColorIndex = colorIndex;

            activeHandColorIndex = activeSkinColorIndex;
            savedHandIndex = activeHandColorIndex;

            activeFootColorIndex = activeSkinColorIndex;

            activeNoseColorIndex = activeSkinColorIndex;
            activeFheadColorIndex = activeSkinColorIndex;
            activeJawColorIndex = activeSkinColorIndex;
            activeETColorIndex = activeSkinColorIndex;
            activeEarColorIndex = activeSkinColorIndex;
            UpdateCharacterSkin();
            UpdateCombinedSkin();

            SaveSkinToSO(CharacterSkin.SkinColor);
        }

        private void Update()
        {
            SaveSkinToSO();

            if (gameObject.name == "human_male")
            {
                SkinsToHumanMale();

                UpdateCharacterSkin();
                UpdateCombinedSkin();
            }

            if (gameObject.name == "human_female")
            {
                SkinsToHumanFemale();

                UpdateCharacterSkin();
                UpdateCombinedSkin();
            }

            heightSliderNumber = heightSlider.value;
            scale = new Vector3(heightSliderNumber, heightSliderNumber, heightSliderNumber);
            gameObject.transform.parent.localScale = scale;
        }


        public void NextEyesSkin()
        {
            if (gameObject.activeSelf)
            {
                activeEyesIndex = (activeEyesIndex + 1) % eyesSkins.Length;
                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.EyeColor);
            }
        }

        public void PrevEyesSkin()
        {
            if (gameObject.activeSelf)
            {
                activeEyesIndex = (activeEyesIndex + eyesSkins.Length - 1) % eyesSkins.Length;
                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.EyeColor);
            }
        }

        public void NextMouthSkin()
        {
            if (gameObject.activeSelf)
            {
                activeMouthIndex = (activeMouthIndex + 1) % mouthSkins.Length;
                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.Mouth);
            }
        }

        public void PrevMouthSkin()
        {
            if (gameObject.activeSelf)
            {
                activeMouthIndex = (activeMouthIndex + mouthSkins.Length - 1) % mouthSkins.Length;
                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.Mouth);
            }
        }

        public void NextScarSkin()
        {
            if (gameObject.activeSelf)
            {
                activeScarIndex = (activeScarIndex + 1) % scarSkins.Length;
                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.Scar);
            }
        }

        public void PrevScarSkin()
        {
            if (gameObject.activeSelf)
            {
                activeScarIndex = (activeScarIndex + scarSkins.Length - 1) % scarSkins.Length;
                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.Scar);
            }
        }

        public void NextNoseSkin()
        {
            if (gameObject.activeSelf)
            {
                activeNoseIndex = (activeNoseIndex + 1) % noseSkins.GetLength(1);

                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.Nose);
            }
        }

        public void PrevNoseSkin()
        {
            if (gameObject.activeSelf)
            {
                activeNoseIndex = (activeNoseIndex - 1 + noseSkins.GetLength(1)) % noseSkins.GetLength(1);

                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.Nose);
            }
        }

        public void NextFheadSkin()
        {
            if (gameObject.activeSelf)
            {
                activeFheadIndex = (activeFheadIndex + 1) % fheadSkins.GetLength(1);

                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.Forehead);
            }
        }

        public void PrevFheadSkin()
        {
            if (gameObject.activeSelf)
            {
                activeFheadIndex = (activeFheadIndex - 1 + fheadSkins.GetLength(1)) % fheadSkins.GetLength(1);

                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.Forehead);
            }
        }

        public void NextJawSkin()
        {
            if (gameObject.activeSelf)
            {
                activeJawIndex = (activeJawIndex + 1) % jawSkins.GetLength(1);

                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.Jaw);
            }
        }

        public void PrevJawSkin()
        {
            if (gameObject.activeSelf)
            {
                activeJawIndex = (activeJawIndex - 1 + jawSkins.GetLength(1)) % jawSkins.GetLength(1);

                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.Jaw);
            }
        }

        public void NextETSkin()
        {
            if (gameObject.activeSelf)
            {
                activeETIndex = (activeETIndex + 1) % eyetypeSkins.GetLength(1);

                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.EyeType);
            }
        }

        public void PrevETSkin()
        {
            if (gameObject.activeSelf)
            {
                activeETIndex = (activeETIndex - 1 + eyetypeSkins.GetLength(1)) % eyetypeSkins.GetLength(1);

                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.EyeType);
            }
        }

        public void NextEarSkin()
        {
            if (gameObject.activeSelf)
            {
                activeEarIndex = (activeEarIndex + 1) % earSkins.GetLength(1);
                savedEarIndex = activeEarIndex;

                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.Ear);
            }
        }

        public void PrevEarSkin()
        {
            if (gameObject.activeSelf)
            {
                activeEarIndex = (activeEarIndex - 1 + earSkins.GetLength(1)) % earSkins.GetLength(1);
                savedEarIndex = activeEarIndex;

                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.Ear);
            }
        }



        public void HairColor(int colorIndex)
        {
            if (gameObject.activeSelf)
            {
                activeHairColorIndex = colorIndex;

                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.HairColor);
            }
        }

        public void NextHairSkin()
        {
            if (gameObject.activeSelf)
            {
                activeHairIndex = (activeHairIndex + 1) % hairSkins.GetLength(1);
                savedHairIndex = activeHairIndex;
                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.Hair); 
            }
        }

        public void PrevHairSkin()
        {
            if (gameObject.activeSelf)
            {
                activeHairIndex = (activeHairIndex - 1 + hairSkins.GetLength(1)) % hairSkins.GetLength(1);
                savedHairIndex = activeHairIndex;
                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.Hair); 
            }
        }

        public void BrowColor(int colorIndex)
        {
            if (gameObject.activeSelf)
            {
                activeBrowColorIndex = colorIndex;
                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.BrowColor);
            }
        }

        public void NextBrowSkin()
        {
            if (gameObject.activeSelf)
            {
                activeBrowIndex = (activeBrowIndex + 1) % browSkins.GetLength(1);
                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.Brow);
            }
        }

        public void PrevBrowSkin()
        {
            if (gameObject.activeSelf)
            {
                activeBrowIndex = (activeBrowIndex - 1 + browSkins.GetLength(1)) % browSkins.GetLength(1);
                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.Brow);
            }
        }

        public void MustashColor(int colorIndex)
        {
            if (equipmentManager.activeSkeleton == 0)
            {
                activeMustashColorIndex = colorIndex;
                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.MustashColor);
            }
           
        }

        public void NextMustashSkin()
        {
            if (equipmentManager.activeSkeleton == 0)
            {
                activeMustashIndex = (activeMustashIndex + 1) % mustashSkins.GetLength(1);
                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.Mustash);
            }
            
        }

        public void PrevMustashSkin()
        {
            if (equipmentManager.activeSkeleton == 0)
            {
                activeMustashIndex = (activeMustashIndex - 1 + mustashSkins.GetLength(1)) % mustashSkins.GetLength(1);
                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.Mustash);
            }
            
        }

        public void BeardColor(int colorIndex)
        {
            if (equipmentManager.activeSkeleton == 0)
            {
                activeBeardColorIndex = colorIndex;
                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.BeardColor);
            }
            
        }

        public void NextBeardSkin()
        {
            if (equipmentManager.activeSkeleton == 0)
            {
                activeBeardIndex = (activeBeardIndex + 1) % beardSkins.GetLength(1);
                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.Beard);
            }
            
        }

        public void PrevBeardSkin()
        {
            if (equipmentManager.activeSkeleton == 0)
            {
                activeBeardIndex = (activeBeardIndex - 1 + beardSkins.GetLength(1)) % beardSkins.GetLength(1);
                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO(CharacterSkin.Beard);
            }
            
        }






        public void Equip(string itemSkin, ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Cloth:
                    clothesSkin = itemSkin;
                    break;
                case ItemType.Pants:
                    pantsSkin = itemSkin;
                    break;
                case ItemType.Belt:
                    beltSkin = itemSkin;
                    break;
                case ItemType.Bracers:
                    bracersSkin = itemSkin;
                    break;
                case ItemType.Boots:
                    bootsSkin = itemSkin;
                    break;
                case ItemType.Helm:
                    helmSkin = itemSkin;
                    break;
                case ItemType.MainHand:
                    mainHandSkin = itemSkin;
                    break;
                case ItemType.OffHand:
                    offHandSkin = itemSkin;
                    break;
                default:
                    break;
            }
            UpdateCombinedSkin();
        }


        public void OptimizeSkin()
        {
            // Create a repacked skin.
            var previousSkin = skeletonAnimation.Skeleton.Skin;
            // Note: materials and textures returned by GetRepackedSkin() behave like 'new Texture2D()' and need to be destroyed
            if (runtimeMaterial)
                Destroy(runtimeMaterial);
            if (runtimeAtlas)
                Destroy(runtimeAtlas);
            Skin repackedSkin = previousSkin.GetRepackedSkin("Repacked skin", skeletonAnimation.SkeletonDataAsset.atlasAssets[0].PrimaryMaterial, out runtimeMaterial, out runtimeAtlas);
            previousSkin.Clear();

            // Use the repacked skin.
            skeletonAnimation.Skeleton.Skin = repackedSkin;
            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
            skeletonAnimation.AnimationState.Apply(skeletonAnimation.Skeleton);

            // `GetRepackedSkin()` and each call to `GetRemappedClone()` with parameter `premultiplyAlpha` set to `true`
            // cache necessarily created Texture copies which can be cleared by calling AtlasUtilities.ClearCache().
            // You can optionally clear the textures cache after multiple repack operations.
            // Just be aware that while this cleanup frees up memory, it is also a costly operation
            // and will likely cause a spike in the framerate.
            AtlasUtilities.ClearCache();
            Resources.UnloadUnusedAssets();

            Debug.Log("Skin Optimized");
        }

        void UpdateCharacterSkin()
        {
            var skeleton = skeletonAnimation.Skeleton;
            var skeletonData = skeleton.Data;
            characterSkin = new Skin("character-base");
            // Note that the result Skin returned by calls to skeletonData.FindSkin()
            // could be cached once in Start() instead of searching for the same skin
            // every time. For demonstration purposes we keep it simple here.

            IndexBorderFixer();

            if (gameObject.name == "human_male")
            {
                characterSkin.AddSkin(skeletonData.FindSkin(noseSkins[activeNoseColorIndex, activeNoseIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(eyesSkins[activeEyesIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(colorSkins[activeSkinColorIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(handSkins[activeHandColorIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(fheadSkins[activeFheadColorIndex, activeFheadIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(jawSkins[activeJawColorIndex, activeJawIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(eyetypeSkins[activeETColorIndex, activeETIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(earSkins[activeEarColorIndex, activeEarIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(mouthSkins[activeMouthIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(scarSkins[activeScarIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(footSkins[activeFootColorIndex, activeFootIndex]));

                characterSkin.AddSkin(skeletonData.FindSkin(hairSkins[activeHairColorIndex, activeHairIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(browSkins[activeBrowColorIndex, activeBrowIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(mustashSkins[activeMustashColorIndex, activeMustashIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(beardSkins[activeBeardColorIndex, activeBeardIndex]));
            }

            else if (gameObject.name == "human_female")
            {
                characterSkin.AddSkin(skeletonData.FindSkin(noseSkins[activeNoseColorIndex, activeNoseIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(eyesSkins[activeEyesIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(colorSkins[activeSkinColorIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(handSkins[activeHandColorIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(fheadSkins[activeFheadColorIndex, activeFheadIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(jawSkins[activeJawColorIndex, activeJawIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(eyetypeSkins[activeETColorIndex, activeETIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(earSkins[activeEarColorIndex, activeEarIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(mouthSkins[activeMouthIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(scarSkins[activeScarIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(footSkins[activeFootColorIndex, activeFootIndex]));

                characterSkin.AddSkin(skeletonData.FindSkin(hairSkins[activeHairColorIndex, activeHairIndex]));
                characterSkin.AddSkin(skeletonData.FindSkin(browSkins[activeBrowColorIndex, activeBrowIndex]));
            }
        }

        private void IndexBorderFixer()
        {
            if (activeNoseIndex >= noseSkins.GetLength(1)) { activeNoseIndex = 0; }
            if (activeNoseColorIndex >= noseSkins.GetLength(0)) { activeNoseColorIndex = 0; }
            if (activeEyesIndex >= eyesSkins.GetLength(0)) { activeEyesIndex = 0; }
            if (activeSkinColorIndex >= colorSkins.GetLength(0)) { activeSkinColorIndex = 0; }
            if (activeHandColorIndex >= handSkins.GetLength(0)) { activeHandColorIndex = 0; }
            if (activeFheadIndex >= fheadSkins.GetLength(1)) { activeFheadIndex = 0; }
            if (activeFheadColorIndex >= fheadSkins.GetLength(0)) { activeFheadColorIndex = 0; }
            if (activeJawIndex >= jawSkins.GetLength(1)) { activeJawIndex = 0; }
            if (activeJawColorIndex >= jawSkins.GetLength(0)) { activeJawColorIndex = 0; }
            if (activeETIndex >= eyetypeSkins.GetLength(1)) { activeETIndex = 0; }
            if (activeETColorIndex >= eyetypeSkins.GetLength(0)) { activeETColorIndex = 0; }
            if (activeEarIndex >= earSkins.GetLength(1)) { activeEarIndex = 0; }
            if (activeEarColorIndex >= earSkins.GetLength(0)) { activeEarColorIndex = 0; }
            if (activeMouthIndex >= mouthSkins.GetLength(0)) { activeMouthIndex = 0; }
            if (activeScarIndex >= scarSkins.GetLength(0)) { activeScarIndex = 0; }
            if (activeFootIndex >= footSkins.GetLength(1)) { activeFootIndex = 0; }
            if (activeFootColorIndex >= footSkins.GetLength(0)) { activeFootColorIndex = 0; }

            if (activeHairIndex >= hairSkins.GetLength(1)) { activeHairIndex = 0; }
            if (activeHairColorIndex >= hairSkins.GetLength(0)) { activeHairColorIndex = 0; }
            if (activeBrowIndex >= browSkins.GetLength(1)) { activeBrowIndex = 0; }
            if (activeBrowColorIndex >= browSkins.GetLength(0)) { activeBrowColorIndex = 0; }
            if (activeMustashIndex >= mustashSkins.GetLength(1)) { activeMustashIndex = 0; }
            if (activeMustashColorIndex >= mustashSkins.GetLength(0)) { activeMustashColorIndex = 0; }
            if (activeBeardIndex >= beardSkins.GetLength(1)) { activeBeardIndex = 0; }
            if (activeBeardColorIndex >= beardSkins.GetLength(0)) { activeBeardColorIndex = 0; }
        }

        void AddEquipmentSkinsTo(Skin combinedSkin)
        {
            var skeleton = skeletonAnimation.Skeleton;
            var skeletonData = skeleton.Data;
            combinedSkin.AddSkin(skeletonData.FindSkin(clothesSkin));
            combinedSkin.AddSkin(skeletonData.FindSkin(pantsSkin));
            if (!string.IsNullOrEmpty(beltSkin)) combinedSkin.AddSkin(skeletonData.FindSkin(beltSkin));
            if (!string.IsNullOrEmpty(bracersSkin)) combinedSkin.AddSkin(skeletonData.FindSkin(bracersSkin));
            if (!string.IsNullOrEmpty(bootsSkin)) combinedSkin.AddSkin(skeletonData.FindSkin(bootsSkin));
            if (!string.IsNullOrEmpty(helmSkin)) combinedSkin.AddSkin(skeletonData.FindSkin(helmSkin));

            if (!string.IsNullOrEmpty(mainHandSkin)) combinedSkin.AddSkin(skeletonData.FindSkin(mainHandSkin));
            if (!string.IsNullOrEmpty(offHandSkin)) combinedSkin.AddSkin(skeletonData.FindSkin(offHandSkin));
        }



        void UpdateCombinedSkin()
        {
            var skeleton = skeletonAnimation.Skeleton;
            var resultCombinedSkin = new Skin("character-combined");

            resultCombinedSkin.AddSkin(characterSkin);
            AddEquipmentSkinsTo(resultCombinedSkin);

            skeleton.SetSkin(resultCombinedSkin);
            skeleton.SetSlotsToSetupPose();
        }

        //hide hair for helm
        public void HideHair()
        {
            activeHairIndex = 0;
            UpdateCharacterSkin();
            UpdateCombinedSkin();
        }

        public void UnhideHair()
        {
            activeHairIndex = savedHairIndex;
            UpdateCharacterSkin();
            UpdateCombinedSkin();
        }

        public void HideEars()
        {
            activeEarIndex = 5;
            UpdateCharacterSkin();
            UpdateCombinedSkin();
        }

        public void UnhideEars()
        {
            activeEarIndex = savedEarIndex;
            UpdateCharacterSkin();
            UpdateCombinedSkin();
        }

        public void ChangeHands()
        {
            //activeHandColorIndex = equipment.replacingSkin;
            UpdateCharacterSkin();
            UpdateCombinedSkin();
        }

        public void ReturnHands()
        {
            activeHandColorIndex = savedHandIndex;
            UpdateCharacterSkin();
            UpdateCombinedSkin();
        }

        public void ChangeFoot()
        {
            activeFootIndex = 1;
            UpdateCharacterSkin();
            UpdateCombinedSkin();
        }

        public void ReturnFoot()
        {
            activeFootIndex = 0;
            UpdateCharacterSkin();
            UpdateCombinedSkin();
        }

        public void RandomSkin()
        {
            if (gameObject.activeSelf)
            {
                activeSkinColorIndex = Random.Range(0, colorSkins.Length);

                activeHandColorIndex = activeSkinColorIndex;
                savedHandIndex = activeHandColorIndex;

                activeFootColorIndex = activeSkinColorIndex;

                activeNoseColorIndex = activeSkinColorIndex;
                activeFheadColorIndex = activeSkinColorIndex;
                activeJawColorIndex = activeSkinColorIndex;
                activeETColorIndex = activeSkinColorIndex;
                activeEarColorIndex = activeSkinColorIndex;

                activeEyesIndex = Random.Range(0, eyesSkins.Length);
                activeMouthIndex = Random.Range(0, mouthSkins.Length);
                activeScarIndex = Random.Range(0, scarSkins.Length);
                activeNoseIndex = Random.Range(0, noseSkins.GetLength(1));
                activeFheadIndex = Random.Range(0, fheadSkins.GetLength(1));
                activeJawIndex = Random.Range(0, jawSkins.GetLength(1));
                activeETIndex = Random.Range(0, eyetypeSkins.GetLength(1));
                
                if (helmSkin == "" || helmSkin == "default")
                {
                    activeEarIndex = Random.Range(0, earSkins.GetLength(1));
                    activeHairIndex = Random.Range(0, hairSkins.GetLength(1));
                }
                
                activeHairColorIndex = Random.Range(0, hairSkins.GetLength(0));
                activeBrowIndex = Random.Range(0, browSkins.GetLength(1));
                activeBrowColorIndex = activeHairColorIndex;

                activeMustashIndex = Random.Range(0, mustashSkins.GetLength(1));
                activeMustashColorIndex = activeHairColorIndex;
                activeBeardIndex = Random.Range(0, beardSkins.GetLength(1));
                activeBeardColorIndex = activeHairColorIndex;

                heightSlider.value = Random.Range((float)0.94, 1);

                savedHairIndex = activeHairIndex;

                UpdateCharacterSkin();
                UpdateCombinedSkin();

                SaveSkinToSO();
            }
            
        }
    }
}