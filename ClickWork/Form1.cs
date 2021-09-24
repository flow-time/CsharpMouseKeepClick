using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClickWork
{
    public partial class Form1 : Form
    {
        bool pauseState = false;
        int frequency = 1;
        bool auto = false;

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCurosrPos(int x, int y);

        [DllImport("user32.dll")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        const int MOUSEEVENTF_LEFTDOWN = 0x0002; //模拟鼠标左键按下

        const int MOUSEEVENTF_LEFTUP = 0x0004; //模拟鼠标左键抬起

        const int MOUSEEVENTF_RIGHTDOWN = 0x0008; //模拟鼠标右键按下

        const int MOUSEEVENTF_RIGHTUP = 0x0010; //模拟鼠标右键抬起

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //注册热键F2，Id号为103.
            //暂停
            HotKey.RegisterHotKey(Handle, 103, HotKey.KeyModifiers.None, Keys.F1);
            //开启/继续
            HotKey.RegisterHotKey(Handle, 104, HotKey.KeyModifiers.None, Keys.F2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pauseState = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pauseState = false;
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            //按快捷键    
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 103:
                            this.Text = "F1";
                            if (!verifyAutoData())
                            {
                                break;
                            }
                            pauseState = true;
                            Work();
                            break;
                        case 104:
                            this.Text = "F2";
                            pauseState = false;
                            Stop();
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// 定时<开启>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                auto = true;
            }
        }

        /// <summary>
        /// 定时<关闭>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                auto = false;
            }
        }

        Thread clicker;
        private object settextcallback;

        private void Work()
        {
            if (clicker == null ||
                !clicker.IsAlive)
            {
                clicker = new Thread(Click);
                clicker.IsBackground = true;
                clicker.Start();
            }
        }


        private void Click()
        {
            frequency = Convert.ToInt32(numericUpDown1.Value);
            while (pauseState ||
                (DateTime.Now < dateTimePicker1.Value && auto))
            {
                //for (int i = 0; i < frequency; i++)
                //{
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                //}
                Thread.Sleep(1000);
                if (DateTime.Now > dateTimePicker1.Value && auto)
                {
                    pauseState = true;
                    SetText("定时任务完成");
                    Stop();
                }
            }
            //richTextBox1.AppendText(DateTime.Now.ToString() + "\n");
        }

        private void Stop()
        {
            if (clicker != null)
            {
                clicker.Abort();
                var state = clicker.ThreadState;
            }
        }

        delegate void SetTextCallBack(string text);

        private bool verifyAutoData()
        {
            if (auto && dateTimePicker1.Value <= DateTime.Now)
            {
                richTextBox1.AppendText("定时时间必须大于当前时间");
                return false;
            }
            return true;
        }

        private void SetText(string text)
        {

            if (this.richTextBox1.InvokeRequired)
            {
                SetTextCallBack setTextCallBack = new SetTextCallBack(SetText);
                this.Invoke(setTextCallBack, text);
            }
            else
            {
                this.richTextBox1.AppendText(text);
            }
        }

    }
}
