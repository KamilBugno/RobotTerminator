using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNL.QLearning
{
    class QLearn
    {
        private double alpha = 0.3;
        private double gamma = 0.3;

        public void Learn(QState prevState, QState currState, int actionNubmer, double reward)
        {
            var qVal = QFunction.Q[(int)prevState.DistanceToEnemy, (int)prevState.EnemyEnergy,
                (int)prevState.EnemyVelocity, (int)prevState.OurEnergy, (int)prevState.OurVelocity,
                actionNubmer];

            var valuesForAnyActionCurrState = new double[] { QFunction.Q[(int)currState.DistanceToEnemy, (int)currState.EnemyEnergy,
                (int)currState.EnemyVelocity, (int)currState.OurEnergy, (int)currState.OurVelocity,
                0], QFunction.Q[(int)currState.DistanceToEnemy, (int)currState.EnemyEnergy,
                (int)currState.EnemyVelocity, (int)currState.OurEnergy, (int)currState.OurVelocity,
                1], QFunction.Q[(int)currState.DistanceToEnemy, (int)currState.EnemyEnergy,
                (int)currState.EnemyVelocity, (int)currState.OurEnergy, (int)currState.OurVelocity,
                2], QFunction.Q[(int)currState.DistanceToEnemy, (int)currState.EnemyEnergy,
                (int)currState.EnemyVelocity, (int)currState.OurEnergy, (int)currState.OurVelocity,
                3], QFunction.Q[(int)currState.DistanceToEnemy, (int)currState.EnemyEnergy,
                (int)currState.EnemyVelocity, (int)currState.OurEnergy, (int)currState.OurVelocity,
                4]};

            var maxQVal = valuesForAnyActionCurrState.Max();

            var newQVal = qVal + alpha * (reward + gamma * maxQVal - qVal);

            QFunction.Q[(int)prevState.DistanceToEnemy, (int)prevState.EnemyEnergy,
                (int)prevState.EnemyVelocity, (int)prevState.OurEnergy, (int)prevState.OurVelocity,
                actionNubmer] = newQVal;

        }
    }
}
