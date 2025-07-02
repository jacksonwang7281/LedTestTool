using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.IO;

namespace RT7216Q_temperature_compensation.Classes
{
    public partial class TemperatureCompensation : UserControl
    {
        public ISM360 ISM360;

        public event EventHandler button_test_Clicked;
        public event EventHandler button_CR_Clicked;
        public event EventHandler button_SCALAR_Clicked;
        public event EventHandler button_TC_linear_Clicked;

        public TemperatureCompensation()
        {
            InitializeComponent();

            ISM360 = new Classes.ISM360();



            //graph1.drawHorseShoe();
        }

        private void TemperatureCompensation_Load(object sender, EventArgs e)
        {
            dataGridView_CIE_points.Rows.Add("Target", "0.3127", "0.329", "7"); dataGridView_CIE_points[1, 0].ReadOnly = true; dataGridView_CIE_points[2, 0].ReadOnly = true;
            dataGridView_CIE_points.Rows.Add("Test", "0.315", "0.33", "2");
            isCieGridReady = true;

            dataGridView_CR.Rows.Add("R", "0", "0", "0");
            dataGridView_CR.Rows.Add("G", "0", "0", "0");
            dataGridView_CR.Rows.Add("B", "0", "0", "0");
            dataGridView_CR.Rows.Add("Calculate", "", "", ""); dataGridView_CR.Rows[3].ReadOnly = true;

            dataGridView_CR_result.Rows.Add("R", 0, 0);
            dataGridView_CR_result.Rows.Add("G",0, 0);
            dataGridView_CR_result.Rows.Add("B",0, 0);


            dataGridView_SCALAR.Rows.Add("R", "0", "0", "0");
            dataGridView_SCALAR.Rows.Add("G", "0", "0", "0");
            dataGridView_SCALAR.Rows.Add("B", "0", "0", "0");
            dataGridView_SCALAR.Rows.Add("Calculate", "", "", ""); dataGridView_SCALAR.Rows[3].ReadOnly = true;
            dataGridView_SCALAR.Rows.Add("W1", "0", "0", "0");
            dataGridView_SCALAR.Rows.Add("Calculate", "", "", ""); dataGridView_SCALAR.Rows[5].ReadOnly = true;
            dataGridView_SCALAR.Rows.Add("W2", "0", "0", "0");

            dataGridView_SCALAR_result.Rows.Add("R", "", "");
            dataGridView_SCALAR_result.Rows.Add("G", "", "");
            dataGridView_SCALAR_result.Rows.Add("B", "", "");


            dataGridView_linear_TC.Rows.Add("R (25°C)", "0", "0", "0");
            dataGridView_linear_TC.Rows.Add("G (25°C)", "0", "0", "0");
            dataGridView_linear_TC.Rows.Add("B (25°C)", "0", "0", "0");
            dataGridView_linear_TC.Rows.Add("R (37°C)", "0", "0", "0");
            dataGridView_linear_TC.Rows.Add("G (37°C)", "0", "0", "0");
            dataGridView_linear_TC.Rows.Add("B (37°C)", "0", "0", "0");
            dataGridView_linear_TC.Rows.Add("Calculate", "", "", ""); dataGridView_SCALAR.Rows[6].ReadOnly = true;

            dataGridView_linear_TC_result.Rows.Add("R_base","");
            dataGridView_linear_TC_result.Rows.Add("R_value", "");
            dataGridView_linear_TC_result.Rows.Add("G_base", "");
            dataGridView_linear_TC_result.Rows.Add("G_value", "");
            dataGridView_linear_TC_result.Rows.Add("B_base", "");
            dataGridView_linear_TC_result.Rows.Add("B_value", "");

            tabControl_TC.TabPages.RemoveAt(4);
            tabControl_TC.TabPages.RemoveAt(3);
            //tabControl_TC.TabPages.RemoveAt(2);
            //tabControl_TC.TabPages.RemoveAt(1);
        }




        private void tabControl_TC_DrawItem(object sender, DrawItemEventArgs e)
        {

            var g = e.Graphics;
            var text = this.tabControl_TC.TabPages[e.Index].Text;
            var sizeText = g.MeasureString(text, this.tabControl_TC.Font);

            var x = e.Bounds.Left + 3;
            var y = e.Bounds.Top + (e.Bounds.Height - sizeText.Height) / 2;

            g.DrawString(text, this.tabControl_TC.Font, Brushes.Black, x, y);

        }


        // CIE data manipulation
        double lastCIE;
        bool isCieGridReady = false;
        bool shouldHandle(string name, int col, int row)
        {
            if ((col < 1) || (3 < col)) { return false; }


            switch (name)
            {
                case "dataGridView_CR": if ((row < 0) || (2 < row)) { return false; } break;
                case "dataGridView_SCALAR": if ((row == 3) || (row == 5)) { return false; } break;
                case "dataGridView_linear_TC": if (row == 6) { return false; } break;
                default: break;
            }
            return true;
        }
        private void dataGridView_CIE_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex != 0)
            {  
                var grid = sender as DataGridView;
                var cells = grid.SelectedCells;
                if (cells.Count == 0) { return; }

                if (!shouldHandle(grid.Name, e.ColumnIndex, e.RowIndex)) { return; }

                lastCIE = double.Parse(cells[0].Value.ToString());
            }
        }
        private void dataGridView_CIE_points_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex == 3) || (e.RowIndex == 5)) { return; }
            if (e.ColumnIndex == 0) { return; }

            if (!isCieGridReady) { return; }

           


            var grid = sender as DataGridView;
            var cells = grid.SelectedCells;
            double value;

            if (cells[0].ColumnIndex == 0) { return; }

            isCieGridReady = false;

            //discard change if string does not parse as double
            if (!Double.TryParse((string)cells[0].Value, out value))
            {
                cells[0].Value = lastCIE.ToString();
                isCieGridReady = true;
                return;
            }

            if (((e.ColumnIndex == 1) || (e.ColumnIndex == 2)) && ((value < 0) || (1 < value)))
            {
                cells[0].Value = lastCIE.ToString();
                isCieGridReady = true;
                return;
            }

            if ((e.ColumnIndex == 3) && (value < 0))
            {
                cells[0].Value = lastCIE.ToString();
                isCieGridReady = true;
                return;
            }

            for (int i = 0; i < cells.Count; i++)
            {
                int row = cells[i].RowIndex, col = cells[i].ColumnIndex;
                grid.Rows[row].Cells[col].Value = value;

                if ((col == 1) || (col == 2))
                {
                    double x, y;
                    x = double.Parse(grid[1, row].Value.ToString());
                    y = double.Parse(grid[2, row].Value.ToString());
                    Classes.CIE_Point point = new Classes.CIE_Point() { x = x, y = y };

                    if (grid == dataGridView_CIE_points)
                    {
                        switch (row)
                        {
                            case 0: graph1.target = point; break;
                            case 1: graph1.test = point; break;
                            default: break;
                        }
                    }
                    if (grid == dataGridView_SCALAR)
                    {
                        switch (row)
                        {
                            case 4: graph1.plot_CIE(3, point); break;
                            case 6: graph1.plot_CIE(4, point); break;
                            default: break;
                        }
                    }

                }

            }
            isCieGridReady = true;

        }
        private void dataGridView_button_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                //TODO - invoke external handler
                if(senderGrid == dataGridView_CR){button_CR_Clicked(sender, e);}
                if(senderGrid == dataGridView_SCALAR) { button_SCALAR_Clicked(sender, e); }
                if (senderGrid == dataGridView_linear_TC) { button_TC_linear_Clicked(sender, e); }
            }
            timer.Stop();
            Console.WriteLine("time elapsed {0}", timer.Elapsed);
        }
        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {


            if ((e.Control) && (e.KeyCode == Keys.V))
            {

                var grid = sender as DataGridView;
                var cell = grid.SelectedCells[0];
                int x = cell.ColumnIndex, y = cell.RowIndex;



                if (!shouldHandle(grid.Name, x, y)) { return; }

                isCieGridReady = false;

                var text = Clipboard.GetText().Replace("\r", "");


                String[] lines = text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                String[][] data = new String[lines.Length][];

                for (int i = 0; i < lines.Length; i++)
                {
                    data[i] = lines[i].Split('\t');
                }



                switch (grid.Name)
                {
                    case "dataGridView_CR":
                        for (int i = 0; i < data.Length; i++)
                        {
                            if (2 < (y + i)) { break; }
                            for (int j = 0; j < data[i].Length; j++)
                            {
                                if (3 < (x + j)) { break; }
                                grid[x + j, y + i].Value = data[i][j];
                            }
                        }
                        break;
                    case "dataGridView_SCALAR":
                        bool W1 = false, W2 = false;
                        for (int i = 0; i < data.Length; i++)
                        {
                            int r = y + i;
                            if (r == 4) { W1 = true; }
                            if (r == 6) { W2 = true; }
                            if ((r == 3) || (r == 5) || (6 < r)) { continue; }
                            for (int j = 0; j < data[i].Length; j++)
                            {
                                if (3 < (x + j)) { break; }
                                grid[x + j, y + i].Value = data[i][j];
                            }
                        }
                        if (W1)
                        {
                            CIE_Point p = new CIE_Point();
                            p.x = double.Parse(dataGridView_SCALAR[1, 4].Value.ToString());
                            p.y = double.Parse(dataGridView_SCALAR[2, 4].Value.ToString());
                            graph1.plot_CIE(3, p);
                        }
                        if (W2)
                        {
                            CIE_Point p = new CIE_Point();
                            p.x = double.Parse(dataGridView_SCALAR[1, 6].Value.ToString());
                            p.y = double.Parse(dataGridView_SCALAR[2, 6].Value.ToString());
                            graph1.plot_CIE(4, p);
                        }
                        break;
                    case "dataGridView_linear_TC":
                        for (int i = 0; i < data.Length; i++)
                        {
                            int r = y + i;
                            for (int j = 0; j < data[i].Length; j++)
                            {
                                if (5 < (x + j)) { break; }
                                grid[x + j, y + i].Value = data[i][j];
                            }
                        }


                        break;
                    default: break;
                }
                isCieGridReady = true;
                //Console.WriteLine( String.Format("pasted into (x,y) = ({0},{1}) of {2}!\r\n {3}", cell.ColumnIndex, cell.RowIndex, grid.Name, text));
            }
        }

        private void dataGridView_CIE_points_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                //TODO - invoke external handler
                if (senderGrid == dataGridView_CIE_points) { button_test_Clicked(sender, e); }

            }
        }

        //double CR_step;
        //double percentage;
        //double max_Current;
        public int[] calculate_CR(int max_CR, float CR_step , int max_Current, int measuring_CR)
        {
            double target_x, target_y, target_lm, X, Y, Z, a, b, c, d, e, f, delta, Iv1, Iv2, Iv3, Iv1_c, Iv2_c, Iv3_c, x_R, x_G, x_B, y_R, y_G, y_B, lm_R, lm_G, lm_B, mA60_Lm_R, mA60_Lm_G, mA60_Lm_B;


            target_x = double.Parse(dataGridView_CIE_points[1, 0].Value.ToString());
            target_y = double.Parse(dataGridView_CIE_points[2, 0].Value.ToString());
            target_lm = double.Parse(dataGridView_CIE_points[3, 0].Value.ToString());

            X = target_lm * target_x / target_y;
            Y = target_lm;
            Z = target_lm * (1.00 - target_x - target_y) / target_y;

            //if ((dataGridView_CR[1, 0].Value == "NaN") || (dataGridView_CR[2, 0].Value == "")) { MessageBox.Show("CIE data is not accepted"); return otp; }
            //if ((textBox_CR_G_CIEx.Text == "NaN") || (textBox_CR_G_CIEy.Text == "")) { MessageBox.Show("CIE data is not accepted");return otp; }
            //if ((textBox_CR_B_CIEx.Text == "NaN") || (textBox_CR_B_CIEy.Text == "")) { MessageBox.Show("CIE data is not accepted"); return otp; }


            x_R = double.Parse(dataGridView_CR[1,0].Value.ToString()); y_R = double.Parse(dataGridView_CR[2,0].Value.ToString()); lm_R = double.Parse(dataGridView_CR[3,0].Value.ToString());
            x_G = double.Parse(dataGridView_CR[1,1].Value.ToString()); y_G = double.Parse(dataGridView_CR[2,1].Value.ToString()); lm_G = double.Parse(dataGridView_CR[3,1].Value.ToString());
            x_B = double.Parse(dataGridView_CR[1,2].Value.ToString()); y_B = double.Parse(dataGridView_CR[2,2].Value.ToString()); lm_B = double.Parse(dataGridView_CR[3,2].Value.ToString());

            a = x_R / y_R;
            b = x_G / y_G;
            c = x_B / y_B;

            d = (1.00 - x_R - y_R) / y_R;
            e = (1.00 - x_G - y_G) / y_G;
            f = (1.00 - x_B - y_B) / y_B;

            delta = a * f + c * e + b * d - c * d - b * f - a * e;

            Iv1 = f * X + c * e * Y + Z * b - c * Z - b * f * Y - e * X;
            Iv2 = a * f * Y + d * X + c * Z - c * d * Y - X * f - a * Z;
            Iv3 = a * Z + e * X + b * d * Y - d * X - b * Z - a * e * Y;

            Iv1_c = Iv1 / delta;
            Iv2_c = Iv2 / delta;
            Iv3_c = Iv3 / delta;

            mA60_Lm_R = lm_R / (30.0) * max_Current ;
            mA60_Lm_G = lm_G / (30.0) * max_Current ;
            mA60_Lm_B = lm_B / (30.0) * max_Current ;

            double Iv1_ratio, Iv2_ratio, Iv3_ratio;
            Iv1_ratio = Iv1_c / mA60_Lm_R ;
            Iv2_ratio = Iv2_c / (mA60_Lm_G * 384.0/256.0);
            Iv3_ratio = Iv3_c / mA60_Lm_B ;

            int CR_R = (int)(Math.Round((((float)max_CR * CR_step) + Iv1_ratio - 1) / CR_step, 0));
            int CR_G = (int)(Math.Round((((float)max_CR * CR_step) + Iv2_ratio - 1) / CR_step, 0));
            int CR_B = (int)(Math.Round((((float)max_CR * CR_step) + Iv3_ratio - 1) / CR_step, 0));

            CR_R = limit(CR_R,0xFF); //(CR_R > 0xFF) ? 0xFF : CR_R;
            CR_G = limit(CR_G,0xFF); //(CR_G > 0xFF) ? 0xFF : CR_G;
            CR_B = limit(CR_B,0xFF); //(CR_B > 0xFF) ? 0xFF : CR_B;

            double max_R_mA = Math.Round(max_Current * (1.00 - ((float)max_CR - CR_R) * CR_step), 1); ;
            double max_G_mA = Math.Round(max_Current * (1.00 - ((float)max_CR - CR_G) * CR_step), 1); ;
            double max_B_mA = Math.Round(max_Current * (1.00 - ((float)max_CR - CR_B) * CR_step), 1); ;
            dataGridView_CR_result[1, 0].Value = CR_R; dataGridView_CR_result[2, 0].Value = max_R_mA;
            dataGridView_CR_result[1, 1].Value = CR_G; dataGridView_CR_result[2, 1].Value = max_G_mA;
            dataGridView_CR_result[1, 2].Value = CR_B; dataGridView_CR_result[2, 2].Value = max_B_mA;

            dataGridView_CR_result[3, 0].Value = Math.Round(max_R_mA / 3.0,1);
            dataGridView_CR_result[3, 1].Value = Math.Round(max_G_mA / 3.0,1);
            dataGridView_CR_result[3, 2].Value = Math.Round(max_B_mA / 3.0,1);

            return new int[3]{CR_R,CR_G,CR_B};
        }     
        public RT7216N_BD CR_settings(string color)
        {
            double CR_step = 0.00337f;
            double percentage = (1.00 - (0xFF - 0x6B) * CR_step);
            double max_Current = 64.00;

            RT7216N_BD IC = new RT7216N_BD() {
                primary_reg = new RT7216N_BD.PRIMARY_REGISTER() { DET = 0, OFF_WDG = 1, OFF_SLP = 1, tc_r_en = 0, IOHL = 2, READ_SPD = 3 },
                misc = new RT7216N_BD.MISC() {tc_g_en = 0, tc_b_en = 0, enable = new bool[3]{true,true,false} },
                otp_reg = new RT7216N_BD.OTP() { max_PWM_chip = 0x2FD, CR_R = 0x6B, CR_G = 0x6B, CR_B = 0x6B, PWM_scalar_R = 0x100, PWM_scalar_G = 0x100, PWM_scalar_B = 0x100}
            };
            IC.otp_reg.en[0] = IC.otp_reg.en[1] = true;
            IC.otp_reg.en[2] = IC.otp_reg.en[3] = IC.otp_reg.en[4] = IC.otp_reg.en[5] = IC.otp_reg.en[6] = IC.otp_reg.en[7] = true;
            IC.otp_reg.en[8] = IC.otp_reg.en[9] = IC.otp_reg.en[10] = true;
            IC.otp_reg.en[13] = IC.otp_reg.en[14] = false;

            int pwm = IC.otp_reg.max_PWM_chip/3;

            switch (color.ToLower())
            {
                case "r": IC.pwm.set(pwm, 0, 0); break;
                case "g": IC.pwm.set(0, pwm, 0); break;
                case "b": IC.pwm.set(0, 0, pwm); break;
            }

            dataGridView_CR_result[1, 0].Value = IC.otp_reg.CR_R; dataGridView_CR_result[2, 0].Value = max_Current * (1.00 - (0xFF - IC.otp_reg.CR_R) * CR_step);
            dataGridView_CR_result[1, 1].Value = IC.otp_reg.CR_R; dataGridView_CR_result[2, 1].Value = max_Current * (1.00 - (0xFF - IC.otp_reg.CR_G) * CR_step);
            dataGridView_CR_result[1, 2].Value = IC.otp_reg.CR_R; dataGridView_CR_result[2, 2].Value = max_Current * (1.00 - (0xFF - IC.otp_reg.CR_B) * CR_step);


            return IC;
        }

        public int[] calculate_SCALAR( bool includeOffset)
        {
            double target_x, target_y, target_lm, 
                X, Y, Z, 
                a, b, c, d, e, f, delta, 
                Iv1, Iv2, Iv3, Iv1_c, Iv2_c, Iv3_c, 
                R_x, G_x, B_x, 
                R_y, G_y, B_y, 
                R_lm, G_lm, B_lm, 
                offset_x, offset_y, offset_lm;

            target_x  = includeOffset ? double.Parse(dataGridView_SCALAR[1, 4].Value.ToString()) : double.Parse(dataGridView_CIE_points[1,0].Value.ToString());
            target_y  = includeOffset ? double.Parse(dataGridView_SCALAR[2, 4].Value.ToString()) : double.Parse(dataGridView_CIE_points[2, 0].Value.ToString());
            target_lm = includeOffset ? double.Parse(dataGridView_SCALAR[3, 4].Value.ToString()) : double.Parse(dataGridView_CIE_points[3, 0].Value.ToString()); 

            offset_x =  double.Parse(dataGridView_CIE_points[1, 0].Value.ToString()) - target_x;
            offset_y =  double.Parse(dataGridView_CIE_points[2, 0].Value.ToString()) - target_y;
            offset_lm = double.Parse(dataGridView_CIE_points[3, 0].Value.ToString()) - target_lm;

            target_x =  double.Parse(dataGridView_CIE_points[1, 0].Value.ToString()) + offset_x;
            target_y =  double.Parse(dataGridView_CIE_points[2, 0].Value.ToString()) + offset_y;
            target_lm = double.Parse(dataGridView_CIE_points[3, 0].Value.ToString()) + offset_lm;

            X = target_lm * target_x / target_y;
            Y = target_lm;
            Z = target_lm * (1.00 - target_x - target_y) / target_y;

            //if ((textBox_SCALAR_R_CIEx.Text == "NaN") || (textBox_SCALAR_R_CIEx.Text == "")) { return 1; }
            //if ((textBox_SCALAR_G_CIEx.Text == "NaN") || (textBox_SCALAR_R_CIEx.Text == "")) { return 1; }
            //if ((textBox_SCALAR_B_CIEx.Text == "NaN") || (textBox_SCALAR_R_CIEx.Text == "")) { return 1; }

            R_x = double.Parse(dataGridView_SCALAR[1, 0].Value.ToString()); R_y = double.Parse(dataGridView_SCALAR[2, 0].Value.ToString()); R_lm = double.Parse(dataGridView_SCALAR[3, 0].Value.ToString());
            G_x = double.Parse(dataGridView_SCALAR[1, 1].Value.ToString()); G_y = double.Parse(dataGridView_SCALAR[2, 1].Value.ToString()); G_lm = double.Parse(dataGridView_SCALAR[3, 1].Value.ToString());
            B_x = double.Parse(dataGridView_SCALAR[1, 2].Value.ToString()); B_y = double.Parse(dataGridView_SCALAR[2, 2].Value.ToString()); B_lm = double.Parse(dataGridView_SCALAR[3, 2].Value.ToString());

            a = R_x / R_y;
            b = G_x / G_y;
            c = B_x / B_y;

            d = (1.00 - R_x - R_y) / R_y;
            e = (1.00 - G_x - G_y) / G_y;
            f = (1.00 - B_x - B_y) / B_y;

            delta = a * f + c * e + b * d - c * d - b * f - a * e;

            Iv1 = f * X + c * e * Y + Z * b - c * Z - b * f * Y - e * X;
            Iv2 = a * f * Y + d * X + c * Z - c * d * Y - X * f - a * Z;
            Iv3 = a * Z + e * X + b * d * Y - d * X - b * Z - a * e * Y;

            Iv1_c = Iv1 / delta;
            Iv2_c = Iv2 / delta;
            Iv3_c = Iv3 / delta;

            int SCALAR_R = (int)(Math.Round(Iv1_c / R_lm * 256.00, 0));
            int SCALAR_G = (int)(Math.Round(Iv2_c / G_lm * 256.00, 0));
            int SCALAR_B = (int)(Math.Round(Iv3_c / B_lm * 256.00, 0));

            //if ((SCALAR_R < 0) || (0xFFFF < SCALAR_R)) { return 1; }
            //if ((SCALAR_G < 0) || (0xFFFF < SCALAR_G)) { return 1; }
            //if ((SCALAR_B < 0) || (0xFFFF < SCALAR_B)) { return 1; }


            //double duty = ((double)((IC.pwm.R * SCALAR_R) / 256)) / ((double)IC.otp_reg.max_PWM_chip);
            double pwm = ((double) 0x2FD) / 3.0;

            double duty_R = Math.Round(100.0 * ((double)((pwm * SCALAR_R) / 256)) / ((double)0x2FD), 1);
            double duty_G = Math.Round(100.0 * ((double)((pwm * SCALAR_G) / 256)) / ((double)0x2FD), 1);
            double duty_B = Math.Round(100.0 * ((double)((pwm * SCALAR_B) / 256)) / ((double)0x2FD), 1);

            dataGridView_SCALAR_result[1, 0].Value = SCALAR_R; dataGridView_SCALAR_result[2, 0].Value = duty_R;
            dataGridView_SCALAR_result[1, 1].Value = SCALAR_G; dataGridView_SCALAR_result[2, 1].Value = duty_G;
            dataGridView_SCALAR_result[1, 2].Value = SCALAR_B; dataGridView_SCALAR_result[2, 2].Value = duty_B;

            

            dataGridView_SCALAR_result[3, 0].Value = duty_R * Double.Parse(dataGridView_CR_result[2, 0].Value.ToString())/100.0;
            dataGridView_SCALAR_result[3, 1].Value = duty_G * Double.Parse(dataGridView_CR_result[2, 1].Value.ToString())/100.0;
            dataGridView_SCALAR_result[3, 2].Value = duty_B * Double.Parse(dataGridView_CR_result[2, 2].Value.ToString())/100.0;

            return new int[3]{SCALAR_R, SCALAR_G, SCALAR_B};
        }
        public RT7216N_BD SCALAR_settings(int stage)
        {
            RT7216N_BD IC = new RT7216N_BD()
            {
                primary_reg = new RT7216N_BD.PRIMARY_REGISTER() { DET = 0, OFF_WDG = 1, OFF_SLP = 1, tc_r_en = 0, IOHL = 2, READ_SPD = 3 },
                misc = new RT7216N_BD.MISC() { tc_g_en = 0, tc_b_en = 0, enable = new bool[3] { true, true, false } },
                otp_reg = new RT7216N_BD.OTP() { max_PWM_chip = 0x2FD,  }
            };


            IC.otp_reg.en[0] = IC.otp_reg.en[1] = true;
            IC.otp_reg.en[2] = IC.otp_reg.en[3] = IC.otp_reg.en[4] = IC.otp_reg.en[5] = IC.otp_reg.en[6] = IC.otp_reg.en[7] = true;
            IC.otp_reg.en[8] = IC.otp_reg.en[9] = IC.otp_reg.en[10] = false;
            IC.otp_reg.en[13] = IC.otp_reg.en[14] = false;

            int pwm = IC.otp_reg.max_PWM_chip / 3;

            switch (stage)
            {
                case 0: IC.pwm.set(pwm, 0, 0);     IC.otp_reg.PWM_scalar_R = IC.otp_reg.PWM_scalar_G = IC.otp_reg.PWM_scalar_B = 0x100; break;
                case 1: IC.pwm.set(0, pwm, 0);     IC.otp_reg.PWM_scalar_R = IC.otp_reg.PWM_scalar_G = IC.otp_reg.PWM_scalar_B = 0x100; break;
                case 2: IC.pwm.set(0, 0, pwm);     IC.otp_reg.PWM_scalar_R = IC.otp_reg.PWM_scalar_G = IC.otp_reg.PWM_scalar_B = 0x100; break;
                case 3: IC.pwm.set(pwm, pwm, pwm);  break;
            }

            //double duty = ((double)( (IC.pwm.R * IC.otp_reg.PWM_scalar_R) / 256))  / ( (double) IC.otp_reg.max_PWM_chip);
            dataGridView_SCALAR_result[1, 0].Value = IC.otp_reg.PWM_scalar_R; dataGridView_SCALAR_result[2, 0].Value = Math.Round(100.0 * ((double)((IC.pwm.R * IC.otp_reg.PWM_scalar_R) / 256)) / ((double)IC.otp_reg.max_PWM_chip), 1);
            dataGridView_SCALAR_result[1, 1].Value = IC.otp_reg.PWM_scalar_G; dataGridView_SCALAR_result[2, 1].Value = Math.Round(100.0 * ((double)((IC.pwm.G * IC.otp_reg.PWM_scalar_G) / 256)) / ((double)IC.otp_reg.max_PWM_chip), 1);
            dataGridView_SCALAR_result[1, 2].Value = IC.otp_reg.PWM_scalar_B; dataGridView_SCALAR_result[2, 2].Value = Math.Round(100.0 * ((double)((IC.pwm.B * IC.otp_reg.PWM_scalar_B) / 256)) / ((double)IC.otp_reg.max_PWM_chip), 1);

            return IC;
        }

        //graph control
        public void Plot_decay()
        {
            int [] Temperature = new int[]{25, 37};
            double [][] Lm = new double[2][];
            for (int i = 0; i < 2; i++) {
                Lm[i] = new double[3];
            }
            Lm[0][0] = double.Parse(dataGridView_linear_TC[3, 0].Value.ToString());
            Lm[0][1] = double.Parse(dataGridView_linear_TC[3, 1].Value.ToString());
            Lm[0][2] = double.Parse(dataGridView_linear_TC[3, 2].Value.ToString());
            Lm[1][0] = double.Parse(dataGridView_linear_TC[3, 3].Value.ToString());
            Lm[1][1] = double.Parse(dataGridView_linear_TC[3, 4].Value.ToString());
            Lm[1][2] = double.Parse(dataGridView_linear_TC[3, 5].Value.ToString()); 

            //graph1.plot_brightness_T(Lm, Temperature);

            double Lm_R = Lm[0][0], Lm_G = Lm[0][1], Lm_B = Lm[0][2], diff_R, diff_G, diff_B;
            diff_R = (Lm[1][0] - Lm[0][0]) / (Temperature[1] - Temperature[0]);
            diff_G = (Lm[1][1] - Lm[0][1]) / (Temperature[1] - Temperature[0]);
            diff_B = (Lm[1][2] - Lm[0][2]) / (Temperature[1] - Temperature[0]);



            Temperature = new int[] { -10, 2, 14, 25, 37, 48, 60, 71, 83, 94, 106, 117 };
            Lm = new double[Temperature.Length][];
            for (int i = 0; i < Temperature.Length; i++) { 
                Lm[i] = new double[3]; 
                Lm[i][0] = Lm_R + ((double)(Temperature[i] - 25)) * diff_R;
                Lm[i][1] = Lm_G + ((double)(Temperature[i] - 25)) * diff_G;
                Lm[i][2] = Lm_B + ((double)(Temperature[i] - 25)) * diff_B;

            }


            graph1.plot_brightness_T(Lm, Temperature);

        }
        private void TargetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem t = sender as ToolStripMenuItem;

            isCieGridReady = false;
            //dataGridView_CIE_points.Rows[0].ReadOnly = false;
            switch (t.Name)
            {
                case "d65ToolStripMenuItem": graph1.D65_MacAdam(3.0, 360); 
                    dataGridView_CIE_points[1, 0].Value = graph1.D65.x.ToString(); 
                    dataGridView_CIE_points[2, 0].Value = graph1.D65.y.ToString();
                    graph1.plot_CIE(1, graph1.D65);
                    break;
                case "d38ToolStripMenuItem": 
                    graph1.D38_MacAdam(3.0, 360); 
                    dataGridView_CIE_points[1, 0].Value = graph1.D38.x.ToString(); 
                    dataGridView_CIE_points[2, 0].Value = graph1.D38.y.ToString();
                    graph1.plot_CIE(1, graph1.D38);
                    break;
                default: break;
            }
            //dataGridView_CIE_points.Rows[0].ReadOnly = true;
            isCieGridReady = true;
        }




        // ISM-360 control
        private void setDataOutPathToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using (openFileDialog1)
            {
                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "ini files (*.ini)|*.ini|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string folder = Path.GetDirectoryName(openFileDialog1.FileName);
                    openFileDialog1.InitialDirectory = folder;
                    ISM360.watch(folder); 
                }
            }

        }
        private void setTriggeriniPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using ( openFileDialog2)
            {
                openFileDialog2.InitialDirectory = "c:\\";
                openFileDialog2.Filter = "ini files (*.ini)|*.ini|All files (*.*)|*.*";
                openFileDialog2.FilterIndex = 2;
                openFileDialog2.RestoreDirectory = true;

                if (openFileDialog2.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(openFileDialog2.FileName)) { ISM360.TriggerPath = openFileDialog2.FileName; }
                }
            }
        }
        private void triggerMeasureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ISM360.triggerMeasure();
        }




        int limit(int value, int max, int min = 0) { return (min < value) ? (value < max) ? value : max : min; }


    }
}
