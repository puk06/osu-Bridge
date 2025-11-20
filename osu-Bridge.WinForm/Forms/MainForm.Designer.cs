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
            showSongsFolderPath = new CheckBox();
            showOsuFolderPath = new CheckBox();
            showOsuLazerFolderPath = new CheckBox();
            label3 = new Label();
            osuLazerFolderTextBox = new TextBox();
            openOsuLazerFolderButton = new Button();
            lazerModeCheckbox = new CheckBox();
            label5 = new Label();
            label4 = new Label();
            osuFolderTextBox = new TextBox();
            openSongsFolderButton = new Button();
            serverComboBox = new ComboBox();
            openOsuFolderButton = new Button();
            label6 = new Label();
            songsFolderTextBox = new TextBox();
            profileComboBox = new ComboBox();
            label2 = new Label();
            menuStrip1 = new MenuStrip();
            toolMenu = new ToolStripMenuItem();
            selectSkinMenu = new ToolStripMenuItem();
            selectServerMenu = new ToolStripMenuItem();
            serverMenu = new ToolStripMenuItem();
            generateServerButton = new ToolStripMenuItem();
            duplicateServerMenu = new ToolStripMenuItem();
            removeServerMenu = new ToolStripMenuItem();
            profileMenu = new ToolStripMenuItem();
            generateProfileButton = new ToolStripMenuItem();
            duplicateProfileMenu = new ToolStripMenuItem();
            removeProfileMenu = new ToolStripMenuItem();
            label1 = new Label();
            profileSettingsPanel = new Panel();
            updateCheck = new ToolStripMenuItem();
            settingsPanels.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // launchButton
            // 
            launchButton.Font = new Font("Yu Gothic UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 128);
            launchButton.Location = new Point(768, 503);
            launchButton.Name = "launchButton";
            launchButton.Size = new Size(299, 58);
            launchButton.TabIndex = 0;
            launchButton.Text = "Launch";
            launchButton.UseVisualStyleBackColor = true;
            launchButton.Click += LaunchButton_Click;
            // 
            // settingsPanels
            // 
            settingsPanels.AutoScroll = true;
            settingsPanels.BackColor = SystemColors.Control;
            settingsPanels.BorderStyle = BorderStyle.FixedSingle;
            settingsPanels.Controls.Add(showSongsFolderPath);
            settingsPanels.Controls.Add(showOsuFolderPath);
            settingsPanels.Controls.Add(showOsuLazerFolderPath);
            settingsPanels.Controls.Add(label3);
            settingsPanels.Controls.Add(osuLazerFolderTextBox);
            settingsPanels.Controls.Add(openOsuLazerFolderButton);
            settingsPanels.Controls.Add(lazerModeCheckbox);
            settingsPanels.Controls.Add(label5);
            settingsPanels.Controls.Add(label4);
            settingsPanels.Controls.Add(osuFolderTextBox);
            settingsPanels.Controls.Add(openSongsFolderButton);
            settingsPanels.Controls.Add(serverComboBox);
            settingsPanels.Controls.Add(openOsuFolderButton);
            settingsPanels.Controls.Add(label6);
            settingsPanels.Controls.Add(songsFolderTextBox);
            settingsPanels.Dock = DockStyle.Left;
            settingsPanels.Location = new Point(0, 24);
            settingsPanels.Name = "settingsPanels";
            settingsPanels.Size = new Size(271, 549);
            settingsPanels.TabIndex = 1;
            // 
            // showSongsFolderPath
            // 
            showSongsFolderPath.AutoSize = true;
            showSongsFolderPath.Font = new Font("Yu Gothic UI", 10F);
            showSongsFolderPath.Location = new Point(164, 142);
            showSongsFolderPath.Name = "showSongsFolderPath";
            showSongsFolderPath.Size = new Size(89, 23);
            showSongsFolderPath.TabIndex = 27;
            showSongsFolderPath.Text = "パスの表示";
            showSongsFolderPath.UseVisualStyleBackColor = true;
            showSongsFolderPath.CheckedChanged += ShowSongsFolderPath_CheckedChanged;
            // 
            // showOsuFolderPath
            // 
            showOsuFolderPath.AutoSize = true;
            showOsuFolderPath.Font = new Font("Yu Gothic UI", 10F);
            showOsuFolderPath.Location = new Point(164, 8);
            showOsuFolderPath.Name = "showOsuFolderPath";
            showOsuFolderPath.Size = new Size(89, 23);
            showOsuFolderPath.TabIndex = 26;
            showOsuFolderPath.Text = "パスの表示";
            showOsuFolderPath.UseVisualStyleBackColor = true;
            showOsuFolderPath.CheckedChanged += ShowOsuFolderPath_CheckedChanged;
            // 
            // showOsuLazerFolderPath
            // 
            showOsuLazerFolderPath.AutoSize = true;
            showOsuLazerFolderPath.Font = new Font("Yu Gothic UI", 10F);
            showOsuLazerFolderPath.Location = new Point(162, 71);
            showOsuLazerFolderPath.Name = "showOsuLazerFolderPath";
            showOsuLazerFolderPath.Size = new Size(89, 23);
            showOsuLazerFolderPath.TabIndex = 25;
            showOsuLazerFolderPath.Text = "パスの表示";
            showOsuLazerFolderPath.UseVisualStyleBackColor = true;
            showOsuLazerFolderPath.CheckedChanged += ShowOsuLazerFolderPath_CheckedChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            label3.Location = new Point(9, 73);
            label3.Name = "label3";
            label3.Size = new Size(147, 21);
            label3.TabIndex = 23;
            label3.Text = "osu! Lazer フォルダー";
            // 
            // osuLazerFolderTextBox
            // 
            osuLazerFolderTextBox.Font = new Font("Yu Gothic UI", 11F);
            osuLazerFolderTextBox.Location = new Point(9, 97);
            osuLazerFolderTextBox.Name = "osuLazerFolderTextBox";
            osuLazerFolderTextBox.PasswordChar = '*';
            osuLazerFolderTextBox.Size = new Size(170, 27);
            osuLazerFolderTextBox.TabIndex = 22;
            osuLazerFolderTextBox.TextChanged += OsuLazerFolderTextBox_TextChanged;
            // 
            // openOsuLazerFolderButton
            // 
            openOsuLazerFolderButton.Location = new Point(185, 97);
            openOsuLazerFolderButton.Name = "openOsuLazerFolderButton";
            openOsuLazerFolderButton.Size = new Size(68, 27);
            openOsuLazerFolderButton.TabIndex = 24;
            openOsuLazerFolderButton.Text = "開く";
            openOsuLazerFolderButton.UseVisualStyleBackColor = true;
            openOsuLazerFolderButton.Click += OpenOsuLazerFolderButton_Click;
            // 
            // lazerModeCheckbox
            // 
            lazerModeCheckbox.AutoSize = true;
            lazerModeCheckbox.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lazerModeCheckbox.Location = new Point(11, 280);
            lazerModeCheckbox.Name = "lazerModeCheckbox";
            lazerModeCheckbox.Size = new Size(115, 29);
            lazerModeCheckbox.TabIndex = 21;
            lazerModeCheckbox.Text = "Lazerモード";
            lazerModeCheckbox.UseVisualStyleBackColor = true;
            lazerModeCheckbox.CheckedChanged += LazerModeCheckbox_CheckedChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            label5.Location = new Point(9, 222);
            label5.Name = "label5";
            label5.Size = new Size(58, 21);
            label5.TabIndex = 18;
            label5.Text = "サーバー";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            label4.Location = new Point(9, 10);
            label4.Name = "label4";
            label4.Size = new Size(105, 21);
            label4.TabIndex = 10;
            label4.Text = "osu! フォルダー";
            // 
            // osuFolderTextBox
            // 
            osuFolderTextBox.Font = new Font("Yu Gothic UI", 11F);
            osuFolderTextBox.Location = new Point(9, 34);
            osuFolderTextBox.Name = "osuFolderTextBox";
            osuFolderTextBox.PasswordChar = '*';
            osuFolderTextBox.Size = new Size(170, 27);
            osuFolderTextBox.TabIndex = 9;
            osuFolderTextBox.TextChanged += OsuFolderTextBox_TextChanged;
            // 
            // openSongsFolderButton
            // 
            openSongsFolderButton.Location = new Point(185, 169);
            openSongsFolderButton.Name = "openSongsFolderButton";
            openSongsFolderButton.Size = new Size(68, 27);
            openSongsFolderButton.TabIndex = 17;
            openSongsFolderButton.Text = "開く";
            openSongsFolderButton.UseVisualStyleBackColor = true;
            openSongsFolderButton.Click += OpenSongsFolderButton_Click;
            // 
            // serverComboBox
            // 
            serverComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            serverComboBox.Font = new Font("Yu Gothic UI", 11F);
            serverComboBox.FormattingEnabled = true;
            serverComboBox.Location = new Point(9, 246);
            serverComboBox.Name = "serverComboBox";
            serverComboBox.Size = new Size(233, 28);
            serverComboBox.TabIndex = 5;
            serverComboBox.SelectedIndexChanged += ServerComboBox_SelectedIndexChanged;
            // 
            // openOsuFolderButton
            // 
            openOsuFolderButton.Location = new Point(185, 34);
            openOsuFolderButton.Name = "openOsuFolderButton";
            openOsuFolderButton.Size = new Size(68, 27);
            openOsuFolderButton.TabIndex = 11;
            openOsuFolderButton.Text = "開く";
            openOsuFolderButton.UseVisualStyleBackColor = true;
            openOsuFolderButton.Click += OpenOsuFolderButton_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            label6.Location = new Point(9, 144);
            label6.Name = "label6";
            label6.Size = new Size(119, 21);
            label6.TabIndex = 16;
            label6.Text = "Songs フォルダー";
            // 
            // songsFolderTextBox
            // 
            songsFolderTextBox.Font = new Font("Yu Gothic UI", 11F);
            songsFolderTextBox.Location = new Point(9, 168);
            songsFolderTextBox.Name = "songsFolderTextBox";
            songsFolderTextBox.PasswordChar = '*';
            songsFolderTextBox.Size = new Size(170, 27);
            songsFolderTextBox.TabIndex = 15;
            songsFolderTextBox.TextChanged += SongsFolderTextBox_TextChanged;
            // 
            // profileComboBox
            // 
            profileComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            profileComboBox.Font = new Font("Yu Gothic UI", 12F);
            profileComboBox.FormattingEnabled = true;
            profileComboBox.Location = new Point(384, 521);
            profileComboBox.Name = "profileComboBox";
            profileComboBox.Size = new Size(314, 29);
            profileComboBox.TabIndex = 3;
            profileComboBox.SelectedIndexChanged += ProfileComboBox_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic UI Semibold", 15F, FontStyle.Bold);
            label2.Location = new Point(277, 518);
            label2.Name = "label2";
            label2.Size = new Size(101, 28);
            label2.TabIndex = 4;
            label2.Text = "プロファイル";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolMenu, serverMenu, profileMenu, updateCheck });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1079, 24);
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
            // serverMenu
            // 
            serverMenu.DropDownItems.AddRange(new ToolStripItem[] { generateServerButton, duplicateServerMenu, removeServerMenu });
            serverMenu.Name = "serverMenu";
            serverMenu.Size = new Size(55, 20);
            serverMenu.Text = "サーバー";
            // 
            // generateServerButton
            // 
            generateServerButton.Name = "generateServerButton";
            generateServerButton.Size = new Size(122, 22);
            generateServerButton.Text = "新規作成";
            generateServerButton.Click += GenerateServerButton_Click;
            // 
            // duplicateServerMenu
            // 
            duplicateServerMenu.Name = "duplicateServerMenu";
            duplicateServerMenu.Size = new Size(122, 22);
            duplicateServerMenu.Text = "複製";
            // 
            // removeServerMenu
            // 
            removeServerMenu.Name = "removeServerMenu";
            removeServerMenu.Size = new Size(122, 22);
            removeServerMenu.Text = "削除";
            // 
            // profileMenu
            // 
            profileMenu.DropDownItems.AddRange(new ToolStripItem[] { generateProfileButton, duplicateProfileMenu, removeProfileMenu });
            profileMenu.Name = "profileMenu";
            profileMenu.Size = new Size(71, 20);
            profileMenu.Text = "プロファイル";
            // 
            // generateProfileButton
            // 
            generateProfileButton.Name = "generateProfileButton";
            generateProfileButton.Size = new Size(122, 22);
            generateProfileButton.Text = "新規作成";
            generateProfileButton.Click += GenerateProfileButton_Click;
            // 
            // duplicateProfileMenu
            // 
            duplicateProfileMenu.Name = "duplicateProfileMenu";
            duplicateProfileMenu.Size = new Size(122, 22);
            duplicateProfileMenu.Text = "複製";
            // 
            // removeProfileMenu
            // 
            removeProfileMenu.Name = "removeProfileMenu";
            removeProfileMenu.Size = new Size(122, 22);
            removeProfileMenu.Text = "削除";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI Semibold", 13F, FontStyle.Bold);
            label1.Location = new Point(277, 24);
            label1.Name = "label1";
            label1.Size = new Size(243, 25);
            label1.TabIndex = 19;
            label1.Text = "サーバー / プロファイル編集パネル";
            // 
            // profileSettingsPanel
            // 
            profileSettingsPanel.AutoScroll = true;
            profileSettingsPanel.BackColor = SystemColors.Control;
            profileSettingsPanel.BorderStyle = BorderStyle.FixedSingle;
            profileSettingsPanel.Location = new Point(270, 52);
            profileSettingsPanel.Name = "profileSettingsPanel";
            profileSettingsPanel.Size = new Size(809, 445);
            profileSettingsPanel.TabIndex = 18;
            // 
            // updateCheck
            // 
            updateCheck.Name = "updateCheck";
            updateCheck.Size = new Size(102, 20);
            updateCheck.Text = "アップデートチェック";
            updateCheck.Click += UpdateCheck_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1079, 573);
            Controls.Add(label1);
            Controls.Add(profileSettingsPanel);
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
            Text = "osu! Bridge - WinForm Edition";
            FormClosing += MainForm_FormClosing;
            settingsPanels.ResumeLayout(false);
            settingsPanels.PerformLayout();
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
        private ComboBox serverComboBox;
        private TextBox osuFolderTextBox;
        private Label label4;
        private Button openOsuFolderButton;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolMenu;
        private ToolStripMenuItem selectSkinMenu;
        private ToolStripMenuItem selectServerMenu;
        private Button openSongsFolderButton;
        private Label label6;
        private TextBox songsFolderTextBox;
        private Label label1;
        private Panel profileSettingsPanel;
        private Label label5;
        private CheckBox lazerModeCheckbox;
        private Label label3;
        private TextBox osuLazerFolderTextBox;
        private Button openOsuLazerFolderButton;
        private CheckBox showOsuFolderPath;
        private CheckBox showOsuLazerFolderPath;
        private CheckBox showSongsFolderPath;
        private ToolStripMenuItem serverMenu;
        private ToolStripMenuItem generateServerButton;
        private ToolStripMenuItem duplicateServerMenu;
        private ToolStripMenuItem removeServerMenu;
        private ToolStripMenuItem profileMenu;
        private ToolStripMenuItem generateProfileButton;
        private ToolStripMenuItem duplicateProfileMenu;
        private ToolStripMenuItem removeProfileMenu;
        private ToolStripMenuItem updateCheck;
    }
}
