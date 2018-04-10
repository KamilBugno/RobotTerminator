using Robocode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FNL.QLearning
{
    class QRobot :  AdvancedRobot
    {
        private List<QState> states = new List<QState>();
        private QState currentState = new QState();
        private QState previousState = new QState();

        public override void Run()
        {
            var moveAmount = Math.Max(BattleFieldWidth, BattleFieldHeight);

            var qAction = new QAction(this);
            var qLearn = new QLearn();

            //ReadQFunctionFromFile();

            //var a = QFunction.Q;

            Ahead(moveAmount);
            TurnGunRight(90);
            TurnRight(90);

            while (true)
            {
                previousState = currentState;
                var actionNumber = qAction.SampleAction();
                currentState.Discretise(moveAmount, previousState);
                qLearn.Learn(previousState, currentState, actionNumber, currentState.OurEnergy);
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

        public override void OnRoundEnded(RoundEndedEvent e)
        {
            //WriteQFunctionToFile();
            base.OnRoundEnded(e);
        }

        public override void OnRobotDeath(RobotDeathEvent e)
        {
            //WriteQFunctionToFile();
            base.OnRobotDeath(e);
        }   

        private bool WriteQFunctionToFile()
        {
            try
            {
                using (Stream count = GetDataFile("count.dat"))
                {
                    var data = count;
                    using (TextWriter tw = new StreamWriter(count))
                    {
                        for(var i = 0; i < QFunction.one; i++)
                        {
                            for(var j = 0; j < QFunction.two; j++)
                            {
                                for(var k = 0; k < QFunction.three; k++)
                                {
                                    for(var m = 0; m < QFunction.four; m++)
                                    {
                                        for(var n = 0; n < QFunction.five; n++)
                                        {
                                            for(var f = 0; f < QFunction.six; f++)
                                            {
                                                tw.WriteLine(QFunction.Q[i,j,k,m,n,f]);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        private bool WriteToFile(QState qstate, int actionNumber)
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
                            + (int)qstate.OurVelocity + " "
                            + actionNumber);
                    }
                }

            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }

        private bool ReadQFunctionFromFile()
        {
            try
            {
                using (Stream count = GetDataFile("count.dat"))
                {
                    if (count.Length != 0)
                    {
                        using (TextReader tr = new StreamReader(count))
                        {
                            for (var i = 0; i < QFunction.one; i++)
                            {
                                for (var j = 0; j < QFunction.two; j++)
                                {
                                    for (var k = 0; k < QFunction.three; k++)
                                    {
                                        for (var m = 0; m < QFunction.four; m++)
                                        {
                                            for (var n = 0; n < QFunction.five; n++)
                                            {
                                                for (var f = 0; f < QFunction.six; f++)
                                                {
                                                    QFunction.Q[i, j, k, m, n, f] = double.Parse(tr.ReadLine());
                                                }
                                            }
                                        }
                                    }
                                }
                            }
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
