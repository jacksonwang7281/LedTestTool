using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Collections;
using System.Threading;
using System.Diagnostics;

using RT7216_B = RT7216N_BD;

namespace RT7216Q_temperature_compensation
{

    public class AT32F403A
    {
        

        public System.IO.Ports.SerialPort port = new SerialPort();

        public int r = 0xFFFF, g = 0xFFFF, b = 0xFFFF, count_en = 0, count_time = 100, button = 0xFF, pwm_max = 0xFFFF, delay_time=330,delay_en=0, COLUMNS=1;
        public byte[] send_buffer = new byte[64];
        public byte[] recv_buffer = new byte[64];
        public Status state = Status.closed;
        public byte msg_count = 0;
        public double timeout = 1.5;

        public enum Status
        {
            connected = 0,
            busy = 1,
            closed = 2,
            error = 3
        }

        private enum version
        {
            RT7216Q = 0,
            RT7216N_BD=1,
        }
        public AT32F403A()
        {
            port.ReceivedBytesThreshold = 63;
            port.DataReceived += handleResponse;
            setup();
        }

        public void setup()
        {
            port.BaudRate = 115200;
            port.Parity = Parity.None;
            port.DataBits = 8;
            port.StopBits = StopBits.One;
            port.Encoding = Encoding.Unicode;
        }
        public Status open(string com)
        {
            if (port.IsOpen) { port.Close(); }
            this.port.PortName = com;
            return open();
        }
        public Status open()
        {
            if (port.IsOpen) { return Status.connected; }
            setup();
            try { port.Open(); }
            catch (Exception error)
            {
                switch (error.GetType().ToString())
                {
                    case "System.UnauthorizedAccessException": MessageBox.Show("Unauthorized access to port"); break;
                    case "System.InvalidOperationException": MessageBox.Show("InvalidOperationException"); break;
                    default: Console.WriteLine("{0} \r\n some error has occurred, check if MCU is connected", error.ToString()); break;
                }
                return Status.closed;
            }
            state = Status.connected;
            return state;
        }
        public Status close()
        {
            if (!port.IsOpen) { return Status.closed; }

            try { port.Close(); }
            catch (Exception error)
            {
                switch (error.GetType().ToString())
                {
                    case "System.UnauthorizedAccessException": MessageBox.Show("Unauthorized access to port"); break;
                    case "System.InvalidOperationException": MessageBox.Show("InvalidOperationException"); break;
                    default: Console.WriteLine("{0} \r\n some error has occurred, check if MCU is connected", error.ToString()); break;
                }
                return Status.error;
            }

            return Status.closed;
        }
        void handleResponse(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                port.Read(recv_buffer, 0, port.ReceivedBytesThreshold);
            }
            catch (System.IO.IOException exIO)
            {
                //MessageBox.Show("expected error");
                //port.Close();
                //port.Open();
                state = Status.connected;
                return;
            }
            catch(System.TimeoutException TimeEx){

            }
            catch (Exception ex)
            {
                //state = Status.error;
                //MessageBox.Show("show sort of exception occured");
            }
            state = Status.connected;
        }

        public Status usb_send()
        {
            Stopwatch timer = new Stopwatch();
            int tick = 0;
            send_buffer[2] = (byte)COLUMNS;

            send_buffer[52] = (byte)(delay_time / 10);
            send_buffer[53] = (byte)((pwm_max & 0xFF00) >> 8);
            send_buffer[54] = (byte)(pwm_max & 0xFF);
            send_buffer[55] = (byte)button;
            send_buffer[56] = (byte)(count_time / 10);
            send_buffer[57] = (byte) ((delay_en<<1) + count_en);
            send_buffer[58] = (byte)((r & 0xFF00) >> 8);
            send_buffer[59] = (byte)(r & 0xFF);
            send_buffer[60] = (byte)((g & 0xFF00) >> 8);
            send_buffer[61] = (byte)(g & 0xFF);
            send_buffer[62] = (byte)((b & 0xFF00) >> 8);
            send_buffer[63] = (byte)(b & 0xFF);



            try
            {
                double t = timeout;
                t += (delay_en == 1) ? delay_time : 0;
                t += (count_en == 1) ? count_time : 0;
                if (timeout > (double)(t / 1000)) { t = timeout; }
                TimeSpan ts2 = new TimeSpan(0), ts = new TimeSpan(0), tend = TimeSpan.FromSeconds(t);

                if (!port.IsOpen) { state = Status.closed; return state; };

                timer.Start();
                //while ((state == Status.busy) && (ts2 < tend)) { ts2 = timer.Elapsed; } 
                //timer.Stop();
                //if (ts2 > tend) { return close(); }
                
                

                this.state = Status.busy;
                send_buffer[1] = msg_count;


                port.DiscardOutBuffer();
                port.DiscardInBuffer();

                timer.Reset();
                timer.Start();
                port.Write(send_buffer, 0, send_buffer.Length);

                while ((recv_buffer[1] != msg_count) && (ts < tend)) { 
                    ts = timer.Elapsed;
                }
                if (recv_buffer[1] != msg_count)
                {
                    state = Status.error;
                    return state;
                }
                timer.Stop();
                if (ts > tend) {  state = Status.error; return state; }
                msg_count = (byte)((msg_count + 1) & 0xFF);



            }
            catch (Exception error)
            {
                MessageBox.Show("Exception occurred");
                state = Status.error;
                return Status.error;

            }
            //Console.WriteLine("data sent!");
            state = Status.connected;
            return state;
        }


        public Status usb_send(RT7216Q.PWM pwm)
        {
            this.r = pwm.R;
            this.g = pwm.G;
            this.b = pwm.B;
            send_buffer[0] = (byte)version.RT7216Q;//RT7216Q
            send_buffer[9] = (byte)1; //PWM
            send_buffer[10] = (byte)(pwm.R >> 8);
            send_buffer[11] = (byte)(pwm.R & 0xFF);
            send_buffer[12] = (byte)(pwm.G >> 8);
            send_buffer[13] = (byte)(pwm.G & 0xFF);
            send_buffer[14] = (byte)(pwm.B >> 8);
            send_buffer[15] = (byte)(pwm.B & 0xFF);
            send_buffer[16] = (byte)count_en;
            send_buffer[17] = (byte)(count_time / 10);

            return usb_send();
        }

        public Status usb_send(RT7216Q.PRIMARY_REGISTER cmd)
        {
            send_buffer[0] = (byte)version.RT7216Q;//RT7216Q
            send_buffer[2] = 1;
            send_buffer[9] = (byte)2; //primary
            send_buffer[10] = (byte)cmd.DET;
            send_buffer[11] = (byte)cmd.DETL;
            send_buffer[12] = (byte)cmd.IOHL;
            send_buffer[13] = (byte)cmd.TRF;
            send_buffer[14] = (byte)cmd.PWM_MODE_EN;//
            send_buffer[15] = (byte)cmd.THM_MODE_EN;
            send_buffer[16] = (byte)cmd.temp_run_avg_en;
            send_buffer[17] = (byte)cmd.temp_update_freq;
            send_buffer[18] = (byte)cmd.tc_r_en;
            send_buffer[19] = (byte)cmd.tc_g_en;
            send_buffer[20] = (byte)cmd.tc_b_en;
            send_buffer[21] = (byte)cmd.SRST;
            send_buffer[22] = (byte)cmd.OFF_TSD;
            send_buffer[23] = (byte)cmd.OFF_WDG;
            send_buffer[24] = (byte)cmd.OFF_SLP;
            send_buffer[25] = (byte)cmd.OFF_LSD;
            send_buffer[26] = (byte)cmd.OFF_LOD;
            send_buffer[27] = (byte)cmd.OTP_init;
            send_buffer[28] = (byte)cmd.FORCE_D2A_EN_ON;//
            send_buffer[29] = (byte)cmd.FORCE_OSC_EN;//
            send_buffer[30] = (byte)cmd.Dout_SPD;//new
            send_buffer[31] = (byte)cmd.READ_SPD;
            send_buffer[32] = (byte)cmd.ISEL_B;//new
            send_buffer[33] = (byte)cmd.ISEL_G;//new
            send_buffer[34] = (byte)cmd.ISEL_R;//new

            return usb_send();
        }

        public Status usb_send(RT7216Q.MISC misc)
        {
            send_buffer[0] = (byte)version.RT7216Q;//RT7216Q
            send_buffer[2] = 1; //version = B
            send_buffer[9] = (byte)3; // misc

            send_buffer[26] = (byte)((misc.ANALOG_SEL<<2) + (misc.CKO_ANALOG_SEL));
            send_buffer[27] = 0;
            send_buffer[28] = 0;
            send_buffer[29] = 0;
            send_buffer[30] = (byte)misc.ADC_VREF_VOLT;
            send_buffer[31] = 0;
            send_buffer[32] = 0;
            send_buffer[33] = 0;
            send_buffer[34] = (byte)((misc.READ_AVG_THM_IDX << 2) + (misc.READ_NOW_THM_IDX << 1) + misc.READ_USE_THM_IDX);
            send_buffer[35] = 0;
            send_buffer[36] = 0;
            send_buffer[37] = 0;
            send_buffer[38] = (byte)(misc.enable[0]?1:0);
            send_buffer[39] = (byte)(misc.enable[1]?1:0);
            send_buffer[40] = (byte)(misc.enable[2]?1:0);

            return usb_send();
        }

        public Status usb_otp_REG_write(RT7216Q.OTP otp)
        {
            send_buffer[0] = (byte)version.RT7216Q;//RT7216Q
            //pwm_max = (otp[1] << 8) | otp[0];
            send_buffer[2] = (byte)1; //version = B
            send_buffer[9] = (byte)6;//
            for (int i = 0; i < 16; i++)
            {
                send_buffer[10 + i] = (byte)otp[i];
                send_buffer[26 + i] = (byte)(otp.en[i] ? 1 : 0);
            }

            send_buffer[42] = 1;
            send_buffer[43] = 0;
            var state = usb_send();
            if (state != Status.connected) { return state; }

            for (int i = 0; i < 16; i++)
            {
                send_buffer[10 + i] = (byte)otp[i + 16];
                send_buffer[26 + i] = (byte)(otp.en[i + 16] ? 1 : 0);
            }

            send_buffer[42] = 0;
            send_buffer[43] = 1;
            return usb_send();
        }
        public Status usb_otp_REG_read(RT7216Q.OTP otp)
        {
            send_buffer[0] = (byte)version.RT7216Q;//RT7216Q
            send_buffer[2] = (byte)1; //version = B
            send_buffer[9] = (byte)7;

            for (int i = 0; i < 32; i++) { send_buffer[10 + i] = 0; }

            return usb_send();
        }
        public Status usb_otp_MEM_write(RT7216Q.OTP otp)
        {
            int delay = 40;
            send_buffer[0] = (byte)version.RT7216Q;//RT7216Q
            send_buffer[2] = (byte)1; //version = B
            send_buffer[9] = (byte)4;
            for (int i = 0; i < 16; i++)
            {
                send_buffer[10 + i] = (byte)otp[i];
                send_buffer[26 + i] = (byte)(otp.en[i] ? 1 : 0);
                delay += (otp.en[i])?60:0;
            }

            send_buffer[42] = 1;
            send_buffer[43] = 0;
            usb_send();

            Thread.Sleep(delay);
            if (state != Status.connected) { state = Status.error; return state; }

            for (int i = 0; i < 16; i++)
            {
                send_buffer[10 + i] = (byte)otp[i + 16];
                send_buffer[26 + i] = (byte)(otp.en[i + 16] ? 1 : 0);
            }

            send_buffer[42] = 0;
            send_buffer[43] = 1;
            return usb_send();
        }
        public Status usb_otp_MEM_read(RT7216Q.OTP otp)
        {
            send_buffer[0] = (byte)version.RT7216Q;//RT7216Q
            send_buffer[2] = (byte)1; //version = B
            send_buffer[9] = (byte)5;
            for (int i = 0; i < 32; i++) { send_buffer[10 + i] = 0; }
            return usb_send();
        }



        public Status usb_send(RT7216_B.PWM pwm)
        {
            this.r = pwm.R;
            this.g = pwm.G;
            this.b = pwm.B;
            send_buffer[0] = (byte)version.RT7216N_BD;
            send_buffer[9] = (byte) 1; //PWM
            send_buffer[10] = (byte)(pwm.R >> 8);
            send_buffer[11] = (byte)(pwm.R & 0xFF);
            send_buffer[12] = (byte)(pwm.G >> 8);
            send_buffer[13] = (byte)(pwm.G & 0xFF);
            send_buffer[14] = (byte)(pwm.B >> 8);
            send_buffer[15] = (byte)(pwm.B & 0xFF);
            send_buffer[16] = (byte)count_en;
            send_buffer[17] = (byte)(count_time / 10);

            return usb_send();
        }

        public Status usb_send(RT7216_B.PRIMARY_REGISTER cmd)
        {
            send_buffer[0] = (byte)version.RT7216N_BD;
            send_buffer[2] = 1;
            send_buffer[9] = (byte)2; //primary
            send_buffer[10] = (byte)cmd.DET;
            send_buffer[11] = (byte)cmd.DETL;
            send_buffer[12] = (byte)cmd.IOHL;
            send_buffer[13] = (byte)cmd.TRF;
            send_buffer[14] = (byte)cmd.pwm_18bit_mode;
            send_buffer[15] = (byte)cmd.temp_run_avg_en;
            send_buffer[16] = (byte)cmd.temp_update_freq;
            send_buffer[17] = (byte)cmd.tc_r_en;
            send_buffer[18] = (byte)cmd.SRST;
            send_buffer[19] = (byte)cmd.OFF_TSD;
            send_buffer[20] = (byte)cmd.OFF_WDG;
            send_buffer[21] = (byte)cmd.OFF_SLP;
            send_buffer[22] = (byte)cmd.OFF_LSD;
            send_buffer[23] = (byte)cmd.OFF_LOD;
            send_buffer[24] = (byte)cmd.OFF_UVLO;
            send_buffer[25] = (byte)cmd.FORCE_SLP_UVLO;
            send_buffer[26] = (byte)cmd.FORCE_CH_PW_ON;
            send_buffer[27] = (byte)cmd.READ_SPD;
            send_buffer[28] = (byte)cmd.GBC;

            return usb_send();
        }

        public Status usb_send(RT7216_B.MISC misc)
        {
            send_buffer[0] = (byte)version.RT7216N_BD;
            send_buffer[2] = 1; //version = B
            send_buffer[9] = (byte)3; // misc
            send_buffer[26] = (byte)misc.tc_g_base;
            send_buffer[27] = (byte)misc.tc_g_gradient;
            send_buffer[28] = (byte)misc.tc_g_en;
            send_buffer[29] = 0;
            send_buffer[30] = (byte)misc.tc_b_base;
            send_buffer[31] = (byte)misc.tc_b_gradient;
            send_buffer[32] = (byte)misc.tc_b_en;
            send_buffer[33] = 0;
            send_buffer[34] = (byte)misc.read_avg_thm_idx;
            send_buffer[35] = 0;
            send_buffer[36] = 0;
            send_buffer[37] = 0;
            send_buffer[38] = (byte)(misc.enable[0] ? 1 : 0);
            send_buffer[39] =(byte)(misc.enable[1] ? 1 : 0);
            send_buffer[40] = (byte)(misc.enable[2] ? 1 : 0);
            return usb_send();
        }

        public Status usb_otp_REG_write(RT7216_B.OTP otp)
        {
            send_buffer[0] = (byte)version.RT7216N_BD;
            //pwm_max = (otp[1] << 8) | otp[0];
            send_buffer[2] = (byte)1; //version = B
            send_buffer[9] = (byte)6 ;//
            for (int i = 0; i < 16; i++)
            {
                send_buffer[10 + i] = (byte)otp[i];
                send_buffer[26 + i] = (byte)(otp.en[i] ? 1 : 0);
            }

            send_buffer[42] = 1;
            send_buffer[43] = 0;
            var state = usb_send();
            if (state != Status.connected) { return state; }

            for (int i = 0; i < 16; i++)
            {
                send_buffer[10 + i] = (byte)otp[i + 16];
                send_buffer[26 + i] = (byte)(otp.en[i + 16] ? 1 : 0);
            }

            send_buffer[42] = 0;
            send_buffer[43] = 1;
            return usb_send();
        }
        public Status usb_otp_REG_read(RT7216_B.OTP otp)
        {
            send_buffer[0] = (byte)version.RT7216N_BD;
            send_buffer[2] = (byte)1; //version = B
            send_buffer[9] = (byte)7;

            for (int i = 0; i < 32; i++) { send_buffer[10 + i] = 0; }

            return usb_send();
        }
        public Status usb_otp_MEM_write(RT7216_B.OTP otp)
        {
            send_buffer[0] = (byte)version.RT7216N_BD;
            send_buffer[2] = (byte)1; //version = B
            send_buffer[9] = (byte)4;
            for (int i = 0; i < 16; i++)
            {
                send_buffer[10 + i] = (byte)otp[i];
                send_buffer[26 + i] = (byte)(otp.en[i] ? 1 : 0);
            }

            send_buffer[42] = 1;
            send_buffer[43] = 0;
            usb_send();
            while (state != Status.connected) ;

            for (int i = 0; i < 16; i++)
            {
                send_buffer[10 + i] = (byte)otp[i + 16];
                send_buffer[26 + i] = (byte)(otp.en[i + 16] ? 1 : 0);
            }

            send_buffer[42] = 0;
            send_buffer[43] = 1;
            return usb_send();
        }
        public Status usb_otp_MEM_read(RT7216_B.OTP otp)
        {
            send_buffer[0] = (byte)version.RT7216N_BD;
            send_buffer[2] = (byte)1; //version = B
            send_buffer[9] = (byte)5;
            for (int i = 0; i < 32; i++) { send_buffer[10 + i] = 0; }
            return usb_send();
        }



        public Status debug()
        {
            send_buffer[0] = 0xFF;
            return usb_send();
        }

        public Status reset()
        {
            if (open() != Status.connected) { return Status.error; }
            send_buffer[0] = 0xFE;
            var res = usb_send();
            send_buffer[0] = 0;

            Thread.Sleep(500);
            return res;
        }

        internal void set_rgb(int R, int G, int B)
        {
            r = R;
            g = G;
            b = B;

        }
    }

}