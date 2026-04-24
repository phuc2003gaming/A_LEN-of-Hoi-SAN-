
namespace X61GX42H1ST
{
    partial class A_PlcComp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(A_PlcComp));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.File_Command = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonVeh = new System.Windows.Forms.Button();
            this.labelVeh = new System.Windows.Forms.Label();
            this.VEHICLE_c1FlexGrid = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonTag = new System.Windows.Forms.Button();
            this.labelTag = new System.Windows.Forms.Label();
            this.TAG_c1FlexGrid = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.menuStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VEHICLE_c1FlexGrid)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TAG_c1FlexGrid)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File_Command});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1002, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // File_Command
            // 
            this.File_Command.Name = "File_Command";
            this.File_Command.Size = new System.Drawing.Size(43, 20);
            this.File_Command.Text = "終了";
            this.File_Command.Click += new System.EventHandler(this.File_Command_Click_1);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buttonVeh);
            this.tabPage2.Controls.Add(this.labelVeh);
            this.tabPage2.Controls.Add(this.VEHICLE_c1FlexGrid);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(992, 587);
            this.tabPage2.TabIndex = 4;
            this.tabPage2.Text = "車種情報";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonVeh
            // 
            this.buttonVeh.Location = new System.Drawing.Point(888, 548);
            this.buttonVeh.Name = "buttonVeh";
            this.buttonVeh.Size = new System.Drawing.Size(96, 31);
            this.buttonVeh.TabIndex = 4;
            this.buttonVeh.Text = "全データ";
            this.buttonVeh.UseVisualStyleBackColor = true;
            this.buttonVeh.Click += new System.EventHandler(this.DsipMode_Click);
            // 
            // labelVeh
            // 
            this.labelVeh.AutoSize = true;
            this.labelVeh.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelVeh.Location = new System.Drawing.Point(0, 552);
            this.labelVeh.Name = "labelVeh";
            this.labelVeh.Size = new System.Drawing.Size(199, 24);
            this.labelVeh.TabIndex = 3;
            this.labelVeh.Text = "不一致があります";
            // 
            // VEHICLE_c1FlexGrid
            // 
            this.VEHICLE_c1FlexGrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.VEHICLE_c1FlexGrid.AllowEditing = false;
            this.VEHICLE_c1FlexGrid.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.VEHICLE_c1FlexGrid.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.VEHICLE_c1FlexGrid.ColumnInfo = resources.GetString("VEHICLE_c1FlexGrid.ColumnInfo");
            this.VEHICLE_c1FlexGrid.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.VEHICLE_c1FlexGrid.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
            this.VEHICLE_c1FlexGrid.Location = new System.Drawing.Point(0, 0);
            this.VEHICLE_c1FlexGrid.Name = "VEHICLE_c1FlexGrid";
            this.VEHICLE_c1FlexGrid.Rows.Count = 1962;
            this.VEHICLE_c1FlexGrid.Rows.Fixed = 2;
            this.VEHICLE_c1FlexGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.VEHICLE_c1FlexGrid.Size = new System.Drawing.Size(992, 544);
            this.VEHICLE_c1FlexGrid.TabIndex = 1;
            this.VEHICLE_c1FlexGrid.DoubleClick += new System.EventHandler(this.VEHICLE_c1FlexGrid_DoubleClick);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonTag);
            this.tabPage1.Controls.Add(this.labelTag);
            this.tabPage1.Controls.Add(this.TAG_c1FlexGrid);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(992, 587);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "タグ情報";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonTag
            // 
            this.buttonTag.Location = new System.Drawing.Point(888, 548);
            this.buttonTag.Name = "buttonTag";
            this.buttonTag.Size = new System.Drawing.Size(96, 31);
            this.buttonTag.TabIndex = 5;
            this.buttonTag.Text = "全データ";
            this.buttonTag.UseVisualStyleBackColor = true;
            this.buttonTag.Click += new System.EventHandler(this.DsipMode_Click);
            // 
            // labelTag
            // 
            this.labelTag.AutoSize = true;
            this.labelTag.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelTag.Location = new System.Drawing.Point(0, 552);
            this.labelTag.Name = "labelTag";
            this.labelTag.Size = new System.Drawing.Size(199, 24);
            this.labelTag.TabIndex = 2;
            this.labelTag.Text = "不一致があります";
            // 
            // TAG_c1FlexGrid
            // 
            this.TAG_c1FlexGrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.TAG_c1FlexGrid.AllowEditing = false;
            this.TAG_c1FlexGrid.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.TAG_c1FlexGrid.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.TAG_c1FlexGrid.ColumnInfo = resources.GetString("TAG_c1FlexGrid.ColumnInfo");
            this.TAG_c1FlexGrid.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TAG_c1FlexGrid.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
            this.TAG_c1FlexGrid.Location = new System.Drawing.Point(0, 0);
            this.TAG_c1FlexGrid.Name = "TAG_c1FlexGrid";
            this.TAG_c1FlexGrid.Rows.Count = 502;
            this.TAG_c1FlexGrid.Rows.Fixed = 2;
            this.TAG_c1FlexGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.TAG_c1FlexGrid.Size = new System.Drawing.Size(992, 544);
            this.TAG_c1FlexGrid.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tabControl1.Location = new System.Drawing.Point(0, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1000, 616);
            this.tabControl1.TabIndex = 3;
            // 
            // A_PlcComp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 645);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "A_PlcComp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "照合結果";
            this.Load += new System.EventHandler(this.A_PlcComp_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VEHICLE_c1FlexGrid)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TAG_c1FlexGrid)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem File_Command;
        private System.Windows.Forms.TabPage tabPage2;
        private C1.Win.C1FlexGrid.C1FlexGrid VEHICLE_c1FlexGrid;
        private System.Windows.Forms.TabPage tabPage1;
        private C1.Win.C1FlexGrid.C1FlexGrid TAG_c1FlexGrid;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button buttonVeh;
        private System.Windows.Forms.Label labelVeh;
        private System.Windows.Forms.Button buttonTag;
        private System.Windows.Forms.Label labelTag;
    }
}