using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IRCclient
{
	public partial class ChannelPage
	{
		public ChannelPage(ConnectionGroup group, string channel)
			: base(channel)
		{
			this.tInf = new RichTextBox();
			this.tChan = new RichTextBox();

			this.tInf.Dock = DockStyle.Top;
			this.tInf.Text = "";
			this.tInf.ReadOnly = true;
			this.tInf.BackColor = Color.FromArgb(224, 255, 192);
			this.tInf.TabIndex = 1;
			this.tInf.Multiline = false;
			this.tInf.Height = 21;
			this.tInf.LinkClicked += new LinkClickedEventHandler(MainForm.thisfrm.tLog_LinkClicked);
			this.tInf.PreviewKeyDown += new PreviewKeyDownEventHandler(MainForm.thisfrm.tLine_PreviewKeyDown);
			this.tInf.TabStop = false;

			this.splCon = new SplitContainer();
			this.splCon.TabStop = false;
			this.splCon.Dock = DockStyle.Fill;
			this.splCon.FixedPanel = FixedPanel.Panel2;
			this.splCon.SplitterDistance = Width - 160;
			this.splCon.TabIndex = 2;

			this.tChan.Dock = DockStyle.Fill;
			this.tChan.Text = "";
			this.tChan.ReadOnly = true;
			this.tChan.BackColor = Color.White;
			this.tChan.LinkClicked += new LinkClickedEventHandler(MainForm.thisfrm.tLog_LinkClicked);
			this.tChan.PreviewKeyDown += new PreviewKeyDownEventHandler(MainForm.thisfrm.tLine_PreviewKeyDown);
			this.tChan.TabStop = false;

			this.userList = new ListBox();
			this.userList.Dock = DockStyle.Fill;
			this.userList.Sorted = true;
			this.userList.IntegralHeight = false;
			this.userList.ContextMenuStrip = MainForm.thisfrm.userContext;
			this.userList.SelectionMode = SelectionMode.MultiExtended;
			this.userList.PreviewKeyDown += new PreviewKeyDownEventHandler(MainForm.thisfrm.tLine_PreviewKeyDown);
			this.userList.TabStop = false;

			if (channel.StartsWith("#"))
			{
				this.splCon.Panel1.Controls.Add(this.tChan);
				this.splCon.Panel2.Controls.Add(this.userList);
				this.Controls.Add(this.splCon);

				this.Controls.Add(this.tInf);
			}
			else
				this.Controls.Add(this.tChan);
			this.Padding = new Padding(3);

			this.topic = "";
			this.chanmode.init();
			this.tChanged = 0;
			this.selected = false;
			this.group = group;
		}

		private RichTextBox tInf = null;
		private RichTextBox tChan = null;
		private SplitContainer splCon = null;
		private ListBox userList = null;
		private string topic = null;
		struct modes
		{
			public bool[] mode;
			public string key;
			public int limit;
			public void init()
			{
				mode = new bool['z' + 1];
				key = "";
				limit = 0;
			}
			public string getMode()
			{
				string result = "";
				for (int i = 'A'; i <= 'Z'; i++)
					if (mode[i]) result += (char)i;
				for (int i = 'a'; i <= 'z'; i++)
					if (mode[i] && i != 'b') result += (char)i;
				if (mode['k']) result += " " + key;
				if (mode['l']) result += " " + limit;
				return result != "" ? "[+" + result + "]" : "";
			}
		}
		modes chanmode;
		public int tChanged;			//0:no change(black) 1:system(Navy) 2:talk(Blue)
		public bool selected;
		public ConnectionGroup group;
	}
}
