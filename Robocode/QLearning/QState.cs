using Robocode;
using System;
using System.Linq;

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

        public void Discretise(double maxDistance, QState previousState)
        {
            UpdateValue(previousState, maxDistance);
        }

        private double DiscretiseData(double maxValue, double value, int numberOfBucket)
        {
            value = Math.Abs(value);
            var ratios = value / maxValue;
            var obs = (int)(Math.Round((buckets[numberOfBucket] - 1.0)) * ratios);
            var numberOne = buckets[numberOfBucket] - 1;
            var numberTwo = Math.Max(0, obs);
            obs = (Math.Min(numberOne, numberTwo));
            return obs + 0.0001;
        }

        private void UpdateValue(QState previousState, double maxDistance)
        {
            var numbers = Enumerable.Range(0, 7).ToList();
            var doubleNubmers = numbers.Select(x => x + 0.0001).ToList();

            if (doubleNubmers.Contains(DistanceToEnemy))
            {
                DistanceToEnemy = previousState.DistanceToEnemy;
            }
            else
            {
                DistanceToEnemy = DiscretiseData(maxDistance, DistanceToEnemy, 0);
            }
               
            if (doubleNubmers.Contains(EnemyEnergy))
            {
                EnemyEnergy = previousState.EnemyEnergy;
            }
            else
            {
                EnemyEnergy = DiscretiseData(100, EnemyEnergy, 1);
            }
            
            if (doubleNubmers.Contains(EnemyVelocity))
            {
                EnemyVelocity = previousState.EnemyVelocity;
            }
            else
            {
                EnemyVelocity = DiscretiseData(Rules.MAX_VELOCITY, EnemyVelocity, 2);
            }
                
            if (doubleNubmers.Contains(OurEnergy))
            {
                OurEnergy = previousState.OurEnergy;
            }
            else
            {
                OurEnergy = DiscretiseData(100, OurEnergy, 3);
            }

            if (doubleNubmers.Contains(OurVelocity))
            {
                OurVelocity = previousState.OurVelocity;
            }
            else
            {
                OurVelocity = DiscretiseData(Rules.MAX_VELOCITY, OurVelocity, 4);
            }

        }
    }
}