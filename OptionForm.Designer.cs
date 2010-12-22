namespace IRCclient
{
	partial class OptionForm
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
			this.bOK = new System.Windows.Forms.Button();
			this.bCancel = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tabOption = new System.Windows.Forms.TabControl();
			this.pConnect = new System.Windows.Forms.TabPage();
			this.bcEncode = new System.Windows.Forms.Button();
			this.bFavorite = new System.Windows.Forms.Button();
			this.bRemoveServer = new System.Windows.Forms.Button();
			this.bAddServer = new System.Windows.Forms.Button();
			this.tGroup = new System.Windows.Forms.ComboBox();
			this.lGroup = new System.Windows.Forms.Label();
			this.cEncode = new System.Windows.Forms.ComboBox();
			this.lEncoding = new System.Windows.Forms.Label();
			this.tHost = new System.Windows.Forms.ComboBox();
			this.lHost = new System.Windows.Forms.Label();
			this.pMessage = new System.Windows.Forms.TabPage();
			this.tVersion = new System.Windows.Forms.TextBox();
			this.lVersion = new System.Windows.Forms.Label();
			this.tQuit = new System.Windows.Forms.TextBox();
			this.tUser = new System.Windows.Forms.TextBox();
			this.lQuit = new System.Windows.Forms.Label();
			this.lUser = new System.Windows.Forms.Label();
			this.pOther = new System.Windows.Forms.TabPage();
			this.cLog = new System.Windows.Forms.CheckBox();
			this.tDefNick = new System.Windows.Forms.TextBox();
			this.lDefNick = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.tabOption.SuspendLayout();
			this.pConnect.SuspendLayout();
			this.pMessage.SuspendLayout();
			this.pOther.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(156, 6);
			this.bOK.Name = "bOK";
			this.bOK.Size = new System.Drawing.Size(70, 21);
			this.bOK.TabIndex = 1;
			this.bOK.Text = "OK";
			this.bOK.UseVisualStyleBackColor = true;
			this.bOK.Click += new System.EventHandler(this.bOK_Click);
			// 
			// bCancel
			// 
			this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bCancel.Location = new System.Drawing.Point(232, 6);
			this.bCancel.Name = "bCancel";
			this.bCancel.Size = new System.Drawing.Size(70, 21);
			this.bCancel.TabIndex = 2;
			this.bCancel.Text = "Cancel";
			this.bCancel.UseVisualStyleBackColor = true;
			this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.bOK);
			this.panel1.Controls.Add(this.bCancel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 178);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(314, 34);
			this.panel1.TabIndex = 3;
			// 
			// tabOption
			// 
			this.tabOption.Controls.Add(this.pConnect);
			this.tabOption.Controls.Add(this.pMessage);
			this.tabOption.Controls.Add(this.pOther);
			this.tabOption.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabOption.Location = new System.Drawing.Point(0, 0);
			this.tabOption.Name = "tabOption";
			this.tabOption.SelectedIndex = 0;
			this.tabOption.Size = new System.Drawing.Size(314, 178);
			this.tabOption.TabIndex = 0;
			// 
			// pConnect
			// 
			this.pConnect.Controls.Add(this.bcEncode);
			this.pConnect.Controls.Add(this.bFavorite);
			this.pConnect.Controls.Add(this.bRemoveServer);
			this.pConnect.Controls.Add(this.bAddServer);
			this.pConnect.Controls.Add(this.tGroup);
			this.pConnect.Controls.Add(this.lGroup);
			this.pConnect.Controls.Add(this.cEncode);
			this.pConnect.Controls.Add(this.lEncoding);
			this.pConnect.Controls.Add(this.tHost);
			this.pConnect.Controls.Add(this.lHost);
			this.pConnect.Location = new System.Drawing.Point(4, 22);
			this.pConnect.Name = "pConnect";
			this.pConnect.Padding = new System.Windows.Forms.Padding(3);
			this.pConnect.Size = new System.Drawing.Size(306, 152);
			this.pConnect.TabIndex = 0;
			this.pConnect.Text = "Server";
			this.pConnect.UseVisualStyleBackColor = true;
			// 
			// bcEncode
			// 
			this.bcEncode.Location = new System.Drawing.Point(152, 60);
			this.bcEncode.Name = "bcEncode";
			this.bcEncode.Size = new System.Drawing.Size(146, 21);
			this.bcEncode.TabIndex = 4;
			this.bcEncode.Text = "Change Encoding";
			this.bcEncode.UseVisualStyleBackColor = true;
			this.bcEncode.Click += new System.EventHandler(this.bcEncode_Click);
			// 
			// bFavorite
			// 
			this.bFavorite.Location = new System.Drawing.Point(12, 125);
			this.bFavorite.Name = "bFavorite";
			this.bFavorite.Size = new System.Drawing.Size(100, 21);
			this.bFavorite.TabIndex = 7;
			this.bFavorite.Text = "Favorites...";
			this.bFavorite.UseVisualStyleBackColor = true;
			this.bFavorite.Click += new System.EventHandler(this.bFavorite_Click);
			// 
			// bRemoveServer
			// 
			this.bRemoveServer.Location = new System.Drawing.Point(228, 125);
			this.bRemoveServer.Name = "bRemoveServer";
			this.bRemoveServer.Size = new System.Drawing.Size(70, 21);
			this.bRemoveServer.TabIndex = 9;
			this.bRemoveServer.Text = "Remove";
			this.bRemoveServer.UseVisualStyleBackColor = true;
			this.bRemoveServer.Click += new System.EventHandler(this.bRemoveServer_Click);
			// 
			// bAddServer
			// 
			this.bAddServer.Location = new System.Drawing.Point(152, 125);
			this.bAddServer.Name = "bAddServer";
			this.bAddServer.Size = new System.Drawing.Size(70, 21);
			this.bAddServer.TabIndex = 8;
			this.bAddServer.Text = "Add";
			this.bAddServer.UseVisualStyleBackColor = true;
			this.bAddServer.Click += new System.EventHandler(this.bAddServer_Click);
			// 
			// tGroup
			// 
			this.tGroup.FormattingEnabled = true;
			this.tGroup.Location = new System.Drawing.Point(93, 9);
			this.tGroup.Name = "tGroup";
			this.tGroup.Size = new System.Drawing.Size(205, 20);
			this.tGroup.TabIndex = 1;
			this.tGroup.TextChanged += new System.EventHandler(this.tGroup_TextChanged);
			// 
			// lGroup
			// 
			this.lGroup.AutoSize = true;
			this.lGroup.Location = new System.Drawing.Point(10, 13);
			this.lGroup.Name = "lGroup";
			this.lGroup.Size = new System.Drawing.Size(39, 12);
			this.lGroup.TabIndex = 0;
			this.lGroup.Text = "Group";
			// 
			// cEncode
			// 
			this.cEncode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cEncode.FormattingEnabled = true;
			this.cEncode.Items.AddRange(new object[] {
            "CP949",
            "UTF-8",
            "UTF-16"});
			this.cEncode.Location = new System.Drawing.Point(93, 34);
			this.cEncode.Name = "cEncode";
			this.cEncode.Size = new System.Drawing.Size(205, 20);
			this.cEncode.TabIndex = 3;
			this.cEncode.SelectedIndexChanged += new System.EventHandler(this.cEncode_SelectedIndexChanged);
			// 
			// lEncoding
			// 
			this.lEncoding.AutoSize = true;
			this.lEncoding.Location = new System.Drawing.Point(10, 38);
			this.lEncoding.Name = "lEncoding";
			this.lEncoding.Size = new System.Drawing.Size(58, 12);
			this.lEncoding.TabIndex = 2;
			this.lEncoding.Text = "Encoding";
			// 
			// tHost
			// 
			this.tHost.FormattingEnabled = true;
			this.tHost.Location = new System.Drawing.Point(93, 94);
			this.tHost.Name = "tHost";
			this.tHost.Size = new System.Drawing.Size(205, 20);
			this.tHost.TabIndex = 6;
			// 
			// lHost
			// 
			this.lHost.AutoSize = true;
			this.lHost.Location = new System.Drawing.Point(10, 98);
			this.lHost.Name = "lHost";
			this.lHost.Size = new System.Drawing.Size(41, 12);
			this.lHost.TabIndex = 5;
			this.lHost.Text = "Server";
			// 
			// pMessage
			// 
			this.pMessage.Controls.Add(this.tVersion);
			this.pMessage.Controls.Add(this.lVersion);
			this.pMessage.Controls.Add(this.tQuit);
			this.pMessage.Controls.Add(this.tUser);
			this.pMessage.Controls.Add(this.lQuit);
			this.pMessage.Controls.Add(this.lUser);
			this.pMessage.Location = new System.Drawing.Point(4, 22);
			this.pMessage.Name = "pMessage";
			this.pMessage.Padding = new System.Windows.Forms.Padding(3);
			this.pMessage.Size = new System.Drawing.Size(306, 152);
			this.pMessage.TabIndex = 5;
			this.pMessage.Text = "Message";
			this.pMessage.UseVisualStyleBackColor = true;
			// 
			// tVersion
			// 
			this.tVersion.Location = new System.Drawing.Point(76, 43);
			this.tVersion.Name = "tVersion";
			this.tVersion.Size = new System.Drawing.Size(222, 21);
			this.tVersion.TabIndex = 3;
			this.tVersion.Text = "Minus One IRC [ver]";
			// 
			// lVersion
			// 
			this.lVersion.AutoSize = true;
			this.lVersion.Location = new System.Drawing.Point(8, 48);
			this.lVersion.Name = "lVersion";
			this.lVersion.Size = new System.Drawing.Size(58, 12);
			this.lVersion.TabIndex = 2;
			this.lVersion.Text = "VERSION";
			// 
			// tQuit
			// 
			this.tQuit.Location = new System.Drawing.Point(76, 71);
			this.tQuit.Name = "tQuit";
			this.tQuit.Size = new System.Drawing.Size(222, 21);
			this.tQuit.TabIndex = 5;
			this.tQuit.Text = "Minus One IRC";
			// 
			// tUser
			// 
			this.tUser.Location = new System.Drawing.Point(76, 15);
			this.tUser.Name = "tUser";
			this.tUser.Size = new System.Drawing.Size(222, 21);
			this.tUser.TabIndex = 1;
			this.tUser.Text = "minus1 0 * :Minus One";
			// 
			// lQuit
			// 
			this.lQuit.AutoSize = true;
			this.lQuit.Location = new System.Drawing.Point(8, 76);
			this.lQuit.Name = "lQuit";
			this.lQuit.Size = new System.Drawing.Size(33, 12);
			this.lQuit.TabIndex = 4;
			this.lQuit.Text = "QUIT";
			// 
			// lUser
			// 
			this.lUser.AutoSize = true;
			this.lUser.Location = new System.Drawing.Point(8, 20);
			this.lUser.Name = "lUser";
			this.lUser.Size = new System.Drawing.Size(37, 12);
			this.lUser.TabIndex = 0;
			this.lUser.Text = "USER";
			// 
			// pOther
			// 
			this.pOther.Controls.Add(this.cLog);
			this.pOther.Controls.Add(this.tDefNick);
			this.pOther.Controls.Add(this.lDefNick);
			this.pOther.Location = new System.Drawing.Point(4, 22);
			this.pOther.Name = "pOther";
			this.pOther.Padding = new System.Windows.Forms.Padding(3);
			this.pOther.Size = new System.Drawing.Size(306, 212);
			this.pOther.TabIndex = 6;
			this.pOther.Text = "Other";
			this.pOther.UseVisualStyleBackColor = true;
			// 
			// cLog
			// 
			this.cLog.AutoSize = true;
			this.cLog.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cLog.Enabled = false;
			this.cLog.Location = new System.Drawing.Point(6, 48);
			this.cLog.Name = "cLog";
			this.cLog.Size = new System.Drawing.Size(101, 16);
			this.cLog.TabIndex = 11;
			this.cLog.Text = "Autosave Log";
			this.cLog.UseVisualStyleBackColor = true;
			// 
			// tDefNick
			// 
			this.tDefNick.Location = new System.Drawing.Point(98, 15);
			this.tDefNick.Name = "tDefNick";
			this.tDefNick.Size = new System.Drawing.Size(200, 21);
			this.tDefNick.TabIndex = 9;
			this.tDefNick.Text = "Nick[-1]";
			// 
			// lDefNick
			// 
			this.lDefNick.AutoSize = true;
			this.lDefNick.Location = new System.Drawing.Point(8, 20);
			this.lDefNick.Name = "lDefNick";
			this.lDefNick.Size = new System.Drawing.Size(72, 12);
			this.lDefNick.TabIndex = 8;
			this.lDefNick.Text = "Default Nick";
			// 
			// OptionForm
			// 
			this.AcceptButton = this.bOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bCancel;
			this.ClientSize = new System.Drawing.Size(314, 212);
			this.Controls.Add(this.tabOption);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionForm";
			this.ShowInTaskbar = false;
			this.Text = "Options";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionForm_FormClosing);
			this.panel1.ResumeLayout(false);
			this.tabOption.ResumeLayout(false);
			this.pConnect.ResumeLayout(false);
			this.pConnect.PerformLayout();
			this.pMessage.ResumeLayout(false);
			this.pMessage.PerformLayout();
			this.pOther.ResumeLayout(false);
			this.pOther.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button bOK;
		private System.Windows.Forms.Button bCancel;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TabPage pConnect;
		private System.Windows.Forms.ComboBox cEncode;
		private System.Windows.Forms.Label lEncoding;
		private System.Windows.Forms.ComboBox tHost;
		private System.Windows.Forms.Label lHost;
		private System.Windows.Forms.TabControl tabOption;
		private System.Windows.Forms.TabPage pMessage;
		private System.Windows.Forms.TextBox tQuit;
		private System.Windows.Forms.TextBox tUser;
		private System.Windows.Forms.Label lQuit;
		private System.Windows.Forms.Label lUser;
		private System.Windows.Forms.TextBox tVersion;
		private System.Windows.Forms.Label lVersion;
		private System.Windows.Forms.ComboBox tGroup;
		private System.Windows.Forms.Label lGroup;
		private System.Windows.Forms.Button bAddServer;
		private System.Windows.Forms.Button bRemoveServer;
		private System.Windows.Forms.Button bFavorite;
		private System.Windows.Forms.Button bcEncode;
		private System.Windows.Forms.TabPage pOther;
		private System.Windows.Forms.CheckBox cLog;
		private System.Windows.Forms.TextBox tDefNick;
		private System.Windows.Forms.Label lDefNick;
	}
}