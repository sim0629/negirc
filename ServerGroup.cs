using System;
using System.Collections.Generic;
using System.Text;

namespace IRCclient
{
	public class ServerGroup
	{
		public string name;
		public Encoding encode;
		public List<string> host;
		public List<string> favorites;

		public ServerGroup(string name)
			: this(name, Encoding.UTF8, new List<string>(), new List<string>()) { }
		public ServerGroup(string name, Encoding encode)
			: this(name, encode, new List<string>(), new List<string>()) { }
		public ServerGroup(string name, Encoding encode, List<string> host, List<string> favorites)
		{
			this.name = name;
			this.encode = encode;
			this.host = host;
			this.favorites = favorites;
		}
	}
}