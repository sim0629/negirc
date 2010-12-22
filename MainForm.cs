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
		private ConnectionPage SelectedPage;
		public MainForm()
		{
			OptionForm.thisfrm = new OptionForm();
			FavoriteForm.thisfrm = new FavoriteForm();
			InitializeComponent();
			addNewTab();
			this.tNick.Text = OptionForm.thisfrm.defaultNick;

			if (thisfrm == null)
				thisfrm = this;

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
				SelectedPage.Con = new Thread(new ParameterizedThreadStart(SelectedPage.ConnectStart));
				SelectedPage.Con.Start(tNick.Text);
			}
			else if (bConnect.Text == "Cancel")
			{
				try
				{
					if (SelectedPage.Con.IsAlive) SelectedPage.Con.Abort();
				}
				catch { }
				SelectedPage.Disconnect();
			}
			else SelectedPage.Disconnect();
		}
		private void bOption_Click(object sender, EventArgs e)
		{
			OptionForm.thisfrm.Open(SelectedPage);
		}
		private void NickChange(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter && tNick.Text != "")
				SelectedPage.NickChange(tNick.Text);
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
			SelectedPage = (ConnectionPage)tabControl.SelectedTab;
			SetProperty(SelectedPage, true);
			SetProperty(SelectedPage, false);
			tLineSelect();
		}
		private void bClose_Click(object sender, EventArgs e)
		{
			try
			{
				if (SelectedPage.connected != Util.ConnectedState.Disconnected) SelectedPage.Disconnect();
				Util.removeTab(tabControl, SelectedPage);
				tabControl.SelectedIndex = 0;
				SelectedPage = (ConnectionPage)tabControl.SelectedTab;
			}
			catch { }
		}
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			foreach (ConnectionPage pCon in tabControl.Controls)
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
				SelectedPage.tLineEntered(tLine.Text);
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
				else if (e.KeyCode == Keys.F4)
				{
					SelectedPage.bClose_Click(null,null);
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
			}
			else if (e.Modifiers == Keys.Alt)
			{
				if (e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9 && SelectedPage.tabControl.TabCount > (e.KeyCode - Keys.D1))
					SelectedPage.tabControl.SelectedIndex = e.KeyCode - Keys.D1;
				else if (e.KeyCode == Keys.D0 && SelectedPage.tabControl.TabCount > 9)
					SelectedPage.tabControl.SelectedIndex = 9;
				else if (e.KeyCode == Keys.Left && SelectedPage.tabControl.SelectedIndex > 0)
					SelectedPage.tabControl.SelectedIndex--;
				else if (e.KeyCode == Keys.Right && SelectedPage.tabControl.SelectedIndex < SelectedPage.tabControl.TabCount - 1)
					SelectedPage.tabControl.SelectedIndex++;
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
				if (e.Shift)
				{
					if (SelectedPage.tabControl.SelectedIndex == 0)
						SelectedPage.tabControl.SelectedIndex = SelectedPage.tabControl.TabCount - 1;
					else SelectedPage.tabControl.SelectedIndex--;
				}
				else
				{
					if (SelectedPage.tabControl.SelectedIndex == SelectedPage.tabControl.TabCount - 1)
						SelectedPage.tabControl.SelectedIndex = 0;
					else SelectedPage.tabControl.SelectedIndex++;
				}
			}
		}

		private void groupContext_Opening(object sender, CancelEventArgs e)
		{
			bClose.Enabled = tabControl.TabCount > 1;
		}

		public void tLog_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start(e.LinkText);
			}
			catch { }
		}

		private void newServerWindowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			addNewTab();
		}

		private void addNewTab()
		{
			SelectedPage = new ConnectionPage(OptionForm.thisfrm.Groups[0], OptionForm.thisfrm.defaultNick);
			tabControl.Controls.Add(SelectedPage);
			tabControl.SelectedTab = SelectedPage;
		}

		public void tLineSelect()
		{
			tLine.Select();
			tLine.Focus();
		}

		public bool isSelected(ConnectionPage cp)
		{
			return SelectedPage == cp;
		}

		private void tNick_TextChanged(object sender, EventArgs e)
		{
			if (SelectedPage.connected == Util.ConnectedState.Disconnected)
				SelectedPage.nick = tNick.Text;
		}

		public void SetProperty(ConnectionPage pCon, bool bnick)
		{
			if (pCon == SelectedPage)
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
	}
}
