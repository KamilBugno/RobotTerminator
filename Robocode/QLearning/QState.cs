using Robocode;
using System;

namespace FNL.QLearning
{
    class QState
    {
        public int[] buckets = { 7, 5, 3, 5, 3 };
        public double DistanceToEnemy { set; get; } // 0 - maxDistance
        public double EnemyEnergy { set; get; } // 0 - 100
        public double EnemyVelocity { set; get; } // 0 - Rules.MAX_VELOCITY
        public double OurEnergy { set; get; } // 0 - 100
        public double OurVelocity { set; get; } // 0 - Rules.MAX_VELOCITY

        public void Discretise(double maxDistance)
        {
            DistanceToEnemy = DiscretiseData(maxDistance, DistanceToEnemy, 0);
            EnemyEnergy = DiscretiseData(100, EnemyEnergy, 1);
            EnemyVelocity = DiscretiseData(Rules.MAX_VELOCITY, EnemyVelocity, 2);
            OurEnergy = DiscretiseData(100, OurEnergy, 3);
            OurVelocity = DiscretiseData(Rules.MAX_VELOCITY, OurVelocity, 4);
        }

        private int DiscretiseData(double maxValue, double value, int numberOfBucket)
        {
            var ratios = value / maxValue;
            var obs = (int)(Math.Round((buckets[numberOfBucket] - 1.0)) * ratios);
            obs = (int)Math.Min(buckets[numberOfBucket] - 1, Math.Max(0, DistanceToEnemy));
            return obs;
        }
    }
}