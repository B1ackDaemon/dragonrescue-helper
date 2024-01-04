namespace dragonrescue_helper
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.groupBox_step1 = new System.Windows.Forms.GroupBox();
            this.button_export = new System.Windows.Forms.Button();
            this.groupBox_step1_account = new System.Windows.Forms.GroupBox();
            this.textBox_character = new System.Windows.Forms.TextBox();
            this.textBox_login = new System.Windows.Forms.TextBox();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.label_password = new System.Windows.Forms.Label();
            this.label_login = new System.Windows.Forms.Label();
            this.label_character = new System.Windows.Forms.Label();
            this.groupBox_step1_server = new System.Windows.Forms.GroupBox();
            this.radioButton_edge = new System.Windows.Forms.RadioButton();
            this.radioButton_sodoff = new System.Windows.Forms.RadioButton();
            this.groupBox_step1_mode = new System.Windows.Forms.GroupBox();
            this.radioButton_export = new System.Windows.Forms.RadioButton();
            this.radioButton_import = new System.Windows.Forms.RadioButton();
            this.richTextBox_log = new System.Windows.Forms.RichTextBox();
            this.groupBox_step2 = new System.Windows.Forms.GroupBox();
            this.button_import = new System.Windows.Forms.Button();
            this.groupBox_xml = new System.Windows.Forms.GroupBox();
            this.comboBox_xml = new System.Windows.Forms.ComboBox();
            this.groupBox_step2_mode = new System.Windows.Forms.GroupBox();
            this.radioButton_viking = new System.Windows.Forms.RadioButton();
            this.radioButton_inventory = new System.Windows.Forms.RadioButton();
            this.radioButton_dragons = new System.Windows.Forms.RadioButton();
            this.label_log = new System.Windows.Forms.Label();
            this.checkBox_advanced = new System.Windows.Forms.CheckBox();
            this.label_about = new System.Windows.Forms.Label();
            this.groupBox_step1.SuspendLayout();
            this.groupBox_step1_account.SuspendLayout();
            this.groupBox_step1_server.SuspendLayout();
            this.groupBox_step1_mode.SuspendLayout();
            this.groupBox_step2.SuspendLayout();
            this.groupBox_xml.SuspendLayout();
            this.groupBox_step2_mode.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_step1
            // 
            this.groupBox_step1.Controls.Add(this.button_export);
            this.groupBox_step1.Controls.Add(this.groupBox_step1_account);
            this.groupBox_step1.Controls.Add(this.groupBox_step1_server);
            this.groupBox_step1.Controls.Add(this.groupBox_step1_mode);
            this.groupBox_step1.Location = new System.Drawing.Point(15, 12);
            this.groupBox_step1.Name = "groupBox_step1";
            this.groupBox_step1.Size = new System.Drawing.Size(233, 342);
            this.groupBox_step1.TabIndex = 0;
            this.groupBox_step1.TabStop = false;
            this.groupBox_step1.Text = "Step 1";
            // 
            // button_export
            // 
            this.button_export.Location = new System.Drawing.Point(73, 298);
            this.button_export.Name = "button_export";
            this.button_export.Size = new System.Drawing.Size(75, 23);
            this.button_export.TabIndex = 3;
            this.button_export.Text = "Export!";
            this.button_export.UseVisualStyleBackColor = true;
            this.button_export.Click += new System.EventHandler(this.button_export_Click);
            // 
            // groupBox_step1_account
            // 
            this.groupBox_step1_account.Controls.Add(this.textBox_character);
            this.groupBox_step1_account.Controls.Add(this.textBox_login);
            this.groupBox_step1_account.Controls.Add(this.textBox_password);
            this.groupBox_step1_account.Controls.Add(this.label_password);
            this.groupBox_step1_account.Controls.Add(this.label_login);
            this.groupBox_step1_account.Controls.Add(this.label_character);
            this.groupBox_step1_account.Location = new System.Drawing.Point(16, 132);
            this.groupBox_step1_account.Name = "groupBox_step1_account";
            this.groupBox_step1_account.Size = new System.Drawing.Size(200, 146);
            this.groupBox_step1_account.TabIndex = 2;
            this.groupBox_step1_account.TabStop = false;
            this.groupBox_step1_account.Text = "My account details:";
            // 
            // textBox_character
            // 
            this.textBox_character.Location = new System.Drawing.Point(6, 113);
            this.textBox_character.Name = "textBox_character";
            this.textBox_character.Size = new System.Drawing.Size(100, 20);
            this.textBox_character.TabIndex = 7;
            // 
            // textBox_login
            // 
            this.textBox_login.Location = new System.Drawing.Point(6, 35);
            this.textBox_login.Name = "textBox_login";
            this.textBox_login.Size = new System.Drawing.Size(100, 20);
            this.textBox_login.TabIndex = 5;
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(6, 74);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.PasswordChar = '*';
            this.textBox_password.Size = new System.Drawing.Size(100, 20);
            this.textBox_password.TabIndex = 6;
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Location = new System.Drawing.Point(3, 58);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(96, 13);
            this.label_password.TabIndex = 3;
            this.label_password.Text = "(Server) Password:";
            // 
            // label_login
            // 
            this.label_login.AutoSize = true;
            this.label_login.Location = new System.Drawing.Point(3, 19);
            this.label_login.Name = "label_login";
            this.label_login.Size = new System.Drawing.Size(76, 13);
            this.label_login.TabIndex = 0;
            this.label_login.Text = "(Server) Login:";
            // 
            // label_character
            // 
            this.label_character.AutoSize = true;
            this.label_character.Location = new System.Drawing.Point(3, 97);
            this.label_character.Name = "label_character";
            this.label_character.Size = new System.Drawing.Size(108, 13);
            this.label_character.TabIndex = 4;
            this.label_character.Text = "(Server) Viking name:";
            // 
            // groupBox_step1_server
            // 
            this.groupBox_step1_server.Controls.Add(this.radioButton_edge);
            this.groupBox_step1_server.Controls.Add(this.radioButton_sodoff);
            this.groupBox_step1_server.Location = new System.Drawing.Point(16, 83);
            this.groupBox_step1_server.Name = "groupBox_step1_server";
            this.groupBox_step1_server.Size = new System.Drawing.Size(200, 42);
            this.groupBox_step1_server.TabIndex = 1;
            this.groupBox_step1_server.TabStop = false;
            this.groupBox_step1_server.Text = "I\'m playing on server:";
            // 
            // radioButton_edge
            // 
            this.radioButton_edge.AutoSize = true;
            this.radioButton_edge.Location = new System.Drawing.Point(97, 19);
            this.radioButton_edge.Name = "radioButton_edge";
            this.radioButton_edge.Size = new System.Drawing.Size(86, 17);
            this.radioButton_edge.TabIndex = 5;
            this.radioButton_edge.TabStop = true;
            this.radioButton_edge.Text = "Project Edge";
            this.radioButton_edge.UseVisualStyleBackColor = true;
            this.radioButton_edge.CheckedChanged += new System.EventHandler(this.radioButton_edge_CheckedChanged);
            // 
            // radioButton_sodoff
            // 
            this.radioButton_sodoff.AutoSize = true;
            this.radioButton_sodoff.Location = new System.Drawing.Point(6, 19);
            this.radioButton_sodoff.Name = "radioButton_sodoff";
            this.radioButton_sodoff.Size = new System.Drawing.Size(60, 17);
            this.radioButton_sodoff.TabIndex = 4;
            this.radioButton_sodoff.TabStop = true;
            this.radioButton_sodoff.Text = "SoDOff";
            this.radioButton_sodoff.UseVisualStyleBackColor = true;
            this.radioButton_sodoff.CheckedChanged += new System.EventHandler(this.radioButton_sodoff_CheckedChanged);
            // 
            // groupBox_step1_mode
            // 
            this.groupBox_step1_mode.Controls.Add(this.radioButton_export);
            this.groupBox_step1_mode.Controls.Add(this.radioButton_import);
            this.groupBox_step1_mode.Location = new System.Drawing.Point(16, 19);
            this.groupBox_step1_mode.Name = "groupBox_step1_mode";
            this.groupBox_step1_mode.Size = new System.Drawing.Size(200, 48);
            this.groupBox_step1_mode.TabIndex = 0;
            this.groupBox_step1_mode.TabStop = false;
            this.groupBox_step1_mode.Text = "I want to:";
            // 
            // radioButton_export
            // 
            this.radioButton_export.AutoSize = true;
            this.radioButton_export.Location = new System.Drawing.Point(97, 19);
            this.radioButton_export.Name = "radioButton_export";
            this.radioButton_export.Size = new System.Drawing.Size(55, 17);
            this.radioButton_export.TabIndex = 2;
            this.radioButton_export.TabStop = true;
            this.radioButton_export.Text = "Export";
            this.radioButton_export.UseVisualStyleBackColor = true;
            this.radioButton_export.CheckedChanged += new System.EventHandler(this.radioButton_export_CheckedChanged);
            // 
            // radioButton_import
            // 
            this.radioButton_import.AutoSize = true;
            this.radioButton_import.Location = new System.Drawing.Point(6, 19);
            this.radioButton_import.Name = "radioButton_import";
            this.radioButton_import.Size = new System.Drawing.Size(54, 17);
            this.radioButton_import.TabIndex = 1;
            this.radioButton_import.TabStop = true;
            this.radioButton_import.Text = "Import";
            this.radioButton_import.UseVisualStyleBackColor = true;
            this.radioButton_import.CheckedChanged += new System.EventHandler(this.radioButton_import_CheckedChanged);
            // 
            // richTextBox_log
            // 
            this.richTextBox_log.Location = new System.Drawing.Point(271, 258);
            this.richTextBox_log.Name = "richTextBox_log";
            this.richTextBox_log.Size = new System.Drawing.Size(294, 96);
            this.richTextBox_log.TabIndex = 1;
            this.richTextBox_log.Text = "";
            this.richTextBox_log.TextChanged += new System.EventHandler(this.richTextBox_log_TextChanged);
            // 
            // groupBox_step2
            // 
            this.groupBox_step2.Controls.Add(this.button_import);
            this.groupBox_step2.Controls.Add(this.groupBox_xml);
            this.groupBox_step2.Controls.Add(this.groupBox_step2_mode);
            this.groupBox_step2.Location = new System.Drawing.Point(271, 12);
            this.groupBox_step2.Name = "groupBox_step2";
            this.groupBox_step2.Size = new System.Drawing.Size(294, 203);
            this.groupBox_step2.TabIndex = 2;
            this.groupBox_step2.TabStop = false;
            this.groupBox_step2.Text = "Step 2";
            // 
            // button_import
            // 
            this.button_import.Location = new System.Drawing.Point(109, 157);
            this.button_import.Name = "button_import";
            this.button_import.Size = new System.Drawing.Size(75, 23);
            this.button_import.TabIndex = 2;
            this.button_import.Text = "Import!";
            this.button_import.UseVisualStyleBackColor = true;
            this.button_import.Click += new System.EventHandler(this.button_import_Click);
            // 
            // groupBox_xml
            // 
            this.groupBox_xml.Controls.Add(this.comboBox_xml);
            this.groupBox_xml.Location = new System.Drawing.Point(23, 83);
            this.groupBox_xml.Name = "groupBox_xml";
            this.groupBox_xml.Size = new System.Drawing.Size(259, 53);
            this.groupBox_xml.TabIndex = 1;
            this.groupBox_xml.TabStop = false;
            this.groupBox_xml.Text = "Suitable xml file to import";
            // 
            // comboBox_xml
            // 
            this.comboBox_xml.FormattingEnabled = true;
            this.comboBox_xml.Location = new System.Drawing.Point(7, 20);
            this.comboBox_xml.Name = "comboBox_xml";
            this.comboBox_xml.Size = new System.Drawing.Size(121, 21);
            this.comboBox_xml.TabIndex = 0;
            this.comboBox_xml.SelectedIndexChanged += new System.EventHandler(this.comboBox_xml_SelectedIndexChanged);
            // 
            // groupBox_step2_mode
            // 
            this.groupBox_step2_mode.Controls.Add(this.radioButton_viking);
            this.groupBox_step2_mode.Controls.Add(this.radioButton_inventory);
            this.groupBox_step2_mode.Controls.Add(this.radioButton_dragons);
            this.groupBox_step2_mode.Location = new System.Drawing.Point(16, 19);
            this.groupBox_step2_mode.Name = "groupBox_step2_mode";
            this.groupBox_step2_mode.Size = new System.Drawing.Size(266, 48);
            this.groupBox_step2_mode.TabIndex = 0;
            this.groupBox_step2_mode.TabStop = false;
            this.groupBox_step2_mode.Text = "I want to import:";
            // 
            // radioButton_viking
            // 
            this.radioButton_viking.AutoSize = true;
            this.radioButton_viking.Location = new System.Drawing.Point(188, 19);
            this.radioButton_viking.Name = "radioButton_viking";
            this.radioButton_viking.Size = new System.Drawing.Size(54, 17);
            this.radioButton_viking.TabIndex = 2;
            this.radioButton_viking.TabStop = true;
            this.radioButton_viking.Text = "Viking";
            this.radioButton_viking.UseVisualStyleBackColor = true;
            this.radioButton_viking.CheckedChanged += new System.EventHandler(this.radioButton_viking_CheckedChanged);
            // 
            // radioButton_inventory
            // 
            this.radioButton_inventory.AutoSize = true;
            this.radioButton_inventory.Location = new System.Drawing.Point(96, 19);
            this.radioButton_inventory.Name = "radioButton_inventory";
            this.radioButton_inventory.Size = new System.Drawing.Size(69, 17);
            this.radioButton_inventory.TabIndex = 1;
            this.radioButton_inventory.TabStop = true;
            this.radioButton_inventory.Text = "Inventory";
            this.radioButton_inventory.UseVisualStyleBackColor = true;
            this.radioButton_inventory.CheckedChanged += new System.EventHandler(this.radioButton_inventory_CheckedChanged);
            // 
            // radioButton_dragons
            // 
            this.radioButton_dragons.AutoSize = true;
            this.radioButton_dragons.Location = new System.Drawing.Point(7, 19);
            this.radioButton_dragons.Name = "radioButton_dragons";
            this.radioButton_dragons.Size = new System.Drawing.Size(65, 17);
            this.radioButton_dragons.TabIndex = 0;
            this.radioButton_dragons.TabStop = true;
            this.radioButton_dragons.Text = "Dragons";
            this.radioButton_dragons.UseVisualStyleBackColor = true;
            this.radioButton_dragons.CheckedChanged += new System.EventHandler(this.radioButton_dragons_CheckedChanged);
            // 
            // label_log
            // 
            this.label_log.AutoSize = true;
            this.label_log.Location = new System.Drawing.Point(271, 231);
            this.label_log.Name = "label_log";
            this.label_log.Size = new System.Drawing.Size(59, 13);
            this.label_log.TabIndex = 3;
            this.label_log.Text = "Debug log:";
            // 
            // checkBox_advanced
            // 
            this.checkBox_advanced.AutoSize = true;
            this.checkBox_advanced.Location = new System.Drawing.Point(461, 230);
            this.checkBox_advanced.Name = "checkBox_advanced";
            this.checkBox_advanced.Size = new System.Drawing.Size(104, 17);
            this.checkBox_advanced.TabIndex = 4;
            this.checkBox_advanced.Text = "Advanced mode";
            this.checkBox_advanced.UseVisualStyleBackColor = true;
            this.checkBox_advanced.CheckedChanged += new System.EventHandler(this.checkBox_advanced_CheckedChanged);
            // 
            // label_about
            // 
            this.label_about.AutoSize = true;
            this.label_about.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_about.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_about.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label_about.Location = new System.Drawing.Point(172, 373);
            this.label_about.Name = "label_about";
            this.label_about.Size = new System.Drawing.Size(247, 13);
            this.label_about.TabIndex = 5;
            this.label_about.Text = "Dragonrescue Helper, GUI for dragonrescue-import";
            this.label_about.Click += new System.EventHandler(this.label_about_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 399);
            this.Controls.Add(this.label_about);
            this.Controls.Add(this.checkBox_advanced);
            this.Controls.Add(this.label_log);
            this.Controls.Add(this.groupBox_step2);
            this.Controls.Add(this.richTextBox_log);
            this.Controls.Add(this.groupBox_step1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Dragonrescue Helper";
            this.groupBox_step1.ResumeLayout(false);
            this.groupBox_step1_account.ResumeLayout(false);
            this.groupBox_step1_account.PerformLayout();
            this.groupBox_step1_server.ResumeLayout(false);
            this.groupBox_step1_server.PerformLayout();
            this.groupBox_step1_mode.ResumeLayout(false);
            this.groupBox_step1_mode.PerformLayout();
            this.groupBox_step2.ResumeLayout(false);
            this.groupBox_xml.ResumeLayout(false);
            this.groupBox_step2_mode.ResumeLayout(false);
            this.groupBox_step2_mode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_step1;
        private System.Windows.Forms.RadioButton radioButton_edge;
        private System.Windows.Forms.RadioButton radioButton_sodoff;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.RadioButton radioButton_export;
        private System.Windows.Forms.RadioButton radioButton_import;
        private System.Windows.Forms.Label label_login;
        private System.Windows.Forms.GroupBox groupBox_step1_server;
        private System.Windows.Forms.GroupBox groupBox_step1_mode;
        private System.Windows.Forms.GroupBox groupBox_step1_account;
        private System.Windows.Forms.Label label_character;
        private System.Windows.Forms.TextBox textBox_login;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.TextBox textBox_character;
        private System.Windows.Forms.RichTextBox richTextBox_log;
        private System.Windows.Forms.Button button_export;
        private System.Windows.Forms.GroupBox groupBox_step2;
        private System.Windows.Forms.GroupBox groupBox_step2_mode;
        private System.Windows.Forms.RadioButton radioButton_viking;
        private System.Windows.Forms.RadioButton radioButton_inventory;
        private System.Windows.Forms.RadioButton radioButton_dragons;
        private System.Windows.Forms.GroupBox groupBox_xml;
        private System.Windows.Forms.ComboBox comboBox_xml;
        private System.Windows.Forms.Button button_import;
        private System.Windows.Forms.Label label_log;
        private System.Windows.Forms.CheckBox checkBox_advanced;
        private System.Windows.Forms.Label label_about;
    }
}

