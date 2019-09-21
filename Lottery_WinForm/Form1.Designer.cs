namespace Lottery_WinForm
{
    partial class ThisGo
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Cookie_TxtBox = new System.Windows.Forms.TextBox();
            this.Read_Btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Balance_Text = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.AccumulatedProfit_TextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SingleNoteAmount_TextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CycleTimes_TextBox = new System.Windows.Forms.TextBox();
            this.implement_Btn = new System.Windows.Forms.Button();
            this.Record_List = new System.Windows.Forms.ListView();
            this.DateTime = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.CumulativeBuying_TextBox = new System.Windows.Forms.TextBox();
            this.StopIt_Btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Cookie_TxtBox
            // 
            this.Cookie_TxtBox.Location = new System.Drawing.Point(84, 29);
            this.Cookie_TxtBox.Name = "Cookie_TxtBox";
            this.Cookie_TxtBox.Size = new System.Drawing.Size(179, 21);
            this.Cookie_TxtBox.TabIndex = 0;
            // 
            // Read_Btn
            // 
            this.Read_Btn.Location = new System.Drawing.Point(276, 29);
            this.Read_Btn.Name = "Read_Btn";
            this.Read_Btn.Size = new System.Drawing.Size(73, 21);
            this.Read_Btn.TabIndex = 1;
            this.Read_Btn.Text = "读取";
            this.Read_Btn.UseVisualStyleBackColor = true;
            this.Read_Btn.Click += new System.EventHandler(this.Read_Btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Cookies：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(381, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "账户金额：";
            // 
            // Balance_Text
            // 
            this.Balance_Text.Location = new System.Drawing.Point(453, 30);
            this.Balance_Text.Name = "Balance_Text";
            this.Balance_Text.ReadOnly = true;
            this.Balance_Text.Size = new System.Drawing.Size(91, 21);
            this.Balance_Text.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(574, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "累计盈利金额：";
            // 
            // AccumulatedProfit_TextBox
            // 
            this.AccumulatedProfit_TextBox.Location = new System.Drawing.Point(677, 31);
            this.AccumulatedProfit_TextBox.Name = "AccumulatedProfit_TextBox";
            this.AccumulatedProfit_TextBox.ReadOnly = true;
            this.AccumulatedProfit_TextBox.Size = new System.Drawing.Size(93, 21);
            this.AccumulatedProfit_TextBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(150, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "单注金额：";
            // 
            // SingleNoteAmount_TextBox
            // 
            this.SingleNoteAmount_TextBox.Location = new System.Drawing.Point(217, 79);
            this.SingleNoteAmount_TextBox.Name = "SingleNoteAmount_TextBox";
            this.SingleNoteAmount_TextBox.Size = new System.Drawing.Size(45, 21);
            this.SingleNoteAmount_TextBox.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "循环次数：";
            // 
            // CycleTimes_TextBox
            // 
            this.CycleTimes_TextBox.Location = new System.Drawing.Point(83, 78);
            this.CycleTimes_TextBox.Name = "CycleTimes_TextBox";
            this.CycleTimes_TextBox.Size = new System.Drawing.Size(57, 21);
            this.CycleTimes_TextBox.TabIndex = 8;
            // 
            // implement_Btn
            // 
            this.implement_Btn.Location = new System.Drawing.Point(276, 80);
            this.implement_Btn.Name = "implement_Btn";
            this.implement_Btn.Size = new System.Drawing.Size(73, 21);
            this.implement_Btn.TabIndex = 12;
            this.implement_Btn.Text = "执行";
            this.implement_Btn.UseVisualStyleBackColor = true;
            // 
            // Record_List
            // 
            this.Record_List.HideSelection = false;
            this.Record_List.Location = new System.Drawing.Point(44, 107);
            this.Record_List.Name = "Record_List";
            this.Record_List.Size = new System.Drawing.Size(835, 365);
            this.Record_List.TabIndex = 13;
            this.Record_List.UseCompatibleStateImageBehavior = false;
            // 
            // DateTime
            // 
            this.DateTime.AutoSize = true;
            this.DateTime.Location = new System.Drawing.Point(770, 494);
            this.DateTime.Name = "DateTime";
            this.DateTime.Size = new System.Drawing.Size(41, 12);
            this.DateTime.TabIndex = 14;
            this.DateTime.Text = "label6";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(574, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "累计买入次数：";
            // 
            // CumulativeBuying_TextBox
            // 
            this.CumulativeBuying_TextBox.Location = new System.Drawing.Point(677, 75);
            this.CumulativeBuying_TextBox.Name = "CumulativeBuying_TextBox";
            this.CumulativeBuying_TextBox.ReadOnly = true;
            this.CumulativeBuying_TextBox.Size = new System.Drawing.Size(93, 21);
            this.CumulativeBuying_TextBox.TabIndex = 16;
            // 
            // StopIt_Btn
            // 
            this.StopIt_Btn.Location = new System.Drawing.Point(355, 80);
            this.StopIt_Btn.Name = "StopIt_Btn";
            this.StopIt_Btn.Size = new System.Drawing.Size(73, 21);
            this.StopIt_Btn.TabIndex = 17;
            this.StopIt_Btn.Text = "停止";
            this.StopIt_Btn.UseVisualStyleBackColor = true;
            // 
            // ThisGo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 515);
            this.Controls.Add(this.StopIt_Btn);
            this.Controls.Add(this.CumulativeBuying_TextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.DateTime);
            this.Controls.Add(this.Record_List);
            this.Controls.Add(this.implement_Btn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SingleNoteAmount_TextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CycleTimes_TextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.AccumulatedProfit_TextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Balance_Text);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Read_Btn);
            this.Controls.Add(this.Cookie_TxtBox);
            this.Name = "ThisGo";
            this.Text = "恭喜发财";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Cookie_TxtBox;
        private System.Windows.Forms.Button Read_Btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Balance_Text;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox AccumulatedProfit_TextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox SingleNoteAmount_TextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox CycleTimes_TextBox;
        private System.Windows.Forms.Button implement_Btn;
        private System.Windows.Forms.ListView Record_List;
        private System.Windows.Forms.Label DateTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox CumulativeBuying_TextBox;
        private System.Windows.Forms.Button StopIt_Btn;
    }
}

