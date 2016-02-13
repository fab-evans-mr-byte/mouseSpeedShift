using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace mouseSpeedShift
{
    public partial class Form1 : Form
    {
        [DllImport("User32.dll")]
        static extern Boolean SystemParametersInfo(
            UInt32 uiAction,
            UInt32 uiParam,
            UInt32 pvParam,
            UInt32 fWinIni);

        [DllImport("user32.dll")]
        static extern int GetAsyncKeyState(Int32 i);

        private const UInt32 SPI_SETMOUSESPEED = 0x0071;
        private const int VK_SHIFT = 0x10;

        private bool shiftPressed = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void set_slow()
        {
            SystemParametersInfo(
                SPI_SETMOUSESPEED,
                0,
                3,
                0);
        }

        private void set_fast()
        {
            SystemParametersInfo(
                SPI_SETMOUSESPEED,
                0,
                20,
                0);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            set_fast();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Hide();
            timer1.Enabled = true;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            int keyState = GetAsyncKeyState(VK_SHIFT);
            if (!shiftPressed && keyState != 0)
            {
                shiftPressed = true;
                set_slow();
            }
            else if (shiftPressed && keyState == 0)
            {
                shiftPressed = false;
                set_fast();
            }
            timer1.Start();
        }
    }
}
