using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;

namespace IRCclient
{
	public partial class ConnectionPage : TabPage
	{
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
			foreach (TabPage page in tabControl.Controls)
				if (page.Text == channel)
					pChan = (ChannelPage)page;
			if (pChan == null)
			{
				pChan = new ChannelPage(channel, this);
				Util.addTab(tabControl, pChan);
			}
			if (selecting) Util.selectPage(tabControl, pChan);
			return pChan;
		}
		public void part(string channel)		//closes a channel window
		{
			foreach (TabPage page in tabControl.Controls)
				if (page.Text == channel)
					Util.removeTab(tabControl, page);
		}

		public void AddNick(string nick, string channel)
		{
			foreach (TabPage page in tabControl.Controls)
				if (page.Text == channel)
					((ChannelPage)page).AddNick(nick);
		}
		public void RemoveNick(string nick, string channel)
		{
			foreach (TabPage page in tabControl.Controls)
				if (page.Text == channel)
					((ChannelPage)page).RemoveNick(nick);
		}
		public void QuitNick(string nick, string qmsg)
		{
			foreach (TabPage page in tabControl.Controls)
				if (!page.Text.EndsWith("\t"))
				{
					ChannelPage pChan = (ChannelPage)page;
					if (pChan.containsNick(nick) != 0)
					{
						pChan.RemoveNick(nick);
						if (qmsg != null)
							AddChat("[QUIT] " + nick + " (" + qmsg + ")", pChan.Text, 2);
						else AddChat("[QUIT] " + nick, pChan.Text, 2);
					}
				}
		}
		public void ChangeNick(string nick, string newnick)
		{
			foreach (TabPage page in tabControl.Controls)
				if (!page.Text.EndsWith("\t"))
				{
					ChannelPage pChan = (ChannelPage)page;
					if (pChan.ChangeNick(nick, newnick) > 0)
						AddChat("[NICK] " + nick + " -> " + newnick, pChan.Text, 3);
				}
		}
		public void SetNick(string nick)
		{
			this.nick = nick;
			MainForm.thisfrm.SetProperty(this, true);
		}
		public void SetTopic(string topic, string channel, string nick)
		{
			foreach (TabPage page in tabControl.Controls)
				if (page.Text == channel)
				{
					ChannelPage pChan = (ChannelPage)page;
					pChan.SetTopic(topic);
					if (nick != null) AddChat("* " + nick + " sets topic: " + topic, channel, 3);
				}
		}
		public void AddChat(string msg, string channel, int level = 0)  //level: 0-chat 1-3:system
		{
			foreach (TabPage page in tabControl.Controls)
				if (page.Text == channel)
				{
					ChannelPage pChan = (ChannelPage)page;
					pChan.AddChat("[" + Util.timestamp() + "] " + msg, level);
					return;
				}
			if (!channel.StartsWith("#"))
			{
				ChannelPage nChan = join(channel, false);
				nChan.AddChat("[" + Util.timestamp() + "] " + msg, level);
			}
		}
		public void SetMode(string mode, string channel, string nick)
		{
			foreach (TabPage page in tabControl.Controls)
				if (page.Text == channel)
				{
					ChannelPage pChan = (ChannelPage)page;
					pChan.SetMode(mode);
					if (nick != null) AddChat("* " + nick + " sets mode: " + mode, channel, 3);
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
			for (; tabControl.Controls.Count > 1; )	 //Removes all TabPages except initial pages
				tabControl.Controls.RemoveAt(1);
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

		public void tLineEntered(string line)
		{
			string openchan = tabControl.SelectedTab.Text;
			string result;
			if (line.StartsWith("/"))
			{
				result = line.Substring(1);
				if (result == "uptime" && !openchan.EndsWith("\t"))
				{
					int tick = Environment.TickCount / 1000;
					net.SendData("PRIVMSG " + openchan + " :Uptime: " + tick / 86400 + "days " + (tick % 86400) / 3600 + "hours " + (tick % 3600) / 60 + "mins " + tick % 60 + "secs", false);
				}
				else
					net.SendData(result, true);
			}
			else
			{
				if (!openchan.EndsWith("\t"))
					net.SendData("PRIVMSG " + openchan + " :" + line, false);
			}
		}

		private void bWhois_Click(object sender, EventArgs e)
		{
			ChannelPage pChan = (ChannelPage)tabControl.SelectedTab;
			if (pChan.selectedUsers().Length == 0) return;
			net.SendData("WHOIS " + pChan.selectedUser(), false);
		}
		private void bQuery_Click(object sender, EventArgs e)
		{
			ChannelPage pChan = (ChannelPage)tabControl.SelectedTab;
			if (pChan.selectedUsers().Length == 0) return;
			join(pChan.selectedUser());
		}
		private void bpOp_Click(object sender, EventArgs e)
		{
			ChannelPage pChan = (ChannelPage)tabControl.SelectedTab;
			string[] selectedUsers = pChan.selectedUsers();
			int count = selectedUsers.Length;
			if (count > 0)
			{
				int witch = 0;
				string sending = "";
				for (int i = 0; i < count; i++)
				{
					if (i % 4 == 0)
					{
						net.SendData(sending, false);
						sending = "MODE " + pChan.Text + " +";
						witch = sending.Length;
					}
					sending = sending.Insert(witch, "o") + " " + selectedUsers[i];
				}
				net.SendData(sending, false);
			}
		}
		private void bpDeop_Click(object sender, EventArgs e)
		{
			ChannelPage pChan = (ChannelPage)tabControl.SelectedTab;
			string[] selectedUsers = pChan.selectedUsers();
			int count = selectedUsers.Length;
			if (count > 0)
			{
				int witch = 0;
				string sending = "";
				for (int i = 0; i < count; i++)
				{
					if (i % 4 == 0)
					{
						net.SendData(sending, false);
						sending = "MODE " + pChan.Text + " -";
						witch = sending.Length;
					}
					sending = sending.Insert(witch, "o") + " " + selectedUsers[i];
				}
				net.SendData(sending, false);
			}
		}
		private void bpVoice_Click(object sender, EventArgs e)
		{
			ChannelPage pChan = (ChannelPage)tabControl.SelectedTab;
			string[] selectedUsers = pChan.selectedUsers();
			int count = selectedUsers.Length;
			if (count > 0)
			{
				int witch = 0;
				string sending = "";
				for (int i = 0; i < count; i++)
				{
					if (i % 4 == 0)
					{
						net.SendData(sending, false);
						sending = "MODE " + pChan.Text + " +";
						witch = sending.Length;
					}
					sending = sending.Insert(witch, "v") + " " + selectedUsers[i];
				}
				net.SendData(sending, false);
			}
		}
		private void bpDevoice_Click(object sender, EventArgs e)
		{
			ChannelPage pChan = (ChannelPage)tabControl.SelectedTab;
			string[] selectedUsers = pChan.selectedUsers();
			int count = selectedUsers.Length;
			if (count > 0)
			{
				int witch = 0;
				string sending = "";
				for (int i = 0; i < count; i++)
				{
					if (i % 4 == 0)
					{
						net.SendData(sending, false);
						sending = "MODE " + pChan.Text + " -";
						witch = sending.Length;
					}
					sending = sending.Insert(witch, "v") + " " + selectedUsers[i];
				}
				net.SendData(sending, false);
			}
		}
		private void bpKick_Click(object sender, EventArgs e)
		{
			ChannelPage pChan = (ChannelPage)tabControl.SelectedTab;
			if (pChan.selectedUsers().Length == 0) return;
			net.SendData("KICK " + pChan.Text + " " + pChan.selectedUser(), false);
		}
		private void bpKickReason_Click(object sender, EventArgs e)
		{
			ChannelPage pChan = (ChannelPage)tabControl.SelectedTab;
			if (pChan.selectedUsers().Length == 0) return;
			string reason = Util.GetFromDialog("Reason?", "Kick", pChan.selectedUser());
			if (reason != "")
				net.SendData("KICK " + pChan.Text + " " + pChan.selectedUser() + " :" + reason, false);
		}
		private void bpBan_Click(object sender, EventArgs e)
		{
		}
		private void bcVERSION_Click(object sender, EventArgs e)
		{
			ChannelPage pChan = (ChannelPage)tabControl.SelectedTab;
			if (pChan.selectedUsers().Length == 0) return;
			net.SendData("PRIVMSG " + pChan.selectedUser() + " :VERSION", false);
		}

		void tabControl_Click(object sender, EventArgs e)
		{
			MainForm.thisfrm.tLineSelect();
		}
		void tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			for (int i = 1; i < tabControl.TabCount; i++)
			{
				ChannelPage pChan = (ChannelPage)tabControl.TabPages[i];
				if (i != tabControl.SelectedIndex) pChan.selected = false;
				else
				{
					pChan.selected = true;
					pChan.tChanged = 0;
				}
			}
			MainForm.thisfrm.tLineSelect();
		}

		public void NickChange(string nick)
		{
			if (this.nick != nick)
				net.SendData("NICK " + nick, true);
		}

		private void chanContext_Opening(object sender, CancelEventArgs e)
		{
			if (tabControl.SelectedIndex >= 1)
			{
				bAddFav.Enabled = tabControl.SelectedTab.Text.StartsWith("#");
				bClose.Enabled = true;
			}
			else bAddFav.Enabled = bClose.Enabled = false;
		}
		private void chanContext_Closed(object sender, ToolStripDropDownClosedEventArgs e)
		{
			bAddFav.Enabled = bClose.Enabled = true;
		}
		private void bAddFav_Click(object sender, EventArgs e)
		{
			string chan = tabControl.SelectedTab.Text;
			if (chan.StartsWith("#") && !sGroup.favorites.Contains(chan))
			{
				sGroup.favorites.Add(chan);
				OptionForm.thisfrm.saveAll();
			}
		}
		public void bClose_Click(object sender, EventArgs e)
		{
			string temp = tabControl.SelectedTab.Text;
			if (!temp.EndsWith("\t"))
			{
				if (temp.StartsWith("#"))
					net.SendData("PART " + temp, false);
				else
					part(temp);
			}
		}

		void tabControl_DrawItem(object sender, DrawItemEventArgs e)
		{
			Graphics g = e.Graphics;
			Brush tbrush = Brushes.Black;
			if (e.Index == tabControl.SelectedIndex)
				g.FillRectangle(Brushes.White, e.Bounds);
			else
			{
				if (e.Index == 0) tbrush = Brushes.Green;
				else
				{
					ChannelPage pChan = (ChannelPage)tabControl.Controls[e.Index];
					if (pChan.tChanged == 1) tbrush = Brushes.Navy;
					else if (pChan.tChanged == 2) tbrush = Brushes.Blue;
				}
			}
			g.DrawString(tabControl.Controls[e.Index].Text, e.Font, tbrush, new Point(e.Bounds.X + 1, e.Bounds.Y + 2));
		}
	}
}
