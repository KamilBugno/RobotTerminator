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
                var a1 = currentState.DistanceToEnemy;
                var b2 = currentState.EnemyEnergy;
                var c3 = currentState.EnemyVelocity;
                var d4 = currentState.OurEnergy;
                var e5 = currentState.OurVelocity;
                currentState.Discretise(moveAmount);
                var a = currentState.DistanceToEnemy;
                var b = currentState.EnemyEnergy;
                var c = currentState.EnemyVelocity;
                var d = currentState.OurEnergy;
                var e = currentState.OurVelocity;
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
