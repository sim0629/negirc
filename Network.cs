using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Threading;
using System.Text;
using System.IO;

namespace IRCclient
{
	public class Network
	{
		ConnectionPage con;
		private TcpClient client = null;
		private NetworkStream stream = null;
		private StreamReader reader = null;
		private StreamWriter writer = null;
		Thread Receiver;

		public string nick = null;

		public Network(ConnectionPage con)
		{
			this.con = con;
		}

		public bool Connect(string ip, string nick)
		{
			bool SSL = false;
			this.nick = nick;
			string[] hostarray = ip.Split('/');
			try
			{
				string host = hostarray[0];
				if (hostarray[0].EndsWith("&"))
				{
					SSL = true;
					host = hostarray[0].Substring(0, hostarray[0].Length - 1);
				}
				client = new TcpClient(host, int.Parse(hostarray[1]));
				stream = client.GetStream();
				Encoding encode = con.GetEncoding();

				if (SSL)
				{
					SslStream sstream = new SslStream(stream);
					sstream.AuthenticateAsClient(host);
					sstream.WriteTimeout = 5000;
					reader = new StreamReader(sstream, encode);
					writer = new StreamWriter(sstream, encode);
				}
				else
				{
					reader = new StreamReader(stream, encode);
					writer = new StreamWriter(stream, encode);
				}

				Receiver = new Thread(new ThreadStart(ReceiveData));
				Receiver.Start();

				return true;
			}
			catch (Exception e)
			{
				Console.Out.WriteLine(e.Message);
				return false;
			}
		}
		public void Disconnect()
		{
			con.setdis();
			try
			{
				if (client.Connected)
				{
					if (con.GetQmsg() == "")
						SendData("QUIT", true);
					else SendData("QUIT :" + con.GetQmsg(), true);
					if (reader != null) reader.Close();
					if (writer != null) writer.Close();
					if (stream != null) stream.Close();
					client.Close();
				}
			}
			catch { }
			try
			{
				if (Receiver.IsAlive) Receiver.Abort();
			}
			catch { }
		}

		static public string Checkver()
		{
			try
			{
				WebClient verclient = new WebClient();
				return verclient.DownloadString(Util.clientURL + "data/[-1]ver.txt").Trim();
			}
			catch (Exception)
			{
				return null;
			}
		}
		static public string Checklog()
		{
			try
			{
				WebClient logclient = new WebClient();
				string log = logclient.DownloadString(Util.clientURL + "data/[-1]log.txt");
				string[] tokens = log.Split('\n');
				string result = "";

				for (int i = 0; i < tokens.Length; i++)
				{
					if (tokens[i].StartsWith("v"))
					{
						if (tokens[i] == "v" + Util.VER)
							break;
						else continue;
					}
					if (tokens[i] != "")
						result += "\r\n" + tokens[i];
				}
				return result;
			}
			catch (Exception)
			{
				return "";
			}
		}
		static public bool Download(string URL, string file)
		{
			try
			{
				WebClient logclient = new WebClient();
				logclient.DownloadFile(URL, file);
				return true;
			}
			catch (Exception) { return false; }
		}

		public void SendData(string msg, bool writelog)
		{
			if (msg == null || msg == "") return;
			try
			{
				bool bcontent = false;
				string[] commands = null;
				string content = null;
				string temp = msg;

				if (msg.Contains(" :")) bcontent = true;

				if (bcontent)
				{
					int index = temp.IndexOf(" :");
					content = temp.Substring(index + 2);
					temp = temp.Substring(0, index);
				}
				commands = temp.Split(' ');

				writer.WriteLine(msg);
				writer.Flush();
				try
				{
					if (commands[0].ToUpper() == "PRIVMSG")
					{
						if (msg.Contains(" :"))
							con.AddChat("<" + nick + "> " + content, commands[1]);
						else
							con.AddChat("<" + nick + "> " + commands[commands.Length - 1], commands[1]);
					}
					else if (commands[0].ToUpper() == "NOTICE")
					{
						if (msg.Contains(" :"))
							con.AddChat("<" + nick + "> " + content, commands[1], 4);
						else
							con.AddChat("<" + nick + "> " + commands[commands.Length - 1], commands[1], 4);
					}
				}
				catch { }
				if (writelog) con.addLog(">> " + msg);
			}
			catch (Exception)
			{
				Disconnect();
			}
		}

		public void ReceiveData()
		{
			string msg = null;
			try
			{
				while ((msg = reader.ReadLine()) != null)
				{
					bool bsender = false;
					bool bcontent = false;
					bool buser = false;
					string sender = null;
					string[] commands = null;
					string content = null;
					string[] contents = null;
					string temp = msg;

					if (msg.StartsWith(":")) bsender = true;
					if (msg.Contains(" :")) bcontent = true;

					if (bsender)
					{
						int index = temp.IndexOf(' ');
						sender = temp.Substring(1, index - 1);
						if (sender.Contains("!"))
						{
							sender = sender.Substring(0, sender.LastIndexOf('!'));
							buser = true;
						}
						temp = temp.Substring(index + 1);
					}
					if (bcontent)
					{
						int index = temp.IndexOf(" :");
						content = temp.Substring(index + 2);
						contents = content.Split(' ');
						temp = temp.Substring(0, index);
					}
					commands = temp.Split(' ');

					try
					{
						string chan = null;
						string saying = null;
						string result = null;

						switch (commands[0])
						{
							case "PING":				//PING,PONG message
								con.setPing(++con.ping);
								SendData("PONG" + msg.Substring(4), false);
								break;
							case "PRIVMSG":				//PRIVMSG
								if (commands[1] == nick)
								{
									con.AddChat("<" + sender + "> " + content, sender, 0);
									if (content.ToUpper() == "VERSION" && con.GetVmsg() != "")
										SendData("NOTICE " + sender + " :VERSION " + con.GetVmsg() + '', true);
								}
								else con.AddChat("<" + sender + "> " + content, commands[1], 0);
								break;
							case "NOTICE":					//NOTICE
								if (buser)					//user notice
								{
									if (commands[1] == nick)
										con.AddChat("<" + sender + "> " + content, sender, 4);
									else if (commands[1].StartsWith("#"))
										con.AddChat("<" + sender + "> " + content, commands[1], 4);
								}
								else con.addLog("<< " + msg);	//server notice
								break;
							case "001":						//Welcome message
								con.addLog("<< " + msg);
								this.nick = commands[1];
								con.SetNick(this.nick);
								result = "";
								foreach (string fav in con.sGroup.favorites)
								{
									if (result != "") result += ",";
									result += fav;
								}
								if (result != "") SendData("JOIN " + result, true);
								break;
							case "324":						 //Mode
								con.addLog("<< " + msg);
								result = commands[3];
								for (int i = 4; i < commands.Length; i++)
									result += " " + commands[i];
								con.SetMode(result, commands[2], null);
								break;
							case "332":						 //Topic
								con.addLog("<< " + msg);
								con.SetTopic(content, commands[2], null);
								break;
							case "353":						 //User list
								con.addLog("<< " + msg);
								for (int i = 0; i < contents.Length; i++)
									con.AddNick(contents[i], commands[3]);
								break;
							case "JOIN":						//JOIN to a channel
								if (commands.Length > 1)
								{
									chan = commands[1];
									saying = content;
								}
								else chan = content;

								if (sender == nick)
								{
									SendData("MODE " + chan, true);
									con.join(chan);
								}
								else
								{
									con.AddNick(sender, chan);
									con.AddChat("[JOIN] " + sender, chan, 1);
								}
								break;
							case "PART":						//PART from a channel
								if (commands.Length > 1)
								{
									chan = commands[1];
									saying = content;
								}
								else chan = content;

								if (sender == nick)
									con.part(chan);
								else
								{
									con.RemoveNick(sender, chan);
									if (saying != null)
										con.AddChat("[PART] " + sender + " (" + saying + ")", chan, 1);
									else con.AddChat("[PART] " + sender, chan, 1);
								}
								break;
							case "KICK":						//KICK from a channel
								string kicked = null;

								chan = commands[1];
								if (commands.Length > 2)
								{
									kicked = commands[2];
									saying = content;
								}
								else kicked = content;

								if (kicked == nick)
									con.part(chan);
								else
								{
									con.RemoveNick(kicked, chan);
									if (saying != null)
										con.AddChat(sender + " kicks " + kicked + " (" + saying + ")", chan, 3);
									else con.AddChat(sender + " kicks " + kicked, chan, 3);
								}
								break;
							case "QUIT":						//Client QUIT
								con.QuitNick(sender, content);
								break;
							case "NICK":						//NICK change
								string newnick = null;
								if (commands.Length > 1)
									newnick = commands[1];
								else newnick = content;
								if (sender == nick)
								{
									nick = newnick;
									con.SetNick(nick);
								}
								con.ChangeNick(sender, newnick);
								break;
							case "MODE":						//MODE change
								if (!commands[1].StartsWith("#")) continue;
								result = commands[2];
								for (int i = 3; i < commands.Length; i++)
									result += " " + commands[i];
								con.SetMode(result, commands[1], sender);
								break;
							case "TOPIC":					   //TOPIC change
								con.SetTopic(content, commands[1], sender);
								break;
							default:
								con.addLog("<< " + msg);
								break;
						}
					}
					catch (Exception ex)
					{
#if DEBUG
						con.addLog("U< " + msg);
						try
						{
							string log = "[" + Util.timestamp() + "] (" + msg + ") " + ex.ToString();
							StreamWriter file = File.AppendText("CrashLog.log");
							file.WriteLine(log);
							file.Close();
						}
						catch { }
#endif
					}
				}
			}
			catch (ThreadAbortException) { }
			catch (Exception ex)
			{
#if DEBUG
				try
				{
					string log = "[" + Util.timestamp() + "] (" + msg + ") " + ex.ToString();
					StreamWriter file = File.AppendText("CrashLog.log");
					file.WriteLine(log);
					file.Close();
				}
				catch { }
#endif
			}
			finally
			{
				Disconnect();
			}
		}
	}
}
