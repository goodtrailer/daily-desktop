
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
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            mainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            bottomPanel = new System.Windows.Forms.Panel();
            stateLabel = new System.Windows.Forms.Label();
            okButton = new System.Windows.Forms.Button();
            mainTabControl = new System.Windows.Forms.TabControl();
            mainTabPage = new System.Windows.Forms.TabPage();
            mainTableLayout2 = new System.Windows.Forms.TableLayoutPanel();
            wallpaperGroupBox = new System.Windows.Forms.GroupBox();
            wallpaperDescriptionRichTextBox = new System.Windows.Forms.RichTextBox();
            wallpaperCreditFlowLayout = new System.Windows.Forms.FlowLayoutPanel();
            wallpaperTitleLinkLabel = new System.Windows.Forms.LinkLabel();
            wallpaperCreditLabel = new System.Windows.Forms.Label();
            wallpaperAuthorLinkLabel = new System.Windows.Forms.LinkLabel();
            wallpaperUpdatedLabel = new System.Windows.Forms.Label();
            providerGroupBox = new System.Windows.Forms.GroupBox();
            providerDescriptionTableLayout = new System.Windows.Forms.TableLayoutPanel();
            providerSourceLinkLabel = new System.Windows.Forms.LinkLabel();
            providerDescriptionLabel = new System.Windows.Forms.Label();
            providerComboBox = new System.Windows.Forms.ComboBox();
            optionsTabPage = new System.Windows.Forms.TabPage();
            optionsPanel = new System.Windows.Forms.Panel();
            optionsResizeCheckBox = new System.Windows.Forms.CheckBox();
            optionsBlurStrengthTrackBar = new System.Windows.Forms.TrackBar();
            optionsBlurStrengthLabel = new System.Windows.Forms.Label();
            optionsBlurredFitCheckBox = new System.Windows.Forms.CheckBox();
            optionsUpdateTimePicker = new System.Windows.Forms.DateTimePicker();
            optionsEnabledCheckBox = new System.Windows.Forms.CheckBox();
            optionsUpdateTimeLabel = new System.Windows.Forms.Label();
            optionsProvidersDirectoryButton = new System.Windows.Forms.Button();
            optionsUpdateWallpaperButton = new System.Windows.Forms.Button();
            aboutTabPage = new System.Windows.Forms.TabPage();
            aboutTableLayout = new System.Windows.Forms.TableLayoutPanel();
            licenseGroupBox = new System.Windows.Forms.GroupBox();
            licenseRichTextBox = new System.Windows.Forms.RichTextBox();
            overviewGroupBox = new System.Windows.Forms.GroupBox();
            overviewRichTextBox = new System.Windows.Forms.RichTextBox();
            bannerPicture = new System.Windows.Forms.PictureBox();
            mainToolTip = new System.Windows.Forms.ToolTip(components);
            stateBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            mainTableLayout.SuspendLayout();
            bottomPanel.SuspendLayout();
            mainTabControl.SuspendLayout();
            mainTabPage.SuspendLayout();
            mainTableLayout2.SuspendLayout();
            wallpaperGroupBox.SuspendLayout();
            wallpaperCreditFlowLayout.SuspendLayout();
            providerGroupBox.SuspendLayout();
            providerDescriptionTableLayout.SuspendLayout();
            optionsTabPage.SuspendLayout();
            optionsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)optionsBlurStrengthTrackBar).BeginInit();
            aboutTabPage.SuspendLayout();
            aboutTableLayout.SuspendLayout();
            licenseGroupBox.SuspendLayout();
            overviewGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bannerPicture).BeginInit();
            SuspendLayout();
            // 
            // mainTableLayout
            // 
            mainTableLayout.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            mainTableLayout.AutoScroll = true;
            mainTableLayout.ColumnCount = 1;
            mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            mainTableLayout.Controls.Add(bottomPanel, 0, 1);
            mainTableLayout.Controls.Add(mainTabControl, 0, 0);
            mainTableLayout.Location = new System.Drawing.Point(10, 237);
            mainTableLayout.Margin = new System.Windows.Forms.Padding(0);
            mainTableLayout.Name = "mainTableLayout";
            mainTableLayout.RowCount = 2;
            mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            mainTableLayout.Size = new System.Drawing.Size(418, 645);
            mainTableLayout.TabIndex = 1;
            // 
            // bottomPanel
            // 
            bottomPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            bottomPanel.Controls.Add(stateLabel);
            bottomPanel.Controls.Add(okButton);
            bottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            bottomPanel.Location = new System.Drawing.Point(0, 604);
            bottomPanel.Margin = new System.Windows.Forms.Padding(0);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Size = new System.Drawing.Size(418, 41);
            bottomPanel.TabIndex = 3;
            // 
            // stateLabel
            // 
            stateLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            stateLabel.AutoSize = true;
            stateLabel.Location = new System.Drawing.Point(3, 9);
            stateLabel.Name = "stateLabel";
            stateLabel.Size = new System.Drawing.Size(70, 20);
            stateLabel.TabIndex = 9;
            stateLabel.Text = "Unknown";
            // 
            // okButton
            // 
            okButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            okButton.Location = new System.Drawing.Point(329, 4);
            okButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(86, 31);
            okButton.TabIndex = 10;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // mainTabControl
            // 
            mainTabControl.Controls.Add(mainTabPage);
            mainTabControl.Controls.Add(optionsTabPage);
            mainTabControl.Controls.Add(aboutTabPage);
            mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            mainTabControl.HotTrack = true;
            mainTabControl.Location = new System.Drawing.Point(0, 0);
            mainTabControl.Margin = new System.Windows.Forms.Padding(0);
            mainTabControl.Name = "mainTabControl";
            mainTabControl.SelectedIndex = 0;
            mainTabControl.Size = new System.Drawing.Size(418, 604);
            mainTabControl.TabIndex = 0;
            // 
            // mainTabPage
            // 
            mainTabPage.BackColor = System.Drawing.SystemColors.Window;
            mainTabPage.Controls.Add(mainTableLayout2);
            mainTabPage.Location = new System.Drawing.Point(4, 29);
            mainTabPage.Margin = new System.Windows.Forms.Padding(0);
            mainTabPage.Name = "mainTabPage";
            mainTabPage.Size = new System.Drawing.Size(410, 571);
            mainTabPage.TabIndex = 0;
            mainTabPage.Text = "Main";
            // 
            // mainTableLayout2
            // 
            mainTableLayout2.ColumnCount = 1;
            mainTableLayout2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            mainTableLayout2.Controls.Add(wallpaperGroupBox, 0, 1);
            mainTableLayout2.Controls.Add(providerGroupBox, 0, 0);
            mainTableLayout2.Dock = System.Windows.Forms.DockStyle.Fill;
            mainTableLayout2.Location = new System.Drawing.Point(0, 0);
            mainTableLayout2.Margin = new System.Windows.Forms.Padding(0);
            mainTableLayout2.Name = "mainTableLayout2";
            mainTableLayout2.RowCount = 2;
            mainTableLayout2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            mainTableLayout2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            mainTableLayout2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            mainTableLayout2.Size = new System.Drawing.Size(410, 571);
            mainTableLayout2.TabIndex = 0;
            // 
            // wallpaperGroupBox
            // 
            wallpaperGroupBox.Controls.Add(wallpaperDescriptionRichTextBox);
            wallpaperGroupBox.Controls.Add(wallpaperCreditFlowLayout);
            wallpaperGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            wallpaperGroupBox.Location = new System.Drawing.Point(3, 289);
            wallpaperGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            wallpaperGroupBox.Name = "wallpaperGroupBox";
            wallpaperGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            wallpaperGroupBox.Size = new System.Drawing.Size(404, 278);
            wallpaperGroupBox.TabIndex = 2;
            wallpaperGroupBox.TabStop = false;
            wallpaperGroupBox.Text = "Wallpaper";
            // 
            // wallpaperDescriptionRichTextBox
            // 
            wallpaperDescriptionRichTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            wallpaperDescriptionRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            wallpaperDescriptionRichTextBox.Location = new System.Drawing.Point(13, 64);
            wallpaperDescriptionRichTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            wallpaperDescriptionRichTextBox.Name = "wallpaperDescriptionRichTextBox";
            wallpaperDescriptionRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            wallpaperDescriptionRichTextBox.Size = new System.Drawing.Size(378, 206);
            wallpaperDescriptionRichTextBox.TabIndex = 1;
            wallpaperDescriptionRichTextBox.TabStop = false;
            wallpaperDescriptionRichTextBox.Text = "No description.";
            // 
            // wallpaperCreditFlowLayout
            // 
            wallpaperCreditFlowLayout.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            wallpaperCreditFlowLayout.Controls.Add(wallpaperTitleLinkLabel);
            wallpaperCreditFlowLayout.Controls.Add(wallpaperCreditLabel);
            wallpaperCreditFlowLayout.Controls.Add(wallpaperAuthorLinkLabel);
            wallpaperCreditFlowLayout.Controls.Add(wallpaperUpdatedLabel);
            wallpaperCreditFlowLayout.Location = new System.Drawing.Point(10, 29);
            wallpaperCreditFlowLayout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            wallpaperCreditFlowLayout.Name = "wallpaperCreditFlowLayout";
            wallpaperCreditFlowLayout.Size = new System.Drawing.Size(384, 27);
            wallpaperCreditFlowLayout.TabIndex = 0;
            wallpaperCreditFlowLayout.WrapContents = false;
            // 
            // wallpaperTitleLinkLabel
            // 
            wallpaperTitleLinkLabel.AutoSize = true;
            wallpaperTitleLinkLabel.DisabledLinkColor = System.Drawing.SystemColors.ControlText;
            wallpaperTitleLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            wallpaperTitleLinkLabel.LinkColor = System.Drawing.SystemColors.HotTrack;
            wallpaperTitleLinkLabel.Location = new System.Drawing.Point(0, 0);
            wallpaperTitleLinkLabel.Margin = new System.Windows.Forms.Padding(0);
            wallpaperTitleLinkLabel.Name = "wallpaperTitleLinkLabel";
            wallpaperTitleLinkLabel.Size = new System.Drawing.Size(36, 20);
            wallpaperTitleLinkLabel.TabIndex = 3;
            wallpaperTitleLinkLabel.TabStop = true;
            wallpaperTitleLinkLabel.Text = "Null";
            wallpaperTitleLinkLabel.LinkClicked += wallpaperTitleLinkLabel_LinkClicked;
            // 
            // wallpaperCreditLabel
            // 
            wallpaperCreditLabel.AutoSize = true;
            wallpaperCreditLabel.Location = new System.Drawing.Point(36, 0);
            wallpaperCreditLabel.Margin = new System.Windows.Forms.Padding(0);
            wallpaperCreditLabel.Name = "wallpaperCreditLabel";
            wallpaperCreditLabel.Size = new System.Drawing.Size(25, 20);
            wallpaperCreditLabel.TabIndex = 1;
            wallpaperCreditLabel.Text = "by";
            // 
            // wallpaperAuthorLinkLabel
            // 
            wallpaperAuthorLinkLabel.AutoSize = true;
            wallpaperAuthorLinkLabel.DisabledLinkColor = System.Drawing.SystemColors.ControlText;
            wallpaperAuthorLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            wallpaperAuthorLinkLabel.LinkColor = System.Drawing.SystemColors.HotTrack;
            wallpaperAuthorLinkLabel.Location = new System.Drawing.Point(61, 0);
            wallpaperAuthorLinkLabel.Margin = new System.Windows.Forms.Padding(0);
            wallpaperAuthorLinkLabel.Name = "wallpaperAuthorLinkLabel";
            wallpaperAuthorLinkLabel.Size = new System.Drawing.Size(33, 20);
            wallpaperAuthorLinkLabel.TabIndex = 4;
            wallpaperAuthorLinkLabel.TabStop = true;
            wallpaperAuthorLinkLabel.Text = "null";
            wallpaperAuthorLinkLabel.LinkClicked += wallpaperAuthorLinkLabel_LinkClicked;
            // 
            // wallpaperUpdatedLabel
            // 
            wallpaperUpdatedLabel.AutoSize = true;
            wallpaperUpdatedLabel.Location = new System.Drawing.Point(94, 0);
            wallpaperUpdatedLabel.Margin = new System.Windows.Forms.Padding(0);
            wallpaperUpdatedLabel.Name = "wallpaperUpdatedLabel";
            wallpaperUpdatedLabel.Size = new System.Drawing.Size(108, 20);
            wallpaperUpdatedLabel.TabIndex = 3;
            wallpaperUpdatedLabel.Text = "fetched on null";
            wallpaperUpdatedLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // providerGroupBox
            // 
            providerGroupBox.Controls.Add(providerDescriptionTableLayout);
            providerGroupBox.Controls.Add(providerComboBox);
            providerGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            providerGroupBox.Location = new System.Drawing.Point(3, 4);
            providerGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            providerGroupBox.Name = "providerGroupBox";
            providerGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            providerGroupBox.Size = new System.Drawing.Size(404, 277);
            providerGroupBox.TabIndex = 1;
            providerGroupBox.TabStop = false;
            providerGroupBox.Text = "Provider";
            // 
            // providerDescriptionTableLayout
            // 
            providerDescriptionTableLayout.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            providerDescriptionTableLayout.ColumnCount = 1;
            providerDescriptionTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            providerDescriptionTableLayout.Controls.Add(providerSourceLinkLabel, 0, 1);
            providerDescriptionTableLayout.Controls.Add(providerDescriptionLabel, 0, 0);
            providerDescriptionTableLayout.Location = new System.Drawing.Point(10, 68);
            providerDescriptionTableLayout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            providerDescriptionTableLayout.Name = "providerDescriptionTableLayout";
            providerDescriptionTableLayout.RowCount = 2;
            providerDescriptionTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            providerDescriptionTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            providerDescriptionTableLayout.Size = new System.Drawing.Size(384, 201);
            providerDescriptionTableLayout.TabIndex = 2;
            // 
            // providerSourceLinkLabel
            // 
            providerSourceLinkLabel.AutoEllipsis = true;
            providerSourceLinkLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            providerSourceLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            providerSourceLinkLabel.LinkColor = System.Drawing.SystemColors.HotTrack;
            providerSourceLinkLabel.Location = new System.Drawing.Point(0, 178);
            providerSourceLinkLabel.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            providerSourceLinkLabel.Name = "providerSourceLinkLabel";
            providerSourceLinkLabel.Size = new System.Drawing.Size(384, 19);
            providerSourceLinkLabel.TabIndex = 2;
            providerSourceLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            providerSourceLinkLabel.LinkClicked += providerSourceLinkLabel_LinkClicked;
            // 
            // providerDescriptionLabel
            // 
            providerDescriptionLabel.AutoEllipsis = true;
            providerDescriptionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            providerDescriptionLabel.Location = new System.Drawing.Point(0, 4);
            providerDescriptionLabel.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            providerDescriptionLabel.Name = "providerDescriptionLabel";
            providerDescriptionLabel.Size = new System.Drawing.Size(384, 166);
            providerDescriptionLabel.TabIndex = 0;
            // 
            // providerComboBox
            // 
            providerComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            providerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            providerComboBox.FormattingEnabled = true;
            providerComboBox.Location = new System.Drawing.Point(10, 29);
            providerComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            providerComboBox.Name = "providerComboBox";
            providerComboBox.Size = new System.Drawing.Size(384, 28);
            providerComboBox.TabIndex = 1;
            providerComboBox.DropDown += providerComboBox_DropDown;
            providerComboBox.SelectedIndexChanged += providerComboBox_SelectedIndexChanged;
            // 
            // optionsTabPage
            // 
            optionsTabPage.BackColor = System.Drawing.SystemColors.Window;
            optionsTabPage.Controls.Add(optionsPanel);
            optionsTabPage.Location = new System.Drawing.Point(4, 29);
            optionsTabPage.Margin = new System.Windows.Forms.Padding(0);
            optionsTabPage.Name = "optionsTabPage";
            optionsTabPage.Size = new System.Drawing.Size(410, 571);
            optionsTabPage.TabIndex = 1;
            optionsTabPage.Text = "Options";
            // 
            // optionsPanel
            // 
            optionsPanel.AutoScroll = true;
            optionsPanel.Controls.Add(optionsResizeCheckBox);
            optionsPanel.Controls.Add(optionsBlurStrengthTrackBar);
            optionsPanel.Controls.Add(optionsBlurStrengthLabel);
            optionsPanel.Controls.Add(optionsBlurredFitCheckBox);
            optionsPanel.Controls.Add(optionsUpdateTimePicker);
            optionsPanel.Controls.Add(optionsEnabledCheckBox);
            optionsPanel.Controls.Add(optionsUpdateTimeLabel);
            optionsPanel.Controls.Add(optionsProvidersDirectoryButton);
            optionsPanel.Controls.Add(optionsUpdateWallpaperButton);
            optionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            optionsPanel.Location = new System.Drawing.Point(0, 0);
            optionsPanel.Margin = new System.Windows.Forms.Padding(0);
            optionsPanel.Name = "optionsPanel";
            optionsPanel.Size = new System.Drawing.Size(410, 571);
            optionsPanel.TabIndex = 11;
            // 
            // optionsResizeCheckBox
            // 
            optionsResizeCheckBox.AutoSize = true;
            optionsResizeCheckBox.Location = new System.Drawing.Point(10, 74);
            optionsResizeCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            optionsResizeCheckBox.Name = "optionsResizeCheckBox";
            optionsResizeCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            optionsResizeCheckBox.Size = new System.Drawing.Size(137, 24);
            optionsResizeCheckBox.TabIndex = 3;
            optionsResizeCheckBox.Text = "Resize to screen";
            mainToolTip.SetToolTip(optionsResizeCheckBox, "Sizes wallpaper to screen resolution if larger. May slightly\r\nreduce image quality, but may also significantly improve\r\ncomputaitonal/storage performance.");
            optionsResizeCheckBox.UseVisualStyleBackColor = true;
            optionsResizeCheckBox.CheckedChanged += optionsResizeCheckBox_CheckedChanged;
            // 
            // optionsBlurStrengthTrackBar
            // 
            optionsBlurStrengthTrackBar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            optionsBlurStrengthTrackBar.LargeChange = 10;
            optionsBlurStrengthTrackBar.Location = new System.Drawing.Point(183, 138);
            optionsBlurStrengthTrackBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            optionsBlurStrengthTrackBar.Maximum = 100;
            optionsBlurStrengthTrackBar.Name = "optionsBlurStrengthTrackBar";
            optionsBlurStrengthTrackBar.Size = new System.Drawing.Size(220, 56);
            optionsBlurStrengthTrackBar.TabIndex = 5;
            optionsBlurStrengthTrackBar.TickFrequency = 10;
            optionsBlurStrengthTrackBar.Scroll += optionsBlurStrengthTrackBar_Scroll;
            optionsBlurStrengthTrackBar.KeyUp += optionsBlurStrengthTrackBar_KeyUp;
            optionsBlurStrengthTrackBar.MouseUp += optionsBlurStrengthTrackBar_MouseUp;
            // 
            // optionsBlurStrengthLabel
            // 
            optionsBlurStrengthLabel.AutoSize = true;
            optionsBlurStrengthLabel.Location = new System.Drawing.Point(10, 138);
            optionsBlurStrengthLabel.Name = "optionsBlurStrengthLabel";
            optionsBlurStrengthLabel.Size = new System.Drawing.Size(176, 20);
            optionsBlurStrengthLabel.TabIndex = 9;
            optionsBlurStrengthLabel.Text = "Background blur strength";
            mainToolTip.SetToolTip(optionsBlurStrengthLabel, "Background blur strength. Only applicable if blurred-fit\r\nmode is turned on. Higher blur strengths do not require\r\nextra computation time.");
            // 
            // optionsBlurredFitCheckBox
            // 
            optionsBlurredFitCheckBox.AutoSize = true;
            optionsBlurredFitCheckBox.Location = new System.Drawing.Point(10, 106);
            optionsBlurredFitCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            optionsBlurredFitCheckBox.Name = "optionsBlurredFitCheckBox";
            optionsBlurredFitCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            optionsBlurredFitCheckBox.Size = new System.Drawing.Size(142, 24);
            optionsBlurredFitCheckBox.TabIndex = 4;
            optionsBlurredFitCheckBox.Text = "Blurred-fit mode";
            mainToolTip.SetToolTip(optionsBlurredFitCheckBox, resources.GetString("optionsBlurredFitCheckBox.ToolTip"));
            optionsBlurredFitCheckBox.UseVisualStyleBackColor = true;
            optionsBlurredFitCheckBox.CheckedChanged += optionsBlurredFitCheckBox_CheckedChanged;
            // 
            // optionsUpdateTimePicker
            // 
            optionsUpdateTimePicker.Cursor = System.Windows.Forms.Cursors.IBeam;
            optionsUpdateTimePicker.CustomFormat = "h:mm tt";
            optionsUpdateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            optionsUpdateTimePicker.Location = new System.Drawing.Point(103, 39);
            optionsUpdateTimePicker.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            optionsUpdateTimePicker.Name = "optionsUpdateTimePicker";
            optionsUpdateTimePicker.ShowUpDown = true;
            optionsUpdateTimePicker.Size = new System.Drawing.Size(105, 27);
            optionsUpdateTimePicker.TabIndex = 2;
            optionsUpdateTimePicker.Value = new System.DateTime(2021, 4, 26, 0, 0, 0, 0);
            optionsUpdateTimePicker.ValueChanged += optionsUpdateTimePicker_ValueChanged;
            // 
            // optionsEnabledCheckBox
            // 
            optionsEnabledCheckBox.AutoSize = true;
            optionsEnabledCheckBox.Location = new System.Drawing.Point(10, 10);
            optionsEnabledCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            optionsEnabledCheckBox.Name = "optionsEnabledCheckBox";
            optionsEnabledCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            optionsEnabledCheckBox.Size = new System.Drawing.Size(85, 24);
            optionsEnabledCheckBox.TabIndex = 1;
            optionsEnabledCheckBox.Text = "Enabled";
            mainToolTip.SetToolTip(optionsEnabledCheckBox, "Whether automatic desktop wallpaper update triggers should be enabled or disabled.");
            optionsEnabledCheckBox.UseVisualStyleBackColor = false;
            optionsEnabledCheckBox.CheckedChanged += optionsEnabledCheckBox_CheckedChanged;
            // 
            // optionsUpdateTimeLabel
            // 
            optionsUpdateTimeLabel.AutoSize = true;
            optionsUpdateTimeLabel.Location = new System.Drawing.Point(10, 42);
            optionsUpdateTimeLabel.Name = "optionsUpdateTimeLabel";
            optionsUpdateTimeLabel.Size = new System.Drawing.Size(92, 20);
            optionsUpdateTimeLabel.TabIndex = 1;
            optionsUpdateTimeLabel.Text = "Update time";
            mainToolTip.SetToolTip(optionsUpdateTimeLabel, "When in the day to trigger an automatic desktop\r\nwallpaper update.\r\nIf the time is missed, then it will trigger on next logon.");
            // 
            // optionsProvidersDirectoryButton
            // 
            optionsProvidersDirectoryButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            optionsProvidersDirectoryButton.Location = new System.Drawing.Point(10, 241);
            optionsProvidersDirectoryButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            optionsProvidersDirectoryButton.Name = "optionsProvidersDirectoryButton";
            optionsProvidersDirectoryButton.Size = new System.Drawing.Size(390, 31);
            optionsProvidersDirectoryButton.TabIndex = 7;
            optionsProvidersDirectoryButton.Text = "Open providers folder";
            optionsProvidersDirectoryButton.UseVisualStyleBackColor = true;
            optionsProvidersDirectoryButton.Click += optionsProvidersDirectoryButton_Click;
            // 
            // optionsUpdateWallpaperButton
            // 
            optionsUpdateWallpaperButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            optionsUpdateWallpaperButton.Location = new System.Drawing.Point(10, 202);
            optionsUpdateWallpaperButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            optionsUpdateWallpaperButton.Name = "optionsUpdateWallpaperButton";
            optionsUpdateWallpaperButton.Size = new System.Drawing.Size(390, 31);
            optionsUpdateWallpaperButton.TabIndex = 6;
            optionsUpdateWallpaperButton.Text = "Update desktop wallpaper";
            optionsUpdateWallpaperButton.UseVisualStyleBackColor = true;
            optionsUpdateWallpaperButton.Click += optionsUpdateWallpaperButton_Click;
            // 
            // aboutTabPage
            // 
            aboutTabPage.BackColor = System.Drawing.SystemColors.Window;
            aboutTabPage.Controls.Add(aboutTableLayout);
            aboutTabPage.Location = new System.Drawing.Point(4, 29);
            aboutTabPage.Margin = new System.Windows.Forms.Padding(0);
            aboutTabPage.Name = "aboutTabPage";
            aboutTabPage.Size = new System.Drawing.Size(410, 571);
            aboutTabPage.TabIndex = 2;
            aboutTabPage.Text = "About";
            // 
            // aboutTableLayout
            // 
            aboutTableLayout.ColumnCount = 1;
            aboutTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            aboutTableLayout.Controls.Add(licenseGroupBox, 0, 1);
            aboutTableLayout.Controls.Add(overviewGroupBox, 0, 0);
            aboutTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            aboutTableLayout.Location = new System.Drawing.Point(0, 0);
            aboutTableLayout.Margin = new System.Windows.Forms.Padding(0);
            aboutTableLayout.Name = "aboutTableLayout";
            aboutTableLayout.RowCount = 2;
            aboutTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.45454F));
            aboutTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.54546F));
            aboutTableLayout.Size = new System.Drawing.Size(410, 571);
            aboutTableLayout.TabIndex = 3;
            // 
            // licenseGroupBox
            // 
            licenseGroupBox.Controls.Add(licenseRichTextBox);
            licenseGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            licenseGroupBox.Location = new System.Drawing.Point(3, 263);
            licenseGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            licenseGroupBox.Name = "licenseGroupBox";
            licenseGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            licenseGroupBox.Size = new System.Drawing.Size(404, 304);
            licenseGroupBox.TabIndex = 2;
            licenseGroupBox.TabStop = false;
            licenseGroupBox.Text = "License";
            // 
            // licenseRichTextBox
            // 
            licenseRichTextBox.AcceptsTab = true;
            licenseRichTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            licenseRichTextBox.BackColor = System.Drawing.SystemColors.Window;
            licenseRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            licenseRichTextBox.Location = new System.Drawing.Point(13, 28);
            licenseRichTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            licenseRichTextBox.Name = "licenseRichTextBox";
            licenseRichTextBox.ReadOnly = true;
            licenseRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            licenseRichTextBox.Size = new System.Drawing.Size(378, 268);
            licenseRichTextBox.TabIndex = 1;
            licenseRichTextBox.TabStop = false;
            licenseRichTextBox.Text = resources.GetString("licenseRichTextBox.Text");
            // 
            // overviewGroupBox
            // 
            overviewGroupBox.Controls.Add(overviewRichTextBox);
            overviewGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            overviewGroupBox.Location = new System.Drawing.Point(3, 4);
            overviewGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            overviewGroupBox.Name = "overviewGroupBox";
            overviewGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            overviewGroupBox.Size = new System.Drawing.Size(404, 251);
            overviewGroupBox.TabIndex = 1;
            overviewGroupBox.TabStop = false;
            overviewGroupBox.Text = "Overview";
            // 
            // overviewRichTextBox
            // 
            overviewRichTextBox.AcceptsTab = true;
            overviewRichTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            overviewRichTextBox.BackColor = System.Drawing.SystemColors.Window;
            overviewRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            overviewRichTextBox.Location = new System.Drawing.Point(13, 28);
            overviewRichTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            overviewRichTextBox.Name = "overviewRichTextBox";
            overviewRichTextBox.ReadOnly = true;
            overviewRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            overviewRichTextBox.Size = new System.Drawing.Size(378, 215);
            overviewRichTextBox.TabIndex = 0;
            overviewRichTextBox.TabStop = false;
            overviewRichTextBox.Text = resources.GetString("overviewRichTextBox.Text");
            // 
            // bannerPicture
            // 
            bannerPicture.BackgroundImage = (System.Drawing.Image)resources.GetObject("bannerPicture.BackgroundImage");
            bannerPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            bannerPicture.ErrorImage = null;
            bannerPicture.InitialImage = null;
            bannerPicture.Location = new System.Drawing.Point(10, 16);
            bannerPicture.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            bannerPicture.Name = "bannerPicture";
            bannerPicture.Size = new System.Drawing.Size(407, 217);
            bannerPicture.TabIndex = 2;
            bannerPicture.TabStop = false;
            // 
            // stateBackgroundWorker
            // 
            stateBackgroundWorker.WorkerReportsProgress = true;
            stateBackgroundWorker.WorkerSupportsCancellation = true;
            stateBackgroundWorker.DoWork += stateBackgroundWorker_DoWork;
            stateBackgroundWorker.ProgressChanged += stateBackgroundWorker_ProgressChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(439, 895);
            Controls.Add(mainTableLayout);
            Controls.Add(bannerPicture);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimumSize = new System.Drawing.Size(443, 784);
            Name = "MainForm";
            Text = "Daily Desktop";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            mainTableLayout.ResumeLayout(false);
            bottomPanel.ResumeLayout(false);
            bottomPanel.PerformLayout();
            mainTabControl.ResumeLayout(false);
            mainTabPage.ResumeLayout(false);
            mainTableLayout2.ResumeLayout(false);
            wallpaperGroupBox.ResumeLayout(false);
            wallpaperCreditFlowLayout.ResumeLayout(false);
            wallpaperCreditFlowLayout.PerformLayout();
            providerGroupBox.ResumeLayout(false);
            providerDescriptionTableLayout.ResumeLayout(false);
            optionsTabPage.ResumeLayout(false);
            optionsPanel.ResumeLayout(false);
            optionsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)optionsBlurStrengthTrackBar).EndInit();
            aboutTabPage.ResumeLayout(false);
            aboutTableLayout.ResumeLayout(false);
            licenseGroupBox.ResumeLayout(false);
            overviewGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bannerPicture).EndInit();
            ResumeLayout(false);
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
        private System.Windows.Forms.TabPage aboutTabPage;
        private System.Windows.Forms.RichTextBox overviewRichTextBox;
        private System.Windows.Forms.TableLayoutPanel aboutTableLayout;
        private System.Windows.Forms.GroupBox licenseGroupBox;
        private System.Windows.Forms.GroupBox overviewGroupBox;
        private System.Windows.Forms.RichTextBox licenseRichTextBox;
    }
}

