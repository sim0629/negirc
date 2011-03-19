using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;

namespace IRCclient
{
	public class ConnectionGroup
	{
		public ServerGroup sGroup;
		public Encoding encode;
		public string host;
		public TabPage pLog;
		private RichTextBox tLog;
		public List<ChannelPage> channelPages;
		public string nick;
		public int ping = -1;
		public Util.ConnectedState connected = Util.ConnectedState.Disconnected;
		public bool Disconnected
		{
			get
			{
				return connected == Util.ConnectedState.Disconnected;
			}
		}
		private string text;
		public string Text
		{
			get
			{
				return text;
			}
			set
			{
				pLog.Text = text = value;
			}
		}

		public Network net;
		public Thread Con;

		public ConnectionGroup(ServerGroup sGroup, string nick)
		{
			this.pLog = new TabPage("Log\t");
			this.tLog = new RichTextBox();

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
			this.tLog.LanguageOption = RichTextBoxLanguageOptions.DualFont;

			this.sGroup = sGroup;
			this.encode = sGroup.encode;
			this.host = sGroup.host[0];
			this.channelPages = new List<ChannelPage>();
			this.net = new Network(this);
			this.nick = nick;
			this.Text = sGroup.name;
		}

		public void addLog(string msg, int level = 0)
		{
			Util.addText(this.tLog, msg, Util.levelColor(level));
		}

		public void sendID(string nick)
		{
			net.SendData("PASS pass", true);
			net.SendData("NICK " + nick, true);
			net.SendData("USER " + OptionForm.thisfrm.usermsg, true);
		}
		public void ConnectStart(object nick)					 //Start the connect
		{
			setConnectedState(Util.ConnectedState.Connecting);
			if (!net.Connect(host, (string)nick))
			{
				addLog("Cannot connect to server");
				setConnectedState(Util.ConnectedState.Disconnected);
			}
			else
			{
				setConnectedState(Util.ConnectedState.Connected);
				sendID((string)nick);
			}
		}

		private void setConnectedState(Util.ConnectedState connectedState)
		{
			this.connected = connectedState;
			if (connected == Util.ConnectedState.Connected && ping < 0) ping = 0;
			MainForm.thisfrm.SetProperty(this, false);
		}

		public ChannelPage join(string channel, bool selecting = true)		//opens a channel window
		{
			ChannelPage pChan = null;
			foreach (ChannelPage page in channelPages)
				if (page.Text == channel)
				{
					pChan = page;
					break;
				}
			if (pChan == null)
			{
				pChan = new ChannelPage(this, channel);
				channelPages.Add(pChan);
				MainForm.thisfrm.InsertChan(this, pChan);
			}
			if (selecting) Util.selectPage(MainForm.thisfrm.tabControl, pChan);
			return pChan;
		}
		public void part(string channel)		//closes a channel window
		{
			foreach (ChannelPage page in channelPages)
				if (page.Text == channel)
				{
					part(page);
					break;
				}
		}
		public void part(ChannelPage page)
		{
			Util.removeTab(MainForm.thisfrm.tabControl, page);
			channelPages.Remove(page);
		}

		public void AddNick(string nick, string channel)
		{
			foreach (ChannelPage page in channelPages)
				if (page.Text == channel)
					page.AddNick(nick);
		}
		public void RemoveNick(string nick, string channel)
		{
			foreach (ChannelPage page in channelPages)
				if (page.Text == channel)
					page.RemoveNick(nick);
		}
		public void QuitNick(string nick, string qmsg)
		{
			foreach (ChannelPage page in channelPages)
				if (!page.Text.EndsWith("\t"))
				{
					if (page.containsNick(nick) != 0)
					{
						page.RemoveNick(nick);
						if (qmsg != null)
							AddChat("[QUIT] " + nick + " (" + qmsg + ")", page, 2);
						else AddChat("[QUIT] " + nick, page, 2);
					}
				}
		}
		public void ChangeNick(string nick, string newnick)
		{
			foreach (ChannelPage page in channelPages)
				if (!page.Text.EndsWith("\t"))
				{
					if (page.ChangeNick(nick, newnick) > 0)
						AddChat("[NICK] " + nick + " -> " + newnick, page, 3);
				}
		}
		public void SetNick(string nick)
		{
			this.nick = nick;
			MainForm.thisfrm.SetProperty(this, true);
		}
		public void SetTopic(string topic, string channel, string nick)
		{
			foreach (ChannelPage page in channelPages)
				if (page.Text == channel)
				{
					page.SetTopic(topic);
					if (nick != null) AddChat("* " + nick + " sets topic: " + topic, page, 3);
				}
		}
		public void AddChat(string msg, string channel, int level = 0)  //level: 0-chat 1-3:system
		{
			foreach (ChannelPage page in channelPages)
				if (page.Text == channel)
				{
					AddChat(msg, page, level);
					return;
				}
			if (!channel.StartsWith("#"))
			{
				ChannelPage nChan = join(channel, false);
				nChan.AddChat("[" + Util.timestamp() + "] " + msg, level);
			}
		}
		public void AddChat(string msg, ChannelPage pChan, int level = 0)
		{
			pChan.AddChat("[" + Util.timestamp() + "] " + msg, level);
		}
		public void SetMode(string mode, string channel, string nick)
		{
			foreach (ChannelPage page in channelPages)
				if (page.Text == channel)
				{
					page.SetMode(mode);
					if (nick != null) AddChat("* " + nick + " sets mode: " + mode, page, 3);
				}
		}

		public string GetQmsg()
		{
			return OptionForm.thisfrm.qmsg;
		}
		public string GetVmsg()
		{
			return OptionForm.thisfrm.vermsg;
		}
		public Encoding GetEncoding()
		{
			return this.encode;
		}

		public void tLog_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			MainForm.thisfrm.tLog_LinkClicked(sender, e);
			MainForm.thisfrm.tLineSelect();
		}
		private void tLine_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			MainForm.thisfrm.tLine_PreviewKeyDown(sender, e);
		}

		public void setdis()
		{
			this.ping = -1;
			setConnectedState(Util.ConnectedState.Disconnected);
			for (int i = 0; i < channelPages.Count; i++)	 //Removes all TabPages except initial pages
				Util.removeTab(MainForm.thisfrm.tabControl, channelPages[i]);
			channelPages.Clear();
		}

		public void setPing(int ping)
		{
			this.ping = ping;
			MainForm.thisfrm.SetProperty(this, false);
		}

		public void Disconnect()
		{
			net.Disconnect();
		}

		public void tLineEntered(string channel, string line)
		{
			string result;
			if (line.StartsWith("/"))
			{
				result = line.Substring(1);
				if (result == "uptime" && !channel.EndsWith("\t"))
				{
					int tick = Environment.TickCount / 1000;
					net.SendData("PRIVMSG " + channel + " :Uptime: " + tick / 86400 + "days " + (tick % 86400) / 3600 + "hours " + (tick % 3600) / 60 + "mins " + tick % 60 + "secs", false);
				}
				else
					net.SendData(result, true);
			}
			else
			{
				if (channel != null)
					net.SendData("PRIVMSG " + channel + " :" + line, false);
				else net.SendData(line, true);
			}
		}

		public void NickChange(string nick)
		{
			if (this.nick != nick)
				net.SendData("NICK " + nick, true);
		}
	}
}
