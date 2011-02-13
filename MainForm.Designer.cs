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
			this.bNewServer = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.bCloseServer = new System.Windows.Forms.ToolStripMenuItem();
			this.chanContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.bAddFav = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.bMoveLeft = new System.Windows.Forms.ToolStripMenuItem();
			this.bMoveRight = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.bClose = new System.Windows.Forms.ToolStripMenuItem();
			this.userContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.bWhois = new System.Windows.Forms.ToolStripMenuItem();
			this.bQuery = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.bOp = new System.Windows.Forms.ToolStripMenuItem();
			this.bDeop = new System.Windows.Forms.ToolStripMenuItem();
			this.bVoice = new System.Windows.Forms.ToolStripMenuItem();
			this.bDevoice = new System.Windows.Forms.ToolStripMenuItem();
			this.bKick = new System.Windows.Forms.ToolStripMenuItem();
			this.bKickReason = new System.Windows.Forms.ToolStripMenuItem();
			this.bBan = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.bVersion = new System.Windows.Forms.ToolStripMenuItem();
			this.panel1.SuspendLayout();
			this.groupContext.SuspendLayout();
			this.chanContext.SuspendLayout();
			this.userContext.SuspendLayout();
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
			this.tNick.Text = "Nick[-1]";
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
			this.tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tabControl.ContextMenuStrip = this.groupContext;
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.tabControl.Location = new System.Drawing.Point(0, 35);
			this.tabControl.Multiline = true;
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(752, 482);
			this.tabControl.TabIndex = 8;
			this.tabControl.TabStop = false;
			this.tabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl_DrawItem);
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
			this.tabControl.Click += new System.EventHandler(this.tabControl_Click);
			// 
			// groupContext
			// 
			this.groupContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bNewServer,
            this.toolStripMenuItem1,
            this.bCloseServer});
			this.groupContext.Name = "chanContext";
			this.groupContext.Size = new System.Drawing.Size(185, 54);
			this.groupContext.Opening += new System.ComponentModel.CancelEventHandler(this.groupContext_Opening);
			// 
			// bNewServer
			// 
			this.bNewServer.Name = "bNewServer";
			this.bNewServer.Size = new System.Drawing.Size(184, 22);
			this.bNewServer.Text = "New server window";
			this.bNewServer.Click += new System.EventHandler(this.addNewTab);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(181, 6);
			// 
			// bCloseServer
			// 
			this.bCloseServer.Name = "bCloseServer";
			this.bCloseServer.Size = new System.Drawing.Size(184, 22);
			this.bCloseServer.Text = "Close server window";
			this.bCloseServer.Click += new System.EventHandler(this.bCloseServer_Click);
			// 
			// chanContext
			// 
			this.chanContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bAddFav,
            this.toolStripMenuItem2,
            this.bMoveLeft,
            this.bMoveRight,
            this.toolStripMenuItem3,
            this.bClose});
			this.chanContext.Name = "chanContext";
			this.chanContext.Size = new System.Drawing.Size(196, 104);
			this.chanContext.Opening += new System.ComponentModel.CancelEventHandler(this.chanContext_Opening);
			// 
			// bAddFav
			// 
			this.bAddFav.Name = "bAddFav";
			this.bAddFav.Size = new System.Drawing.Size(195, 22);
			this.bAddFav.Text = "Add to favorite";
			this.bAddFav.Click += new System.EventHandler(this.bAddFav_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(192, 6);
			// 
			// bMoveLeft
			// 
			this.bMoveLeft.Name = "bMoveLeft";
			this.bMoveLeft.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
			this.bMoveLeft.Size = new System.Drawing.Size(195, 22);
			this.bMoveLeft.Text = "Move left";
			this.bMoveLeft.Click += new System.EventHandler(this.bMoveLeft_Click);
			// 
			// bMoveRight
			// 
			this.bMoveRight.Name = "bMoveRight";
			this.bMoveRight.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
			this.bMoveRight.Size = new System.Drawing.Size(195, 22);
			this.bMoveRight.Text = "Move right";
			this.bMoveRight.Click += new System.EventHandler(this.bMoveRight_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(192, 6);
			// 
			// bClose
			// 
			this.bClose.Name = "bClose";
			this.bClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
			this.bClose.Size = new System.Drawing.Size(195, 22);
			this.bClose.Text = "Close";
			this.bClose.Click += new System.EventHandler(this.bClose_Click);
			// 
			// userContext
			// 
			this.userContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bWhois,
            this.bQuery,
            this.toolStripMenuItem4,
            this.bOp,
            this.bDeop,
            this.bVoice,
            this.bDevoice,
            this.bKick,
            this.bKickReason,
            this.bBan,
            this.toolStripMenuItem5,
            this.bVersion});
			this.userContext.Name = "userContext";
			this.userContext.Size = new System.Drawing.Size(154, 236);
			// 
			// bWhois
			// 
			this.bWhois.Name = "bWhois";
			this.bWhois.Size = new System.Drawing.Size(153, 22);
			this.bWhois.Text = "Whois";
			this.bWhois.Click += new System.EventHandler(this.bWhois_Click);
			// 
			// bQuery
			// 
			this.bQuery.Name = "bQuery";
			this.bQuery.Size = new System.Drawing.Size(153, 22);
			this.bQuery.Text = "Query";
			this.bQuery.Click += new System.EventHandler(this.bQuery_Click);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(150, 6);
			// 
			// bOp
			// 
			this.bOp.Name = "bOp";
			this.bOp.Size = new System.Drawing.Size(153, 22);
			this.bOp.Text = "Op";
			this.bOp.Click += new System.EventHandler(this.bpOp_Click);
			// 
			// bDeop
			// 
			this.bDeop.Name = "bDeop";
			this.bDeop.Size = new System.Drawing.Size(153, 22);
			this.bDeop.Text = "Deop";
			this.bDeop.Click += new System.EventHandler(this.bpDeop_Click);
			// 
			// bVoice
			// 
			this.bVoice.Name = "bVoice";
			this.bVoice.Size = new System.Drawing.Size(153, 22);
			this.bVoice.Text = "Voice";
			this.bVoice.Click += new System.EventHandler(this.bpVoice_Click);
			// 
			// bDevoice
			// 
			this.bDevoice.Name = "bDevoice";
			this.bDevoice.Size = new System.Drawing.Size(153, 22);
			this.bDevoice.Text = "Devoice";
			this.bDevoice.Click += new System.EventHandler(this.bpDevoice_Click);
			// 
			// bKick
			// 
			this.bKick.Name = "bKick";
			this.bKick.Size = new System.Drawing.Size(153, 22);
			this.bKick.Text = "Kick";
			this.bKick.Click += new System.EventHandler(this.bpKick_Click);
			// 
			// bKickReason
			// 
			this.bKickReason.Name = "bKickReason";
			this.bKickReason.Size = new System.Drawing.Size(153, 22);
			this.bKickReason.Text = "Kick w/ reason";
			this.bKickReason.Click += new System.EventHandler(this.bpKickReason_Click);
			// 
			// bBan
			// 
			this.bBan.Enabled = false;
			this.bBan.Name = "bBan";
			this.bBan.Size = new System.Drawing.Size(153, 22);
			this.bBan.Text = "Ban";
			this.bBan.Click += new System.EventHandler(this.bpBan_Click);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(150, 6);
			// 
			// bVersion
			// 
			this.bVersion.Name = "bVersion";
			this.bVersion.Size = new System.Drawing.Size(153, 22);
			this.bVersion.Text = "VERSION";
			this.bVersion.Click += new System.EventHandler(this.bcVERSION_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(752, 538);
			this.ContextMenuStrip = this.groupContext;
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.tLine);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "Minus One IRC-[ver] by 2009.11.29-2011.2.14 Yoshi-TS4";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.groupContext.ResumeLayout(false);
			this.chanContext.ResumeLayout(false);
			this.userContext.ResumeLayout(false);
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
		private System.Windows.Forms.ToolStripMenuItem bCloseServer;
		private System.Windows.Forms.Button bOption;
		private System.Windows.Forms.Button bUpdate;
		public System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.ToolStripMenuItem bNewServer;
		private System.Windows.Forms.TextBox tNick;
		public System.Windows.Forms.CheckBox cScrollEnd;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ContextMenuStrip chanContext;
		private System.Windows.Forms.ToolStripMenuItem bAddFav;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem bMoveLeft;
		private System.Windows.Forms.ToolStripMenuItem bMoveRight;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem bClose;
		private System.Windows.Forms.ToolStripMenuItem bWhois;
		private System.Windows.Forms.ToolStripMenuItem bQuery;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem bOp;
		private System.Windows.Forms.ToolStripMenuItem bDeop;
		private System.Windows.Forms.ToolStripMenuItem bVoice;
		private System.Windows.Forms.ToolStripMenuItem bDevoice;
		private System.Windows.Forms.ToolStripMenuItem bKick;
		private System.Windows.Forms.ToolStripMenuItem bKickReason;
		private System.Windows.Forms.ToolStripMenuItem bBan;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
		private System.Windows.Forms.ToolStripMenuItem bVersion;
		public System.Windows.Forms.ContextMenuStrip userContext;
	}
}
