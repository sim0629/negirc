using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace IRCclient
{
	public partial class MainForm : Form
	{
		public static MainForm thisfrm = null;
		private List<ConnectionGroup> connectionGroups = new List<ConnectionGroup>();
		private ConnectionGroup selectedGroup = null;
		private int selectedGroupIndex = -1;
		private ConnectionGroup SelectedGroup
		{
			get
			{
				if (selectedGroup != null) return selectedGroup;
				else
				{
					for (int i = 1; i < connectionGroups.Count; i++)
					{
						if (tabControl.SelectedIndex < tabControl.Controls.IndexOf(connectionGroups[i].pLog))
						{
							selectedGroupIndex = i - 1;
							selectedGroup = connectionGroups[selectedGroupIndex];
							return selectedGroup;
						}
					}
					selectedGroupIndex = connectionGroups.Count - 1;
					selectedGroup = connectionGroups[selectedGroupIndex];
					return selectedGroup;
				}
			}
			set
			{
				selectedGroup = value;
				selectedGroupIndex = -1;
				tabControl.SelectedTab = selectedGroup.pLog;
			}
		}
		private int SelectedGroupIndex
		{
			get
			{
				if (selectedGroupIndex != -1) return selectedGroupIndex;
				for (int i = 1; i < connectionGroups.Count; i++)
				{
					if (tabControl.SelectedIndex < tabControl.Controls.IndexOf(connectionGroups[i].pLog))
					{
						selectedGroupIndex = i - 1;
						selectedGroup = connectionGroups[selectedGroupIndex];
						return selectedGroupIndex;
					}
				}
				selectedGroupIndex = connectionGroups.Count - 1;
				selectedGroup = connectionGroups[selectedGroupIndex];
				return selectedGroupIndex;
			}
		}
		private int SelectedGroupLocation
		{
			get
			{
				return connectionGroups.IndexOf(SelectedGroup);
			}
		}
		private int NextGroupLocation(ConnectionGroup cgroup)
		{
			return connectionGroups.IndexOf(cgroup) + cgroup.channelPages.Count + 1;
		}

		string tsearch = "";

		public MainForm()
		{
			new OptionForm();
			new FavoriteForm();
			InitializeComponent();
			addNewTab();
			this.tNick.Text = OptionForm.thisfrm.defaultNick;

			if (thisfrm == null)
				thisfrm = this;

			this.Text = this.Text.Replace("[ver]", "v" + Util.VER);
#if DEBUG
			this.Text += " [Debug]";
#endif
		}

		public void setPing(int ping)	//sets Ping/Pong Label
		{
			try
			{
				if (ping != -1)
					Util.setText(lPing, "Ping count: " + ping);
				else Util.setText(lPing, "Disconnected");
			}
			catch { }
		}

		private void bConnect_Click(object sender, EventArgs e)
		{
			if (bConnect.Text == "Connect")
			{
				bConnect.Text = "Cancel";
				SelectedGroup.Con = new Thread(new ParameterizedThreadStart(SelectedGroup.ConnectStart));
				SelectedGroup.Con.Start(tNick.Text);
			}
			else if (bConnect.Text == "Cancel")
			{
				try
				{
					if (SelectedGroup.Con.IsAlive) SelectedGroup.Con.Abort();
				}
				catch { }
				SelectedGroup.Disconnect();
			}
			else SelectedGroup.Disconnect();
		}
		private void bOption_Click(object sender, EventArgs e)
		{
			OptionForm.thisfrm.Open(SelectedGroup);
		}
		private void NickChange(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter && tNick.Text != "")
			{
				if (SelectedGroup.Disconnected)
					bConnect_Click(null, null);
				else SelectedGroup.NickChange(tNick.Text);
			}
		}
		private void MainForm_Resize(object sender, EventArgs e)
		{
			if (Width <= 700)
			{
				lPing.Anchor = AnchorStyles.Top | AnchorStyles.Left;
				lPing.Left = 564;
			}
			else
			{
				lPing.Anchor = AnchorStyles.Top | AnchorStyles.Right;
				lPing.Left = Width - 136;
			}
		}
		private void tabControl_Click(object sender, EventArgs e)
		{
			tLineSelect();
		}
		private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			selectedGroup = null;
			selectedGroupIndex = -1;
			SetProperty(SelectedGroup, true);
			SetProperty(SelectedGroup, false);
			tLineSelect();

			foreach (TabPage page in tabControl.TabPages)
				if(page is ChannelPage)
				{
					ChannelPage pChan = (ChannelPage)page;
					pChan.selected = false;
				}

			if (tabControl.SelectedTab is ChannelPage)
			{
				this.ContextMenuStrip = tabControl.ContextMenuStrip = chanContext;
				ChannelPage pChan = (ChannelPage)tabControl.SelectedTab;
				pChan.selected = true;
				pChan.tChanged = 0;
			}
			else this.ContextMenuStrip = tabControl.ContextMenuStrip = groupContext;
		}
		private void tabControl_DrawItem(object sender, DrawItemEventArgs e)
		{
			Graphics g = e.Graphics;
			Brush tbrush = Brushes.Black;
			if (e.Index == tabControl.SelectedIndex)
				g.FillRectangle(Brushes.White, e.Bounds);
			else
			{
				TabPage page = (TabPage)tabControl.Controls[e.Index];
				if(page is ChannelPage)
				{
					ChannelPage pChan = (ChannelPage)page;
					if (pChan.tChanged == 1) tbrush = Brushes.Navy;
					else if (pChan.tChanged == 2) tbrush = Brushes.Blue;
				}
				else tbrush = Brushes.LightSeaGreen;
			}
			g.DrawString(tabControl.Controls[e.Index].Text, e.Font, tbrush, new Point(e.Bounds.X + 1, e.Bounds.Y + 2));
		}
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			foreach (ConnectionGroup pCon in connectionGroups)
				if (pCon.connected != Util.ConnectedState.Disconnected) pCon.Disconnect();
		}

		private void bUpdate_Click(object sender, EventArgs e)
		{
			if (bUpdate.Text == "Update")
			{
				Util.setText(bUpdate, "Checking...");
				Thread updateThread = new Thread(new ThreadStart(UpdateStart));
				updateThread.Start();
			}
		}
		private void UpdateStart()
		{
			string newver = Network.Checkver();
			if (newver == null)
				Util.ShowMessageBox(this, "Cannot check version info", "Version check");
			else if (newver == Util.VER)
				Util.ShowMessageBox(this, "Your client is up-to-date", "Version check");
			else
			{
				string newlog = Network.Checklog();
				DialogResult result = DialogResult.OK;
				if (newlog != "")
					result = Util.ShowMessageBox(this, "Your version:\t" + Util.VER + "\r\nNew version:\t" + newver + "\r\n" + newlog + "\r\n\r\n\r\nDownload new version?", "New version available", MessageBoxButtons.YesNo);
				else result = Util.ShowMessageBox(this, "Your version:\t" + Util.VER + "\r\nNew version:\t" + newver + "\r\n\r\n\r\nDownload new version?", "New version available", MessageBoxButtons.YesNo);
				if (result == DialogResult.Yes)
				{
					if (Network.Download(Util.clientURL + "[-1]IRC-v" + newver + ".exe", "[-1]IRC-v" + newver + ".exe"))
						Util.ShowMessageBox(this, "Download complete!", "Update");
					else Util.ShowMessageBox(this, "Download failed", "Update");
				}
			}
			Util.setText(bUpdate, "Update");
		}

		private void tLine_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter && tLine.Text != "")
			{
				SelectedGroup.tLineEntered(tabControl.SelectedTab.Text, tLine.Text);
				tLine.Clear();
				e.Handled = true;
			}
		}
		public void tLine_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.Modifiers == Keys.Control)
			{
				if (e.KeyCode == Keys.B)
					tLine.SelectedText += '';
				else if (e.KeyCode == Keys.U)
					tLine.SelectedText += '';
				else if (e.KeyCode == Keys.R)
					tLine.SelectedText += '';
				else if (e.KeyCode == Keys.K)
					tLine.SelectedText += '';
				else if (e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9 && tabControl.TabCount > (e.KeyCode - Keys.D1))
					tabControl.SelectedIndex = e.KeyCode - Keys.D1;
				else if (e.KeyCode == Keys.D0 && tabControl.TabCount > 9)
					tabControl.SelectedIndex = 9;
				else if (e.KeyCode == Keys.Tab)
				{
					if (tabControl.SelectedIndex == tabControl.TabCount - 1)
						tabControl.SelectedIndex = 0;
					else tabControl.SelectedIndex++;
				}
			}
			else if (e.Modifiers == (Keys.Control | Keys.Shift))
			{
				if (e.KeyCode == Keys.Tab)
				{
					if (tabControl.SelectedIndex == 0)
						tabControl.SelectedIndex = tabControl.TabCount - 1;
					else tabControl.SelectedIndex--;
				}
				else if (e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9 && tabControl.TabCount > (e.KeyCode - Keys.D1))
					tabControl.SelectedIndex = e.KeyCode - Keys.D1 + 10;
				else if (e.KeyCode == Keys.D0 && tabControl.TabCount > 19)
					tabControl.SelectedIndex = 19;
			}
			else if (e.Modifiers == Keys.Alt)
			{
				if (e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9 && tabControl.TabCount > (e.KeyCode - Keys.D1))
					tabControl.SelectedIndex = e.KeyCode - Keys.D1;
				else if (e.KeyCode == Keys.D0 && tabControl.TabCount > 9)
					tabControl.SelectedIndex = 9;
				else if (e.KeyCode == Keys.Left && tabControl.SelectedIndex > 0)
					tabControl.SelectedIndex--;
				else if (e.KeyCode == Keys.Right && tabControl.SelectedIndex < tabControl.TabCount - 1)
					tabControl.SelectedIndex++;
			}
			else if (e.Modifiers == (Keys.Alt | Keys.Shift))
			{
				if (e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9 && tabControl.TabCount > (e.KeyCode - Keys.D1))
					tabControl.SelectedIndex = e.KeyCode - Keys.D1 + 10;
				else if (e.KeyCode == Keys.D0 && tabControl.TabCount > 19)
					tabControl.SelectedIndex = 19;
				else if (e.KeyCode == Keys.Left && tabControl.SelectedIndex > 0)
					tabControl.SelectedIndex--;
				else if (e.KeyCode == Keys.Right && tabControl.SelectedIndex < tabControl.TabCount - 1)
					tabControl.SelectedIndex++;
			}
			else if (e.Control && e.Alt)
			{
				if (e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9)
					tLine.SelectedText += (char)(e.KeyCode - Keys.D0 + (e.Shift ? 10 : 0));
				else if (e.KeyCode == Keys.D0)
					tLine.SelectedText += (char)(e.Shift ? 20 : 10);
			}
			else if (e.KeyCode == Keys.Tab)
			{
				if (tabControl.SelectedTab.Text.StartsWith("#") && tabControl.SelectedTab is ChannelPage)
				{
					ChannelPage pChan = (ChannelPage)tabControl.SelectedTab;
					if (tsearch == "")
						tsearch = tLine.Text;

					string result = pChan.nextUser(tsearch, tLine.Text);
					if (result != null)
						tLine.Text = result;
					tLine.Select(tLine.TextLength, 0);
				}
			}
			else tsearch = "";
		}

		private void groupContext_Opening(object sender, CancelEventArgs e)
		{
			bCloseServer.Enabled = connectionGroups.Count > 1;
		}

		public void tLog_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start(e.LinkText);
			}
			catch { }
		}

		private void nNewServer_Click(object sender, EventArgs e)
		{
			addNewTab();
		}
		private void bCloseServer_Click(object sender, EventArgs e)
		{
			try
			{
				if (SelectedGroup.connected != Util.ConnectedState.Disconnected)
					SelectedGroup.Disconnect();
				int begin = tabControl.Controls.IndexOf(SelectedGroup.pLog);
				int num = NextGroupLocation(SelectedGroup) - begin;
				connectionGroups.Remove(SelectedGroup);
				for (int i = 0; i < num; i++)
					Util.removeTab(tabControl, begin);
				tabControl.SelectedIndex = begin;
			}
			catch { }
		}

		private void addNewTab()
		{
			ServerGroup tba = OptionForm.thisfrm.Groups[0];
			foreach (ServerGroup sgroup in OptionForm.thisfrm.Groups)
			{
				bool pos = true;
				foreach (ConnectionGroup cgroup in connectionGroups)
				{
					if (cgroup.sGroup == sgroup)
					{
						pos = false;
						break;
					}
				}
				if (pos)
				{
					tba = sgroup;
					break;
				}
			}
			ConnectionGroup nGroup = new ConnectionGroup(tba, OptionForm.thisfrm.defaultNick);
			tabControl.Controls.Add(nGroup.pLog);
			connectionGroups.Add(nGroup);
			SelectedGroup = nGroup;
		}

		public void tLineSelect()
		{
			tLine.Select();
			tLine.Focus();
		}

		public bool isSelected(ConnectionGroup cp)
		{
			return SelectedGroup == cp;
		}

		private void tNick_TextChanged(object sender, EventArgs e)
		{
			if (SelectedGroup.connected == Util.ConnectedState.Disconnected)
				SelectedGroup.nick = tNick.Text;
		}

		public void SetProperty(ConnectionGroup pCon, bool bnick)
		{
			if (pCon == SelectedGroup)
			{
				if (bnick)
					Util.setText(tNick, pCon.nick);
				else
				{
					Util.setText(bConnect, Util.ConnectedString(pCon.connected));
					setPing(pCon.ping);
				}
			}
		}

		private void cScrollEnd_CheckedChanged(object sender, EventArgs e)
		{
			tLineSelect();
		}
		private void bClose_Click(object sender, EventArgs e)
		{
			if (tabControl.SelectedTab is ChannelPage)
			{
				string temp = tabControl.SelectedTab.Text;
				if (temp.StartsWith("#"))
					SelectedGroup.net.SendData("PART " + temp, false);
				else
					SelectedGroup.part((ChannelPage)tabControl.SelectedTab);
			}
		}

		private void chanContext_Opening(object sender, CancelEventArgs e)
		{
			if (tabControl.SelectedIndex > SelectedGroupLocation)
				bAddFav.Enabled = tabControl.SelectedTab.Text.StartsWith("#");
			else bAddFav.Enabled = false;

			if (tabControl.SelectedIndex > SelectedGroupLocation + 1)
				bMoveLeft.Enabled = bClose.Enabled = true;
			else bMoveLeft.Enabled = bCloseServer.Enabled = false;

			if (tabControl.SelectedIndex < NextGroupLocation(SelectedGroup) - 1)
				bMoveRight.Enabled = true;
			else bMoveRight.Enabled = false;
		}
		private void chanContext_Closed(object sender, ToolStripDropDownClosedEventArgs e)
		{
			bAddFav.Enabled = bMoveLeft.Enabled = bMoveRight.Enabled = bClose.Enabled = true;
		}
		private void bAddFav_Click(object sender, EventArgs e)
		{
			string chan = tabControl.SelectedTab.Text;
			if (chan.StartsWith("#") && !SelectedGroup.sGroup.favorites.Contains(chan))
			{
				SelectedGroup.sGroup.favorites.Add(chan);
				OptionForm.thisfrm.saveAll();
			}
		}
		private void bMoveLeft_Click(object sender, EventArgs e)
		{
			TabControl.TabPageCollection pcol = tabControl.TabPages;
			int index = tabControl.SelectedIndex;
			TabPage pChan = tabControl.SelectedTab;
			if (index > SelectedGroupLocation + 1)
			{
				pcol.RemoveAt(index);
				pcol.Insert(index - 1, pChan);
			}
			Util.selectPage(tabControl, pChan);
		}
		private void bMoveRight_Click(object sender, EventArgs e)
		{
			TabControl.TabPageCollection pcol = tabControl.TabPages;
			int index = tabControl.SelectedIndex;
			TabPage pChan = tabControl.SelectedTab;
			if (index < NextGroupLocation(SelectedGroup) - 1)
			{
				pcol.RemoveAt(index);
				pcol.Insert(index + 1, pChan);
			}
			Util.selectPage(tabControl, pChan);
		}

		private void bWhois_Click(object sender, EventArgs e)
		{
			ChannelPage pChan = (ChannelPage)tabControl.SelectedTab;
			if (pChan.selectedUsers().Length == 0) return;
			SelectedGroup.net.SendData("WHOIS " + pChan.selectedUser(), false);
		}
		private void bQuery_Click(object sender, EventArgs e)
		{
			ChannelPage pChan = (ChannelPage)tabControl.SelectedTab;
			if (pChan.selectedUsers().Length == 0) return;
			SelectedGroup.join(pChan.selectedUser());
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
						SelectedGroup.net.SendData(sending, false);
						sending = "MODE " + pChan.Text + " +";
						witch = sending.Length;
					}
					sending = sending.Insert(witch, "o") + " " + selectedUsers[i];
				}
				SelectedGroup.net.SendData(sending, false);
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
						SelectedGroup.net.SendData(sending, false);
						sending = "MODE " + pChan.Text + " -";
						witch = sending.Length;
					}
					sending = sending.Insert(witch, "o") + " " + selectedUsers[i];
				}
				SelectedGroup.net.SendData(sending, false);
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
						SelectedGroup.net.SendData(sending, false);
						sending = "MODE " + pChan.Text + " +";
						witch = sending.Length;
					}
					sending = sending.Insert(witch, "v") + " " + selectedUsers[i];
				}
				SelectedGroup.net.SendData(sending, false);
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
						SelectedGroup.net.SendData(sending, false);
						sending = "MODE " + pChan.Text + " -";
						witch = sending.Length;
					}
					sending = sending.Insert(witch, "v") + " " + selectedUsers[i];
				}
				SelectedGroup.net.SendData(sending, false);
			}
		}
		private void bpKick_Click(object sender, EventArgs e)
		{
			ChannelPage pChan = (ChannelPage)tabControl.SelectedTab;
			if (pChan.selectedUsers().Length == 0) return;
			SelectedGroup.net.SendData("KICK " + pChan.Text + " " + pChan.selectedUser(), false);
		}
		private void bpKickReason_Click(object sender, EventArgs e)
		{
			ChannelPage pChan = (ChannelPage)tabControl.SelectedTab;
			if (pChan.selectedUsers().Length == 0) return;
			string reason = Util.GetFromDialog("Reason?", "Kick", pChan.selectedUser());
			if (reason != "")
				SelectedGroup.net.SendData("KICK " + pChan.Text + " " + pChan.selectedUser() + " :" + reason, false);
		}
		private void bpBan_Click(object sender, EventArgs e)
		{
		}
		private void bcVERSION_Click(object sender, EventArgs e)
		{
			ChannelPage pChan = (ChannelPage)tabControl.SelectedTab;
			if (pChan.selectedUsers().Length == 0) return;
			SelectedGroup.net.SendData("PRIVMSG " + pChan.selectedUser() + " :VERSION", false);
		}

		public ChannelPage FindChan(ConnectionGroup connectionGroup, string channel)
		{
			for (int i = tabControl.Controls.IndexOf(connectionGroup.pLog) + 1; ; i++)
			{
				if (tabControl.Controls[i] is ChannelPage)
				{
					if (tabControl.Controls[i].Text == channel)
						return (ChannelPage)tabControl.Controls[i];
				}
				else return null;
			}
		}

		public void InsertChan(ConnectionGroup cGroup, ChannelPage pChan)
		{
			Util.insertTab(tabControl, pChan, NextGroupLocation(cGroup) - 1);
		}
	}
}
