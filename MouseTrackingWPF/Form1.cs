using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace MouseTrackingWPF
{
    
    public partial class Form1 : Form
    {
       

        private static List<Point> coords = new List<Point>();
        private  static Boolean record { get; set; }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);


        private static void recordData()
        {
            Point current_pos, prev_pos;
            prev_pos.X = 0;
            prev_pos.Y = 0;

            do
            {
                Thread.Sleep(500);
                if (GetCursorPos(out current_pos))
                {
                    if ((current_pos.X != prev_pos.X) || (current_pos.Y != prev_pos.Y))
                    {
                        Console.WriteLine("({0},{1})", current_pos.X, current_pos.Y);
                        coords.Add(current_pos);
                    }
                    prev_pos.X = current_pos.X;
                    prev_pos.Y = current_pos.Y;
                }

            } while (record);
        }

        public Form1()
        {
            InitializeComponent();
        }

        static Thread t;

        private void recordButton_click(object sender, EventArgs e)
        {
            t = new Thread(new ThreadStart(recordData));
            record = true;
            if (!t.IsAlive)
            {
                t.Start();
            }
            recordButton.Enabled = false;
            button2.Enabled = true;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            record = false;
            if (t.IsAlive)
            {
                t.Join();
            }

            string datum = DateTime.Now.ToString("MM-dd-yyyy_H_mm");
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);

            string path = appPath + @"\logdata\" + datum + ".txt";
            Console.WriteLine(path);
            Directory.CreateDirectory(appPath + @"\logdata\");
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(path)) 
            {
                foreach (Point line in coords)
                {
                    file.WriteLine(line.ToString());
                }
            }
            recordButton.Enabled = true;
            button2.Enabled = false;
        }

        private void tabPageContacts_Click(object sender, EventArgs e)
        {

        }
    }
}
