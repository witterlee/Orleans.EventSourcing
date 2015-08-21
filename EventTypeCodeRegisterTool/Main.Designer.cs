using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Dotpay.EventTypeCodeRegisterTool
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private IContainer components = null;

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
            this.btnSelectFile = new Button();
            this.openFileDialog1 = new OpenFileDialog();
            this.txtFileName = new TextBox();
            this.label1 = new Label();
            this.btnGenerate = new Button();
            this.groupBox1 = new GroupBox();
            this.txtCode = new TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new Point(550, 26);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new Size(75, 23);
            this.btnSelectFile.TabIndex = 0;
            this.btnSelectFile.Text = "Select...";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new EventHandler(this.btnSelectFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new Point(197, 28);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new Size(347, 21);
            this.txtFileName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 10F, FontStyle.Bold);
            this.label1.Location = new Point(30, 30);
            this.label1.Name = "label1";
            this.label1.Size = new Size(94, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Event DLL：";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new Point(631, 26);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new Size(75, 23);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Regenerate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new EventHandler(this.btnGenerate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCode);
            this.groupBox1.Dock = DockStyle.Bottom;
            this.groupBox1.Location = new Point(0, 71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(708, 518);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Code";
            // 
            // txtCode
            // 
            this.txtCode.Dock = DockStyle.Fill;
            this.txtCode.Location = new Point(3, 17);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new Size(702, 498);
            this.txtCode.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new SizeF(6F, 12F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(708, 589);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnSelectFile);
            this.Name = "Main";
            this.Text = "EventTypeCodeGenerator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnSelectFile;
        private OpenFileDialog openFileDialog1;
        private TextBox txtFileName;
        private Label label1;
        private Button btnGenerate;
        private GroupBox groupBox1;
        private TextBox txtCode;
    }
}

