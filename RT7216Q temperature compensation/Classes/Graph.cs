using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace RT7216Q_temperature_compensation.Classes
{
    public partial class Graph : UserControl
    {
        public CIE_Point
            D38 = new CIE_Point() { x = 0.3898, y = 0.3821, lm = 1 },
            D50 = new CIE_Point() { x = 0.34567, y = 0.35850, lm = 1 },
            D55 = new CIE_Point() { x = 0.33242, y = 0.34743, lm = 1 },
            D65 = new CIE_Point() { x = 0.3127, y = 0.329, lm = 1 },
            D75 = new CIE_Point() { x = 0.29902, y = 0.31485, lm = 1 },
            D93 = new CIE_Point() { x = 0.28315, y = 0.29711, lm = 1 },
            E   = new CIE_Point() { x = 0.33333, y = 0.33333, lm = 1 }
            ;

        static double GAMMA_REC709 = 0;
        static double GAMMA = 2.4;

        public Graph()
        {
            InitializeComponent();

            D65_MacAdam(3.0, 360);
            target = new CIE_Point() { x = 0.3127, y = 0.329 };
            test = new CIE_Point() { x = 0.315, y = 0.33 };
            draw_color_matching_function();

            tabControl_1.TabPages.RemoveAt(4);
            tabControl_1.TabPages.RemoveAt(3);
            tabControl_1.TabPages.RemoveAt(2);
            tabControl_1.TabPages.RemoveAt(1);

            //drawHorseShoe();
            //CIE_Point
            //    r = new CIE_Point() { x = 0.6895, y = 0.3063, lm = 2.86758 },
            //    g = new CIE_Point() { x = 0.1437, y = 0.7097, lm = 6.47695 },
            //    b = new CIE_Point() { x = 0.1515, y = 0.0306, lm = 0.351621 },
            //    w = new CIE_Point() { x = 0.3128, y = 0.3297, lm = 9.22897 };
            ////Console.WriteLine((r + g + b).ToString());
            ////Console.WriteLine(w.ToString());
            //var XYZ = xyYtoXYZ(w.x, w.y, w.lm);
            //var sRGB = XYZtosRGB(XYZ[0], XYZ[1], XYZ[2]);
            //Console.WriteLine("D65 to sRGB: {0} {1} {2}", sRGB[0], sRGB[1], sRGB[2]);

            //CIE_Point
            //    r = new CIE_Point() { x = 0.64000, y = 0.33000, lm = 21.267 },
            //    g = new CIE_Point() { x = 0.30000, y = 0.60000, lm = 71.515 },
            //    b = new CIE_Point() { x = 0.15000, y = 0.06000, lm = 7.217 },
            //    w = new CIE_Point() { x = 0.31273, y = 0.32902, lm = 100.000 };

            //Console.WriteLine("CIE_xyY:\t"+r.ToString());
            //Console.WriteLine("XYZ:\t" + r.toXYZ().ToString());
            //Console.WriteLine("sRGB:\t{0} {1} {2}", r.toXYZ().tosRGB()[0], r.toXYZ().tosRGB()[1], r.toXYZ().tosRGB()[2]);

            //Console.WriteLine("CIE_xyY:\t" + g.ToString());
            //Console.WriteLine("XYZ:\t" + g.toXYZ().ToString());
            //Console.WriteLine("sRGB:\t{0} {1} {2}", g.toXYZ().tosRGB()[0], g.toXYZ().tosRGB()[1], g.toXYZ().tosRGB()[2]);

            //Console.WriteLine("CIE_xyY:\t" + b.ToString());
            //Console.WriteLine("XYZ:\t" + b.toXYZ().ToString());
            //Console.WriteLine("sRGB:\t{0} {1} {2}", b.toXYZ().tosRGB()[0], b.toXYZ().tosRGB()[1], b.toXYZ().tosRGB()[2]);

            //Console.WriteLine("CIE_xyY:\t" + w.ToString());
            //Console.WriteLine("XYZ:\t" + w.toXYZ().ToString());
            //Console.WriteLine("sRGB:\t{0} {1} {2}", w.toXYZ().tosRGB()[0], w.toXYZ().tosRGB()[1], w.toXYZ().tosRGB()[2]);

            //CIE_Point rg = r + g, gb = g + b, rb = r + b;

            //Console.WriteLine("CIE_xyY:\t" + rg.ToString());
            //Console.WriteLine("XYZ:\t" + rg.toXYZ().ToString());
            //Console.WriteLine("sRGB:\t{0} {1} {2}", rg.toXYZ().tosRGB()[0], rg.toXYZ().tosRGB()[1], rg.toXYZ().tosRGB()[2]);

            //Console.WriteLine("CIE_xyY:\t" + gb.ToString());
            //Console.WriteLine("XYZ:\t" + gb.toXYZ().ToString());
            //Console.WriteLine("sgbB:\t{0} {1} {2}", gb.toXYZ().tosRGB()[0], gb.toXYZ().tosRGB()[1], gb.toXYZ().tosRGB()[2]);

            //Console.WriteLine("CIE_xyY:\t" + rb.ToString());
            //Console.WriteLine("XYZ:\t" + rb.toXYZ().ToString());
            //Console.WriteLine("sRGB:\t{0} {1} {2}", rb.toXYZ().tosRGB()[0], rb.toXYZ().tosRGB()[1], rb.toXYZ().tosRGB()[2]);

            //Spectrum m = new Spectrum() { data = new Dictionary<double, double>() };
            //XYZ R, G, B, W;
            //CIE_Point p_R, p_G, p_B, p_W;
            //int[] sRGB;

            //Console.WriteLine("R");
            //m.load(@"D:\Projects\RT7216\Autotemp\data\calculation\sweep\R_spectral_data.csv");
            //R = m.toXYZ();
            //p_R = R.toxyY();
            //sRGB = p_R.tosRGB();
            //Console.WriteLine("CIE_xyY:\t" + p_R.ToString());
            //Console.WriteLine("XYZ:\t" + R.ToString());
            //Console.WriteLine("sRGB:\t{0} {1} {2}", sRGB[0], sRGB[1], sRGB[2]);

            //Console.WriteLine("G");
            //m.load(@"D:\Projects\RT7216\Autotemp\data\calculation\sweep\G_spectral_data.csv");
            //G = m.toXYZ();
            //p_G = G.toxyY();
            //sRGB = p_G.tosRGB();
            //Console.WriteLine("CIE_xyY:\t" + p_G.ToString());
            //Console.WriteLine("XYZ:\t" + G.ToString());
            //Console.WriteLine("sRGB:\t{0} {1} {2}", sRGB[0], sRGB[1], sRGB[2]);


            //Console.WriteLine("B");
            //m.load(@"D:\Projects\RT7216\Autotemp\data\calculation\sweep\B_spectral_data.csv");
            //B = m.toXYZ();
            //p_B = B.toxyY();
            //sRGB = p_B.tosRGB();
            //Console.WriteLine("CIE_xyY:\t" + p_B.ToString());
            //Console.WriteLine("XYZ:\t" + B.ToString());
            //Console.WriteLine("sRGB:\t{0} {1} {2}", sRGB[0], sRGB[1], sRGB[2]);

            //Console.WriteLine("\r\n\r\nR+G+B");
            //p_W = p_R + p_G + p_B;
            //W = p_W.toXYZ();
            //sRGB = p_W.tosRGB();
            //Console.WriteLine("CIE_xyY:\t" + p_W.ToString());
            //Console.WriteLine("XYZ:\t" + W.ToString());
            //Console.WriteLine("sRGB:\t{0} {1} {2}", sRGB[0], sRGB[1], sRGB[2]);

            //Console.WriteLine("RGB");
            //m.load(@"D:\Projects\RT7216\Autotemp\data\calculation\sweep\RGB_spectral_data.csv");
            //W = m.toXYZ();
            //p_W = W.toxyY();
            //sRGB = p_W.tosRGB();
            //Console.WriteLine("CIE_xyY:\t" + p_W.ToString());
            //Console.WriteLine("XYZ:\t" + W.ToString());
            //Console.WriteLine("sRGB:\t{0} {1} {2}", sRGB[0], sRGB[1], sRGB[2]);

            //CIE_Point p = new CIE_Point() { x = 0.2702, y = 0.2156, lm = 11.9903 };
            //var XYZ = xyYtoXYZ(p.x, p.y, p.lm);


        }


        public CIE_Point target { 
            get { 
                return new CIE_Point() { 
                    x = chart1.Series[1].Points[0].XValue, 
                    y = chart1.Series[1].Points[0].YValues[0] }; 
            }
            set {
                chart1.Series[1].Points.Clear();
                chart1.Series[1].Points.AddXY(value.x, value.y);
                chart1.Series[1].Points[0].Label = "Target"; chart1.Series[1].Points[0].LabelForeColor = Color.Red;

            }
        }
        public CIE_Point test
        {
            get
            {
                return new CIE_Point()
                {
                    x = chart1.Series[2].Points[0].XValue,
                    y = chart1.Series[2].Points[0].YValues[0]
                };
            }
            set
            {
                chart1.Series[2].Points.Clear();
                chart1.Series[2].Points.AddXY(value.x, value.y);
                chart1.Series[2].Points[0].Label = "Test"; chart1.Series[2].Points[0].LabelForeColor = Color.DarkOrange;

            }
        }

        public void plot_CIE(int idx, CIE_Point p)
        {
            if (!((0 < idx) && (idx < 5))) { return; }

            string label = ""; Color c = new Color();
            switch (idx)
            {
                case 0: break;
                case 1: label = "Target" ; c = Color.Red;break;
                case 2: label = "Test" ; c = Color.DarkOrange;break;
                case 3: label = "W1"; c = Color.Black;break;
                case 4: label = "W2"; c = Color.Black; break;
                default: break;
            }

            chart1.Series[idx].Points.Clear();
            chart1.Series[idx].Points.AddXY(p.x, p.y);
            chart1.Series[idx].Points[0].Label = label;
            chart1.Series[idx].Points[0].LabelForeColor = c;
        }
        

        public void D38_MacAdam(double step, int points)
        {

            // a,b,theta obtained from OSRAM colorCalculator 
            double a = 0.003230244873, b = 0.001268151683, theta = 59.778 * Math.PI / 180.000;
            double angle, x, y;

            chart1.Series[0].Points.Clear();

            for (int i = 0; i < points; i++)
            {
                angle = ((double)i) / ((double)points) * 360.0 * Math.PI / 180.000;
                x = D38.x + step * a * Math.Cos(angle) * Math.Cos(theta) - step * b * Math.Sin(angle) * Math.Sin(theta);
                y = D38.y + step * a * Math.Cos(angle) * Math.Sin(theta) + step * b * Math.Sin(angle) * Math.Cos(theta);

                chart1.Series[0].Points.AddXY(x, y);
            }
            //var t = (TextAnnotation)chart1.Annotations[0];
            //t.Text = "D65";
            chart1.Series[0].Points[0].Label = String.Format("D38 {0} step", step);
            Layout_CIExy(D38, 3.0);
        }

        public void D65_MacAdam(double step, int points)
        {
            // a,b,theta obtained from OSRAM colorCalculator 
            double a = 0.003297588957, b = 0.001131450779, theta = 64.898 * Math.PI / 180.000; 
            double angle,x,y;

            chart1.Series[0].Points.Clear();

            for (int i = 0; i < points; i++)
            {
                angle = ((double) i)/((double)points) * 360.0  * Math.PI / 180.000;
                x = D65.x + step * a * Math.Cos(angle) * Math.Cos(theta) - step * b * Math.Sin(angle) * Math.Sin(theta);
                y = D65.y + step * a * Math.Cos(angle) * Math.Sin(theta) + step * b * Math.Sin(angle) * Math.Cos(theta);

                chart1.Series[0].Points.AddXY(x,y);
            }
            //var t = (TextAnnotation)chart1.Annotations[0];
            //t.Text = "D65";
            chart1.Series[0].Points[0].Label = String.Format("D65 {0} step",step);
            Layout_CIExy(D65, 3.0);
        }

        public double distance_to_D65(CIE_Point p)
        {
            double a = 0.003297588957, b = 0.001131450779, theta = 64.898 * Math.PI / 180.000;
            double angle, x, y,d, c_x, c_y;

            //x = D65.x + d * a * Math.Cos(angle) * Math.Cos(theta) - d * b * Math.Sin(angle) * Math.Sin(theta);
            //y = D65.y + d * a * Math.Cos(angle) * Math.Sin(theta) + d * b * Math.Sin(angle) * Math.Cos(theta);

            return a;
        }

        public void Layout_CIExy(CIE_Point centre, double stretch)
        {
            double d = 0.005;
            Axis x = chart1.ChartAreas[0].AxisX;
            Axis y = chart1.ChartAreas[0].AxisY;

            x.Minimum = Math.Round(centre.x - stretch * d, 3);
            x.Maximum = x.Minimum + stretch * 2 * d;
            x.MajorGrid.Interval = 0.01;

            y.Minimum = Math.Round(centre.y - stretch * d, 3);
            y.Maximum = y.Minimum + stretch * 2 * d;
            y.MajorGrid.Interval = 0.01;


	       

        }
        public void Layout_PWM_temperature() { }


        public void plot_brightness_T(double[][] Lm, int[] Temperature)
        {
            int len = Lm.Length;
            if(len != Temperature.Length){return;}

            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();
            chart2.Series[2].Points.Clear();
            chart2.Series[3].Points.Clear();

            for(int i = 0 ; i < len; i++){
                chart2.Series[0].Points.AddXY(Temperature[i],Lm[i][0]);
                chart2.Series[1].Points.AddXY(Temperature[i],Lm[i][1]);
                chart2.Series[2].Points.AddXY(Temperature[i],Lm[i][2]);
                //chart2.Series[3].Points.AddXY(Temperature[i], Lm[i][0] + Lm[i][1] + Lm[i][2]);
            }
            
        }

        public void Layout_Brightness_Temperature() {}


        public void drawHorseShoe()
        {
            int count = 0;
            int[] rgb;

            chart4.ChartAreas[0].AxisX.Minimum = 0; chart4.ChartAreas[0].AxisX.Maximum = 1;
            chart4.ChartAreas[0].AxisY.Minimum = 0; chart4.ChartAreas[0].AxisY.Maximum = 1;

            var series = chart1.Series[5];
            series = chart4.Series[0];

            for (double x = 0; x < 1.01; x += 0.005)
            {
                for (double y = 0; y < 1.01; y += 0.005)
                {
                    series.Points.AddXY(x, y);
                    rgb = rgbData(x, y, 1 - x - y);
                    if ((x == 0.33) && (y == 0.33)) { series.Points[count].Label = "E"; }
                    series.Points[count++].Color = Color.FromArgb(rgb[0], rgb[1], rgb[2]);

                }
            }

            //100*100 points , [0  ,  1] [0  ,  1] = 4
            //                 [.3 , .4] [.3 , .4] = 40
            series.MarkerSize = 1;

        }

        private bool isValid(double[] rgb)
        {
            int R = (int)Math.Floor(255 * rgb[0]),
                G = (int)Math.Floor(255 * rgb[1]),
                B = (int)Math.Floor(255 * rgb[2]);

            if ((R < 0) || (255 < R)) { return false; }
            if ((G < 0) || (255 < G)) { return false; }
            if ((B < 0) || (255 < B)) { return false; }
            return true;
        }

        double limit(double value, double max, double min = 0)
        {
            return (min < value) ? (value <= max) ? value : max : min;
        }


        private int[] rgbData(params double [] xyz)
        {
            int[] rgb = new Int32[3];
            int r, g, b;
            double R, G, B;


            double[] sRGB = XYZtoRGB(xyz[0], xyz[1], xyz[2]);

            R = limit(sRGB[0], 1.0);
            G = limit(sRGB[1], 1.0);
            B = limit(sRGB[2], 1.0);

            double maxRGB = Math.Max( Math.Max(R,G),B);

            r = (Int32)Math.Round((255 * R / maxRGB));
            g = (Int32)Math.Round((255 * G / maxRGB));
            b = (Int32)Math.Round((255 * B / maxRGB));

            rgb[0] = r;
            rgb[1] = g;
            rgb[2] = b;

            return rgb;
        }

        private double[] XYZtoRGB(double xc, double yc, double zc)
        {
            var xr = NTSCsystem.xRed;
            var yr = NTSCsystem.yRed;
            var zr = 1.0 - xr - yr;
            var xg = NTSCsystem.xGreen;
            var yg = NTSCsystem.yGreen;
            var zg = 1.0 - xg - yg;
            var xb = NTSCsystem.xBlue;
            var yb = NTSCsystem.yBlue;
            var zb = 1.0 - xb - yb;

            var d = xr * yg * zb - xg * yr * zb - xr * yb * zg + xb * yr * zg + xg * yb * zr - xb * yg * zr;

            var R = (-xg * yc * zb + xc * yg * zb + xg * yb * zc - xb * yg * zc - xc * yb * zg + xb * yc * zg) / d;
            var G = (xr * yc * zb - xc * yr * zb - xr * yb * zc + xb * yr * zc + xc * yb * zr - xb * yc * zr) / d;
            var B = (xr * yg * zc - xg * yr * zc - xr * yc * zg + xc * yr * zg + xg * yc * zr - xc * yg * zr) / d;

            return new[] { R, G, B };
        }

        // https://en.wikipedia.org/wiki/SRGB#From_sRGB_to_CIE_XYZ
        public XYZ sRGBtoXYZ(int r, int g, int b)
        {
            double X, Y, Z;
            float R = ((float)r) / 255f
                , G = ((float)g) / 255f
                , B = ((float)b) / 255f;

            R = (R <= 0.04045) ? R / 12.92f : (float)Math.Pow((R + 0.055) / 1.055, GAMMA);
            G = (G <= 0.04045) ? G / 12.92f : (float)Math.Pow((G + 0.055) / 1.055, GAMMA);
            B = (B <= 0.04045) ? B / 12.92f : (float)Math.Pow((B + 0.055) / 1.055, GAMMA);


            X = 0.4124 * R + 0.3576 * G + 0.1805 * B;
            Y = 0.2126 * R + 0.7152 * G + 0.0722 * B;
            Z = 0.0193 * R + 0.1192 * G + 0.9505 * B;

            return new XYZ(X,Y,Z);
        }

      



      

        //public double[] XYZ_2_

        public void draw_color_matching_function()
        {
            for (double wavelength = 380; wavelength < 780.1; wavelength++)
            {
                chart_Spectrum.Series[0].Points.AddXY(wavelength, Spectrum.x_bar(wavelength));
                chart_Spectrum.Series[1].Points.AddXY(wavelength, Spectrum.y_bar(wavelength));
                chart_Spectrum.Series[2].Points.AddXY(wavelength, Spectrum.z_bar(wavelength));
            }
        }

        colourSystem
            NTSCsystem = new colourSystem(0.67, 0.33, 0.21, 0.71, 0.14, 0.08, new CIE_Point(0.3101, 0.3162)),
            EBU_PAM_SECAM = new colourSystem(0.64,0.33,0.29,0.60,0.15,0.06,new CIE_Point(0.3127,0.3291)),
            SMPTE = new colourSystem(0.63,0.34,0.31,0.595,0.155,0.07, new CIE_Point(0.3127,0.3291)),
            HDTV = new colourSystem(0.67,0.33,0.21,0.71,0.15,0.06, new CIE_Point(0.3127,0.3291)),
            CIE = new colourSystem(0.7355,0.2645,0.2658,0.7243,0.1669,0.0085, new CIE_Point(0.33333333333,0.333333333333)),
            CIE_REC_709 = new colourSystem(0.64,0.33,0.3,0.6,0.15,0.06, new CIE_Point(0.3127,0.3291));


        int limit(int value, int max, int min = 0) { return (min < value) ? (value < max) ? value : max : min; }
    }



    public struct colourSystem{
        public double xRed, xGreen, xBlue, xWhite, yRed, yGreen, yBlue, yWhite;
        public colourSystem(double Rx, double Ry, double Gx, double Gy, double Bx, double By, CIE_Point p)
        {
            xRed = Rx; xGreen = Gx; xBlue = Bx; xWhite = p.x;
            yRed = Ry; yGreen = Gy; yBlue = By; yWhite = p.y;
        }
    }

    public struct CIE_Point{
        public double x, y, lm;
        public CIE_Point(double X, double Y, double Lm = 1){ x = X; y = Y; lm = Lm;  }

        public static CIE_Point operator +(CIE_Point a, CIE_Point b){
            double x,y,lm;
            x = (a.x * a.lm /a.y + b.x * b.lm / b.y) / (a.lm / a.y + b.lm / b.y);
            y = (a.lm +  b.lm ) / (a.lm / a.y + b.lm / b.y);
            lm = (a.lm +  b.lm );
            return new CIE_Point(x,y,lm);
        }
        public override string ToString()
        {
            return String.Format("({0:0.0000}, {1:0.0000}, {2:0.0000})", x, y, lm);
        }

        // https://en.wikipedia.org/wiki/Relative_luminance
        public XYZ toXYZ()
        {
            //if (xyY.Length != 3) { return null; }
            double X, Y, Z;
            Y = lm; //normalized Y = [0,1]
            X = (lm / y) * x;
            Z = (lm / y) * (1 - x - y);
            return new XYZ(X,Y,Z) { };
        }

        public int[] tosRGB()
        {
            return toXYZ().tosRGB();
        }

    }

    public struct Spectrum
    {
        public Dictionary<double, double> data;

        public double this[double wavelength]
        {
            get { if(data.Keys.Contains(wavelength)){return data[wavelength];} return 0;}
            set { if(data.Keys.Contains(wavelength)){data[wavelength] = value;} }
        }
        public Dictionary<double, double>.KeyCollection keys { get { return data.Keys; } }

        public void load(string path)
        {
            if (File.Exists(path))
            {
                var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                using (StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default))
                {
                    string line;
                    while (sr.Peek() >= 0)
                    {
                        line = sr.ReadLine();
                        //throw away title and empty lines
                        //if ( line == "[Data]" || line == string.Empty) { continue; }

                        string[] words = line.Split(',');
                        if (words.Length != 2) { continue; }

                        double key = double.Parse(words[0].Trim(' '));
                        double value = double.Parse(words[1].Trim(' '));

                        if (!data.ContainsKey(key)) { data.Add(key, value); }
                        else { data[key] = value; }
                    }
                    sr.Close();
                }
                fs.Close();
            }
        }

        // color matching function calculated from https://en.wikipedia.org/wiki/CIE_1931_color_space
        public static double x_bar(double wavelength) { return 1.056 * g(wavelength, 599.8, 0.0264, 0.0323) + 0.362 * g(wavelength, 442, 0.0624, 0.0374) - 0.065 * g(wavelength, 501.1, 0.049, 0.0382); }
        public static double y_bar(double wavelength) { return 0.821 * g(wavelength, 568.8, 0.0213, 0.0247) + 0.286 * g(wavelength, 530.9, 0.0613, 0.0322); }
        public static double z_bar(double wavelength) { return 1.217 * g(wavelength, 437, 0.0845, 0.0278) + 0.681 * g(wavelength, 459, 0.0385, 0.0725); }
        private static double g(double x, double u, double t1, double t2)
        {
            if (x < u) { return Math.Exp(-Math.Pow(t1, 2.0) * Math.Pow(x - u, 2.0) / 2); }
            else { return Math.Exp(-Math.Pow(t2, 2.0) * Math.Pow(x - u, 2.0) / 2); }
        }

        public XYZ toXYZ()
        {
            double X=0, Y=0, Z=0;
            var keys = this.keys.ToArray();
            double wavelength, delta;
            for (int i = 0; i < keys.Length - 1; i++)
            {
                wavelength = keys[i];
                delta = keys[i + 1] - keys[i];
                X += data[wavelength] * x_bar(wavelength) * delta ;
                Y += data[wavelength] * y_bar(wavelength) * delta ;
                Z += data[wavelength] * z_bar(wavelength) * delta ;
            }
            //foreach (var wavelength in keys)
            //{
            //    XYZ[0] += data[wavelength] * x_bar(wavelength);
            //    XYZ[1] += data[wavelength] * y_bar(wavelength);
            //    XYZ[2] += data[wavelength] * z_bar(wavelength);
            //}
            X /=  (double)(keys.Length);
            Y /=  (double)(keys.Length);
            Z /=  (double)(keys.Length);

            return new XYZ() { X=X,Y=Y,Z=Z};
        }
    }

    public struct XYZ
    {
        public double X, Y, Z;
        public static double Gamma = 2.4;
        public XYZ(double x, double y, double z) { X = x; Y = y; Z = z; }

        // https://en.wikipedia.org/wiki/SRGB#From_sRGB_to_CIE_XYZ
        public int[] tosRGB()
        {
            double x = X / 100, y = Y / 100, z = Z / 100;

            double R =  3.2406255 * x - 1.5372080 * y - 0.4986286 * z   //R_linear
                 , G = -0.9689307 * x + 1.8758561 * y + 0.0415175 * z   //G_linear
                 , B =  0.0557101 * x - 0.2040211 * y + 1.0569959 * z;  //B_linear

            R = (R <= 0.0031308) ? (12.92 * R) : (1.055 * Math.Pow(R, 1.0 / Gamma) - 0.055);
            G = (G <= 0.0031308) ? (12.92 * G) : (1.055 * Math.Pow(G, 1.0 / Gamma) - 0.055);
            B = (B <= 0.0031308) ? (12.92 * B) : (1.055 * Math.Pow(B, 1.0 / Gamma) - 0.055);

            //if ((1 < R) || (1 < G) || (1 < B)) { return new int[3] { (int)limit(R, 255), (int)limit(G, 255), (int)limit(B, 255) }; }

            return new int[3] { (int)Math.Round(limit(255 * R, 255)), (int)Math.Round(limit(255 * G, 255)), (int)Math.Round(limit(255 * B, 255)) };
        }

        public CIE_Point toxyY()
        {
            double SUM = X + Y + Z;
            return new CIE_Point( X / SUM, Y / SUM, Y );
        }

        public override string ToString() { return string.Format("{0} {1} {2}", X, Y, Z); }

        double limit(double value, double max, double min = 0) {return (min < value) ? (value <= max) ? value : max : min; }
    }
}



/*
    from my understanding:
 * 
 * light is composed of different wavelengths of varying amplitude
 *      Spectral analysis => obtains data (amplitude vs wavelength)
 *      integration => [ data(lamda) . color_matching_function(lamda) ] obtains XYZ
 *      XYZtoxyY => obtains CIE1931_xyY
 * 
 * 
 
 
 */

/*
    double[] CIExyY_to_XYZ(double x, double y, double Lm)
        {
            double X, Y, Z;
            X= x * (Lm/y);
            Y= Lm;
            Z= (1-x-y)/(Lm/y);
            return new double[3]{X,Y,Z};
        }

        double[] XYZ_to_CIExyY(double X, double Y, double Z)
        {
            double x, y, Lm;
            x= X/(X+Y+Z);
            y=Y/(X+Y+Z);
            Lm=Y;
            return new double[3]{x,y,Lm};
        }

        double[] XYZ_to_sRGB(double X, double Y, double Z)
        {
            //X, Y and Z input refer to a D65/2° standard illuminant.
            //sR, sG and sB (standard RGB) output range = (0~255) ÷ 255

            double r, g, b; // 
            double x = X / 100;
            double y = Y / 100;
            double z = Z / 100;

            r = x *  3.2406 + y * -1.5372 + z * -0.4986;
            g = x * -0.9689 + y *  1.8758 + z *  0.0415;
            b = x *  0.0557 + y * -0.2040 + z *  1.0570;
            
            if ( r > 0.0031308 ){ r = 1.055 * Math.Pow(r,( 1.0 / 2.4 ) ) - 0.055;}
            else                     {r = 12.92 * r;}
            if ( g > 0.0031308 ) {g = 1.055 *  Math.Pow(g,( 1.0 / 2.4 ) ) - 0.055;}
            else                     {g = 12.92 * g;}
            if ( b > 0.0031308 ) {b = 1.055 *  Math.Pow(b,( 1.0 / 2.4 ) ) - 0.055;}
            else                    { b = 12.92 * b;}

            return new double[3] { r, g, b };
        }

        double[] sRGB_to_XYZ(int R, int G, int B)
        {
            //sR, sG and sB (Standard RGB) input range = (0~255) ÷ 255
            //X, Y and Z output refer to a D65/2° standard illuminant.
            double X, Y, Z;
            double sR= ( (double)R / 255.0 ) ;
            double sG= ( (double)G / 255.0 ) ;
            double sB= ( (double)B / 255.0 ) ;

            if ( sR > 0.04045 ){ sR = Math.Pow(( sR + 0.055 ) / 1.055 , 2.4);}
            else                   {sR = sR / 12.92;}
            if ( sG > 0.04045 ) {sG = Math.Pow(( sG + 0.055 ) / 1.055 , 2.4);}
            else                   {sG = sG / 12.92;}
            if ( sB > 0.04045 ) {sB = Math.Pow(( sB + 0.055 ) / 1.055 , 2.4);}
            else                  { sB = sB / 12.92;}

            sR = sR * 100;
            sG = sG * 100;
            sB = sB * 100;

            X = sR * 0.4124 + sG * 0.3576 + sB * 0.1805;
            Y = sR * 0.2126 + sG * 0.7152 + sB * 0.0722;
            Z = sR * 0.0193 + sG * 0.1192 + sB * 0.9505;
            return new double[3] { X, Y, Z };
        }
 
 */