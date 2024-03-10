using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.IO.Compression;

namespace dragonrescue_helper
{
    public partial class Main : Form
    {
        //to properly pass path argements to dragonrescue-import
        const string quote = "\"";
        //mode, import or export
        string mode = "";
        //server, sodoff or edge
        string server = "";
        //current directory, where dragonrescue helper launched
        string current_dir = "";
        //path to dragonrescue-import
        string tool_path = "";
        //server userapi url, sodoff or edge
        string server_userapi = "";
        //server contentapi url, sodoff or edge
        string server_contentapi = "";
        //profile directory path, used for import
        string import_dir = "";
        //profile xml path, used for import
        string import_path = "";
        //export directory path, used for export
        string export_path = "";
        //import data - dragons, inventory or viking xml
        string import_data = "";
        //config path, used for override.ini
        string config_path = "";
        //log file path
        string log_path = "";
        //files list, used to locate dragonrescue-import, override.ini and profile xml files
        string[] files;

        public Main()
        {
            InitializeComponent();

            //writing to log
            richTextBox_log.Text += "Program started." + "\n";
            //storing current directory path
            current_dir = Directory.GetCurrentDirectory();
            //temporary setting profile directory path as current directory
            import_dir = current_dir + @"\";
            //temporary setting export directory path as current directory
            export_path = current_dir + @"\";
            //writing to log
            richTextBox_log.Text += "Program path: " + current_dir + "\n";
            //getting files list in current directory
            files = Directory.GetFiles(current_dir);

            //going through files list
            foreach (string file in files)
            {
                //looking for dragonrescue-import tool
                if (file.Contains("dragonrescue-import.exe") == true)
                {
                    //storing path to dragonrescue-import tool
                    tool_path = file;
                    //writing to log
                    richTextBox_log.Text += "Tool path: " + tool_path + "\n";
                }
                //looking for advanced config, override.ini
                else if (file.Contains("override.ini") == true)
                {
                    //storing override.ini path
                    config_path = file;
                    //setting advanced more checkbox
                    checkBox_advanced.Checked = true;
                }
                else if (file.Contains("log.txt") == true)
                {
                    //storing log.txt path
                    log_path = file;
                }
            }

            //if dragonrescue-import tool wasn't found
            if (tool_path == "")
            {
                //offer to download it
                DialogResult result = MessageBox.Show("Dragonrescue-import not found, do you want to download it?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //download zip from RPaciorek's github repo
                    WebClient Client = new WebClient();
                    Client.DownloadFile("https://github.com/SoDOff-Project/dragonrescue/releases/download/v0.6.0/dragonrescue-import--win-x86_0.6.0.zip", "dragonrescue-import--win-x86_0.6.0.zip");

                    //read zip contents
                    byte[] zip = ReadFile(Directory.GetCurrentDirectory() + @"\dragonrescue-import--win-x86_0.6.0.zip");
                    MemoryStream memzip = new MemoryStream();
                    memzip.Write(zip, 0, zip.Length);
                    ZipArchive archive = new ZipArchive(memzip);

                    //extract it in current directory
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        Stream unzippedEntryStream = entry.Open();
                        if (entry.FullName != "dragonrescue-import--win-x86_0.6.0/")
                        {
                            using (Stream file = File.Create(entry.FullName.Replace("dragonrescue-import--win-x86_0.6.0/", "")))
                            {
                                CopyStream(unzippedEntryStream, file);
                            }
                        }
                    }

                    //check files in current directory again and obtain path to dragonrescue-import tool
                    files = Directory.GetFiles(current_dir);
                    foreach (string file in files)
                    {
                        if (file.Contains("dragonrescue-import.exe") == true)
                        {
                            tool_path = file;
                            richTextBox_log.Text += "Tool path: " + tool_path + "\n";
                        }
                    }
                }
            }
        }

        private void radioButton_import_CheckedChanged(object sender, EventArgs e)
        {
            //if mode been set to import
            if (radioButton_import.Checked == true)
            {
                //write to log
                richTextBox_log.Text += "Mode set to: import." + "\n";
                mode = "import";

                //disable export button and hide it
                button_export.Enabled = false;
                button_export.Visible = false;
                //enable Step 2 interface for import
                groupBox_step2.Visible = true;

                //check, if advanced more enabled
                if (checkBox_advanced.Checked == true)
                {
                    //write override.ini
                    WriteOverride(config_path);
                }
            }
        }

        private void radioButton_export_CheckedChanged(object sender, EventArgs e)
        {
            //if mode been set to export
            if (radioButton_export.Checked == true)
            {
                //write to log
                richTextBox_log.Text += "Mode set to: export." + "\n";
                mode = "export";

                //enable export button and make it visible
                button_export.Enabled = true;
                button_export.Visible = true;
                //disable Step 2 interface for import
                groupBox_step2.Visible = false;

                //check, if advanced more enabled
                if (checkBox_advanced.Checked == true)
                {
                    //write override.ini
                    WriteOverride(config_path);
                }
            }
        }

        private void radioButton_sodoff_CheckedChanged(object sender, EventArgs e)
        {
            //if server been set to SoDOff
            if (radioButton_sodoff.Checked == true)
            {
                //write to log
                richTextBox_log.Text += "Server set to: SoDOff." + "\n";
                server = "sodoff";

                //set server urls to SoDOff
                server_userapi = "https://api.sodoff.spirtix.com/";
                server_contentapi = "https://api.sodoff.spirtix.com/";

                //set labels to SoDOff
                label_login.Text = "(SoDOff) Login:";
                label_password.Text = "(SoDOff) Password:";
                label_character.Text = "(SoDOff) Viking name:";

                //check, if advanced more enabled
                if (checkBox_advanced.Checked == true)
                {
                    //write override.ini
                    WriteOverride(config_path);
                }
            }
        }

        private void radioButton_edge_CheckedChanged(object sender, EventArgs e)
        {
            //if server been set to Project Edge
            if (radioButton_edge.Checked == true)
            {
                //write to log
                richTextBox_log.Text += "Server set to: Project Edge." + "\n";
                server = "edge";

                //set server urls to Project Edge
                server_userapi = "http://localhost:5321";
                server_contentapi = "http://localhost:5320";

                //set labels to Project Edge
                label_login.Text = "(Edge) Login:";
                label_password.Text = "(Edge) Password:";
                label_character.Text = "(Edge) Viking name:";

                //check, if advanced more enabled
                if (checkBox_advanced.Checked == true)
                {
                    //write override.ini
                    WriteOverride(config_path);
                }
            }
        }

        private void radioButton_guild_CheckedChanged(object sender, EventArgs e)
        {
            //if server been set to Riders Guild
            if (radioButton_guild.Checked == true)
            {
                //write to log
                richTextBox_log.Text += "Server set to: Riders Guild." + "\n";
                server = "guild";

                //set server urls to Riders Guild
                server_userapi = "https://api.ridersguild.org/";
                server_contentapi = "https://api.ridersguild.org/";

                //set labels to Riders Guild
                label_login.Text = "(Guild) Login:";
                label_password.Text = "(Guild) Password:";
                label_character.Text = "(Guild) Viking name:";

                //check, if advanced more enabled
                if (checkBox_advanced.Checked == true)
                {
                    //write override.ini
                    WriteOverride(config_path);
                }
            }
        }

        private void button_export_Click(object sender, EventArgs e)
        {
            uint server_selected = 0;
            uint login_entered = 0;
            //checking, if server urls been set
            if (server_contentapi != "" && server_userapi != "")
            {
                server_selected = 1;
                richTextBox_log.Text += "[Export] server urls checks passed." + "\n";
            }
            //checking, if account details textboxes been filled
            if (textBox_login.Text != "" && textBox_password.Text != "" && textBox_character.Text != "")
            {
                login_entered = 1;
                richTextBox_log.Text += "[Export] account details checks passed." + "\n";
            }
            //checking, if dragonrescue-import tool been set, and server/login details filled
            if (tool_path != "" && server_selected == 1 && login_entered == 1)
            {
                richTextBox_log.Text += "[Export] dragonrescue-import tool check passed." + "\n";
                //making directory name for export, account name + viking name
                string export_dir = textBox_login.Text + "_" + textBox_character.Text;
                richTextBox_log.Text += "[Export] generating new directory for storing profile: " + export_dir + "\n";
                //checking, if advanced more is disabled
                if (checkBox_advanced.Checked == false)
                {
                    //in that case we set export directory in current directory
                    export_path = current_dir + @"\";
                    richTextBox_log.Text += "[Export] advanced mode is disabled, using current directory path: " + current_dir + "\n";
                }
                //checking, if directory name we use for export exists (in case, if we did export before)
                if (Directory.Exists(export_path + export_dir) == true)
                {
                    richTextBox_log.Text += "[Export] generated directory name exists, attempting to generate new one " + "\n";
                    for (int i = 0; i < 99; i++)
                    {
                        //adding digit to directory name
                        string new_dir = export_dir + "_" + i.ToString();
                        richTextBox_log.Text += "[Export] generating new directory name: " + new_dir + "\n";
                        //and checking it again
                        if (Directory.Exists(export_path + new_dir) == false)
                        {
                            //creating new directory
                            Directory.CreateDirectory(export_path + new_dir);
                            //setting export directory path
                            export_dir = quote + export_path + new_dir + quote;
                            richTextBox_log.Text += "[Export] created new directory: " + export_dir + "\n";
                            i = 99;
                        }
                    }
                }
                //checking, if directory name we use for export not exist (for 1st time export)
                else if (Directory.Exists(export_path + export_dir) == false)
                {
                    //creating new directory
                    Directory.CreateDirectory(export_path + export_dir);
                    //setting export directory path
                    export_dir = quote + export_path + export_dir + quote;
                    richTextBox_log.Text += "[Export] created new directory: " + export_dir + "\n";
                }

                //creating process for dragonrescue-import tool
                Process exportProcess = new Process();
                richTextBox_log.Text += "[Export] creating new process: " + "\n";
                //setting path to tool
                exportProcess.StartInfo.FileName = tool_path;
                richTextBox_log.Text += "[Export] tool path: " + tool_path + "\n";
                //passing argumnents for export
                exportProcess.StartInfo.Arguments = "--userApiUrl=" + server_userapi + " --contentApiUrl=" + server_contentapi + " --username=" + textBox_login.Text + " --password=" + textBox_password.Text + " --viking=" + textBox_character.Text + " export --path " + export_dir;
                richTextBox_log.Text += "[Export] using arguments: " + "--userApiUrl=" + server_userapi + " --contentApiUrl=" + server_contentapi + " --username=[REDACTED] --password=[REDACTED] --viking=[REDACTED] export --path " + export_dir + "\n";
                //starting process
                exportProcess.Start();
                richTextBox_log.Text += "[Export] starting dragonrescue-import process." + "\n";
                //wait, until it finishes
                exportProcess.WaitForExit();
                richTextBox_log.Text += "[Export] dragonrescue-import process finished it's job." + "\n";
            }
            //show error message, in case if something is not set
            else if (tool_path != "" && server_selected == 0 || login_entered == 0)
            {
                string error_msg = "Following errors been encountered:" + "\n";
                richTextBox_log.Text += "[Export] failed to pass fool-proof checks." + "\n";
                if (server_selected == 0)
                {
                    error_msg += "-Server not selected!" + "\n";
                    richTextBox_log.Text += "[Export] server urls checks failed." + "\n";
                }
                if (login_entered == 0)
                {
                    error_msg += "-Login details not filled!" + "\n";
                    richTextBox_log.Text += "[Export] account details checks failed." + "\n";
                }
                MessageBox.Show(error_msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButton_dragons_CheckedChanged(object sender, EventArgs e)
        {
            //if import data was set to Dragons
            if (radioButton_dragons.Checked == true)
            {
                //write to log
                richTextBox_log.Text += "Import mode set to: dragons." + "\n";

                //set import data to dragons
                import_data = "dragons";

                //clean elements in comboBox
                comboBox_xml.Items.Clear();
                comboBox_xml.Text = "";

                //check, if advanced mode is disabled
                if (checkBox_advanced.Checked == false)
                {
                    //then use current directory for profile import path
                    import_dir = current_dir + @"\";
                }

                //getting files list, including subdirectories
                files = Directory.GetFiles(import_dir, "*.*", SearchOption.AllDirectories);
                //going through files list
                foreach (string file in files)
                {
                    //looking for dragons xml file
                    if (file.Contains("GetAllActivePetsByuserId.xml") == true)
                    {
                        //adding it's path to comboBox
                        comboBox_xml.Items.Add(file);
                        //writing to log
                        richTextBox_log.Text += file + " file added." + "\n";
                    }
                }

                //check, if advanced more enabled
                if (checkBox_advanced.Checked == true)
                {
                    //write override.ini
                    WriteOverride(config_path);
                }
            }
        }

        private void radioButton_inventory_CheckedChanged(object sender, EventArgs e)
        {
            //if import data was set to Inventory
            if (radioButton_inventory.Checked == true)
            {
                //writing to log
                richTextBox_log.Text += "Import mode set to: inventory." + "\n";

                //set import data to inventory
                import_data = "inventory";

                //clean elements in comboBox
                comboBox_xml.Items.Clear();
                comboBox_xml.Text = "";

                //check, if advanced mode is disabled
                if (checkBox_advanced.Checked == false)
                {
                    //then use current directory for profile import path
                    import_dir = current_dir + @"\";
                }

                //getting files list, including subdirectories
                files = Directory.GetFiles(import_dir, "*.*", SearchOption.AllDirectories);
                //going through files list
                foreach (string file in files)
                {
                    //looking for inventory xml file
                    if (file.Contains("GetCommonInventory.xml") == true)
                    {
                        //adding it's path to comboBox
                        comboBox_xml.Items.Add(file);
                        //writing to log
                        richTextBox_log.Text += file + " file added." + "\n";
                    }
                }

                //check, if advanced more enabled
                if (checkBox_advanced.Checked == true)
                {
                    //write override.ini
                    WriteOverride(config_path);
                }
            }
        }

        private void radioButton_viking_CheckedChanged(object sender, EventArgs e)
        {
            //if import data was set to Viking
            if (radioButton_viking.Checked == true)
            {
                //writing to log
                richTextBox_log.Text += "Import mode set to: viking." + "\n";

                //set import data to viking
                import_data = "viking";

                //clean elements in comboBox
                comboBox_xml.Items.Clear();
                comboBox_xml.Text = "";

                //check, if advanced mode is disabled
                if (checkBox_advanced.Checked == false)
                {
                    //then use current directory for profile import path
                    import_dir = current_dir + @"\";
                }

                //getting files list, including subdirectories
                files = Directory.GetFiles(import_dir, "*.*", SearchOption.AllDirectories);
                //going through files list
                foreach (string file in files)
                {
                    //looking for viking xml file
                    if (file.Contains("GetDetailedChildList.xml") == true || file.Contains("VikingProfileData.xml") == true)
                    {
                        //adding it's path to comboBox
                        comboBox_xml.Items.Add(file);
                        //writing to log
                        richTextBox_log.Text += file + " file added." + "\n";
                    }
                }

                //check, if advanced more enabled
                if (checkBox_advanced.Checked == true)
                {
                    //write override.ini
                    WriteOverride(config_path);
                }
            }
        }

        private void radioButton_stables_CheckedChanged(object sender, EventArgs e)
        {
            //if import data was set to Stables
            if (radioButton_stables.Checked == true)
            {
                //writing to log
                richTextBox_log.Text += "Import mode set to: stables." + "\n";

                //set import data to stables
                import_data = "stables";

                //clean elements in comboBox
                comboBox_xml.Items.Clear();
                comboBox_xml.Text = "";

                //check, if advanced mode is disabled
                if (checkBox_advanced.Checked == false)
                {
                    //then use current directory for profile import path
                    import_dir = current_dir + @"\";
                }

                //getting files list, including subdirectories
                files = Directory.GetFiles(import_dir, "*.*", SearchOption.AllDirectories);
                //going through files list
                foreach (string file in files)
                {
                    //looking for stables xml file
                    if (file.Contains("Stables.xml") == true)
                    {
                        //adding it's path to comboBox
                        comboBox_xml.Items.Add(file);
                        //writing to log
                        richTextBox_log.Text += file + " file added." + "\n";
                    }
                }

                //check, if advanced more enabled
                if (checkBox_advanced.Checked == true)
                {
                    //write override.ini
                    WriteOverride(config_path);
                }
            }
        }

        private void radioButton_farm_CheckedChanged(object sender, EventArgs e)
        {
            //if import data was set to Farm
            if (radioButton_farm.Checked == true)
            {
                //writing to log
                richTextBox_log.Text += "Import mode set to: farm." + "\n";

                //set import data to farm
                import_data = "farm";

                //clean elements in comboBox
                comboBox_xml.Items.Clear();
                comboBox_xml.Text = "";

                //check, if advanced mode is disabled
                if (checkBox_advanced.Checked == false)
                {
                    //then use current directory for profile import path
                    import_dir = current_dir + @"\";
                }

                //getting files list, including subdirectories
                files = Directory.GetFiles(import_dir, "*.*", SearchOption.AllDirectories);
                //going through files list
                foreach (string file in files)
                {
                    //looking for farm xml file
                    if (file.Contains("GetUserRoomList.xml") == true)
                    {
                        //adding it's path to comboBox
                        comboBox_xml.Items.Add(file);
                        //writing to log
                        richTextBox_log.Text += file + " file added." + "\n";
                    }
                }

                //check, if advanced more enabled
                if (checkBox_advanced.Checked == true)
                {
                    //write override.ini
                    WriteOverride(config_path);
                }
            }
        }

        private void radioButton_hideout_CheckedChanged(object sender, EventArgs e)
        {
            //if import data was set to Hideout
            if (radioButton_hideout.Checked == true)
            {
                //writing to log
                richTextBox_log.Text += "Import mode set to: hideout." + "\n";

                //set import data to farm
                import_data = "hideout";

                //clean elements in comboBox
                comboBox_xml.Items.Clear();
                comboBox_xml.Text = "";

                //check, if advanced mode is disabled
                if (checkBox_advanced.Checked == false)
                {
                    //then use current directory for profile import path
                    import_dir = current_dir + @"\";
                }

                //getting files list, including subdirectories
                files = Directory.GetFiles(import_dir, "*.*", SearchOption.AllDirectories);
                //going through files list
                foreach (string file in files)
                {
                    //looking for hideout xml file
                    if (file.Contains("GetUserItemPositions_MyRoomINT.xml") == true)
                    {
                        //adding it's path to comboBox
                        comboBox_xml.Items.Add(file);
                        //writing to log
                        richTextBox_log.Text += file + " file added." + "\n";
                    }
                }

                //check, if advanced more enabled
                if (checkBox_advanced.Checked == true)
                {
                    //write override.ini
                    WriteOverride(config_path);
                }
            }
        }

        private void comboBox_xml_SelectedIndexChanged(object sender, EventArgs e)
        {
            //storing xml file path from comboBox
            import_path = quote + comboBox_xml.SelectedItem.ToString() + quote;
            //writing to log
            richTextBox_log.Text += "XML file import path: " + import_path + "\n";
        }

        private void button_import_Click(object sender, EventArgs e)
        {
            uint server_selected = 0;
            uint login_entered = 0;
            uint import_selected = 0;
            //checking, if server urls been set
            if (server_contentapi != "" && server_userapi != "")
            {
                server_selected = 1;
                richTextBox_log.Text += "[Import] server urls checks passed." + "\n";
            }
            //checking, if account details textboxes been filled
            if (textBox_login.Text != "" && textBox_password.Text != "" && textBox_character.Text != "")
            {
                login_entered = 1;
                richTextBox_log.Text += "[Import] account details checks passed." + "\n";
            }
            //checking, if xml import path been set
            if (import_path != "")
            {
                import_selected = 1;
                richTextBox_log.Text += "[Import] XML path check passed." + "\n";
            }
            //checking, if dragonrescue-import tool been set, server/login and import details filled
            if (tool_path != "" && server_selected == 1 && login_entered == 1 && import_selected == 1)
            {
                richTextBox_log.Text += "[Import] dragonrescue-import tool check passed." + "\n";
                //if import data set to dragons
                if (radioButton_dragons.Checked == true)
                {
                    richTextBox_log.Text += "[Import] importing data: dragons." + "\n";

                    //creating process for dragonrescue-import tool
                    Process exportProcess = new Process();
                    richTextBox_log.Text += "[Import] creating new process: " + "\n";
                    //setting path to tool
                    exportProcess.StartInfo.FileName = tool_path;
                    richTextBox_log.Text += "[Import] tool path: " + tool_path + "\n";
                    //passing argumnents for import dragons xml
                    exportProcess.StartInfo.Arguments = "--userApiUrl=" + server_userapi + " --contentApiUrl=" + server_contentapi + " --username=" + textBox_login.Text + " --password=" + textBox_password.Text + " --viking=" + textBox_character.Text + " import --file " + import_path;
                    richTextBox_log.Text += "[Import] using arguments: " + "--userApiUrl=" + server_userapi + " --contentApiUrl=" + server_contentapi + " --username=[REDACTED] --password=[REDACTED] --viking=[REDACTED] import --file " + import_path + "\n";
                    //starting process
                    exportProcess.Start();
                    richTextBox_log.Text += "[Import] starting dragonrescue-import process." + "\n";
                    //wait, until it finishes
                    exportProcess.WaitForExit();
                    richTextBox_log.Text += "[Import] dragonrescue-import process finished it's job." + "\n";
                }
                //if import data set to inventory
                else if (radioButton_inventory.Checked == true)
                {
                    richTextBox_log.Text += "[Import] importing data: inventory." + "\n";

                    //creating process for dragonrescue-import tool
                    Process exportProcess = new Process();
                    richTextBox_log.Text += "[Import] creating new process: " + "\n";
                    //setting path to tool
                    exportProcess.StartInfo.FileName = tool_path;
                    richTextBox_log.Text += "[Import] tool path: " + tool_path + "\n";
                    //passing argumnents for import inventory xml
                    exportProcess.StartInfo.Arguments = "--userApiUrl=" + server_userapi + " --contentApiUrl=" + server_contentapi + " --username=" + textBox_login.Text + " --password=" + textBox_password.Text + " --viking=" + textBox_character.Text + " import --file " + import_path + " --mode=inventory";
                    richTextBox_log.Text += "[Import] using arguments: " + "--userApiUrl=" + server_userapi + " --contentApiUrl=" + server_contentapi + " --username=[REDACTED] --password=[REDACTED] --viking=[REDACTED] import --file " + import_path + " --mode=inventory" + "\n";
                    //starting process
                    exportProcess.Start();
                    richTextBox_log.Text += "[Import] starting dragonrescue-import process." + "\n";
                    //wait, until it finishes
                    exportProcess.WaitForExit();
                    richTextBox_log.Text += "[Import] dragonrescue-import process finished it's job." + "\n";
                }
                //if import data set to viking
                else if (radioButton_viking.Checked == true)
                {
                    richTextBox_log.Text += "[Import] importing data: viking." + "\n";

                    //creating process for dragonrescue-import tool
                    Process exportProcess = new Process();
                    richTextBox_log.Text += "[Import] creating new process: " + "\n";
                    //setting path to tool
                    exportProcess.StartInfo.FileName = tool_path;
                    richTextBox_log.Text += "[Import] tool path: " + tool_path + "\n";
                    //passing argumnents for import viking xml
                    exportProcess.StartInfo.Arguments = "--userApiUrl=" + server_userapi + " --contentApiUrl=" + server_contentapi + " --username=" + textBox_login.Text + " --password=" + textBox_password.Text + " --viking=" + textBox_character.Text + " import --file " + import_path + " --mode=avatar";
                    richTextBox_log.Text += "[Import] using arguments: " + "--userApiUrl=" + server_userapi + " --contentApiUrl=" + server_contentapi + " --username=[REDACTED] --password=[REDACTED] --viking=[REDACTED] import --file " + import_path + " --mode=avatar" + "\n";
                    //starting process
                    exportProcess.Start();
                    richTextBox_log.Text += "[Import] starting dragonrescue-import process." + "\n";
                    //wait, until it finishes
                    exportProcess.WaitForExit();
                    richTextBox_log.Text += "[Import] dragonrescue-import process finished it's job." + "\n";
                }
                //if import data set to stables
                else if (radioButton_stables.Checked == true)
                {
                    richTextBox_log.Text += "[Import] importing data: stables." + "\n";

                    //creating process for dragonrescue-import tool
                    Process exportProcess = new Process();
                    richTextBox_log.Text += "[Import] creating new process: " + "\n";
                    //setting path to tool
                    exportProcess.StartInfo.FileName = tool_path;
                    richTextBox_log.Text += "[Import] tool path: " + tool_path + "\n";
                    //passing argumnents for import stables xml
                    exportProcess.StartInfo.Arguments = "--userApiUrl=" + server_userapi + " --contentApiUrl=" + server_contentapi + " --username=" + textBox_login.Text + " --password=" + textBox_password.Text + " --viking=" + textBox_character.Text + " import --file " + import_path + " --mode=stables";
                    richTextBox_log.Text += "[Import] using arguments: " + "--userApiUrl=" + server_userapi + " --contentApiUrl=" + server_contentapi + " --username=[REDACTED] --password=[REDACTED] --viking=[REDACTED] import --file " + import_path + " --mode=stables" + "\n";
                    //starting process
                    exportProcess.Start();
                    richTextBox_log.Text += "[Import] starting dragonrescue-import process." + "\n";
                    //wait, until it finishes
                    exportProcess.WaitForExit();
                    richTextBox_log.Text += "[Import] dragonrescue-import process finished it's job." + "\n";
                }
                //if import data set to farm
                else if (radioButton_farm.Checked == true)
                {
                    richTextBox_log.Text += "[Import] importing data: farm." + "\n";

                    //creating process for dragonrescue-import tool
                    Process exportProcess = new Process();
                    richTextBox_log.Text += "[Import] creating new process: " + "\n";
                    //setting path to tool
                    exportProcess.StartInfo.FileName = tool_path;
                    richTextBox_log.Text += "[Import] tool path: " + tool_path + "\n";
                    //passing argumnents for import farm xml
                    exportProcess.StartInfo.Arguments = "--userApiUrl=" + server_userapi + " --contentApiUrl=" + server_contentapi + " --username=" + textBox_login.Text + " --password=" + textBox_password.Text + " --viking=" + textBox_character.Text + " import --file " + import_path + " --mode=farm";
                    richTextBox_log.Text += "[Import] using arguments: " + "--userApiUrl=" + server_userapi + " --contentApiUrl=" + server_contentapi + " --username=[REDACTED] --password=[REDACTED] --viking=[REDACTED] import --file " + import_path + " --mode=farm" + "\n";
                    //starting process
                    exportProcess.Start();
                    richTextBox_log.Text += "[Import] starting dragonrescue-import process." + "\n";
                    //wait, until it finishes
                    exportProcess.WaitForExit();
                    richTextBox_log.Text += "[Import] dragonrescue-import process finished it's job." + "\n";
                }
                //if import data set to hideout
                else if (radioButton_hideout.Checked == true)
                {
                    richTextBox_log.Text += "[Import] importing data: hideout." + "\n";

                    //creating process for dragonrescue-import tool
                    Process exportProcess = new Process();
                    richTextBox_log.Text += "[Import] creating new process: " + "\n";
                    //setting path to tool
                    exportProcess.StartInfo.FileName = tool_path;
                    richTextBox_log.Text += "[Import] tool path: " + tool_path + "\n";
                    //passing argumnents for import hideout xml
                    exportProcess.StartInfo.Arguments = "--userApiUrl=" + server_userapi + " --contentApiUrl=" + server_contentapi + " --username=" + textBox_login.Text + " --password=" + textBox_password.Text + " --viking=" + textBox_character.Text + " import --file " + import_path + " --mode=hideout";
                    richTextBox_log.Text += "[Import] using arguments: " + "--userApiUrl=" + server_userapi + " --contentApiUrl=" + server_contentapi + " --username=[REDACTED] --password=[REDACTED] --viking=[REDACTED] import --file " + import_path + " --mode=hideout" + "\n";
                    //starting process
                    exportProcess.Start();
                    richTextBox_log.Text += "[Import] starting dragonrescue-import process." + "\n";
                    //wait, until it finishes
                    exportProcess.WaitForExit();
                    richTextBox_log.Text += "[Import] dragonrescue-import process finished it's job." + "\n";
                }
            }
            //show error message, in case if something is not set
            else if (tool_path != "" && server_selected == 0 || login_entered == 0 || import_selected == 0)
            {
                string error_msg = "Following errors been encountered:" + "\n";
                richTextBox_log.Text += "[Import] failed to pass fool-proof checks." + "\n";
                if (server_selected == 0)
                {
                    error_msg += "-Server not selected!" + "\n";
                    richTextBox_log.Text += "[Import] server urls checks failed." + "\n";
                }
                if (login_entered == 0)
                {
                    error_msg += "-Login details not filled!" + "\n";
                    richTextBox_log.Text += "[Import] account details checks failed." + "\n";
                }
                if (import_selected == 0)
                {
                    error_msg += "-Import file not selected!" + "\n";
                    richTextBox_log.Text += "[Import] XML path check failed." + "\n";
                }
                MessageBox.Show(error_msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox_advanced_CheckedChanged(object sender, EventArgs e)
        {
            //if advanced mode is enabled
            if (checkBox_advanced.Checked == true)
            {
                //write to log
                richTextBox_log.Text += "Advanced mode: enabled." + "\n";

                //check, if override.ini path exists
                if (config_path != "")
                {
                    //write to log
                    richTextBox_log.Text += "override.ini config detected." + "\n";

                    //reading config contents
                    ParseOverride(config_path);
                }
                else if (config_path == "")
                {
                    //set config path to override.ini
                    config_path = "override.ini";
                    //create new file in current directory
                    FileStream fs = File.Create(config_path);
                    fs.Close();
                    //write contents to override.ini
                    WriteOverride(config_path);
                }
            }
            //if advanced mode is disabled
            else if (checkBox_advanced.Checked == false)
            {
                //write to log
                richTextBox_log.Text += "Advanced mode: disabled." + "\n";

                //getting files list in current directory
                files = Directory.GetFiles(current_dir);

                //going through files list
                foreach (string file in files)
                {
                    //looking for dragonrescue-import tool
                    if (file.Contains("dragonrescue-import.exe") == true)
                    {
                        //storing path to dragonrescue-import tool
                        tool_path = file;
                        //writing to log
                        richTextBox_log.Text += "Tool path: " + tool_path + "\n";
                    }
                }

                //resetting import and export paths to current directory
                import_dir = current_dir + @"\";
                export_path = current_dir + @"\";

                //unchecking import data radioboxes
                radioButton_dragons.Checked = false;
                radioButton_inventory.Checked = false;
                radioButton_viking.Checked = false;
                radioButton_stables.Checked = false;
                radioButton_farm.Checked = false;
                radioButton_hideout.Checked = false;

                //clean elements in comboBox
                comboBox_xml.Items.Clear();
                comboBox_xml.Text = "";
            }
        }

        public void ParseOverride(string path)
        {
            //reading override.ini contents
            string[] config = System.IO.File.ReadAllLines(path);
            //going through settings stored in override.ini
            for (int i = 0; i < config.Length; i++)
            {
                //looking for dragonrescue-import tool path
                if (config[i].Contains("ToolPath=") == true)
                {
                    //storing path to dragonrescue-import tool
                    tool_path = config[i].Replace("ToolPath=", "");

                    richTextBox_log.Text += "[Override] set tool path: " + tool_path + "\n";
                }
                //looking profile directory path, used for import
                else if (config[i].Contains("ImportDir=") == true)
                {
                    //storing profile directory path
                    import_dir = config[i].Replace("ImportDir=", "");

                    richTextBox_log.Text += "[Override] set import dir: " + import_dir + "\n";
                }
                //looking for export directory path, used for export
                else if (config[i].Contains("ExportDir=") == true)
                {
                    //storing export directory path
                    export_path = config[i].Replace("ExportDir=", "");

                    richTextBox_log.Text += "[Override] set export dir: " + export_path + "\n";
                }
                //looking for mode setting
                else if (config[i].Contains("Mode=") == true)
                {
                    //storing mode setting
                    mode = config[i].Replace("Mode=", "");

                    richTextBox_log.Text += "[Override] set mode: " + mode + "\n";
                }
                //looking for server setting
                else if (config[i].Contains("Server=") == true)
                {
                    //storing server setting
                    server = config[i].Replace("Server=", "");

                    richTextBox_log.Text += "[Override] set server: " + server + "\n";
                }
                //looking for login setting
                else if (config[i].Contains("Login=") == true)
                {
                    //storing login setting
                    textBox_login.Text = config[i].Replace("Login=", "");

                    richTextBox_log.Text += "[Override] set login: [REDACTED]" + "\n";
                }
                //looking for password setting
                else if (config[i].Contains("Password=") == true)
                {
                    //storing password setting
                    textBox_password.Text = config[i].Replace("Password=", "");

                    richTextBox_log.Text += "[Override] set password: [REDACTED]" + "\n"; ;
                }
                //looking for viking name setting
                else if (config[i].Contains("VikingName=") == true)
                {
                    //storing viking name setting
                    textBox_character.Text = config[i].Replace("VikingName=", "");

                    richTextBox_log.Text += "[Override] set viking name: [REDACTED]" + "\n"; ;
                }
                //looking for import data setting
                else if (config[i].Contains("ImportData=") == true)
                {
                    //storing import data setting
                    import_data = config[i].Replace("ImportData=", "");

                    richTextBox_log.Text += "[Override] set import data: " + import_data + "\n";
                }
            }

            //setting GUI radiobuttons
            if (mode == "import")
            {
                radioButton_import.Checked = true;
            }
            else if (mode == "export")
            {
                radioButton_export.Checked = true;
            }
            if (server == "sodoff")
            {
                radioButton_sodoff.Checked = true;
            }
            else if (server == "edge")
            {
                radioButton_edge.Checked = true;
            }
            else if (server == "guild")
            {
                radioButton_guild.Checked = true;
            }
            if (import_data == "dragons")
            {
                radioButton_dragons.Checked = true;
            }
            else if (import_data == "inventory")
            {
                radioButton_inventory.Checked = true;
            }
            else if (import_data == "viking")
            {
                radioButton_viking.Checked = true;
            }
            else if (import_data == "stables")
            {
                radioButton_stables.Checked = true;
            }
            else if (import_data == "farm")
            {
                radioButton_farm.Checked = true;
            }
            else if (import_data == "hideout")
            {
                radioButton_hideout.Checked = true;
            }
        }

        public void WriteOverride(string path)
        {
            //reading settings into string array
            string[] config = new string[9];
            config[0] = "ToolPath=" + tool_path;
            config[1] = "ImportDir=" + import_dir;
            config[2] = "ExportDir=" + export_path;
            config[3] = "Mode=" + mode;
            config[4] = "Server=" + server;
            config[5] = "Login=" + textBox_login.Text;
            config[6] = "Password=" + textBox_password.Text;
            config[7] = "VikingName=" + textBox_character.Text;
            config[8] = "ImportData=" + import_data;
            //and storing them in override.ini
            System.IO.File.WriteAllLines(path, config);
        }

        private void richTextBox_log_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            richTextBox_log.SelectionStart = richTextBox_log.Text.Length;
            // scroll it automatically
            richTextBox_log.ScrollToCaret();

            //checking, if log file exists
            if (log_path != "")
            {
                //write contents to log.txt
                System.IO.File.WriteAllText(log_path, richTextBox_log.Text);
            }
            //checking, if log file doesn't exist
            else if (log_path == "")
            {
                //set log path to log.txt
                log_path = "log.txt";
                //create new log file in current directory
                FileStream fs = File.Create(log_path);
                fs.Close();
                //write contents to log.txt
                System.IO.File.WriteAllText(log_path, richTextBox_log.Text);
            }
        }

        byte[] ReadFile(string path)
        {
            using (FileStream fsSource = new FileStream(path,
                    FileMode.Open, FileAccess.Read))
            {
                // Read the source file into a byte array.
                byte[] bytes = new byte[fsSource.Length];

                int numBytesToRead = (int)fsSource.Length;
                int numBytesRead = 0;
                while (numBytesToRead > 0)
                {
                    // Read may return anything from 0 to numBytesToRead.
                    int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

                    // Break when the end of the file is reached.
                    if (n == 0)
                        break;

                    numBytesRead += n;
                    numBytesToRead -= n;
                }
                numBytesToRead = bytes.Length;
                return bytes;
            }
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        private void label_about_Click(object sender, EventArgs e)
        {
            About about_dialog = new About();
            about_dialog.Show();
        }
    }
}