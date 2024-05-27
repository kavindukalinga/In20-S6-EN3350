using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryScript : MonoBehaviour
{
    [System.Serializable]
    public class Animals {
        public int ElephantCount = 0;
        public int LionCount = 0;
        public int TigerCount = 0;
        public int GiraffeCount = 0;
        public int ZebraCount = 0;

        public void addAnimal(int animalId) {
            if (animalId == 0) {
                ElephantCount++;
            } else if (animalId == 1) {
                LionCount++;
            } else if (animalId == 2) {
                TigerCount++;
            } else if (animalId == 3) {
                GiraffeCount++;
            } else if (animalId == 4) {
                ZebraCount++;
            }
        }
    }

    [System.Serializable]
    public class Foods {
        public int HayCount = 0;
        public int GrassCount = 0;
        public int MeatCount = 0;
        public int FishCount = 0;
        public int BananaCount = 0;
        public int MilkCount = 0;
        public int BambooCount = 0;
        public int HoneyCount = 0;
        public int MagicSpellCount = 0;

        public void addFood(int foodId) {
            if (foodId == 0) {
                HayCount++;
            } else if (foodId == 1) {
                GrassCount++;
            } else if (foodId == 2) {
                MeatCount++;
            } else if (foodId == 3) {
                FishCount++;
            } else if (foodId == 4) {
                BananaCount++;
            } else if (foodId == 5) {
                MilkCount++;
            } else if (foodId == 6) {
                BambooCount++;
            } else if (foodId == 7) {
                HoneyCount++;
            } else if (foodId == 8) {
                MagicSpellCount++;
            }
        }
    }

    public class AnimalHealth {
        public int ElephantHealth = 100;
        public int LionHealth = 100;
        public int TigerHealth = 100;
        public int GiraffeHealth = 100;
        public int ZebraHealth = 100;
    }
}
