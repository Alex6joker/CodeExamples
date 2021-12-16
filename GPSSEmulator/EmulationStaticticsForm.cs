using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GPSSEmu.GPSSBlocksImplementaion;
using GPSSEmu.Tables;

namespace GPSSEmu
{
    public partial class EmulationStaticticsForm : Form
    {
        public EmulationStaticticsForm(Object[] Stats)
        {
            InitializeComponent();
            for(int Line = 0; Line < Stats.Length; Line++)
            {
                String[] NewLineText = (String[])Stats[Line];
                if (Line == 0)
                {
                    StatisticInfo.Text += (String.Join("\t", new String[] { "START TIME", "END TIME", "BLOCKS", "FACILITIES", "STORAGES" }));
                    StatisticInfo.Text += "\n";
                }


                if (NewLineText[NewLineText.Length - 1].Contains("QUEUE"))
                    NewLineText[NewLineText.Length - 1] = "QUEUE";
                if (NewLineText[NewLineText.Length - 1].Contains("SEIZE"))
                    NewLineText[NewLineText.Length - 1] = "SEIZE";
                if (NewLineText[NewLineText.Length - 1].Contains("Device"))
                    NewLineText[NewLineText.Length - 1] = "Device";
                StatisticInfo.Text += (String.Join(" \t\t", NewLineText));
                StatisticInfo.Text += "\n";
            }
        }
    }
}
