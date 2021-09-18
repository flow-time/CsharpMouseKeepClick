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

namespace MainWork
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCurosrPos(int x, int y);

        [DllImport("user32.dll")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        const int MOUSEEVENTF_LEFTDOWN = 0x0002; //模拟鼠标左键按下

        const int MOUSEEVENTF_LEFTUP = 0x0004; //模拟鼠标左键抬起

        const int MOUSEEVENTF_RIGHTDOWN = 0x0008; //模拟鼠标右键按下

        const int MOUSEEVENTF_RIGHTUP = 0x0010; //模拟鼠标右键抬起

        int x;
        int y;
        int clickTime;
        int clickNumber;
        bool autoClose;
        bool pause;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 开启F1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (x == 0 && y == 0)
            {
                richTextBox1.AppendText("请先确定鼠标位置！");
            }
            else
            {
                Start();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 定时关闭的到期时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            var endTime = dateTimePicker1.Value;
        }

        /// <summary>
        /// 连点次数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                clickNumber = Convert.ToInt32(textBox2.Text);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// 获取鼠标位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                //Thread.Sleep(2000);
                //SetCurosrPos(mousePosition.X, mousePosition.Y);

            }
        }

        /// <summary>
        /// 暂停F2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            pause = true;

        }

        /// <summary>
        /// 键盘KeyDown事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var mousePosition = Control.MousePosition;
                richTextBox1.AppendText("当前坐标：" + mousePosition.X.ToString() + " , " + mousePosition.Y.ToString() + "\n");
                x = mousePosition.X;
                y = mousePosition.Y;
            }
            if (e.KeyCode == Keys.F12)
            {
                pause = true;
            }
        }

        /// <summary>
        /// 定时关闭（关闭）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            autoClose = false;
        }

        /// <summary>
        /// 继续F3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            pause = false;
        }

        private void Start()
        {
            pause = false;
            SetCurosrPos(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            richTextBox1.AppendText(DateTime.Now.ToString() + "\n");
            Thread.Sleep(1000);
        }

        /// <summary>
        /// 定时关闭（开启）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            autoClose = true;
        }
    }
}
