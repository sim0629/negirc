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
		private ConnectionGroup SelectedGroup	// SelectedGroup
		{
			get
			{
				TabPage sel = tabControl.SelectedTab;
				if (sel is ChannelPage) return ((ChannelPage)sel).group;
				else
				{
					for (int i = 0; i < connectionGroups.Count; i++)
					{
						if (sel == connectionGroups[i].pLog)
							return connectionGroups[i];
					}
					return null;
				}
			}
			set
			{
				tabControl.SelectedTab = value.pLog;
			}
		}
		private int SelectedGroupIndex	// Index in connectionGroups
		{
			get
			{
				TabPage sel = tabControl.SelectedTab;
				if (sel is ChannelPage) return connectionGroups.IndexOf(((ChannelPage)sel).group);
				else
				{
					for (int i = 0; i < connectionGroups.Count; i++)
					{
						if (sel == connectionGroups[i].pLog)
							return i;
					}
					return -1;
				}
			}
			set
			{
				tabControl.SelectedTab = connectionGroups[value].pLog;
			}
		}
		private int GroupLocation(ConnectionGroup cgroup)
		{
			return tabControl.TabPages.IndexOf(cgroup.pLog);
		}
		private int GroupEndLocation(ConnectionGroup cgroup)
		{
			return tabControl.TabPages.IndexOf(cgroup.pLog) + cgroup.channelPages.Count;
		}

		string tsearch = null;

		public MainForm()
		{
			this.Font = (new OptionForm()).fonttemp;
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
			SetProperty(SelectedGroup, true);
			SetProperty(SelectedGroup, false);
			tLineSelect();

			foreach (TabPage page in tabControl.TabPages)
			{
				ChannelPage pChan = page as ChannelPage;
				if (pChan != null)
					pChan.selected = false;
			}

			ChannelPage sChan = tabControl.SelectedTab as ChannelPage;
			if (sChan != null)
			{
				this.ContextMenuStrip = tabControl.ContextMenuStrip = chanContext;
				sChan.selected = true;
				sChan.tChanged = 0;
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
				ChannelPage pChan = tabControl.Controls[e.Index] as ChannelPage;
				if(pChan != null)
				{
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
				if (tabControl.SelectedTab is ChannelPage)
					SelectedGroup.tLineEntered(tabControl.SelectedTab.Text, tLine.Text);
				else SelectedGroup.tLineEntered(null, tLine.Text);
				tLine.Clear();
				e.Handled = true;
			}
		}

		public bool CtrlOrAlt_Key(Keys key)
		{
			if (key >= Keys.D1 && key <= Keys.D9)
			{
				tabControl.SelectedIndex = key - Keys.D1;
				return true;
			}
			else if (key == Keys.D0)
			{
				tabControl.SelectedIndex = 9;
				return true;
			}
			else if (key == Keys.A)
			{
				foreach (TabPage page in tabControl.TabPages)
				{
					ChannelPage pChan = page as ChannelPage;
					if (pChan != null && pChan.tChanged == 2)
					{
						tabControl.SelectedTab = pChan;
						return true;
					}
				}
				foreach (TabPage page in tabControl.TabPages)
				{
					ChannelPage pChan = page as ChannelPage;
					if (pChan != null && pChan.tChanged == 1)
					{
						tabControl.SelectedTab = pChan;
						return true;
					}
				}
				return true;
			}
			else return false;
		}
		public void tLine_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.Modifiers == Keys.Control)
			{
				if (!CtrlOrAlt_Key(e.KeyCode))
					switch (e.KeyCode)
					{
						case Keys.B: tLine.SelectedText += ''; break;
						case Keys.U: tLine.SelectedText += ''; break;
						case Keys.R: tLine.SelectedText += ''; break;
						case Keys.K: tLine.SelectedText += ''; break;
						case Keys.D0: tabControl.SelectedIndex = 9; break;
						case Keys.Tab:
							if (tabControl.SelectedIndex == tabControl.TabCount - 1)
								tabControl.SelectedIndex = 0;
							else tabControl.SelectedIndex++;
							break;
					}
			}
			else if (e.Modifiers == (Keys.Control | Keys.Shift))
			{
				if (e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9)
					tabControl.SelectedIndex = e.KeyCode - Keys.D1 + 20;
				else switch (e.KeyCode)
					{
						case Keys.Tab:
							if (tabControl.SelectedIndex == 0)
								tabControl.SelectedIndex = tabControl.TabCount - 1;
							else tabControl.SelectedIndex--;
							break;
						case Keys.D0: tabControl.SelectedIndex = 19; break;
					}
			}
			else if (e.Modifiers == Keys.Alt)
			{
				if (!CtrlOrAlt_Key(e.KeyCode))
					switch (e.KeyCode)
					{
						case Keys.Left:
							if (tabControl.SelectedIndex > 0)
								tabControl.SelectedIndex--;
							break;
						case Keys.Right:
							if (tabControl.SelectedIndex < tabControl.TabCount - 1)
								tabControl.SelectedIndex++;
							break;
						case Keys.Q: tabControl.SelectedIndex = 10; break;
						case Keys.W: tabControl.SelectedIndex = 11; break;
						case Keys.E: tabControl.SelectedIndex = 12; break;
						case Keys.R: tabControl.SelectedIndex = 13; break;
						case Keys.T: tabControl.SelectedIndex = 14; break;
						case Keys.Y: tabControl.SelectedIndex = 15; break;
						case Keys.U: tabControl.SelectedIndex = 16; break;
						case Keys.I: tabControl.SelectedIndex = 17; break;
						case Keys.O: tabControl.SelectedIndex = 18; break;
						case Keys.P: tabControl.SelectedIndex = 19; break;
					}
			}
			else if (e.Modifiers == (Keys.Alt | Keys.Shift))
			{
				if (e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9)
					tabControl.SelectedIndex = e.KeyCode - Keys.D1 + 10;
				else switch (e.KeyCode)
					{
						case Keys.D0: tabControl.SelectedIndex = 19; break;
						case Keys.Left:
							if (tabControl.SelectedIndex > 0)
								tabControl.SelectedIndex--;
							break;
						case Keys.Right:
							if (tabControl.SelectedIndex < tabControl.TabCount - 1)
								tabControl.SelectedIndex++;
							break;
						case Keys.Q: tabControl.SelectedIndex = 20; break;
						case Keys.W: tabControl.SelectedIndex = 21; break;
						case Keys.E: tabControl.SelectedIndex = 22; break;
						case Keys.R: tabControl.SelectedIndex = 23; break;
						case Keys.T: tabControl.SelectedIndex = 24; break;
						case Keys.Y: tabControl.SelectedIndex = 25; break;
						case Keys.U: tabControl.SelectedIndex = 26; break;
						case Keys.I: tabControl.SelectedIndex = 27; break;
						case Keys.O: tabControl.SelectedIndex = 28; break;
						case Keys.P: tabControl.SelectedIndex = 29; break;
					}
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
				ChannelPage pChan = tabControl.SelectedTab as ChannelPage;
				if (pChan != null && tabControl.SelectedTab.Text.StartsWith("#"))
				{
					if (tsearch == null)
						tsearch = tLine.Text;

					string result = pChan.nextUser(tsearch, tLine.Text);
					if (result != null)
						tLine.Text = result;
					tLine.Select(tLine.TextLength, 0);
				}
			}
			else tsearch = null;
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
			tLineSelect();
		}

		private void addNewTab(object sender = null, EventArgs e = null)
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
		private void bCloseServer_Click(object sender, EventArgs e)
		{
			try
			{
				ConnectionGroup sel = SelectedGroup;
				if (!sel.Disconnected)
					sel.Disconnect();
				int begin = tabControl.Controls.IndexOf(SelectedGroup.pLog);
				int num = sel.channelPages.Count + 1;
				connectionGroups.Remove(sel);
				for (int i = 0; i < num; i++)
					Util.removeTab(tabControl, begin);
				tabControl.SelectedIndex = begin;
			}
			catch { }
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
			ChannelPage pChan = tabControl.SelectedTab as ChannelPage;
			if (pChan != null)
			{
				string temp = pChan.Text;
				if (temp.StartsWith("#"))
					SelectedGroup.net.SendData("PART " + temp, false);
				else
					SelectedGroup.part(pChan);
			}
		}

		private void chanContext_Opening(object sender, CancelEventArgs e)
		{
			ChannelPage pChan = tabControl.SelectedTab as ChannelPage;

			bAddFav.Enabled = tabControl.SelectedTab.Text.StartsWith("#");

			if (tabControl.SelectedIndex > GroupLocation(SelectedGroup) + 1)
				bMoveLeft.Enabled = bClose.Enabled = true;
			else bMoveLeft.Enabled = bCloseServer.Enabled = false;

			if (tabControl.SelectedIndex < GroupEndLocation(SelectedGroup))
				bMoveRight.Enabled = true;
			else bMoveRight.Enabled = false;
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
			ChannelPage pChan = tabControl.SelectedTab as ChannelPage;
			if (index > GroupLocation(SelectedGroup) + 1)
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
			ChannelPage pChan = tabControl.SelectedTab as ChannelPage;
			if (pChan != null && index < GroupEndLocation(SelectedGroup))
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
			foreach (ChannelPage pChan in connectionGroup.channelPages)
			{
				if (pChan.Text == channel) return pChan;
			}
			return null;
		}

		public void InsertChan(ConnectionGroup cGroup, ChannelPage pChan)
		{
			Util.insertTab(tabControl, pChan, GroupEndLocation(cGroup));
		}

		private void chanContext_Closing(object sender, ToolStripDropDownClosingEventArgs e)
		{
			bMoveLeft.Enabled = bMoveRight.Enabled;
		}
	}
}
