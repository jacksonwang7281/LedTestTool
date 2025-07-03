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
    public partial class RT7216Q_Control : UserControl
    {
        public RT7216Q IC;

        private RT7216Q.OTP Response_OtpReg = new RT7216Q.OTP(RT7216Q.OTP.Type.register);
        private RT7216Q.OTP Response_OtpMem = new RT7216Q.OTP(RT7216Q.OTP.Type.memory);
        private bool isReadReady = false;

        private bool isPWMGridReady = false;
        private bool isPrimaryReady = false;
        private bool isMiscReady = false;
        private bool isOTPGridReady = false;
        
        private DataGridView currentFocus;

        public RT7216Q_Control()
        {
            InitializeComponent();

            IC = new RT7216Q() {

                otp_reg = new RT7216Q.OTP(RT7216Q.OTP.Type.register)
                {
                    tc_r_base = 168, tc_r_gradient = 22,
                    tc_g_base = 228, tc_g_gradient = 7,
                    tc_b_base = 232, tc_b_gradient = 6,
                    CR_R = 125,
                    CR_G = 127,
                    CR_B = 51,
                    tc_fine_shift10 = 1
                }
            };

            IC.otp_memory.max_PWM_chip = 12285;
            for (int i = 0; i < 32; i++)
            {
                if (i == 13) { continue; }
                if (i == 14) { continue; }
                if (i == 15) { continue; }
                if (i == 29) { continue; }
                if (i == 31) { continue; }
                IC.otp_memory.en[i] = true;
            }
            IC.otp_reg.tc_fine_thermal_idx = new int[11] { 22, 14, 7, 2, 1, 2, 9, 30, 70, 120, 140 };

            dataGridView_PWM.Rows.Add(new string[]{ "R", "0", "0%", "0%" });
            dataGridView_PWM.Rows.Add(new string[]{ "G", "0", "0%", "0%" });
            dataGridView_PWM.Rows.Add(new string[]{ "B", "0", "0%", "0%" });
            dataGridView_PWM.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            DataGridViewComboBoxCell combobox;
            dataGridView_PrimaryRegister.Rows.Add("F_DET");             combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,0] ; dataGridView_PrimaryRegister[0,0] .ToolTipText = "Detection function select"; combobox.Items.Add("0 (None)"); combobox.Items.Add("1 (LSD)"); combobox.Items.Add("2 (LOD)"); combobox.Items.Add("3 (Temperature)");
            dataGridView_PrimaryRegister.Rows.Add("F_DETL");            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,1] ; dataGridView_PrimaryRegister[0,1] .ToolTipText = "LOD, LSD level select"; combobox.Items.Add("0 (LOD=0.2  LSD=VDD-0.2)"); combobox.Items.Add("1 (LOD=0.4  LSD=VDD-0.4)"); combobox.Items.Add("2 (LOD=0.6  LSD=VDD-0.6)"); combobox.Items.Add("3 (LOD=0.8  LSD=VDD-0.8)");
            dataGridView_PrimaryRegister.Rows.Add("F_IOHL");            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,2] ; dataGridView_PrimaryRegister[0,2] .ToolTipText = "Output buffer driving current select"; combobox.Items.Add("0 lowest"); combobox.Items.Add("1 low"); combobox.Items.Add("2 high"); combobox.Items.Add("3 highest");
            dataGridView_PrimaryRegister.Rows.Add("F_TRF");             combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,3] ; dataGridView_PrimaryRegister[0,3] .ToolTipText = "Channel IOUT slew rate"; combobox.Items.Add("0 slow"); combobox.Items.Add("1 fast");
            dataGridView_PrimaryRegister.Rows.Add("DISP_PWM_MODE_EN");  combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,4] ; dataGridView_PrimaryRegister[0,4] .ToolTipText = "PWM gradual change effect"; combobox.Items.Add("0 Disable"); combobox.Items.Add("1 Enable");
            dataGridView_PrimaryRegister.Rows.Add("DISP_THM_MODE_EN");  combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,5] ; dataGridView_PrimaryRegister[0,5] .ToolTipText = "Temperature gradual change effect"; combobox.Items.Add("0 Disable"); combobox.Items.Add("1 Enable");
            dataGridView_PrimaryRegister.Rows.Add("TEMP_RUN_AVG_EN");   combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,6] ; dataGridView_PrimaryRegister[0,6] .ToolTipText = "Running average function for temperature compensation"; combobox.Items.Add("0 Disable"); combobox.Items.Add("1 Enable");
            dataGridView_PrimaryRegister.Rows.Add("TEMP_UPDATE_FREQ");  combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,7] ; dataGridView_PrimaryRegister[0,7] .ToolTipText = "Running average data update frequency"; combobox.Items.Add("0 (1-frame)"); combobox.Items.Add("1 (4-frame)"); combobox.Items.Add("2 (8-frame)"); combobox.Items.Add("3 (16-frame)"); combobox.Items.Add("4 (32-frame)"); combobox.Items.Add("5 (64-frame)"); combobox.Items.Add("6 (128-frame)"); combobox.Items.Add("7 (256-frame)");
            dataGridView_PrimaryRegister.Rows.Add("TC_R_EN");           combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,8] ; dataGridView_PrimaryRegister[0,8] .ToolTipText = "Temperature compensation for OUTR"; combobox.Items.Add("0 Disable"); combobox.Items.Add("1 Enable");
            dataGridView_PrimaryRegister.Rows.Add("TC_G_EN");           combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,9] ; dataGridView_PrimaryRegister[0,9] .ToolTipText = "Temperature compensation for OUTG"; combobox.Items.Add("0 Disable"); combobox.Items.Add("1 Enable");
            dataGridView_PrimaryRegister.Rows.Add("TC_B_EN");           combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,10]; dataGridView_PrimaryRegister[0,10].ToolTipText = "Temperature compensation for OUTB"; combobox.Items.Add("0 Disable"); combobox.Items.Add("1 Enable");
            dataGridView_PrimaryRegister.Rows.Add("F_SRST");            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,11]; dataGridView_PrimaryRegister[0,11].ToolTipText = "Software reset display"; combobox.Items.Add("0 Normal Display"); combobox.Items.Add("1 Reset Display");
            dataGridView_PrimaryRegister.Rows.Add("OFF_TSD");           combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,12]; dataGridView_PrimaryRegister[0,12].ToolTipText = "Thermal shutdown function"; combobox.Items.Add("0 Enable"); combobox.Items.Add("1 Disable");
            dataGridView_PrimaryRegister.Rows.Add("OFF_WDG");           combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,13]; dataGridView_PrimaryRegister[0,13].ToolTipText = "Watchdog function"; combobox.Items.Add("0 Enable"); combobox.Items.Add("1 Disable");
            dataGridView_PrimaryRegister.Rows.Add("OFF_SLP");           combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,14]; dataGridView_PrimaryRegister[0,14].ToolTipText = "Sleep function"; combobox.Items.Add("0 Enable"); combobox.Items.Add("1 Disable");
            dataGridView_PrimaryRegister.Rows.Add("OFF_LSD");           combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,15]; dataGridView_PrimaryRegister[0,15].ToolTipText = "Short mark function"; combobox.Items.Add("0 Enable"); combobox.Items.Add("1 Disable");
            dataGridView_PrimaryRegister.Rows.Add("OFF_LOD");           combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,16]; dataGridView_PrimaryRegister[0,16].ToolTipText = "Open mark function"; combobox.Items.Add("0 Enable"); combobox.Items.Add("1 Disable");
            dataGridView_PrimaryRegister.Rows.Add("OTP_INIT");          combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,17]; dataGridView_PrimaryRegister[0,17].ToolTipText = "Read OTP memory (takes 400us)"; combobox.Items.Add("0 Activate read"); combobox.Items.Add("1 No action");
            dataGridView_PrimaryRegister.Rows.Add("FORCE_D2A_EN_ON");   combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,18]; dataGridView_PrimaryRegister[0,18].ToolTipText = ""; combobox.Items.Add("0 Disable"); combobox.Items.Add("1 D2A always on");
            dataGridView_PrimaryRegister.Rows.Add("FORCE_OSC_EN_ON");   combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,19]; dataGridView_PrimaryRegister[0,19].ToolTipText = ""; combobox.Items.Add("0 Disable"); combobox.Items.Add("1 Osc always on");
            dataGridView_PrimaryRegister.Rows.Add("DOUT_SPD");          combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,20]; dataGridView_PrimaryRegister[0,20].ToolTipText = "Data Out frequency select";               combobox.Items.Add("0 (15MHz)"); combobox.Items.Add("1 (7.5MHz)"); combobox.Items.Add("2 (3.75MHz)"); combobox.Items.Add("3 (1.87MHz)"); combobox.Items.Add("4 (0.93MHz)"); combobox.Items.Add("others (0.46MHz)");
            dataGridView_PrimaryRegister.Rows.Add("READ_SPD");          combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 21]; dataGridView_PrimaryRegister[0, 21].ToolTipText = "Report frequency select (READ_SPD <= DOUT_SPD)"; combobox.Items.Add("0 (15MHz)"); combobox.Items.Add("1 (7.5MHz)"); combobox.Items.Add("2 (3.75MHz)"); combobox.Items.Add("3 (1.87MHz)"); combobox.Items.Add("4 (0.93MHz)"); combobox.Items.Add("others (0.46MHz)");
            dataGridView_PrimaryRegister.Rows.Add("ISEL_R");            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,22]; dataGridView_PrimaryRegister[0,22].ToolTipText = "Maximum IOUT for OUTR"; combobox.Items.Add("0 (30mA) "); combobox.Items.Add("1 (60mA)");
            dataGridView_PrimaryRegister.Rows.Add("ISEL_G");            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,23]; dataGridView_PrimaryRegister[0,23].ToolTipText = "Maximum IOUT for OUTG"; combobox.Items.Add("0 (30mA) "); combobox.Items.Add("1 (60mA)");
            dataGridView_PrimaryRegister.Rows.Add("ISEL_B");            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,24]; dataGridView_PrimaryRegister[0,24].ToolTipText = "Maximum IOUT for OUTB"; combobox.Items.Add("0 (30mA) "); combobox.Items.Add("1 (60mA)");

            dataGridView_MISC.Rows.Add(false,"ANALOG_SEL");             combobox = (DataGridViewComboBoxCell)dataGridView_MISC[2, 0]; dataGridView_MISC[1, 0].ToolTipText = "DAO mode select (requires CKO_ANALOG_SEL = 1)"; combobox.Items.Add("0 None"); combobox.Items.Add("1 A2D_LSOD_R"); combobox.Items.Add("2 A2D_LSOD_G"); combobox.Items.Add("3 A2D_LSOD_B"); combobox.Items.Add("4 TSD"); combobox.Items.Add("5 A2D_ADC_DOUT[0]"); combobox.Items.Add("6 A2D_ADC_DOUT[1]"); combobox.Items.Add("7 A2D_ADC_DOUT[2]"); combobox.Items.Add("8 A2D_ADC_DOUT[3]"); combobox.Items.Add("9 A2D_ADC_DOUT[4]"); combobox.Items.Add("10 A2D_ADC_DOUT[5]"); combobox.Items.Add("11 OSC 78.125 Hz"); combobox.Items.Add("default 1");
            dataGridView_MISC.Rows.Add(false,"CKO_ANALOG_SEL");         combobox = (DataGridViewComboBoxCell)dataGridView_MISC[2, 1]; dataGridView_MISC[1, 1].ToolTipText = "CKO mode select"; combobox.Items.Add("0 normal"); combobox.Items.Add("1 test mode"); 
            dataGridView_MISC.Rows.Add(false,"ADC_VREF_VOLT");          combobox = (DataGridViewComboBoxCell)dataGridView_MISC[2, 2]; dataGridView_MISC[1, 2].ToolTipText = "ADC reference level select"; combobox.Items.Add("0 (Vbe)"); combobox.Items.Add("1 (0.5)"); combobox.Items.Add("2 (0.6)"); combobox.Items.Add("3 (0.7)");
            dataGridView_MISC.Rows.Add(false,"READ_AVG_THM_IDX");       combobox = (DataGridViewComboBoxCell)dataGridView_MISC[2, 3]; dataGridView_MISC[1, 3].ToolTipText = ""; combobox.Items.Add("0 normal"); combobox.Items.Add("1 running average");
            dataGridView_MISC.Rows.Add(false,"READ_NOW_THM_IDX");       combobox = (DataGridViewComboBoxCell)dataGridView_MISC[2, 4]; dataGridView_MISC[1, 4].ToolTipText = ""; combobox.Items.Add("0 normal"); combobox.Items.Add("1 Temperature reading");
            dataGridView_MISC.Rows.Add(false,"READ_USE_THM_IDX");       combobox = (DataGridViewComboBoxCell)dataGridView_MISC[2, 5]; dataGridView_MISC[1, 5].ToolTipText = ""; combobox.Items.Add("0 normal"); combobox.Items.Add("1 active tc index");

            for (int i = 0; i < 32; i++) { dataGridView_OTP_ADDR.Rows.Add(false, i.ToString(), 0, ""); } //dataGridView_OTP_ADDR[3, i].ToolTipText = "Will only update after a read command is sent.";

            dataGridView_OTP_Variables.Rows.Add(false, "max_PWM_chip", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "PWM_scalar_R", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "PWM_scalar_G", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "PWM_scalar_B", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "pwm_18bit_mode", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "tc_r_base", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "tc_r_gradient", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "tc_g_base", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "tc_g_gradient", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "tc_b_base", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "tc_b_gradient", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "CR_R", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "CR_G", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "CR_B", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "IOUT_trim", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "OSC_Freq_Trim", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "thermal_bias", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "thermal_gradient", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "V_THD_Trim", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "tc_fine_thermal_idx0", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "tc_fine_thermal_idx1", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "tc_fine_thermal_idx2", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "tc_fine_thermal_idx3", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "tc_fine_thermal_idx4", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "tc_fine_thermal_idx5", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "tc_fine_thermal_idx6", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "tc_fine_thermal_idx7", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "tc_fine_thermal_idx8", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "tc_fine_thermal_idx9", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "tc_fine_thermal_idx10", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "tc_fine_shift10", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "tc_fine_shift9", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "tc_fine_shift8", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "otp_internal_lock", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "otp_program_lock", 0, "");

            //dataGridView_PrimaryRegister[1, 0].Value = "1 (LSD)";

            dataGridView_OTP_ADDR.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            dataGridView_response.Rows.Add("", "", "");
        }
        private void RT7216Q_Control_Load(object sender, EventArgs e)
        {
            isPWMGridReady = true;
            isPrimaryReady = true;
            isMiscReady = true;
            isOTPGridReady = true;

            dataGridView_Enter(dataGridView_PWM, null);

            comboBox_OTP_Type.SelectedIndex = 1;

            isReadReady = true;
            updateIC2Form(); 
        }

        public void ImportFileSetting(RT7216Q IC)
        {



        }


        public void updateIC2Form() //jackson
        {
            isPWMGridReady = false;
            dataGridView_PWM[1, 0].Value = IC.pwm.R;
            dataGridView_PWM[1, 1].Value = IC.pwm.G;
            dataGridView_PWM[1, 2].Value = IC.pwm.B;
            isPWMGridReady = true;

            DataGridViewComboBoxCell combobox;
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,0] ; combobox.Value = combobox.Items [IC.primary_reg.DET];  //jackson
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,1] ; combobox.Value = combobox.Items [IC.primary_reg.DETL];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,2] ; combobox.Value = combobox.Items [IC.primary_reg.IOHL];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,3] ; combobox.Value = combobox.Items [IC.primary_reg.TRF];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,4] ; combobox.Value = combobox.Items [IC.primary_reg.PWM_MODE_EN];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,5] ; combobox.Value = combobox.Items [IC.primary_reg.THM_MODE_EN];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,6] ; combobox.Value = combobox.Items [IC.primary_reg.temp_run_avg_en];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,7] ; combobox.Value = combobox.Items [IC.primary_reg.temp_update_freq];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,8] ; combobox.Value = combobox.Items [IC.primary_reg.tc_r_en];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,9] ; combobox.Value = combobox.Items [IC.primary_reg.tc_g_en];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,10]; combobox.Value = combobox.Items[IC.primary_reg.tc_b_en];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,11]; combobox.Value = combobox.Items[IC.primary_reg.SRST];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,12]; combobox.Value = combobox.Items[IC.primary_reg.OFF_TSD];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,13]; combobox.Value = combobox.Items[IC.primary_reg.OFF_WDG];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,14]; combobox.Value = combobox.Items[IC.primary_reg.OFF_SLP];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,15]; combobox.Value = combobox.Items[IC.primary_reg.OFF_LSD];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,16]; combobox.Value = combobox.Items[IC.primary_reg.OFF_LOD];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,17]; combobox.Value = combobox.Items[IC.primary_reg.OTP_init];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,18]; combobox.Value = combobox.Items[IC.primary_reg.FORCE_D2A_EN_ON];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,19]; combobox.Value = combobox.Items[IC.primary_reg.FORCE_OSC_EN];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,20]; combobox.Value = combobox.Items[IC.primary_reg.Dout_SPD];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,21]; combobox.Value = combobox.Items[IC.primary_reg.READ_SPD];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,22]; combobox.Value = combobox.Items[IC.primary_reg.ISEL_R];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,23]; combobox.Value = combobox.Items[IC.primary_reg.ISEL_G];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,24]; combobox.Value = combobox.Items[IC.primary_reg.ISEL_B];

            combobox = (DataGridViewComboBoxCell)dataGridView_MISC[2, 0]; combobox.Value = combobox.Items[IC.misc.ANALOG_SEL];
            combobox = (DataGridViewComboBoxCell)dataGridView_MISC[2, 1]; combobox.Value = combobox.Items[IC.misc.CKO_ANALOG_SEL];
            combobox = (DataGridViewComboBoxCell)dataGridView_MISC[2, 2]; combobox.Value = combobox.Items[IC.misc.ADC_VREF_VOLT];
            combobox = (DataGridViewComboBoxCell)dataGridView_MISC[2, 3]; combobox.Value = combobox.Items[IC.misc.READ_AVG_THM_IDX];
            combobox = (DataGridViewComboBoxCell)dataGridView_MISC[2, 4]; combobox.Value = combobox.Items[IC.misc.READ_NOW_THM_IDX];
            combobox = (DataGridViewComboBoxCell)dataGridView_MISC[2, 5]; combobox.Value = combobox.Items[IC.misc.READ_USE_THM_IDX];
            dataGridView_MISC[0, 0].Value = (IC.misc.enable[0]);
            dataGridView_MISC[0, 1].Value = (IC.misc.enable[0]);
            dataGridView_MISC[0, 2].Value = (IC.misc.enable[1]);
            dataGridView_MISC[0, 3].Value = (IC.misc.enable[2]);
            dataGridView_MISC[0, 4].Value = (IC.misc.enable[2]);
            dataGridView_MISC[0, 5].Value = (IC.misc.enable[2]);



            isOTPGridReady = false;
            if (comboBox_OTP_Type.SelectedIndex == 0) // memory
            {
                for (int i = 0; i < 32; i++) { 
                    dataGridView_OTP_ADDR[2, i].Value = IC.otp_memory[i];
                    dataGridView_OTP_ADDR[0, i].Value = IC.otp_memory.en[i];
                }

                dataGridView_OTP_Variables[2, 0].Value =  IC.otp_memory.max_PWM_chip;
                dataGridView_OTP_Variables[2, 1].Value =  IC.otp_memory.PWM_scalar_R           ;
                dataGridView_OTP_Variables[2, 2].Value =  IC.otp_memory.PWM_scalar_G           ;
                dataGridView_OTP_Variables[2, 3].Value =  IC.otp_memory.PWM_scalar_B           ;
                dataGridView_OTP_Variables[2, 4].Value =  IC.otp_memory.pwm_18bit_mode         ;
                dataGridView_OTP_Variables[2, 5].Value =  IC.otp_memory.tc_r_base              ;
                dataGridView_OTP_Variables[2, 6].Value =  IC.otp_memory.tc_r_gradient          ;
                dataGridView_OTP_Variables[2, 7].Value =  IC.otp_memory.tc_g_base              ;
                dataGridView_OTP_Variables[2, 8].Value =  IC.otp_memory.tc_g_gradient          ;
                dataGridView_OTP_Variables[2, 9].Value =  IC.otp_memory.tc_b_base              ;
                dataGridView_OTP_Variables[2, 10].Value = IC.otp_memory.tc_b_gradient          ;
                dataGridView_OTP_Variables[2, 11].Value = IC.otp_memory.CR_R                   ;
                dataGridView_OTP_Variables[2, 12].Value = IC.otp_memory.CR_G                   ;
                dataGridView_OTP_Variables[2, 13].Value = IC.otp_memory.CR_B                   ;
                dataGridView_OTP_Variables[2, 14].Value = IC.otp_memory.IOUT_trim              ;
                dataGridView_OTP_Variables[2, 15].Value = IC.otp_memory.OSC_Freq_Trim          ;
                dataGridView_OTP_Variables[2, 16].Value = IC.otp_memory.thermal_bias           ;
                dataGridView_OTP_Variables[2, 17].Value = IC.otp_memory.thermal_gradient       ;
                dataGridView_OTP_Variables[2, 18].Value = IC.otp_memory.V_THD_Trim             ;
                dataGridView_OTP_Variables[2, 19].Value = IC.otp_memory.tc_fine_thermal_idx[0 ];
                dataGridView_OTP_Variables[2, 20].Value = IC.otp_memory.tc_fine_thermal_idx[1 ];
                dataGridView_OTP_Variables[2, 21].Value = IC.otp_memory.tc_fine_thermal_idx[2 ];
                dataGridView_OTP_Variables[2, 22].Value = IC.otp_memory.tc_fine_thermal_idx[3 ];
                dataGridView_OTP_Variables[2, 23].Value = IC.otp_memory.tc_fine_thermal_idx[4 ];
                dataGridView_OTP_Variables[2, 24].Value = IC.otp_memory.tc_fine_thermal_idx[5 ];
                dataGridView_OTP_Variables[2, 25].Value = IC.otp_memory.tc_fine_thermal_idx[6 ];
                dataGridView_OTP_Variables[2, 26].Value = IC.otp_memory.tc_fine_thermal_idx[7 ];
                dataGridView_OTP_Variables[2, 27].Value = IC.otp_memory.tc_fine_thermal_idx[8 ];
                dataGridView_OTP_Variables[2, 28].Value = IC.otp_memory.tc_fine_thermal_idx[9 ];
                dataGridView_OTP_Variables[2, 29].Value = IC.otp_memory.tc_fine_thermal_idx[10];
                dataGridView_OTP_Variables[2, 30].Value = IC.otp_memory.tc_fine_shift10        ;
                dataGridView_OTP_Variables[2, 31].Value = IC.otp_memory.tc_fine_shift9         ;
                dataGridView_OTP_Variables[2, 32].Value = IC.otp_memory.tc_fine_shift8         ;
                dataGridView_OTP_Variables[2, 33].Value = IC.otp_memory.otp_internal_lock      ;
                dataGridView_OTP_Variables[2, 34].Value = IC.otp_memory.otp_program_lock       ;
            }
            else
            {
                for (int i = 0; i < 32; i++) { 
                    dataGridView_OTP_ADDR[2, i].Value = IC.otp_reg[i]; 
                    dataGridView_OTP_ADDR[0, i].Value = IC.otp_reg.en[i]; 
                }

                dataGridView_OTP_Variables[2, 0].Value = IC.otp_reg.max_PWM_chip;
                dataGridView_OTP_Variables[2, 1].Value = IC.otp_reg.PWM_scalar_R;
                dataGridView_OTP_Variables[2, 2].Value = IC.otp_reg.PWM_scalar_G;
                dataGridView_OTP_Variables[2, 3].Value = IC.otp_reg.PWM_scalar_B;
                dataGridView_OTP_Variables[2, 4].Value = IC.otp_reg.pwm_18bit_mode;
                dataGridView_OTP_Variables[2, 5].Value = IC.otp_reg.tc_r_base;
                dataGridView_OTP_Variables[2, 6].Value = IC.otp_reg.tc_r_gradient;
                dataGridView_OTP_Variables[2, 7].Value = IC.otp_reg.tc_g_base;
                dataGridView_OTP_Variables[2, 8].Value = IC.otp_reg.tc_g_gradient;
                dataGridView_OTP_Variables[2, 9].Value = IC.otp_reg.tc_b_base;
                dataGridView_OTP_Variables[2, 10].Value = IC.otp_reg.tc_b_gradient;
                dataGridView_OTP_Variables[2, 11].Value = IC.otp_reg.CR_R;
                dataGridView_OTP_Variables[2, 12].Value = IC.otp_reg.CR_G;
                dataGridView_OTP_Variables[2, 13].Value = IC.otp_reg.CR_B;
                dataGridView_OTP_Variables[2, 14].Value = IC.otp_reg.IOUT_trim;
                dataGridView_OTP_Variables[2, 15].Value = IC.otp_reg.OSC_Freq_Trim;
                dataGridView_OTP_Variables[2, 16].Value = IC.otp_reg.thermal_bias;
                dataGridView_OTP_Variables[2, 17].Value = IC.otp_reg.thermal_gradient;
                dataGridView_OTP_Variables[2, 18].Value = IC.otp_reg.V_THD_Trim;
                dataGridView_OTP_Variables[2, 19].Value = IC.otp_reg.tc_fine_thermal_idx[0];
                dataGridView_OTP_Variables[2, 20].Value = IC.otp_reg.tc_fine_thermal_idx[1];
                dataGridView_OTP_Variables[2, 21].Value = IC.otp_reg.tc_fine_thermal_idx[2];
                dataGridView_OTP_Variables[2, 22].Value = IC.otp_reg.tc_fine_thermal_idx[3];
                dataGridView_OTP_Variables[2, 23].Value = IC.otp_reg.tc_fine_thermal_idx[4];
                dataGridView_OTP_Variables[2, 24].Value = IC.otp_reg.tc_fine_thermal_idx[5];
                dataGridView_OTP_Variables[2, 25].Value = IC.otp_reg.tc_fine_thermal_idx[6];
                dataGridView_OTP_Variables[2, 26].Value = IC.otp_reg.tc_fine_thermal_idx[7];
                dataGridView_OTP_Variables[2, 27].Value = IC.otp_reg.tc_fine_thermal_idx[8];
                dataGridView_OTP_Variables[2, 28].Value = IC.otp_reg.tc_fine_thermal_idx[9];
                dataGridView_OTP_Variables[2, 29].Value = IC.otp_reg.tc_fine_thermal_idx[10];
                dataGridView_OTP_Variables[2, 30].Value = IC.otp_reg.tc_fine_shift10;
                dataGridView_OTP_Variables[2, 31].Value = IC.otp_reg.tc_fine_shift9;
                dataGridView_OTP_Variables[2, 32].Value = IC.otp_reg.tc_fine_shift8;
                dataGridView_OTP_Variables[2, 33].Value = IC.otp_reg.otp_internal_lock;
                dataGridView_OTP_Variables[2, 34].Value = IC.otp_reg.otp_program_lock;
            }
            isOTPGridReady = true;
        }
        public void updateForm2IC()
        {
            IC.pwm.R = int.Parse(dataGridView_PWM[1, 0].Value.ToString());
            IC.pwm.G = int.Parse(dataGridView_PWM[1, 1].Value.ToString());
            IC.pwm.B = int.Parse(dataGridView_PWM[1, 2].Value.ToString());

            DataGridViewComboBoxCell cell;
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,0] ;  IC.primary_reg.DET =              cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,1] ;  IC.primary_reg.DETL =             cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,2] ;  IC.primary_reg.IOHL =             cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,3] ;  IC.primary_reg.TRF =              cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,4] ;  IC.primary_reg.PWM_MODE_EN =      cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,5] ;  IC.primary_reg.THM_MODE_EN =      cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,6] ;  IC.primary_reg.temp_run_avg_en =  cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,7] ;  IC.primary_reg.temp_update_freq = cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,8] ;  IC.primary_reg.tc_r_en =          cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,9] ;  IC.primary_reg.tc_g_en =          cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,10];  IC.primary_reg.tc_b_en =          cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,11];  IC.primary_reg.SRST =             cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,12];  IC.primary_reg.OFF_TSD =          cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,13];  IC.primary_reg.OFF_WDG =          cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,14];  IC.primary_reg.OFF_SLP =          cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,15];  IC.primary_reg.OFF_LSD =          cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,16];  IC.primary_reg.OFF_LOD =          cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,17];  IC.primary_reg.OTP_init =         cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,18];  IC.primary_reg.FORCE_D2A_EN_ON =  cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,19];  IC.primary_reg.FORCE_OSC_EN =     cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,20];  IC.primary_reg.Dout_SPD =         cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,21];  IC.primary_reg.READ_SPD =         cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,22];  IC.primary_reg.ISEL_R =           cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,23];  IC.primary_reg.ISEL_G =           cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1,24];  IC.primary_reg.ISEL_B =            cell.Items.IndexOf(cell.Value);

            cell = (DataGridViewComboBoxCell)dataGridView_MISC[2, 0]; IC.misc.ANALOG_SEL = cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_MISC[2, 1]; IC.misc.CKO_ANALOG_SEL = cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_MISC[2, 2]; IC.misc.ADC_VREF_VOLT= cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_MISC[2, 3]; IC.misc.READ_AVG_THM_IDX = cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_MISC[2, 4]; IC.misc.READ_NOW_THM_IDX= cell.Items.IndexOf(cell.Value);
            cell = (DataGridViewComboBoxCell)dataGridView_MISC[2, 5]; IC.misc.READ_USE_THM_IDX= cell.Items.IndexOf(cell.Value);

            if (comboBox_OTP_Type.SelectedIndex == 0) //memory
            {
                for(int i = 0 ; i < 32; i++){ IC.otp_memory[i] = int.Parse(dataGridView_OTP_ADDR[2, i].Value.ToString());}
            }
            else //register
            {
                for (int i = 0; i < 32; i++){IC.otp_reg[i] = int.Parse(dataGridView_OTP_ADDR[2, i].Value.ToString());}
            }
        }

        public void updateResponse(int type, byte res)
        {
            
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
                case 3: dataGridView_response[0, 0].Value = "Temperature"; dataGridView_response[1, 0].Value = res; dataGridView_response[2, 0].Value = RT7216Q.PRIMARY_REGISTER.idx2temperature(res).ToString() + "' C    tc_idx=" + RT7216Q.PRIMARY_REGISTER.idx2tc_idx(res).ToString(); break;
                case 4: dataGridView_response[0, 0].Value = "Average T"; dataGridView_response[1, 0].Value = res; dataGridView_response[2, 0].Value = RT7216Q.PRIMARY_REGISTER.idx2temperature(res).ToString() + "' C    tc_idx=" + RT7216Q.PRIMARY_REGISTER.idx2tc_idx(res).ToString(); break;
                case 5: dataGridView_response[0, 0].Value = "Current T"; dataGridView_response[1, 0].Value = res; dataGridView_response[2, 0].Value = RT7216Q.PRIMARY_REGISTER.idx2temperature(res).ToString() + "' C    tc_idx=" + RT7216Q.PRIMARY_REGISTER.idx2tc_idx(res).ToString(); break;
                case 6: dataGridView_response[0, 0].Value = "Current idx"; dataGridView_response[1, 0].Value = res; dataGridView_response[2, 0].Value = "" ; break;
                default: break;
            }
        }
        public void updateOtpRead(RT7216Q.OTP OTP_res = null)
        {
            if (!isReadReady) { return; }
            if (OTP_res != null)
            {
                if (OTP_res.type == RT7216Q.OTP.Type.register){Response_OtpReg = OTP_res;}
                if (OTP_res.type == RT7216Q.OTP.Type.memory){Response_OtpMem = OTP_res; }
            }

            RT7216Q.OTP otp = (comboBox_OTP_Type.SelectedIndex == 0) ? Response_OtpMem : Response_OtpReg;
            
            for (int i = 0; i < 32; i++) { dataGridView_OTP_ADDR[3, i].Value = otp[i]; }
            dataGridView_OTP_Variables[3, 0].Value =  otp.max_PWM_chip;
            dataGridView_OTP_Variables[3, 1].Value =  otp.PWM_scalar_R;
            dataGridView_OTP_Variables[3, 2].Value =  otp.PWM_scalar_G;
            dataGridView_OTP_Variables[3, 3].Value =  otp.PWM_scalar_B;
            dataGridView_OTP_Variables[3, 4].Value =  otp.pwm_18bit_mode;
            dataGridView_OTP_Variables[3, 5].Value =  otp.tc_r_base;
            dataGridView_OTP_Variables[3, 6].Value =  otp.tc_r_gradient;
            dataGridView_OTP_Variables[3, 7].Value =  otp.tc_g_base;
            dataGridView_OTP_Variables[3, 8].Value =  otp.tc_g_gradient;
            dataGridView_OTP_Variables[3, 9].Value =  otp.tc_b_base;
            dataGridView_OTP_Variables[3, 10].Value = otp.tc_b_gradient;
            dataGridView_OTP_Variables[3, 11].Value = otp.CR_R;
            dataGridView_OTP_Variables[3, 12].Value = otp.CR_G;
            dataGridView_OTP_Variables[3, 13].Value = otp.CR_B;
            dataGridView_OTP_Variables[3, 14].Value = otp.IOUT_trim;
            dataGridView_OTP_Variables[3, 15].Value = otp.OSC_Freq_Trim;
            dataGridView_OTP_Variables[3, 16].Value = otp.thermal_bias;
            dataGridView_OTP_Variables[3, 17].Value = otp.thermal_gradient;
            dataGridView_OTP_Variables[3, 18].Value = otp.V_THD_Trim;
            dataGridView_OTP_Variables[3, 19].Value = otp.tc_fine_thermal_idx[0];
            dataGridView_OTP_Variables[3, 20].Value = otp.tc_fine_thermal_idx[1];
            dataGridView_OTP_Variables[3, 21].Value = otp.tc_fine_thermal_idx[2];
            dataGridView_OTP_Variables[3, 22].Value = otp.tc_fine_thermal_idx[3];
            dataGridView_OTP_Variables[3, 23].Value = otp.tc_fine_thermal_idx[4];
            dataGridView_OTP_Variables[3, 24].Value = otp.tc_fine_thermal_idx[5];
            dataGridView_OTP_Variables[3, 25].Value = otp.tc_fine_thermal_idx[6];
            dataGridView_OTP_Variables[3, 26].Value = otp.tc_fine_thermal_idx[7];
            dataGridView_OTP_Variables[3, 27].Value = otp.tc_fine_thermal_idx[8];
            dataGridView_OTP_Variables[3, 28].Value = otp.tc_fine_thermal_idx[9];
            dataGridView_OTP_Variables[3, 29].Value = otp.tc_fine_thermal_idx[10];
            dataGridView_OTP_Variables[3, 30].Value = otp.tc_fine_shift10;
            dataGridView_OTP_Variables[3, 31].Value = otp.tc_fine_shift9;
            dataGridView_OTP_Variables[3, 32].Value = otp.tc_fine_shift8;
            dataGridView_OTP_Variables[3, 33].Value = otp.otp_internal_lock;
            dataGridView_OTP_Variables[3, 34].Value = otp.otp_program_lock;
        }
        private void button_Click(object sender, EventArgs e)
        {
           // updateForm2IC();
        }

        int lastPWMValue;
        private void dataGridView_PWM_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1) {  //PWM column
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


        private void dataGridView_ComboBox_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0){return;   }

            DataGridView datagridview = sender as DataGridView;
            if ((datagridview.Name == "dataGridView_PrimaryRegister") && (e.ColumnIndex != 1)) { return; }
            if ((datagridview.Name == "dataGridView_MISC") && (e.ColumnIndex != 2)) { return; }

            datagridview.EndEdit();
            datagridview.CurrentCell = datagridview[e.ColumnIndex, e.RowIndex];
            datagridview.BeginEdit(false);
            ((ComboBox)datagridview.EditingControl).DroppedDown = true;
        }
        private void dataGridView_PrimaryRegister_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 1) { return; }
            if (!isPrimaryReady) { return; }
            isPrimaryReady = false;

            DataGridView grid = sender as DataGridView;
            DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)grid[1, e.RowIndex];
            int index = cell.Items.IndexOf(cell.Value);
            switch (e.RowIndex)
            {
                case 0: IC.primary_reg.DET = index; break;
                case 1: IC.primary_reg.DETL = index; break;
                case 2: IC.primary_reg.IOHL = index; break;
                case 3: IC.primary_reg.TRF = index; break;
                case 4: IC.primary_reg.PWM_MODE_EN = index; break;
                case 5: IC.primary_reg.THM_MODE_EN = index; break;
                case 6: IC.primary_reg.temp_run_avg_en = index; break;
                case 7: IC.primary_reg.temp_update_freq = index; break;
                case 8: IC.primary_reg.tc_r_en = index; break;
                case 9: IC.primary_reg.tc_g_en = index; break;
                case 10: IC.primary_reg.tc_b_en = index; break;
                case 11: IC.primary_reg.SRST = index; break;
                case 12: IC.primary_reg.OFF_TSD = index; break;
                case 13: IC.primary_reg.OFF_WDG = index; break;
                case 14: IC.primary_reg.OFF_SLP = index; break;
                case 15: IC.primary_reg.OFF_LSD = index; break;
                case 16: IC.primary_reg.OFF_LOD = index; break;
                case 17: IC.primary_reg.OTP_init = index; break;
                case 18: IC.primary_reg.FORCE_D2A_EN_ON = index; break;
                case 19: IC.primary_reg.FORCE_OSC_EN = index; break;
                case 20: IC.primary_reg.Dout_SPD = index; break;
                case 21: IC.primary_reg.READ_SPD = index; break;
                case 22: IC.primary_reg.ISEL_R = index; break;
                case 23: IC.primary_reg.ISEL_G = index; break;
                case 24: IC.primary_reg.ISEL_B = index; break;
                default: break;
            }
            isPrimaryReady = true;
        }

        private void dataGridView_MISC_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 1) { return; }
            if (!isMiscReady) { return; }


            isMiscReady = false;

            DataGridView grid = sender as DataGridView;

            if (e.ColumnIndex == 0)
            {

                bool firstValue = (bool)grid[0, grid.SelectedCells[0].RowIndex].Value;

                for (int i = 0; i < grid.SelectedCells.Count; i++)
                {
                    int row = grid.SelectedCells[i].RowIndex;
                   
                    switch (row)
                    {
                        case 0: IC.misc.enable[0] = firstValue; break;
                        case 1: IC.misc.enable[0] = firstValue; break;
                        case 2: IC.misc.enable[1] = firstValue; break;
                        case 3: IC.misc.enable[2] = firstValue; break;
                        case 4: IC.misc.enable[2] = firstValue; break;
                        case 5: IC.misc.enable[2] = firstValue; break;
                    }
                }
                updateIC2Form();
            }
            else if (e.ColumnIndex == 2)
            {
                DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)grid[2, e.RowIndex];
                int index = cell.Items.IndexOf(cell.Value);
                switch (e.RowIndex)
                {
                    case 0: IC.misc.ANALOG_SEL = index; break;
                    case 1: IC.misc.CKO_ANALOG_SEL = index; break;
                    case 2: IC.misc.ADC_VREF_VOLT = index; break;
                    case 3: IC.misc.READ_AVG_THM_IDX = index; break;
                    case 4: IC.misc.READ_NOW_THM_IDX = index; break;
                    case 5: IC.misc.READ_USE_THM_IDX = index; break;
                    default: break;
                }
            }

            
            isMiscReady = true;
        }

        private void comboBox_OTP_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_OTP_Type.SelectedIndex == 0) { IC.reg2mem(); }
            updateIC2Form();
            updateOtpRead();
        }
        int lastOTPValue=0;
        private void dataGridView_OTP_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DataGridView datagridview = sender as DataGridView;

                datagridview.BeginEdit(false);

            }
            else if (e.ColumnIndex == 2)
            {  //To send column
                var grid = sender as DataGridView;
                lastOTPValue = int.Parse(grid[2,e.RowIndex].Value.ToString());
            }
        }
        private void dataGridView_OTPADDR_CellValueChanged(object sender, DataGridViewCellEventArgs e) //jackson
        {

            if (e.ColumnIndex == 1) { return; }
            if (!isOTPGridReady) { return; }
            isOTPGridReady = false;

            var grid = sender as DataGridView;
            var cells = grid.SelectedCells;

            if (e.ColumnIndex == 0)
            {
                RT7216Q.OTP dummy = (comboBox_OTP_Type.SelectedIndex == 0) ? IC.otp_memory : IC.otp_reg;

                bool firstValue = (bool) dataGridView_OTP_ADDR[0, dataGridView_OTP_ADDR.SelectedCells[0].RowIndex].Value;

                for (int i = 0; i < dataGridView_OTP_ADDR.SelectedCells.Count; i++)
                {
                    int row = dataGridView_OTP_ADDR.SelectedCells[i].RowIndex;
                    //dataGridView_OTP_ADDR[0, row].Value = firstValue;
                    dummy.en[row] = firstValue;
                }

                //if (comboBox_OTP_Type.SelectedIndex == 0) { IC.otp_memory = dummy; } else { IC.otp_reg = dummy; }
            }
            else if (e.ColumnIndex == 2) //jackson
            {
                int value;
                if (!Int32.TryParse((string)cells[0].Value, out value)) { cells[0].Value = lastOTPValue.ToString(); isOTPGridReady = true; return; }

                RT7216Q.OTP dummy = (comboBox_OTP_Type.SelectedIndex == 0) ? IC.otp_memory : IC.otp_reg;

                for (int i = 0; i < cells.Count; i++)
                {
                    int row = cells[i].RowIndex;
                    dummy[row] = value;
                    grid[2, row].Value = value;
                }
            }

            updateIC2Form();
            isOTPGridReady = true;
        }
        private void dataGridView_OTPVAR_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 1) { return; }
            if (!isOTPGridReady) { return; }
            isOTPGridReady = false;

            var grid = sender as DataGridView;
            var cells = grid.SelectedCells;

            if (e.ColumnIndex == 0)
            {
                RT7216Q.OTP dummy = (comboBox_OTP_Type.SelectedIndex == 0) ? IC.otp_memory : IC.otp_reg;

                bool firstValue = (bool)grid[0, grid.SelectedCells[0].RowIndex].Value;

                for (int i = 0; i < grid.SelectedCells.Count; i++)
                {
                    int row = grid.SelectedCells[i].RowIndex;
                    //grid[0, row].Value = firstValue;//do this in updateIC2Form()
                    //dummy.en[row] = firstValue;
                    switch (row)
                    {
                        case 0: dummy.en[0] = dummy.en[1] = firstValue; break;
                        case 1: dummy.en[5] = dummy.en[2] = dummy.en[3] = dummy.en[4] = firstValue; break;
                        case 2: dummy.en[5] = dummy.en[2] = dummy.en[3] = dummy.en[4] = firstValue; break;
                        case 3: dummy.en[5] = dummy.en[2] = dummy.en[3] = dummy.en[4] = firstValue; break;
                        case 4: dummy.en[5] = dummy.en[2] = dummy.en[3] = dummy.en[4] = firstValue; break;
                        case 5: dummy.en[6] = firstValue; break;
                        case 6: dummy.en[7] = firstValue; break;
                        case 7: dummy.en[11] = firstValue; break;
                        case 8: dummy.en[12] = firstValue; break;
                        case 9: dummy.en[27] = firstValue; break;
                        case 10: dummy.en[28] = firstValue; break;
                        case 11: dummy.en[8] = firstValue; break;
                        case 12: dummy.en[9] = firstValue; break;
                        case 13: dummy.en[10] = firstValue; break;
                        case 14: dummy.en[13] = firstValue; break;
                        case 15: dummy.en[14] = firstValue; break;
                        case 16: dummy.en[15] = firstValue; break;
                        case 17: dummy.en[29]  = firstValue; break;
                        case 18: dummy.en[29] = firstValue; break;//
                        case 19: dummy.en[16] = firstValue; break;
                        case 20: dummy.en[17] = firstValue; break;
                        case 21: dummy.en[18] = firstValue; break;
                        case 22: dummy.en[19] = firstValue; break;
                        case 23: dummy.en[20] = firstValue; break;
                        case 24: dummy.en[21] = firstValue; break;
                        case 25: dummy.en[22] = firstValue; break;
                        case 26: dummy.en[23] = firstValue; break;
                        case 27: dummy.en[24] = firstValue; break;
                        case 28: dummy.en[25] = firstValue; break;
                        case 29: dummy.en[26] = firstValue; break;
                        case 30: dummy.en[30] = firstValue; break;
                        case 31: dummy.en[30] = firstValue; break;
                        case 32: dummy.en[30] = firstValue; break;
                        case 33: dummy.en[31] = firstValue; break;
                        case 34: dummy.en[31] = firstValue; break;
                    }
                }
            }
            else if (e.ColumnIndex == 2)
            {
                int value;
                if (!Int32.TryParse((string)cells[0].Value, out value)) { cells[0].Value = lastOTPValue.ToString(); isOTPGridReady = true; return; }

                RT7216Q.OTP dummy = (comboBox_OTP_Type.SelectedIndex == 0) ? IC.otp_memory : IC.otp_reg;

                for (int i = 0; i < cells.Count; i++)
                {
                    int row = cells[i].RowIndex;
                    switch (row)
                    {
                        case 0: dummy.max_PWM_chip = value; grid[2, row].Value = dummy.max_PWM_chip; break;
                        case 1: dummy.PWM_scalar_R = value; grid[2, row].Value = dummy.PWM_scalar_R; break;
                        case 2: dummy.PWM_scalar_G = value; grid[2, row].Value = dummy.PWM_scalar_G; break;
                        case 3: dummy.PWM_scalar_B = value; grid[2, row].Value = dummy.PWM_scalar_B; break;
                        case 4: dummy.pwm_18bit_mode = value; grid[2, row].Value = dummy.pwm_18bit_mode; break;
                        case 5: dummy.tc_r_base = value; grid[2, row].Value = dummy.tc_r_base; break;
                        case 6: dummy.tc_r_gradient = value; grid[2, row].Value = dummy.tc_r_gradient; break;
                        case 7: dummy.tc_g_base = value; grid[2, row].Value = dummy.tc_g_base; break;
                        case 8: dummy.tc_g_gradient = value; grid[2, row].Value = dummy.tc_g_gradient; break;
                        case 9: dummy.tc_b_base = value; grid[2, row].Value = dummy.tc_g_base; break;
                        case 10: dummy.tc_b_gradient = value; grid[2, row].Value = dummy.tc_b_gradient; break;
                        case 11: dummy.CR_R = value; grid[2, row].Value = dummy.CR_R; break;
                        case 12: dummy.CR_G = value; grid[2, row].Value = dummy.CR_G; break;
                        case 13: dummy.CR_B = value; grid[2, row].Value = dummy.CR_B; break;
                        case 14: dummy.IOUT_trim = value; grid[2, row].Value = dummy.IOUT_trim; break;
                        case 15: dummy.OSC_Freq_Trim = value; grid[2, row].Value = dummy.OSC_Freq_Trim; break;
                        case 16: dummy.thermal_bias = value; grid[2, row].Value = dummy.thermal_bias; break;
                        case 17: dummy.thermal_gradient = value; grid[2, row].Value = dummy.thermal_gradient; break;
                        case 18: dummy.V_THD_Trim = value; grid[2, row].Value = dummy.V_THD_Trim; break;
                        case 19: dummy.tc_fine_thermal_idx[0] = value; grid[2, row].Value = dummy.tc_fine_thermal_idx[0]; break;
                        case 20: dummy.tc_fine_thermal_idx[1] = value; grid[2, row].Value = dummy.tc_fine_thermal_idx[1]; break;
                        case 21: dummy.tc_fine_thermal_idx[2] = value; grid[2, row].Value = dummy.tc_fine_thermal_idx[2]; break;
                        case 22: dummy.tc_fine_thermal_idx[3] = value; grid[2, row].Value = dummy.tc_fine_thermal_idx[3]; break;
                        case 23: dummy.tc_fine_thermal_idx[4] = value; grid[2, row].Value = dummy.tc_fine_thermal_idx[4]; break;
                        case 24: dummy.tc_fine_thermal_idx[5] = value; grid[2, row].Value = dummy.tc_fine_thermal_idx[5]; break;
                        case 25: dummy.tc_fine_thermal_idx[6] = value; grid[2, row].Value = dummy.tc_fine_thermal_idx[6]; break;
                        case 26: dummy.tc_fine_thermal_idx[7] = value; grid[2, row].Value = dummy.tc_fine_thermal_idx[7]; break;
                        case 27: dummy.tc_fine_thermal_idx[8] = value; grid[2, row].Value = dummy.tc_fine_thermal_idx[8]; break;
                        case 28: dummy.tc_fine_thermal_idx[9] = value; grid[2, row].Value = dummy.tc_fine_thermal_idx[9]; break;
                        case 29: dummy.tc_fine_thermal_idx[10] = value; grid[2, row].Value = dummy.tc_fine_thermal_idx[10]; break;
                        case 30: dummy.tc_fine_shift10 = value; grid[2, row].Value = dummy.tc_fine_shift10; break;
                        case 31: dummy.tc_fine_shift9 = value; grid[2, row].Value = dummy.tc_fine_shift9; break;
                        case 32: dummy.tc_fine_shift8 = value; grid[2, row].Value = dummy.tc_fine_shift8; break;
                        case 33: dummy.otp_internal_lock = value; grid[2, row].Value = dummy.otp_internal_lock; break;
                        case 34: dummy.otp_program_lock = value; grid[2, row].Value = dummy.otp_program_lock; break;
                    }
                }
            }


            updateIC2Form();
            isOTPGridReady = true;

        }


        private void dataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if ((grid.CurrentCell.ColumnIndex == 0))
            {
                grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        private void dataGridView_Enter(object sender, EventArgs e)
        {
            currentFocus = sender as DataGridView;

            if(currentFocus != dataGridView_response){dataGridView_response.ClearSelection();        }
            if(currentFocus != dataGridView_PWM){dataGridView_PWM.ClearSelection();             }
            if(currentFocus != dataGridView_PrimaryRegister){dataGridView_PrimaryRegister.ClearSelection(); }
            if(currentFocus != dataGridView_MISC){dataGridView_MISC.ClearSelection();            }
            if(currentFocus != dataGridView_OTP_ADDR){dataGridView_OTP_ADDR.ClearSelection();        }
            if (currentFocus != dataGridView_OTP_Variables) { dataGridView_OTP_Variables.ClearSelection(); }
        }

        private void button_KeyDown(object sender, KeyEventArgs e)
        {
            //TODO
            //currentFocus.Focus();

            //if (currentFocus == dataGridView_response) {  }
            //if (currentFocus == dataGridView_PWM) {  }
            //if (currentFocus == dataGridView_PrimaryRegister) {  }
            //if (currentFocus == dataGridView_MISC) { }
            //if (currentFocus == dataGridView_OTP_ADDR) {  }
            //if (currentFocus == dataGridView_OTP_Variables) {  }
        }



       






    }
}
