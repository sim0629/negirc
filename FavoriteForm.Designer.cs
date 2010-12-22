namespace IRCclient
{
	partial class FavoriteForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.lFavorite = new System.Windows.Forms.ListBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.bOK = new System.Windows.Forms.Button();
			this.bAddFav = new System.Windows.Forms.Button();
			this.tFavorite = new System.Windows.Forms.TextBox();
			this.favoriteContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearFavoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.panel1.SuspendLayout();
			this.favoriteContext.SuspendLayout();
			this.SuspendLayout();
			// 
			// lFavorite
			// 
			this.lFavorite.ContextMenuStrip = this.favoriteContext;
			this.lFavorite.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lFavorite.FormattingEnabled = true;
			this.lFavorite.ItemHeight = 12;
			this.lFavorite.Location = new System.Drawing.Point(0, 0);
			this.lFavorite.Name = "lFavorite";
			this.lFavorite.Size = new System.Drawing.Size(294, 172);
			this.lFavorite.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.bOK);
			this.panel1.Controls.Add(this.bAddFav);
			this.panel1.Controls.Add(this.tFavorite);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 172);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(294, 40);
			this.panel1.TabIndex = 1;
			// 
			// bOK
			// 
			this.bOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bOK.Location = new System.Drawing.Point(320, 50);
			this.bOK.Name = "bOK";
			this.bOK.Size = new System.Drawing.Size(10, 10);
			this.bOK.TabIndex = 2;
			this.bOK.TabStop = false;
			this.bOK.Text = "OK";
			this.bOK.UseVisualStyleBackColor = true;
			this.bOK.Click += new System.EventHandler(this.bOK_Click);
			// 
			// bAddFav
			// 
			this.bAddFav.Location = new System.Drawing.Point(162, 6);
			this.bAddFav.Name = "bAddFav";
			this.bAddFav.Size = new System.Drawing.Size(120, 21);
			this.bAddFav.TabIndex = 1;
			this.bAddFav.Text = "Add to Favorite";
			this.bAddFav.UseVisualStyleBackColor = true;
			this.bAddFav.Click += new System.EventHandler(this.bAddFav_Click);
			// 
			// tFavorite
			// 
			this.tFavorite.Location = new System.Drawing.Point(12, 6);
			this.tFavorite.Name = "tFavorite";
			this.tFavorite.Size = new System.Drawing.Size(144, 21);
			this.tFavorite.TabIndex = 0;
			// 
			// favoriteContext
			// 
			this.favoriteContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem,
            this.clearFavoritesToolStripMenuItem});
			this.favoriteContext.Name = "favoriteContext";
			this.favoriteContext.Size = new System.Drawing.Size(222, 70);
			this.favoriteContext.Opening += new System.ComponentModel.CancelEventHandler(this.favoriteContext_Opening);
			// 
			// removeToolStripMenuItem
			// 
			this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
			this.removeToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
			this.removeToolStripMenuItem.Text = "Remove this from Favorites";
			this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
			// 
			// clearFavoritesToolStripMenuItem
			// 
			this.clearFavoritesToolStripMenuItem.Name = "clearFavoritesToolStripMenuItem";
			this.clearFavoritesToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
			this.clearFavoritesToolStripMenuItem.Text = "Clear Favorites";
			this.clearFavoritesToolStripMenuItem.Click += new System.EventHandler(this.clearFavoritesToolStripMenuItem_Click);
			// 
			// FavoriteForm
			// 
			this.AcceptButton = this.bAddFav;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bOK;
			this.ClientSize = new System.Drawing.Size(294, 212);
			this.Controls.Add(this.lFavorite);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FavoriteForm";
			this.ShowInTaskbar = false;
			this.Text = "Favorites";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FavoriteForm_FormClosing);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.favoriteContext.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox lFavorite;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button bAddFav;
		private System.Windows.Forms.TextBox tFavorite;
		private System.Windows.Forms.Button bOK;
		private System.Windows.Forms.ContextMenuStrip favoriteContext;
		private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearFavoritesToolStripMenuItem;
	}
}