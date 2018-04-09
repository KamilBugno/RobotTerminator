using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;

namespace FNL
{
    class FirstRobot : Robot
    {

        public override void Run()
        {
            var moveAmount = Math.Max(BattleFieldWidth, BattleFieldHeight);
           
            TurnLeft(Heading % 90);
            Ahead(moveAmount);
            TurnGunRight(90);
            TurnRight(90);

            while (true)
            {
                Ahead(moveAmount / 2);
                TurnGunLeft(360);
                Ahead(moveAmount / 2);
                TurnGunLeft(360);
                Ahead(-moveAmount/2);
                TurnGunLeft(360);
                Ahead(-moveAmount / 2);
                TurnGunLeft(360);
            }
        }

        public override void OnScannedRobot(ScannedRobotEvent e)
        {
            Fire(1);
        }

    }
}
