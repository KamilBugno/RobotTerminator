using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNL.QLearning
{
    class QAction
    {
        private QRobot qrobot;
        private Random random;

        public QAction(QRobot qrobot)
        {
            this.qrobot = qrobot;
            random = new Random();
        }

        public int SampleAction()
        {
            var actionNumber = random.Next(0, 5);
            switch(actionNumber)
            {
                case 0:
                    qrobot.Ahead(100);
                    break;
                case 1:
                    qrobot.TurnLeft(90);
                    break;
                case 2:
                    qrobot.TurnRight(90);
                    break;
                case 3:
                    qrobot.TurnGunLeft(90);
                    break;
                case 4:
                    qrobot.TurnGunRight(90);
                    break;
                
            }
            return actionNumber;
        }
    }
}
