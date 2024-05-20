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
}
