
namespace X61GX42H1ST
{
    partial class A_PlcRecvWarring
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(A_PlcRecvWarring));
            this.c1FlexGrid = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label3 = new System.Windows.Forms.Label();
            this.button_Close = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // c1FlexGrid
            // 
            this.c1FlexGrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1FlexGrid.AllowEditing = false;
            this.c1FlexGrid.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1FlexGrid.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1FlexGrid.ColumnInfo = resources.GetString("c1FlexGrid.ColumnInfo");
            this.c1FlexGrid.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.c1FlexGrid.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.c1FlexGrid.Location = new System.Drawing.Point(8, 48);
            this.c1FlexGrid.Name = "c1FlexGrid";
            this.c1FlexGrid.Rows.Count = 100;
            this.c1FlexGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGrid.Size = new System.Drawing.Size(656, 416);
            this.c1FlexGrid.StyleInfo = resources.GetString("c1FlexGrid.StyleInfo");
            this.c1FlexGrid.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(8, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(461, 30);
            this.label3.TabIndex = 5;
            this.label3.Text = "PLCから読み取った結果に、マスタに存在しない項目が含まれています。\n該当項目は下記リストをご確認ください。";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button_Close
            // 
            this.button_Close.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Close.Location = new System.Drawing.Point(8, 472);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(104, 24);
            this.button_Close.TabIndex = 8;
            this.button_Close.Text = "閉じる";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // A_PlcRecvWarring
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 505);
            this.ControlBox = false;
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.c1FlexGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "A_PlcRecvWarring";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ＰＬＣから読込み";
            this.Load += new System.EventHandler(this.A_PlcRecvWarring_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_Close;
    }
}