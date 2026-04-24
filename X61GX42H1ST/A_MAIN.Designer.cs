
namespace X61GX42H1ST
{
    partial class A_MAIN
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.File_Command = new System.Windows.Forms.ToolStripMenuItem();
            this.ファイル読込ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.コピーToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.削除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.データベース最適化修復ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.検索ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.アプリケーション終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ファイル名を付けて終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Edit_Command = new System.Windows.Forms.ToolStripMenuItem();
            this.識別コードマスターToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.レシピマスタToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.タグ情報マスタToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Disp_Command = new System.Windows.Forms.ToolStripMenuItem();
            this.レシピ参照ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Comm_Command = new System.Windows.Forms.ToolStripMenuItem();
            this.タグ情報アップロードToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.レシピ情報アップロードパターン１ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.パターン１ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.パターン２ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.パターン３ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pLCから読み込みToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.パータン１ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.パターン２ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.パターン３ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pLCとの照合パターン１ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.パターン１ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.パターン２ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.パターン３ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.Print_Command = new System.Windows.Forms.ToolStripMenuItem();
            this.車種情報印刷ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label2 = new System.Windows.Forms.Label();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.backgroundWork = new System.ComponentModel.BackgroundWorker();
            this.Panel_Progress = new System.Windows.Forms.Panel();
            this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelTile = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            this.panelSearch.SuspendLayout();
            this.Panel_Progress.SuspendLayout();
            this.SuspendLayout();
            // 
            // Timer1
            // 
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File_Command,
            this.Edit_Command,
            this.Disp_Command,
            this.Comm_Command,
            this.Print_Command});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(893, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // File_Command
            // 
            this.File_Command.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイル読込ToolStripMenuItem,
            this.コピーToolStripMenuItem,
            this.削除ToolStripMenuItem,
            this.データベース最適化修復ToolStripMenuItem,
            this.検索ToolStripMenuItem,
            this.アプリケーション終了ToolStripMenuItem,
            this.ファイル名を付けて終了ToolStripMenuItem});
            this.File_Command.Name = "File_Command";
            this.File_Command.Size = new System.Drawing.Size(79, 29);
            this.File_Command.Text = "ファイル";
            // 
            // ファイル読込ToolStripMenuItem
            // 
            this.ファイル読込ToolStripMenuItem.Name = "ファイル読込ToolStripMenuItem";
            this.ファイル読込ToolStripMenuItem.Size = new System.Drawing.Size(303, 34);
            this.ファイル読込ToolStripMenuItem.Text = "ファイル読込";
            this.ファイル読込ToolStripMenuItem.Click += new System.EventHandler(this.File_Command_Click);
            // 
            // コピーToolStripMenuItem
            // 
            this.コピーToolStripMenuItem.Name = "コピーToolStripMenuItem";
            this.コピーToolStripMenuItem.Size = new System.Drawing.Size(303, 34);
            this.コピーToolStripMenuItem.Text = "コピー";
            this.コピーToolStripMenuItem.Click += new System.EventHandler(this.File_Command_Click);
            // 
            // 削除ToolStripMenuItem
            // 
            this.削除ToolStripMenuItem.Name = "削除ToolStripMenuItem";
            this.削除ToolStripMenuItem.Size = new System.Drawing.Size(303, 34);
            this.削除ToolStripMenuItem.Text = "削除";
            this.削除ToolStripMenuItem.Click += new System.EventHandler(this.File_Command_Click);
            // 
            // データベース最適化修復ToolStripMenuItem
            // 
            this.データベース最適化修復ToolStripMenuItem.Name = "データベース最適化修復ToolStripMenuItem";
            this.データベース最適化修復ToolStripMenuItem.Size = new System.Drawing.Size(303, 34);
            this.データベース最適化修復ToolStripMenuItem.Text = "データベース最適化／修復";
            this.データベース最適化修復ToolStripMenuItem.Click += new System.EventHandler(this.File_Command_Click);
            // 
            // 検索ToolStripMenuItem
            // 
            this.検索ToolStripMenuItem.Name = "検索ToolStripMenuItem";
            this.検索ToolStripMenuItem.Size = new System.Drawing.Size(303, 34);
            this.検索ToolStripMenuItem.Text = "検索";
            this.検索ToolStripMenuItem.Click += new System.EventHandler(this.File_Command_Click);
            // 
            // アプリケーション終了ToolStripMenuItem
            // 
            this.アプリケーション終了ToolStripMenuItem.Name = "アプリケーション終了ToolStripMenuItem";
            this.アプリケーション終了ToolStripMenuItem.Size = new System.Drawing.Size(303, 34);
            this.アプリケーション終了ToolStripMenuItem.Text = "アプリケーション終了";
            this.アプリケーション終了ToolStripMenuItem.Click += new System.EventHandler(this.File_Command_Click);
            // 
            // ファイル名を付けて終了ToolStripMenuItem
            // 
            this.ファイル名を付けて終了ToolStripMenuItem.Name = "ファイル名を付けて終了ToolStripMenuItem";
            this.ファイル名を付けて終了ToolStripMenuItem.Size = new System.Drawing.Size(303, 34);
            this.ファイル名を付けて終了ToolStripMenuItem.Text = "ファイル名を付けて終了";
            this.ファイル名を付けて終了ToolStripMenuItem.Click += new System.EventHandler(this.File_Command_Click);
            // 
            // Edit_Command
            // 
            this.Edit_Command.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.識別コードマスターToolStripMenuItem,
            this.レシピマスタToolStripMenuItem,
            this.タグ情報マスタToolStripMenuItem});
            this.Edit_Command.Name = "Edit_Command";
            this.Edit_Command.Size = new System.Drawing.Size(64, 29);
            this.Edit_Command.Text = "編集";
            // 
            // 識別コードマスターToolStripMenuItem
            // 
            this.識別コードマスターToolStripMenuItem.Name = "識別コードマスターToolStripMenuItem";
            this.識別コードマスターToolStripMenuItem.Size = new System.Drawing.Size(242, 34);
            this.識別コードマスターToolStripMenuItem.Text = "識別コードマスター";
            this.識別コードマスターToolStripMenuItem.Click += new System.EventHandler(this.Edit_Command_Click);
            // 
            // レシピマスタToolStripMenuItem
            // 
            this.レシピマスタToolStripMenuItem.Name = "レシピマスタToolStripMenuItem";
            this.レシピマスタToolStripMenuItem.Size = new System.Drawing.Size(242, 34);
            this.レシピマスタToolStripMenuItem.Text = "レシピマスタ－";
            this.レシピマスタToolStripMenuItem.Click += new System.EventHandler(this.Edit_Command_Click);
            // 
            // タグ情報マスタToolStripMenuItem
            // 
            this.タグ情報マスタToolStripMenuItem.Name = "タグ情報マスタToolStripMenuItem";
            this.タグ情報マスタToolStripMenuItem.Size = new System.Drawing.Size(242, 34);
            this.タグ情報マスタToolStripMenuItem.Text = "タグ情報マスタ－";
            this.タグ情報マスタToolStripMenuItem.Click += new System.EventHandler(this.Edit_Command_Click);
            // 
            // Disp_Command
            // 
            this.Disp_Command.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.レシピ参照ToolStripMenuItem});
            this.Disp_Command.Name = "Disp_Command";
            this.Disp_Command.Size = new System.Drawing.Size(64, 29);
            this.Disp_Command.Text = "表示";
            // 
            // レシピ参照ToolStripMenuItem
            // 
            this.レシピ参照ToolStripMenuItem.Name = "レシピ参照ToolStripMenuItem";
            this.レシピ参照ToolStripMenuItem.Size = new System.Drawing.Size(190, 34);
            this.レシピ参照ToolStripMenuItem.Text = "レシピ参照";
            this.レシピ参照ToolStripMenuItem.Click += new System.EventHandler(this.Disp_Command_Click);
            // 
            // Comm_Command
            // 
            this.Comm_Command.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.タグ情報アップロードToolStripMenuItem,
            this.レシピ情報アップロードパターン１ToolStripMenuItem,
            this.pLCから読み込みToolStripMenuItem,
            this.pLCとの照合パターン１ToolStripMenuItem});
            this.Comm_Command.Name = "Comm_Command";
            this.Comm_Command.Size = new System.Drawing.Size(64, 29);
            this.Comm_Command.Text = "通信";
            // 
            // タグ情報アップロードToolStripMenuItem
            // 
            this.タグ情報アップロードToolStripMenuItem.Name = "タグ情報アップロードToolStripMenuItem";
            this.タグ情報アップロードToolStripMenuItem.Size = new System.Drawing.Size(267, 34);
            this.タグ情報アップロードToolStripMenuItem.Text = "タグ情報アップロード";
            this.タグ情報アップロードToolStripMenuItem.Click += new System.EventHandler(this.Comm_TagUpLoad_Click);
            // 
            // レシピ情報アップロードパターン１ToolStripMenuItem
            // 
            this.レシピ情報アップロードパターン１ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.パターン１ToolStripMenuItem,
            this.パターン２ToolStripMenuItem,
            this.パターン３ToolStripMenuItem});
            this.レシピ情報アップロードパターン１ToolStripMenuItem.Name = "レシピ情報アップロードパターン１ToolStripMenuItem";
            this.レシピ情報アップロードパターン１ToolStripMenuItem.Size = new System.Drawing.Size(267, 34);
            this.レシピ情報アップロードパターン１ToolStripMenuItem.Text = "レシピ情報アップロード";
            // 
            // パターン１ToolStripMenuItem
            // 
            this.パターン１ToolStripMenuItem.Name = "パターン１ToolStripMenuItem";
            this.パターン１ToolStripMenuItem.Size = new System.Drawing.Size(186, 34);
            this.パターン１ToolStripMenuItem.Text = "パターン１";
            this.パターン１ToolStripMenuItem.Click += new System.EventHandler(this.Comm_RepUpLoad_Click);
            // 
            // パターン２ToolStripMenuItem
            // 
            this.パターン２ToolStripMenuItem.Name = "パターン２ToolStripMenuItem";
            this.パターン２ToolStripMenuItem.Size = new System.Drawing.Size(186, 34);
            this.パターン２ToolStripMenuItem.Text = "パターン２";
            this.パターン２ToolStripMenuItem.Click += new System.EventHandler(this.Comm_RepUpLoad_Click);
            // 
            // パターン３ToolStripMenuItem
            // 
            this.パターン３ToolStripMenuItem.Name = "パターン３ToolStripMenuItem";
            this.パターン３ToolStripMenuItem.Size = new System.Drawing.Size(186, 34);
            this.パターン３ToolStripMenuItem.Text = "パターン３";
            this.パターン３ToolStripMenuItem.Click += new System.EventHandler(this.Comm_RepUpLoad_Click);
            // 
            // pLCから読み込みToolStripMenuItem
            // 
            this.pLCから読み込みToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.パータン１ToolStripMenuItem,
            this.パターン２ToolStripMenuItem1,
            this.パターン３ToolStripMenuItem1});
            this.pLCから読み込みToolStripMenuItem.Name = "pLCから読み込みToolStripMenuItem";
            this.pLCから読み込みToolStripMenuItem.Size = new System.Drawing.Size(267, 34);
            this.pLCから読み込みToolStripMenuItem.Text = "PLCから読込み";
            // 
            // パータン１ToolStripMenuItem
            // 
            this.パータン１ToolStripMenuItem.Name = "パータン１ToolStripMenuItem";
            this.パータン１ToolStripMenuItem.Size = new System.Drawing.Size(186, 34);
            this.パータン１ToolStripMenuItem.Text = "パターン１";
            this.パータン１ToolStripMenuItem.Click += new System.EventHandler(this.Comm_Read_Click);
            // 
            // パターン２ToolStripMenuItem1
            // 
            this.パターン２ToolStripMenuItem1.Name = "パターン２ToolStripMenuItem1";
            this.パターン２ToolStripMenuItem1.Size = new System.Drawing.Size(186, 34);
            this.パターン２ToolStripMenuItem1.Text = "パターン２";
            this.パターン２ToolStripMenuItem1.Click += new System.EventHandler(this.Comm_Read_Click);
            // 
            // パターン３ToolStripMenuItem1
            // 
            this.パターン３ToolStripMenuItem1.Name = "パターン３ToolStripMenuItem1";
            this.パターン３ToolStripMenuItem1.Size = new System.Drawing.Size(186, 34);
            this.パターン３ToolStripMenuItem1.Text = "パターン３";
            this.パターン３ToolStripMenuItem1.Click += new System.EventHandler(this.Comm_Read_Click);
            // 
            // pLCとの照合パターン１ToolStripMenuItem
            // 
            this.pLCとの照合パターン１ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.パターン１ToolStripMenuItem1,
            this.パターン２ToolStripMenuItem2,
            this.パターン３ToolStripMenuItem2});
            this.pLCとの照合パターン１ToolStripMenuItem.Name = "pLCとの照合パターン１ToolStripMenuItem";
            this.pLCとの照合パターン１ToolStripMenuItem.Size = new System.Drawing.Size(267, 34);
            this.pLCとの照合パターン１ToolStripMenuItem.Text = "PLCとの照合";
            // 
            // パターン１ToolStripMenuItem1
            // 
            this.パターン１ToolStripMenuItem1.Name = "パターン１ToolStripMenuItem1";
            this.パターン１ToolStripMenuItem1.Size = new System.Drawing.Size(186, 34);
            this.パターン１ToolStripMenuItem1.Text = "パターン１";
            this.パターン１ToolStripMenuItem1.Click += new System.EventHandler(this.Comm_Compare_Click);
            // 
            // パターン２ToolStripMenuItem2
            // 
            this.パターン２ToolStripMenuItem2.Name = "パターン２ToolStripMenuItem2";
            this.パターン２ToolStripMenuItem2.Size = new System.Drawing.Size(186, 34);
            this.パターン２ToolStripMenuItem2.Text = "パターン２";
            this.パターン２ToolStripMenuItem2.Click += new System.EventHandler(this.Comm_Compare_Click);
            // 
            // パターン３ToolStripMenuItem2
            // 
            this.パターン３ToolStripMenuItem2.Name = "パターン３ToolStripMenuItem2";
            this.パターン３ToolStripMenuItem2.Size = new System.Drawing.Size(186, 34);
            this.パターン３ToolStripMenuItem2.Text = "パターン３";
            this.パターン３ToolStripMenuItem2.Click += new System.EventHandler(this.Comm_Compare_Click);
            // 
            // Print_Command
            // 
            this.Print_Command.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.車種情報印刷ToolStripMenuItem});
            this.Print_Command.Name = "Print_Command";
            this.Print_Command.Size = new System.Drawing.Size(64, 29);
            this.Print_Command.Text = "印刷";
            // 
            // 車種情報印刷ToolStripMenuItem
            // 
            this.車種情報印刷ToolStripMenuItem.Name = "車種情報印刷ToolStripMenuItem";
            this.車種情報印刷ToolStripMenuItem.Size = new System.Drawing.Size(222, 34);
            this.車種情報印刷ToolStripMenuItem.Text = "車種情報印刷";
            this.車種情報印刷ToolStripMenuItem.Click += new System.EventHandler(this.Print_Command_Click);
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1FlexGrid1.AllowEditing = false;
            this.c1FlexGrid1.AllowNodeCellCheck = false;
            this.c1FlexGrid1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1FlexGrid1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1FlexGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1FlexGrid1.AreRowDetailsFrozen = false;
            this.c1FlexGrid1.AutoGenerateColumns = false;
            this.c1FlexGrid1.ColumnInfo = "10,1,0,0,0,-1,Columns:";
            this.c1FlexGrid1.Font = new System.Drawing.Font("MS PGothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.c1FlexGrid1.Location = new System.Drawing.Point(8, 56);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGrid1.Size = new System.Drawing.Size(880, 544);
            this.c1FlexGrid1.TabIndex = 1;
            this.c1FlexGrid1.DoubleClick += new System.EventHandler(this.c1FlexGrid1_DoubleClick);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("MS PGothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(8, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(880, 25);
            this.label2.TabIndex = 2;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelSearch
            // 
            this.panelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSearch.Controls.Add(this.button_Cancel);
            this.panelSearch.Controls.Add(this.button_OK);
            this.panelSearch.Controls.Add(this.label3);
            this.panelSearch.Controls.Add(this.textBoxSearch);
            this.panelSearch.Controls.Add(this.label4);
            this.panelSearch.Location = new System.Drawing.Point(716, 0);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(176, 44);
            this.panelSearch.TabIndex = 4;
            this.panelSearch.Visible = false;
            // 
            // button_Cancel
            // 
            this.button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Cancel.Font = new System.Drawing.Font("MS PGothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Cancel.Location = new System.Drawing.Point(120, 20);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(52, 19);
            this.button_Cancel.TabIndex = 3;
            this.button_Cancel.Text = "ｷｬﾝｾﾙ";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_OK
            // 
            this.button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_OK.Font = new System.Drawing.Font("MS PGothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_OK.Location = new System.Drawing.Point(88, 20);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(32, 19);
            this.button_OK.TabIndex = 2;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Highlight;
            this.label3.Font = new System.Drawing.Font("MS PGothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label3.Location = new System.Drawing.Point(4, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 18);
            this.label3.TabIndex = 1;
            this.label3.Text = "識別名の検索";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.Font = new System.Drawing.Font("MS PGothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxSearch.Location = new System.Drawing.Point(4, 20);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(84, 25);
            this.textBoxSearch.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.SystemColors.Highlight;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(176, 20);
            this.label4.TabIndex = 4;
            // 
            // backgroundWork
            // 
            this.backgroundWork.WorkerReportsProgress = true;
            this.backgroundWork.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWork1_DoWork);
            this.backgroundWork.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWork1_ProgressChanged);
            this.backgroundWork.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWork1_RunWorkerCompleted);
            // 
            // Panel_Progress
            // 
            this.Panel_Progress.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Panel_Progress.BackColor = System.Drawing.SystemColors.Control;
            this.Panel_Progress.Controls.Add(this.ProgressBar1);
            this.Panel_Progress.Controls.Add(this.labelTile);
            this.Panel_Progress.Controls.Add(this.label11);
            this.Panel_Progress.Controls.Add(this.label8);
            this.Panel_Progress.Controls.Add(this.label5);
            this.Panel_Progress.Controls.Add(this.label9);
            this.Panel_Progress.Controls.Add(this.label6);
            this.Panel_Progress.Controls.Add(this.label10);
            this.Panel_Progress.Controls.Add(this.label7);
            this.Panel_Progress.Controls.Add(this.label13);
            this.Panel_Progress.Location = new System.Drawing.Point(180, 236);
            this.Panel_Progress.Name = "Panel_Progress";
            this.Panel_Progress.Size = new System.Drawing.Size(536, 184);
            this.Panel_Progress.TabIndex = 61;
            this.Panel_Progress.Visible = false;
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Location = new System.Drawing.Point(56, 128);
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(428, 23);
            this.ProgressBar1.TabIndex = 61;
            // 
            // labelTile
            // 
            this.labelTile.BackColor = System.Drawing.SystemColors.Control;
            this.labelTile.Font = new System.Drawing.Font("MS Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelTile.Location = new System.Drawing.Point(16, 32);
            this.labelTile.Name = "labelTile";
            this.labelTile.Size = new System.Drawing.Size(504, 72);
            this.labelTile.TabIndex = 61;
            this.labelTile.Text = "車種情報の印刷中";
            this.labelTile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.DodgerBlue;
            this.label11.Location = new System.Drawing.Point(12, 12);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(508, 2);
            this.label11.TabIndex = 56;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.DodgerBlue;
            this.label8.Location = new System.Drawing.Point(520, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(2, 161);
            this.label8.TabIndex = 59;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.DodgerBlue;
            this.label5.Location = new System.Drawing.Point(8, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(516, 2);
            this.label5.TabIndex = 52;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.DodgerBlue;
            this.label9.Location = new System.Drawing.Point(12, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(2, 160);
            this.label9.TabIndex = 58;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.DodgerBlue;
            this.label6.Location = new System.Drawing.Point(8, 176);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(516, 2);
            this.label6.TabIndex = 53;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.DodgerBlue;
            this.label10.Location = new System.Drawing.Point(12, 172);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(508, 2);
            this.label10.TabIndex = 57;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.DodgerBlue;
            this.label7.Location = new System.Drawing.Point(8, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(2, 170);
            this.label7.TabIndex = 54;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.DodgerBlue;
            this.label13.Location = new System.Drawing.Point(524, 8);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(2, 170);
            this.label13.TabIndex = 55;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Font = new System.Drawing.Font("MS PGothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(8, 608);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(880, 25);
            this.label1.TabIndex = 3;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // A_MEIN
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(893, 632);
            this.Controls.Add(this.Panel_Progress);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.c1FlexGrid1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimizeBox = false;
            this.Name = "A_MEIN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "1STシート車種データ一覧";
            this.Load += new System.EventHandler(this.A_MEIN_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.Panel_Progress.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer Timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem File_Command;
        private System.Windows.Forms.ToolStripMenuItem ファイル読込ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem コピーToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 削除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem データベース最適化修復ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Edit_Command;
        private System.Windows.Forms.ToolStripMenuItem Disp_Command;
        private System.Windows.Forms.ToolStripMenuItem Comm_Command;
        private System.Windows.Forms.ToolStripMenuItem Print_Command;
        private System.Windows.Forms.ToolStripMenuItem 検索ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem アプリケーション終了ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ファイル名を付けて終了ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 識別コードマスターToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem レシピマスタToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem タグ情報マスタToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem レシピ参照ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem タグ情報アップロードToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem レシピ情報アップロードパターン１ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 車種情報印刷ToolStripMenuItem;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label4;
        private System.ComponentModel.BackgroundWorker backgroundWork;
        private System.Windows.Forms.Panel Panel_Progress;
        private System.Windows.Forms.ProgressBar ProgressBar1;
        private System.Windows.Forms.Label labelTile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ToolStripMenuItem pLCから読み込みToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pLCとの照合パターン１ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem パターン１ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem パターン２ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem パターン３ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem パータン１ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem パターン２ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem パターン３ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem パターン１ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem パターン２ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem パターン３ToolStripMenuItem2;
    }
}

