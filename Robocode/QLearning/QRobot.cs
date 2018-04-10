using Robocode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNL.QLearning
{
    class QRobot :  AdvancedRobot
    {
        private double previousEnergy;
        private List<QState> states = new List<QState>();
        private QState currentState = new QState();
        private QState previousState = new QState();

        public override void Run()
        {
            var moveAmount = Math.Max(BattleFieldWidth, BattleFieldHeight);

            var qAction = new QAction(this);

            TurnLeft(Heading % 90);
            Ahead(moveAmount);
            TurnGunRight(90);
            TurnRight(90);

            while (true)
            {
                previousState = currentState;
                qAction.SampleAction();
               
                currentState.Discretise(moveAmount, previousState);
               
                states.Add(currentState);
            }
        }

        public override void OnScannedRobot(ScannedRobotEvent e)
        {
            Fire(1);
            currentState.DistanceToEnemy = e.Distance;
            currentState.EnemyEnergy = e.Energy;
            currentState.EnemyVelocity = e.Velocity;
        }

        public override void OnStatus(StatusEvent e)
        {
            currentState.OurEnergy = e.Status.Energy;
            currentState.OurVelocity = e.Status.Velocity;
            base.OnStatus(e);
        }
    }
}
