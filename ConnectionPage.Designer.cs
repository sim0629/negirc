using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace IRCclient
{
	public partial class ConnectionPage
	{
		public ConnectionPage(ServerGroup sGroup, string nick)
		{
			this.tabControl = new TabControl();
			this.pLog = new TabPage("Log\t");
			this.tLog = new RichTextBox();
			this.chanContext = new ContextMenuStrip();
			this.bAddFav = new ToolStripMenuItem();
			this.bClose = new ToolStripMenuItem();
			this.userContext = new ContextMenuStrip();
			this.bWhois = new ToolStripMenuItem();
			this.bQuery = new ToolStripMenuItem();
			this.bPrivilege = new ToolStripMenuItem();
			this.bpOp = new ToolStripMenuItem();
			this.bpDeop = new ToolStripMenuItem();
			this.bpVoice = new ToolStripMenuItem();
			this.bpDevoice = new ToolStripMenuItem();
			this.bpKick = new ToolStripMenuItem();
			this.bpKickReason = new ToolStripMenuItem();
			this.bpBan = new ToolStripMenuItem();
			this.bCTCP = new ToolStripMenuItem();
			this.bcVERSION = new ToolStripMenuItem();

			this.tabControl.ContextMenuStrip = this.chanContext;
			this.tabControl.Appearance = TabAppearance.FlatButtons;
			this.tabControl.ContextMenuStrip = this.chanContext;
			this.tabControl.Controls.Add(this.pLog);
			this.tabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
			this.tabControl.Multiline = true;
			this.tabControl.Name = "tabControl";
			this.tabControl.TabIndex = 0;
			this.tabControl.TabStop = false;
			this.tabControl.Dock = DockStyle.Fill;
			this.tabControl.Click += new EventHandler(tabControl_Click);
			this.tabControl.SelectedIndexChanged += new EventHandler(tabControl_SelectedIndexChanged);
			this.tabControl.DrawItem += new DrawItemEventHandler(tabControl_DrawItem);

			this.chanContext.Items.AddRange(new ToolStripItem[] {
			this.bAddFav,
			this.bClose});
			this.chanContext.Name = "chanContext";
			this.chanContext.Size = new System.Drawing.Size(180, 48);
			this.chanContext.Closed += new ToolStripDropDownClosedEventHandler(this.chanContext_Closed);
			this.chanContext.Opening += new System.ComponentModel.CancelEventHandler(this.chanContext_Opening);

			this.bAddFav.Name = "bAddFav";
			this.bAddFav.Size = new System.Drawing.Size(179, 22);
			this.bAddFav.Text = "Add to favorite";
			this.bAddFav.Click += new System.EventHandler(this.bAddFav_Click);

			this.bClose.Name = "bClose";
			this.bClose.ShortcutKeys = ((Keys)((Keys.Control | Keys.F4)));
			this.bClose.Size = new System.Drawing.Size(179, 22);
			this.bClose.Text = "Close";
			this.bClose.Click += new System.EventHandler(this.bClose_Click);

			this.pLog.Controls.Add(this.tLog);
			this.pLog.Name = "pLog";
			this.pLog.Padding = new Padding(3);
			this.pLog.TabIndex = 0;
			this.pLog.UseVisualStyleBackColor = true;
			this.pLog.Controls.Add(tLog);

			this.tLog.BackColor = System.Drawing.Color.FromArgb(224, 255, 192);
			this.tLog.Dock = DockStyle.Fill;
			this.tLog.Location = new System.Drawing.Point(3, 3);
			this.tLog.Name = "tLog";
			this.tLog.ReadOnly = true;
			this.tLog.Size = new System.Drawing.Size(738, 447);
			this.tLog.TabIndex = 0;
			this.tLog.Text = "Minus One IRC client ver " + Util.VER + "\r\nby Yoshi-TS4";
			this.tLog.TabStop = false;
			this.tLog.LinkClicked += new LinkClickedEventHandler(tLog_LinkClicked);
			this.tLog.PreviewKeyDown += new PreviewKeyDownEventHandler(tLine_PreviewKeyDown);

			// 
			// userContext
			// 
			this.userContext.Items.AddRange(new ToolStripItem[] {
			this.bWhois,
			this.bQuery,
			this.bPrivilege,
			this.bCTCP});
			this.userContext.Name = "userContext";
			this.userContext.Size = new System.Drawing.Size(120, 92);
			// 
			// bWhois
			// 
			this.bWhois.Name = "bWhois";
			this.bWhois.Size = new System.Drawing.Size(119, 22);
			this.bWhois.Text = "Whois";
			this.bWhois.Click += new System.EventHandler(this.bWhois_Click);
			// 
			// bQuery
			// 
			this.bQuery.Name = "bQuery";
			this.bQuery.Size = new System.Drawing.Size(119, 22);
			this.bQuery.Text = "Query";
			this.bQuery.Click += new System.EventHandler(this.bQuery_Click);
			// 
			// bPrivilege
			// 
			this.bPrivilege.DropDownItems.AddRange(new ToolStripItem[] {
			this.bpOp,
			this.bpDeop,
			this.bpVoice,
			this.bpDevoice,
			this.bpKick,
			this.bpKickReason,
			this.bpBan});
			this.bPrivilege.Name = "bPrivilege";
			this.bPrivilege.Size = new System.Drawing.Size(119, 22);
			this.bPrivilege.Text = "Privilege";
			// 
			// bpOp
			// 
			this.bpOp.Name = "bpOp";
			this.bpOp.Size = new System.Drawing.Size(153, 22);
			this.bpOp.Text = "Op";
			this.bpOp.Click += new System.EventHandler(this.bpOp_Click);
			// 
			// bpDeop
			// 
			this.bpDeop.Name = "bpDeop";
			this.bpDeop.Size = new System.Drawing.Size(153, 22);
			this.bpDeop.Text = "Deop";
			this.bpDeop.Click += new System.EventHandler(this.bpDeop_Click);
			// 
			// bpVoice
			// 
			this.bpVoice.Name = "bpVoice";
			this.bpVoice.Size = new System.Drawing.Size(153, 22);
			this.bpVoice.Text = "Voice";
			this.bpVoice.Click += new System.EventHandler(this.bpVoice_Click);
			// 
			// bpDevoice
			// 
			this.bpDevoice.Name = "bpDevoice";
			this.bpDevoice.Size = new System.Drawing.Size(153, 22);
			this.bpDevoice.Text = "Devoice";
			this.bpDevoice.Click += new System.EventHandler(this.bpDevoice_Click);
			// 
			// bpKick
			// 
			this.bpKick.Name = "bpKick";
			this.bpKick.Size = new System.Drawing.Size(153, 22);
			this.bpKick.Text = "Kick";
			this.bpKick.Click += new System.EventHandler(this.bpKick_Click);
			// 
			// bpKickReason
			// 
			this.bpKickReason.Name = "bpKickReason";
			this.bpKickReason.Size = new System.Drawing.Size(153, 22);
			this.bpKickReason.Text = "Kick w/ reason";
			this.bpKickReason.Click += new System.EventHandler(this.bpKickReason_Click);
			// 
			// bpBan
			// 
			this.bpBan.Name = "bpBan";
			this.bpBan.Size = new System.Drawing.Size(153, 22);
			this.bpBan.Text = "Ban";
			this.bpBan.Enabled = false;
			this.bpBan.Click += new System.EventHandler(this.bpBan_Click);
			// 
			// bCTCP
			// 
			this.bCTCP.DropDownItems.AddRange(new ToolStripItem[] {
			this.bcVERSION});
			this.bCTCP.Name = "bCTCP";
			this.bCTCP.Size = new System.Drawing.Size(119, 22);
			this.bCTCP.Text = "CTCP";
			// 
			// bcVERSION
			// 
			this.bcVERSION.Name = "bcVERSION";
			this.bcVERSION.Size = new System.Drawing.Size(123, 22);
			this.bcVERSION.Text = "VERSION";
			this.bcVERSION.Click += new System.EventHandler(this.bcVERSION_Click);

			this.Controls.Add(tabControl);

			this.sGroup = sGroup;
			this.encode = sGroup.encode;
			this.host = sGroup.host[0];
			this.pChans = new List<ChannelPage>();
			this.net = new Network(this);
			this.nick = nick;
			this.Text = sGroup.name;
		}

		public ServerGroup sGroup;
		public Encoding encode;
		public string host;
		public List<ChannelPage> pChans;
		private Network net;
		public TabControl tabControl;
		private TabPage pLog;
		private RichTextBox tLog;
		public string nick;
		public int ping = -1;
		public Util.ConnectedState connected = Util.ConnectedState.Disconnected;

		private ToolStripMenuItem bWhois;
		private ToolStripMenuItem bQuery;
		private ToolStripMenuItem bPrivilege;
		private ToolStripMenuItem bpOp;
		private ToolStripMenuItem bpDeop;
		private ToolStripMenuItem bpVoice;
		private ToolStripMenuItem bpDevoice;
		private ToolStripMenuItem bpKick;
		private ToolStripMenuItem bpKickReason;
		private ToolStripMenuItem bpBan;
		private ToolStripMenuItem bCTCP;
		private ToolStripMenuItem bcVERSION;
		public ContextMenuStrip userContext;

		private ToolStripMenuItem bAddFav;
		private ToolStripMenuItem bClose;
		private ContextMenuStrip chanContext;
		public Thread Con;
	}
}
