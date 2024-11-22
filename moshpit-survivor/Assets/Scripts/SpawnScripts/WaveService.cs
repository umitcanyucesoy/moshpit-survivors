using System;
using UnityEngine;

namespace SpawnScripts
{
    [Serializable]
    public class WaveService
    {
        public GameObject enemyToSpawn;
        public float waveLength;
        public float timeBetweenSpawns;
    }
}