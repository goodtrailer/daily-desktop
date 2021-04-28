
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
            this.mainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.okButton = new System.Windows.Forms.Button();
            this.optionsGroupBox = new System.Windows.Forms.GroupBox();
            this.enabledCheckBox = new System.Windows.Forms.CheckBox();
            this.providersDirectoryButton = new System.Windows.Forms.Button();
            this.updateWallpaperButton = new System.Windows.Forms.Button();
            this.updateTimeLabel = new System.Windows.Forms.Label();
            this.updateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.providerComboBox = new System.Windows.Forms.ComboBox();
            this.descriptionGroupBox = new System.Windows.Forms.GroupBox();
            this.descriptionTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.providerSourceLinkLabel = new System.Windows.Forms.LinkLabel();
            this.providerDescriptionLabel = new System.Windows.Forms.Label();
            this.providerGroupBox = new System.Windows.Forms.GroupBox();
            this.bannerPicture = new System.Windows.Forms.PictureBox();
            this.mainTableLayout.SuspendLayout();
            this.optionsGroupBox.SuspendLayout();
            this.descriptionGroupBox.SuspendLayout();
            this.descriptionTableLayout.SuspendLayout();
            this.providerGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bannerPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTableLayout
            // 
            this.mainTableLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTableLayout.ColumnCount = 1;
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayout.Controls.Add(this.providerGroupBox, 0, 0);
            this.mainTableLayout.Controls.Add(this.okButton, 0, 2);
            this.mainTableLayout.Controls.Add(this.optionsGroupBox, 0, 1);
            this.mainTableLayout.Location = new System.Drawing.Point(9, 178);
            this.mainTableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.mainTableLayout.Name = "mainTableLayout";
            this.mainTableLayout.RowCount = 3;
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.6798F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.3202F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTableLayout.Size = new System.Drawing.Size(356, 374);
            this.mainTableLayout.TabIndex = 1;
            // 
            // okButton
            // 
            this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.okButton.Location = new System.Drawing.Point(278, 346);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 10;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // optionsGroupBox
            // 
            this.optionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optionsGroupBox.Controls.Add(this.enabledCheckBox);
            this.optionsGroupBox.Controls.Add(this.providersDirectoryButton);
            this.optionsGroupBox.Controls.Add(this.updateWallpaperButton);
            this.optionsGroupBox.Controls.Add(this.updateTimeLabel);
            this.optionsGroupBox.Controls.Add(this.updateTimePicker);
            this.optionsGroupBox.Location = new System.Drawing.Point(3, 190);
            this.optionsGroupBox.Name = "optionsGroupBox";
            this.optionsGroupBox.Size = new System.Drawing.Size(350, 149);
            this.optionsGroupBox.TabIndex = 2;
            this.optionsGroupBox.TabStop = false;
            this.optionsGroupBox.Text = "Options";
            // 
            // enabledCheckBox
            // 
            this.enabledCheckBox.AutoSize = true;
            this.enabledCheckBox.Location = new System.Drawing.Point(6, 44);
            this.enabledCheckBox.Name = "enabledCheckBox";
            this.enabledCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.enabledCheckBox.Size = new System.Drawing.Size(68, 19);
            this.enabledCheckBox.TabIndex = 10;
            this.enabledCheckBox.Text = "Enabled";
            this.enabledCheckBox.UseVisualStyleBackColor = false;
            this.enabledCheckBox.CheckedChanged += new System.EventHandler(this.enabledCheckBox_CheckedChanged);
            // 
            // providersDirectoryButton
            // 
            this.providersDirectoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.providersDirectoryButton.Location = new System.Drawing.Point(6, 99);
            this.providersDirectoryButton.Name = "providersDirectoryButton";
            this.providersDirectoryButton.Size = new System.Drawing.Size(338, 23);
            this.providersDirectoryButton.TabIndex = 9;
            this.providersDirectoryButton.Text = "Open providers folder";
            this.providersDirectoryButton.UseVisualStyleBackColor = true;
            this.providersDirectoryButton.Click += new System.EventHandler(this.providersDirectoryButton_Click);
            // 
            // updateWallpaperButton
            // 
            this.updateWallpaperButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.updateWallpaperButton.Location = new System.Drawing.Point(6, 69);
            this.updateWallpaperButton.Name = "updateWallpaperButton";
            this.updateWallpaperButton.Size = new System.Drawing.Size(338, 23);
            this.updateWallpaperButton.TabIndex = 8;
            this.updateWallpaperButton.Text = "Update desktop background";
            this.updateWallpaperButton.UseVisualStyleBackColor = true;
            this.updateWallpaperButton.Click += new System.EventHandler(this.updateWallpaperButton_Click);
            // 
            // updateTimeLabel
            // 
            this.updateTimeLabel.AutoSize = true;
            this.updateTimeLabel.Location = new System.Drawing.Point(6, 19);
            this.updateTimeLabel.Name = "updateTimeLabel";
            this.updateTimeLabel.Size = new System.Drawing.Size(75, 15);
            this.updateTimeLabel.TabIndex = 1;
            this.updateTimeLabel.Text = "Update time:";
            // 
            // updateTimePicker
            // 
            this.updateTimePicker.CustomFormat = "h:mm tt";
            this.updateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.updateTimePicker.Location = new System.Drawing.Point(87, 15);
            this.updateTimePicker.Name = "updateTimePicker";
            this.updateTimePicker.ShowUpDown = true;
            this.updateTimePicker.Size = new System.Drawing.Size(92, 23);
            this.updateTimePicker.TabIndex = 1;
            this.updateTimePicker.Value = new System.DateTime(2021, 4, 26, 0, 0, 0, 0);
            this.updateTimePicker.ValueChanged += new System.EventHandler(this.updateTimePicker_ValueChanged);
            // 
            // providerComboBox
            // 
            this.providerComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.providerComboBox.FormattingEnabled = true;
            this.providerComboBox.Location = new System.Drawing.Point(6, 22);
            this.providerComboBox.Name = "providerComboBox";
            this.providerComboBox.Size = new System.Drawing.Size(338, 23);
            this.providerComboBox.TabIndex = 0;
            this.providerComboBox.DropDown += new System.EventHandler(this.providerComboBox_DropDown);
            this.providerComboBox.SelectedIndexChanged += new System.EventHandler(this.providerComboBox_SelectedIndexChanged);
            // 
            // descriptionGroupBox
            // 
            this.descriptionGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionGroupBox.Controls.Add(this.descriptionTableLayout);
            this.descriptionGroupBox.Location = new System.Drawing.Point(6, 51);
            this.descriptionGroupBox.Name = "descriptionGroupBox";
            this.descriptionGroupBox.Size = new System.Drawing.Size(338, 124);
            this.descriptionGroupBox.TabIndex = 2;
            this.descriptionGroupBox.TabStop = false;
            this.descriptionGroupBox.Text = "Description";
            // 
            // descriptionTableLayout
            // 
            this.descriptionTableLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionTableLayout.ColumnCount = 1;
            this.descriptionTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.descriptionTableLayout.Controls.Add(this.providerDescriptionLabel, 0, 0);
            this.descriptionTableLayout.Controls.Add(this.providerSourceLinkLabel, 0, 1);
            this.descriptionTableLayout.Location = new System.Drawing.Point(7, 23);
            this.descriptionTableLayout.Name = "descriptionTableLayout";
            this.descriptionTableLayout.RowCount = 2;
            this.descriptionTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.descriptionTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.descriptionTableLayout.Size = new System.Drawing.Size(325, 95);
            this.descriptionTableLayout.TabIndex = 1;
            // 
            // providerSourceLinkLabel
            // 
            this.providerSourceLinkLabel.AutoEllipsis = true;
            this.providerSourceLinkLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.providerSourceLinkLabel.Location = new System.Drawing.Point(3, 75);
            this.providerSourceLinkLabel.Name = "providerSourceLinkLabel";
            this.providerSourceLinkLabel.Size = new System.Drawing.Size(319, 20);
            this.providerSourceLinkLabel.TabIndex = 1;
            this.providerSourceLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.providerSourceLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.providerSourceLinkLabel_LinkClicked);
            // 
            // providerDescriptionLabel
            // 
            this.providerDescriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.providerDescriptionLabel.AutoEllipsis = true;
            this.providerDescriptionLabel.Location = new System.Drawing.Point(3, 3);
            this.providerDescriptionLabel.Margin = new System.Windows.Forms.Padding(3);
            this.providerDescriptionLabel.Name = "providerDescriptionLabel";
            this.providerDescriptionLabel.Size = new System.Drawing.Size(319, 69);
            this.providerDescriptionLabel.TabIndex = 0;
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
            this.providerGroupBox.Size = new System.Drawing.Size(350, 181);
            this.providerGroupBox.TabIndex = 0;
            this.providerGroupBox.TabStop = false;
            this.providerGroupBox.Text = "Provider";
            // 
            // bannerPicture
            // 
            this.bannerPicture.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bannerPicture.BackgroundImage")));
            this.bannerPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bannerPicture.ErrorImage = null;
            this.bannerPicture.InitialImage = null;
            this.bannerPicture.Location = new System.Drawing.Point(9, 12);
            this.bannerPicture.Name = "bannerPicture";
            this.bannerPicture.Size = new System.Drawing.Size(356, 163);
            this.bannerPicture.TabIndex = 2;
            this.bannerPicture.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 561);
            this.Controls.Add(this.bannerPicture);
            this.Controls.Add(this.mainTableLayout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(390, 600);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Daily Desktop";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainTableLayout.ResumeLayout(false);
            this.optionsGroupBox.ResumeLayout(false);
            this.optionsGroupBox.PerformLayout();
            this.descriptionGroupBox.ResumeLayout(false);
            this.descriptionTableLayout.ResumeLayout(false);
            this.providerGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bannerPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel mainTableLayout;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.GroupBox optionsGroupBox;
        private System.Windows.Forms.Label updateTimeLabel;
        private System.Windows.Forms.DateTimePicker updateTimePicker;
        private System.Windows.Forms.Button updateWallpaperButton;
        private System.Windows.Forms.Button providersDirectoryButton;
        private System.Windows.Forms.CheckBox enabledCheckBox;
        private System.Windows.Forms.GroupBox providerGroupBox;
        private System.Windows.Forms.GroupBox descriptionGroupBox;
        private System.Windows.Forms.TableLayoutPanel descriptionTableLayout;
        private System.Windows.Forms.Label providerDescriptionLabel;
        private System.Windows.Forms.LinkLabel providerSourceLinkLabel;
        private System.Windows.Forms.ComboBox providerComboBox;
        private System.Windows.Forms.PictureBox bannerPicture;
    }
}

