using System;
using System.Collections;

/**
 * Implementation of RT7216H by Daniel
 * Use like this:
 * 
 * RT7216H myIC = new RT7216H();
 * 
 * int PWM_R = myIC.pwm.R;
 * int PWM_G = myIC.pwm.G;
 * int PWM_B = myIC.pwm.B;
 * int PWM_W = myIC.pwm.W;
 * 
 * int GBC = myIC.primary_reg.GBC;
 * 
 * myIC.pwm.R = 0x8000;
 * myIC.primary_reg.GBC= 0x1F;
 * 
 * myIC.otp_reg[0] = 0x12;
 * myIC.otp_reg[1] = 0x34; //max_PWM_chip = 0x3412 
 * 
 * myIC.otp_reg.max_PWM_chip = 0xFFFE; 
 * myIC.otp_reg[0] // 0xFE
 * myIC.otp_reg[1] // 0xFF
 */



public class RT7216N_AA
{


    public PWM pwm;
    public PRIMARY_REGISTER primary_reg;
    public MISC misc;
    public OTP otp_reg;
    public OTP otp_memory;

    public RT7216N_AA()
    {
        pwm = new PWM();
        primary_reg = new PRIMARY_REGISTER();
        misc = new MISC();
        otp_reg = new OTP(OTP.Type.register) { };
        otp_memory = new OTP(OTP.Type.memory) { };
    }

    public static BitArray generate_CRC(BitArray p)
    {
        BitArray _values = (BitArray)p.Clone();

        /*int[] CRC_mask0 = new int[] { 0, 3, 5, 6, 9, 10, 11, 12, 13, 17, 18, 20, 21, 22, 24, 26, 31, 34, 36, 37, 40, 41, 42, 43, 44, 48, 49, 51, 52, 53, 55, 57, 62 };
        int[] CRC_mask1 = new int[] { 1, 4, 6, 7, 10, 11, 12, 13, 14, 18, 19, 21, 22, 23, 25, 27, 32, 35, 37, 38, 41, 42, 43, 44, 45, 49, 50, 52, 53, 54, 56, 58, 63 };
        int[] CRC_mask2 = new int[] { 0, 2, 3, 6, 7, 8, 9, 10, 14, 15, 17, 18, 19, 21, 23, 28, 31, 33, 34, 37, 38, 39, 40, 41, 45, 46, 48, 49, 50, 52, 53, 59, 62 };
        int[] CRC_mask3 = new int[] { 1, 3, 4, 7, 8, 9, 10, 11, 15, 16, 18, 19, 20, 22, 24, 29, 32, 34, 35, 38, 39, 40, 41, 42, 46, 47, 49, 50, 51, 53, 55, 60, 63 };
        int[] CRC_mask4 = new int[] { 2, 4, 5, 8, 9, 10, 11, 12, 16, 17, 19, 20, 21, 23, 25, 30, 33, 35, 36, 39, 40, 41, 42, 43, 47, 48, 50, 51, 52, 54, 56, 61 };
        bool bit;
        bit = true;     foreach (int pos in CRC_mask0) { bit = bit ^ _values[pos]; }    _values[67] = bit;
        bit = true;     foreach (int pos in CRC_mask1) { bit = bit ^ _values[pos]; }    _values[68] = bit;
        bit = false;    foreach (int pos in CRC_mask2) { bit = bit ^ _values[pos]; }    _values[69] = bit;
        bit = false;    foreach (int pos in CRC_mask3) { bit = bit ^ _values[pos]; }    _values[70] = bit;
        bit = true;     foreach (int pos in CRC_mask4) { bit = bit ^ _values[pos]; }    _values[71] = bit;*/
        int[] CRC_mask0 = new int[25] { 0, 3, 5, 6, 9, 10, 11, 12, 13, 17, 18, 20, 21, 22, 24, 26, 31, 34, 36, 37, 40, 41, 42, 43, 44 };
        int[] CRC_mask1 = new int[25] { 1, 4, 6, 7, 10, 11, 12, 13, 14, 18, 19, 21, 22, 23, 25, 27, 32, 35, 37, 38, 41, 42, 43, 44, 45 };
        int[] CRC_mask2 = new int[26] { 0, 2, 3, 6, 7, 8, 9, 10, 14, 15, 17, 18, 19, 21, 23, 28, 31, 33, 34, 37, 38, 39, 40, 41, 45, 46 };
        int[] CRC_mask3 = new int[26] { 1, 3, 4, 7, 8, 9, 10, 11, 15, 16, 18, 19, 20, 22, 24, 29, 32, 34, 35, 38, 39, 40, 41, 42, 46, 47 };
        int[] CRC_mask4 = new int[25] { 2, 4, 5, 8, 9, 10, 11, 12, 16, 17, 19, 20, 21, 23, 25, 30, 33, 35, 36, 39, 40, 41, 42, 43, 47 };
        bool bit;
        bit = false; foreach (int pos in CRC_mask0) { bit = bit ^ _values[pos]; } _values[51] = bit;
        bit = true; foreach (int pos in CRC_mask1) { bit = bit ^ _values[pos]; } _values[52] = bit;
        bit = false; foreach (int pos in CRC_mask2) { bit = bit ^ _values[pos]; } _values[53] = bit;
        bit = false; foreach (int pos in CRC_mask3) { bit = bit ^ _values[pos]; } _values[54] = bit;
        bit = false; foreach (int pos in CRC_mask4) { bit = bit ^ _values[pos]; } _values[55] = bit;


        return _values;
    }
    public static byte[] toBytes(BitArray p)
    {
        /*        Packet                     Bytes
         *        0 1 2 3  4 5 6 7        //: 7170696867666564                     
         *        8 91011 12131415        //: 6362616059585756
         *       16171819 20212223        0: 5554535251504948
         *       24252627 28293031        1: 4746454443424140
         *       32333435 36373839        2: 3938373635343332
         *       40414243 44454647        3: 3130292827262524
         *       48495051 52535455        4: 2322212019181716
         *       56575859 60616263        5: 151413121110 9 8
         *       64656667 68697071        6:  7 6 5 4 3 2 1 0
         */

        byte[] msg = new byte[7];
        for (int i = 0; i < 7; i++)
        {
            if (p[i * 8 + 0]) { msg[6 - i] += 0x01; }
            if (p[i * 8 + 1]) { msg[6 - i] += 0x02; }
            if (p[i * 8 + 2]) { msg[6 - i] += 0x04; }
            if (p[i * 8 + 3]) { msg[6 - i] += 0x08; }
            if (p[i * 8 + 4]) { msg[6 - i] += 0x10; }
            if (p[i * 8 + 5]) { msg[6 - i] += 0x20; }
            if (p[i * 8 + 6]) { msg[6 - i] += 0x40; }
            if (p[i * 8 + 7]) { msg[6 - i] += 0x80; }
        }
        return msg;
    }

    public static int limit(int value, int max, int min = 0)
    {
        return (min < value) ? (value <= max) ? value : max : min;
    }

    public class PWM
    {
        public int R = 0,
                    G = 0,
                    B = 0;
        private int max = 0xFFFF;

        public PWM() { }
        public PWM(int r, int g, int b)
        {
            if (r < 0) { R = 0; } else if (r > max) { R = max; } else { R = r; }
            if (g < 0) { G = 0; } else if (g > max) { G = max; } else { G = g; }
            if (b < 0) { B = 0; } else if (b > max) { B = max; } else { B = b; }
        }
        public PWM(PWM pwm)
        {
            this.R = pwm.R;
            this.G = pwm.G;
            this.B = pwm.B;
        }

        public void set(int r, int g, int b, int w)
        {
            R = ((0 < r) && (r <= max)) ? r : 0;
            G = ((0 < g) && (g <= max)) ? g : 0;
            B = ((0 < b) && (b <= max)) ? b : 0;
        }
        public BitArray toPacket()
        {
            BitArray p = new BitArray(56, false);

            int r = this.R,
                g = this.G,
                b = this.B;

            p[48] = true;
            p[49] = false;
            p[50] = false;

            p[0 + 0] = (b & 0x0001) == 0x0001; p[8] = (b & 0x0100) == 0x0100;
            p[1 + 0] = (b & 0x0002) == 0x0002; p[9] = (b & 0x0200) == 0x0200;
            p[2 + 0] = (b & 0x0004) == 0x0004; p[10] = (b & 0x0400) == 0x0400;
            p[3 + 0] = (b & 0x0008) == 0x0008; p[11] = (b & 0x0800) == 0x0800;
            p[4 + 0] = (b & 0x0010) == 0x0010; p[12] = (b & 0x1000) == 0x1000;
            p[5 + 0] = (b & 0x0020) == 0x0020; p[13] = (b & 0x2000) == 0x2000;
            p[6 + 0] = (b & 0x0040) == 0x0040; p[14] = (b & 0x4000) == 0x4000;
            p[7 + 0] = (b & 0x0080) == 0x0080; p[15] = (b & 0x8000) == 0x8000;

            p[0 + 16] = (g & 0x0001) == 0x0001; p[8 + 16] = (g & 0x0100) == 0x0100;
            p[1 + 16] = (g & 0x0002) == 0x0002; p[9 + 16] = (g & 0x0200) == 0x0200;
            p[2 + 16] = (g & 0x0004) == 0x0004; p[10 + 16] = (g & 0x0400) == 0x0400;
            p[3 + 16] = (g & 0x0008) == 0x0008; p[11 + 16] = (g & 0x0800) == 0x0800;
            p[4 + 16] = (g & 0x0010) == 0x0010; p[12 + 16] = (g & 0x1000) == 0x1000;
            p[5 + 16] = (g & 0x0020) == 0x0020; p[13 + 16] = (g & 0x2000) == 0x2000;
            p[6 + 16] = (g & 0x0040) == 0x0040; p[14 + 16] = (g & 0x4000) == 0x4000;
            p[7 + 16] = (g & 0x0080) == 0x0080; p[15 + 16] = (g & 0x8000) == 0x8000;

            p[0 + 32] = (r & 0x0001) == 0x0001; p[8 + 32] = (r & 0x0100) == 0x0100;
            p[1 + 32] = (r & 0x0002) == 0x0002; p[9 + 32] = (r & 0x0200) == 0x0200;
            p[2 + 32] = (r & 0x0004) == 0x0004; p[10 + 32] = (r & 0x0400) == 0x0400;
            p[3 + 32] = (r & 0x0008) == 0x0008; p[11 + 32] = (r & 0x0800) == 0x0800;
            p[4 + 32] = (r & 0x0010) == 0x0010; p[12 + 32] = (r & 0x1000) == 0x1000;
            p[5 + 32] = (r & 0x0020) == 0x0020; p[13 + 32] = (r & 0x2000) == 0x2000;
            p[6 + 32] = (r & 0x0040) == 0x0040; p[14 + 32] = (r & 0x4000) == 0x4000;
            p[7 + 32] = (r & 0x0080) == 0x0080; p[15 + 32] = (r & 0x8000) == 0x8000;

            return p;
        }
        public byte[] toBytes()
        {
            BitArray p = this.toPacket();
            p = RT7216Q.generate_CRC(p);
            return RT7216Q.toBytes(p);
        }
    }
    public class PRIMARY_REGISTER
    {
        public int GBC = 0x3F,
                    READ_SPD = 0,
                    phase_dly = 0, 

                    OFF_LOD = 0,
                    OFF_LSD = 0,
                    OFF_SLP = 0,
                    OFF_WDG = 0,
                    OFF_TSD = 0,
                    SRST = 0,
                    tc_r_en = 0,        
                    temp_update_freq = 0,
                    TRF = 1,
                    IOHL = 2,
                    DETL = 2,
                    DET = 0;

        public PRIMARY_REGISTER() { }
        public BitArray toPacket()
        {
            BitArray p = new BitArray(56, false);

            p[48] = false;
            p[49] = true;
            p[50] = false;

            p[0] = (this.GBC & 0x01) == 0x01;
            p[1] = (this.GBC & 0x02) == 0x02;
            p[2] = (this.GBC & 0x04) == 0x04;
            p[3] = (this.GBC & 0x08) == 0x08;
            p[4] = (this.GBC & 0x10) == 0x10;


            p[8] = (this.READ_SPD & 0x01) == 0x01;
            p[9] = (this.READ_SPD & 0x02) == 0x02;
            p[10] = (this.READ_SPD & 0x04) == 0x04;

            p[16] = (this.phase_dly & 0x01) == 0x01;
            p[17] = (this.phase_dly & 0x02) == 0x02;

            p[20] = (this.OFF_LOD & 0x01) == 0x01;
            p[21] = (this.OFF_LSD & 0x01) == 0x01;
            p[22] = (this.OFF_SLP & 0x01) == 0x01;
            p[23] = (this.OFF_WDG & 0x01) == 0x01;

            p[24] = (this.OFF_TSD & 0x01) == 0x01;
            p[25] = (this.SRST & 0x01) == 0x01;

            p[31] = (this.tc_r_en & 0x01) == 0x01;
            p[32] = (this.temp_update_freq & 0x01) == 0x01;
            p[33] = (this.temp_update_freq & 0x02) == 0x02;
            p[34] = (this.temp_update_freq & 0x04) == 0x04;

            p[41] = (this.TRF & 0x01) == 0x01;
            p[42] = (this.IOHL & 0x01) == 0x01;
            p[43] = (this.IOHL & 0x02) == 0x02;
            p[44] = (this.DETL & 0x01) == 0x01;
            p[45] = (this.DETL & 0x02) == 0x02;
            p[46] = (this.DET & 0x01) == 0x01;
            p[47] = (this.DET & 0x02) == 0x02;

            return p;
        }
        public byte[] toBytes()
        {
            BitArray p = this.toPacket();
            p = RT7216Q.generate_CRC(p);
            return RT7216Q.toBytes(p);
        }
    }
    public class MISC
    {
        public int[] _tc_fine_thermal_idx = new int[12];

        //public int[] reg = new int[3];
        public bool[] enable = new bool[3];

        public MISC() { }
        //public int this[int i]
        //{
        //    get
        //    {
        //        switch (i)
        //        {
        //            case 0:return (_tc_fine_thermal_idx[3] << 24) + (_tc_fine_thermal_idx[2] << 16) + (_tc_fine_thermal_idx[1] <<8)+(_tc_fine_thermal_idx[0] << 0);
        //            case 1: return (_tc_fine_thermal_idx[7] << 24) + (_tc_fine_thermal_idx[6] << 16) + (_tc_fine_thermal_idx[5] << 8) + (_tc_fine_thermal_idx[4] << 0);
        //            case 2: return (_tc_fine_thermal_idx[11] << 24) + (_tc_fine_thermal_idx[10] << 16) + (_tc_fine_thermal_idx[9] << 8) + (_tc_fine_thermal_idx[8] << 0);
        //            default: return 0;
        //        }
        //    }
        //    set
        //    {
        //        switch (i)
        //        {
        //            case 0: reg[0] = limit(value, 0xFFFFFFF, 0);// (0 < value) ? (value < 0xFFFFFFF) ? value : 0xFFFFFFF : 0;
        //                tc_g_base = (reg[0] & 0xFF0) >> 4;
        //                tc_g_gradient = (reg[0] & 0xFF000) >> 12;
        //                tc_g_en = (reg[0] & 0x100000) >> 20; break;
        //            case 1: reg[1] = limit(value, 0xFFFFFFF, 0);
        //                tc_b_base = (reg[1] & 0xFF0) >> 4;
        //                tc_b_gradient = (reg[1] & 0xFF000) >> 12;
        //                tc_b_en = (reg[1] & 0x100000) >> 20; break;
        //            case 2: reg[2] = limit(value, 0xFFFFFFF, 0);
        //                read_avg_thm_idx = (reg[2] & 0x0010) >> 4; break;
        //            default: return;
        //        }
        //    }
        //}
        public BitArray toPacket(int addr)
        {
            BitArray p = new BitArray(56, false);
            p[48] = true;
            p[49] = true;
            p[50] = false;

            switch (addr)
            {
                case 0:
                    p[16] = (_tc_fine_thermal_idx[0] & 0x01) == 0x01; p[16+8] = (_tc_fine_thermal_idx[1] & 0x01) == 0x01; p[16+16] = (_tc_fine_thermal_idx[2] & 0x01) == 0x01; p[16+24] = (_tc_fine_thermal_idx[3] & 0x01) == 0x01;
                    p[17] = (_tc_fine_thermal_idx[0] & 0x02) == 0x02; p[17+8] = (_tc_fine_thermal_idx[1] & 0x02) == 0x02; p[17+16] = (_tc_fine_thermal_idx[2] & 0x02) == 0x02; p[17+24] = (_tc_fine_thermal_idx[3] & 0x02) == 0x02;
                    p[18] = (_tc_fine_thermal_idx[0] & 0x04) == 0x04; p[18+8] = (_tc_fine_thermal_idx[1] & 0x04) == 0x04; p[18+16] = (_tc_fine_thermal_idx[2] & 0x04) == 0x04; p[18+24] = (_tc_fine_thermal_idx[3] & 0x04) == 0x04;
                    p[19] = (_tc_fine_thermal_idx[0] & 0x08) == 0x08; p[19+8] = (_tc_fine_thermal_idx[1] & 0x08) == 0x08; p[19+16] = (_tc_fine_thermal_idx[2] & 0x08) == 0x08; p[19+24] = (_tc_fine_thermal_idx[3] & 0x08) == 0x08;
                    p[20] = (_tc_fine_thermal_idx[0] & 0x10) == 0x10; p[20+8] = (_tc_fine_thermal_idx[1] & 0x10) == 0x10; p[20+16] = (_tc_fine_thermal_idx[2] & 0x10) == 0x10; p[20+24] = (_tc_fine_thermal_idx[3] & 0x10) == 0x10;
                    p[21] = (_tc_fine_thermal_idx[0] & 0x20) == 0x20; p[21+8] = (_tc_fine_thermal_idx[1] & 0x20) == 0x20; p[21+16] = (_tc_fine_thermal_idx[2] & 0x20) == 0x20; p[21+24] = (_tc_fine_thermal_idx[3] & 0x20) == 0x20;
                    p[22] = (_tc_fine_thermal_idx[0] & 0x40) == 0x40; p[22+8] = (_tc_fine_thermal_idx[1] & 0x40) == 0x40; p[22+16] = (_tc_fine_thermal_idx[2] & 0x40) == 0x40; p[22+24] = (_tc_fine_thermal_idx[3] & 0x40) == 0x40;
                    p[23] = (_tc_fine_thermal_idx[0] & 0x80) == 0x80; p[23+8] = (_tc_fine_thermal_idx[1] & 0x80) == 0x80; p[23+16] = (_tc_fine_thermal_idx[2] & 0x80) == 0x80; p[23+24] = (_tc_fine_thermal_idx[3] & 0x80) == 0x80;
                    break;
                case 1:
                    p[16] = (_tc_fine_thermal_idx[4] & 0x01) == 0x01; p[16+8] = (_tc_fine_thermal_idx[5] & 0x01) == 0x01; p[16+16] = (_tc_fine_thermal_idx[6] & 0x01) == 0x01;  p[16+24] = (_tc_fine_thermal_idx[7] & 0x01) == 0x01;
                    p[17] = (_tc_fine_thermal_idx[4] & 0x02) == 0x02; p[17+8] = (_tc_fine_thermal_idx[5] & 0x02) == 0x02; p[17+16] = (_tc_fine_thermal_idx[6] & 0x02) == 0x02;  p[17+24] = (_tc_fine_thermal_idx[7] & 0x02) == 0x02;
                    p[18] = (_tc_fine_thermal_idx[4] & 0x04) == 0x04; p[18+8] = (_tc_fine_thermal_idx[5] & 0x04) == 0x04; p[18+16] = (_tc_fine_thermal_idx[6] & 0x04) == 0x04;  p[18+24] = (_tc_fine_thermal_idx[7] & 0x04) == 0x04;
                    p[19] = (_tc_fine_thermal_idx[4] & 0x08) == 0x08; p[19+8] = (_tc_fine_thermal_idx[5] & 0x08) == 0x08; p[19+16] = (_tc_fine_thermal_idx[6] & 0x08) == 0x08;  p[19+24] = (_tc_fine_thermal_idx[7] & 0x08) == 0x08;
                    p[20] = (_tc_fine_thermal_idx[4] & 0x10) == 0x10; p[20+8] = (_tc_fine_thermal_idx[5] & 0x10) == 0x10; p[20+16] = (_tc_fine_thermal_idx[6] & 0x10) == 0x10;  p[20+24] = (_tc_fine_thermal_idx[7] & 0x10) == 0x10;
                    p[21] = (_tc_fine_thermal_idx[4] & 0x20) == 0x20; p[21+8] = (_tc_fine_thermal_idx[5] & 0x20) == 0x20; p[21+16] = (_tc_fine_thermal_idx[6] & 0x20) == 0x20;  p[21+24] = (_tc_fine_thermal_idx[7] & 0x20) == 0x20;
                    p[22] = (_tc_fine_thermal_idx[4] & 0x40) == 0x40; p[22+8] = (_tc_fine_thermal_idx[5] & 0x40) == 0x40; p[22+16] = (_tc_fine_thermal_idx[6] & 0x40) == 0x40;  p[22+24] = (_tc_fine_thermal_idx[7] & 0x40) == 0x40;
                    p[23] = (_tc_fine_thermal_idx[4] & 0x80) == 0x80; p[23+8] = (_tc_fine_thermal_idx[5] & 0x80) == 0x80; p[23+16] = (_tc_fine_thermal_idx[6] & 0x80) == 0x80;  p[23+24] = (_tc_fine_thermal_idx[7] & 0x80) == 0x80;
                    break;
                case 2:
                    p[16] = (_tc_fine_thermal_idx[8] & 0x01) == 0x01; p[16+8] = (_tc_fine_thermal_idx[9] & 0x01) == 0x01; p[16+16] = (_tc_fine_thermal_idx[10] & 0x01) == 0x01; p[16+24] = (_tc_fine_thermal_idx[11] & 0x01) == 0x01;
                    p[17] = (_tc_fine_thermal_idx[8] & 0x02) == 0x02; p[17+8] = (_tc_fine_thermal_idx[9] & 0x02) == 0x02; p[17+16] = (_tc_fine_thermal_idx[10] & 0x02) == 0x02; p[17+24] = (_tc_fine_thermal_idx[11] & 0x02) == 0x02;
                    p[18] = (_tc_fine_thermal_idx[8] & 0x04) == 0x04; p[18+8] = (_tc_fine_thermal_idx[9] & 0x04) == 0x04; p[18+16] = (_tc_fine_thermal_idx[10] & 0x04) == 0x04; p[18+24] = (_tc_fine_thermal_idx[11] & 0x04) == 0x04;
                    p[19] = (_tc_fine_thermal_idx[8] & 0x08) == 0x08; p[19+8] = (_tc_fine_thermal_idx[9] & 0x08) == 0x08; p[19+16] = (_tc_fine_thermal_idx[10] & 0x08) == 0x08; p[19+24] = (_tc_fine_thermal_idx[11] & 0x08) == 0x08;
                    p[20] = (_tc_fine_thermal_idx[8] & 0x10) == 0x10; p[20+8] = (_tc_fine_thermal_idx[9] & 0x10) == 0x10; p[20+16] = (_tc_fine_thermal_idx[10] & 0x10) == 0x10; p[20+24] = (_tc_fine_thermal_idx[11] & 0x10) == 0x10;
                    p[21] = (_tc_fine_thermal_idx[8] & 0x20) == 0x20; p[21+8] = (_tc_fine_thermal_idx[9] & 0x20) == 0x20; p[21+16] = (_tc_fine_thermal_idx[10] & 0x20) == 0x20; p[21+24] = (_tc_fine_thermal_idx[11] & 0x20) == 0x20;
                    p[22] = (_tc_fine_thermal_idx[8] & 0x40) == 0x40; p[22+8] = (_tc_fine_thermal_idx[9] & 0x40) == 0x40; p[22+16] = (_tc_fine_thermal_idx[10] & 0x40) == 0x40; p[22+24] = (_tc_fine_thermal_idx[11] & 0x40) == 0x40;
                    p[23] = (_tc_fine_thermal_idx[8] & 0x80) == 0x80; p[23+8] = (_tc_fine_thermal_idx[9] & 0x80) == 0x80; p[23+16] = (_tc_fine_thermal_idx[10] & 0x80) == 0x80; p[23+24] = (_tc_fine_thermal_idx[11] & 0x80) == 0x80;
                    break;
                default: break;
            }

            p[8] = (addr & 0x01) == 0x01;  ///test git 1 2 3 4  
            p[9] = (addr & 0x02) == 0x02;
            p[10] = (addr & 0x04) == 0x04;   
            p[11] = (addr & 0x08) == 0x08;

            return p;
        }
        public byte[] toBytes(int addr)
        {
            BitArray p = this.toPacket(addr);
            p = RT7216Q.generate_CRC(p);
            return RT7216Q.toBytes(p);
        }
    }
    public class OTP
    {
        public enum Type { memory = 0, register = 1 }
        private Type type;

        private int _max_PWM_chip = 0xFFFF;
        private int _PWM_scalar_R = 0x0100;
        private int _PWM_scalar_G = 0x0100;
        private int _PWM_scalar_B = 0x0100;
        private int _CR_R = 0x00;
        private int _CR_G = 0x00;
        private int _CR_B = 0x00;
        private int _tc_r_base = 0x00;          //temp_comp_base
        private int _tc_r_gradient = 0x00;      //temp_comp_diff
        private int _IOUT_trim = 0x00;
        private int _thermal_gradient = 0x0;
        private int _V_THD_Trim = 0;
        private int _OSC_Freq_Trim = 0x00;
        private int _thermal_bias = 0x00;
        private int[] _tc_fine_thermal_idx = new int[12];
        private int _tc_fine_shift3_0 = 0x0;
        private int _tc_fine_shift7_4 = 0x0;
        private int _tc_fine_shift11_8 = 0x0;
        private int _otp_internal_lock = 0,
                    _otp_program_lock = 0;

        public int max_PWM_chip { get { return _max_PWM_chip; } set { _max_PWM_chip = limit(value, 0xFFFF, 0); } }
        public int PWM_scalar_R { get { return _PWM_scalar_R; } set { _PWM_scalar_R = limit(value, 0xFFFF, 0); } }
        public int PWM_scalar_G { get { return _PWM_scalar_G; } set { _PWM_scalar_G = limit(value, 0xFFFF, 0); } }
        public int PWM_scalar_B { get { return _PWM_scalar_B; } set { _PWM_scalar_B = limit(value, 0xFFFF, 0); } }

        public int tc_r_base { get { return _tc_r_base; } set { _tc_r_base = limit(value, 0xFF, 0); } }
        public int tc_r_gradient { get { return _tc_r_gradient; } set { _tc_r_gradient = limit(value, 0xFF, 0); } }

        public int CR_R { get { return _CR_R; } set { _CR_R = limit(value, 0xFF, 0); reg[8] = _CR_R; } }
        public int CR_G { get { return _CR_G; } set { _CR_G = limit(value, 0xFF, 0); reg[9] = _CR_G; } }
        public int CR_B { get { return _CR_B; } set { _CR_B = limit(value, 0xFF, 0); reg[10] = _CR_B; } }

        public int IOUT_trim { get { return _IOUT_trim; } set { _IOUT_trim = limit(value, 63, 0); reg[13] = (reg[13] & 0xC0) + (_IOUT_trim & 0x3F); } }
        public int thermal_gradient { get { return _thermal_gradient; } set { _thermal_gradient = limit(value, 7, 0); reg[13] = (reg[13] & 0x3F) + ((_thermal_gradient & 0x03) << 6); reg[14] = (reg[14] & 0xFE) + ((_thermal_gradient & 0x04) >> 2); } }
        public int V_THD_Trim { get { return _V_THD_Trim; } set { _V_THD_Trim = limit(value, 15, 0); reg[14] = (reg[14] & 0xE1) + ((_V_THD_Trim & 0x0F) << 1); } }
        public int OSC_Freq_Trim { get { return _OSC_Freq_Trim; } set { _OSC_Freq_Trim = limit(value, 7, 0); reg[14] = (reg[14] & 0x1F) + ((_OSC_Freq_Trim & 0x07) << 5); } }
        public int thermal_bias { get { return _thermal_bias; } set { _thermal_bias = limit(value, 63, 0); reg[15] = (reg[15] & 0xC0) + (_thermal_bias & 0x3F); } }


        public int tc_fine_shift11_8 { get { return _tc_fine_shift11_8; } set { _tc_fine_shift11_8 = limit(value, 255, 0); reg[30] = (reg[30] & 0xCF) + ((_tc_fine_shift11_8 & 3) << 4); } }
        public int tc_fine_shift7_4 { get { return _tc_fine_shift7_4; } set { _tc_fine_shift7_4 = limit(value, 255, 0); reg[30] = (reg[30] & 0xF3) + ((_tc_fine_shift7_4 & 3) << 2); } }
        public int tc_fine_shift3_0 { get { return _tc_fine_shift3_0; } set { _tc_fine_shift3_0 = limit(value, 255, 0); reg[30] = (reg[30] & 0xFC) + ((_tc_fine_shift3_0 & 3) << 0); } }
        public int[] tc_fine_thermal_idx
        {
            get { return _tc_fine_thermal_idx; }
            set { tc_fine_thermal_idx = value; }
        }


        public int otp_internal_lock { get { return _otp_internal_lock; } set { _otp_internal_lock = limit(value, 1, 0); reg[31] = (reg[31] & 0xFE) + (_otp_internal_lock & 1); } }
        public int otp_program_lock { get { return _otp_program_lock; } set { _otp_program_lock = limit(value, 1, 0); reg[31] = (reg[31] & 0xEF) + ((_otp_program_lock & 1) << 4); } }
        // This is another way of visualing how OTP stores values
        // OTP[0] = max_PWM_chip_lo
        // OTP[1] = max_PWM_chip_hi 
        // ...

        public int[] reg = new int[32]; //1~7,13,14,31
        public bool[] en = new bool[32];

        public OTP(Type t = Type.register) { type = t; reg[5] = 21; }
        public int this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return (_max_PWM_chip & 0x00FF);
                    case 1: return (_max_PWM_chip & 0xFF00) >> 8;
                    case 2: return (_PWM_scalar_R & 0x00FF);
                    case 3: return (_PWM_scalar_R & 0xFF00) >> 8;
                    case 4: return (_PWM_scalar_G & 0x00FF);
                    case 5: return (_PWM_scalar_G & 0xFF00) >> 8;
                    case 6: return (_PWM_scalar_B & 0x00FF);
                    case 7: return (_PWM_scalar_B & 0xFF00) >> 8;
                    case 8: return _CR_R;
                    case 9: return _CR_G;
                    case 10: return _CR_B;
                    case 11: return _tc_r_base & 0xFF;
                    case 12: return _tc_r_gradient & 0xFF;
                    case 13: return reg[13];//IOUT_trim & 0x1F;//(((thermal_gradient & 0x3) << 6) + IOUT_trim); // +(thermal_gradient & 0x3) << 6; //need testing
                    case 14: return reg[14];//OSC_Freq_Trim & 0x1F;
                    case 15: return reg[15];
                    case 16: return _tc_fine_thermal_idx[0];
                    case 17: return _tc_fine_thermal_idx[1];
                    case 18: return _tc_fine_thermal_idx[2];
                    case 19: return _tc_fine_thermal_idx[3];
                    case 20: return _tc_fine_thermal_idx[4];
                    case 21: return _tc_fine_thermal_idx[5];
                    case 22: return _tc_fine_thermal_idx[6];
                    case 23: return _tc_fine_thermal_idx[7];
                    case 24: return _tc_fine_thermal_idx[8];
                    case 25: return _tc_fine_thermal_idx[9];
                    case 26: return _tc_fine_thermal_idx[10];
                    case 27: return _tc_fine_thermal_idx[11];
                    case 28: return _tc_fine_shift3_0;
                    case 29: return _tc_fine_shift7_4;
                    case 30: return _tc_fine_shift11_8;
                    case 31: return reg[31];//((otp_internal_lock & 0x01) + (otp_program_lock << 4));
                    default: return 0;
                }
            }
            set
            {
                switch (i)
                {
                    case 0: _max_PWM_chip = (_max_PWM_chip & 0xFF00) + (limit(value, 255) << 0); break;
                    case 1: _max_PWM_chip = (_max_PWM_chip & 0x00FF) + (limit(value, 255) << 8); break;
                    case 2: _PWM_scalar_R = (_PWM_scalar_R & 0xFF00) + (limit(value, 255) << 0); break;
                    case 3: _PWM_scalar_R = (_PWM_scalar_R & 0x00FF) + (limit(value, 255) << 8); break;
                    case 4: _PWM_scalar_G = (_PWM_scalar_G & 0xFF00) + (limit(value, 255) << 0); break;
                    case 5: _PWM_scalar_G = (_PWM_scalar_G & 0x00FF) + (limit(value, 255) << 8); break;
                    case 6: _PWM_scalar_B = (_PWM_scalar_B & 0xFF00) + (limit(value, 255) << 0); break;
                    case 7: _PWM_scalar_B = (_PWM_scalar_B & 0x00FF) + (limit(value, 255) << 8); break;
                    case 8: _CR_R = limit(value, 255); break;
                    case 9: _CR_G = limit(value, 255); break;
                    case 10: _CR_B = limit(value, 255); break;
                    case 11: _tc_r_base = limit(value, 255); break;
                    case 12: _tc_r_gradient = limit(value, 255); break;
                    case 13: reg[13] = limit(value, 255);
                        IOUT_trim = reg[13] & 0x3F;
                        _thermal_gradient = (_thermal_gradient & 4) + (reg[13] >> 6); break;
                    case 14: reg[14] = limit(value, 255);
                        _thermal_gradient = (_thermal_gradient & 3) + ((reg[14] & 0x1) << 2);
                        _V_THD_Trim = (reg[14] >> 1) & 0xF;
                        _OSC_Freq_Trim = (reg[14] >> 5) & 0x7; break;
                    case 15: reg[15] = limit(value, 255); thermal_bias = reg[15]; break;
                    case 16: _tc_fine_thermal_idx[0] = limit(value, 255); break;
                    case 17: _tc_fine_thermal_idx[1] = limit(value, 255); break;
                    case 18: _tc_fine_thermal_idx[2] = limit(value, 255); break;
                    case 19: _tc_fine_thermal_idx[3] = limit(value, 255); break;
                    case 20: _tc_fine_thermal_idx[4] = limit(value, 255); break;
                    case 21: _tc_fine_thermal_idx[5] = limit(value, 255); break;
                    case 22: _tc_fine_thermal_idx[6] = limit(value, 255); break;
                    case 23: _tc_fine_thermal_idx[7] = limit(value, 255); break;
                    case 24: _tc_fine_thermal_idx[8] = limit(value, 255); break;
                    case 25: _tc_fine_thermal_idx[9] = limit(value, 255); break;
                    case 26: _tc_fine_thermal_idx[10] = limit(value, 255); break;
                    case 27: _tc_fine_thermal_idx[11] = limit(value, 255); break;
                    case 28: _tc_fine_shift3_0 = limit(value, 255); break;
                    case 29: _tc_fine_shift7_4 = limit(value, 255); break;
                    case 30: _tc_fine_shift11_8 = limit(value, 255); break;
                    case 31: reg[31] = limit(value, 255);
                        _otp_internal_lock = reg[31] & 0x1;
                        _otp_program_lock = (reg[31] & 0x10) >> 4;
                        break;
                    default: return;
                }
            }
        }
        public BitArray toPacket_write(int addr)
        {
            BitArray p = new BitArray(56, false);
            int val = this[addr];

            p[48] = false;
            p[49] = (this.type == Type.register) ? true : false;
            p[50] = true;

            p[0] = (val & 0x01) == 0x01;
            p[1] = (val & 0x02) == 0x02;
            p[2] = (val & 0x04) == 0x04;
            p[3] = (val & 0x08) == 0x08;
            p[4] = (val & 0x10) == 0x10;
            p[5] = (val & 0x20) == 0x20;
            p[6] = (val & 0x40) == 0x40;
            p[7] = (val & 0x80) == 0x80;

            p[8] = (addr & 0x01) == 0x01;
            p[9] = (addr & 0x02) == 0x02;
            p[10] = (addr & 0x04) == 0x04;
            p[11] = (addr & 0x08) == 0x08;
            p[12] = (addr & 0x10) == 0x10;
            return p;
        }
        public BitArray toPacket_read(int addr)
        {
            BitArray p = new BitArray(56, false);

            p[48] = true;
            p[49] = (this.type == Type.register) ? true : false;
            p[50] = true;

            p[8] = (addr & 0x01) == 0x01;
            p[9] = (addr & 0x02) == 0x02;
            p[10] = (addr & 0x04) == 0x04;
            p[11] = (addr & 0x08) == 0x08;
            p[12] = (addr & 0x10) == 0x10;
            return p;
        }
        public byte[] toBytes_write(int addr)
        {
            BitArray p = this.toPacket_write(addr);
            p = RT7216Q.generate_CRC(p);
            return RT7216Q.toBytes(p);
        }
        public byte[] toBytes_read(int addr)
        {
            BitArray p = this.toPacket_read(addr);
            p = RT7216Q.generate_CRC(p);
            return RT7216Q.toBytes(p);
        }
    }
}


