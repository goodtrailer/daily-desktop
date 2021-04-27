
namespace DailyDesktop.Desktop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.providerGroupBox = new System.Windows.Forms.GroupBox();
            this.descriptionGroupBox = new System.Windows.Forms.GroupBox();
            this.providerDescriptionBox = new System.Windows.Forms.TextBox();
            this.providerComboBox = new System.Windows.Forms.ComboBox();
            this.okButton = new System.Windows.Forms.Button();
            this.optionsGroupBox = new System.Windows.Forms.GroupBox();
            this.updateTimeLabel = new System.Windows.Forms.Label();
            this.updateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel.SuspendLayout();
            this.providerGroupBox.SuspendLayout();
            this.descriptionGroupBox.SuspendLayout();
            this.optionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.providerGroupBox, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.okButton, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.optionsGroupBox, 0, 1);
            this.tableLayoutPanel.Location = new System.Drawing.Point(9, 9);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.6798F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.3202F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(350, 423);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // providerGroupBox
            // 
            this.providerGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.providerGroupBox.Controls.Add(this.descriptionGroupBox);
            this.providerGroupBox.Controls.Add(this.providerComboBox);
            this.providerGroupBox.Location = new System.Drawing.Point(3, 3);
            this.providerGroupBox.Name = "providerGroupBox";
            this.providerGroupBox.Size = new System.Drawing.Size(344, 208);
            this.providerGroupBox.TabIndex = 0;
            this.providerGroupBox.TabStop = false;
            this.providerGroupBox.Text = "Provider";
            // 
            // descriptionGroupBox
            // 
            this.descriptionGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionGroupBox.Controls.Add(this.providerDescriptionBox);
            this.descriptionGroupBox.Location = new System.Drawing.Point(6, 51);
            this.descriptionGroupBox.Name = "descriptionGroupBox";
            this.descriptionGroupBox.Size = new System.Drawing.Size(332, 151);
            this.descriptionGroupBox.TabIndex = 2;
            this.descriptionGroupBox.TabStop = false;
            this.descriptionGroupBox.Text = "Description";
            // 
            // providerDescriptionBox
            // 
            this.providerDescriptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.providerDescriptionBox.BackColor = System.Drawing.SystemColors.Menu;
            this.providerDescriptionBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.providerDescriptionBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.providerDescriptionBox.Location = new System.Drawing.Point(6, 22);
            this.providerDescriptionBox.Multiline = true;
            this.providerDescriptionBox.Name = "providerDescriptionBox";
            this.providerDescriptionBox.PlaceholderText = "No description.";
            this.providerDescriptionBox.ReadOnly = true;
            this.providerDescriptionBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.providerDescriptionBox.Size = new System.Drawing.Size(320, 123);
            this.providerDescriptionBox.TabIndex = 1;
            this.providerDescriptionBox.TabStop = false;
            // 
            // providerComboBox
            // 
            this.providerComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.providerComboBox.FormattingEnabled = true;
            this.providerComboBox.Location = new System.Drawing.Point(6, 22);
            this.providerComboBox.Name = "providerComboBox";
            this.providerComboBox.Size = new System.Drawing.Size(332, 23);
            this.providerComboBox.TabIndex = 2;
            this.providerComboBox.SelectedIndexChanged += new System.EventHandler(this.providerComboBox_SelectedIndexChanged);
            this.providerComboBox.DropDown += new System.EventHandler(this.providerComboBox_DropDown);
            // 
            // okButton
            // 
            this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.okButton.Location = new System.Drawing.Point(272, 395);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // optionsGroupBox
            // 
            this.optionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optionsGroupBox.Controls.Add(this.updateTimeLabel);
            this.optionsGroupBox.Controls.Add(this.updateTimePicker);
            this.optionsGroupBox.Location = new System.Drawing.Point(3, 217);
            this.optionsGroupBox.Name = "optionsGroupBox";
            this.optionsGroupBox.Size = new System.Drawing.Size(344, 171);
            this.optionsGroupBox.TabIndex = 2;
            this.optionsGroupBox.TabStop = false;
            this.optionsGroupBox.Text = "Options";
            // 
            // updateTimeLabel
            // 
            this.updateTimeLabel.AutoSize = true;
            this.updateTimeLabel.Location = new System.Drawing.Point(6, 19);
            this.updateTimeLabel.Name = "updateTimeLabel";
            this.updateTimeLabel.Size = new System.Drawing.Size(74, 15);
            this.updateTimeLabel.TabIndex = 1;
            this.updateTimeLabel.Text = "Update Time";
            // 
            // updateTimePicker
            // 
            this.updateTimePicker.CustomFormat = "h:mm tt";
            this.updateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.updateTimePicker.Location = new System.Drawing.Point(86, 15);
            this.updateTimePicker.Name = "updateTimePicker";
            this.updateTimePicker.ShowUpDown = true;
            this.updateTimePicker.Size = new System.Drawing.Size(92, 23);
            this.updateTimePicker.TabIndex = 1;
            this.updateTimePicker.Value = new System.DateTime(2021, 4, 26, 0, 0, 0, 0);
            this.updateTimePicker.ValueChanged += new System.EventHandler(this.updateTimePicker_ValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 441);
            this.Controls.Add(this.tableLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(384, 480);
            this.Name = "MainForm";
            this.Text = "Daily Desktop";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.providerGroupBox.ResumeLayout(false);
            this.descriptionGroupBox.ResumeLayout(false);
            this.descriptionGroupBox.PerformLayout();
            this.optionsGroupBox.ResumeLayout(false);
            this.optionsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.GroupBox providerGroupBox;
        private System.Windows.Forms.TextBox providerDescriptionBox;
        private System.Windows.Forms.ComboBox providerComboBox;
        private System.Windows.Forms.GroupBox optionsGroupBox;
        private System.Windows.Forms.GroupBox descriptionGroupBox;
        private System.Windows.Forms.Label updateTimeLabel;
        private System.Windows.Forms.DateTimePicker updateTimePicker;
    }
}

