using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace RT7216Q_temperature_compensation.Classes
{
    public partial class GUI : UserControl
    {
        public RT7216N_BD IC_BD = new RT7216N_BD();
        public RT7216Q IC_Q = new RT7216Q();
        public RT7216N_AA IC_AA = new RT7216N_AA();

        public AT32F403A MCU = new AT32F403A();

        public GUI()
        {
            InitializeComponent();

            dataGridView_response.Rows.Add("", "", "");

            dataGridView_PWM.Rows.Add(new string[] { "R", "0", "0%", "0%" });
            dataGridView_PWM.Rows.Add(new string[] { "G", "0", "0%", "0%" });
            dataGridView_PWM.Rows.Add(new string[] { "B", "0", "0%", "0%" });
            dataGridView_PWM.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            dataGridView_Pulse.Rows.Add(false, "100");
            dataGridView_Pulse.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            comboBox_IC_Type.SelectedIndex = 2;

            //ChartArea chtArea = new ChartArea("ViewArea");
            //chtArea.AxisX.Minimum = 0; //X軸數值從0開始
            //chtArea.AxisX.ScaleView.Size = 10; //設定視窗範圍內一開始顯示多少點
            //chtArea.AxisX.Interval = 5;
            //chtArea.AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            //chtArea.AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All; //設定scrollbar
            //chart1.ChartAreas[0] = chtArea; // chart new 出來時就有內建第一個chartarea
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            isPWMGridReady = true;
        }

        dynamic IC
        {
            get
            {
                switch (comboBox_IC_Type.SelectedIndex)
                {
                    case 0: return IC_AA;
                    case 1: return IC_BD;
                    case 2: return IC_Q;
                    default: return null;
                }
            }
        }

        private void comboBox_IC_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView_PrimaryRegister.Rows.Clear();

            switch (comboBox_IC_Type.SelectedIndex) 
            {
                case 0: //type AA
                    break;
                case 1: //type BD
                    
                    break;
                case 2: //type Q
                    break;
                default: break;
            }
        }


        public void updateIC2Form()
        {
            
            isPWMGridReady = false;
            dataGridView_PWM[1, 0].Value = IC.pwm.R;
            dataGridView_PWM[1, 1].Value = IC.pwm.G;
            dataGridView_PWM[1, 2].Value = IC.pwm.B;
            isPWMGridReady = true;
            
            MCU.count_time = int.Parse(dataGridView_Pulse[1, 0].Value.ToString());
            MCU.count_en = (int) ((bool)dataGridView_Pulse[0, 0].Value ? 1 : 0);

            //DataGridViewComboBoxCell combobox;
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 0]; combobox.Value = combobox.Items[IC.primary_reg.DET];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 1]; combobox.Value = combobox.Items[IC.primary_reg.DETL];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 2]; combobox.Value = combobox.Items[IC.primary_reg.IOHL];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 3]; combobox.Value = combobox.Items[IC.primary_reg.TRF];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 4]; combobox.Value = combobox.Items[IC.primary_reg.PWM_MODE_EN];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 5]; combobox.Value = combobox.Items[IC.primary_reg.THM_MODE_EN];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 6]; combobox.Value = combobox.Items[IC.primary_reg.temp_run_avg_en];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 7]; combobox.Value = combobox.Items[IC.primary_reg.temp_update_freq];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 8]; combobox.Value = combobox.Items[IC.primary_reg.tc_r_en];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 9]; combobox.Value = combobox.Items[IC.primary_reg.tc_g_en];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 10]; combobox.Value = combobox.Items[IC.primary_reg.tc_b_en];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 11]; combobox.Value = combobox.Items[IC.primary_reg.SRST];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 12]; combobox.Value = combobox.Items[IC.primary_reg.OFF_TSD];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 13]; combobox.Value = combobox.Items[IC.primary_reg.OFF_WDG];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 14]; combobox.Value = combobox.Items[IC.primary_reg.OFF_SLP];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 15]; combobox.Value = combobox.Items[IC.primary_reg.OFF_LSD];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 16]; combobox.Value = combobox.Items[IC.primary_reg.OFF_LOD];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 17]; combobox.Value = combobox.Items[IC.primary_reg.OTP_init];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 18]; combobox.Value = combobox.Items[IC.primary_reg.FORCE_D2A_EN_ON];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 19]; combobox.Value = combobox.Items[IC.primary_reg.FORCE_OSC_EN];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 20]; combobox.Value = combobox.Items[IC.primary_reg.Dout_SPD];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 21]; combobox.Value = combobox.Items[IC.primary_reg.READ_SPD];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 22]; combobox.Value = combobox.Items[IC.primary_reg.ISEL_R];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 23]; combobox.Value = combobox.Items[IC.primary_reg.ISEL_G];
            //combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 24]; combobox.Value = combobox.Items[IC.primary_reg.ISEL_B];

            //combobox = (DataGridViewComboBoxCell)dataGridView_MISC[2, 0]; combobox.Value = combobox.Items[IC.misc.ANALOG_SEL];
            //combobox = (DataGridViewComboBoxCell)dataGridView_MISC[2, 1]; combobox.Value = combobox.Items[IC.misc.CKO_ANALOG_SEL];
            //combobox = (DataGridViewComboBoxCell)dataGridView_MISC[2, 2]; combobox.Value = combobox.Items[IC.misc.ADC_VREF_VOLT];
            //combobox = (DataGridViewComboBoxCell)dataGridView_MISC[2, 3]; combobox.Value = combobox.Items[IC.misc.READ_AVG_THM_IDX];
            //combobox = (DataGridViewComboBoxCell)dataGridView_MISC[2, 4]; combobox.Value = combobox.Items[IC.misc.READ_NOW_THM_IDX];
            //combobox = (DataGridViewComboBoxCell)dataGridView_MISC[2, 5]; combobox.Value = combobox.Items[IC.misc.READ_USE_THM_IDX];
            //dataGridView_MISC[0, 0].Value = (IC.misc.enable[0]);
            //dataGridView_MISC[0, 1].Value = (IC.misc.enable[0]);
            //dataGridView_MISC[0, 2].Value = (IC.misc.enable[1]);
            //dataGridView_MISC[0, 3].Value = (IC.misc.enable[2]);
            //dataGridView_MISC[0, 4].Value = (IC.misc.enable[2]);
            //dataGridView_MISC[0, 5].Value = (IC.misc.enable[2]);



            //isOTPGridReady = false;
            //if (comboBox_OTP_Type.SelectedIndex == 0) // memory
            //{
            //    for (int i = 0; i < 32; i++)
            //    {
            //        dataGridView_OTP_ADDR[2, i].Value = IC.otp_memory[i];
            //        dataGridView_OTP_ADDR[0, i].Value = IC.otp_memory.en[i];
            //    }

            //    dataGridView_OTP_Variables[2, 0].Value = IC.otp_memory.max_PWM_chip;
            //    dataGridView_OTP_Variables[2, 1].Value = IC.otp_memory.PWM_scalar_R;
            //    dataGridView_OTP_Variables[2, 2].Value = IC.otp_memory.PWM_scalar_G;
            //    dataGridView_OTP_Variables[2, 3].Value = IC.otp_memory.PWM_scalar_B;
            //    dataGridView_OTP_Variables[2, 4].Value = IC.otp_memory.pwm_18bit_mode;
            //    dataGridView_OTP_Variables[2, 5].Value = IC.otp_memory.tc_r_base;
            //    dataGridView_OTP_Variables[2, 6].Value = IC.otp_memory.tc_r_gradient;
            //    dataGridView_OTP_Variables[2, 7].Value = IC.otp_memory.tc_g_base;
            //    dataGridView_OTP_Variables[2, 8].Value = IC.otp_memory.tc_g_gradient;
            //    dataGridView_OTP_Variables[2, 9].Value = IC.otp_memory.tc_b_base;
            //    dataGridView_OTP_Variables[2, 10].Value = IC.otp_memory.tc_b_gradient;
            //    dataGridView_OTP_Variables[2, 11].Value = IC.otp_memory.CR_R;
            //    dataGridView_OTP_Variables[2, 12].Value = IC.otp_memory.CR_G;
            //    dataGridView_OTP_Variables[2, 13].Value = IC.otp_memory.CR_B;
            //    dataGridView_OTP_Variables[2, 14].Value = IC.otp_memory.IOUT_trim;
            //    dataGridView_OTP_Variables[2, 15].Value = IC.otp_memory.OSC_Freq_Trim;
            //    dataGridView_OTP_Variables[2, 16].Value = IC.otp_memory.thermal_bias;
            //    dataGridView_OTP_Variables[2, 17].Value = IC.otp_memory.thermal_gradient;
            //    dataGridView_OTP_Variables[2, 18].Value = IC.otp_memory.V_THD_Trim;
            //    dataGridView_OTP_Variables[2, 19].Value = IC.otp_memory.tc_fine_thermal_idx[0];
            //    dataGridView_OTP_Variables[2, 20].Value = IC.otp_memory.tc_fine_thermal_idx[1];
            //    dataGridView_OTP_Variables[2, 21].Value = IC.otp_memory.tc_fine_thermal_idx[2];
            //    dataGridView_OTP_Variables[2, 22].Value = IC.otp_memory.tc_fine_thermal_idx[3];
            //    dataGridView_OTP_Variables[2, 23].Value = IC.otp_memory.tc_fine_thermal_idx[4];
            //    dataGridView_OTP_Variables[2, 24].Value = IC.otp_memory.tc_fine_thermal_idx[5];
            //    dataGridView_OTP_Variables[2, 25].Value = IC.otp_memory.tc_fine_thermal_idx[6];
            //    dataGridView_OTP_Variables[2, 26].Value = IC.otp_memory.tc_fine_thermal_idx[7];
            //    dataGridView_OTP_Variables[2, 27].Value = IC.otp_memory.tc_fine_thermal_idx[8];
            //    dataGridView_OTP_Variables[2, 28].Value = IC.otp_memory.tc_fine_thermal_idx[9];
            //    dataGridView_OTP_Variables[2, 29].Value = IC.otp_memory.tc_fine_thermal_idx[10];
            //    dataGridView_OTP_Variables[2, 30].Value = IC.otp_memory.tc_fine_shift10;
            //    dataGridView_OTP_Variables[2, 31].Value = IC.otp_memory.tc_fine_shift9;
            //    dataGridView_OTP_Variables[2, 32].Value = IC.otp_memory.tc_fine_shift8;
            //    dataGridView_OTP_Variables[2, 33].Value = IC.otp_memory.otp_internal_lock;
            //    dataGridView_OTP_Variables[2, 34].Value = IC.otp_memory.otp_program_lock;
            //}
            //else
            //{
            //    for (int i = 0; i < 32; i++)
            //    {
            //        dataGridView_OTP_ADDR[2, i].Value = IC.otp_reg[i];
            //        dataGridView_OTP_ADDR[0, i].Value = IC.otp_reg.en[i];
            //    }

            //    dataGridView_OTP_Variables[2, 0].Value = IC.otp_reg.max_PWM_chip;
            //    dataGridView_OTP_Variables[2, 1].Value = IC.otp_reg.PWM_scalar_R;
            //    dataGridView_OTP_Variables[2, 2].Value = IC.otp_reg.PWM_scalar_G;
            //    dataGridView_OTP_Variables[2, 3].Value = IC.otp_reg.PWM_scalar_B;
            //    dataGridView_OTP_Variables[2, 4].Value = IC.otp_reg.pwm_18bit_mode;
            //    dataGridView_OTP_Variables[2, 5].Value = IC.otp_reg.tc_r_base;
            //    dataGridView_OTP_Variables[2, 6].Value = IC.otp_reg.tc_r_gradient;
            //    dataGridView_OTP_Variables[2, 7].Value = IC.otp_reg.tc_g_base;
            //    dataGridView_OTP_Variables[2, 8].Value = IC.otp_reg.tc_g_gradient;
            //    dataGridView_OTP_Variables[2, 9].Value = IC.otp_reg.tc_b_base;
            //    dataGridView_OTP_Variables[2, 10].Value = IC.otp_reg.tc_b_gradient;
            //    dataGridView_OTP_Variables[2, 11].Value = IC.otp_reg.CR_R;
            //    dataGridView_OTP_Variables[2, 12].Value = IC.otp_reg.CR_G;
            //    dataGridView_OTP_Variables[2, 13].Value = IC.otp_reg.CR_B;
            //    dataGridView_OTP_Variables[2, 14].Value = IC.otp_reg.IOUT_trim;
            //    dataGridView_OTP_Variables[2, 15].Value = IC.otp_reg.OSC_Freq_Trim;
            //    dataGridView_OTP_Variables[2, 16].Value = IC.otp_reg.thermal_bias;
            //    dataGridView_OTP_Variables[2, 17].Value = IC.otp_reg.thermal_gradient;
            //    dataGridView_OTP_Variables[2, 18].Value = IC.otp_reg.V_THD_Trim;
            //    dataGridView_OTP_Variables[2, 19].Value = IC.otp_reg.tc_fine_thermal_idx[0];
            //    dataGridView_OTP_Variables[2, 20].Value = IC.otp_reg.tc_fine_thermal_idx[1];
            //    dataGridView_OTP_Variables[2, 21].Value = IC.otp_reg.tc_fine_thermal_idx[2];
            //    dataGridView_OTP_Variables[2, 22].Value = IC.otp_reg.tc_fine_thermal_idx[3];
            //    dataGridView_OTP_Variables[2, 23].Value = IC.otp_reg.tc_fine_thermal_idx[4];
            //    dataGridView_OTP_Variables[2, 24].Value = IC.otp_reg.tc_fine_thermal_idx[5];
            //    dataGridView_OTP_Variables[2, 25].Value = IC.otp_reg.tc_fine_thermal_idx[6];
            //    dataGridView_OTP_Variables[2, 26].Value = IC.otp_reg.tc_fine_thermal_idx[7];
            //    dataGridView_OTP_Variables[2, 27].Value = IC.otp_reg.tc_fine_thermal_idx[8];
            //    dataGridView_OTP_Variables[2, 28].Value = IC.otp_reg.tc_fine_thermal_idx[9];
            //    dataGridView_OTP_Variables[2, 29].Value = IC.otp_reg.tc_fine_thermal_idx[10];
            //    dataGridView_OTP_Variables[2, 30].Value = IC.otp_reg.tc_fine_shift10;
            //    dataGridView_OTP_Variables[2, 31].Value = IC.otp_reg.tc_fine_shift9;
            //    dataGridView_OTP_Variables[2, 32].Value = IC.otp_reg.tc_fine_shift8;
            //    dataGridView_OTP_Variables[2, 33].Value = IC.otp_reg.otp_internal_lock;
            //    dataGridView_OTP_Variables[2, 34].Value = IC.otp_reg.otp_program_lock;
            //}
            //isOTPGridReady = true;
        }

        bool isPWMGridReady = false;
        int lastPWMValue;
        private void dataGridView_PWM_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {  //PWM column
                var cells = dataGridView_PWM.SelectedCells;
                if (cells.Count == 0) { return; }
                lastPWMValue = int.Parse(cells[0].Value.ToString());
            }
        }
        private void dataGridView_PWM_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 1) { return; }
            if (!isPWMGridReady) { return; }

            isPWMGridReady = false;


            var grid = sender as DataGridView;
            var cells = grid.SelectedCells;
            int value;
            if (!Int32.TryParse((string)cells[0].Value, out value))
            {
                cells[0].Value = lastPWMValue.ToString();
                isPWMGridReady = true;
                return;
            }


            RT7216Q.PWM dummy_pwm = new RT7216Q.PWM(value, 0, 0);

            for (int i = 0; i < cells.Count; i++)
            {
                int row = cells[i].RowIndex;
                dataGridView_PWM.Rows[row].Cells[1].Value = dummy_pwm.R.ToString();
                dataGridView_PWM.Rows[row].Cells[2].Value = String.Format("{0:F1}%", (float)(100 * dummy_pwm.R) / (float)IC.otp_reg.max_PWM_chip);
                //TODO
                //dataGridView_PWM.Rows[row].Cells[col + 2].Value = String.Format("{0:F1}%", (float)(100 * dummy_pwm.R) / (float)IC.otp_reg.max_PWM_chip);

                switch (row)
                {
                    case 0: IC.pwm.R = dummy_pwm.R; break;
                    case 1: IC.pwm.G = dummy_pwm.R; break;
                    case 2: IC.pwm.B = dummy_pwm.R; break;
                    default: break;
                }
            }
            isPWMGridReady = true;

        }
        private void dataGridView_PWM_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn )
            {

                switch (e.RowIndex)
                {
                    case 0: IC.pwm.R = 0xFFFF;  IC.pwm.G = 0;       IC.pwm.B = 0; break;
                    case 1: IC.pwm.R = 0;       IC.pwm.G = 0xFFFF;  IC.pwm.B = 0; ; break;
                    case 2: IC.pwm.R = 0;       IC.pwm.G = 0;       IC.pwm.B = 0xFFFF; ; break;
                    default: ; break;
                }

                updateIC2Form();
                if (MCU.state == AT32F403A.Status.connected) { MCU.usb_send(IC.pwm); }
            }

        }

        private void button_PWM_Click(object sender, EventArgs e)
        {
            updateIC2Form();
            if (MCU.state == AT32F403A.Status.connected) { MCU.usb_send(IC.pwm); }
        }

        private void button_Detect_Click(object sender, EventArgs e)
        {

            Button button = sender as Button;
            switch (button.Name)
            {
                case "button_LSD": IC.primary_reg.DET = 1; MCU.usb_send(IC.primary_reg); updateResponse(1, MCU.recv_buffer[3]); break;
                case "button_LOD": IC.primary_reg.DET = 2; MCU.usb_send(IC.primary_reg); updateResponse(2, MCU.recv_buffer[3]); break;
                case "button_Temperature": IC.primary_reg.DET = 3; MCU.usb_send(IC.primary_reg); updateResponse(3, MCU.recv_buffer[3]); break;
                case "button_Temperature_ave": updateResponse(4,TemperatureDET_average()); break;
                default: break;
            }
            
        }

        private byte TemperatureDET_average()
        {
            switch (comboBox_IC_Type.SelectedIndex)
            {
                case 0: //type AA
                    break;
                case 1: //type BD
                    break;
                case 2: //type Q
                    IC_Q.primary_reg.DET = 3;
                    IC_Q.primary_reg.temp_run_avg_en = 1;
                    IC_Q.primary_reg.tc_r_en = IC_Q.primary_reg.tc_g_en = IC_Q.primary_reg.tc_b_en = 1;
                    IC_Q.misc.enable[2] = true;
                    IC_Q.misc.READ_AVG_THM_IDX = 1;
                    MCU.usb_send(IC_Q.primary_reg);
                    MCU.usb_send(IC_Q.misc);
                    return MCU.recv_buffer[3];

                default: break;
            }
            return 0;
        }

        public void updateResponse(int type, byte res)
        {
            dataGridView_response[1, 0].Value = res;
            string OUT = "";
            switch (type)
            {
                case 1: dataGridView_response[0, 0].Value = "LSD";
                    dataGridView_response[1, 0].Value = ((res >> 4) & 0x07);
                    if (((res >> 4) & 0x07) == 0) { dataGridView_response[2, 0].Value = "None is Short Circuit"; break; }

                    if (((res >> 4) & 0x01) == 0x01) { OUT = "R"; }
                    if (((res >> 4) & 0x02) == 0x02) { OUT += "G"; }
                    if (((res >> 4) & 0x04) == 0x04) { OUT += "B"; }
                    dataGridView_response[2, 0].Value = OUT + " is Short Circuit";
                    break;
                case 2: dataGridView_response[0, 0].Value = "LOD";
                    dataGridView_response[1, 0].Value = (res & 0x07);
                    if ((res & 0x07) == 0) { dataGridView_response[2, 0].Value = "None is Open Circuit"; break; }

                    if ((res & 0x01) == 0x01) { OUT = "R"; }
                    if ((res & 0x02) == 0x02) { OUT += "G"; }
                    if ((res & 0x04) == 0x04) { OUT += "B"; }
                    dataGridView_response[2, 0].Value = OUT + " is Open Circuit";
                    break;
                case 3: dataGridView_response[0, 0].Value = "Temperature"; dataGridView_response[1, 0].Value = res; dataGridView_response[2, 0].Value = RT7216N_BD.PRIMARY_REGISTER.idx2temperature(res).ToString() + "' C    tc_idx=" + RT7216N_BD.PRIMARY_REGISTER.idx2tc_idx(res).ToString(); break;
                case 4: dataGridView_response[0, 0].Value = "Average Temp"; dataGridView_response[1, 0].Value = res; dataGridView_response[2, 0].Value = RT7216N_BD.PRIMARY_REGISTER.idx2temperature(res).ToString() + "' C    tc_idx=" + RT7216N_BD.PRIMARY_REGISTER.idx2tc_idx(res).ToString(); break;
                default: break;
            }
        }

        private void tabControl2_DrawItem(object sender, DrawItemEventArgs e)
        {

            var g = e.Graphics;
            var text = this.tabControl_TC.TabPages[e.Index].Text;
            var sizeText = g.MeasureString(text, this.tabControl_TC.Font);

            var x = e.Bounds.Left + 3;
            var y = e.Bounds.Top + (e.Bounds.Height - sizeText.Height) / 2;

            g.DrawString(text, this.tabControl_TC.Font, Brushes.Black, x, y);
        }


    }
}
