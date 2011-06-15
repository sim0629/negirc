using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IRCclient
{
	public partial class FavoriteForm : Form
	{
		public static FavoriteForm thisfrm = null;
		private ServerGroup sGroup;

		public FavoriteForm()
		{
			InitializeComponent();

			if (thisfrm == null)
				thisfrm = this;
		}

		public void Open(ServerGroup sGroup)
		{
			this.sGroup = sGroup;
			lFavorite.Items.Clear();
			foreach (string fav in sGroup.favorites)
				Util.addItem(lFavorite, fav);

			this.Font = MainForm.thisfrm.Font;

			this.ShowDialog(OptionForm.thisfrm);
		}

		private void FavoriteForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			sGroup.favorites.Clear();
			foreach (string fav in lFavorite.Items)
				sGroup.favorites.Add(fav);
		}

		private void bAddFav_Click(object sender, EventArgs e)
		{
			string chan = tFavorite.Text;
			if (chan != "")
			{
				if (!chan.StartsWith("#")) chan = "#" + chan;
				Util.addItem(lFavorite, chan);
				tFavorite.Clear();
			}
		}

		private void bOK_Click(object sender, EventArgs e)
		{
			this.Hide();
		}

		private void favoriteContext_Opening(object sender, CancelEventArgs e)
		{
			if (lFavorite.SelectedItems.Count > 0)
				removeToolStripMenuItem.Enabled = true;
			else removeToolStripMenuItem.Enabled = false;
		}

		private void removeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			while (lFavorite.SelectedItems.Count > 0)
			{
				lFavorite.Items.Remove(lFavorite.SelectedItems[0]);
			}
		}

		private void clearFavoritesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			lFavorite.Items.Clear();
		}
	}
}
