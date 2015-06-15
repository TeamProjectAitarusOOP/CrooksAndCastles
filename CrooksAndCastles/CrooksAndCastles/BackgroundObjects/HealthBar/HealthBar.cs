using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CrooksAndCastles.BackgroundObjects.HealthBar
{
    class HealthBar
    {
        public HealthBar()
        {
            this.HealBar = new ProgressBar();
            this.HealBar.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.HealBar.Location = new System.Drawing.Point(10, 20);
            this.HealBar.MarqueeAnimationSpeed = 1;
            this.HealBar.Size = new System.Drawing.Size(199, 22);
            this.HealBar.TabIndex = 4;
        }

        public ProgressBar HealBar { get; set; }

        public void ChangeSize(int health)
        {
            this.HealBar.Value = health;
        }

    }
}
