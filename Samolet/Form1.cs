using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Net;
using System.Numerics;
using System.Windows.Forms;
using System.Linq;

namespace Samolet
{
       partial class Form1 : Form
    {
        public int b;
        public int c;
        public int d;
        public int param1 = 142;
        public int param2 = 130;
        public int param3 = 142;
        public int param4 = 220;

        public int Param1 = 148;
        public int Param2 = 156;
        public int Param3 = 88;
        public int Param4 = 156;
        public int Param5 = 148;
        public int Param6 = 186;
        
        public int Param12 = 188;
        public int Param22 = 156;
        public int Param32 = 248;
        public int Param42 = 156;
        public int Param52 = 188;
        public int Param62 = 186;
        //public int Param72 = 220;
        float angle = 0;
        public int n = 0;
        Image image1 = Image.FromFile(@"C:\Users\lilm8\Desktop\samolet\Samolet\Samolet\22.png");
        Model model;


        public Form1()
        {
            InitializeComponent();
            trackBar1.Scroll += trackBar1_Scroll;
            trackBar1.MouseDown += trackBar1_MouseDown;
            trackBar1.MouseUp += trackBar1_MouseUp;
            label1.Text = string.Format("" + trackBar1.Value +"ft");
            label2.Text = string.Format("9500 ft");
            label3.Text = string.Format("Высота");
            label7.Text = String.Format("" + (trackBar1.Value + 500));
            label8.Text = String.Format("" + trackBar1.Value);
            label9.Text = String.Format("" + (trackBar1.Value - 500));
            label5.Text = String.Format(""+(trackBar2.Value) + "km/h");
            label6.Text = String.Format("Скорость km/h");
            label4.Text = String.Format("600 km/h");
            label12.Text = String.Format(""  + trackBar2.Value);
            label13.Text = String.Format("" + (trackBar2.Value + 20));
            label14.Text = String.Format("" + (trackBar2.Value + 40));


            model = new Model();
            //model.LoadFromObj(new StreamReader("c:\\1.obj"));
            model.LoadFromObj(new StreamReader(new WebClient().OpenRead("http://www.wonthelp.info/superjoebob/TutorialImages/objPlane.obj")));


            pictureBox1.Bounds = new Rectangle(43, 220, 500, 500);
            pictureBox1.Paint += new PaintEventHandler(pictureBox1_Paint);
            pictureBox3.Paint += new PaintEventHandler(pictureBox3_Paint);

            if (trackBar3.Value == 0)
            {
                panel1.Enabled = false;
            }
            else
            {
                panel1.Enabled = true;
            }




        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
       


        void pictureBox3_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = pictureBox3.CreateGraphics();
            Pen WhitePen = new Pen(Color.FromArgb(255, 255, 0, 0), 4);
            g.DrawLine(WhitePen, param1, param2, param3, param4);
        }

        float yaw = 0;
        float pitch = 0;
        float roll = 0;
        float scale = 10;
        Vector3 position = new Vector3(200, 200, 200);
        void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var scaleM = Matrix4x4.CreateScale(scale / 2);
            //матрица вращения
            var rotateM = Matrix4x4.CreateFromYawPitchRoll(yaw, pitch, roll);
            //матрица переноса
            var translateM = Matrix4x4.CreateTranslation(position);
            //матрица проекции
            var paneXY = new Matrix4x4() { M11 = 1f, M22 = 1f, M44 = 1f };
            //результирующая матрица
            var m = scaleM * rotateM * translateM * paneXY;

            //умножаем вектора на матрицу
            var vertexes = model.Vertexes.Select(v => Vector3.Transform(v, m)).ToList();

            //создаем graphicsPath
            using (var path = new GraphicsPath())
            {
                //создаем грани
                var prev = Vector3.Zero;
                var prevF = 0;
                foreach (var f in model.Fig)
                {
                    if (f == 0) path.CloseFigure();
                    var v = vertexes[f];
                    if (prevF != 0 && f != 0)
                        path.AddLine(prev.X, prev.Y, v.X, v.Y);
                    prev = v;
                    prevF = f;
                }

                //отрисовываем
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                e.Graphics.DrawPath(Pens.Black, path);
            }
        }
        public void trackBar1_MouseDown(object sender, EventArgs e)
        {
            b = trackBar1.Value;
            

        }

        public void trackBar1_MouseUp(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
            angle = 0f;
            param4 = (int)(90 * Math.Sin((2 * Math.PI * (trackBar1.Value - 2500)) / 10000) + 130);
            param3 = (int)(90 * Math.Cos((2 * Math.PI * (trackBar1.Value - 2500)) / 10000) + 142);
            Line(param3, param4);
            Param1 = 140;
            Param2 = 206;
            Param3 = 80;
            Param4 = 206;
            Param5 = 140;
            Param6 = 246;

            Param12 = 180;
            Param22 = 206;
            Param32 = 240;
            Param42 = 206;
            Param52 = 180;
            Param62 = 246;
            Line2(Param1, Param2, Param3, Param4, Param5, Param6, Param12, Param22, Param32, Param42, Param52, Param62);

        }

        public void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = string.Format("" + trackBar1.Value + "ft");
            int a = trackBar1.Value / 1000;
            label9.Text = String.Format("" + a * 1000);
            label8.Text = String.Format("" + trackBar1.Value);
            label7.Text = String.Format("" + ((a * 1000)+1000));
            if (b > trackBar1.Value)
            {
                pictureBox1.Invalidate();
                angle = -14f;
                Param2 = (int)((20 * Math.Sin(Math.PI / 4) + 206) );
                Param1 = (int)(20 * Math.Cos(Math.PI / 4) + 168);
                Param4 = (int)((80 * Math.Sin(Math.PI / 4) + 206) );
                Param3 = (int)(80 * Math.Cos(Math.PI / 4) + 168) ;
                Param6 = (int)((60 * Math.Sin(Math.PI * 4 / 6) + 206));
                Param5 = (int)(60 * Math.Cos(Math.PI * 4 / 6) + 168);
                Param22 = (int)((20 * Math.Sin(Math.PI * 5 / 4) + 206) );
                Param12 = (int)(20 * Math.Cos(Math.PI * 5 / 4) + 168);
                Param42 = (int)((80 * Math.Sin(Math.PI * 5 / 4) + 206) );
                Param32 = (int)(80 * Math.Cos(Math.PI * 5 / 4) + 168);
                Param62 = (int)((60 * Math.Sin(Math.PI * 5 / 6) + 206) );
                Param52 = (int)(60 * Math.Cos(Math.PI * 5 / 6) + 168);
                yaw = b;
                pitch = b;
                roll = 0;
                scale = 10;

            } else
            {
                pictureBox1.Invalidate();
                angle = 14f;
                Param2 = (int)(20 * Math.Sin(Math.PI *7/4) + 206);
                Param1 = (int)(20 * Math.Cos(Math.PI *7/ 4) + 168);
                Param4 = (int)(80 * Math.Sin(Math.PI *7/ 4) + 206);
                Param3 = (int)(80 * Math.Cos(Math.PI *7/ 4) + 168);
                Param6 = (int)(60 * Math.Sin((-Math.PI * 5 / 6)-Math.PI) + 206);
                Param5 = (int)(60 * Math.Cos((-Math.PI * 5 / 6) - Math.PI) + 168);
                Param22 = (int)(20 * Math.Sin(-Math.PI * 5 / 4) + 206);
                Param12 = (int)(20 * Math.Cos(-Math.PI * 5 / 4) + 168);
                Param42 = (int)(80 * Math.Sin(-Math.PI * 5 / 4) + 206);
                Param32 = (int)(80 * Math.Cos(-Math.PI * 5 / 4) + 168);
                Param62 = (int)(60 * Math.Sin((-Math.PI * 4 / 6) - Math.PI) + 206);
                Param52 = (int)(60 * Math.Cos((-Math.PI * 4 / 6) - Math.PI) + 168);
                yaw = b;
                pitch = 0;
                roll = b;
                scale = 10;

            }
            pictureBox2.Image = Properties.Resources.visot1;
;
            Line2(Param1, Param2, Param3, Param4, Param5, Param6, Param12, Param22, Param32, Param42, Param52, Param62);
            param4 = (int)(90*Math.Sin((2 * Math.PI * (trackBar1.Value - 2500)) / 10000)+130);
            param3 = (int)(90*Math.Cos((2 * Math.PI * (trackBar1.Value - 2500)) / 10000)+142);
            pictureBox3.Image = Properties.Resources.res;
            Line(param3, param4);
         

        }

        private void Line(int param3, int param4)
        {
           
            Graphics g = pictureBox3.CreateGraphics();
            Pen WhitePen = new Pen(Color.FromArgb(255, 255, 0, 0), 4);
            g.DrawLine(WhitePen, param1, param2, param3, param4);
        }

        private void Line2(int Param1, int Param2, int Param3, int Param4, int Param5, int Param6, int Param12, int Param22, int Param32, int Param42,
            int Param52, int Param62)
        {
            double j = (((trackBar1.Value * 100)/ 9500)+0.001);
            Param2 = (int)(Param2 - j);
            Param4 = (int)(Param4 - j);
            Param22 = (int)(Param22 -j);
            Param42 = (int)(Param42 - j);
            Param62 = (int)(Param62 - j);
            Param6 = (int)(Param6 -j);
            Graphics g = pictureBox2.CreateGraphics();
            //g.Clear(Color.White);
            Pen YellowPen = new Pen(Color.FromArgb(255, 255, 255, 0), 4);
            g.DrawLine(YellowPen, Param1, Param2, Param3, Param4);
            g.DrawLine(YellowPen, Param12, Param22, Param32, Param42);
            g.DrawLine(YellowPen, Param1, Param2, Param5, Param6);
            g.DrawLine(YellowPen, Param12, Param22, Param52, Param62);
            //g.DrawLine(YellowPen, Param1, Param2, Param1, Param2 + 8);
            
        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

     

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label18 = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label36 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.trackBar6 = new System.Windows.Forms.TrackBar();
            this.label38 = new System.Windows.Forms.Label();
            this.trackBar5 = new System.Windows.Forms.TrackBar();
            this.label35 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.trackBar4 = new System.Windows.Forms.TrackBar();
            this.label32 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.label15 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1306, 0);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.textBox1.Location = new System.Drawing.Point(537, 40);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(437, 420);
            this.textBox1.TabIndex = 32;
            this.textBox1.Visible = false;
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.SystemColors.Control;
            this.label22.Location = new System.Drawing.Point(862, 62);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(18, 20);
            this.label22.TabIndex = 33;
            this.label22.Text = "X";
            this.label22.Visible = false;
            this.label22.Click += new System.EventHandler(this.label22_Click);
            // 
            // pictureBox9
            // 
            this.pictureBox9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox9.Image")));
            this.pictureBox9.Location = new System.Drawing.Point(998, 114);
            this.pictureBox9.MaximumSize = new System.Drawing.Size(294, 267);
            this.pictureBox9.MinimumSize = new System.Drawing.Size(294, 267);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(294, 267);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox9.TabIndex = 22;
            this.pictureBox9.TabStop = false;
            // 
            // pictureBox10
            // 
            this.pictureBox10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(153)))), ((int)(((byte)(209)))));
            this.pictureBox10.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox10.Image")));
            this.pictureBox10.Location = new System.Drawing.Point(3, 423);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(510, 99);
            this.pictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox10.TabIndex = 26;
            this.pictureBox10.TabStop = false;
            this.pictureBox10.Click += new System.EventHandler(this.pictureBox10_Click);
            // 
            // pictureBox11
            // 
            this.pictureBox11.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox11.Image")));
            this.pictureBox11.Location = new System.Drawing.Point(53, 62);
            this.pictureBox11.MaximumSize = new System.Drawing.Size(152, 439);
            this.pictureBox11.MinimumSize = new System.Drawing.Size(138, 180);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(138, 189);
            this.pictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox11.TabIndex = 31;
            this.pictureBox11.TabStop = false;
            this.pictureBox11.Click += new System.EventHandler(this.pictureBox11_Click);
            // 
            // pictureBox12
            // 
            this.pictureBox12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox12.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox12.Image")));
            this.pictureBox12.Location = new System.Drawing.Point(231, 199);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(221, 182);
            this.pictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox12.TabIndex = 34;
            this.pictureBox12.TabStop = false;
            this.pictureBox12.Click += new System.EventHandler(this.pictureBox12_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(214, 530);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(0, 20);
            this.label23.TabIndex = 35;
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(32, 525);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(249, 20);
            this.label24.TabIndex = 36;
            this.label24.Text = "ПАНЕЛЬ УПРАВЛЕНИЯ РЕЖИМОМ";
            // 
            // label25
            // 
            this.label25.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(309, 384);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(52, 20);
            this.label25.TabIndex = 37;
            this.label25.Text = "MMR1";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(77, 263);
            this.label26.MaximumSize = new System.Drawing.Size(120, 50);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(82, 20);
            this.label26.TabIndex = 38;
            this.label26.Text = "ADRIU Left";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1104, 630);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(185, 50);
            this.button1.TabIndex = 45;
            this.button1.Text = "FAQ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label27.Location = new System.Drawing.Point(151, 139);
            this.label27.Name = "label27";
            this.label27.Padding = new System.Windows.Forms.Padding(0, 0, 167, 0);
            this.label27.Size = new System.Drawing.Size(396, 20);
            this.label27.TabIndex = 42;
            this.label27.Text = "Инерциальная система отсчета";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pictureBox4.BackColor = System.Drawing.Color.Yellow;
            this.pictureBox4.Location = new System.Drawing.Point(642, 52);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(368, 533);
            this.pictureBox4.TabIndex = 46;
            this.pictureBox4.TabStop = false;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label18.Location = new System.Drawing.Point(649, 129);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(131, 40);
            this.label18.TabIndex = 27;
            this.label18.Text = "Пневматический \r\nсенсор";
            this.label18.Click += new System.EventHandler(this.label18_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.pictureBox6.Location = new System.Drawing.Point(862, 62);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(130, 436);
            this.pictureBox6.TabIndex = 2;
            this.pictureBox6.TabStop = false;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label19.Location = new System.Drawing.Point(888, 223);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(85, 60);
            this.label19.TabIndex = 28;
            this.label19.Text = "Обработка\r\n графики \r\nи данных";
            this.label19.Click += new System.EventHandler(this.label19_Click);
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label21.Location = new System.Drawing.Point(865, 525);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(93, 40);
            this.label21.TabIndex = 30;
            this.label21.Text = "Внутренний\r\nдатчик\r\n";
            this.label21.Click += new System.EventHandler(this.label21_Click);
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label20.Location = new System.Drawing.Point(671, 525);
            this.label20.Name = "label20";
            this.label20.Padding = new System.Windows.Forms.Padding(10);
            this.label20.Size = new System.Drawing.Size(89, 40);
            this.label20.TabIndex = 29;
            this.label20.Text = "Питание\r\n";
            this.label20.Click += new System.EventHandler(this.label20_Click);
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label29.Location = new System.Drawing.Point(435, 463);
            this.label29.Name = "label29";
            this.label29.Padding = new System.Windows.Forms.Padding(0, 0, 256, 0);
            this.label29.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label29.Size = new System.Drawing.Size(437, 20);
            this.label29.TabIndex = 44;
            this.label29.Text = "Выбранный курс/режим";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label28.Location = new System.Drawing.Point(397, 280);
            this.label28.Name = "label28";
            this.label28.Padding = new System.Windows.Forms.Padding(0, 0, 110, 0);
            this.label28.Size = new System.Drawing.Size(363, 20);
            this.label28.TabIndex = 43;
            this.label28.Text = "Курсовой маяк и наклон глиссады ";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(420, 30);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1306, 724);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 47;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.flowLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1298, 686);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Модель";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(153)))), ((int)(((byte)(209)))));
            this.flowLayoutPanel1.Controls.Add(this.panel3);
            this.flowLayoutPanel1.Controls.Add(this.panel4);
            this.flowLayoutPanel1.Controls.Add(this.panel5);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1292, 680);
            this.flowLayoutPanel1.TabIndex = 113;
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.Controls.Add(this.label36);
            this.panel3.Controls.Add(this.label33);
            this.panel3.Controls.Add(this.label37);
            this.panel3.Controls.Add(this.label34);
            this.panel3.Controls.Add(this.trackBar6);
            this.panel3.Controls.Add(this.label38);
            this.panel3.Controls.Add(this.trackBar5);
            this.panel3.Controls.Add(this.label35);
            this.panel3.Controls.Add(this.label30);
            this.panel3.Controls.Add(this.label31);
            this.panel3.Controls.Add(this.trackBar4);
            this.panel3.Controls.Add(this.label32);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.label16);
            this.panel3.Controls.Add(this.trackBar3);
            this.panel3.Controls.Add(this.label15);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(291, 136);
            this.panel3.TabIndex = 109;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint_1);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.BackColor = System.Drawing.Color.Silver;
            this.label36.Location = new System.Drawing.Point(232, 32);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(33, 20);
            this.label36.TabIndex = 120;
            this.label36.Text = "Вкл";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.BackColor = System.Drawing.Color.Silver;
            this.label33.Location = new System.Drawing.Point(158, 32);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(33, 20);
            this.label33.TabIndex = 116;
            this.label33.Text = "Вкл";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(225, 116);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(44, 20);
            this.label37.TabIndex = 119;
            this.label37.Text = "Выкл";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(151, 116);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(44, 20);
            this.label34.TabIndex = 115;
            this.label34.Text = "Выкл";
            // 
            // trackBar6
            // 
            this.trackBar6.LargeChange = 1;
            this.trackBar6.Location = new System.Drawing.Point(225, 52);
            this.trackBar6.Maximum = 1;
            this.trackBar6.Name = "trackBar6";
            this.trackBar6.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBar6.Size = new System.Drawing.Size(56, 62);
            this.trackBar6.TabIndex = 118;
            this.trackBar6.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.BackColor = System.Drawing.Color.Salmon;
            this.label38.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label38.Location = new System.Drawing.Point(219, 7);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(69, 23);
            this.label38.TabIndex = 117;
            this.label38.Text = "Отказ 3";
            // 
            // trackBar5
            // 
            this.trackBar5.LargeChange = 1;
            this.trackBar5.Location = new System.Drawing.Point(158, 52);
            this.trackBar5.Maximum = 1;
            this.trackBar5.Name = "trackBar5";
            this.trackBar5.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBar5.Size = new System.Drawing.Size(56, 62);
            this.trackBar5.TabIndex = 114;
            this.trackBar5.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.BackColor = System.Drawing.Color.Salmon;
            this.label35.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label35.Location = new System.Drawing.Point(145, 7);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(69, 23);
            this.label35.TabIndex = 113;
            this.label35.Text = "Отказ 2";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.BackColor = System.Drawing.Color.Silver;
            this.label30.Location = new System.Drawing.Point(84, 32);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(33, 20);
            this.label30.TabIndex = 112;
            this.label30.Text = "Вкл";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(77, 116);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(44, 20);
            this.label31.TabIndex = 111;
            this.label31.Text = "Выкл";
            // 
            // trackBar4
            // 
            this.trackBar4.LargeChange = 1;
            this.trackBar4.Location = new System.Drawing.Point(84, 52);
            this.trackBar4.Maximum = 1;
            this.trackBar4.Name = "trackBar4";
            this.trackBar4.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBar4.Size = new System.Drawing.Size(56, 62);
            this.trackBar4.TabIndex = 110;
            this.trackBar4.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.BackColor = System.Drawing.Color.Salmon;
            this.label32.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label32.Location = new System.Drawing.Point(70, 7);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(69, 23);
            this.label32.TabIndex = 109;
            this.label32.Text = "Отказ 1";
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Silver;
            this.label17.Location = new System.Drawing.Point(-1522, 32);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(33, 20);
            this.label17.TabIndex = 108;
            this.label17.Text = "Вкл";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 116);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(44, 20);
            this.label16.TabIndex = 107;
            this.label16.Text = "Выкл";
            // 
            // trackBar3
            // 
            this.trackBar3.LargeChange = 1;
            this.trackBar3.Location = new System.Drawing.Point(15, 52);
            this.trackBar3.Maximum = 1;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBar3.Size = new System.Drawing.Size(56, 62);
            this.trackBar3.TabIndex = 106;
            this.trackBar3.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar3.Scroll += new System.EventHandler(this.trackBar3_Scroll);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Salmon;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(64, 32);
            this.label15.TabIndex = 105;
            this.label15.Text = "Сеть";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.trackBar1);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.trackBar2);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Enabled = false;
            this.panel4.Location = new System.Drawing.Point(300, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(624, 136);
            this.panel4.TabIndex = 110;
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.LargeChange = 100;
            this.trackBar1.Location = new System.Drawing.Point(115, 7);
            this.trackBar1.Maximum = 9500;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(332, 56);
            this.trackBar1.SmallChange = 100;
            this.trackBar1.TabIndex = 100;
            this.trackBar1.TickFrequency = 500;
            this.trackBar1.Value = 4500;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "label3";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(453, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 20);
            this.label2.TabIndex = 14;
            this.label2.Text = "label2";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 20);
            this.label6.TabIndex = 19;
            this.label6.Text = "label6";
            // 
            // trackBar2
            // 
            this.trackBar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar2.LargeChange = 10;
            this.trackBar2.Location = new System.Drawing.Point(115, 52);
            this.trackBar2.Maximum = 600;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(332, 56);
            this.trackBar2.SmallChange = 5;
            this.trackBar2.TabIndex = 10;
            this.trackBar2.TabStop = false;
            this.trackBar2.TickFrequency = 25;
            this.trackBar2.Value = 300;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(453, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 20);
            this.label4.TabIndex = 22;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(59, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 20);
            this.label5.TabIndex = 21;
            this.label5.Text = "label5";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel5.Controls.Add(this.pictureBox1);
            this.panel5.Location = new System.Drawing.Point(3, 145);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(781, 514);
            this.panel5.TabIndex = 111;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Location = new System.Drawing.Point(106, 104);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(562, 304);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.flowLayoutPanel2.Controls.Add(this.panel1);
            this.flowLayoutPanel2.Controls.Add(this.panel6);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(790, 145);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(497, 514);
            this.flowLayoutPanel2.TabIndex = 112;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(153)))), ((int)(((byte)(209)))));
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(494, 259);
            this.panel1.TabIndex = 16;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // button4
            // 
            this.button4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button4.BackColor = System.Drawing.Color.DimGray;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 12.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button4.Location = new System.Drawing.Point(623, 63);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(21, 57);
            this.button4.TabIndex = 112;
            this.button4.Text = "+";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.Color.DimGray;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button3.Location = new System.Drawing.Point(269, 8);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(58, 28);
            this.button3.TabIndex = 111;
            this.button3.Text = "HP/IN";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.AutoSize = true;
            this.button2.BackColor = System.Drawing.Color.DimGray;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button2.Location = new System.Drawing.Point(182, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(62, 28);
            this.button2.TabIndex = 110;
            this.button2.Text = "APP";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.DimGray;
            this.label14.ForeColor = System.Drawing.SystemColors.Control;
            this.label14.Location = new System.Drawing.Point(994, 39);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(25, 20);
            this.label14.TabIndex = 103;
            this.label14.Text = "50";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.DimGray;
            this.label13.ForeColor = System.Drawing.SystemColors.Control;
            this.label13.Location = new System.Drawing.Point(994, 84);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(25, 20);
            this.label13.TabIndex = 102;
            this.label13.Text = "50";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Black;
            this.label12.ForeColor = System.Drawing.SystemColors.Control;
            this.label12.Location = new System.Drawing.Point(994, 124);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(25, 20);
            this.label12.TabIndex = 101;
            this.label12.Text = "30";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.DimGray;
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(1129, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 20);
            this.label7.TabIndex = 16;
            this.label7.Text = "label7";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.DimGray;
            this.label9.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label9.Location = new System.Drawing.Point(1129, -174);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 20);
            this.label9.TabIndex = 18;
            this.label9.Text = "label9";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Black;
            this.label8.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label8.Location = new System.Drawing.Point(1129, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 20);
            this.label8.TabIndex = 17;
            this.label8.Text = "label8";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = global::Samolet.Properties.Resources.visot1;
            this.pictureBox2.Location = new System.Drawing.Point(129, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(251, 269);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.Controls.Add(this.pictureBox3);
            this.panel6.Location = new System.Drawing.Point(3, 268);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(494, 234);
            this.panel6.TabIndex = 17;
            this.panel6.Paint += new System.Windows.Forms.PaintEventHandler(this.panel6_Paint);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.Image = global::Samolet.Properties.Resources.res;
            this.pictureBox3.Location = new System.Drawing.Point(129, 10);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(251, 224);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1298, 686);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Схема";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(153)))), ((int)(((byte)(209)))));
            this.panel2.Controls.Add(this.label26);
            this.panel2.Controls.Add(this.label22);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.label27);
            this.panel2.Controls.Add(this.label28);
            this.panel2.Controls.Add(this.label29);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.label21);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Controls.Add(this.pictureBox6);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.pictureBox4);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.label25);
            this.panel2.Controls.Add(this.label24);
            this.panel2.Controls.Add(this.label23);
            this.panel2.Controls.Add(this.pictureBox12);
            this.panel2.Controls.Add(this.pictureBox11);
            this.panel2.Controls.Add(this.pictureBox10);
            this.panel2.Controls.Add(this.pictureBox9);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1292, 680);
            this.panel2.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(153)))), ((int)(((byte)(209)))));
            this.ClientSize = new System.Drawing.Size(1306, 724);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.SizeChanged += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            if (panel1.Visible != true) { panel2.Hide(); panel1.Show(); panel3.Show(); }
            

        }

        private void label11_Click(object sender, EventArgs e)
        {
            if (panel2.Visible != true) { panel1.Hide(); panel2.Show(); panel3.Hide(); }
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            if (textBox1.Visible != true) { label22.Show();  textBox1.Show();
                n = 1;
            }

            TextCreate(pictureBox11, e);
        }

        

        private void label22_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Visible == true) { textBox1.Hide(); label22.Hide(); }
            
        }
        private void pictureBox12_Click(object sender, EventArgs e)
        {
            if (textBox1.Visible != true)
            {
                label22.Show(); textBox1.Show();
                n = 2;
            }
            TextCreate(pictureBox12, e);
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            if (textBox1.Visible != true)
            {
                label22.Show(); textBox1.Show();
                n = 3;
            }
            TextCreate(pictureBox10, e);
        }

        private void label18_Click(object sender, EventArgs e)
        {
            if (textBox1.Visible != true)
            {
                label22.Show(); textBox1.Show();
                n = 4;
            }
            TextCreate(label18, e);
        }

        private void label19_Click(object sender, EventArgs e)
        {
            if (textBox1.Visible != true)
            {
                label22.Show(); textBox1.Show();
                n = 5;
            }
            TextCreate(label19, e);
        }

        private void label21_Click(object sender, EventArgs e)
        {
            if (textBox1.Visible != true)
            {
                label22.Show(); textBox1.Show();
                n = 6;
            }
            TextCreate(label21, e);
        }

        private void label20_Click(object sender, EventArgs e)
        {
            if (textBox1.Visible != true)
            {
                label22.Show(); textBox1.Show();
                n = 7;
                

            }
            TextCreate(label20, e);

        }

        public void TextCreate (object sender, EventArgs e)
        {
            switch (n)
            {
                case 1:
                    textBox1.Text = "\r\n\r\nAdiru left Совмещённая система воздушных сигналов и инерциальная система. Инерциальная система отсчета воздушных данных(ADIRS) предоставляет данные такого типа экипажу и системам самолета как высота, скорость, температура, давление. Датчик общей температуры воздуха измеряет температуру наружного воздуха. Он изменяет значение температуры в электрический сигнал.Электрический сигнал поступает на совмещённая систему воздушных сигналов и инерциальная систему.Датчики угла атаки измеряют и преобразуют в электрические сигналы.Эти сигналы поступают в совмещённую систему воздушных сигналов и инерциальную систему.Блок индикации инерциальной системы передает данные о начальном положении и курсе в совмещённую систему воздушных сигналов и инерциальную систему. ";
                    break;
                case 2:
                    textBox1.Text = "\r\n\r\nMMR Многорежимный приемник содержит приемник курсо-глиссадную систему и блок датчика глобальной системы позиционирования. Функция курсо-глиссадного приемника обеспечивает информацию об курсовом маяке и отклонение от глиссады различным системам самолета. Блок датчика глобальной системы позиционирования передает данные о положении и времени в компьютерную систему управления полетом";
                    break;
                case 3:
                    textBox1.Text = "\r\n\r\nMode control panel панель управления режимами. Компьютер управления полетом передают данные на панель управления режимами.Панель управления режимами отправляет данные в FMCS";
                    break;
                case 4:
                    textBox1.Text = "\r\n\r\n Pnevmartic";
                    break;
                case 5:
                    textBox1.Text = "\r\n\r\n Обработчик";
                    break;
                case 6:
                    textBox1.Text = "\r\n\r\nВнутренний датчик";
                    break;
                case 7:
                    textBox1.Text = "\r\n\r\nПитание";
                    break;
                case 8:
                    textBox1.Text = "\r\n\r\nПолетный дисплей  INTEGRATED STANDBY FLIGHT DISPLAY Интегрированный полетный дисплей предоставляет летному экипажу параметры: \r\n угол продольного и поперечного наклона;\r\nуказанную скорость полёта;\r\n высоту;\r\nметрическая высота.\r\nПолетный дисплей получает данные о курсе от инерциального опорного блока воздушных данных. А также рассчитывает внутри блока высоту, положение самолета и его воздушную скорость.Полетный дисплей имеет элементы управления на панели прибора:\r\nпереключатели яркости дисплея;\r\nпереключатель режима захода на посадку;\r\nпереключатель барометрической коррекции(гектопаскали / дюймы ртутного столба);\r\nбарометрический контроль;\r\nпереключатель сброса положения;";
                    break;
            }
            
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (textBox1.Visible != true)
            {
                label22.Show(); textBox1.Show();
                n = 8;
            }
            TextCreate(pictureBox4, e);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label12.Text = String.Format("" + trackBar2.Value);
            label13.Text = String.Format("" + (trackBar2.Value + 20));
            label14.Text = String.Format("" + (trackBar2.Value + 40));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("C:\\Users\\lilm8\\Desktop\\samolet\\Samolet\\Samolet\\Boeing.pdf") { UseShellExecute =true});
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            c = +1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            d += 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if ((c + d) >= 2)
            {
                Form2 form2 = new Form2();
                form2.Show();
                c = 0;
                d = 0;
            }
            else { MessageBox.Show("Не верно введена команда"); }

        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            if (trackBar3.Value == 1)
            {
                label15.BackColor = Color.Green;
                panel1.Enabled = true;
                panel4.Enabled = true;

            }
            else
            {
                label15.BackColor = Color.Salmon;
                panel1.Enabled = false;
                panel4.Enabled = false;
            }
            
        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }
        




        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
            {
                this.Font = new Font("Times New Roman", 24, FontStyle.Regular, GraphicsUnit.Pixel);
            }
            
        }
        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}