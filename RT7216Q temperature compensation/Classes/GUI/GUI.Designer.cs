namespace RT7216Q_temperature_compensation.Classes
{
    partial class GUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            this.comboBox_IC_Type = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView_PWM = new System.Windows.Forms.DataGridView();
            this.Column_OUT = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column_PWM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Duty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_TC_Duty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_Main = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel_Detect = new System.Windows.Forms.TableLayoutPanel();
            this.button_LSD = new System.Windows.Forms.Button();
            this.button_LOD = new System.Windows.Forms.Button();
            this.button_Temperature = new System.Windows.Forms.Button();
            this.button_Temperature_ave = new System.Windows.Forms.Button();
            this.dataGridView_response = new System.Windows.Forms.DataGridView();
            this.Column_Response_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Response_Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Response_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel_PWM = new System.Windows.Forms.TableLayoutPanel();
            this.button_PWM = new System.Windows.Forms.Button();
            this.dataGridView_Pulse = new System.Windows.Forms.DataGridView();
            this.Pulse = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column_Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage_Primary = new System.Windows.Forms.TabPage();
            this.tabPage_OTP = new System.Windows.Forms.TabPage();
            this.tabPage_TC = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl_TC = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView_PrimaryRegister = new System.Windows.Forms.DataGridView();
            this.Column_PrimaryRegister_Variable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_PrimaryRegister_Value = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.graph1 = new RT7216Q_temperature_compensation.Classes.Graph();
            this.tabPage_MISC = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_PWM)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage_Main.SuspendLayout();
            this.tableLayoutPanel_Detect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_response)).BeginInit();
            this.tableLayoutPanel_PWM.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Pulse)).BeginInit();
            this.tabPage_Primary.SuspendLayout();
            this.tabPage_TC.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl_TC.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_PrimaryRegister)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox_IC_Type
            // 
            this.comboBox_IC_Type.FormattingEnabled = true;
            this.comboBox_IC_Type.Items.AddRange(new object[] {
            "RT7216N_AA",
            "RT7216N_BD",
            "RT7216Q"});
            this.comboBox_IC_Type.Location = new System.Drawing.Point(26, 19);
            this.comboBox_IC_Type.Name = "comboBox_IC_Type";
            this.comboBox_IC_Type.Size = new System.Drawing.Size(105, 20);
            this.comboBox_IC_Type.TabIndex = 0;
            this.comboBox_IC_Type.SelectedIndexChanged += new System.EventHandler(this.comboBox_IC_Type_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "IC";
            // 
            // dataGridView_PWM
            // 
            this.dataGridView_PWM.AllowUserToAddRows = false;
            this.dataGridView_PWM.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            this.dataGridView_PWM.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_PWM.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_PWM.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView_PWM.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView_PWM.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView_PWM.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_PWM.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_PWM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_PWM.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_OUT,
            this.Column_PWM,
            this.Column_Duty,
            this.Column_TC_Duty});
            this.tableLayoutPanel_PWM.SetColumnSpan(this.dataGridView_PWM, 2);
            this.dataGridView_PWM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_PWM.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGridView_PWM.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridView_PWM.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_PWM.Name = "dataGridView_PWM";
            this.dataGridView_PWM.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView_PWM.RowHeadersVisible = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            this.dataGridView_PWM.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridView_PWM.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView_PWM.RowTemplate.Height = 24;
            this.dataGridView_PWM.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_PWM.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView_PWM.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView_PWM.Size = new System.Drawing.Size(277, 116);
            this.dataGridView_PWM.TabIndex = 2;
            this.dataGridView_PWM.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_PWM_CellContentClick);
            this.dataGridView_PWM.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_PWM_CellEnter);
            this.dataGridView_PWM.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_PWM_CellValueChanged);
            // 
            // Column_OUT
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            this.Column_OUT.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column_OUT.FillWeight = 50F;
            this.Column_OUT.HeaderText = "OUT";
            this.Column_OUT.MinimumWidth = 40;
            this.Column_OUT.Name = "Column_OUT";
            // 
            // Column_PWM
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.Column_PWM.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column_PWM.FillWeight = 70F;
            this.Column_PWM.HeaderText = "PWM (Decimal)";
            this.Column_PWM.MinimumWidth = 50;
            this.Column_PWM.Name = "Column_PWM";
            this.Column_PWM.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column_PWM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_Duty
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            this.Column_Duty.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column_Duty.FillWeight = 60F;
            this.Column_Duty.HeaderText = "Duty (%)";
            this.Column_Duty.MinimumWidth = 50;
            this.Column_Duty.Name = "Column_Duty";
            this.Column_Duty.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column_Duty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_TC_Duty
            // 
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            this.Column_TC_Duty.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column_TC_Duty.HeaderText = "Temperature Compensated Duty (%)";
            this.Column_TC_Duty.MinimumWidth = 85;
            this.Column_TC_Duty.Name = "Column_TC_Duty";
            this.Column_TC_Duty.ReadOnly = true;
            this.Column_TC_Duty.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column_TC_Duty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_Main);
            this.tabControl1.Controls.Add(this.tabPage_Primary);
            this.tabControl1.Controls.Add(this.tabPage_MISC);
            this.tabControl1.Controls.Add(this.tabPage_OTP);
            this.tabControl1.Controls.Add(this.tabPage_TC);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(143, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(994, 470);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage_Main
            // 
            this.tabPage_Main.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_Main.Controls.Add(this.tableLayoutPanel_Detect);
            this.tabPage_Main.Controls.Add(this.dataGridView_response);
            this.tabPage_Main.Controls.Add(this.tableLayoutPanel_PWM);
            this.tabPage_Main.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Main.Name = "tabPage_Main";
            this.tabPage_Main.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Main.Size = new System.Drawing.Size(986, 444);
            this.tabPage_Main.TabIndex = 0;
            this.tabPage_Main.Text = "Main Page";
            // 
            // tableLayoutPanel_Detect
            // 
            this.tableLayoutPanel_Detect.ColumnCount = 4;
            this.tableLayoutPanel_Detect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel_Detect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel_Detect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel_Detect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel_Detect.Controls.Add(this.button_LSD, 0, 0);
            this.tableLayoutPanel_Detect.Controls.Add(this.button_LOD, 1, 0);
            this.tableLayoutPanel_Detect.Controls.Add(this.button_Temperature, 2, 0);
            this.tableLayoutPanel_Detect.Controls.Add(this.button_Temperature_ave, 3, 0);
            this.tableLayoutPanel_Detect.Location = new System.Drawing.Point(350, 132);
            this.tableLayoutPanel_Detect.Name = "tableLayoutPanel_Detect";
            this.tableLayoutPanel_Detect.RowCount = 1;
            this.tableLayoutPanel_Detect.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Detect.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tableLayoutPanel_Detect.Size = new System.Drawing.Size(325, 58);
            this.tableLayoutPanel_Detect.TabIndex = 12;
            // 
            // button_LSD
            // 
            this.button_LSD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_LSD.Location = new System.Drawing.Point(3, 3);
            this.button_LSD.Name = "button_LSD";
            this.button_LSD.Size = new System.Drawing.Size(75, 52);
            this.button_LSD.TabIndex = 13;
            this.button_LSD.Text = "LSD";
            this.button_LSD.UseVisualStyleBackColor = true;
            this.button_LSD.Click += new System.EventHandler(this.button_Detect_Click);
            // 
            // button_LOD
            // 
            this.button_LOD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_LOD.Location = new System.Drawing.Point(84, 3);
            this.button_LOD.Name = "button_LOD";
            this.button_LOD.Size = new System.Drawing.Size(75, 52);
            this.button_LOD.TabIndex = 13;
            this.button_LOD.Text = "LOD";
            this.button_LOD.UseVisualStyleBackColor = true;
            this.button_LOD.Click += new System.EventHandler(this.button_Detect_Click);
            // 
            // button_Temperature
            // 
            this.button_Temperature.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_Temperature.Location = new System.Drawing.Point(165, 3);
            this.button_Temperature.Name = "button_Temperature";
            this.button_Temperature.Size = new System.Drawing.Size(75, 52);
            this.button_Temperature.TabIndex = 13;
            this.button_Temperature.Text = "Temperature";
            this.button_Temperature.UseVisualStyleBackColor = true;
            this.button_Temperature.Click += new System.EventHandler(this.button_Detect_Click);
            // 
            // button_Temperature_ave
            // 
            this.button_Temperature_ave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_Temperature_ave.Location = new System.Drawing.Point(246, 3);
            this.button_Temperature_ave.Name = "button_Temperature_ave";
            this.button_Temperature_ave.Size = new System.Drawing.Size(76, 52);
            this.button_Temperature_ave.TabIndex = 13;
            this.button_Temperature_ave.Text = "Average Temperature";
            this.button_Temperature_ave.UseVisualStyleBackColor = true;
            this.button_Temperature_ave.Click += new System.EventHandler(this.button_Detect_Click);
            // 
            // dataGridView_response
            // 
            this.dataGridView_response.AllowUserToAddRows = false;
            this.dataGridView_response.AllowUserToDeleteRows = false;
            this.dataGridView_response.AllowUserToResizeRows = false;
            this.dataGridView_response.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_response.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridView_response.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_response.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_Response_Type,
            this.Column_Response_Value,
            this.Column_Response_Description});
            this.dataGridView_response.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView_response.Location = new System.Drawing.Point(350, 71);
            this.dataGridView_response.Name = "dataGridView_response";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("PMingLiU", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_response.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridView_response.RowHeadersVisible = false;
            this.dataGridView_response.RowTemplate.Height = 24;
            this.dataGridView_response.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView_response.Size = new System.Drawing.Size(325, 54);
            this.dataGridView_response.TabIndex = 11;
            // 
            // Column_Response_Type
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column_Response_Type.DefaultCellStyle = dataGridViewCellStyle12;
            this.Column_Response_Type.HeaderText = "Response Type";
            this.Column_Response_Type.MinimumWidth = 120;
            this.Column_Response_Type.Name = "Column_Response_Type";
            this.Column_Response_Type.ReadOnly = true;
            this.Column_Response_Type.Width = 120;
            // 
            // Column_Response_Value
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column_Response_Value.DefaultCellStyle = dataGridViewCellStyle13;
            this.Column_Response_Value.HeaderText = "Value";
            this.Column_Response_Value.MinimumWidth = 50;
            this.Column_Response_Value.Name = "Column_Response_Value";
            this.Column_Response_Value.ReadOnly = true;
            this.Column_Response_Value.Width = 50;
            // 
            // Column_Response_Description
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Column_Response_Description.DefaultCellStyle = dataGridViewCellStyle14;
            this.Column_Response_Description.HeaderText = "Description";
            this.Column_Response_Description.MinimumWidth = 150;
            this.Column_Response_Description.Name = "Column_Response_Description";
            this.Column_Response_Description.ReadOnly = true;
            this.Column_Response_Description.Width = 150;
            // 
            // tableLayoutPanel_PWM
            // 
            this.tableLayoutPanel_PWM.ColumnCount = 2;
            this.tableLayoutPanel_PWM.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel_PWM.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel_PWM.Controls.Add(this.dataGridView_PWM, 0, 0);
            this.tableLayoutPanel_PWM.Controls.Add(this.button_PWM, 1, 1);
            this.tableLayoutPanel_PWM.Controls.Add(this.dataGridView_Pulse, 0, 1);
            this.tableLayoutPanel_PWM.Location = new System.Drawing.Point(38, 71);
            this.tableLayoutPanel_PWM.Name = "tableLayoutPanel_PWM";
            this.tableLayoutPanel_PWM.RowCount = 2;
            this.tableLayoutPanel_PWM.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_PWM.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel_PWM.Size = new System.Drawing.Size(283, 177);
            this.tableLayoutPanel_PWM.TabIndex = 5;
            // 
            // button_PWM
            // 
            this.button_PWM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_PWM.Location = new System.Drawing.Point(186, 125);
            this.button_PWM.Name = "button_PWM";
            this.button_PWM.Size = new System.Drawing.Size(94, 49);
            this.button_PWM.TabIndex = 4;
            this.button_PWM.Text = "Send";
            this.button_PWM.UseVisualStyleBackColor = true;
            this.button_PWM.Click += new System.EventHandler(this.button_PWM_Click);
            // 
            // dataGridView_Pulse
            // 
            this.dataGridView_Pulse.AllowUserToAddRows = false;
            this.dataGridView_Pulse.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            this.dataGridView_Pulse.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView_Pulse.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Pulse.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView_Pulse.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView_Pulse.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView_Pulse.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_Pulse.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView_Pulse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Pulse.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Pulse,
            this.Column_Time});
            this.dataGridView_Pulse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Pulse.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGridView_Pulse.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridView_Pulse.Location = new System.Drawing.Point(3, 125);
            this.dataGridView_Pulse.Name = "dataGridView_Pulse";
            this.dataGridView_Pulse.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView_Pulse.RowHeadersVisible = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            this.dataGridView_Pulse.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView_Pulse.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView_Pulse.RowTemplate.Height = 24;
            this.dataGridView_Pulse.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_Pulse.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView_Pulse.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView_Pulse.Size = new System.Drawing.Size(177, 49);
            this.dataGridView_Pulse.TabIndex = 3;
            // 
            // Pulse
            // 
            this.Pulse.FillWeight = 50.76142F;
            this.Pulse.HeaderText = "Pulse";
            this.Pulse.MinimumWidth = 50;
            this.Pulse.Name = "Pulse";
            // 
            // Column_Time
            // 
            this.Column_Time.FillWeight = 149.2386F;
            this.Column_Time.HeaderText = "Time (ms)";
            this.Column_Time.Name = "Column_Time";
            // 
            // tabPage_Primary
            // 
            this.tabPage_Primary.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_Primary.Controls.Add(this.dataGridView_PrimaryRegister);
            this.tabPage_Primary.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Primary.Name = "tabPage_Primary";
            this.tabPage_Primary.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Primary.Size = new System.Drawing.Size(986, 444);
            this.tabPage_Primary.TabIndex = 1;
            this.tabPage_Primary.Text = "Primary Settings";
            // 
            // tabPage_OTP
            // 
            this.tabPage_OTP.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_OTP.Location = new System.Drawing.Point(4, 22);
            this.tabPage_OTP.Name = "tabPage_OTP";
            this.tabPage_OTP.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_OTP.Size = new System.Drawing.Size(986, 444);
            this.tabPage_OTP.TabIndex = 2;
            this.tabPage_OTP.Text = "OTP ";
            // 
            // tabPage_TC
            // 
            this.tabPage_TC.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_TC.Controls.Add(this.tableLayoutPanel1);
            this.tabPage_TC.Location = new System.Drawing.Point(4, 22);
            this.tabPage_TC.Name = "tabPage_TC";
            this.tabPage_TC.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_TC.Size = new System.Drawing.Size(986, 444);
            this.tabPage_TC.TabIndex = 3;
            this.tabPage_TC.Text = "Temperature Compensation";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.61816F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.38184F));
            this.tableLayoutPanel1.Controls.Add(this.tabControl_TC, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.graph1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(980, 438);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tabControl_TC
            // 
            this.tabControl_TC.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl_TC.Controls.Add(this.tabPage5);
            this.tabControl_TC.Controls.Add(this.tabPage6);
            this.tabControl_TC.Controls.Add(this.tabPage7);
            this.tabControl_TC.Controls.Add(this.tabPage8);
            this.tabControl_TC.Controls.Add(this.tabPage9);
            this.tabControl_TC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_TC.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl_TC.ItemSize = new System.Drawing.Size(30, 100);
            this.tabControl_TC.Location = new System.Drawing.Point(3, 3);
            this.tabControl_TC.Multiline = true;
            this.tabControl_TC.Name = "tabControl_TC";
            this.tabControl_TC.SelectedIndex = 0;
            this.tabControl_TC.Size = new System.Drawing.Size(284, 432);
            this.tabControl_TC.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl_TC.TabIndex = 0;
            this.tabControl_TC.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl2_DrawItem);
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage5.Location = new System.Drawing.Point(104, 4);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(176, 424);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "Summary";
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage6.Location = new System.Drawing.Point(104, 4);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(176, 424);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "CR";
            // 
            // tabPage7
            // 
            this.tabPage7.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage7.Location = new System.Drawing.Point(104, 4);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(176, 424);
            this.tabPage7.TabIndex = 2;
            this.tabPage7.Text = "Scalar";
            // 
            // tabPage8
            // 
            this.tabPage8.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage8.Location = new System.Drawing.Point(104, 4);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(176, 424);
            this.tabPage8.TabIndex = 3;
            this.tabPage8.Text = "TC linear";
            // 
            // tabPage9
            // 
            this.tabPage9.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage9.Location = new System.Drawing.Point(104, 4);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage9.Size = new System.Drawing.Size(176, 424);
            this.tabPage9.TabIndex = 4;
            this.tabPage9.Text = "TC non linear";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tabControl1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 476F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1140, 476);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox_IC_Type);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(134, 470);
            this.panel1.TabIndex = 4;
            // 
            // dataGridView_PrimaryRegister
            // 
            this.dataGridView_PrimaryRegister.AllowUserToAddRows = false;
            this.dataGridView_PrimaryRegister.AllowUserToDeleteRows = false;
            this.dataGridView_PrimaryRegister.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_PrimaryRegister.ColumnHeadersVisible = false;
            this.dataGridView_PrimaryRegister.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_PrimaryRegister_Variable,
            this.Column_PrimaryRegister_Value});
            this.dataGridView_PrimaryRegister.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView_PrimaryRegister.Location = new System.Drawing.Point(16, 6);
            this.dataGridView_PrimaryRegister.Name = "dataGridView_PrimaryRegister";
            this.dataGridView_PrimaryRegister.RowHeadersVisible = false;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView_PrimaryRegister.RowsDefaultCellStyle = dataGridViewCellStyle18;
            this.dataGridView_PrimaryRegister.RowTemplate.Height = 24;
            this.dataGridView_PrimaryRegister.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_PrimaryRegister.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_PrimaryRegister.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView_PrimaryRegister.Size = new System.Drawing.Size(307, 425);
            this.dataGridView_PrimaryRegister.TabIndex = 4;
            // 
            // Column_PrimaryRegister_Variable
            // 
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            this.Column_PrimaryRegister_Variable.DefaultCellStyle = dataGridViewCellStyle16;
            this.Column_PrimaryRegister_Variable.FillWeight = 130F;
            this.Column_PrimaryRegister_Variable.HeaderText = "";
            this.Column_PrimaryRegister_Variable.MinimumWidth = 100;
            this.Column_PrimaryRegister_Variable.Name = "Column_PrimaryRegister_Variable";
            this.Column_PrimaryRegister_Variable.ReadOnly = true;
            this.Column_PrimaryRegister_Variable.Width = 123;
            // 
            // Column_PrimaryRegister_Value
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Column_PrimaryRegister_Value.DefaultCellStyle = dataGridViewCellStyle17;
            this.Column_PrimaryRegister_Value.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.Column_PrimaryRegister_Value.FillWeight = 165F;
            this.Column_PrimaryRegister_Value.HeaderText = "Value";
            this.Column_PrimaryRegister_Value.MaxDropDownItems = 64;
            this.Column_PrimaryRegister_Value.MinimumWidth = 100;
            this.Column_PrimaryRegister_Value.Name = "Column_PrimaryRegister_Value";
            this.Column_PrimaryRegister_Value.Width = 165;
            // 
            // graph1
            // 
            this.graph1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graph1.Location = new System.Drawing.Point(293, 3);
            this.graph1.Name = "graph1";
            this.graph1.Size = new System.Drawing.Size(684, 432);
            this.graph1.TabIndex = 1;
            // 
            // tabPage_MISC
            // 
            this.tabPage_MISC.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_MISC.Location = new System.Drawing.Point(4, 22);
            this.tabPage_MISC.Name = "tabPage_MISC";
            this.tabPage_MISC.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_MISC.Size = new System.Drawing.Size(986, 444);
            this.tabPage_MISC.TabIndex = 4;
            this.tabPage_MISC.Text = "MISC";
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "GUI";
            this.Size = new System.Drawing.Size(1140, 476);
            this.Load += new System.EventHandler(this.GUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_PWM)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage_Main.ResumeLayout(false);
            this.tableLayoutPanel_Detect.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_response)).EndInit();
            this.tableLayoutPanel_PWM.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Pulse)).EndInit();
            this.tabPage_Primary.ResumeLayout(false);
            this.tabPage_TC.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl_TC.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_PrimaryRegister)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_IC_Type;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView_PWM;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_Main;
        private System.Windows.Forms.TabPage tabPage_Primary;
        private System.Windows.Forms.TabPage tabPage_OTP;
        private System.Windows.Forms.TabPage tabPage_TC;
        private System.Windows.Forms.DataGridView dataGridView_Pulse;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_PWM;
        private System.Windows.Forms.Button button_PWM;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Pulse;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Time;
        private System.Windows.Forms.DataGridViewButtonColumn Column_OUT;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_PWM;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Duty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_TC_Duty;
        private System.Windows.Forms.DataGridView dataGridView_response;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Response_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Response_Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Response_Description;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Detect;
        private System.Windows.Forms.Button button_LSD;
        private System.Windows.Forms.Button button_LOD;
        private System.Windows.Forms.Button button_Temperature;
        private System.Windows.Forms.Button button_Temperature_ave;
        private System.Windows.Forms.TabControl tabControl_TC;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.TabPage tabPage9;
        private Graph graph1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView_PrimaryRegister;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_PrimaryRegister_Variable;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column_PrimaryRegister_Value;
        private System.Windows.Forms.TabPage tabPage_MISC;
    }
}
