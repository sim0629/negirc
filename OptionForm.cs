using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace IRCclient
{
	public partial class OptionForm : Form
	{
		/// <summary>
		/// singleton pattern에서 사용되는 정적 멤버
		/// </summary>
		public static OptionForm thisfrm = null;

		private ConnectionGroup con;
		public List<ServerGroup> Groups;
		/// <summary> PASS 메시지 </summary>
		public string passmsg = "PASS";
		/// <summary> USER 메시지 </summary>
		public string usermsg;
		/// <summary> QUIT 메시지 </summary>
		public string qmsg;
		/// <summary> CTCP VERSION 메시지 </summary>
		public string vermsg;
		/// <summary> 기본 닉 </summary>
		public string defaultNick;

		public Font fonttemp;
		private static string path = Application.ProductName + ".ini";

		/// <summary>
		/// 생성자. singleton pattern을 따른다.
		/// </summary>
		public OptionForm()
		{
			InitializeComponent();

			readAll();

			if (thisfrm == null)
				thisfrm = this;
		}

		/// <summary> ini파일에서 불러들인다. </summary>
		private void readAll()
		{
			StreamReader reader = null;
			try
			{
				reader = new StreamReader(path);
				string line;
				string lineupper;

				Groups = new List<ServerGroup>();
				ServerGroup sGroup = null;

				while ((line = reader.ReadLine()) != null)
				{
					lineupper = line.ToUpper();
					if (lineupper.StartsWith("FONT="))
					{
						try
						{
							String[] fontstr = line.Substring(5).Split(',');
							this.fonttemp = new Font(
								fontstr[0].Trim(),
								(float)Convert.ToDouble(fontstr[1]),
								(FontStyle)(Convert.ToInt32(fontstr[2])));
						}
						catch (ArgumentException) { }
					}
					else if (lineupper.StartsWith("NICK="))
						defaultNick = line.Substring(5);
					else if (lineupper.StartsWith("PASS="))
						passmsg = line.Substring(5);
					else if (lineupper.StartsWith("USER="))
						usermsg = line.Substring(5);
					else if (lineupper.StartsWith("VERSION="))
						vermsg = line.Substring(8);
					else if (lineupper.StartsWith("QUIT="))
						qmsg = line.Substring(5);
					else if (lineupper.StartsWith("GROUPNAME="))
					{
						sGroup = new ServerGroup(line.Substring(10));
						Groups.Add(sGroup);
					}
					else if (lineupper.StartsWith("ENCODING="))
					{
						switch (line.Substring(9))
						{
							case "UTF8": sGroup.encode = Encoding.UTF8; break;
							case "CP949": sGroup.encode = Encoding.GetEncoding(949); break;
							case "UTF16": sGroup.encode = Encoding.Unicode; break;
							default: sGroup.encode = Encoding.UTF8; break;
						}
					}
					else if (lineupper.StartsWith("HOST="))
						sGroup.host = new List<string>(line.Substring(5).Split(','));
					else if (lineupper.StartsWith("FAVORITE="))
					{
						if (line.Length > 9)
							sGroup.favorites = new List<string>(line.Substring(9).Split(','));
						else sGroup.favorites = new List<string>();
					}
				}
				if (Groups.Count == 0)
					Groups.Add(new ServerGroup("Freenode", Encoding.UTF8, new List<string> { "irc.freenode.net/8000", "irc.freenode.net&/7000" }, new List<string>()));
			}
			catch (Exception)
			{
				Groups = new List<ServerGroup>();

				Groups.Add(new ServerGroup("UriIRC", Encoding.UTF8, new List<string> { "irc.uriirc.org&/16663", "irc.uriirc.org/16667" }, new List<string>()));
				Groups.Add(new ServerGroup("HanIRC", Encoding.GetEncoding(949), new List<string> { "irc.hanirc.org/6664", "ddos.hanirc.org/6664", "purple.hanirc.org/6664", "64.71.156.44/6664" }, new List<string>()));
				Groups.Add(new ServerGroup("Freenode", Encoding.UTF8, new List<string> { "irc.freenode.net/8000", "irc.freenode.net&/7000" }, new List<string>()));
				cEncode.SelectedIndex = 1;

				passmsg = tPass.Text;
				usermsg = tUser.Text;
				qmsg = tQuit.Text;
				vermsg = "Minus One IRC v" + Util.VER;
				defaultNick = "Nick[-1]";
			}
			finally
			{
				tGroup.Items.Clear();
				foreach (ServerGroup sGroup in Groups)
					tGroup.Items.Add(sGroup.name);

				tGroup.SelectedIndex = 0;

				tPass.Text = passmsg;
				tUser.Text = usermsg;
				tQuit.Text = qmsg;
				if (vermsg == null || vermsg.StartsWith("Minus One IRC v")) vermsg = "Minus One IRC v" + Util.VER;
				tDefNick.Text = defaultNick;
				tVersion.Text = vermsg;

				if (reader != null) reader.Close();
				else saveAll();
			}
		}
		/// <summary> ini파일에 저장한다. </summary>
		public void saveAll()
		{
			StreamWriter writer = null;
			try
			{
				writer = new StreamWriter(path);

				writer.WriteLine("FONT=" + fonttemp.FontFamily.Name + "," + fonttemp.Size + "," + (int)fonttemp.Style);
				writer.WriteLine();
				writer.WriteLine("Nick=" + defaultNick);
				writer.WriteLine("PASS=" + passmsg);
				writer.WriteLine("USER=" + usermsg);
				writer.WriteLine("VERSION=" + vermsg);
				writer.WriteLine("QUIT=" + qmsg);
				foreach(ServerGroup sGroup in Groups)
				{
					string temp;
					writer.WriteLine();
					writer.WriteLine("GroupName=" + sGroup.name);
					temp = "Encoding=";
					switch (EncodeIndex(sGroup.encode))
					{
						case 0: temp += "CP949"; break;
						case 1: temp += "UTF8"; break;
						case 2: temp += "UTF16"; break;
						default: temp += "UTF8"; break;
					}
					writer.WriteLine(temp);
					temp = "Host=";
					foreach (string host in sGroup.host)
						temp += host + ",";
					writer.WriteLine(temp.Substring(0, temp.EndsWith(",") ? temp.Length - 1 : temp.Length));
					temp = "Favorite=";
					foreach (string fav in sGroup.favorites)
						temp += fav + ",";
					writer.WriteLine(temp.Substring(0, temp.EndsWith(",") ? temp.Length - 1 : temp.Length));
				}
			}
			catch { }
			finally
			{
				if (writer != null) writer.Close();
			}
		}

		/// <summary>
		/// ShowDialog대신 사용한다.
		/// Option중에는 ConnectionGroup에 따라 다른 설정이 저장되어 있는 것이 있으므로
		/// ConnectionGroup을 인자로 받는다.
		/// </summary>
		/// <param name="con"> 현재 지정된 ConnectionGroup </param>
		public void Open(ConnectionGroup con)
		{
			tGroup.SelectedIndex = -1;
			this.con = con;
			tGroup.Text = con.Text;
			tHost.Text = con.host;
			cEncode.SelectedIndex = EncodeIndex(con.encode);
			passmsg = tPass.Text;
			usermsg = tUser.Text;
			qmsg = tQuit.Text;
			vermsg = tVersion.Text;

			fontDialog.Font = fonttemp = MainForm.thisfrm.Font;
			fontTextUpdate();

			this.Font = MainForm.thisfrm.Font;

			this.ShowDialog(MainForm.thisfrm);
		}

		/// <summary> bFont의 텍스트를 업데이트한다. </summary>
		private void fontTextUpdate()
		{
			bFont.Text = fonttemp.FontFamily.Name + ", " + fonttemp.Size + ", " + fonttemp.Style.ToString();
		}

		private Encoding EncodeIndex(int index)
		{
			if (index == 0) return Encoding.GetEncoding(949);
			else if (index == 2) return Encoding.Unicode;
			else return Encoding.UTF8;
		}
		private int EncodeIndex(Encoding encode)
		{
			if (encode == Encoding.GetEncoding(949)) return 0;
			else if (encode == Encoding.Unicode) return 2;
			else return 1;
		}

		private void bOK_Click(object sender, EventArgs e)
		{
			if (con.connected == Util.ConnectedState.Disconnected)
			{
				con.Text = tGroup.Text;
				con.host = tHost.Text;
				con.encode = EncodeIndex(cEncode.SelectedIndex);

				AddHost(tGroup.Text, tHost.Text); 
				con.sGroup = findGroup(tGroup.Text);
			}
			MainForm.thisfrm.Font = fonttemp;
			passmsg = tPass.Text;
			usermsg = tUser.Text;
			qmsg = tQuit.Text;
			vermsg = tVersion.Text;
			defaultNick = tDefNick.Text;

			this.Close();
		}

		private void bCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private ServerGroup findGroup(string name)
		{
			foreach (ServerGroup group in Groups)
				if (group.name == name) return group;
			return null;
		}

		private void bcEncode_Click(object sender, EventArgs e)
		{
			ServerGroup group = findGroup(tGroup.Text);
			if (group != null)
				group.encode = EncodeIndex(cEncode.SelectedIndex);
			bcEncode.Enabled = false;
			cEncode.Select();
		}

		private void tGroup_TextChanged(object sender, EventArgs e)
		{
			ServerGroup group = findGroup(tGroup.Text);
			tHost.Items.Clear();
			bcEncode.Enabled = false;
			if (group != null)
			{
				tHost.Items.AddRange(group.host.ToArray());
				if (!group.host.Contains(tHost.Text))
					tHost.Text = group.host[0];
				cEncode.SelectedIndex = EncodeIndex(group.encode);
			}
			else
				tHost.ResetText();
		}

		private void cEncode_SelectedIndexChanged(object sender, EventArgs e)
		{
			ServerGroup group = findGroup(tGroup.Text);
			if (group != null)
				bcEncode.Enabled = (cEncode.SelectedIndex != EncodeIndex(group.encode));
		}

		private void AddHost(string group, string host)
		{
			if (group == "" || host == "") return;
			Util.addItem(tHost, host);
			ServerGroup sGroup = findGroup(group);
			if (sGroup == null)
			{
				Util.addItem(tGroup, group);
				sGroup = new ServerGroup(group, EncodeIndex(cEncode.SelectedIndex));
				Groups.Add(sGroup);
			}
			if (!sGroup.host.Contains(host))
				sGroup.host.Add(host);
		}

		private void bAddServer_Click(object sender, EventArgs e)
		{
			AddHost(tGroup.Text, tHost.Text);
		}

		private void bRemoveServer_Click(object sender, EventArgs e)
		{
			string sGroup = tGroup.Text;
			string host = tHost.Text;

			if (tHost.Items.Contains(host))
			{
				Util.removeItem(tHost, host);
				ServerGroup group = findGroup(sGroup);
				group.host.Remove(host);
				if (group.host.Count == 0)
				{
					Util.removeItem(tGroup, sGroup);
					Groups.Remove(group);
					Util.setText(tGroup, Groups.Count > 0 ? tGroup.Items[0].ToString() : "");
				}
				else Util.setText(tHost, tHost.Items[0].ToString());
			}
		}

		private void bFavorite_Click(object sender, EventArgs e)
		{
			FavoriteForm.thisfrm.Open(findGroup(tGroup.Text));
		}

		private void OptionForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			saveAll();
		}

		private void bFont_Click(object sender, EventArgs e)
		{
			fontDialog.ShowDialog(this);
			this.fonttemp = fontDialog.Font;
			fontTextUpdate();
		}
	}
}
