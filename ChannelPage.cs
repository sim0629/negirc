using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IRCclient
{
	public partial class ChannelPage : TabPage
	{
		public string selectedUser()
		{
			if (userList.SelectedItems.Count == 0) return "";
			string user = (string)userList.SelectedItem;
			if (user.StartsWith("@")) return user.Substring(1);
			else return user;
		}
		public string[] selectedUsers()
		{
			string[] result = new string[userList.SelectedItems.Count];
			userList.SelectedItems.CopyTo(result, 0);
			for (int i = 0; i < result.Length; i++)
				if (result[i].StartsWith("@")) result[i] = result[i].Substring(1);
			return result;
		}

		public void AddNick(string nick)				//Add nick in the userList
		{
			Util.addItem(userList, nick.StartsWith("+") ? nick.Substring(1) : nick);
		}
		public int RemoveNick(string nick)			  //Remove nick from the userList. Returns the status of nick(012)
		{
			int result = containsNick(nick);
			if (result == 2) nick = '@' + nick;
			if (result >= 1)
				Util.removeItem(userList, nick.StartsWith("+") ? nick.Substring(1) : nick);
			return result;
		}
		public int containsNick(string nick)			//0=no 1=contains 2=ob
		{
			if (this.userList.Items.Contains(nick)) return 1;
			else if (this.userList.Items.Contains('@' + nick)) return 2;
			else return 0;
		}
		public int ChangeNick(string nick, string newnick)	  //Changes nick in the userList
		{
			int result = this.RemoveNick(nick);
			if (result == 1 || newnick.StartsWith("@")) this.AddNick(newnick);
			else if (result == 2)
				this.AddNick('@' + newnick);
			return result;
		}
		public void SetTopic(string topic)
		{
			this.topic = topic;
			setInf();
		}

		public void AddChat(string msg, int level = 0)
		{
			Util.addText(this.tChan, msg, Util.levelColor(level));

			if (level == 1 || level == 2) setInf();
			if (!selected)
			{
				if (level == 0 || level == 4)
				{
					if (tChanged < 2)
					{
						tChanged = 2;
						Util.Refresh(con.tabControl, false);
					}
				}
				else if (tChanged == 0)
				{
					tChanged = 1;
					Util.Refresh(con.tabControl, false);
				}
			}
		}

		public void SetMode(string mode)
		{
			string[] tokens = mode.Split(' ');
			char[] chars = tokens[0].ToCharArray();
			bool positive = true;
			int parammods = 0;
			for (int i = 0; i < tokens[0].Length; i++)
			{
				if (mode[i] == '+') positive = true;
				else if (mode[i] == '-') positive = false;
				else if (mode[i] == 'o')
				{
					++parammods;
					if (positive) ChangeNick(tokens[parammods], "@" + tokens[parammods]);
					else ChangeNick("@" + tokens[parammods], tokens[parammods]);
				}
				else if (mode[i] == 'v' || mode[i] == 'h' || mode[i] == 'b')
					++parammods;
				else
				{
					if (mode[i] == 'l')
					{
						if (positive) chanmode.limit = int.Parse(tokens[++parammods]);
					}
					else if (mode[i] == 'k')
						chanmode.key = tokens[++parammods];
					chanmode.mode[mode[i]] = positive;
				}
				setInf();
			}
		}
		public void setInf()
		{
			Util.setText(tInf, this.channel + " [" + userList.Items.Count + "] " + chanmode.getMode() + (topic != "" ? ": " + topic : ""));
		}
	}
}
