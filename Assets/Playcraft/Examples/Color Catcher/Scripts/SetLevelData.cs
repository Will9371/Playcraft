using System;
using UnityEngine;

namespace Playcraft.Examples.ColorCatcher
{
    public class SetLevelData : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] GetIntByThreshold scoreRef;
        [SerializeField] GetFloatFromArray spawnTimeRef;
        [SerializeField] GetFloatFromArray[] riseSpeedRef;
        [SerializeField] GetFloatFromArray[] fastSpeedRef;
        [SerializeField] LevelData[] levels;
        [SerializeField] bool validate;
        #pragma warning restore 0649
    
        private void OnValidate()
        {
            if (!validate) return;
    
            var levelCount = levels.Length;
            var scores = new int[levelCount];
            var spawnTimes = new float[levelCount];
            var riseSpeeds = new float[levelCount];
            var fastSpeeds = new float[levelCount];
        
            for (int i = 0; i < levelCount; i++)
            {
                scores[i] = levels[i].minimumScore;
                spawnTimes[i] = levels[i].timeBetweenSpawns;
                riseSpeeds[i] = levels[i].riseSpeeds;
                fastSpeeds[i] = levels[i].fastSpeeds;
            }
        
            scoreRef.SetMinimums(scores);
            spawnTimeRef.SetValues(spawnTimes);
            
            foreach (var speed in riseSpeedRef)
                speed.SetValues(riseSpeeds);
        
            foreach (var speed in fastSpeedRef)
                speed.SetValues(fastSpeeds);
        }
    
        [Serializable] struct LevelData
        {
            #pragma warning disable 0649
            public int minimumScore;
            public float timeBetweenSpawns;
            public float riseSpeeds;
            public float fastSpeeds;
            #pragma warning restore 0649
        }
    }
}

