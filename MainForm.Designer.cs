namespace IRCclient
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.tLine = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.cScrollEnd = new System.Windows.Forms.CheckBox();
			this.bUpdate = new System.Windows.Forms.Button();
			this.bOption = new System.Windows.Forms.Button();
			this.lPing = new System.Windows.Forms.Label();
			this.tNick = new System.Windows.Forms.TextBox();
			this.lNick = new System.Windows.Forms.Label();
			this.bConnect = new System.Windows.Forms.Button();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.groupContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.bNew = new System.Windows.Forms.ToolStripMenuItem();
			this.bClose = new System.Windows.Forms.ToolStripMenuItem();
			this.panel1.SuspendLayout();
			this.groupContext.SuspendLayout();
			this.SuspendLayout();
			// 
			// tLine
			// 
			this.tLine.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tLine.Location = new System.Drawing.Point(0, 517);
			this.tLine.Name = "tLine";
			this.tLine.Size = new System.Drawing.Size(752, 21);
			this.tLine.TabIndex = 0;
			this.tLine.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tLine_KeyPress);
			this.tLine.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tLine_PreviewKeyDown);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.cScrollEnd);
			this.panel1.Controls.Add(this.bUpdate);
			this.panel1.Controls.Add(this.bOption);
			this.panel1.Controls.Add(this.lPing);
			this.panel1.Controls.Add(this.tNick);
			this.panel1.Controls.Add(this.lNick);
			this.panel1.Controls.Add(this.bConnect);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(752, 35);
			this.panel1.TabIndex = 1;
			// 
			// cScrollEnd
			// 
			this.cScrollEnd.AutoSize = true;
			this.cScrollEnd.Checked = true;
			this.cScrollEnd.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cScrollEnd.Location = new System.Drawing.Point(465, 10);
			this.cScrollEnd.Name = "cScrollEnd";
			this.cScrollEnd.Size = new System.Drawing.Size(95, 16);
			this.cScrollEnd.TabIndex = 7;
			this.cScrollEnd.TabStop = false;
			this.cScrollEnd.Text = "Scroll to end";
			this.cScrollEnd.UseVisualStyleBackColor = true;
			this.cScrollEnd.CheckedChanged += new System.EventHandler(this.cScrollEnd_CheckedChanged);
			// 
			// bUpdate
			// 
			this.bUpdate.Location = new System.Drawing.Point(365, 7);
			this.bUpdate.Name = "bUpdate";
			this.bUpdate.Size = new System.Drawing.Size(90, 20);
			this.bUpdate.TabIndex = 5;
			this.bUpdate.TabStop = false;
			this.bUpdate.Text = "Update";
			this.bUpdate.UseVisualStyleBackColor = true;
			this.bUpdate.Click += new System.EventHandler(this.bUpdate_Click);
			// 
			// bOption
			// 
			this.bOption.Location = new System.Drawing.Point(270, 7);
			this.bOption.Name = "bOption";
			this.bOption.Size = new System.Drawing.Size(90, 21);
			this.bOption.TabIndex = 4;
			this.bOption.TabStop = false;
			this.bOption.Text = "Options...";
			this.bOption.UseVisualStyleBackColor = true;
			this.bOption.Click += new System.EventHandler(this.bOption_Click);
			// 
			// lPing
			// 
			this.lPing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lPing.Location = new System.Drawing.Point(632, 11);
			this.lPing.Name = "lPing";
			this.lPing.Size = new System.Drawing.Size(108, 12);
			this.lPing.TabIndex = 6;
			this.lPing.Text = "Disconnected";
			this.lPing.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tNick
			// 
			this.tNick.Location = new System.Drawing.Point(45, 7);
			this.tNick.Name = "tNick";
			this.tNick.Size = new System.Drawing.Size(125, 21);
			this.tNick.TabIndex = 2;
			this.tNick.TabStop = false;
			this.tNick.Text = "Yoshi[-1]";
			this.tNick.TextChanged += new System.EventHandler(this.tNick_TextChanged);
			this.tNick.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NickChange);
			this.tNick.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tLine_PreviewKeyDown);
			// 
			// lNick
			// 
			this.lNick.AutoSize = true;
			this.lNick.Location = new System.Drawing.Point(9, 11);
			this.lNick.Name = "lNick";
			this.lNick.Size = new System.Drawing.Size(30, 12);
			this.lNick.TabIndex = 1;
			this.lNick.Text = "Nick";
			// 
			// bConnect
			// 
			this.bConnect.Location = new System.Drawing.Point(175, 7);
			this.bConnect.Name = "bConnect";
			this.bConnect.Size = new System.Drawing.Size(90, 21);
			this.bConnect.TabIndex = 3;
			this.bConnect.TabStop = false;
			this.bConnect.Text = "Connect";
			this.bConnect.UseVisualStyleBackColor = true;
			this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
			// 
			// tabControl
			// 
			this.tabControl.ContextMenuStrip = this.groupContext;
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 35);
			this.tabControl.Multiline = true;
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(752, 482);
			this.tabControl.TabIndex = 8;
			this.tabControl.TabStop = false;
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
			this.tabControl.Click += new System.EventHandler(this.tabControl_Click);
			// 
			// groupContext
			// 
			this.groupContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bNew,
            this.bClose});
			this.groupContext.Name = "chanContext";
			this.groupContext.Size = new System.Drawing.Size(185, 48);
			this.groupContext.Opening += new System.ComponentModel.CancelEventHandler(this.groupContext_Opening);
			// 
			// bNew
			// 
			this.bNew.Name = "bNew";
			this.bNew.Size = new System.Drawing.Size(184, 22);
			this.bNew.Text = "New server window";
			this.bNew.Click += new System.EventHandler(this.newServerWindowToolStripMenuItem_Click);
			// 
			// bClose
			// 
			this.bClose.Name = "bClose";
			this.bClose.Size = new System.Drawing.Size(184, 22);
			this.bClose.Text = "Close server window";
			this.bClose.Click += new System.EventHandler(this.bClose_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(752, 538);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.tLine);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "Minus One IRC-v1.1.2.58 by 2009.11.29-2010.11.18 Yoshi-TS4";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.groupContext.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tLine;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lNick;
		public System.Windows.Forms.Button bConnect;
		private System.Windows.Forms.Label lPing;
		private System.Windows.Forms.ContextMenuStrip groupContext;
		private System.Windows.Forms.ToolStripMenuItem bClose;
		private System.Windows.Forms.Button bOption;
		private System.Windows.Forms.Button bUpdate;
		public System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.ToolStripMenuItem bNew;
		private System.Windows.Forms.TextBox tNick;
		public System.Windows.Forms.CheckBox cScrollEnd;
	}
}
