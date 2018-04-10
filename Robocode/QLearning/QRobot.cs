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

            var i = 0;
            while (true)
            {
                i++;
                previousState = currentState;
                qAction.SampleAction();
               
                currentState.Discretise(moveAmount, previousState);
               
                states.Add(currentState);
                if(i % 50 == 0)
                    WriteToFile(currentState);
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

        private bool WriteToFile(QState qstate)
        {
            try
            {
                using (Stream count = GetDataFile("count.dat"))
                {
                    var data = count;
                    using (var tw = new StreamWriter(count))
                    {
                        data.CopyTo(tw.BaseStream);
                        tw.WriteLine((int)qstate.DistanceToEnemy + " "
                            + (int)qstate.EnemyEnergy + " "
                            + (int)qstate.EnemyVelocity + " "
                            + (int)qstate.OurEnergy + " "
                            + (int)qstate.OurVelocity + " ");
                    }
                }

            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }

        private bool ReadFile()
        {
            try
            {
                using (Stream count = GetDataFile("count.dat"))
                {
                    if (count.Length != 0)
                    {
                        using (TextReader tr = new StreamReader(count))
                        {
                            //roundCount = int.Parse(tr.ReadLine());
                            //battleCount = int.Parse(tr.ReadLine());
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        
    }
}
