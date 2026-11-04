namespace PosmosVV.Proc
{
    public static class RandomGen
    {
        public static float RandomNum(ref int seed)
        {
            seed = (seed * 1354 + 7391) % 4273;
            return seed / 4273.0f;
        }

        public static bool Chance(float probability, ref int seed)
        {
            return RandomNum(ref seed) < probability;
        }

        public static int Interval(int min, int max, ref int seed)
        {
            return ((int)(RandomNum(ref seed) * 100000f) % (max - min)) + min;
        }

        public static float IntervalF(float min, float max, ref int seed)
        {
            return (RandomNum(ref seed) * 100000f / 100000.0f) * (max - min) + min;
        }
    }

    public static class Utility{
        public static bool IsPrime(int num){
            if (num <= 1) return false;
            if (num == 2) return true;
            if (num % 2 == 0) return false;

            for (int i = 3; i * i <= Math.Sqrt(num); i += 2)
            {
                if (num % i == 0) return false;
            }
            return true;

        }

        public static float maximum(float v1, float v2)
        {
            if (v1 > v2) return v1;
            else return v2;
        }
    }
}
