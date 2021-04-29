
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.providerGroupBox = new System.Windows.Forms.GroupBox();
            this.descriptionGroupBox = new System.Windows.Forms.GroupBox();
            this.descriptionTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.providerDescriptionLabel = new System.Windows.Forms.Label();
            this.providerSourceLinkLabel = new System.Windows.Forms.LinkLabel();
            this.providerComboBox = new System.Windows.Forms.ComboBox();
            this.okButton = new System.Windows.Forms.Button();
            this.optionsGroupBox = new System.Windows.Forms.GroupBox();
            this.optionsPanel = new System.Windows.Forms.Panel();
            this.blurStrengthTrackBar = new System.Windows.Forms.TrackBar();
            this.blurStrengthLabel = new System.Windows.Forms.Label();
            this.blurredFitCheckBox = new System.Windows.Forms.CheckBox();
            this.updateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.enabledCheckBox = new System.Windows.Forms.CheckBox();
            this.updateTimeLabel = new System.Windows.Forms.Label();
            this.providersDirectoryButton = new System.Windows.Forms.Button();
            this.updateWallpaperButton = new System.Windows.Forms.Button();
            this.bannerPicture = new System.Windows.Forms.PictureBox();
            this.mainToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.mainTableLayout.SuspendLayout();
            this.providerGroupBox.SuspendLayout();
            this.descriptionGroupBox.SuspendLayout();
            this.descriptionTableLayout.SuspendLayout();
            this.optionsGroupBox.SuspendLayout();
            this.optionsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blurStrengthTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bannerPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTableLayout
            // 
            this.mainTableLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTableLayout.AutoScroll = true;
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
            this.mainTableLayout.Size = new System.Drawing.Size(356, 484);
            this.mainTableLayout.TabIndex = 1;
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
            this.providerGroupBox.Size = new System.Drawing.Size(350, 241);
            this.providerGroupBox.TabIndex = 0;
            this.providerGroupBox.TabStop = false;
            this.providerGroupBox.Text = "Provider";
            // 
            // descriptionGroupBox
            // 
            this.descriptionGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionGroupBox.Controls.Add(this.descriptionTableLayout);
            this.descriptionGroupBox.Location = new System.Drawing.Point(6, 51);
            this.descriptionGroupBox.Name = "descriptionGroupBox";
            this.descriptionGroupBox.Size = new System.Drawing.Size(338, 184);
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
            this.descriptionTableLayout.Size = new System.Drawing.Size(325, 155);
            this.descriptionTableLayout.TabIndex = 1;
            // 
            // providerDescriptionLabel
            // 
            this.providerDescriptionLabel.AutoEllipsis = true;
            this.providerDescriptionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.providerDescriptionLabel.Location = new System.Drawing.Point(3, 3);
            this.providerDescriptionLabel.Margin = new System.Windows.Forms.Padding(3);
            this.providerDescriptionLabel.Name = "providerDescriptionLabel";
            this.providerDescriptionLabel.Size = new System.Drawing.Size(319, 129);
            this.providerDescriptionLabel.TabIndex = 0;
            // 
            // providerSourceLinkLabel
            // 
            this.providerSourceLinkLabel.AutoEllipsis = true;
            this.providerSourceLinkLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.providerSourceLinkLabel.Location = new System.Drawing.Point(3, 135);
            this.providerSourceLinkLabel.Name = "providerSourceLinkLabel";
            this.providerSourceLinkLabel.Size = new System.Drawing.Size(319, 20);
            this.providerSourceLinkLabel.TabIndex = 1;
            this.providerSourceLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.providerSourceLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.providerSourceLinkLabel_LinkClicked);
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
            // okButton
            // 
            this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.okButton.Location = new System.Drawing.Point(278, 456);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 8;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // optionsGroupBox
            // 
            this.optionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optionsGroupBox.Controls.Add(this.optionsPanel);
            this.optionsGroupBox.Location = new System.Drawing.Point(3, 250);
            this.optionsGroupBox.Name = "optionsGroupBox";
            this.optionsGroupBox.Size = new System.Drawing.Size(350, 199);
            this.optionsGroupBox.TabIndex = 2;
            this.optionsGroupBox.TabStop = false;
            this.optionsGroupBox.Text = "Options";
            // 
            // optionsPanel
            // 
            this.optionsPanel.AutoScroll = true;
            this.optionsPanel.Controls.Add(this.blurStrengthTrackBar);
            this.optionsPanel.Controls.Add(this.blurStrengthLabel);
            this.optionsPanel.Controls.Add(this.blurredFitCheckBox);
            this.optionsPanel.Controls.Add(this.updateTimePicker);
            this.optionsPanel.Controls.Add(this.enabledCheckBox);
            this.optionsPanel.Controls.Add(this.updateTimeLabel);
            this.optionsPanel.Controls.Add(this.providersDirectoryButton);
            this.optionsPanel.Controls.Add(this.updateWallpaperButton);
            this.optionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionsPanel.Location = new System.Drawing.Point(3, 19);
            this.optionsPanel.Name = "optionsPanel";
            this.optionsPanel.Size = new System.Drawing.Size(344, 177);
            this.optionsPanel.TabIndex = 11;
            // 
            // blurStrengthTrackBar
            // 
            this.blurStrengthTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.blurStrengthTrackBar.LargeChange = 10;
            this.blurStrengthTrackBar.Location = new System.Drawing.Point(151, 69);
            this.blurStrengthTrackBar.Maximum = 100;
            this.blurStrengthTrackBar.Name = "blurStrengthTrackBar";
            this.blurStrengthTrackBar.Size = new System.Drawing.Size(190, 45);
            this.blurStrengthTrackBar.TabIndex = 5;
            this.blurStrengthTrackBar.TickFrequency = 10;
            this.blurStrengthTrackBar.Scroll += new System.EventHandler(this.blurStrengthTrackBar_Scroll);
            // 
            // blurStrengthLabel
            // 
            this.blurStrengthLabel.AutoSize = true;
            this.blurStrengthLabel.Location = new System.Drawing.Point(3, 72);
            this.blurStrengthLabel.Name = "blurStrengthLabel";
            this.blurStrengthLabel.Size = new System.Drawing.Size(142, 15);
            this.blurStrengthLabel.TabIndex = 8;
            this.blurStrengthLabel.Text = "Background blur strength";
            this.mainToolTip.SetToolTip(this.blurStrengthLabel, "Background blur strength. Only applicable if blurred-fit mode is turned on.");
            // 
            // blurredFitCheckBox
            // 
            this.blurredFitCheckBox.AutoSize = true;
            this.blurredFitCheckBox.Location = new System.Drawing.Point(2, 50);
            this.blurredFitCheckBox.Name = "blurredFitCheckBox";
            this.blurredFitCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.blurredFitCheckBox.Size = new System.Drawing.Size(114, 19);
            this.blurredFitCheckBox.TabIndex = 4;
            this.blurredFitCheckBox.Text = "Blurred-fit mode";
            this.mainToolTip.SetToolTip(this.blurredFitCheckBox, resources.GetString("blurredFitCheckBox.ToolTip"));
            this.blurredFitCheckBox.UseVisualStyleBackColor = true;
            this.blurredFitCheckBox.CheckedChanged += new System.EventHandler(this.blurredFitCheckBox_CheckedChanged);
            // 
            // updateTimePicker
            // 
            this.updateTimePicker.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.updateTimePicker.CustomFormat = "h:mm tt";
            this.updateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.updateTimePicker.Location = new System.Drawing.Point(81, 20);
            this.updateTimePicker.Name = "updateTimePicker";
            this.updateTimePicker.ShowUpDown = true;
            this.updateTimePicker.Size = new System.Drawing.Size(92, 23);
            this.updateTimePicker.TabIndex = 3;
            this.updateTimePicker.Value = new System.DateTime(2021, 4, 26, 0, 0, 0, 0);
            this.updateTimePicker.ValueChanged += new System.EventHandler(this.updateTimePicker_ValueChanged);
            // 
            // enabledCheckBox
            // 
            this.enabledCheckBox.AutoSize = true;
            this.enabledCheckBox.Location = new System.Drawing.Point(2, 3);
            this.enabledCheckBox.Name = "enabledCheckBox";
            this.enabledCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.enabledCheckBox.Size = new System.Drawing.Size(68, 19);
            this.enabledCheckBox.TabIndex = 2;
            this.enabledCheckBox.Text = "Enabled";
            this.mainToolTip.SetToolTip(this.enabledCheckBox, "Whether automatic desktop wallpaper update triggers should be enabled or disabled" +
        ".");
            this.enabledCheckBox.UseVisualStyleBackColor = false;
            this.enabledCheckBox.CheckedChanged += new System.EventHandler(this.enabledCheckBox_CheckedChanged);
            // 
            // updateTimeLabel
            // 
            this.updateTimeLabel.AutoSize = true;
            this.updateTimeLabel.Location = new System.Drawing.Point(3, 25);
            this.updateTimeLabel.Name = "updateTimeLabel";
            this.updateTimeLabel.Size = new System.Drawing.Size(72, 15);
            this.updateTimeLabel.TabIndex = 1;
            this.updateTimeLabel.Text = "Update time";
            this.mainToolTip.SetToolTip(this.updateTimeLabel, "When in the day to trigger an automatic desktop wallpaper update.\r\nIf the time is" +
        " missed, then it will trigger on next logon.");
            // 
            // providersDirectoryButton
            // 
            this.providersDirectoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.providersDirectoryButton.Location = new System.Drawing.Point(3, 149);
            this.providersDirectoryButton.Name = "providersDirectoryButton";
            this.providersDirectoryButton.Size = new System.Drawing.Size(338, 23);
            this.providersDirectoryButton.TabIndex = 7;
            this.providersDirectoryButton.Text = "Open providers folder";
            this.providersDirectoryButton.UseVisualStyleBackColor = true;
            this.providersDirectoryButton.Click += new System.EventHandler(this.providersDirectoryButton_Click);
            // 
            // updateWallpaperButton
            // 
            this.updateWallpaperButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.updateWallpaperButton.Location = new System.Drawing.Point(3, 120);
            this.updateWallpaperButton.Name = "updateWallpaperButton";
            this.updateWallpaperButton.Size = new System.Drawing.Size(338, 23);
            this.updateWallpaperButton.TabIndex = 6;
            this.updateWallpaperButton.Text = "Update desktop wallpaper";
            this.updateWallpaperButton.UseVisualStyleBackColor = true;
            this.updateWallpaperButton.Click += new System.EventHandler(this.updateWallpaperButton_Click);
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
            this.ClientSize = new System.Drawing.Size(374, 671);
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
            this.providerGroupBox.ResumeLayout(false);
            this.descriptionGroupBox.ResumeLayout(false);
            this.descriptionTableLayout.ResumeLayout(false);
            this.optionsGroupBox.ResumeLayout(false);
            this.optionsPanel.ResumeLayout(false);
            this.optionsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blurStrengthTrackBar)).EndInit();
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
        private System.Windows.Forms.LinkLabel providerSourceLinkLabel;
        private System.Windows.Forms.ComboBox providerComboBox;
        private System.Windows.Forms.PictureBox bannerPicture;
        private System.Windows.Forms.Panel optionsPanel;
        private System.Windows.Forms.Label providerDescriptionLabel;
        private System.Windows.Forms.CheckBox blurredFitCheckBox;
        private System.Windows.Forms.Label blurStrengthLabel;
        private System.Windows.Forms.TrackBar blurStrengthTrackBar;
        private System.Windows.Forms.ToolTip mainToolTip;
    }
}

