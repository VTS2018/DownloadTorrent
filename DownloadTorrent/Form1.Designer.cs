namespace DemoYunfile
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtCookie = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFileURLList = new System.Windows.Forms.TextBox();
            this.btnSaveAS = new System.Windows.Forms.Button();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.btnChengGetURL = new System.Windows.Forms.Button();
            this.btnZhuanChengtong = new System.Windows.Forms.Button();
            this.btnImportDURL = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnSaveV2 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.btnCheckTorrent = new System.Windows.Forms.Button();
            this.btnGetBTName = new System.Windows.Forms.Button();
            this.btnBTYibu = new System.Windows.Forms.Button();
            this.btnBTTongbu = new System.Windows.Forms.Button();
            this.btBTGongchang = new System.Windows.Forms.Button();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.btnSunFileDownLoad = new System.Windows.Forms.Button();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnDel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnImportList = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSetDir = new System.Windows.Forms.Button();
            this.btnIMPOR = new System.Windows.Forms.Button();
            this.btnTANce = new System.Windows.Forms.Button();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.txtDir = new System.Windows.Forms.TextBox();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.txtUserURL = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.btnParseTorrent = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.tabPage11.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入Cookie信息:";
            // 
            // txtCookie
            // 
            this.txtCookie.Location = new System.Drawing.Point(115, 11);
            this.txtCookie.Multiline = true;
            this.txtCookie.Name = "txtCookie";
            this.txtCookie.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCookie.Size = new System.Drawing.Size(482, 53);
            this.txtCookie.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "文件列表:";
            // 
            // txtFileURLList
            // 
            this.txtFileURLList.Location = new System.Drawing.Point(76, 141);
            this.txtFileURLList.Multiline = true;
            this.txtFileURLList.Name = "txtFileURLList";
            this.txtFileURLList.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFileURLList.Size = new System.Drawing.Size(325, 85);
            this.txtFileURLList.TabIndex = 3;
            // 
            // btnSaveAS
            // 
            this.btnSaveAS.Location = new System.Drawing.Point(6, 6);
            this.btnSaveAS.Name = "btnSaveAS";
            this.btnSaveAS.Size = new System.Drawing.Size(87, 23);
            this.btnSaveAS.TabIndex = 4;
            this.btnSaveAS.Text = "转存V1版本";
            this.btnSaveAS.UseVisualStyleBackColor = true;
            this.btnSaveAS.Click += new System.EventHandler(this.btnSaveAS_Click);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(617, 10);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCode.Size = new System.Drawing.Size(466, 342);
            this.txtCode.TabIndex = 5;
            // 
            // btnChengGetURL
            // 
            this.btnChengGetURL.Location = new System.Drawing.Point(136, 16);
            this.btnChengGetURL.Name = "btnChengGetURL";
            this.btnChengGetURL.Size = new System.Drawing.Size(124, 23);
            this.btnChengGetURL.TabIndex = 6;
            this.btnChengGetURL.Text = "获得城通真实地址";
            this.btnChengGetURL.UseVisualStyleBackColor = true;
            this.btnChengGetURL.Click += new System.EventHandler(this.btnChengGetURL_Click);
            // 
            // btnZhuanChengtong
            // 
            this.btnZhuanChengtong.Location = new System.Drawing.Point(6, 16);
            this.btnZhuanChengtong.Name = "btnZhuanChengtong";
            this.btnZhuanChengtong.Size = new System.Drawing.Size(124, 23);
            this.btnZhuanChengtong.TabIndex = 7;
            this.btnZhuanChengtong.Text = "转存城通文件";
            this.btnZhuanChengtong.UseVisualStyleBackColor = true;
            this.btnZhuanChengtong.Click += new System.EventHandler(this.btnZhuanChengtong_Click);
            // 
            // btnImportDURL
            // 
            this.btnImportDURL.Location = new System.Drawing.Point(2, 6);
            this.btnImportDURL.Name = "btnImportDURL";
            this.btnImportDURL.Size = new System.Drawing.Size(92, 23);
            this.btnImportDURL.TabIndex = 9;
            this.btnImportDURL.Text = "导出下载地址";
            this.btnImportDURL.UseVisualStyleBackColor = true;
            this.btnImportDURL.Click += new System.EventHandler(this.btnImportDURL_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage10);
            this.tabControl1.Controls.Add(this.tabPage11);
            this.tabControl1.Location = new System.Drawing.Point(12, 358);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(585, 93);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnParseTorrent);
            this.tabPage1.Controls.Add(this.btnSaveV2);
            this.tabPage1.Controls.Add(this.btnSaveAS);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(577, 67);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Yunfile";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnSaveV2
            // 
            this.btnSaveV2.Location = new System.Drawing.Point(6, 35);
            this.btnSaveV2.Name = "btnSaveV2";
            this.btnSaveV2.Size = new System.Drawing.Size(87, 23);
            this.btnSaveV2.TabIndex = 5;
            this.btnSaveV2.Text = "转存V2改进版";
            this.btnSaveV2.UseVisualStyleBackColor = true;
            this.btnSaveV2.Click += new System.EventHandler(this.btnSaveV2_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnZhuanChengtong);
            this.tabPage2.Controls.Add(this.btnChengGetURL);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(577, 67);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "400GB";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage10
            // 
            this.tabPage10.Controls.Add(this.btnCheckTorrent);
            this.tabPage10.Controls.Add(this.btnGetBTName);
            this.tabPage10.Controls.Add(this.btnBTYibu);
            this.tabPage10.Controls.Add(this.btnBTTongbu);
            this.tabPage10.Controls.Add(this.btBTGongchang);
            this.tabPage10.Location = new System.Drawing.Point(4, 22);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Size = new System.Drawing.Size(577, 67);
            this.tabPage10.TabIndex = 5;
            this.tabPage10.Text = "BT工厂";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // btnCheckTorrent
            // 
            this.btnCheckTorrent.Location = new System.Drawing.Point(207, 3);
            this.btnCheckTorrent.Name = "btnCheckTorrent";
            this.btnCheckTorrent.Size = new System.Drawing.Size(75, 23);
            this.btnCheckTorrent.TabIndex = 2;
            this.btnCheckTorrent.Text = "BT完整检查";
            this.btnCheckTorrent.UseVisualStyleBackColor = true;
            this.btnCheckTorrent.Click += new System.EventHandler(this.btnCheckTorrent_Click);
            // 
            // btnGetBTName
            // 
            this.btnGetBTName.Location = new System.Drawing.Point(105, 31);
            this.btnGetBTName.Name = "btnGetBTName";
            this.btnGetBTName.Size = new System.Drawing.Size(96, 23);
            this.btnGetBTName.TabIndex = 1;
            this.btnGetBTName.Text = "BT种子真实名字";
            this.btnGetBTName.UseVisualStyleBackColor = true;
            this.btnGetBTName.Click += new System.EventHandler(this.btnGetBTName_Click);
            // 
            // btnBTYibu
            // 
            this.btnBTYibu.Location = new System.Drawing.Point(3, 31);
            this.btnBTYibu.Name = "btnBTYibu";
            this.btnBTYibu.Size = new System.Drawing.Size(96, 23);
            this.btnBTYibu.TabIndex = 0;
            this.btnBTYibu.Text = "单个异步测试";
            this.btnBTYibu.UseVisualStyleBackColor = true;
            this.btnBTYibu.Click += new System.EventHandler(this.btnBTYibu_Click);
            // 
            // btnBTTongbu
            // 
            this.btnBTTongbu.Location = new System.Drawing.Point(3, 3);
            this.btnBTTongbu.Name = "btnBTTongbu";
            this.btnBTTongbu.Size = new System.Drawing.Size(96, 23);
            this.btnBTTongbu.TabIndex = 0;
            this.btnBTTongbu.Text = "单个同步测试";
            this.btnBTTongbu.UseVisualStyleBackColor = true;
            this.btnBTTongbu.Click += new System.EventHandler(this.btnBTTongbu_Click);
            // 
            // btBTGongchang
            // 
            this.btBTGongchang.Location = new System.Drawing.Point(105, 3);
            this.btBTGongchang.Name = "btBTGongchang";
            this.btBTGongchang.Size = new System.Drawing.Size(96, 23);
            this.btBTGongchang.TabIndex = 0;
            this.btBTGongchang.Text = "开始下载";
            this.btBTGongchang.UseVisualStyleBackColor = true;
            this.btBTGongchang.Click += new System.EventHandler(this.btBTGongchang_Click);
            // 
            // tabPage11
            // 
            this.tabPage11.Controls.Add(this.btnSunFileDownLoad);
            this.tabPage11.Location = new System.Drawing.Point(4, 22);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Size = new System.Drawing.Size(577, 67);
            this.tabPage11.TabIndex = 6;
            this.tabPage11.Text = "sunfile";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // btnSunFileDownLoad
            // 
            this.btnSunFileDownLoad.Location = new System.Drawing.Point(3, 3);
            this.btnSunFileDownLoad.Name = "btnSunFileDownLoad";
            this.btnSunFileDownLoad.Size = new System.Drawing.Size(92, 23);
            this.btnSunFileDownLoad.TabIndex = 0;
            this.btnSunFileDownLoad.Text = "下载地址探测";
            this.btnSunFileDownLoad.UseVisualStyleBackColor = true;
            this.btnSunFileDownLoad.Click += new System.EventHandler(this.btnSunFileDownLoad_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Location = new System.Drawing.Point(12, 73);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(585, 279);
            this.tabControl2.TabIndex = 14;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnDel);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.btnImportList);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.btnSetDir);
            this.tabPage3.Controls.Add(this.btnIMPOR);
            this.tabPage3.Controls.Add(this.btnTANce);
            this.tabPage3.Controls.Add(this.txtNumber);
            this.tabPage3.Controls.Add(this.txtDir);
            this.tabPage3.Controls.Add(this.txtFilePath);
            this.tabPage3.Controls.Add(this.txtUserURL);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.txtFileURLList);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(577, 253);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "输 入";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(491, 62);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 7;
            this.btnDel.Text = "比较删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(413, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = " ";
            // 
            // btnImportList
            // 
            this.btnImportList.Location = new System.Drawing.Point(410, 141);
            this.btnImportList.Name = "btnImportList";
            this.btnImportList.Size = new System.Drawing.Size(75, 23);
            this.btnImportList.TabIndex = 5;
            this.btnImportList.Text = "导出列表";
            this.btnImportList.UseVisualStyleBackColor = true;
            this.btnImportList.Click += new System.EventHandler(this.btnImportList_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "设置数量:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "设置目录:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "文本导入:";
            // 
            // btnSetDir
            // 
            this.btnSetDir.Location = new System.Drawing.Point(410, 62);
            this.btnSetDir.Name = "btnSetDir";
            this.btnSetDir.Size = new System.Drawing.Size(75, 23);
            this.btnSetDir.TabIndex = 2;
            this.btnSetDir.Text = " 设 置 ";
            this.btnSetDir.UseVisualStyleBackColor = true;
            this.btnSetDir.Click += new System.EventHandler(this.btnSetDir_Click);
            // 
            // btnIMPOR
            // 
            this.btnIMPOR.Location = new System.Drawing.Point(410, 35);
            this.btnIMPOR.Name = "btnIMPOR";
            this.btnIMPOR.Size = new System.Drawing.Size(75, 23);
            this.btnIMPOR.TabIndex = 2;
            this.btnIMPOR.Text = "选择导入";
            this.btnIMPOR.UseVisualStyleBackColor = true;
            this.btnIMPOR.Click += new System.EventHandler(this.btnIMPOR_Click);
            // 
            // btnTANce
            // 
            this.btnTANce.Location = new System.Drawing.Point(410, 10);
            this.btnTANce.Name = "btnTANce";
            this.btnTANce.Size = new System.Drawing.Size(75, 23);
            this.btnTANce.TabIndex = 2;
            this.btnTANce.Text = "开始探测";
            this.btnTANce.UseVisualStyleBackColor = true;
            this.btnTANce.Click += new System.EventHandler(this.btnTANce_Click);
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(76, 91);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(325, 21);
            this.txtNumber.TabIndex = 1;
            this.txtNumber.Text = "1000";
            // 
            // txtDir
            // 
            this.txtDir.Location = new System.Drawing.Point(76, 64);
            this.txtDir.Name = "txtDir";
            this.txtDir.Size = new System.Drawing.Size(325, 21);
            this.txtDir.TabIndex = 1;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(76, 37);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(325, 21);
            this.txtFilePath.TabIndex = 1;
            // 
            // txtUserURL
            // 
            this.txtUserURL.Location = new System.Drawing.Point(76, 10);
            this.txtUserURL.Name = "txtUserURL";
            this.txtUserURL.Size = new System.Drawing.Size(325, 21);
            this.txtUserURL.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "用户地址:";
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage7);
            this.tabControl3.Location = new System.Drawing.Point(617, 358);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(466, 93);
            this.tabControl3.TabIndex = 15;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.btnImportDURL);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(458, 67);
            this.tabPage7.TabIndex = 0;
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // btnParseTorrent
            // 
            this.btnParseTorrent.Location = new System.Drawing.Point(491, 35);
            this.btnParseTorrent.Name = "btnParseTorrent";
            this.btnParseTorrent.Size = new System.Drawing.Size(75, 23);
            this.btnParseTorrent.TabIndex = 6;
            this.btnParseTorrent.Text = "解析种子";
            this.btnParseTorrent.UseVisualStyleBackColor = true;
            this.btnParseTorrent.Click += new System.EventHandler(this.btnParseTorrent_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 460);
            this.Controls.Add(this.tabControl3);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtCookie);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "网盘工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            this.tabPage11.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabControl3.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCookie;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFileURLList;
        private System.Windows.Forms.Button btnSaveAS;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Button btnChengGetURL;
        private System.Windows.Forms.Button btnZhuanChengtong;
        private System.Windows.Forms.Button btnImportDURL;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnTANce;
        private System.Windows.Forms.TextBox txtUserURL;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnIMPOR;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.Button btnImportList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSaveV2;
        private System.Windows.Forms.Button btnBTTongbu;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.Button btBTGongchang;
        private System.Windows.Forms.Button btnBTYibu;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSetDir;
        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.Button btnGetBTName;
        private System.Windows.Forms.Button btnCheckTorrent;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.Button btnSunFileDownLoad;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnParseTorrent;
    }
}

