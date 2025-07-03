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
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;
using System.Management;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace RT7216Q_temperature_compensation
{
    public partial class Form1 : Form
    {
        AT32F403A MCU;

        Stopwatch timer = new Stopwatch();
        int timeout = 3000;

        public Form1()
        {
            InitializeComponent();

            rT7216Q_Control1.button_PWM.Click += PWM_Click;
            rT7216Q_Control1.button_PrimaryRegister.Click += PrimaryReg_Click;
            rT7216Q_Control1.button_MISC.Click += Misc_Click;
            rT7216Q_Control1.button_OTP_send.Click += OTP_Send_Click;
            rT7216Q_Control1.button_OTP_read.Click += OTP_Read_Click;

            rT7216N_BD_Control1.button_PWM.Click += PWM_Click;
            rT7216N_BD_Control1.button_PrimaryRegister.Click += PrimaryReg_Click;
            rT7216N_BD_Control1.button_MISC.Click += Misc_Click;
            rT7216N_BD_Control1.button_OTP_send.Click += OTP_Send_Click;
            rT7216N_BD_Control1.button_OTP_read.Click += OTP_Read_Click;

            temperatureCompensation1.button_test_Clicked += button_TC_test_Clicked;
            temperatureCompensation1.button_CR_Clicked += button_TC_CR_Clicked;
            temperatureCompensation1.button_SCALAR_Clicked += button_TC_SCALAR_Clicked;
            temperatureCompensation1.button_TC_linear_Clicked += button_TC_linear_Clicked;


            dataGridView_Pulse.Rows.Add("length",true, "100");
            dataGridView_Pulse.Rows.Add("delay",true, "330");
            dataGridView_Pulse.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            isPulseReady = true;
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            MCU = new AT32F403A();
            MCU.count_en = 1; MCU.count_time = 500;
            MCU.delay_en = 1;

            rT7216N_BD_Control1.IC.primary_reg = new RT7216N_BD.PRIMARY_REGISTER()
            {
                OFF_WDG = 1,
                OFF_SLP = 1,
                IOHL = 2,
                READ_SPD = 3
            };
            rT7216N_BD_Control1.updateIC2Form();


            tabControl1.TabPages.Remove(tabPage2);

        }

        private void rT7216Q_Control1_Load(object sender, EventArgs e)
        {

        }


        private void PWM_Click(object sender, EventArgs e) 
        {
            AT32F403A.Status state = AT32F403A.Status.busy;
            switch (tabControl1.SelectedIndex)
            {
                case 0: state = MCU.usb_send(rT7216Q_Control1.IC.pwm); break;
                case 1: state = MCU.usb_send(rT7216N_BD_Control1.IC.pwm); break;
                default: break;
            }
            if (state != AT32F403A.Status.connected) { MessageBox.Show("MCU is either busy or disconnected"); }
        }
        private void PrimaryReg_Click(object sender, EventArgs e)
        {
            AT32F403A.Status state = AT32F403A.Status.busy;
            switch (tabControl1.SelectedIndex)
            {
                case 0: state = MCU.usb_send(rT7216Q_Control1.IC.primary_reg); if (rT7216Q_Control1.IC.primary_reg.DET != 0) { rT7216Q_Control1.updateResponse(rT7216Q_Control1.IC.primary_reg.DET, MCU.recv_buffer[3]); } break;
                case 1: state = MCU.usb_send(rT7216N_BD_Control1.IC.primary_reg); if (rT7216N_BD_Control1.IC.primary_reg.DET != 0) { rT7216N_BD_Control1.updateResponse(rT7216N_BD_Control1.IC.primary_reg.DET, MCU.recv_buffer[3]); } break;
                default: break;
            }
            if (state != AT32F403A.Status.connected) { MessageBox.Show("MCU is either busy or disconnected"); }
        }
        private void Misc_Click(object sender, EventArgs e) 
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0: 
                    MCU.usb_send(rT7216Q_Control1.IC.misc);
                    if (rT7216Q_Control1.IC.misc.enable[2])
                    {
                        RT7216Q.MISC misc = rT7216Q_Control1.IC.misc;
                             if ((misc.READ_AVG_THM_IDX == 1) && (misc.READ_NOW_THM_IDX == 0) && (misc.READ_USE_THM_IDX == 0)) { rT7216Q_Control1.updateResponse(4, MCU.recv_buffer[3]); }
                        else if ((misc.READ_AVG_THM_IDX == 0) && (misc.READ_NOW_THM_IDX == 1) && (misc.READ_USE_THM_IDX == 0)) { rT7216Q_Control1.updateResponse(5, MCU.recv_buffer[3]); }
                        else if ((misc.READ_AVG_THM_IDX == 0) && (misc.READ_NOW_THM_IDX == 0) && (misc.READ_USE_THM_IDX == 1)) { rT7216Q_Control1.updateResponse(6, MCU.recv_buffer[3]); }
                        
                    }
                    break;
                case 1: 
                    MCU.usb_send(rT7216N_BD_Control1.IC.misc);
                    if (rT7216N_BD_Control1.IC.misc.enable[2]) { rT7216N_BD_Control1.updateResponse(4, MCU.recv_buffer[3]); }
                    break;
                default: break;
            }
        }
        private void OTP_Send_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            { 
                case 0:  //Q
                    if (rT7216Q_Control1.comboBox_OTP_Type.SelectedIndex == 0) {
                        OTPprogramForm Check = new OTPprogramForm();
                        DialogResult result = Check.ShowDialog();
                        if (result != System.Windows.Forms.DialogResult.OK) { return; }
                        MCU.usb_otp_MEM_write(rT7216Q_Control1.IC.otp_memory);
                    }
                    else{ MCU.usb_otp_REG_write(rT7216Q_Control1.IC.otp_reg);}
                    break;
                case 1:
                    if (rT7216N_BD_Control1.comboBox_OTP_Type.SelectedIndex == 0) { MCU.usb_otp_MEM_write(rT7216N_BD_Control1.IC.otp_memory); }
                    else { MCU.usb_otp_REG_write(rT7216N_BD_Control1.IC.otp_reg); }
                    break;
                default: break;
            }
        }
        private void OTP_Read_Click(object sender, EventArgs e)
        {
            
            switch (tabControl1.SelectedIndex)
            {
                case 0:  //Q
                    RT7216Q.OTP otp_res1;
                    
                    if (rT7216Q_Control1.comboBox_OTP_Type.SelectedIndex == 0) {
                        otp_res1 = new RT7216Q.OTP(RT7216Q.OTP.Type.memory);
                        MCU.usb_otp_MEM_read(rT7216Q_Control1.IC.otp_memory); 
                    }
                    else {
                        otp_res1 = new RT7216Q.OTP(RT7216Q.OTP.Type.register); 
                        MCU.usb_otp_REG_read(rT7216Q_Control1.IC.otp_reg); 
                        
                    }

                    for (int i = 0; i < 32; i++) { otp_res1[i] = MCU.recv_buffer[10 + i]; }
                    rT7216Q_Control1.updateOtpRead(otp_res1);
                    break;
                case 1:
                    RT7216N_BD.OTP otp_res2;
                    
                    if (rT7216N_BD_Control1.comboBox_OTP_Type.SelectedIndex == 0) {
                        otp_res2 = new RT7216N_BD.OTP(RT7216N_BD.OTP.Type.memory);
                        MCU.usb_otp_MEM_read(rT7216N_BD_Control1.IC.otp_memory); 
                    }
                    else {
                        otp_res2 = new RT7216N_BD.OTP(RT7216N_BD.OTP.Type.register);
                        MCU.usb_otp_REG_read(rT7216N_BD_Control1.IC.otp_reg); 
                    }
                    
                    
                    for (int i = 0; i < 32; i++) { otp_res2[i] = MCU.recv_buffer[10 + i]; }
                    rT7216N_BD_Control1.updateOtpRead(otp_res2);
                    break;
                default: break;
            }



        }
        void isSuccess(AT32F403A.Status state) { 
            switch (state) {
                case AT32F403A.Status.closed: foreach (Classes.COMPORT com in flowLayoutPanel_Comports.Controls) {com.tableLayoutPanel1.BackColor = SystemColors.ControlLight; } break;
                case AT32F403A.Status.error: foreach (Classes.COMPORT com in flowLayoutPanel_Comports.Controls) { com.tableLayoutPanel1.BackColor = SystemColors.ControlLight; } break;
                default: break;
            } 
        }


        //async control
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        public async Task TaskDelay_s(int seconds)
        {
            for (int i = seconds*100; i > 0; i--)
            {
                if (tokenSource.Token.IsCancellationRequested) { return; }
                await Task.Delay(10);
            }
        }
        public async Task TaskDelay_ms(int milliseconds)
        {
            for (int i = milliseconds / 10; i > 0; i--)
            {
                if (tokenSource.Token.IsCancellationRequested) { return; }
                await Task.Delay(10);
            }
        }
        private void button_Abort_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
            //label_timer.Text = "Aborted!";
        }

        //excel sheet control
        private void testExcel()
        {
            //string projectDir = Environment.CurrentDirectory;
            //string filePath = Path.Combine(projectDir, "光寶_20240827_IC3_D65_RB補償.xlsx");

            //Excel.Application   excelApp = new Excel.Application();
            //Excel.Workbook workbook = excelApp.Workbooks.Open(filePath);
            //Excel.Workbook workbook = excelApp.Workbooks.Add();
            //Excel.Worksheet worksheet = new Excel.Worksheet();
            //Excel.Worksheet worksheet = workbook.Worksheets[1];
            //worksheet.Name = "test";

            //excelApp.Visible = true;

            ////(row,col)
            //excelApp.Cells[1, 1] = "(1,1)";
            //excelApp.Cells[1, 2] = "(1,2)";
            //excelApp.Cells[2, 1] = "(2,1)";
            //excelApp.Cells[2, 2] = "(2,2)";


            // 讀取資料
            //int rowCount = worksheet.UsedRange.Rows.Count;
            //int colCount = worksheet.UsedRange.Columns.Count;

            //for (int row = 1; row <= rowCount; row++)
            //{
            //    for (int col = 1; col <= colCount; col++)
            //    {
            //        // 使用Cells物件來取得單元格的值
            //        Excel.Range cell = worksheet.Cells[row, col];
            //        string cellValue = cell.Value != null ? cell.Value.ToString() : "";
            //        Console.Write(cellValue + "\t");
            //    }
            //    Console.WriteLine();
            //}


            //workbook.SaveAs(Path.Combine(projectDir, "光寶_20240827_IC3_D65_RB補償2.xlsx"));
            //// 關閉Excel檔案
            //workbook.Close();
            //excelApp.Quit();

            //// 釋放資源
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

            //Console.ReadLine();

        }


        // COMPORT connect
        private void button_RefreshPorts_Click(object sender, EventArgs e)
        {
            if (MCU.state == AT32F403A.Status.connected) { MCU.close();  }

            string[] portnames = System.IO.Ports.SerialPort.GetPortNames().Distinct().ToArray();
            flowLayoutPanel_Comports.Controls.Clear();

            using (var searcher = new ManagementObjectSearcher("SELECT * FROM WIN32_SerialPort"))
            {
                var ps = searcher.Get().Cast<ManagementBaseObject>().ToList();

                foreach (var port in ps)
                {
                    
                    string DevID = port.GetPropertyValue("DeviceID").ToString().Trim();
                    string Description = port.GetPropertyValue("Description").ToString().Trim();
                    string PNPDevID = port.GetPropertyValue("PNPDeviceID").ToString().Trim();
                    //if (port.GetPropertyValue("Description").ToString().Trim() == "USB Serial Device") {
                        var newPORT = new Classes.COMPORT();
                        newPORT.label_DevID.Text = DevID;
                        newPORT.label_Description.Text = Description;
                        newPORT.label_PNPDevID.Text = PNPDevID;
                        newPORT.ClickHandler += Comport_Click;

                        flowLayoutPanel_Comports.Controls.Add(newPORT);
                        
                    //}
                    //string[] properties = new string[] { "Name", "Caption", "Description", "DeviceID", "PNPDeviceID" };
                    //foreach (var property in properties)
                    //{
                    //    Console.WriteLine(property + ":\t" + port.GetPropertyValue(property));
                    //}

                }

            }
        
        }
        private void Comport_Click(object sender, EventArgs e)
        {
            var comport = (sender as Classes.COMPORT);


            //if (!comport.label_Description.Text.Contains("USB")) { MessageBox.Show("This is not a USB serial device"); return; }



            foreach (Classes.COMPORT com in flowLayoutPanel_Comports.Controls)
            {
                com.tableLayoutPanel1.BackColor = SystemColors.ControlLight;
            }


            var state = MCU.open(comport.label_DevID.Text);
            if (state != AT32F403A.Status.connected) { MessageBox.Show("Connection failed, try again"); return; }
            comport.tableLayoutPanel1.BackColor = Color.LightGreen;
            //gui1.MCU = MCU;
        }
        bool isPulseReady = false;
        private void dataGridView_Pulse_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!isPulseReady) { return; }
            //Console.WriteLine(e.ColumnIndex);

            int r = e.RowIndex;

            switch (e.ColumnIndex)
            {
                case 1: 
                    MCU.count_en = ((bool)dataGridView_Pulse[1, 0].Value) ? 1 : 0; 
                    MCU.delay_en = ((bool)dataGridView_Pulse[1, 1].Value) ? 1 : 0;
                    break;
                case 2:
                    int t, row = e.RowIndex;


                    if (!int.TryParse(dataGridView_Pulse[2, row].Value.ToString(), out t))
                    {
                        dataGridView_Pulse[2, 0].Value = MCU.count_time;
                        dataGridView_Pulse[2, 1].Value = MCU.delay_time;
                    }
                    else
                    {
                        t = (0 < t) ? (t <= 2550) ? t : 2550 : 0;
                        dataGridView_Pulse[2, row].Value = t;
                        if (row == 0) { MCU.count_time = t; }
                        if (row == 1) { MCU.delay_time = t; }
                    }
                    break;
            }
            isPulseReady = true;
        }
        private void dataGridView_Pulse_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if ((grid.CurrentCell.ColumnIndex == 0))
            {
                grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }


        // for development
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 3)
            {
                //gui1.IC_Q = rT7216Q_Control1.IC;
                //gui1.IC_BD = rT7216N_BD_Control1.IC;
                //gui1.updateIC2Form();
            }
        }

        private void button_TC_test_Clicked(object sender, EventArgs e)
        {
            var row = ((DataGridViewCellEventArgs)e).RowIndex;
            
            
            int pwm = 255;
            rT7216Q_Control1.IC.pwm.set(pwm, pwm, pwm);
            rT7216Q_Control1.IC.otp_reg.max_PWM_chip = 765;
            rT7216Q_Control1.IC.otp_reg.en[0] = true;
            rT7216Q_Control1.IC.otp_reg.en[1] = true;
            rT7216Q_Control1.updateIC2Form();

            var IC = rT7216Q_Control1.IC;
            MCU.set_rgb(IC.pwm.R, IC.pwm.G, IC.pwm.B);
            MCU.usb_send(IC.primary_reg);
            MCU.usb_otp_REG_write(IC.otp_reg);


            


            if (!temperatureCompensation1.ISM360.isConnected)
            {
                MCU.usb_send(IC.pwm); Thread.Sleep(50);
            }
            else
            {
                //Thread.Sleep(1000);
                temperatureCompensation1.ISM360.triggerMeasure();

                timer.Reset();
                timer.Start();
                var t = TimeSpan.FromMilliseconds(timeout);
                while (temperatureCompensation1.ISM360.state == Classes.ISM360.Status.Measuring)
                {
                    if (timer.Elapsed > t) { temperatureCompensation1.ISM360.state = Classes.ISM360.Status.Idle; break; }
                }
                timer.Stop();
                Thread.Sleep(100); Thread.Sleep(100); Thread.Sleep(100); Thread.Sleep(100); Thread.Sleep(100);
                Thread.Sleep(100); Thread.Sleep(100); Thread.Sleep(100); Thread.Sleep(100); Thread.Sleep(100);
                temperatureCompensation1.ISM360.parseData();
                //temperatureCompensation1.dataGridView_CR.Rows
                //Thread.Sleep(100);

                var values = temperatureCompensation1.ISM360.DataOutContent.Values.ToList();

                var CIEx = float.Parse(values[0]);
                var CIEy = float.Parse(values[1]);
                var photometric = float.Parse(values[2]);

                temperatureCompensation1.dataGridView_CIE_points[1, 1].Value = CIEx;
                temperatureCompensation1.dataGridView_CIE_points[2, 1].Value = CIEy;
                temperatureCompensation1.dataGridView_CIE_points[3, 1].Value = photometric;
                temperatureCompensation1.graph1.test = new Classes.CIE_Point() { x = CIEx, y = CIEy }; ;
                //switch (row)
                //{
                //    case 0: temperatureCompensation1.dataGridView_CR[1, 0].Value = CIEx; temperatureCompensation1.dataGridView_CR[2, 0].Value = CIEy; temperatureCompensation1.dataGridView_CR[3, 0].Value = photometric; break;
                //    case 1: temperatureCompensation1.dataGridView_CR[1, 1].Value = CIEx; temperatureCompensation1.dataGridView_CR[2, 1].Value = CIEy; temperatureCompensation1.dataGridView_CR[3, 1].Value = photometric; break;
                //    case 2: temperatureCompensation1.dataGridView_CR[1, 2].Value = CIEx; temperatureCompensation1.dataGridView_CR[2, 2].Value = CIEy; temperatureCompensation1.dataGridView_CR[3, 2].Value = photometric; break;
                //    default: break;
                //}
            }

        }
        // for temperature compensation
        private void button_TC_CR_Clicked(object sender, EventArgs e)
        {
            var row = ((DataGridViewCellEventArgs)e).RowIndex;
            
            var IC = rT7216Q_Control1.IC;

            IC.primary_reg.DET = 0;
            IC.primary_reg.tc_r_en = 0;
            IC.primary_reg.tc_g_en = 0;
            IC.primary_reg.tc_b_en = 0;
            IC.primary_reg.ISEL_R = 1;
            IC.primary_reg.ISEL_G = 1;
            IC.primary_reg.ISEL_B = 1;
            IC.primary_reg.OFF_WDG = 1;
            IC.primary_reg.OFF_WDG = 1;

            IC.otp_reg.max_PWM_chip = 255 * 3;
            IC.otp_reg.PWM_scalar_R = 256;
            IC.otp_reg.PWM_scalar_G = 256; //384???;
            IC.otp_reg.PWM_scalar_B = 256;
            IC.otp_reg.CR_R = 63;
            IC.otp_reg.CR_G = 63;
            IC.otp_reg.CR_B = 63;

            IC.otp_reg.en[0] = true;
            IC.otp_reg.en[1] = true;
            IC.otp_reg.en[2] = IC.otp_reg.en[3] = IC.otp_reg.en[4] = IC.otp_reg.en[5] = IC.otp_reg.en[6] = IC.otp_reg.en[7] = true;
            IC.otp_reg.en[8] = IC.otp_reg.en[9] = IC.otp_reg.en[10] = true;
            IC.otp_reg.en[13] = IC.otp_reg.en[14] = false;

            int pwm = IC.otp_reg.max_PWM_chip / 3;

            switch (row)
            {
                case 0: rT7216Q_Control1.IC.pwm.set(pwm, 0, 0); break;
                case 1: rT7216Q_Control1.IC.pwm.set(0, pwm, 0); break;
                case 2: rT7216Q_Control1.IC.pwm.set(0, 0, pwm); break;
                case 3:
                    int[] CR = temperatureCompensation1.calculate_CR(127,0.0078f ,60,63);

                    rT7216Q_Control1.IC.otp_reg.CR_R = CR[0];
                    rT7216Q_Control1.IC.otp_reg.CR_G = CR[1];
                    rT7216Q_Control1.IC.otp_reg.CR_B = CR[2];
                    rT7216Q_Control1.updateIC2Form();
                    MCU.usb_otp_REG_write(IC.otp_reg); Thread.Sleep(150);
                    //rT7216N_BD_Control1.IC.otp_reg.CR_R = CR[0];
                    //rT7216N_BD_Control1.IC.otp_reg.CR_G = CR[1];
                    //rT7216N_BD_Control1.IC.otp_reg.CR_B = CR[2];
                    //rT7216N_BD_Control1.updateIC2Form();


                    return;
                default: break;
            }


            MCU.set_rgb(IC.pwm.R, IC.pwm.G, IC.pwm.B);
            rT7216Q_Control1.updateIC2Form();

            MCU.usb_send(IC.primary_reg); Thread.Sleep(50);
            //MCU.usb_send(IC.misc);
            MCU.usb_otp_REG_write(IC.otp_reg); Thread.Sleep(150);

            if (!temperatureCompensation1.ISM360.isConnected) { 
                MCU.usb_send(IC.pwm); Thread.Sleep(50);
            }else{
                //Thread.Sleep(1000);
                temperatureCompensation1.ISM360.triggerMeasure();

                timer.Reset();
                timer.Start();
                var t = TimeSpan.FromMilliseconds(timeout);
                while (temperatureCompensation1.ISM360.state == Classes.ISM360.Status.Measuring)
                {
                    if (timer.Elapsed > t) { temperatureCompensation1.ISM360.state = Classes.ISM360.Status.Idle; break; }
                }
                timer.Stop();
                Thread.Sleep(100); Thread.Sleep(100); Thread.Sleep(100); Thread.Sleep(100); Thread.Sleep(100);
                Thread.Sleep(100); Thread.Sleep(100); Thread.Sleep(100); Thread.Sleep(100); Thread.Sleep(100);
                temperatureCompensation1.ISM360.parseData();
                //temperatureCompensation1.dataGridView_CR.Rows
                //Thread.Sleep(100);

                var values = temperatureCompensation1.ISM360.DataOutContent.Values.ToList();

                var CIEx = float.Parse(values[0]);
                var CIEy = float.Parse(values[1]);
                var photometric = float.Parse(values[2]);

                switch (row)
                {
                    case 0: temperatureCompensation1.dataGridView_CR[1, 0].Value = CIEx; temperatureCompensation1.dataGridView_CR[2, 0].Value = CIEy; temperatureCompensation1.dataGridView_CR[3, 0].Value = photometric; break;
                    case 1: temperatureCompensation1.dataGridView_CR[1, 1].Value = CIEx; temperatureCompensation1.dataGridView_CR[2, 1].Value = CIEy; temperatureCompensation1.dataGridView_CR[3, 1].Value = photometric; break;
                    case 2: temperatureCompensation1.dataGridView_CR[1, 2].Value = CIEx; temperatureCompensation1.dataGridView_CR[2, 2].Value = CIEy; temperatureCompensation1.dataGridView_CR[3, 2].Value = photometric; break;
                    default: break;
                }

            }
            //var row = ((DataGridViewCellEventArgs) e).RowIndex;
            //switch (row)
            //{
            //    case 0: rT7216N_BD_Control1.IC = temperatureCompensation1.CR_settings("R"); break;
            //    case 1: rT7216N_BD_Control1.IC = temperatureCompensation1.CR_settings("G"); break;
            //    case 2: rT7216N_BD_Control1.IC = temperatureCompensation1.CR_settings("B"); break;
            //    case 3: 
            //        int[] CR = temperatureCompensation1.calculate_CR();
            //        rT7216N_BD_Control1.IC.otp_reg.CR_R = CR[0];
            //        rT7216N_BD_Control1.IC.otp_reg.CR_G = CR[1];
            //        rT7216N_BD_Control1.IC.otp_reg.CR_B = CR[2];
            //        rT7216N_BD_Control1.updateIC2Form(); 
            //        return;
            //    default: break;
            //}
            //var IC = rT7216N_BD_Control1.IC;

            //MCU.set_rgb(IC.pwm.R, IC.pwm.G, IC.pwm.B);
            //rT7216N_BD_Control1.updateIC2Form();

            //MCU.usb_send(IC.primary_reg);
            //MCU.usb_send(IC.misc);
            //MCU.usb_otp_REG_write(IC.otp_reg);
            //MCU.usb_send(IC.pwm);
        }
        private void button_TC_SCALAR_Clicked(object sender, EventArgs e)
        {
            var row = ((DataGridViewCellEventArgs)e).RowIndex;
            int[] SCALAR;

            var IC = rT7216Q_Control1.IC;

            IC.primary_reg.DET = 0;
            IC.primary_reg.tc_r_en = 0;
            IC.primary_reg.tc_g_en = 0;
            IC.primary_reg.tc_b_en = 0;
            IC.primary_reg.ISEL_R = 1;
            IC.primary_reg.ISEL_G = 1;
            IC.primary_reg.ISEL_B = 1;
            IC.primary_reg.OFF_WDG = 1;
            IC.primary_reg.OFF_WDG = 1;

            IC.otp_reg.max_PWM_chip = 255 * 3;

            IC.otp_reg.en[0] = true;
            IC.otp_reg.en[1] = true;
            IC.otp_reg.en[2] = IC.otp_reg.en[3] = IC.otp_reg.en[4] = IC.otp_reg.en[5] = IC.otp_reg.en[6] = IC.otp_reg.en[7] = true;
            IC.otp_reg.en[8] = IC.otp_reg.en[9] = IC.otp_reg.en[10] = true;
            IC.otp_reg.en[13] = IC.otp_reg.en[14] = false;

            int pwm = IC.otp_reg.max_PWM_chip / 3;
            switch (row)
            {
                case 0: IC.otp_reg.PWM_scalar_R = IC.otp_reg.PWM_scalar_G = IC.otp_reg.PWM_scalar_B = 256; IC.pwm.set(pwm, 0, 0);break;
                case 1: IC.otp_reg.PWM_scalar_R = IC.otp_reg.PWM_scalar_G = IC.otp_reg.PWM_scalar_B = 256; IC.pwm.set(0, pwm, 0); break;
                case 2: IC.otp_reg.PWM_scalar_R = IC.otp_reg.PWM_scalar_G = IC.otp_reg.PWM_scalar_B = 256; IC.pwm.set(0, 0, pwm); break;
                case 3:
                    SCALAR = temperatureCompensation1.calculate_SCALAR(false);
                    rT7216Q_Control1.IC.otp_reg.PWM_scalar_R = SCALAR[0];
                    rT7216Q_Control1.IC.otp_reg.PWM_scalar_G = SCALAR[1];
                    rT7216Q_Control1.IC.otp_reg.PWM_scalar_B = SCALAR[2];
                    rT7216Q_Control1.updateIC2Form();
                    MCU.usb_otp_REG_write(IC.otp_reg); Thread.Sleep(150);
                    return;
                case 4: IC.pwm.set(pwm, pwm, pwm); break;
                case 5:
                    SCALAR = temperatureCompensation1.calculate_SCALAR(true);
                    rT7216Q_Control1.IC.otp_reg.PWM_scalar_R = SCALAR[0];
                    rT7216Q_Control1.IC.otp_reg.PWM_scalar_G = SCALAR[1];
                    rT7216Q_Control1.IC.otp_reg.PWM_scalar_B = SCALAR[2];
                    rT7216Q_Control1.updateIC2Form();
                     MCU.usb_otp_REG_write(IC.otp_reg); Thread.Sleep(150);
                    return;
                case 6: IC.pwm.set(pwm, pwm, pwm); break;
                default: break;
            }

            rT7216Q_Control1.updateIC2Form();

            MCU.set_rgb(IC.pwm.R, IC.pwm.G, IC.pwm.B);
            rT7216Q_Control1.updateIC2Form();

            MCU.usb_send(IC.primary_reg); Thread.Sleep(10);
            MCU.usb_otp_REG_write(IC.otp_reg); Thread.Sleep(10);

            if (!temperatureCompensation1.ISM360.isConnected)
            {
                MCU.usb_send(IC.pwm); Thread.Sleep(50);
            }
            else
            {
                //Thread.Sleep(1000);
                temperatureCompensation1.ISM360.triggerMeasure();



                timer.Reset();
                timer.Start();
                var t = TimeSpan.FromMilliseconds(timeout);
                while (temperatureCompensation1.ISM360.state == Classes.ISM360.Status.Measuring)
                {
                    if (timer.Elapsed > t) { temperatureCompensation1.ISM360.state = Classes.ISM360.Status.Idle;break; }
                }
                timer.Stop();
                Thread.Sleep(100); Thread.Sleep(100); Thread.Sleep(100); Thread.Sleep(100); Thread.Sleep(100);
                Thread.Sleep(100); Thread.Sleep(100); Thread.Sleep(100); Thread.Sleep(100); Thread.Sleep(100);
                temperatureCompensation1.ISM360.parseData();
                //Thread.Sleep(500); Thread.Sleep(500); Thread.Sleep(500); Thread.Sleep(500); Thread.Sleep(500); Thread.Sleep(500); Thread.Sleep(500); Thread.Sleep(500); Thread.Sleep(500); Thread.Sleep(500);
                //temperatureCompensation1.dataGridView_CR.Rows
                //Thread.Sleep(100);

                var values = temperatureCompensation1.ISM360.DataOutContent.Values.ToList();
                float dummy;
                if (!float.TryParse(values[0], out dummy) || !float.TryParse(values[1], out dummy) )
                {
                    MessageBox.Show("Not a valid reading, please check the IC LED can operate as normal, then re-try the measurement");
                    return;
                }
                var CIEx = float.Parse(values[0]);
                var CIEy = float.Parse(values[1]);
                var photometric = float.Parse(values[2]);

                Classes.CIE_Point point = new Classes.CIE_Point() { x = CIEx, y = CIEy };

                switch (row)
                {
                    case 0: temperatureCompensation1.dataGridView_SCALAR[1, 0].Value = CIEx; temperatureCompensation1.dataGridView_SCALAR[2, 0].Value = CIEy; temperatureCompensation1.dataGridView_SCALAR[3, 0].Value = photometric; break;
                    case 1: temperatureCompensation1.dataGridView_SCALAR[1, 1].Value = CIEx; temperatureCompensation1.dataGridView_SCALAR[2, 1].Value = CIEy; temperatureCompensation1.dataGridView_SCALAR[3, 1].Value = photometric; break;
                    case 2: temperatureCompensation1.dataGridView_SCALAR[1, 2].Value = CIEx; temperatureCompensation1.dataGridView_SCALAR[2, 2].Value = CIEy; temperatureCompensation1.dataGridView_SCALAR[3, 2].Value = photometric; break;
                    case 4: temperatureCompensation1.dataGridView_SCALAR[1, 4].Value = CIEx; temperatureCompensation1.dataGridView_SCALAR[2, 4].Value = CIEy; temperatureCompensation1.dataGridView_SCALAR[3, 4].Value = photometric; temperatureCompensation1.graph1.plot_CIE(3, point); break;
                    case 6: temperatureCompensation1.dataGridView_SCALAR[1, 6].Value = CIEx; temperatureCompensation1.dataGridView_SCALAR[2, 6].Value = CIEy; temperatureCompensation1.dataGridView_SCALAR[3, 6].Value = photometric; temperatureCompensation1.graph1.plot_CIE(4, point); break;
                    default: break;
                }

                //var row = ((DataGridViewCellEventArgs)e).RowIndex;
                //int[] SCALAR;
                //switch (row)
                //{
                //    case 0: rT7216N_BD_Control1.IC = temperatureCompensation1.SCALAR_settings(0); break;
                //    case 1: rT7216N_BD_Control1.IC = temperatureCompensation1.SCALAR_settings(1); break;
                //    case 2: rT7216N_BD_Control1.IC = temperatureCompensation1.SCALAR_settings(2); break;
                //    case 3: 
                //        SCALAR = temperatureCompensation1.calculate_SCALAR( false);
                //        rT7216N_BD_Control1.IC.otp_reg.PWM_scalar_R = SCALAR[0];
                //        rT7216N_BD_Control1.IC.otp_reg.PWM_scalar_G = SCALAR[1];
                //        rT7216N_BD_Control1.IC.otp_reg.PWM_scalar_B = SCALAR[2];
                //        rT7216N_BD_Control1.updateIC2Form(); 
                //        return;
                //    case 4: rT7216N_BD_Control1.IC = temperatureCompensation1.SCALAR_settings(3); break;
                //    case 5:
                //        SCALAR = temperatureCompensation1.calculate_SCALAR(true); 
                //        rT7216N_BD_Control1.IC.otp_reg.PWM_scalar_R = SCALAR[0];
                //        rT7216N_BD_Control1.IC.otp_reg.PWM_scalar_G = SCALAR[1];
                //        rT7216N_BD_Control1.IC.otp_reg.PWM_scalar_B = SCALAR[2];
                //        rT7216N_BD_Control1.updateIC2Form(); 
                //        return;
                //    case 6: rT7216N_BD_Control1.IC = temperatureCompensation1.SCALAR_settings(3); break;
                //    default: break;
                //}
                //var IC = rT7216N_BD_Control1.IC;

                //MCU.set_rgb(IC.pwm.R, IC.pwm.G, IC.pwm.B);
                //rT7216N_BD_Control1.updateIC2Form();

                //MCU.usb_send(IC.primary_reg);
                //MCU.usb_send(IC.misc);
                //MCU.usb_otp_REG_write(IC.otp_reg);
                //MCU.usb_send(IC.pwm);
            }
        }
        private void button_TC_linear_Clicked(object sender, EventArgs e)
        {
            var row = ((DataGridViewCellEventArgs)e).RowIndex;
            switch (row)
            {
                case 0: rT7216N_BD_Control1.IC = temperatureCompensation1.SCALAR_settings(0); break;
                case 1: rT7216N_BD_Control1.IC = temperatureCompensation1.SCALAR_settings(1); break;
                case 2: rT7216N_BD_Control1.IC = temperatureCompensation1.SCALAR_settings(2); break;
                case 3: break;
                case 4: break;
                case 5: break;
                case 6: temperatureCompensation1.Plot_decay(); break;
                default: break;
            }
            var IC = rT7216N_BD_Control1.IC;

            MCU.set_rgb(IC.pwm.R, IC.pwm.G, IC.pwm.B);
            rT7216N_BD_Control1.updateIC2Form();

            MCU.usb_send(IC.primary_reg);
            MCU.usb_send(IC.misc);
            MCU.usb_otp_REG_write(IC.otp_reg);
            MCU.usb_send(IC.pwm);
        }

        private void numericUpDown_IC_count_ValueChanged(object sender, EventArgs e)
        {
            MCU.COLUMNS = (int)numericUpDown_IC_count.Value;
        }

        private void temperatureCompensation1_Load(object sender, EventArgs e)
        {

        }

        private void ReadCellByInterop(string path)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook wb = xlApp.Workbooks.Open(path, ReadOnly: true);
            Excel.Worksheet ws = wb.Worksheets[1];         // Worksheets 由 1 開始
            object val = ws.Cells[5, 2].Value;             // 2:列, 2:欄  ⇒ B2
            //MessageBox.Show($"B2 內容 = {val}");

            if (val != null)
            {
                //string str = val.ToString();  // 轉成字串 "0 (DEL)"
    
                // 方法1：Split（依空白切開，取第0個）
                //string firstPart = str.Split(' ')[0]; // "0"
                //int number = int.Parse(firstPart);    // 轉成 int

                ws = wb.Worksheets[1];   

                rT7216Q_Control1.IC.otp_reg.max_PWM_chip = int.Parse(ws.Cells[1, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.PWM_scalar_R = int.Parse(ws.Cells[2, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.PWM_scalar_G = int.Parse(ws.Cells[3, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.PWM_scalar_B = int.Parse(ws.Cells[4, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.pwm_18bit_mode = int.Parse(ws.Cells[5, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.tc_r_base = int.Parse(ws.Cells[6, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.tc_r_gradient = int.Parse(ws.Cells[7, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.tc_g_base = int.Parse(ws.Cells[8, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.tc_g_gradient = int.Parse(ws.Cells[9, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.tc_b_base = int.Parse(ws.Cells[10, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.tc_b_gradient = int.Parse(ws.Cells[11, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.CR_R = int.Parse(ws.Cells[12, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.CR_G = int.Parse(ws.Cells[13, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.CR_B = int.Parse(ws.Cells[14, 2].Value.ToString());

                rT7216Q_Control1.IC.otp_reg.IOUT_trim = int.Parse(ws.Cells[15, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.OSC_Freq_Trim = int.Parse(ws.Cells[16, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.thermal_bias = int.Parse(ws.Cells[17, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.thermal_gradient = int.Parse(ws.Cells[18, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.V_THD_Trim = int.Parse(ws.Cells[19, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.tc_fine_shift10 = int.Parse(ws.Cells[20, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.tc_fine_shift9 = int.Parse(ws.Cells[21, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.tc_fine_shift8 = int.Parse(ws.Cells[22, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.tc_fine_thermal_idx[0] = int.Parse(ws.Cells[23, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.tc_fine_thermal_idx[1] = int.Parse(ws.Cells[24, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.tc_fine_thermal_idx[2] = int.Parse(ws.Cells[25, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.tc_fine_thermal_idx[3] = int.Parse(ws.Cells[26, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.tc_fine_thermal_idx[4] = int.Parse(ws.Cells[27, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.tc_fine_thermal_idx[5] = int.Parse(ws.Cells[28, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.tc_fine_thermal_idx[6] = int.Parse(ws.Cells[29, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.tc_fine_thermal_idx[7] = int.Parse(ws.Cells[30, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.tc_fine_thermal_idx[8] = int.Parse(ws.Cells[31, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.tc_fine_thermal_idx[9] = int.Parse(ws.Cells[32, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.tc_fine_thermal_idx[10] = int.Parse(ws.Cells[33, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.otp_internal_lock = int.Parse(ws.Cells[34, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_reg.otp_program_lock = int.Parse(ws.Cells[35, 2].Value.ToString());


//===================================================================================================================

                ws = wb.Worksheets[2];

                rT7216Q_Control1.IC.otp_memory.max_PWM_chip = int.Parse(ws.Cells[1, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.PWM_scalar_R = int.Parse(ws.Cells[2, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.PWM_scalar_G = int.Parse(ws.Cells[3, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.PWM_scalar_B = int.Parse(ws.Cells[4, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.pwm_18bit_mode = int.Parse(ws.Cells[5, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.tc_r_base = int.Parse(ws.Cells[6, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.tc_r_gradient = int.Parse(ws.Cells[7, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.tc_g_base = int.Parse(ws.Cells[8, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.tc_g_gradient = int.Parse(ws.Cells[9, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.tc_b_base = int.Parse(ws.Cells[10, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.tc_b_gradient = int.Parse(ws.Cells[11, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.CR_R = int.Parse(ws.Cells[12, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.CR_G = int.Parse(ws.Cells[13, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.CR_B = int.Parse(ws.Cells[14, 2].Value.ToString());

                rT7216Q_Control1.IC.otp_memory.IOUT_trim = int.Parse(ws.Cells[15, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.OSC_Freq_Trim = int.Parse(ws.Cells[16, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.thermal_bias = int.Parse(ws.Cells[17, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.thermal_gradient = int.Parse(ws.Cells[18, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.V_THD_Trim = int.Parse(ws.Cells[19, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.tc_fine_shift10 = int.Parse(ws.Cells[20, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.tc_fine_shift9 = int.Parse(ws.Cells[21, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.tc_fine_shift8 = int.Parse(ws.Cells[22, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.tc_fine_thermal_idx[0] = int.Parse(ws.Cells[23, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.tc_fine_thermal_idx[1] = int.Parse(ws.Cells[24, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.tc_fine_thermal_idx[2] = int.Parse(ws.Cells[25, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.tc_fine_thermal_idx[3] = int.Parse(ws.Cells[26, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.tc_fine_thermal_idx[4] = int.Parse(ws.Cells[27, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.tc_fine_thermal_idx[5] = int.Parse(ws.Cells[28, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.tc_fine_thermal_idx[6] = int.Parse(ws.Cells[29, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.tc_fine_thermal_idx[7] = int.Parse(ws.Cells[30, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.tc_fine_thermal_idx[8] = int.Parse(ws.Cells[31, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.tc_fine_thermal_idx[9] = int.Parse(ws.Cells[32, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.tc_fine_thermal_idx[10] = int.Parse(ws.Cells[33, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.otp_internal_lock = int.Parse(ws.Cells[34, 2].Value.ToString());
                rT7216Q_Control1.IC.otp_memory.otp_program_lock = int.Parse(ws.Cells[35, 2].Value.ToString());



            }

            // 關閉與釋放
            wb.Close(false);
            xlApp.Quit();
            Marshal.ReleaseComObject(ws);
            Marshal.ReleaseComObject(wb);
            Marshal.ReleaseComObject(xlApp);
        }


        private void FileImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel 檔案 (*.xlsx)|*.xlsx|所有檔案 (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // TODO: 在這裡呼叫你的匯入流程，例如：
                    // ImportExcel(ofd.FileName);
                    ReadCellByInterop(ofd.FileName);

                    rT7216Q_Control1.updateIC2Form();

                }
            }
        }

        private void FileExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel 檔案 (*.xlsx)|*.xlsx|所有檔案 (*.*)|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // TODO: 在這裡呼叫你的匯出流程，例如：
                    // ExportExcel(sfd.FileName);
                }
            }
        }



    }
}
