using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IRCclient
{
	public static class Util
	{
		public const string VER = "1.1.2.61";
		public const string clientURL = "http://tail41.snucse.org/program/";
		public static Color levelColor(int level)
		{
			if (level == 1) return Color.SkyBlue;		//join,part
			else if (level == 2) return Color.Blue;		//quit
			else if (level == 3) return Color.Green;	//other system(mode, nick)
			else if (level == 4) return Color.Violet;	//NOTICE
			else return Color.Black;					//normal
		}
		public static int levelColor(Color tcolor)
		{
			if (tcolor == Color.SkyBlue) return 1;		//join,part
			else if (tcolor == Color.Blue) return 2;	//quit
			else if (tcolor == Color.Green) return 3;	//other system(mode, nick)
			else if (tcolor == Color.Violet) return 4;	//NOTICE
			else return 0;								//normal
		}
		public static string timestamp()				//returns current time in the form of timestamp
		{
			DateTime now = DateTime.Now;
			string result = now.Year + "." + now.Month + "." + now.Day + ". " +
				(now.Hour <= 9 ? "0" : "") + now.Hour + ":" +
				(now.Minute <= 9 ? "0" : "") + now.Minute + ":" +
				(now.Second <= 9 ? "0" : "") + now.Second + "." +
				(now.Millisecond <= 99 ? "0" : "") + (now.Millisecond <= 9 ? "0" : "") + now.Millisecond;
			return result;
		}
		public static string GetFromDialog(string text, string title, string defaultstring)
		{
			return Microsoft.VisualBasic.Interaction.InputBox(text, title, defaultstring);
		}
		public enum ConnectedState { Disconnected, Connecting, Connected };
		public static string ConnectedString(ConnectedState state)
		{
			switch (state)
			{
				case ConnectedState.Disconnected: return "Connect";
				case ConnectedState.Connecting: return "Cancel";
				case ConnectedState.Connected: return "Disconnect";
				default: return null;
			}
		}

		private delegate void textHandler(Control ctrl, string text);
		private delegate void rtbHandler(RichTextBox ctrl, string text, Color tcolor);
		private delegate void enableHandler(Control ctrl, bool enable);
		private delegate void voidboolHandler(bool b);
		private delegate void voidintHandler(int b);
		private delegate void tabHandler(TabPage page);
		private delegate void inttabHandler(int i, TabPage page);
		private delegate int intstrHandler(string nick);
		private delegate void voidstrHandler(string nick);
		private delegate DialogResult msgboxHandler(IWin32Window owner, string text, string caption, MessageBoxButtons buttons);
		private delegate void tabPageHandler(TabControl tab, TabPage page);

		public static void Refresh(Control ctrl, bool b)
		{
			if (ctrl.InvokeRequired)
				ctrl.Invoke(new voidboolHandler(ctrl.Invalidate), b);
			else ctrl.Invalidate(b);
		}

		public static void setEnable(Control ctrl, bool enable)
		{
			if (ctrl.InvokeRequired)
				ctrl.Invoke((enableHandler)delegate(Control c, bool e)
				{ c.Enabled = e; }, ctrl, enable);
			else ctrl.Enabled = enable;
		}

		public static void setText(Control ctrl, string text)
		{
			try
			{
				if (ctrl == null) return;
				if (ctrl.InvokeRequired)
					ctrl.Invoke((textHandler)delegate(Control c, string t)
					{ c.Text = t; }, ctrl, text);
				else ctrl.Text = text;
			}
			catch { }
		}

		private static void uaddText(RichTextBox ctrl, string text, Color tcolor)
		{
			if (ctrl.Text != "") text = '\n' + text;
			lock (ctrl)
			{
				ctrl.Select(ctrl.Text.Length, 0);
				ctrl.SelectionColor = tcolor;
				ctrl.AppendText(text);
				ctrl.Select(ctrl.Text.Length, 0);
				if (MainForm.thisfrm.cScrollEnd.Checked) ctrl.ScrollToCaret();
			}
		}
		public static void addText(RichTextBox ctrl, string text, Color tcolor)
		{
			if (ctrl.InvokeRequired)
				ctrl.Invoke(new rtbHandler(uaddText), ctrl, text, tcolor);
			else uaddText(ctrl, text, tcolor);
		}
		public static void addText(RichTextBox ctrl, string text)
		{
			addText(ctrl, text, Color.Black);
		}

		public static void addTab(TabControl tab, TabPage page)
		{
			if (tab.InvokeRequired)
				tab.Invoke(new tabHandler(tab.Controls.Add), page);
			else tab.Controls.Add(page);
		}
		public static void removeTab(TabControl tab, TabPage page)
		{
			if (tab.InvokeRequired)
				tab.Invoke(new tabHandler(tab.Controls.Remove), page);
			else tab.Controls.Remove(page);
		}
		public static void removeTab(TabControl tab, int index)
		{
			if (tab.InvokeRequired)
				tab.Invoke(new voidintHandler(tab.Controls.RemoveAt), index);
			else tab.Controls.RemoveAt(index);
		}
		public static void insertTab(TabControl tab, TabPage page, int index)
		{
			if (tab.InvokeRequired)
				tab.Invoke(new inttabHandler(tab.TabPages.Insert), index, page);
			else tab.TabPages.Insert(index, page);
		}

		private static void uaddItem(Control list, string text)
		{
			lock (list)
			{
				ListBox llist = list as ListBox;
				if (llist != null && !llist.Items.Contains(text)) llist.Items.Add(text);

				ComboBox clist = list as ComboBox;
				if (clist != null && !clist.Items.Contains(text)) clist.Items.Add(text);
			}
		}
		public static void addItem(ListControl list, string text)
		{
			if (list == null || text == null || text == "") return;
			if (list.InvokeRequired)
				list.Invoke(new textHandler(uaddItem), list, text);
			else uaddItem(list, text);
		}
		private static void uremoveItem(Control list, string text)
		{
			lock (list)
				if (list is ListBox)
					((ListBox)list).Items.Remove(text);
				else if (list is ComboBox)
					((ComboBox)list).Items.Remove(text);
		}
		public static void removeItem(ListControl list, string text)
		{
			if (list == null) return;
			if (list.InvokeRequired)
				list.Invoke(new textHandler(uremoveItem), list, text);
			else uremoveItem(list, text);
		}

		public static DialogResult ShowMessageBox(Form owner, string text, string caption, MessageBoxButtons buttons = MessageBoxButtons.OK)
		{
			if (owner.InvokeRequired)
				return (DialogResult)owner.Invoke(new msgboxHandler(MessageBox.Show), owner, text, caption, buttons);
			else return MessageBox.Show(owner, text, caption, buttons);
		}

		public static void selectPage(TabControl tab, TabPage page)
		{
			if (tab.InvokeRequired)
				tab.Invoke((tabHandler)delegate(TabPage p)
				{ tab.SelectedTab = p; }, page);
			else tab.SelectedTab = page;
		}

		public static bool ValidateServerCertificate(
			object sender,
			System.Security.Cryptography.X509Certificates.X509Certificate certificate,
			System.Security.Cryptography.X509Certificates.X509Chain chain,
			System.Net.Security.SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}
	}
}
