using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace QuickScript
{
    public partial class MainForm : Form
    {
        private SQLiteConnection sql_con;
        private List<Script> scripts = new List<Script>();
        private String EMPTY_STRING = String.Empty;
        private Color GREEN = Color.Green;
        private const String PASSWORD = "chinchilla";
        private const String ERROR_FILE_NAME = "\\error_log.txt";
        private const String SCRIPT_UPDATED = "Script updated";
        private const String SCRIPT_ADDED = "New Script added";
        private const String SCRIPT_DELETED = "Script deleted";
        private const String COPIED_CLIPBOARD = "Copied to Clipboard";
        private const String DELETE_CONFIRM_TEXT = "Really delete?";
        private const String DELETE_CONFIRM_TITLE = "Confirm delete";
        private const String PASSWORD_PROMPT_TITLE = "Authentication required for Edit mode";
        private const String PASSWORD_PROMPT_TEXT = "Enter password to enable Edit mode";
        private const String ALL_FIELDS = "Entry to all fields required";

        /// <summary>
        /// Create Form, Connect/Create Database, Create table if not existing, Disable Edit tab
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            SetConnection();
            CreateTable();
            ToggleEditTab(false);
            FillDisplayListbox();
        }

        /// <summary>
        /// Used to call ToString() of object passed to an error log in same directory as executable. Mostly used for printing SQLite exceptions.
        /// </summary>
        /// <param name="error">Object to to call ToString() on; preferable an Exception</param>
        private void writeErrorLog(object error) {
            string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            System.IO.StreamWriter file = new System.IO.StreamWriter(currentDirectory + ERROR_FILE_NAME, true);
            file.WriteLine(DateTime.Now.ToString() + ": " + error);

            file.Close();
        }

        /// <summary>
        /// Events for when Scripts or Edit tab are clicked. Used to prompt for password and only enable the controls on Edit tab if correct.
        /// </summary>
        /// <param name="sender">ingored</param>
        /// <param name="e">ignored</param>
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 1)
            {
                PasswordPrompt();
            }
            else if (tabControl.SelectedIndex == 0)
            {
                FillDisplayListbox();
                ToggleEditTab(false);
            }
            loadBar(GREEN, 0, EMPTY_STRING);
        }

        /// <summary>
        /// Fancy load bar at the bottom of the window to give visual feedback from program.
        /// </summary>
        /// <param name="colour">Colour of the bar, only green appears to work!</param>
        /// <param name="percent">Percentage of the bar to fill</param>
        /// <param name="text">Text to display next to the bar</param>
        private void loadBar(Color colour, int percent, string text)
        {
            progressBar.Value = 0;
            progressBar.Value = percent;
            lblProgress.Text = text;
        }

        #region Database methods

        /// <summary>
        /// Opens previous database file Scripts.sqlite or creates a new one. Both in the same directory as the executable.
        /// </summary>
        private void SetConnection()
        {
            sql_con = new SQLiteConnection("Data Source=Scripts.sqlite;Version=3");
        }

        /// <summary>
        /// Open DB connection, creates the scripts table, if it does exist: duplicate exception is caught, close DB connection
        /// </summary>
        private void CreateTable()
        {
            string sql = "CREATE TABLE scripts (id INTEGER PRIMARY KEY,"
                + "title VARCHAR(255) NOT NULL, searchterms VARCHAR(255) NOT NULL," 
                + "content TEXT NOT NULL)";

            try
            {
                sql_con.Open();
                SQLiteCommand sql_cmd = new SQLiteCommand(sql, sql_con);
                sql_cmd.ExecuteNonQuery();

            }
            catch (SQLiteException sql_ex)
            {
                writeErrorLog(sql_ex.Message);
                //duplicate table exception below, possible to add code to throw another exception if not duplicate but another exception
                //+		sql_ex	{"SQLite error\r\ntable scripts already exists"}	System.Data.SQLite.SQLiteException
            }
            finally
            {
                sql_con.Close();
            }
        }

        /// <summary>
        /// Open DB connection, retreieves all data from scripts table, iterates through results and places them in a List of Script objects, closes DB connection
        /// </summary>
        /// <returns>A List of all Script objects from the database</returns>
        private List<Script> loadScripts()
        {
            string sql = "SELECT * FROM scripts ORDER BY title";
            List<Script> scripts = new List<Script>();
            SQLiteDataReader reader = null;

            try
            {
                sql_con.Open();
                reader = (new SQLiteCommand(sql, sql_con)).ExecuteReader();
                while (reader.Read())
                {
                    scripts.Add(new Script()
                    {
                        ID = (Int64)reader["id"],
                        Title = (string)reader["title"],
                        SearchTerms = (string)reader["searchterms"],
                        Content = (string)reader["content"]
                    });
                }
            }
            catch (SQLiteException sql_ex)
            {
                writeErrorLog(sql_ex.Message);
            }
            finally
            {
                reader.Close();
                sql_con.Close();
            }
            return scripts;
        }

        /// <summary>
        /// Open DB connection, takes inputted strings and stores them into a new row in the database, closes DB connection
        /// Duplicates are checked for from the ListBox on the Edit tab to the Title textbox on the edit tab upon submission
        /// FUTURE: Check database for duplicates before adding
        /// </summary>
        /// <param name="title">Title of item to store</param>
        /// <param name="searchterms">Search terms of item to store</param>
        /// <param name="content">Content of item to store</param>
        private void NewScript(String title, String searchterms, String content)
        {
            string sql = "INSERT INTO scripts (id, title, searchterms, content) values(NULL, @title, @searchterms, @content)";

            try
            {
                sql_con.Open();
                SQLiteCommand sql_cmd = new SQLiteCommand(sql, sql_con);
                sql_cmd.Parameters.Add(new SQLiteParameter("@title", title));
                sql_cmd.Parameters.Add(new SQLiteParameter("@searchterms", searchterms));
                sql_cmd.Parameters.Add(new SQLiteParameter("@content", content));
                sql_cmd.ExecuteNonQuery();
            }
            catch (SQLiteException sql_ex) 
            {
                writeErrorLog(sql_ex.Message);
            }
            finally 
            {
                sql_con.Close();
            }
        }

        /// <summary>
        /// Open DB connection, takes inputed strings and uptakes the item in the database, closes DB connection
        /// </summary>
        /// <param name="id">ID of the item in the database, primary key used for updating</param>
        /// <param name="title">New title of the item</param>
        /// <param name="searchterms">New search terms for the item</param>
        /// <param name="content">New content for the item</param>
        private void EditScript(Int64 id, String title, String searchterms, String content)
        {
            string sql = "UPDATE scripts SET title=@title, searchterms=@searchterms, content=@content WHERE id=@id";
            
            try
            {
                sql_con.Open();
                SQLiteCommand sql_cmd = new SQLiteCommand(sql, sql_con);
                sql_cmd.Parameters.Add(new SQLiteParameter("@id", id));
                sql_cmd.Parameters.Add(new SQLiteParameter("@title", title));
                sql_cmd.Parameters.Add(new SQLiteParameter("@searchterms", searchterms));
                sql_cmd.Parameters.Add(new SQLiteParameter("@content", content));
                sql_cmd.ExecuteNonQuery();
            }
            catch (SQLiteException sql_ex)
            {
                writeErrorLog(sql_ex.Message);
            }
            finally
            {
                sql_con.Close();
            }
        }

        /// <summary>
        /// Open DB connection, deletes item in DB based on ID number, closes DB connection
        /// FUTURE: Check if item exists before attempts to delete
        /// </summary>
        /// <param name="id">ID of item to remove from DB</param>
        private void DeleteScript(Int64 id)
        {
            string sql = "DELETE FROM scripts WHERE id=@id";

            try
            {
                sql_con.Open();
                SQLiteCommand sql_cmd = new SQLiteCommand(sql, sql_con);
                sql_cmd.Parameters.Add(new SQLiteParameter("@id", id));
                sql_cmd.ExecuteNonQuery();
            }
            catch (SQLiteException sql_ex)
            {
                writeErrorLog(sql_ex.Message);
            }
            finally
            {
                sql_con.Close();
            }
        }

        #endregion

        #region Display Pane methods

        /// <summary>
        /// Loads the scripts from the scripts for the database, binds to Display listbox, sets Title as display member
        /// </summary>
        private void FillDisplayListbox()
        {
            scripts = loadScripts();
            lbDisplayScripts.DataSource = scripts;
            lbDisplayScripts.DisplayMember = "Title";
        }

        /// <summary>
        /// Clears Display searchbox and resets load bar indicator
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void tbDisplaySearch_Enter(object sender, EventArgs e)
        {
            loadBar(GREEN, 0, EMPTY_STRING);
            tbDisplayContent.Text = EMPTY_STRING;
            tbDisplaySearch.Text = EMPTY_STRING;
        }

        /// <summary>
        /// Rebinds the Display listbox and sets focus on Display search box
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void btnDisplayReset_Click(object sender, EventArgs e)
        {
            lbDisplayScripts.DataSource = scripts;
            lbDisplayScripts.SelectedIndex = 0;
            tbDisplaySearch.Focus();
        }

        /// <summary>
        /// Fills Display content with selected item from Display listbox if "Enter" key is pushed in the Display Search text box
        /// Sets focus to Display contents and copies text to Clipboard
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">Used to monitor for "Enter" key press</param>
        private void tbDisplaySearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (lbDisplayScripts.SelectedItem != null)
                {
                    FillDisplayContents((Script)lbDisplayScripts.SelectedItem, true);
                }
            }
            List<Script> temp = new List<Script>();
            foreach (Script s in scripts)
            {
                if (ParseTerms(s, tbDisplaySearch.Text) != null)
                {
                    temp.Add(s);
                }
            }
            lbDisplayScripts.DataSource = temp;
        }

        /// <summary>
        /// Fills the Display contents with the selected item from the Display listbox if "Tab" key is pressed in the Display listbox
        /// Tab keypress continues after to goto next tab index (contents textbox)
        /// Sets focus to Display contents and copies text to Clipboard
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">Used to monitor for "Tab" key press</param>
        private void lbDisplayScripts_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (lbDisplayScripts.SelectedItem != null)
                {
                    FillDisplayContents((Script)lbDisplayScripts.SelectedItem, false);
                    Clipboard.SetText(tbDisplayContent.Text);
                    loadBar(GREEN, 100, COPIED_CLIPBOARD);
                }
            }
        }

        /// <summary>
        /// Fills the Display contents with the selected item from the Display listbox if the 
        /// "Enter" "Up" or "Down" keys are pressed in the Display listbox.
        ///  If "Enter", sets focus to Display contents and copies text to Clipboard
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">Used to monitor for "Enter" "Up" "Down" key presses</param>
        private void lbDisplayScripts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (lbDisplayScripts.SelectedItem != null)
                {
                    FillDisplayContents((Script)lbDisplayScripts.SelectedItem, true);
                }
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                if (lbDisplayScripts.SelectedItem != null)
                {
                    tbDisplayContent.Text = ((Script)lbDisplayScripts.SelectedItem).Content;
                }
            }
        }

        /// <summary>
        /// Fills the Display contents with the selected item from the Display listbox when it is clicked on
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void lbDisplayScripts_Click(object sender, EventArgs e)
        {
            if (lbDisplayScripts.SelectedItem != null)
            {
                tbDisplayContent.Text = ((Script)lbDisplayScripts.SelectedItem).Content;
            }
        }

        /// <summary>
        /// Fills the Display contents with the selected item from the Display listbox when it is double clicked on
        /// Sets focus to Display contents and copies text to Clipboard
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void lbDisplayScripts_DoubleClick(object sender, EventArgs e)
        {
            if (lbDisplayScripts.SelectedItem != null)
            {
                FillDisplayContents((Script)lbDisplayScripts.SelectedItem, true);
            }
        }

        /// <summary>
        /// Splits on commas a Script object's SearchTerms attribute into an array
        /// Iterates through the array and checks if Display search text box text is present
        /// and returns the Script obejct if it does
        /// </summary>
        /// <param name="s">Script to check</param>
        /// <param name="searchwords">Display search text box text</param>
        /// <returns>Script object if search matches</returns>
        private Script ParseTerms(Script s, String searchwords)
        {
            String[] terms = s.SearchTerms.Split(',');
            foreach (String t in terms)
            {
                if (t.Contains(searchwords))
                    return s;
            }
            return null;
        }

        /// <summary>
        /// Extracts Script object's attributes and populates infomration to corresponding compoents on the Display tab
        /// </summary>
        /// <param name="s">Script object to populate</param>
        /// <param name="focus">True to focus cursor in Display content textbox</param>
        private void FillDisplayContents(Script s, bool focus)
        {
            tbDisplayContent.Text = s.Content;
            if (focus) tbDisplayContent.Focus();
            tbDisplayContent.SelectAll();
            Clipboard.SetText(tbDisplayContent.Text);
            loadBar(GREEN, 100, COPIED_CLIPBOARD);
        }

        #endregion

        #region Edit Pane

        /// <summary>
        /// Poplates Edit listbox with scripts and sets Title as the main display member
        /// </summary>
        private void FillEditListbox()
        {
            lbEditScripts.DataSource = loadScripts();
            lbEditScripts.DisplayMember = "Title";
        }

        /// <summary>
        /// Toggles the controls on the Edit tab enabled or disabled
        /// </summary>
        /// <param name="mode">true for enabled, false for disabled</param>
        private void ToggleEditTab(bool mode)
        {
            tabControl.TabPages[1].Enabled = mode;
            if (mode)
            {
                FillEditListbox();
            }
        }

        /// <summary>
        /// Creates secondary form on top that prompts for password
        /// FUTURE: non-hardcoded password, maybe hashed
        /// </summary>
        private void PasswordPrompt()
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            textBox.PasswordChar = '*';

            form.Text = PASSWORD_PROMPT_TITLE;
            label.Text = PASSWORD_PROMPT_TEXT;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;

            form.Show();
            buttonOk.Click +=
                delegate
                {
                    if (textBox.Text.ToLower().Equals(PASSWORD.ToLower()))
                    {
                        ToggleEditTab(true);
                        form.Dispose();
                    }
                    else
                    {
                        PasswordPrompt();
                        form.Dispose();
                    }
                };
            buttonCancel.Click +=
                delegate
                {
                    form.Dispose();
                };
            textBox.KeyDown +=
                delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Enter)
                        buttonOk.PerformClick();
                };
        }

        /// <summary>
        /// Calls ValidateEditTextboxes method when Edit titles text changes
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void tbEditTitle_TextChanged(object sender, EventArgs e)
        {
            ValidateEditTextboxes();
        }

        /// <summary>
        /// Calls ValidateEditTextboxes method when Edit search terms text changes
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void tbEditSearchTerms_TextChanged(object sender, EventArgs e)
        {
            ValidateEditTextboxes();
        }

        /// <summary>
        /// Calls ValidateEditTextboxes method when Edit content text changes
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void tbEditContents_TextChanged(object sender, EventArgs e)
        {
            ValidateEditTextboxes();
        }

        /// <summary>
        /// Checks the length of the Edit title, search terms, and contents textboxes
        /// Enables the save button if evaluated true, disables it is false
        /// </summary>
        /// <returns>true if all texts boxes have text length greater than 0, false if not</returns>
        private bool ValidateEditTextboxes()
        {
            if (tbEditTitle.TextLength > 0
                && tbEditSearchTerms.TextLength > 0
                && tbEditContents.TextLength > 0)
            {
                btnEditSave.Enabled = true;
                return true;
            }
            else
            {
                btnEditSave.Enabled = false;
                return false;
            }
        }

        /// <summary>
        /// Clears text of the Edit title, search terms, and contents textboxes
        /// </summary>
        private void ClearEditTextboxes()
        {
            tbEditTitle.Text = EMPTY_STRING;
            tbEditSearchTerms.Text = EMPTY_STRING;
            tbEditContents.Text = EMPTY_STRING;
        }

        /// <summary>
        /// Populates the text of the Edit title, search terms, and contents textboxes with the attributes
        /// of the Script object parameter
        /// </summary>
        /// <param name="s">Script to populate into text boxes</param>
        private void FillEditTextboxes(Script s)
        {
            tbEditTitle.Text = s.Title;
            tbEditSearchTerms.Text = s.SearchTerms;
            tbEditContents.Text = s.Content;
        }

        /// <summary>
        /// Clears the text of the Edit title, search terms, and contents textboxes and puts focus on the Title text box
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void btnEditNew_Click(object sender, EventArgs e)
        {
            ClearEditTextboxes();
            tbEditTitle.Focus();
            ValidateEditTextboxes();
        }

        /// <summary>
        /// Populates the text of the Edit title, search terms, and contents textboxes with the selected item in the Edit listbox
        /// when it is clicked on
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void lbEditScripts_Click(object sender, EventArgs e)
        {
            FillEditTextboxes((Script)lbEditScripts.SelectedItem);
            btnEditSave.Enabled = true;
        }

        /// <summary>
        /// Populates the text of the Edit title, search terms, and contents textboxes with the selected item in the Edit listbox
        /// when it is double clicked on
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void lbEditScripts_DoubleClick(object sender, EventArgs e)
        {
            FillEditTextboxes((Script)lbEditScripts.SelectedItem);
            btnEditSave.Enabled = true;
        }

        /// <summary>
        /// Prompts for deletion confirmation, calls DB delete for the Script ID of the selected item
        /// in the Edit listbox
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void btnEditDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(DELETE_CONFIRM_TEXT, DELETE_CONFIRM_TITLE, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DeleteScript(((Script)lbEditScripts.SelectedItem).ID);
                loadBar(GREEN, 100, SCRIPT_DELETED);
                ClearEditTextboxes();
                FillEditListbox();
            }
        }

        /// <summary>
        /// Checks if provided string exists as a title in the Edit listbox items title attribute
        /// </summary>
        /// <param name="title">new title to check</param>
        /// <returns>true if duplicate exists, false if none</returns>
        private bool EditScriptsDuplicates(string title, out Int64 id)
        {
            foreach (Script s in lbEditScripts.Items)
            {
                if (s.Title.ToLower().Equals(title.ToLower()))
                {
                    //duplicate exists
                    id = s.ID;
                    return true;
                }
            }
            id = -1;
            return false;
        }

        /// <summary>
        /// Validates the Edit title, search terms, and content textboxes, checks if exists already; calls update if 
        /// it does exist else it creates new Script
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void btnEditSave_Click(object sender, EventArgs e)
        {
            if (!ValidateEditTextboxes())
            {
                loadBar(GREEN, 75, ALL_FIELDS);
                return;
            }

            //checks if exists, else adds
            Int64 id;
            if (lbEditScripts.SelectedItem != null 
                && EditScriptsDuplicates(tbEditTitle.Text, out id))
            {
                EditScript(id, tbEditTitle.Text, tbEditSearchTerms.Text, tbEditContents.Text);
                loadBar(GREEN, 100, SCRIPT_UPDATED);
                ClearEditTextboxes();
            }
            else
            {
                NewScript(tbEditTitle.Text, tbEditSearchTerms.Text, tbEditContents.Text);
                loadBar(GREEN, 100, SCRIPT_ADDED);
                ClearEditTextboxes();
            }
            //refresh lb
            FillEditListbox();
        }   

        #endregion

        /// <summary>
        /// Double check for closing database connection when the Main window is closing
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            sql_con.Close();
        }   
    }
}
