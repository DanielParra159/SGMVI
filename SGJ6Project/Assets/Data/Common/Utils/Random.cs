using System;

namespace Common.Utils
{
    public sealed class Random
    {   
        private System.Random rnd;

        public Random()
        {
            rnd = new System.Random();
        }
        public Random(int seed)
        {
            rnd = new System.Random(seed);
        }
        public int RandomRange(int min, int max)
        {
            return rnd.Next(min, max);
        }
        public float RandomRange(float min, float max)
        {
            return (float)((rnd.NextDouble() * (max - min)) + min);
        }
    }

    public static class StaticRandom
    {
        private static int seed = 0;
        private static Random instance;

        public static void Init(int seed)
        {
            StaticRandom.seed = seed;
            instance = new Random(seed);
        }
        public static int RandomRange(int min, int max)
        {
            return instance.RandomRange(min, max);
        }
        public static float RandomRange(float min, float max)
        {
            return instance.RandomRange(min, max);
        }
        public static int ShowSeed()
        {
            return seed;
        }
    }
}