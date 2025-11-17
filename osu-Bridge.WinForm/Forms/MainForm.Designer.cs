namespace osu_Bridge.WinForm.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            launchButton = new Button();
            settingsPanels = new Panel();
            profileComboBox = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            serverComboBox = new ComboBox();
            generateServer = new Button();
            generateProfile = new Button();
            osuFolderTextBox = new TextBox();
            label4 = new Label();
            openOsuFolderButton = new Button();
            removeButton = new Button();
            label5 = new Label();
            menuStrip1 = new MenuStrip();
            toolMenu = new ToolStripMenuItem();
            selectSkinMenu = new ToolStripMenuItem();
            selectServerMenu = new ToolStripMenuItem();
            openSongsFolderButton = new Button();
            label6 = new Label();
            songsFolderTextBox = new TextBox();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // launchButton
            // 
            launchButton.Font = new Font("Yu Gothic UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 128);
            launchButton.Location = new Point(12, 421);
            launchButton.Name = "launchButton";
            launchButton.Size = new Size(340, 58);
            launchButton.TabIndex = 0;
            launchButton.Text = "Launch";
            launchButton.UseVisualStyleBackColor = true;
            launchButton.Click += LaunchButton_Click;
            // 
            // settingsPanels
            // 
            settingsPanels.AutoScroll = true;
            settingsPanels.BackColor = SystemColors.Control;
            settingsPanels.Location = new Point(358, 71);
            settingsPanels.Name = "settingsPanels";
            settingsPanels.Size = new Size(294, 360);
            settingsPanels.TabIndex = 1;
            // 
            // profileComboBox
            // 
            profileComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            profileComboBox.Font = new Font("Yu Gothic UI", 12F);
            profileComboBox.FormattingEnabled = true;
            profileComboBox.Location = new Point(12, 363);
            profileComboBox.Name = "profileComboBox";
            profileComboBox.Size = new Size(250, 29);
            profileComboBox.TabIndex = 3;
            profileComboBox.SelectedIndexChanged += ProfileComboBox_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic UI Semibold", 15F, FontStyle.Bold);
            label2.Location = new Point(12, 332);
            label2.Name = "label2";
            label2.Size = new Size(101, 28);
            label2.TabIndex = 4;
            label2.Text = "プロファイル";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Yu Gothic UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label3.Location = new Point(12, 248);
            label3.Name = "label3";
            label3.Size = new Size(72, 28);
            label3.TabIndex = 6;
            label3.Text = "サーバー";
            // 
            // serverComboBox
            // 
            serverComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            serverComboBox.Font = new Font("Yu Gothic UI", 12F);
            serverComboBox.FormattingEnabled = true;
            serverComboBox.Location = new Point(12, 279);
            serverComboBox.Name = "serverComboBox";
            serverComboBox.Size = new Size(250, 29);
            serverComboBox.TabIndex = 5;
            serverComboBox.SelectedIndexChanged += ServerComboBox_SelectedIndexChanged;
            // 
            // generateServer
            // 
            generateServer.Font = new Font("Yu Gothic UI", 9F);
            generateServer.Location = new Point(268, 279);
            generateServer.Name = "generateServer";
            generateServer.Size = new Size(84, 29);
            generateServer.TabIndex = 7;
            generateServer.Text = "新規作成";
            generateServer.UseVisualStyleBackColor = true;
            generateServer.Click += GenerateServer_Click;
            // 
            // generateProfile
            // 
            generateProfile.Font = new Font("Yu Gothic UI", 9F);
            generateProfile.Location = new Point(268, 363);
            generateProfile.Name = "generateProfile";
            generateProfile.Size = new Size(84, 29);
            generateProfile.TabIndex = 8;
            generateProfile.Text = "新規作成";
            generateProfile.UseVisualStyleBackColor = true;
            generateProfile.Click += GenerateProfile_Click;
            // 
            // osuFolderTextBox
            // 
            osuFolderTextBox.Font = new Font("Yu Gothic UI", 11F);
            osuFolderTextBox.Location = new Point(12, 64);
            osuFolderTextBox.Name = "osuFolderTextBox";
            osuFolderTextBox.Size = new Size(250, 27);
            osuFolderTextBox.TabIndex = 9;
            osuFolderTextBox.TextChanged += OsuFolderTextBox_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            label4.Location = new Point(12, 40);
            label4.Name = "label4";
            label4.Size = new Size(105, 21);
            label4.TabIndex = 10;
            label4.Text = "osu! フォルダー";
            // 
            // openOsuFolderButton
            // 
            openOsuFolderButton.Location = new Point(268, 64);
            openOsuFolderButton.Name = "openOsuFolderButton";
            openOsuFolderButton.Size = new Size(84, 27);
            openOsuFolderButton.TabIndex = 11;
            openOsuFolderButton.Text = "開く";
            openOsuFolderButton.UseVisualStyleBackColor = true;
            openOsuFolderButton.Click += OpenOsuFolderButton_Click;
            // 
            // removeButton
            // 
            removeButton.Font = new Font("Yu Gothic UI Semibold", 13F, FontStyle.Bold);
            removeButton.Location = new Point(358, 437);
            removeButton.Name = "removeButton";
            removeButton.Size = new Size(294, 42);
            removeButton.TabIndex = 12;
            removeButton.Text = "削除";
            removeButton.UseVisualStyleBackColor = true;
            removeButton.Click += RemoveButton_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Yu Gothic UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label5.Location = new Point(358, 40);
            label5.Name = "label5";
            label5.Size = new Size(86, 28);
            label5.TabIndex = 13;
            label5.Text = "プロパティ";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolMenu });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(664, 24);
            menuStrip1.TabIndex = 14;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolMenu
            // 
            toolMenu.DropDownItems.AddRange(new ToolStripItem[] { selectSkinMenu, selectServerMenu });
            toolMenu.Name = "toolMenu";
            toolMenu.Size = new Size(46, 20);
            toolMenu.Text = "ツール";
            // 
            // selectSkinMenu
            // 
            selectSkinMenu.Name = "selectSkinMenu";
            selectSkinMenu.Size = new Size(186, 22);
            selectSkinMenu.Text = "スキン選択";
            // 
            // selectServerMenu
            // 
            selectServerMenu.Name = "selectServerMenu";
            selectServerMenu.Size = new Size(186, 22);
            selectServerMenu.Text = "サーバープロファイル選択";
            // 
            // openSongsFolderButton
            // 
            openSongsFolderButton.Location = new Point(268, 127);
            openSongsFolderButton.Name = "openSongsFolderButton";
            openSongsFolderButton.Size = new Size(84, 27);
            openSongsFolderButton.TabIndex = 17;
            openSongsFolderButton.Text = "開く";
            openSongsFolderButton.UseVisualStyleBackColor = true;
            openSongsFolderButton.Click += OpenSongsFolderButton_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            label6.Location = new Point(12, 103);
            label6.Name = "label6";
            label6.Size = new Size(119, 21);
            label6.TabIndex = 16;
            label6.Text = "Songs フォルダー";
            // 
            // songsFolderTextBox
            // 
            songsFolderTextBox.Font = new Font("Yu Gothic UI", 11F);
            songsFolderTextBox.Location = new Point(12, 127);
            songsFolderTextBox.Name = "songsFolderTextBox";
            songsFolderTextBox.Size = new Size(250, 27);
            songsFolderTextBox.TabIndex = 15;
            songsFolderTextBox.TextChanged += SongsFolderTextBox_TextChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(664, 487);
            Controls.Add(openSongsFolderButton);
            Controls.Add(label6);
            Controls.Add(songsFolderTextBox);
            Controls.Add(label5);
            Controls.Add(removeButton);
            Controls.Add(openOsuFolderButton);
            Controls.Add(label4);
            Controls.Add(osuFolderTextBox);
            Controls.Add(generateProfile);
            Controls.Add(generateServer);
            Controls.Add(label3);
            Controls.Add(serverComboBox);
            Controls.Add(label2);
            Controls.Add(profileComboBox);
            Controls.Add(settingsPanels);
            Controls.Add(launchButton);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "osu! Bridge - Form Edition v1.0";
            FormClosing += MainForm_FormClosing;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button launchButton;
        private Panel settingsPanels;
        private ComboBox profileComboBox;
        private Label label2;
        private Label label3;
        private ComboBox serverComboBox;
        private Button generateServer;
        private Button generateProfile;
        private TextBox osuFolderTextBox;
        private Label label4;
        private Button openOsuFolderButton;
        private Button removeButton;
        private Label label5;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolMenu;
        private ToolStripMenuItem selectSkinMenu;
        private ToolStripMenuItem selectServerMenu;
        private Button openSongsFolderButton;
        private Label label6;
        private TextBox songsFolderTextBox;
    }
}
