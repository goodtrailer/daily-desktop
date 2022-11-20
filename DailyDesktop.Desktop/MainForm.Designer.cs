﻿
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
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.stateLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.mainTabPage = new System.Windows.Forms.TabPage();
            this.mainTableLayout2 = new System.Windows.Forms.TableLayoutPanel();
            this.providerGroupBox = new System.Windows.Forms.GroupBox();
            this.providerDescriptionTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.providerDescriptionLabel = new System.Windows.Forms.Label();
            this.providerSourceLinkLabel = new System.Windows.Forms.LinkLabel();
            this.providerComboBox = new System.Windows.Forms.ComboBox();
            this.wallpaperGroupBox = new System.Windows.Forms.GroupBox();
            this.wallpaperDescriptionRichTextBox = new System.Windows.Forms.RichTextBox();
            this.wallpaperCreditFlowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.wallpaperTitleLinkLabel = new System.Windows.Forms.LinkLabel();
            this.wallpaperCreditLabel = new System.Windows.Forms.Label();
            this.wallpaperAuthorLinkLabel = new System.Windows.Forms.LinkLabel();
            this.wallpaperUpdatedLabel = new System.Windows.Forms.Label();
            this.optionsTabPage = new System.Windows.Forms.TabPage();
            this.optionsPanel = new System.Windows.Forms.Panel();
            this.optionsResizeCheckBox = new System.Windows.Forms.CheckBox();
            this.optionsBlurStrengthTrackBar = new System.Windows.Forms.TrackBar();
            this.optionsBlurStrengthLabel = new System.Windows.Forms.Label();
            this.optionsBlurredFitCheckBox = new System.Windows.Forms.CheckBox();
            this.optionsUpdateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.optionsEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.optionsUpdateTimeLabel = new System.Windows.Forms.Label();
            this.optionsProvidersDirectoryButton = new System.Windows.Forms.Button();
            this.optionsUpdateWallpaperButton = new System.Windows.Forms.Button();
            this.bannerPicture = new System.Windows.Forms.PictureBox();
            this.mainToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.stateBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.mainTableLayout.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.mainTabPage.SuspendLayout();
            this.mainTableLayout2.SuspendLayout();
            this.providerGroupBox.SuspendLayout();
            this.providerDescriptionTableLayout.SuspendLayout();
            this.wallpaperGroupBox.SuspendLayout();
            this.wallpaperCreditFlowLayout.SuspendLayout();
            this.optionsTabPage.SuspendLayout();
            this.optionsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optionsBlurStrengthTrackBar)).BeginInit();
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
            this.mainTableLayout.Controls.Add(this.bottomPanel, 0, 1);
            this.mainTableLayout.Controls.Add(this.mainTabControl, 0, 0);
            this.mainTableLayout.Location = new System.Drawing.Point(10, 237);
            this.mainTableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.mainTableLayout.Name = "mainTableLayout";
            this.mainTableLayout.RowCount = 2;
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.mainTableLayout.Size = new System.Drawing.Size(418, 645);
            this.mainTableLayout.TabIndex = 1;
            // 
            // bottomPanel
            // 
            this.bottomPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bottomPanel.Controls.Add(this.stateLabel);
            this.bottomPanel.Controls.Add(this.okButton);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomPanel.Location = new System.Drawing.Point(0, 604);
            this.bottomPanel.Margin = new System.Windows.Forms.Padding(0);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(418, 41);
            this.bottomPanel.TabIndex = 3;
            // 
            // stateLabel
            // 
            this.stateLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.stateLabel.AutoSize = true;
            this.stateLabel.Location = new System.Drawing.Point(3, 9);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(70, 20);
            this.stateLabel.TabIndex = 9;
            this.stateLabel.Text = "Unknown";
            // 
            // okButton
            // 
            this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.okButton.Location = new System.Drawing.Point(329, 4);
            this.okButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(86, 31);
            this.okButton.TabIndex = 10;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.mainTabPage);
            this.mainTabControl.Controls.Add(this.optionsTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.HotTrack = true;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(418, 604);
            this.mainTabControl.TabIndex = 0;
            // 
            // mainTabPage
            // 
            this.mainTabPage.BackColor = System.Drawing.SystemColors.Window;
            this.mainTabPage.Controls.Add(this.mainTableLayout2);
            this.mainTabPage.Location = new System.Drawing.Point(4, 29);
            this.mainTabPage.Margin = new System.Windows.Forms.Padding(0);
            this.mainTabPage.Name = "mainTabPage";
            this.mainTabPage.Size = new System.Drawing.Size(410, 571);
            this.mainTabPage.TabIndex = 0;
            this.mainTabPage.Text = "Main";
            // 
            // mainTableLayout2
            // 
            this.mainTableLayout2.ColumnCount = 1;
            this.mainTableLayout2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainTableLayout2.Controls.Add(this.providerGroupBox, 0, 0);
            this.mainTableLayout2.Controls.Add(this.wallpaperGroupBox, 0, 1);
            this.mainTableLayout2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayout2.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayout2.Margin = new System.Windows.Forms.Padding(0);
            this.mainTableLayout2.Name = "mainTableLayout2";
            this.mainTableLayout2.RowCount = 2;
            this.mainTableLayout2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainTableLayout2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainTableLayout2.Size = new System.Drawing.Size(410, 571);
            this.mainTableLayout2.TabIndex = 0;
            // 
            // providerGroupBox
            // 
            this.providerGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.providerGroupBox.Controls.Add(this.providerDescriptionTableLayout);
            this.providerGroupBox.Controls.Add(this.providerComboBox);
            this.providerGroupBox.Location = new System.Drawing.Point(3, 4);
            this.providerGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.providerGroupBox.Name = "providerGroupBox";
            this.providerGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.providerGroupBox.Size = new System.Drawing.Size(404, 277);
            this.providerGroupBox.TabIndex = 1;
            this.providerGroupBox.TabStop = false;
            this.providerGroupBox.Text = "Provider";
            // 
            // providerDescriptionTableLayout
            // 
            this.providerDescriptionTableLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.providerDescriptionTableLayout.ColumnCount = 1;
            this.providerDescriptionTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.providerDescriptionTableLayout.Controls.Add(this.providerDescriptionLabel, 0, 0);
            this.providerDescriptionTableLayout.Controls.Add(this.providerSourceLinkLabel, 0, 1);
            this.providerDescriptionTableLayout.Location = new System.Drawing.Point(7, 68);
            this.providerDescriptionTableLayout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.providerDescriptionTableLayout.Name = "providerDescriptionTableLayout";
            this.providerDescriptionTableLayout.RowCount = 2;
            this.providerDescriptionTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.providerDescriptionTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.providerDescriptionTableLayout.Size = new System.Drawing.Size(391, 201);
            this.providerDescriptionTableLayout.TabIndex = 2;
            // 
            // providerDescriptionLabel
            // 
            this.providerDescriptionLabel.AutoEllipsis = true;
            this.providerDescriptionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.providerDescriptionLabel.Location = new System.Drawing.Point(3, 4);
            this.providerDescriptionLabel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.providerDescriptionLabel.Name = "providerDescriptionLabel";
            this.providerDescriptionLabel.Size = new System.Drawing.Size(385, 166);
            this.providerDescriptionLabel.TabIndex = 0;
            // 
            // providerSourceLinkLabel
            // 
            this.providerSourceLinkLabel.AutoEllipsis = true;
            this.providerSourceLinkLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.providerSourceLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.providerSourceLinkLabel.Location = new System.Drawing.Point(0, 174);
            this.providerSourceLinkLabel.Margin = new System.Windows.Forms.Padding(0);
            this.providerSourceLinkLabel.Name = "providerSourceLinkLabel";
            this.providerSourceLinkLabel.Size = new System.Drawing.Size(391, 27);
            this.providerSourceLinkLabel.TabIndex = 2;
            this.providerSourceLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.providerSourceLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.providerSourceLinkLabel_LinkClicked);
            // 
            // providerComboBox
            // 
            this.providerComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.providerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.providerComboBox.FormattingEnabled = true;
            this.providerComboBox.Location = new System.Drawing.Point(7, 29);
            this.providerComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.providerComboBox.Name = "providerComboBox";
            this.providerComboBox.Size = new System.Drawing.Size(390, 28);
            this.providerComboBox.TabIndex = 1;
            this.providerComboBox.DropDown += new System.EventHandler(this.providerComboBox_DropDown);
            this.providerComboBox.SelectedIndexChanged += new System.EventHandler(this.providerComboBox_SelectedIndexChanged);
            // 
            // wallpaperGroupBox
            // 
            this.wallpaperGroupBox.Controls.Add(this.wallpaperDescriptionRichTextBox);
            this.wallpaperGroupBox.Controls.Add(this.wallpaperCreditFlowLayout);
            this.wallpaperGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wallpaperGroupBox.Location = new System.Drawing.Point(3, 289);
            this.wallpaperGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.wallpaperGroupBox.Name = "wallpaperGroupBox";
            this.wallpaperGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.wallpaperGroupBox.Size = new System.Drawing.Size(404, 278);
            this.wallpaperGroupBox.TabIndex = 2;
            this.wallpaperGroupBox.TabStop = false;
            this.wallpaperGroupBox.Text = "Wallpaper";
            // 
            // wallpaperDescriptionRichTextBox
            // 
            this.wallpaperDescriptionRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wallpaperDescriptionRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.wallpaperDescriptionRichTextBox.Location = new System.Drawing.Point(7, 64);
            this.wallpaperDescriptionRichTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.wallpaperDescriptionRichTextBox.Name = "wallpaperDescriptionRichTextBox";
            this.wallpaperDescriptionRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.wallpaperDescriptionRichTextBox.Size = new System.Drawing.Size(391, 206);
            this.wallpaperDescriptionRichTextBox.TabIndex = 1;
            this.wallpaperDescriptionRichTextBox.TabStop = false;
            this.wallpaperDescriptionRichTextBox.Text = "No description.";
            this.wallpaperDescriptionRichTextBox.LinkClicked += wallpaperDescriptionRichTextBox_LinkClicked;
            // 
            // wallpaperCreditFlowLayout
            // 
            this.wallpaperCreditFlowLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wallpaperCreditFlowLayout.Controls.Add(this.wallpaperTitleLinkLabel);
            this.wallpaperCreditFlowLayout.Controls.Add(this.wallpaperCreditLabel);
            this.wallpaperCreditFlowLayout.Controls.Add(this.wallpaperAuthorLinkLabel);
            this.wallpaperCreditFlowLayout.Controls.Add(this.wallpaperUpdatedLabel);
            this.wallpaperCreditFlowLayout.Location = new System.Drawing.Point(7, 29);
            this.wallpaperCreditFlowLayout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.wallpaperCreditFlowLayout.Name = "wallpaperCreditFlowLayout";
            this.wallpaperCreditFlowLayout.Size = new System.Drawing.Size(391, 27);
            this.wallpaperCreditFlowLayout.TabIndex = 0;
            this.wallpaperCreditFlowLayout.WrapContents = false;
            // 
            // wallpaperTitleLinkLabel
            // 
            this.wallpaperTitleLinkLabel.AutoSize = true;
            this.wallpaperTitleLinkLabel.DisabledLinkColor = System.Drawing.SystemColors.ControlText;
            this.wallpaperTitleLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.wallpaperTitleLinkLabel.Location = new System.Drawing.Point(0, 0);
            this.wallpaperTitleLinkLabel.Margin = new System.Windows.Forms.Padding(0);
            this.wallpaperTitleLinkLabel.Name = "wallpaperTitleLinkLabel";
            this.wallpaperTitleLinkLabel.Size = new System.Drawing.Size(33, 20);
            this.wallpaperTitleLinkLabel.TabIndex = 3;
            this.wallpaperTitleLinkLabel.TabStop = true;
            this.wallpaperTitleLinkLabel.Text = "null";
            this.wallpaperTitleLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.wallpaperTitleLinkLabel_LinkClicked);
            // 
            // wallpaperCreditLabel
            // 
            this.wallpaperCreditLabel.AutoSize = true;
            this.wallpaperCreditLabel.Location = new System.Drawing.Point(33, 0);
            this.wallpaperCreditLabel.Margin = new System.Windows.Forms.Padding(0);
            this.wallpaperCreditLabel.Name = "wallpaperCreditLabel";
            this.wallpaperCreditLabel.Size = new System.Drawing.Size(25, 20);
            this.wallpaperCreditLabel.TabIndex = 1;
            this.wallpaperCreditLabel.Text = "by";
            // 
            // wallpaperAuthorLinkLabel
            // 
            this.wallpaperAuthorLinkLabel.AutoSize = true;
            this.wallpaperAuthorLinkLabel.DisabledLinkColor = System.Drawing.SystemColors.ControlText;
            this.wallpaperAuthorLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.wallpaperAuthorLinkLabel.Location = new System.Drawing.Point(58, 0);
            this.wallpaperAuthorLinkLabel.Margin = new System.Windows.Forms.Padding(0);
            this.wallpaperAuthorLinkLabel.Name = "wallpaperAuthorLinkLabel";
            this.wallpaperAuthorLinkLabel.Size = new System.Drawing.Size(33, 20);
            this.wallpaperAuthorLinkLabel.TabIndex = 4;
            this.wallpaperAuthorLinkLabel.TabStop = true;
            this.wallpaperAuthorLinkLabel.Text = "null";
            this.wallpaperAuthorLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.wallpaperAuthorLinkLabel_LinkClicked);
            // 
            // wallpaperUpdatedLabel
            // 
            this.wallpaperUpdatedLabel.AutoSize = true;
            this.wallpaperUpdatedLabel.Location = new System.Drawing.Point(91, 0);
            this.wallpaperUpdatedLabel.Margin = new System.Windows.Forms.Padding(0);
            this.wallpaperUpdatedLabel.Name = "wallpaperUpdatedLabel";
            this.wallpaperUpdatedLabel.Size = new System.Drawing.Size(108, 20);
            this.wallpaperUpdatedLabel.TabIndex = 3;
            this.wallpaperUpdatedLabel.Text = "fetched on null";
            this.wallpaperUpdatedLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // optionsTabPage
            // 
            this.optionsTabPage.BackColor = System.Drawing.SystemColors.Window;
            this.optionsTabPage.Controls.Add(this.optionsPanel);
            this.optionsTabPage.Location = new System.Drawing.Point(4, 29);
            this.optionsTabPage.Margin = new System.Windows.Forms.Padding(0);
            this.optionsTabPage.Name = "optionsTabPage";
            this.optionsTabPage.Size = new System.Drawing.Size(410, 571);
            this.optionsTabPage.TabIndex = 1;
            this.optionsTabPage.Text = "Options";
            // 
            // optionsPanel
            // 
            this.optionsPanel.AutoScroll = true;
            this.optionsPanel.Controls.Add(this.optionsResizeCheckBox);
            this.optionsPanel.Controls.Add(this.optionsBlurStrengthTrackBar);
            this.optionsPanel.Controls.Add(this.optionsBlurStrengthLabel);
            this.optionsPanel.Controls.Add(this.optionsBlurredFitCheckBox);
            this.optionsPanel.Controls.Add(this.optionsUpdateTimePicker);
            this.optionsPanel.Controls.Add(this.optionsEnabledCheckBox);
            this.optionsPanel.Controls.Add(this.optionsUpdateTimeLabel);
            this.optionsPanel.Controls.Add(this.optionsProvidersDirectoryButton);
            this.optionsPanel.Controls.Add(this.optionsUpdateWallpaperButton);
            this.optionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionsPanel.Location = new System.Drawing.Point(0, 0);
            this.optionsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.optionsPanel.Name = "optionsPanel";
            this.optionsPanel.Size = new System.Drawing.Size(410, 571);
            this.optionsPanel.TabIndex = 11;
            // 
            // optionsResizeCheckBox
            // 
            this.optionsResizeCheckBox.AutoSize = true;
            this.optionsResizeCheckBox.Location = new System.Drawing.Point(2, 62);
            this.optionsResizeCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.optionsResizeCheckBox.Name = "optionsResizeCheckBox";
            this.optionsResizeCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.optionsResizeCheckBox.Size = new System.Drawing.Size(137, 24);
            this.optionsResizeCheckBox.TabIndex = 3;
            this.optionsResizeCheckBox.Text = "Resize to screen";
            this.mainToolTip.SetToolTip(this.optionsResizeCheckBox, "Sizes wallpaper to screen resolution if larger. May slightly\r\nreduce image qualit" +
        "y, but may also significantly improve\r\ncomputaitonal/storage performance.");
            this.optionsResizeCheckBox.UseVisualStyleBackColor = true;
            this.optionsResizeCheckBox.CheckedChanged += new System.EventHandler(this.optionsResizeCheckBox_CheckedChanged);
            // 
            // optionsBlurStrengthTrackBar
            // 
            this.optionsBlurStrengthTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optionsBlurStrengthTrackBar.LargeChange = 10;
            this.optionsBlurStrengthTrackBar.Location = new System.Drawing.Point(173, 119);
            this.optionsBlurStrengthTrackBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.optionsBlurStrengthTrackBar.Maximum = 100;
            this.optionsBlurStrengthTrackBar.Name = "optionsBlurStrengthTrackBar";
            this.optionsBlurStrengthTrackBar.Size = new System.Drawing.Size(234, 56);
            this.optionsBlurStrengthTrackBar.TabIndex = 5;
            this.optionsBlurStrengthTrackBar.TickFrequency = 10;
            this.optionsBlurStrengthTrackBar.Scroll += new System.EventHandler(this.optionsBlurStrengthTrackBar_Scroll);
            // 
            // optionsBlurStrengthLabel
            // 
            this.optionsBlurStrengthLabel.AutoSize = true;
            this.optionsBlurStrengthLabel.Location = new System.Drawing.Point(3, 123);
            this.optionsBlurStrengthLabel.Name = "optionsBlurStrengthLabel";
            this.optionsBlurStrengthLabel.Size = new System.Drawing.Size(176, 20);
            this.optionsBlurStrengthLabel.TabIndex = 9;
            this.optionsBlurStrengthLabel.Text = "Background blur strength";
            this.mainToolTip.SetToolTip(this.optionsBlurStrengthLabel, "Background blur strength. Only applicable if blurred-fit\r\nmode is turned on. High" +
        "er blur strengths do not require\r\nextra computation time.");
            // 
            // optionsBlurredFitCheckBox
            // 
            this.optionsBlurredFitCheckBox.AutoSize = true;
            this.optionsBlurredFitCheckBox.Location = new System.Drawing.Point(2, 94);
            this.optionsBlurredFitCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.optionsBlurredFitCheckBox.Name = "optionsBlurredFitCheckBox";
            this.optionsBlurredFitCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.optionsBlurredFitCheckBox.Size = new System.Drawing.Size(142, 24);
            this.optionsBlurredFitCheckBox.TabIndex = 4;
            this.optionsBlurredFitCheckBox.Text = "Blurred-fit mode";
            this.mainToolTip.SetToolTip(this.optionsBlurredFitCheckBox, resources.GetString("optionsBlurredFitCheckBox.ToolTip"));
            this.optionsBlurredFitCheckBox.UseVisualStyleBackColor = true;
            this.optionsBlurredFitCheckBox.CheckedChanged += new System.EventHandler(this.optionsBlurredFitCheckBox_CheckedChanged);
            // 
            // optionsUpdateTimePicker
            // 
            this.optionsUpdateTimePicker.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.optionsUpdateTimePicker.CustomFormat = "h:mm tt";
            this.optionsUpdateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.optionsUpdateTimePicker.Location = new System.Drawing.Point(93, 27);
            this.optionsUpdateTimePicker.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.optionsUpdateTimePicker.Name = "optionsUpdateTimePicker";
            this.optionsUpdateTimePicker.ShowUpDown = true;
            this.optionsUpdateTimePicker.Size = new System.Drawing.Size(105, 27);
            this.optionsUpdateTimePicker.TabIndex = 2;
            this.optionsUpdateTimePicker.Value = new System.DateTime(2021, 4, 26, 0, 0, 0, 0);
            this.optionsUpdateTimePicker.ValueChanged += new System.EventHandler(this.optionsUpdateTimePicker_ValueChanged);
            // 
            // optionsEnabledCheckBox
            // 
            this.optionsEnabledCheckBox.AutoSize = true;
            this.optionsEnabledCheckBox.Location = new System.Drawing.Point(2, 4);
            this.optionsEnabledCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.optionsEnabledCheckBox.Name = "optionsEnabledCheckBox";
            this.optionsEnabledCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.optionsEnabledCheckBox.Size = new System.Drawing.Size(85, 24);
            this.optionsEnabledCheckBox.TabIndex = 1;
            this.optionsEnabledCheckBox.Text = "Enabled";
            this.mainToolTip.SetToolTip(this.optionsEnabledCheckBox, "Whether automatic desktop wallpaper update triggers should be enabled or disabled" +
        ".");
            this.optionsEnabledCheckBox.UseVisualStyleBackColor = false;
            this.optionsEnabledCheckBox.CheckedChanged += new System.EventHandler(this.optionsEnabledCheckBox_CheckedChanged);
            // 
            // optionsUpdateTimeLabel
            // 
            this.optionsUpdateTimeLabel.AutoSize = true;
            this.optionsUpdateTimeLabel.Location = new System.Drawing.Point(3, 33);
            this.optionsUpdateTimeLabel.Name = "optionsUpdateTimeLabel";
            this.optionsUpdateTimeLabel.Size = new System.Drawing.Size(92, 20);
            this.optionsUpdateTimeLabel.TabIndex = 1;
            this.optionsUpdateTimeLabel.Text = "Update time";
            this.mainToolTip.SetToolTip(this.optionsUpdateTimeLabel, "When in the day to trigger an automatic desktop\r\nwallpaper update.\r\nIf the time i" +
        "s missed, then it will trigger on next logon.");
            // 
            // optionsProvidersDirectoryButton
            // 
            this.optionsProvidersDirectoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optionsProvidersDirectoryButton.Location = new System.Drawing.Point(3, 226);
            this.optionsProvidersDirectoryButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.optionsProvidersDirectoryButton.Name = "optionsProvidersDirectoryButton";
            this.optionsProvidersDirectoryButton.Size = new System.Drawing.Size(403, 31);
            this.optionsProvidersDirectoryButton.TabIndex = 7;
            this.optionsProvidersDirectoryButton.Text = "Open providers folder";
            this.optionsProvidersDirectoryButton.UseVisualStyleBackColor = true;
            this.optionsProvidersDirectoryButton.Click += new System.EventHandler(this.optionsProvidersDirectoryButton_Click);
            // 
            // optionsUpdateWallpaperButton
            // 
            this.optionsUpdateWallpaperButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optionsUpdateWallpaperButton.Location = new System.Drawing.Point(3, 187);
            this.optionsUpdateWallpaperButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.optionsUpdateWallpaperButton.Name = "optionsUpdateWallpaperButton";
            this.optionsUpdateWallpaperButton.Size = new System.Drawing.Size(403, 31);
            this.optionsUpdateWallpaperButton.TabIndex = 6;
            this.optionsUpdateWallpaperButton.Text = "Update desktop wallpaper";
            this.optionsUpdateWallpaperButton.UseVisualStyleBackColor = true;
            this.optionsUpdateWallpaperButton.Click += new System.EventHandler(this.optionsUpdateWallpaperButton_Click);
            // 
            // bannerPicture
            // 
            this.bannerPicture.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bannerPicture.BackgroundImage")));
            this.bannerPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bannerPicture.ErrorImage = null;
            this.bannerPicture.InitialImage = null;
            this.bannerPicture.Location = new System.Drawing.Point(10, 16);
            this.bannerPicture.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bannerPicture.Name = "bannerPicture";
            this.bannerPicture.Size = new System.Drawing.Size(407, 217);
            this.bannerPicture.TabIndex = 2;
            this.bannerPicture.TabStop = false;
            // 
            // stateBackgroundWorker
            // 
            this.stateBackgroundWorker.WorkerReportsProgress = true;
            this.stateBackgroundWorker.WorkerSupportsCancellation = true;
            this.stateBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.stateBackgroundWorker_DoWork);
            this.stateBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.stateBackgroundWorker_ProgressChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 895);
            this.Controls.Add(this.mainTableLayout);
            this.Controls.Add(this.bannerPicture);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(443, 784);
            this.Name = "MainForm";
            this.Text = "Daily Desktop";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainTableLayout.ResumeLayout(false);
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.mainTabPage.ResumeLayout(false);
            this.mainTableLayout2.ResumeLayout(false);
            this.providerGroupBox.ResumeLayout(false);
            this.providerDescriptionTableLayout.ResumeLayout(false);
            this.wallpaperGroupBox.ResumeLayout(false);
            this.wallpaperCreditFlowLayout.ResumeLayout(false);
            this.wallpaperCreditFlowLayout.PerformLayout();
            this.optionsTabPage.ResumeLayout(false);
            this.optionsPanel.ResumeLayout(false);
            this.optionsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optionsBlurStrengthTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bannerPicture)).EndInit();
            this.ResumeLayout(false);

        }

        private void WallpaperDescriptionRichTextBox_LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel mainTableLayout;
        private System.Windows.Forms.Label optionsUpdateTimeLabel;
        private System.Windows.Forms.DateTimePicker optionsUpdateTimePicker;
        private System.Windows.Forms.Button optionsUpdateWallpaperButton;
        private System.Windows.Forms.Button optionsProvidersDirectoryButton;
        private System.Windows.Forms.CheckBox optionsEnabledCheckBox;
        private System.Windows.Forms.GroupBox providerGroupBox;
        private System.Windows.Forms.TableLayoutPanel providerDescriptionTableLayout;
        private System.Windows.Forms.LinkLabel providerSourceLinkLabel;
        private System.Windows.Forms.ComboBox providerComboBox;
        private System.Windows.Forms.PictureBox bannerPicture;
        private System.Windows.Forms.Panel optionsPanel;
        private System.Windows.Forms.Label providerDescriptionLabel;
        private System.Windows.Forms.CheckBox optionsBlurredFitCheckBox;
        private System.Windows.Forms.Label optionsBlurStrengthLabel;
        private System.Windows.Forms.TrackBar optionsBlurStrengthTrackBar;
        private System.Windows.Forms.ToolTip mainToolTip;
        private System.ComponentModel.BackgroundWorker stateBackgroundWorker;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage mainTabPage;
        private System.Windows.Forms.TabPage optionsTabPage;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TableLayoutPanel mainTableLayout2;
        private System.Windows.Forms.GroupBox wallpaperGroupBox;
        private System.Windows.Forms.FlowLayoutPanel wallpaperCreditFlowLayout;
        private System.Windows.Forms.LinkLabel wallpaperTitleLinkLabel;
        private System.Windows.Forms.Label wallpaperCreditLabel;
        private System.Windows.Forms.LinkLabel wallpaperAuthorLinkLabel;
        private System.Windows.Forms.Label wallpaperUpdatedLabel;
        private System.Windows.Forms.CheckBox optionsResizeCheckBox;
        private System.Windows.Forms.RichTextBox wallpaperDescriptionRichTextBox;
    }
}

