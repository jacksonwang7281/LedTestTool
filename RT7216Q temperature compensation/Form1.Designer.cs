namespace RT7216Q_temperature_compensation
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();


            this.rT7216Q_Control1 = new RT7216Q_temperature_compensation.Classes.RT7216Q_Control();  //jackson


            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rT7216N_BD_Control1 = new RT7216Q_temperature_compensation.Classes.RT7216N_BD_Control();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.temperatureCompensation1 = new RT7216Q_temperature_compensation.Classes.TemperatureCompensation();
            this.flowLayoutPanel_Comports = new System.Windows.Forms.FlowLayoutPanel();
            this.label_SerialPort = new System.Windows.Forms.Label();
            this.button_RefreshPorts = new System.Windows.Forms.Button();
            this.dataGridView_Pulse = new System.Windows.Forms.DataGridView();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pulse = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column_Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numericUpDown_IC_count = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.檔案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.匯入檔案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Pulse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_IC_count)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Font = new System.Drawing.Font("PMingLiU", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabControl1.Location = new System.Drawing.Point(254, 23);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1094, 510);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.rT7216Q_Control1);
            this.tabPage1.Location = new System.Drawing.Point(4, 42);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1086, 464);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "RT7216Q Control";
            // 
            // rT7216Q_Control1
            // 
            this.rT7216Q_Control1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rT7216Q_Control1.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rT7216Q_Control1.Location = new System.Drawing.Point(3, 3);
            this.rT7216Q_Control1.Name = "rT7216Q_Control1";
            this.rT7216Q_Control1.Size = new System.Drawing.Size(1080, 458);
            this.rT7216Q_Control1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.rT7216N_BD_Control1);
            this.tabPage2.Location = new System.Drawing.Point(4, 42);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1086, 464);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "RT7216N_BD Control";
            // 
            // rT7216N_BD_Control1
            // 
            this.rT7216N_BD_Control1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rT7216N_BD_Control1.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rT7216N_BD_Control1.Location = new System.Drawing.Point(3, 3);
            this.rT7216N_BD_Control1.Name = "rT7216N_BD_Control1";
            this.rT7216N_BD_Control1.Size = new System.Drawing.Size(1080, 458);
            this.rT7216N_BD_Control1.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.temperatureCompensation1);
            this.tabPage4.Location = new System.Drawing.Point(4, 42);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1086, 464);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Temperature Compensation";
            // 
            // temperatureCompensation1
            // 
            this.temperatureCompensation1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.temperatureCompensation1.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.temperatureCompensation1.Location = new System.Drawing.Point(3, 3);
            this.temperatureCompensation1.Name = "temperatureCompensation1";
            this.temperatureCompensation1.Size = new System.Drawing.Size(1080, 458);
            this.temperatureCompensation1.TabIndex = 0;
            this.temperatureCompensation1.Load += new System.EventHandler(this.temperatureCompensation1_Load);
            // 
            // flowLayoutPanel_Comports
            // 
            this.flowLayoutPanel_Comports.AutoScroll = true;
            this.flowLayoutPanel_Comports.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel_Comports.Location = new System.Drawing.Point(3, 68);
            this.flowLayoutPanel_Comports.Name = "flowLayoutPanel_Comports";
            this.flowLayoutPanel_Comports.Size = new System.Drawing.Size(244, 204);
            this.flowLayoutPanel_Comports.TabIndex = 2;
            // 
            // label_SerialPort
            // 
            this.label_SerialPort.AutoSize = true;
            this.label_SerialPort.Font = new System.Drawing.Font("PMingLiU", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_SerialPort.Location = new System.Drawing.Point(1, 29);
            this.label_SerialPort.Name = "label_SerialPort";
            this.label_SerialPort.Size = new System.Drawing.Size(105, 21);
            this.label_SerialPort.TabIndex = 3;
            this.label_SerialPort.Text = "Serial Port";
            // 
            // button_RefreshPorts
            // 
            this.button_RefreshPorts.Location = new System.Drawing.Point(127, 23);
            this.button_RefreshPorts.Name = "button_RefreshPorts";
            this.button_RefreshPorts.Size = new System.Drawing.Size(84, 35);
            this.button_RefreshPorts.TabIndex = 4;
            this.button_RefreshPorts.Text = "Refresh";
            this.button_RefreshPorts.UseVisualStyleBackColor = true;
            this.button_RefreshPorts.Click += new System.EventHandler(this.button_RefreshPorts_Click);
            // 
            // dataGridView_Pulse
            // 
            this.dataGridView_Pulse.AllowUserToAddRows = false;
            this.dataGridView_Pulse.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            this.dataGridView_Pulse.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_Pulse.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Pulse.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView_Pulse.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView_Pulse.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView_Pulse.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_Pulse.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_Pulse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Pulse.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Item,
            this.Pulse,
            this.Column_Time});
            this.dataGridView_Pulse.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView_Pulse.GridColor = System.Drawing.Color.White;
            this.dataGridView_Pulse.Location = new System.Drawing.Point(3, 290);
            this.dataGridView_Pulse.Name = "dataGridView_Pulse";
            this.dataGridView_Pulse.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView_Pulse.RowHeadersVisible = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            this.dataGridView_Pulse.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView_Pulse.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView_Pulse.RowTemplate.Height = 24;
            this.dataGridView_Pulse.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_Pulse.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView_Pulse.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView_Pulse.Size = new System.Drawing.Size(244, 92);
            this.dataGridView_Pulse.TabIndex = 5;
            this.dataGridView_Pulse.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Pulse_CellValueChanged);
            this.dataGridView_Pulse.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView_Pulse_CurrentCellDirtyStateChanged);
            // 
            // Item
            // 
            this.Item.HeaderText = "PWM Pulse";
            this.Item.Name = "Item";
            this.Item.ReadOnly = true;
            // 
            // Pulse
            // 
            this.Pulse.FillWeight = 81.21828F;
            this.Pulse.HeaderText = "enable";
            this.Pulse.MinimumWidth = 80;
            this.Pulse.Name = "Pulse";
            // 
            // Column_Time
            // 
            this.Column_Time.FillWeight = 118.7817F;
            this.Column_Time.HeaderText = "Time (ms)";
            this.Column_Time.Name = "Column_Time";
            // 
            // numericUpDown_IC_count
            // 
            this.numericUpDown_IC_count.Location = new System.Drawing.Point(127, 415);
            this.numericUpDown_IC_count.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown_IC_count.Name = "numericUpDown_IC_count";
            this.numericUpDown_IC_count.Size = new System.Drawing.Size(120, 22);
            this.numericUpDown_IC_count.TabIndex = 6;
            this.numericUpDown_IC_count.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_IC_count.Visible = false;
            this.numericUpDown_IC_count.ValueChanged += new System.EventHandler(this.numericUpDown_IC_count_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 417);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "IC count";
            this.label1.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.檔案ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1348, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 檔案ToolStripMenuItem
            // 
            this.檔案ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.匯入檔案ToolStripMenuItem,
            this.FileExportToolStripMenuItem});
            this.檔案ToolStripMenuItem.Name = "檔案ToolStripMenuItem";
            this.檔案ToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.檔案ToolStripMenuItem.Text = "File";
            // 
            // 匯入檔案ToolStripMenuItem
            // 
            this.匯入檔案ToolStripMenuItem.Name = "匯入檔案ToolStripMenuItem";
            this.匯入檔案ToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.匯入檔案ToolStripMenuItem.Text = "ImportFile";
            this.匯入檔案ToolStripMenuItem.Click += new System.EventHandler(this.FileImportToolStripMenuItem_Click);
            // 
            // FileExportToolStripMenuItem
            // 
            this.FileExportToolStripMenuItem.Name = "FileExportToolStripMenuItem";
            this.FileExportToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.FileExportToolStripMenuItem.Text = "ExportFile";
            this.FileExportToolStripMenuItem.Click += new System.EventHandler(this.FileExportToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1348, 522);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown_IC_count);
            this.Controls.Add(this.dataGridView_Pulse);
            this.Controls.Add(this.button_RefreshPorts);
            this.Controls.Add(this.label_SerialPort);
            this.Controls.Add(this.flowLayoutPanel_Comports);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "RT7216Q GUI";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Pulse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_IC_count)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Classes.RT7216Q_Control rT7216Q_Control1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Classes.RT7216N_BD_Control rT7216N_BD_Control1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_Comports;
        private System.Windows.Forms.Label label_SerialPort;
        private System.Windows.Forms.Button button_RefreshPorts;
        private Classes.GUI gui1;
        private System.Windows.Forms.TabPage tabPage4;
        private Classes.TemperatureCompensation temperatureCompensation1;
        private System.Windows.Forms.DataGridView dataGridView_Pulse;
        private System.Windows.Forms.NumericUpDown numericUpDown_IC_count;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Pulse;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Time;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 檔案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 匯入檔案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileExportToolStripMenuItem;


    }
}

