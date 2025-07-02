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
    public partial class RT7216N_BD_Control : UserControl
    {
        public RT7216N_BD IC;

        private RT7216N_BD.OTP res_memory;
        private RT7216N_BD.OTP res_register;
        public bool showHexadecimal = false;

        //private RT7216N_BD

        private bool isPWMGridReady = false;
        private bool isPrimaryReady = false;
        private bool isMiscReady = false;
        private bool isOTPGridReady = false;

        private DataGridView currentFocus;

        public RT7216N_BD_Control()
        {
            InitializeComponent();

            IC = new RT7216N_BD();
            res_memory = new RT7216N_BD.OTP(RT7216N_BD.OTP.Type.memory); 
            res_register = new RT7216N_BD.OTP(RT7216N_BD.OTP.Type.register);

            dataGridView_PWM.Rows.Add("R", "0", "0%", "0%" );
            dataGridView_PWM.Rows.Add("G", "0", "0%", "0%" );
            dataGridView_PWM.Rows.Add("B", "0", "0%", "0%" );
            dataGridView_PWM.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            DataGridViewComboBoxCell combobox;
            dataGridView_PrimaryRegister.Rows.Add("F_DET"); combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 0]; dataGridView_PrimaryRegister [0, 0] .ToolTipText = "Detection function select"; combobox.Items.Add("0 (None)"); combobox.Items.Add("1 (LSD)"); combobox.Items.Add("2 (LOD)"); combobox.Items.Add("3 (Temperature)");
            dataGridView_PrimaryRegister.Rows.Add("F_DETL"); combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 1]; dataGridView_PrimaryRegister[0, 1].ToolTipText = "LOD, LSD level select"; combobox.Items.Add("0 (LOD=0.2  LSD=VDD-0.2)"); combobox.Items.Add("1 (LOD=0.4  LSD=VDD-0.4)"); combobox.Items.Add("2 (LOD=0.6  LSD=VDD-0.6)"); combobox.Items.Add("3 (LOD=0.8  LSD=VDD-0.8)");
            dataGridView_PrimaryRegister.Rows.Add("F_IOHL"); combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 2]; dataGridView_PrimaryRegister[0, 2].ToolTipText = "Output buffer driving current select"; combobox.Items.Add("0 lowest"); combobox.Items.Add("1 low"); combobox.Items.Add("2 high"); combobox.Items.Add("3 highest");
            dataGridView_PrimaryRegister.Rows.Add("F_TRF"); combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 3]; dataGridView_PrimaryRegister[0, 3].ToolTipText = "Channel IOUT slew rate"; combobox.Items.Add("0 slow"); combobox.Items.Add("1 fast");
            dataGridView_PrimaryRegister.Rows.Add("18bit_mode"); combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 4]; dataGridView_PrimaryRegister[0, 4].ToolTipText = "PWM period x4"; combobox.Items.Add("0 Disable"); combobox.Items.Add("1 Enable");
            dataGridView_PrimaryRegister.Rows.Add("TEMP_RUN_AVG_EN"); combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 5]; dataGridView_PrimaryRegister[0, 5].ToolTipText = "Running average function for temperature compensation"; combobox.Items.Add("0 Disable"); combobox.Items.Add("1 Enable");
            dataGridView_PrimaryRegister.Rows.Add("TEMP_UPDATE_FREQ"); combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 6]; dataGridView_PrimaryRegister[0, 6].ToolTipText = "Running average data update frequency"; combobox.Items.Add("0 (1-frame)"); combobox.Items.Add("1 (4-frame)"); combobox.Items.Add("2 (8-frame)"); combobox.Items.Add("3 (16-frame)"); combobox.Items.Add("4 (32-frame)"); combobox.Items.Add("5 (64-frame)"); combobox.Items.Add("6 (128-frame)"); combobox.Items.Add("7 (256-frame)");
            dataGridView_PrimaryRegister.Rows.Add("TC_R_EN"); combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 7]; dataGridView_PrimaryRegister[0, 7].ToolTipText = "Temperature compensation for OUTR"; combobox.Items.Add("0 Disable"); combobox.Items.Add("1 Enable");
            dataGridView_PrimaryRegister.Rows.Add("F_SRST"); combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 8]; dataGridView_PrimaryRegister[0, 8].ToolTipText = "Software reset display"; combobox.Items.Add("0 Normal Display"); combobox.Items.Add("1 Reset Display");
            dataGridView_PrimaryRegister.Rows.Add("OFF_TSD"); combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 9]; dataGridView_PrimaryRegister[0, 9].ToolTipText = "Thermal shutdown function"; combobox.Items.Add("0 Enable"); combobox.Items.Add("1 Disable");
            dataGridView_PrimaryRegister.Rows.Add("OFF_WDG"); combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 10]; dataGridView_PrimaryRegister[0, 10].ToolTipText = "Watchdog function"; combobox.Items.Add("0 Enable"); combobox.Items.Add("1 Disable");
            dataGridView_PrimaryRegister.Rows.Add("OFF_SLP"); combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 11]; dataGridView_PrimaryRegister[0, 11].ToolTipText = "Sleep function"; combobox.Items.Add("0 Enable"); combobox.Items.Add("1 Disable");
            dataGridView_PrimaryRegister.Rows.Add("OFF_LSD"); combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 12]; dataGridView_PrimaryRegister[0, 12].ToolTipText = "Short mark function"; combobox.Items.Add("0 Enable"); combobox.Items.Add("1 Disable");
            dataGridView_PrimaryRegister.Rows.Add("OFF_LOD"); combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 13]; dataGridView_PrimaryRegister[0, 13].ToolTipText = "Open mark function"; combobox.Items.Add("0 Enable"); combobox.Items.Add("1 Disable");
            dataGridView_PrimaryRegister.Rows.Add("OFF_UVLO"); combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 14]; dataGridView_PrimaryRegister[0, 14].ToolTipText = "Under Voltage Lock Out function"; combobox.Items.Add("0 Enable"); combobox.Items.Add("1 Disable");
            dataGridView_PrimaryRegister.Rows.Add("FORCE_SLP_UVLO"); combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 15]; dataGridView_PrimaryRegister[0, 15].ToolTipText = ""; combobox.Items.Add("0 Disable"); combobox.Items.Add("1 Enable");
            dataGridView_PrimaryRegister.Rows.Add("FORCE_CH_PW_ON"); combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 16]; dataGridView_PrimaryRegister[0, 16].ToolTipText = ""; combobox.Items.Add("0 Disable"); combobox.Items.Add("1 Enable");
            dataGridView_PrimaryRegister.Rows.Add("READ_SPD"); combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 17]; dataGridView_PrimaryRegister[0, 17].ToolTipText = "Report frequency select"; combobox.Items.Add("0 (20MHz)"); combobox.Items.Add("1 (10MHz)"); combobox.Items.Add("2 (5MHz)"); combobox.Items.Add("3 (2.5MHz)"); combobox.Items.Add("4 (1.25MHz)"); combobox.Items.Add("others (0.625MHz)"); 
            dataGridView_PrimaryRegister.Rows.Add("GBC"); combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 18]; dataGridView_PrimaryRegister[0, 18].ToolTipText = "Global Brightness Control"; for (int i = 0; i <= 63; i++) { combobox.Items.Add(String.Format("{0}  ({1:N1}% brightness)", i, (100.0 - ((double)(63 - i)) * 1.432))); }

            dataGridView_MISC.Rows.Add(false, "TC_G_base", ""); dataGridView_MISC[1, 0].ToolTipText = "";
            dataGridView_MISC.Rows.Add(false, "TC_G_gradient", ""); dataGridView_MISC[1, 1].ToolTipText = "";
            dataGridView_MISC.Rows.Add(false, "TC_G_en", ""); dataGridView_MISC[1, 2].ToolTipText = "";
            dataGridView_MISC.Rows.Add(false, "TC_B_base", ""); dataGridView_MISC[1, 3].ToolTipText = "";
            dataGridView_MISC.Rows.Add(false, "TC_B_gradient", ""); dataGridView_MISC[1, 4].ToolTipText = "";
            dataGridView_MISC.Rows.Add(false, "TC_B_en", ""); dataGridView_MISC[1, 5].ToolTipText = "";
            dataGridView_MISC.Rows.Add(false, "read_avg_thm_idx", ""); dataGridView_MISC[1, 6].ToolTipText = "";

            for (int i = 0; i < 32; i++) { dataGridView_OTP_ADDR.Rows.Add(false, i.ToString(), 0, ""); }

            dataGridView_OTP_Variables.Rows.Add(false, "max_PWM_chip", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "PWM_scalar_R", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "PWM_scalar_G", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "PWM_scalar_B", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "CR_R", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "CR_G", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "CR_B", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "tc_r_base", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "tc_r_gradient", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "IOUT_trim", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "thermal_gradient", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "V_THD_Trim", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "OSC_Freq_Trim", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "thermal_bias", 0, "");
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
            dataGridView_OTP_Variables.Rows.Add(false, "tc_fine_thermal_idx11", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "_tc_fine_shift3_0", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "_tc_fine_shift7_4", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "_tc_fine_shift11_8", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "otp_internal_lock", 0, "");
            dataGridView_OTP_Variables.Rows.Add(false, "otp_program_lock", 0, "");

            dataGridView_OTP_ADDR.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            dataGridView_response.Rows.Add("", "", "");
        }


        private void RT7216N_BD_Control_Load(object sender, EventArgs e)
        {
            isPWMGridReady = true;
            isPrimaryReady = true;
            isMiscReady = true;
            isOTPGridReady = true;

            dataGridView_Enter(dataGridView_PWM, null);

            comboBox_OTP_Type.SelectedIndex = 1;
        }
        public void updateOtpRead(RT7216N_BD.OTP res){
            if (res.type == RT7216N_BD.OTP.Type.memory) {  res_memory = res; }
            else{ res_register = res;}
            updateIC2Form();
        }
        public void updateIC2Form()
        {
            isPWMGridReady = false;
            dataGridView_PWM[1, 0].Value = IC.pwm.R;
            dataGridView_PWM[1, 1].Value = IC.pwm.G;
            dataGridView_PWM[1, 2].Value = IC.pwm.B;
            isPWMGridReady = true;

            DataGridViewComboBoxCell combobox;
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 0]; combobox.Value = combobox.Items[IC.primary_reg.DET];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 1]; combobox.Value = combobox.Items[IC.primary_reg.DETL];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 2]; combobox.Value = combobox.Items[IC.primary_reg.IOHL];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 3]; combobox.Value = combobox.Items[IC.primary_reg.TRF];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 4]; combobox.Value = combobox.Items[IC.primary_reg.pwm_18bit_mode];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 5]; combobox.Value = combobox.Items[IC.primary_reg.temp_run_avg_en];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 6]; combobox.Value = combobox.Items[IC.primary_reg.temp_update_freq];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 7]; combobox.Value = combobox.Items[IC.primary_reg.tc_r_en];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 8]; combobox.Value = combobox.Items[IC.primary_reg.SRST];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 9]; combobox.Value = combobox.Items[IC.primary_reg.OFF_TSD];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 10]; combobox.Value = combobox.Items[IC.primary_reg.OFF_WDG];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 11]; combobox.Value = combobox.Items[IC.primary_reg.OFF_SLP];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 12]; combobox.Value = combobox.Items[IC.primary_reg.OFF_LSD];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 13]; combobox.Value = combobox.Items[IC.primary_reg.OFF_LOD];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 14]; combobox.Value = combobox.Items[IC.primary_reg.OFF_UVLO];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 15]; combobox.Value = combobox.Items[IC.primary_reg.FORCE_SLP_UVLO];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 16]; combobox.Value = combobox.Items[IC.primary_reg.FORCE_CH_PW_ON];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 17]; combobox.Value = combobox.Items[IC.primary_reg.READ_SPD];
            combobox = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 18]; combobox.Value = combobox.Items[IC.primary_reg.GBC];

            isMiscReady = false;
            dataGridView_MISC[2, 0].Value = (IC.misc.tc_g_base);
            dataGridView_MISC[2, 1].Value = (IC.misc.tc_g_gradient);
            dataGridView_MISC[2, 2].Value = (IC.misc.tc_g_en);
            dataGridView_MISC[2, 3].Value = (IC.misc.tc_b_base);
            dataGridView_MISC[2, 4].Value = (IC.misc.tc_b_gradient);
            dataGridView_MISC[2, 5].Value = (IC.misc.tc_b_en);
            dataGridView_MISC[2, 6].Value = (IC.misc.read_avg_thm_idx);

            dataGridView_MISC[0, 0].Value = (IC.misc.enable[0]);
            dataGridView_MISC[0, 1].Value = (IC.misc.enable[0]);
            dataGridView_MISC[0, 2].Value = (IC.misc.enable[0]);
            dataGridView_MISC[0, 3].Value = (IC.misc.enable[1]);
            dataGridView_MISC[0, 4].Value = (IC.misc.enable[1]);
            dataGridView_MISC[0, 5].Value = (IC.misc.enable[1]);
            dataGridView_MISC[0, 6].Value = (IC.misc.enable[2]);
            isMiscReady = true;

            isOTPGridReady = false;
            if (comboBox_OTP_Type.SelectedIndex == 0) // memory
            {
                for (int i = 0; i < 32; i++)
                {
                    dataGridView_OTP_ADDR[2, i].Value = IC.otp_memory[i];
                    dataGridView_OTP_ADDR[0, i].Value = IC.otp_memory.en[i];
                    dataGridView_OTP_ADDR[3, i].Value = res_memory[i]; 
                }
                dataGridView_OTP_Variables[2, 0].Value = IC.otp_memory.max_PWM_chip;
                dataGridView_OTP_Variables[2, 1].Value = IC.otp_memory.PWM_scalar_R;
                dataGridView_OTP_Variables[2, 2].Value = IC.otp_memory.PWM_scalar_G;
                dataGridView_OTP_Variables[2, 3].Value = IC.otp_memory.PWM_scalar_B;
                dataGridView_OTP_Variables[2, 4].Value = IC.otp_memory.CR_R;
                dataGridView_OTP_Variables[2, 5].Value = IC.otp_memory.CR_G;
                dataGridView_OTP_Variables[2, 6].Value = IC.otp_memory.CR_B;
                dataGridView_OTP_Variables[2, 7].Value = IC.otp_memory.tc_r_base;
                dataGridView_OTP_Variables[2, 8].Value = IC.otp_memory.tc_r_gradient;
                dataGridView_OTP_Variables[2, 9].Value = IC.otp_memory.IOUT_trim;
                dataGridView_OTP_Variables[2, 10].Value = IC.otp_memory.thermal_gradient;
                dataGridView_OTP_Variables[2, 11].Value = IC.otp_memory.V_THD_Trim;
                dataGridView_OTP_Variables[2, 12].Value = IC.otp_memory.OSC_Freq_Trim;
                dataGridView_OTP_Variables[2, 13].Value = IC.otp_memory.thermal_bias;
                dataGridView_OTP_Variables[2, 14].Value = IC.otp_memory.tc_fine_thermal_idx[0];
                dataGridView_OTP_Variables[2, 15].Value = IC.otp_memory.tc_fine_thermal_idx[1];
                dataGridView_OTP_Variables[2, 16].Value = IC.otp_memory.tc_fine_thermal_idx[2];
                dataGridView_OTP_Variables[2, 17].Value = IC.otp_memory.tc_fine_thermal_idx[3];
                dataGridView_OTP_Variables[2, 18].Value = IC.otp_memory.tc_fine_thermal_idx[4];
                dataGridView_OTP_Variables[2, 19].Value = IC.otp_memory.tc_fine_thermal_idx[5];
                dataGridView_OTP_Variables[2, 20].Value = IC.otp_memory.tc_fine_thermal_idx[6];
                dataGridView_OTP_Variables[2, 21].Value = IC.otp_memory.tc_fine_thermal_idx[7];
                dataGridView_OTP_Variables[2, 22].Value = IC.otp_memory.tc_fine_thermal_idx[8];
                dataGridView_OTP_Variables[2, 23].Value = IC.otp_memory.tc_fine_thermal_idx[9];
                dataGridView_OTP_Variables[2, 24].Value = IC.otp_memory.tc_fine_thermal_idx[10];
                dataGridView_OTP_Variables[2, 25].Value = IC.otp_memory.tc_fine_thermal_idx[11];
                dataGridView_OTP_Variables[2, 26].Value = IC.otp_memory.tc_fine_shift3_0;
                dataGridView_OTP_Variables[2, 27].Value = IC.otp_memory.tc_fine_shift7_4;
                dataGridView_OTP_Variables[2, 28].Value = IC.otp_memory.tc_fine_shift11_8;
                dataGridView_OTP_Variables[2, 29].Value = IC.otp_memory.otp_internal_lock;
                dataGridView_OTP_Variables[2, 30].Value = IC.otp_memory.otp_program_lock;

                dataGridView_OTP_Variables[3, 0].Value = res_memory.max_PWM_chip;
                dataGridView_OTP_Variables[3, 1].Value = res_memory.PWM_scalar_R;
                dataGridView_OTP_Variables[3, 2].Value = res_memory.PWM_scalar_G;
                dataGridView_OTP_Variables[3, 3].Value = res_memory.PWM_scalar_B;
                dataGridView_OTP_Variables[3, 4].Value = res_memory.CR_R;
                dataGridView_OTP_Variables[3, 5].Value = res_memory.CR_G;
                dataGridView_OTP_Variables[3, 6].Value = res_memory.CR_B;
                dataGridView_OTP_Variables[3, 7].Value = res_memory.tc_r_base;
                dataGridView_OTP_Variables[3, 8].Value = res_memory.tc_r_gradient;
                dataGridView_OTP_Variables[3, 9].Value = res_memory.IOUT_trim;
                dataGridView_OTP_Variables[3, 10].Value = res_memory.thermal_gradient;
                dataGridView_OTP_Variables[3, 11].Value = res_memory.V_THD_Trim;
                dataGridView_OTP_Variables[3, 12].Value = res_memory.OSC_Freq_Trim;
                dataGridView_OTP_Variables[3, 13].Value = res_memory.thermal_bias;
                dataGridView_OTP_Variables[3, 14].Value = res_memory.tc_fine_thermal_idx[0];
                dataGridView_OTP_Variables[3, 15].Value = res_memory.tc_fine_thermal_idx[1];
                dataGridView_OTP_Variables[3, 16].Value = res_memory.tc_fine_thermal_idx[2];
                dataGridView_OTP_Variables[3, 17].Value = res_memory.tc_fine_thermal_idx[3];
                dataGridView_OTP_Variables[3, 18].Value = res_memory.tc_fine_thermal_idx[4];
                dataGridView_OTP_Variables[3, 19].Value = res_memory.tc_fine_thermal_idx[5];
                dataGridView_OTP_Variables[3, 20].Value = res_memory.tc_fine_thermal_idx[6];
                dataGridView_OTP_Variables[3, 21].Value = res_memory.tc_fine_thermal_idx[7];
                dataGridView_OTP_Variables[3, 22].Value = res_memory.tc_fine_thermal_idx[8];
                dataGridView_OTP_Variables[3, 23].Value = res_memory.tc_fine_thermal_idx[9];
                dataGridView_OTP_Variables[3, 24].Value = res_memory.tc_fine_thermal_idx[10];
                dataGridView_OTP_Variables[3, 25].Value = res_memory.tc_fine_thermal_idx[11];
                dataGridView_OTP_Variables[3, 26].Value = res_memory.tc_fine_shift3_0;
                dataGridView_OTP_Variables[3, 27].Value = res_memory.tc_fine_shift7_4;
                dataGridView_OTP_Variables[3, 28].Value = res_memory.tc_fine_shift11_8;
                dataGridView_OTP_Variables[3, 29].Value = res_memory.otp_internal_lock;
                dataGridView_OTP_Variables[3, 30].Value = res_memory.otp_program_lock;
                
            }
            else
            {
                for (int i = 0; i < 32; i++)
                {
                    dataGridView_OTP_ADDR[2, i].Value = IC.otp_reg[i];
                    dataGridView_OTP_ADDR[0, i].Value = IC.otp_reg.en[i];
                    dataGridView_OTP_ADDR[3, i].Value = res_register[i]; 
                }

                dataGridView_OTP_Variables[2, 0].Value =  IC.otp_reg.max_PWM_chip;
                dataGridView_OTP_Variables[2, 1].Value = IC.otp_reg.PWM_scalar_R;
                dataGridView_OTP_Variables[2, 2].Value = IC.otp_reg.PWM_scalar_G;
                dataGridView_OTP_Variables[2, 3].Value = IC.otp_reg.PWM_scalar_B;
                dataGridView_OTP_Variables[2, 4].Value = IC.otp_reg.CR_R;
                dataGridView_OTP_Variables[2, 5].Value = IC.otp_reg.CR_G;
                dataGridView_OTP_Variables[2, 6].Value = IC.otp_reg.CR_B;
                dataGridView_OTP_Variables[2, 7].Value = IC.otp_reg.tc_r_base;
                dataGridView_OTP_Variables[2, 8].Value = IC.otp_reg.tc_r_gradient;
                dataGridView_OTP_Variables[2, 9].Value = IC.otp_reg.IOUT_trim;
                dataGridView_OTP_Variables[2, 10].Value = IC.otp_reg.thermal_gradient;
                dataGridView_OTP_Variables[2, 11].Value = IC.otp_reg.V_THD_Trim;
                dataGridView_OTP_Variables[2, 12].Value = IC.otp_reg.OSC_Freq_Trim;
                dataGridView_OTP_Variables[2, 13].Value = IC.otp_reg.thermal_bias;
                dataGridView_OTP_Variables[2, 14].Value = IC.otp_reg.tc_fine_thermal_idx[0];
                dataGridView_OTP_Variables[2, 15].Value = IC.otp_reg.tc_fine_thermal_idx[1];
                dataGridView_OTP_Variables[2, 16].Value = IC.otp_reg.tc_fine_thermal_idx[2];
                dataGridView_OTP_Variables[2, 17].Value = IC.otp_reg.tc_fine_thermal_idx[3];
                dataGridView_OTP_Variables[2, 18].Value = IC.otp_reg.tc_fine_thermal_idx[4];
                dataGridView_OTP_Variables[2, 19].Value = IC.otp_reg.tc_fine_thermal_idx[5];
                dataGridView_OTP_Variables[2, 20].Value = IC.otp_reg.tc_fine_thermal_idx[6];
                dataGridView_OTP_Variables[2, 21].Value = IC.otp_reg.tc_fine_thermal_idx[7];
                dataGridView_OTP_Variables[2, 22].Value = IC.otp_reg.tc_fine_thermal_idx[8];
                dataGridView_OTP_Variables[2, 23].Value = IC.otp_reg.tc_fine_thermal_idx[9];
                dataGridView_OTP_Variables[2, 24].Value = IC.otp_reg.tc_fine_thermal_idx[10];
                dataGridView_OTP_Variables[2, 25].Value = IC.otp_reg.tc_fine_thermal_idx[11];
                dataGridView_OTP_Variables[2, 26].Value = IC.otp_reg.tc_fine_shift3_0;
                dataGridView_OTP_Variables[2, 27].Value = IC.otp_reg.tc_fine_shift7_4;
                dataGridView_OTP_Variables[2, 28].Value = IC.otp_reg.tc_fine_shift11_8;
                dataGridView_OTP_Variables[2, 29].Value = IC.otp_reg.otp_internal_lock;
                dataGridView_OTP_Variables[2, 30].Value = IC.otp_reg.otp_program_lock;

                dataGridView_OTP_Variables[3, 0].Value = res_register.max_PWM_chip;
                dataGridView_OTP_Variables[3, 1].Value = res_register.PWM_scalar_R;
                dataGridView_OTP_Variables[3, 2].Value = res_register.PWM_scalar_G;
                dataGridView_OTP_Variables[3, 3].Value = res_register.PWM_scalar_B;
                dataGridView_OTP_Variables[3, 4].Value = res_register.CR_R;
                dataGridView_OTP_Variables[3, 5].Value = res_register.CR_G;
                dataGridView_OTP_Variables[3, 6].Value = res_register.CR_B;
                dataGridView_OTP_Variables[3, 7].Value = res_register.tc_r_base;
                dataGridView_OTP_Variables[3, 8].Value = res_register.tc_r_gradient;
                dataGridView_OTP_Variables[3, 9].Value = res_register.IOUT_trim;
                dataGridView_OTP_Variables[3, 10].Value = res_register.thermal_gradient;
                dataGridView_OTP_Variables[3, 11].Value = res_register.V_THD_Trim;
                dataGridView_OTP_Variables[3, 12].Value = res_register.OSC_Freq_Trim;
                dataGridView_OTP_Variables[3, 13].Value = res_register.thermal_bias;
                dataGridView_OTP_Variables[3, 14].Value = res_register.tc_fine_thermal_idx[0];
                dataGridView_OTP_Variables[3, 15].Value = res_register.tc_fine_thermal_idx[1];
                dataGridView_OTP_Variables[3, 16].Value = res_register.tc_fine_thermal_idx[2];
                dataGridView_OTP_Variables[3, 17].Value = res_register.tc_fine_thermal_idx[3];
                dataGridView_OTP_Variables[3, 18].Value = res_register.tc_fine_thermal_idx[4];
                dataGridView_OTP_Variables[3, 19].Value = res_register.tc_fine_thermal_idx[5];
                dataGridView_OTP_Variables[3, 20].Value = res_register.tc_fine_thermal_idx[6];
                dataGridView_OTP_Variables[3, 21].Value = res_register.tc_fine_thermal_idx[7];
                dataGridView_OTP_Variables[3, 22].Value = res_register.tc_fine_thermal_idx[8];
                dataGridView_OTP_Variables[3, 23].Value = res_register.tc_fine_thermal_idx[9];
                dataGridView_OTP_Variables[3, 24].Value = res_register.tc_fine_thermal_idx[10];
                dataGridView_OTP_Variables[3, 25].Value = res_register.tc_fine_thermal_idx[11];
                dataGridView_OTP_Variables[3, 26].Value = res_register.tc_fine_shift3_0;
                dataGridView_OTP_Variables[3, 27].Value = res_register.tc_fine_shift7_4;
                dataGridView_OTP_Variables[3, 28].Value = res_register.tc_fine_shift11_8;
                dataGridView_OTP_Variables[3, 29].Value = res_register.otp_internal_lock;
                dataGridView_OTP_Variables[3, 30].Value = res_register.otp_program_lock;
            }
            isOTPGridReady = true;
        }
        //public void updateForm2IC()
        //{
        //    IC.pwm.R = int.Parse(dataGridView_PWM[1, 0].Value.ToString());
        //    IC.pwm.G = int.Parse(dataGridView_PWM[1, 1].Value.ToString());
        //    IC.pwm.B = int.Parse(dataGridView_PWM[1, 2].Value.ToString());

        //    DataGridViewComboBoxCell cell;
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 0]; IC.primary_reg.DET = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 1]; IC.primary_reg.DETL = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 2]; IC.primary_reg.IOHL = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 3]; IC.primary_reg.TRF = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 4]; IC.primary_reg.PWM_MODE_EN = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 5]; IC.primary_reg.THM_MODE_EN = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 6]; IC.primary_reg.temp_run_avg_en = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 7]; IC.primary_reg.temp_update_freq = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 8]; IC.primary_reg.tc_r_en = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 9]; IC.primary_reg.tc_g_en = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 10]; IC.primary_reg.tc_b_en = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 11]; IC.primary_reg.SRST = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 12]; IC.primary_reg.OFF_TSD = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 13]; IC.primary_reg.OFF_WDG = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 14]; IC.primary_reg.OFF_SLP = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 15]; IC.primary_reg.OFF_LSD = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 16]; IC.primary_reg.OFF_LOD = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 17]; IC.primary_reg.OTP_init = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 18]; IC.primary_reg.FORCE_D2A_EN_ON = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 19]; IC.primary_reg.FORCE_OSC_EN = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 20]; IC.primary_reg.Dout_SPD = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 21]; IC.primary_reg.READ_SPD = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 22]; IC.primary_reg.ISEL_R = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 23]; IC.primary_reg.ISEL_G = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_PrimaryRegister[1, 24]; IC.primary_reg.ISEL_B = cell.Items.IndexOf(cell.Value);

        //    cell = (DataGridViewComboBoxCell)dataGridView_MISC[2, 0]; IC.misc.ANALOG_SEL = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_MISC[2, 1]; IC.misc.CKO_ANALOG_SEL = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_MISC[2, 2]; IC.misc.ADC_VREF_VOLT = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_MISC[2, 3]; IC.misc.READ_AVG_THM_IDX = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_MISC[2, 4]; IC.misc.READ_NOW_THM_IDX = cell.Items.IndexOf(cell.Value);
        //    cell = (DataGridViewComboBoxCell)dataGridView_MISC[2, 5]; IC.misc.READ_USE_THM_IDX = cell.Items.IndexOf(cell.Value);

        //    if (comboBox_OTP_Type.SelectedIndex == 0) //memory
        //    {
        //        for (int i = 0; i < 32; i++) { IC.otp_memory[i] = int.Parse(dataGridView_OTP_ADDR[2, i].Value.ToString()); }
        //    }
        //    else //register
        //    {
        //        for (int i = 0; i < 32; i++) { IC.otp_reg[i] = int.Parse(dataGridView_OTP_ADDR[2, i].Value.ToString()); }
        //    }
        //}


        public void updateResponse(int type, byte res)
        {
            dataGridView_response[1, 0].Value = res;
            string OUT = "";
            switch (type)
            {
                case 1: dataGridView_response[0, 0].Value = "LSD";
                    dataGridView_response[1, 0].Value = ((res >> 4) & 0x07);
                    if (((res>>4) & 0x07) == 0) { dataGridView_response[2, 0].Value = "None is Short Circuit"; break; }
                    
                    if (((res>>4) & 0x01) == 0x01) { OUT  = "R"; }
                    if (((res>>4) & 0x02) == 0x02) { OUT += "G"; }
                    if (((res>>4) & 0x04) == 0x04) { OUT += "B"; }
                    dataGridView_response[2, 0].Value = OUT + " is Short Circuit";
                    break;
                case 2: dataGridView_response[0, 0].Value = "LOD";
                    dataGridView_response[1, 0].Value = (res & 0x07);
                    if ((res & 0x07) == 0) { dataGridView_response[2, 0].Value = "None is Open Circuit"; break; }

                    if ((res & 0x01) == 0x01) { OUT  = "R"; }
                    if ((res & 0x02) == 0x02) { OUT += "G"; }
                    if ((res & 0x04) == 0x04) { OUT += "B"; }
                    dataGridView_response[2, 0].Value = OUT + " is Open Circuit";
                    break;
                case 3: dataGridView_response[0, 0].Value = "Temperature"; dataGridView_response[1, 0].Value = res; dataGridView_response[2, 0].Value = RT7216N_BD.PRIMARY_REGISTER.idx2temperature(res).ToString() + "' C    tc_idx=" + RT7216N_BD.PRIMARY_REGISTER.idx2tc_idx(res).ToString(); break;
                case 4: dataGridView_response[0, 0].Value = "Average Temp"; dataGridView_response[1, 0].Value = res; dataGridView_response[2, 0].Value = RT7216N_BD.PRIMARY_REGISTER.idx2temperature(res).ToString() + "' C    tc_idx=" + RT7216N_BD.PRIMARY_REGISTER.idx2tc_idx(res).ToString(); break;
                default: break;
            }
        }
        //private void button_Click(object sender, EventArgs e)
        //{
        //    // updateForm2IC();
        //}

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

        //for primary
        private void dataGridView_ComboBox_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) { return; }

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
                case 4: IC.primary_reg.pwm_18bit_mode = index; break;
                case 5: IC.primary_reg.temp_run_avg_en = index; break;
                case 6: IC.primary_reg.temp_update_freq = index; break;
                case 7: IC.primary_reg.tc_r_en = index; break;
                case 8: IC.primary_reg.SRST = index; break;
                case 9: IC.primary_reg.OFF_TSD = index; break;
                case 10: IC.primary_reg.OFF_WDG = index; break;
                case 11: IC.primary_reg.OFF_SLP = index; break;
                case 12: IC.primary_reg.OFF_LSD = index; break;
                case 13: IC.primary_reg.OFF_LOD = index; break;
                case 14: IC.primary_reg.OFF_UVLO = index; break;
                case 15: IC.primary_reg.FORCE_SLP_UVLO = index; break;
                case 16: IC.primary_reg.FORCE_CH_PW_ON = index; break;
                case 17: IC.primary_reg.READ_SPD = index; break;
                case 18: IC.primary_reg.GBC = index; break;
                default: break;
            }
            isPrimaryReady = true;
        }


        int lastMISCValue;
        private void dataGridView_MISC_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {  
                var cells = dataGridView_MISC.SelectedCells;
                if (cells.Count == 0) { return; }
                lastMISCValue = int.Parse(cells[0].Value.ToString());
            }
        }
        private void dataGridView_MISC_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (!isMiscReady) { return; }

            var grid = sender as DataGridView;
            var cells = grid.SelectedCells;

            if (cells.Count == 0) { return; }

            isMiscReady = false;
            if(e.ColumnIndex == 0){
                bool firstValue = (bool)grid[0, grid.SelectedCells[0].RowIndex].Value;

                for (int i = 0; i < grid.SelectedCells.Count; i++)
                {
                    int row = grid.SelectedCells[i].RowIndex;

                    switch (row)
                    {
                        case 0: IC.misc.enable[0] = firstValue; break;
                        case 1: IC.misc.enable[0] = firstValue; break;
                        case 2: IC.misc.enable[0] = firstValue; break;
                        case 3: IC.misc.enable[1] = firstValue; break;
                        case 4: IC.misc.enable[1] = firstValue; break;
                        case 5: IC.misc.enable[1] = firstValue; break;
                        case 6: IC.misc.enable[2] = firstValue; break;
                    }
                }
            }
            else if (e.ColumnIndex == 2)
            {
                int value;
                
                if (!Int32.TryParse((string)cells[0].Value, out value))
                {
                    cells[0].Value = lastMISCValue.ToString();
                    isMiscReady = true;
                    return;
                }

                for (int i = 0; i < cells.Count; i++)
                {
                    int row = cells[i].RowIndex;

                    switch (row)
                    {
                        case 0: IC.misc.tc_g_base = value; break;
                        case 1: IC.misc.tc_g_gradient = value; break;
                        case 2: IC.misc.tc_g_en = value; break;
                        case 3: IC.misc.tc_b_base = value; break;
                        case 4: IC.misc.tc_b_gradient = value; break;
                        case 5: IC.misc.tc_b_en = value; break;
                        case 6: IC.misc.read_avg_thm_idx = value; break;
                        default: break;
                    }
                }
            }

            updateIC2Form();
            isMiscReady = true;

        }

        private void comboBox_OTP_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateIC2Form();
        }

        int lastOTPValue = 0;
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
                lastOTPValue = int.Parse(grid[2, e.RowIndex].Value.ToString());
            }
        }
        private void dataGridView_OTPADDR_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 1) { return; }
            if (!isOTPGridReady) { return; }
            isOTPGridReady = false;

            var grid = sender as DataGridView;
            var cells = grid.SelectedCells;

            if (e.ColumnIndex == 0)
            {
                RT7216N_BD.OTP dummy = (comboBox_OTP_Type.SelectedIndex == 0) ? IC.otp_memory : IC.otp_reg;

                bool firstValue = (bool)dataGridView_OTP_ADDR[0, dataGridView_OTP_ADDR.SelectedCells[0].RowIndex].Value;

                for (int i = 0; i < dataGridView_OTP_ADDR.SelectedCells.Count; i++)
                {
                    int row = dataGridView_OTP_ADDR.SelectedCells[i].RowIndex;
                    //dataGridView_OTP_ADDR[0, row].Value = firstValue;
                    dummy.en[row] = firstValue;
                }

                //if (comboBox_OTP_Type.SelectedIndex == 0) { IC.otp_memory = dummy; } else { IC.otp_reg = dummy; }
            }
            else if (e.ColumnIndex == 2)
            {
                int value;
                if (!Int32.TryParse((string)cells[0].Value, out value)) { cells[0].Value = lastOTPValue.ToString(); isOTPGridReady = true; return; }

                RT7216N_BD.OTP dummy = (comboBox_OTP_Type.SelectedIndex == 0) ? IC.otp_memory : IC.otp_reg;

                for (int i = 0; i < cells.Count; i++)
                {
                    int row = cells[i].RowIndex;
                    dummy[row] = value;
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
                RT7216N_BD.OTP dummy = (comboBox_OTP_Type.SelectedIndex == 0) ? IC.otp_memory : IC.otp_reg;

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
                        case 17: dummy.en[29] = firstValue; break;
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

                RT7216N_BD.OTP dummy = (comboBox_OTP_Type.SelectedIndex == 0) ? IC.otp_memory : IC.otp_reg;

                for (int i = 0; i < cells.Count; i++)
                {
                    int row = cells[i].RowIndex;
                    switch (row)
                    {
                        case 0: dummy.max_PWM_chip = value;             break;//grid[2, row].Value = dummy.max_PWM_chip; break;
                        case 1: dummy.PWM_scalar_R = value;             break;//grid[2, row].Value = dummy.PWM_scalar_R; break;
                        case 2: dummy.PWM_scalar_G = value;             break;//grid[2, row].Value = dummy.PWM_scalar_G; break;
                        case 3: dummy.PWM_scalar_B = value;             break;//grid[2, row].Value = dummy.PWM_scalar_B; break;
                        case 4: dummy.CR_R = value;           break;//grid[2, row].Value = dummy.pwm_18bit_mode; break;
                        case 5: dummy.CR_G = value;                break;//grid[2, row].Value = dummy.tc_r_base; break;
                        case 6: dummy.CR_B = value;            break;//grid[2, row].Value = dummy.tc_r_gradient; break;
                        case 7: dummy.tc_r_base = value;                break;//grid[2, row].Value = dummy.tc_g_base; break;
                        case 8: dummy.tc_r_gradient = value;            break;//grid[2, row].Value = dummy.tc_g_gradient; break;
                        case 9: dummy. IOUT_trim       = value;                break;//grid[2, row].Value = dummy.tc_g_base; break;
                        case 10: dummy.thermal_gradient = value;           break;//grid[2, row].Value = dummy.tc_b_gradient; break;
                        case 11: dummy.V_THD_Trim       = value;                    break;//grid[2, row].Value = dummy.CR_R; break;
                        case 12: dummy.OSC_Freq_Trim    = value;                    break;//grid[2, row].Value = dummy.CR_G; break;
                        case 13: dummy.thermal_bias = value;                    break;//grid[2, row].Value = dummy.CR_B; break;
                        case 14: dummy.tc_fine_thermal_idx[0]  = RT7216N_BD.limit(value,255);               break;//grid[2, row].Value = dummy.IOUT_trim; break;
                        case 15: dummy.tc_fine_thermal_idx[1]  = RT7216N_BD.limit(value,255);           break;//grid[2, row].Value = dummy.OSC_Freq_Trim; break;
                        case 16: dummy.tc_fine_thermal_idx[2]  = RT7216N_BD.limit(value,255);            break;//grid[2, row].Value = dummy.thermal_bias; break;
                        case 17: dummy.tc_fine_thermal_idx[3]  = RT7216N_BD.limit(value,255);        break;//grid[2, row].Value = dummy.thermal_gradient; break;
                        case 18: dummy.tc_fine_thermal_idx[4] =  RT7216N_BD.limit(value,255);              break;//grid[2, row].Value = dummy.V_THD_Trim; break;
                        case 19: dummy.tc_fine_thermal_idx[5] =  RT7216N_BD.limit(value,255);  break;//grid[2, row].Value = dummy.tc_fine_thermal_idx[0]; break;
                        case 20: dummy.tc_fine_thermal_idx[6] =  RT7216N_BD.limit(value,255);  break;//grid[2, row].Value = dummy.tc_fine_thermal_idx[1]; break;
                        case 21: dummy.tc_fine_thermal_idx[7] =  RT7216N_BD.limit(value,255);  break;//grid[2, row].Value = dummy.tc_fine_thermal_idx[2]; break;
                        case 22: dummy.tc_fine_thermal_idx[8] =  RT7216N_BD.limit(value,255);  break;//grid[2, row].Value = dummy.tc_fine_thermal_idx[3]; break;
                        case 23: dummy.tc_fine_thermal_idx[9] =  RT7216N_BD.limit(value,255);  break;//grid[2, row].Value = dummy.tc_fine_thermal_idx[4]; break;
                        case 24: dummy.tc_fine_thermal_idx[10]=  RT7216N_BD.limit(value,255);  break;//grid[2, row].Value = dummy.tc_fine_thermal_idx[5]; break;
                        case 25: dummy.tc_fine_thermal_idx[11]=  RT7216N_BD.limit(value,255);  break;//grid[2, row].Value = dummy.tc_fine_thermal_idx[6]; break;
                        case 26: dummy.tc_fine_shift3_0= value;  break;//grid[2, row].Value = dummy.tc_fine_thermal_idx[7]; break;
                        case 27: dummy.tc_fine_shift7_4= value;  break;//grid[2, row].Value = dummy.tc_fine_thermal_idx[8]; break;
                        case 28: dummy.tc_fine_shift11_8= value;  break;//grid[2, row].Value = dummy.tc_fine_thermal_idx[9]; break;
                        case 29: dummy.otp_internal_lock = value; break;//grid[2, row].Value = dummy.tc_fine_thermal_idx[10]; break;
                        case 30: dummy.otp_program_lock = value;         break;//grid[2, row].Value = dummy.tc_fine_shift10; break;
                    } 
                }
            }
            updateIC2Form();
            isOTPGridReady = true;
        }


        //specifically for datagridview checkboxes
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

            if (currentFocus != dataGridView_response) { dataGridView_response.ClearSelection(); }
            if (currentFocus != dataGridView_PWM) { dataGridView_PWM.ClearSelection(); }
            if (currentFocus != dataGridView_PrimaryRegister) { dataGridView_PrimaryRegister.ClearSelection(); }
            if (currentFocus != dataGridView_MISC) { dataGridView_MISC.ClearSelection(); }
            if (currentFocus != dataGridView_OTP_ADDR) { dataGridView_OTP_ADDR.ClearSelection(); }
            if (currentFocus != dataGridView_OTP_Variables) { dataGridView_OTP_Variables.ClearSelection(); }

        }
    }
}
    