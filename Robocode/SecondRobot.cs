using Robocode;
using Robocode.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNL
{
    class SecondRobot : Robot
    {
        private int count; 
        private double gunTurnAmt; 
        private String trackName; 

        public override void Run()
        {
            var moveAmount = Math.Max(BattleFieldWidth, BattleFieldHeight);

            TurnLeft(Heading % 90);
            Ahead(moveAmount);
            TurnGunRight(90);
            TurnRight(90);

            trackName = null; 
            IsAdjustGunForRobotTurn = (true);
            gunTurnAmt = 10;

            while (true)
            {
                Ahead(moveAmount / 2);
                SearchForRobots();
                Ahead(moveAmount / 2);
                SearchForRobots();
                Ahead(-moveAmount / 2);
                SearchForRobots();
                Ahead(-moveAmount / 2);
                SearchForRobots();
            }
        }

        private void SearchForRobots()
        {
            TurnGunRight(gunTurnAmt);
            count++;
            if (count > 2) gunTurnAmt = -10;
            if (count > 5) gunTurnAmt = 10;
            if (count > 11) trackName = null;
        }

        public override void OnScannedRobot(ScannedRobotEvent e)
        {
            if (FoundWrongRobot(e.Name)) return;
            if (trackName == null) trackName = e.Name;
            count = 0;
            gunTurnAmt = Utils.NormalRelativeAngleDegrees(e.Bearing + (Heading - RadarHeading));
            TurnGunRight(gunTurnAmt);
            Fire(3);
            Scan();
        }

        private bool FoundWrongRobot(string name)
        {
            return trackName != null && name != trackName;
        }

    }
}

