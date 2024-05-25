namespace GDAPPWIN
{
    partial class MainForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            panel_slide = new Panel();
            panel_submenuAddUser = new Panel();
            Btn_UpdateGDAPP = new Button();
            Btn_subAddUser = new Button();
            panel_submenuPayBack = new Panel();
            BtnSub_Expeness = new Button();
            btnSubApprove = new Button();
            btn_DpReport = new Button();
            panel_submenuCreditOut = new Panel();
            Btn_NotPaid = new Button();
            button_subReport = new Button();
            btn_subOrvcAdd2 = new Button();
            button_subAddRealBill = new Button();
            button_subAddBillCredit = new Button();
            button_subAddCustomer = new Button();
            panel_submenuCreditIn = new Panel();
            BtnSub_EditOrcc = new Button();
            BtnSub_AddOrcc = new Button();
            btnsubCashIN = new Button();
            Btn_AddUser = new Button();
            buttonPayback = new Button();
            buttonCreditOutMain = new Button();
            button_CreditInMain = new Button();
            panel_logo = new Panel();
            label1_LOGO = new Label();
            label_welcome = new Label();
            label_TotalDeposit = new Label();
            panel_main = new Panel();
            panel2 = new Panel();
            lbl_MainTotalOrnc = new Label();
            panel_cover = new Panel();
            panel1 = new Panel();
            panel4 = new Panel();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panel3 = new Panel();
            lblUserRole = new Label();
            lblUserInfo = new Label();
            panel_slide.SuspendLayout();
            panel_submenuAddUser.SuspendLayout();
            panel_submenuPayBack.SuspendLayout();
            panel_submenuCreditOut.SuspendLayout();
            panel_submenuCreditIn.SuspendLayout();
            panel_logo.SuspendLayout();
            panel_main.SuspendLayout();
            panel2.SuspendLayout();
            panel_cover.SuspendLayout();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel_slide
            // 
            panel_slide.AutoScroll = true;
            panel_slide.BackColor = Color.RoyalBlue;
            panel_slide.Controls.Add(panel_submenuAddUser);
            panel_slide.Controls.Add(panel_submenuPayBack);
            panel_slide.Controls.Add(panel_submenuCreditOut);
            panel_slide.Controls.Add(panel_submenuCreditIn);
            panel_slide.Controls.Add(Btn_AddUser);
            panel_slide.Controls.Add(buttonPayback);
            panel_slide.Controls.Add(buttonCreditOutMain);
            panel_slide.Controls.Add(button_CreditInMain);
            panel_slide.Controls.Add(panel_logo);
            panel_slide.Dock = DockStyle.Left;
            panel_slide.Location = new Point(0, 0);
            panel_slide.Name = "panel_slide";
            panel_slide.Size = new Size(210, 861);
            panel_slide.TabIndex = 0;
            // 
            // panel_submenuAddUser
            // 
            panel_submenuAddUser.Controls.Add(Btn_UpdateGDAPP);
            panel_submenuAddUser.Controls.Add(Btn_subAddUser);
            panel_submenuAddUser.Location = new Point(3, 757);
            panel_submenuAddUser.Name = "panel_submenuAddUser";
            panel_submenuAddUser.Size = new Size(185, 76);
            panel_submenuAddUser.TabIndex = 4;
            // 
            // Btn_UpdateGDAPP
            // 
            Btn_UpdateGDAPP.BackColor = Color.DodgerBlue;
            Btn_UpdateGDAPP.FlatAppearance.BorderSize = 0;
            Btn_UpdateGDAPP.FlatStyle = FlatStyle.Flat;
            Btn_UpdateGDAPP.Font = new Font("Segoe UI", 10F);
            Btn_UpdateGDAPP.ForeColor = Color.White;
            Btn_UpdateGDAPP.Location = new Point(0, 34);
            Btn_UpdateGDAPP.Name = "Btn_UpdateGDAPP";
            Btn_UpdateGDAPP.Size = new Size(185, 30);
            Btn_UpdateGDAPP.TabIndex = 3;
            Btn_UpdateGDAPP.Text = "UPDATE";
            Btn_UpdateGDAPP.UseVisualStyleBackColor = false;
            Btn_UpdateGDAPP.Click += Btn_UpdateGDAPP_Click;
            // 
            // Btn_subAddUser
            // 
            Btn_subAddUser.BackColor = Color.DodgerBlue;
            Btn_subAddUser.FlatAppearance.BorderSize = 0;
            Btn_subAddUser.FlatStyle = FlatStyle.Flat;
            Btn_subAddUser.Font = new Font("Segoe UI", 10F);
            Btn_subAddUser.ForeColor = Color.White;
            Btn_subAddUser.Location = new Point(0, 3);
            Btn_subAddUser.Name = "Btn_subAddUser";
            Btn_subAddUser.Size = new Size(185, 30);
            Btn_subAddUser.TabIndex = 3;
            Btn_subAddUser.Text = "เพิ่มผุ้ใช้งาน";
            Btn_subAddUser.UseVisualStyleBackColor = false;
            Btn_subAddUser.Click += Btn_subAddUser_Click;
            // 
            // panel_submenuPayBack
            // 
            panel_submenuPayBack.Controls.Add(BtnSub_Expeness);
            panel_submenuPayBack.Controls.Add(btnSubApprove);
            panel_submenuPayBack.Controls.Add(btn_DpReport);
            panel_submenuPayBack.Location = new Point(3, 580);
            panel_submenuPayBack.Name = "panel_submenuPayBack";
            panel_submenuPayBack.Size = new Size(185, 114);
            panel_submenuPayBack.TabIndex = 3;
            // 
            // BtnSub_Expeness
            // 
            BtnSub_Expeness.BackColor = Color.DodgerBlue;
            BtnSub_Expeness.FlatAppearance.BorderSize = 0;
            BtnSub_Expeness.FlatStyle = FlatStyle.Flat;
            BtnSub_Expeness.Font = new Font("Segoe UI", 10F);
            BtnSub_Expeness.ForeColor = Color.White;
            BtnSub_Expeness.Location = new Point(0, 34);
            BtnSub_Expeness.Name = "BtnSub_Expeness";
            BtnSub_Expeness.Size = new Size(185, 30);
            BtnSub_Expeness.TabIndex = 4;
            BtnSub_Expeness.Text = "เบิกส่วนลด/เงินฝาก/(ORCC)";
            BtnSub_Expeness.UseVisualStyleBackColor = false;
            BtnSub_Expeness.Click += BtnSub_Expense_Click;
            // 
            // btnSubApprove
            // 
            btnSubApprove.BackColor = Color.DodgerBlue;
            btnSubApprove.FlatAppearance.BorderSize = 0;
            btnSubApprove.FlatStyle = FlatStyle.Flat;
            btnSubApprove.Font = new Font("Segoe UI", 10F);
            btnSubApprove.ForeColor = Color.White;
            btnSubApprove.Location = new Point(0, 3);
            btnSubApprove.Name = "btnSubApprove";
            btnSubApprove.Size = new Size(185, 30);
            btnSubApprove.TabIndex = 1;
            btnSubApprove.Text = "บิลรออนุมัติ";
            btnSubApprove.UseVisualStyleBackColor = false;
            btnSubApprove.Click += btn_SubApprove_Click;
            // 
            // btn_DpReport
            // 
            btn_DpReport.BackColor = Color.DodgerBlue;
            btn_DpReport.FlatAppearance.BorderSize = 0;
            btn_DpReport.FlatStyle = FlatStyle.Flat;
            btn_DpReport.Font = new Font("Segoe UI", 10F);
            btn_DpReport.ForeColor = Color.White;
            btn_DpReport.Location = new Point(0, 65);
            btn_DpReport.Name = "btn_DpReport";
            btn_DpReport.Size = new Size(185, 30);
            btn_DpReport.TabIndex = 3;
            btn_DpReport.Text = "รายงาน";
            btn_DpReport.UseVisualStyleBackColor = false;
            btn_DpReport.Click += btn_DpReport_Click;
            // 
            // panel_submenuCreditOut
            // 
            panel_submenuCreditOut.BackColor = Color.RoyalBlue;
            panel_submenuCreditOut.Controls.Add(Btn_NotPaid);
            panel_submenuCreditOut.Controls.Add(button_subReport);
            panel_submenuCreditOut.Controls.Add(btn_subOrvcAdd2);
            panel_submenuCreditOut.Controls.Add(button_subAddRealBill);
            panel_submenuCreditOut.Controls.Add(button_subAddBillCredit);
            panel_submenuCreditOut.Controls.Add(button_subAddCustomer);
            panel_submenuCreditOut.Location = new Point(3, 277);
            panel_submenuCreditOut.Name = "panel_submenuCreditOut";
            panel_submenuCreditOut.Padding = new Padding(35, 0, 0, 0);
            panel_submenuCreditOut.Size = new Size(185, 195);
            panel_submenuCreditOut.TabIndex = 3;
            // 
            // Btn_NotPaid
            // 
            Btn_NotPaid.BackColor = Color.DodgerBlue;
            Btn_NotPaid.FlatAppearance.BorderSize = 0;
            Btn_NotPaid.FlatStyle = FlatStyle.Flat;
            Btn_NotPaid.Font = new Font("Segoe UI", 10F);
            Btn_NotPaid.ForeColor = Color.White;
            Btn_NotPaid.Location = new Point(0, 155);
            Btn_NotPaid.Name = "Btn_NotPaid";
            Btn_NotPaid.Size = new Size(185, 30);
            Btn_NotPaid.TabIndex = 4;
            Btn_NotPaid.Text = "บิลค้างจ่าย";
            Btn_NotPaid.UseVisualStyleBackColor = false;
            Btn_NotPaid.Click += Btn_NotPaid_Click;
            // 
            // button_subReport
            // 
            button_subReport.BackColor = Color.DodgerBlue;
            button_subReport.FlatAppearance.BorderSize = 0;
            button_subReport.FlatStyle = FlatStyle.Flat;
            button_subReport.Font = new Font("Segoe UI", 10F);
            button_subReport.ForeColor = Color.White;
            button_subReport.Location = new Point(0, 124);
            button_subReport.Name = "button_subReport";
            button_subReport.Size = new Size(185, 30);
            button_subReport.TabIndex = 4;
            button_subReport.Text = "รายงาน";
            button_subReport.UseVisualStyleBackColor = false;
            button_subReport.Click += button_subReport_Click;
            // 
            // btn_subOrvcAdd2
            // 
            btn_subOrvcAdd2.BackColor = Color.DodgerBlue;
            btn_subOrvcAdd2.FlatAppearance.BorderSize = 0;
            btn_subOrvcAdd2.FlatStyle = FlatStyle.Flat;
            btn_subOrvcAdd2.Font = new Font("Segoe UI", 10F);
            btn_subOrvcAdd2.ForeColor = Color.White;
            btn_subOrvcAdd2.Location = new Point(0, 93);
            btn_subOrvcAdd2.Name = "btn_subOrvcAdd2";
            btn_subOrvcAdd2.Size = new Size(185, 30);
            btn_subOrvcAdd2.TabIndex = 2;
            btn_subOrvcAdd2.Text = "เพิ่ม/แก้ไข/ยกเลิก (ORVC)";
            btn_subOrvcAdd2.UseVisualStyleBackColor = false;
            btn_subOrvcAdd2.Click += button_subOrvcAdd2_Click;
            // 
            // button_subAddRealBill
            // 
            button_subAddRealBill.BackColor = Color.DodgerBlue;
            button_subAddRealBill.FlatAppearance.BorderSize = 0;
            button_subAddRealBill.FlatStyle = FlatStyle.Flat;
            button_subAddRealBill.Font = new Font("Segoe UI", 10F);
            button_subAddRealBill.ForeColor = Color.White;
            button_subAddRealBill.Location = new Point(0, 62);
            button_subAddRealBill.Name = "button_subAddRealBill";
            button_subAddRealBill.Size = new Size(185, 30);
            button_subAddRealBill.TabIndex = 2;
            button_subAddRealBill.Text = "เปลี่ยนเป็น(ORVC)";
            button_subAddRealBill.UseVisualStyleBackColor = false;
            button_subAddRealBill.Click += button_subAddRealBill_Click;
            // 
            // button_subAddBillCredit
            // 
            button_subAddBillCredit.BackColor = Color.DodgerBlue;
            button_subAddBillCredit.FlatAppearance.BorderSize = 0;
            button_subAddBillCredit.FlatStyle = FlatStyle.Flat;
            button_subAddBillCredit.Font = new Font("Segoe UI", 10F);
            button_subAddBillCredit.ForeColor = Color.White;
            button_subAddBillCredit.Location = new Point(0, 31);
            button_subAddBillCredit.Name = "button_subAddBillCredit";
            button_subAddBillCredit.Size = new Size(185, 30);
            button_subAddBillCredit.TabIndex = 1;
            button_subAddBillCredit.Text = "เพิ่มบิล/ยกเลิก(ORNC)";
            button_subAddBillCredit.UseVisualStyleBackColor = false;
            button_subAddBillCredit.Click += button_subAddBillCredit_Click;
            // 
            // button_subAddCustomer
            // 
            button_subAddCustomer.BackColor = Color.DodgerBlue;
            button_subAddCustomer.FlatAppearance.BorderSize = 0;
            button_subAddCustomer.FlatStyle = FlatStyle.Flat;
            button_subAddCustomer.Font = new Font("Segoe UI", 10F);
            button_subAddCustomer.ForeColor = Color.White;
            button_subAddCustomer.Location = new Point(0, 0);
            button_subAddCustomer.Name = "button_subAddCustomer";
            button_subAddCustomer.Size = new Size(185, 30);
            button_subAddCustomer.TabIndex = 0;
            button_subAddCustomer.Text = "เพิ่มลูกค้า";
            button_subAddCustomer.UseVisualStyleBackColor = false;
            button_subAddCustomer.Click += button_subAddCustomer_Click;
            // 
            // panel_submenuCreditIn
            // 
            panel_submenuCreditIn.Controls.Add(BtnSub_EditOrcc);
            panel_submenuCreditIn.Controls.Add(BtnSub_AddOrcc);
            panel_submenuCreditIn.Controls.Add(btnsubCashIN);
            panel_submenuCreditIn.Dock = DockStyle.Top;
            panel_submenuCreditIn.Location = new Point(0, 130);
            panel_submenuCreditIn.Name = "panel_submenuCreditIn";
            panel_submenuCreditIn.Size = new Size(210, 104);
            panel_submenuCreditIn.TabIndex = 2;
            // 
            // BtnSub_EditOrcc
            // 
            BtnSub_EditOrcc.BackColor = Color.DodgerBlue;
            BtnSub_EditOrcc.FlatAppearance.BorderSize = 0;
            BtnSub_EditOrcc.FlatStyle = FlatStyle.Flat;
            BtnSub_EditOrcc.Font = new Font("Segoe UI", 10F);
            BtnSub_EditOrcc.ForeColor = Color.White;
            BtnSub_EditOrcc.Location = new Point(3, 62);
            BtnSub_EditOrcc.Name = "BtnSub_EditOrcc";
            BtnSub_EditOrcc.Size = new Size(185, 30);
            BtnSub_EditOrcc.TabIndex = 4;
            BtnSub_EditOrcc.Text = "แก้ไขใบเสร็จ (ORCC)";
            BtnSub_EditOrcc.UseVisualStyleBackColor = false;
            BtnSub_EditOrcc.Click += BtnSub_EditOrcc_Click;
            // 
            // BtnSub_AddOrcc
            // 
            BtnSub_AddOrcc.BackColor = Color.DodgerBlue;
            BtnSub_AddOrcc.FlatAppearance.BorderSize = 0;
            BtnSub_AddOrcc.FlatStyle = FlatStyle.Flat;
            BtnSub_AddOrcc.Font = new Font("Segoe UI", 10F);
            BtnSub_AddOrcc.ForeColor = Color.White;
            BtnSub_AddOrcc.Location = new Point(3, 31);
            BtnSub_AddOrcc.Name = "BtnSub_AddOrcc";
            BtnSub_AddOrcc.Size = new Size(185, 30);
            BtnSub_AddOrcc.TabIndex = 4;
            BtnSub_AddOrcc.Text = "เพิ่มบิล (ORCC)";
            BtnSub_AddOrcc.UseVisualStyleBackColor = false;
            BtnSub_AddOrcc.Click += BtnSub_AddOrcc_Click;
            // 
            // btnsubCashIN
            // 
            btnsubCashIN.BackColor = Color.DodgerBlue;
            btnsubCashIN.FlatAppearance.BorderSize = 0;
            btnsubCashIN.FlatStyle = FlatStyle.Flat;
            btnsubCashIN.Font = new Font("Segoe UI", 10F);
            btnsubCashIN.ForeColor = Color.White;
            btnsubCashIN.Location = new Point(3, 0);
            btnsubCashIN.Name = "btnsubCashIN";
            btnsubCashIN.Size = new Size(185, 30);
            btnsubCashIN.TabIndex = 1;
            btnsubCashIN.Text = "ใบเสร็จรับเงิน(ORCC)";
            btnsubCashIN.UseVisualStyleBackColor = false;
            btnsubCashIN.Click += btnsubCashIN_Click;
            // 
            // Btn_AddUser
            // 
            Btn_AddUser.FlatAppearance.BorderSize = 0;
            Btn_AddUser.FlatStyle = FlatStyle.Flat;
            Btn_AddUser.Font = new Font("Segoe UI", 10F);
            Btn_AddUser.ForeColor = Color.White;
            Btn_AddUser.Location = new Point(3, 721);
            Btn_AddUser.Name = "Btn_AddUser";
            Btn_AddUser.Size = new Size(185, 30);
            Btn_AddUser.TabIndex = 3;
            Btn_AddUser.Text = "ผู้ใช้งาน";
            Btn_AddUser.TextAlign = ContentAlignment.MiddleLeft;
            Btn_AddUser.UseVisualStyleBackColor = true;
            Btn_AddUser.Click += Btn_AddUser_Click;
            // 
            // buttonPayback
            // 
            buttonPayback.FlatAppearance.BorderSize = 0;
            buttonPayback.FlatStyle = FlatStyle.Flat;
            buttonPayback.Font = new Font("Segoe UI", 10F);
            buttonPayback.ForeColor = Color.White;
            buttonPayback.Location = new Point(3, 547);
            buttonPayback.Name = "buttonPayback";
            buttonPayback.Size = new Size(185, 30);
            buttonPayback.TabIndex = 3;
            buttonPayback.Text = "จ่ายคืน";
            buttonPayback.TextAlign = ContentAlignment.MiddleLeft;
            buttonPayback.UseVisualStyleBackColor = true;
            buttonPayback.Click += button_PayBack_Click;
            // 
            // buttonCreditOutMain
            // 
            buttonCreditOutMain.BackColor = Color.RoyalBlue;
            buttonCreditOutMain.FlatAppearance.BorderSize = 0;
            buttonCreditOutMain.FlatStyle = FlatStyle.Flat;
            buttonCreditOutMain.Font = new Font("Segoe UI", 10F);
            buttonCreditOutMain.ForeColor = Color.White;
            buttonCreditOutMain.Location = new Point(0, 240);
            buttonCreditOutMain.Name = "buttonCreditOutMain";
            buttonCreditOutMain.Size = new Size(180, 30);
            buttonCreditOutMain.TabIndex = 1;
            buttonCreditOutMain.Text = "เครดิตออก";
            buttonCreditOutMain.TextAlign = ContentAlignment.MiddleLeft;
            buttonCreditOutMain.UseVisualStyleBackColor = false;
            buttonCreditOutMain.Click += button_CreditOutMain_Click;
            // 
            // button_CreditInMain
            // 
            button_CreditInMain.Dock = DockStyle.Top;
            button_CreditInMain.FlatAppearance.BorderSize = 0;
            button_CreditInMain.FlatStyle = FlatStyle.Flat;
            button_CreditInMain.Font = new Font("Segoe UI", 10F);
            button_CreditInMain.ForeColor = Color.White;
            button_CreditInMain.Location = new Point(0, 100);
            button_CreditInMain.Name = "button_CreditInMain";
            button_CreditInMain.Size = new Size(210, 30);
            button_CreditInMain.TabIndex = 2;
            button_CreditInMain.Text = "เครดิตเข้า";
            button_CreditInMain.TextAlign = ContentAlignment.MiddleLeft;
            button_CreditInMain.UseVisualStyleBackColor = true;
            button_CreditInMain.Click += button_CreditInMain_Click;
            // 
            // panel_logo
            // 
            panel_logo.Controls.Add(label1_LOGO);
            panel_logo.Dock = DockStyle.Top;
            panel_logo.Location = new Point(0, 0);
            panel_logo.Name = "panel_logo";
            panel_logo.Size = new Size(210, 100);
            panel_logo.TabIndex = 1;
            // 
            // label1_LOGO
            // 
            label1_LOGO.AutoSize = true;
            label1_LOGO.Font = new Font("Broadway", 15.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1_LOGO.ForeColor = Color.Goldenrod;
            label1_LOGO.Location = new Point(12, 33);
            label1_LOGO.Name = "label1_LOGO";
            label1_LOGO.Size = new Size(140, 24);
            label1_LOGO.TabIndex = 0;
            label1_LOGO.Text = "GD CREDIT";
            label1_LOGO.TextAlign = ContentAlignment.MiddleCenter;
            label1_LOGO.Click += label1_LOGO_Click;
            // 
            // label_welcome
            // 
            label_welcome.AutoSize = true;
            label_welcome.Font = new Font("Segoe UI", 12F);
            label_welcome.ForeColor = Color.White;
            label_welcome.Location = new Point(9, 48);
            label_welcome.Name = "label_welcome";
            label_welcome.Size = new Size(87, 21);
            label_welcome.TabIndex = 0;
            label_welcome.Text = "ยินดีต้อนรับ :";
            // 
            // label_TotalDeposit
            // 
            label_TotalDeposit.AutoSize = true;
            label_TotalDeposit.Font = new Font("Segoe UI", 12F);
            label_TotalDeposit.ForeColor = Color.White;
            label_TotalDeposit.Location = new Point(303, 19);
            label_TotalDeposit.Name = "label_TotalDeposit";
            label_TotalDeposit.Size = new Size(0, 21);
            label_TotalDeposit.TabIndex = 3;
            // 
            // panel_main
            // 
            panel_main.Controls.Add(panel2);
            panel_main.Controls.Add(panel_cover);
            panel_main.Dock = DockStyle.Fill;
            panel_main.Location = new Point(210, 0);
            panel_main.Name = "panel_main";
            panel_main.Size = new Size(1174, 861);
            panel_main.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.RoyalBlue;
            panel2.Controls.Add(lbl_MainTotalOrnc);
            panel2.Controls.Add(label_TotalDeposit);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 803);
            panel2.Name = "panel2";
            panel2.Size = new Size(1174, 58);
            panel2.TabIndex = 2;
            // 
            // lbl_MainTotalOrnc
            // 
            lbl_MainTotalOrnc.AutoSize = true;
            lbl_MainTotalOrnc.Font = new Font("Segoe UI", 12F);
            lbl_MainTotalOrnc.ForeColor = Color.White;
            lbl_MainTotalOrnc.Location = new Point(18, 19);
            lbl_MainTotalOrnc.Name = "lbl_MainTotalOrnc";
            lbl_MainTotalOrnc.Size = new Size(0, 21);
            lbl_MainTotalOrnc.TabIndex = 2;
            // 
            // panel_cover
            // 
            panel_cover.Controls.Add(panel1);
            panel_cover.Controls.Add(panel3);
            panel_cover.Dock = DockStyle.Fill;
            panel_cover.Location = new Point(0, 0);
            panel_cover.Name = "panel_cover";
            panel_cover.Size = new Size(1174, 861);
            panel_cover.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(panel4);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 100);
            panel1.Name = "panel1";
            panel1.Size = new Size(1174, 704);
            panel1.TabIndex = 4;
            // 
            // panel4
            // 
            panel4.AutoScroll = true;
            panel4.AutoSize = true;
            panel4.BackColor = Color.White;
            panel4.Controls.Add(chart1);
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(35, 0, 0, 0);
            panel4.Size = new Size(1174, 704);
            panel4.TabIndex = 3;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            chart1.Dock = DockStyle.Fill;
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new Point(35, 0);
            chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedBar;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart1.Series.Add(series1);
            chart1.Size = new Size(1139, 704);
            chart1.TabIndex = 3;
            chart1.Text = "chart1";
            // 
            // panel3
            // 
            panel3.BackColor = Color.RoyalBlue;
            panel3.Controls.Add(lblUserRole);
            panel3.Controls.Add(lblUserInfo);
            panel3.Controls.Add(label_welcome);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(1174, 100);
            panel3.TabIndex = 0;
            // 
            // lblUserRole
            // 
            lblUserRole.AutoSize = true;
            lblUserRole.Font = new Font("Segoe UI", 12F);
            lblUserRole.ForeColor = Color.White;
            lblUserRole.Location = new Point(128, 69);
            lblUserRole.Name = "lblUserRole";
            lblUserRole.Size = new Size(0, 21);
            lblUserRole.TabIndex = 0;
            // 
            // lblUserInfo
            // 
            lblUserInfo.AutoSize = true;
            lblUserInfo.Font = new Font("Segoe UI", 12F);
            lblUserInfo.ForeColor = Color.White;
            lblUserInfo.Location = new Point(128, 48);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(0, 21);
            lblUserInfo.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1384, 861);
            Controls.Add(panel_main);
            Controls.Add(panel_slide);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1250, 900);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainForm";
            Load += MainForm_Load;
            panel_slide.ResumeLayout(false);
            panel_submenuAddUser.ResumeLayout(false);
            panel_submenuPayBack.ResumeLayout(false);
            panel_submenuCreditOut.ResumeLayout(false);
            panel_submenuCreditIn.ResumeLayout(false);
            panel_logo.ResumeLayout(false);
            panel_logo.PerformLayout();
            panel_main.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel_cover.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel_slide;
        private Button button_CreditInMain;
        private Panel panel_logo;
        private Panel panel_submenuCreditOut;
        private Button button_subAddCustomer;
        private Button button_subAddRealBill;
        private Button button_subAddBillCredit;
        private Label label1_LOGO;
        private Label label_welcome;
        private Label label_TotalDeposit;
        private Label label_TotalCreditOut;
        private Panel panel_main;
        private Panel panel_cover;
        private Panel panel2;
        private Panel panel3;
        private Button button_subReport;
        private Button buttonCreditOutMain;
        private Panel panel_submenuCreditIn;
        private Label lblUserInfo;
        private Label lblUserRole;
        private Button btnsubCashIN;
        private Label lbl_MainTotalOrnc;
        private Button btn_DpReport;
        private Button btnSubApprove;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private Panel panel1;
        private Panel panel_submenuPayBack;
        private Button buttonPayback;
        private Button BtnSub_AddOrcc;
        private Panel panel4;
        private Button btn_subOrvcAdd2;
        private Button Btn_subAddUser;
        private Button Btn_AddUser;
        private Panel panel_submenuAddUser;
        private Button Btn_UpdateGDAPP;
        private Button Btn_NotPaid;
        private Button BtnSub_Expeness;
        private Button BtnSub_EditOrcc;
    }
}