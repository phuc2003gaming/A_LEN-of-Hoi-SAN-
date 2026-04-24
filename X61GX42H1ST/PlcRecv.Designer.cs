
namespace X61GX42H1ST
{
    partial class PlcRecv
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
            this.textBox_Path = new System.Windows.Forms.TextBox();
            this.button_Read = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Ref = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_Path
            // 
            this.textBox_Path.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Path.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
            this.textBox_Path.Location = new System.Drawing.Point(48, 24);
            this.textBox_Path.Name = "textBox_Path";
            this.textBox_Path.Size = new System.Drawing.Size(336, 19);
            this.textBox_Path.TabIndex = 12;
            this.textBox_Path.Text = "ファイルを選択してください。。。";
            // 
            // button_Read
            // 
            this.button_Read.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Read.Location = new System.Drawing.Point(48, 72);
            this.button_Read.Name = "button_Read";
            this.button_Read.Size = new System.Drawing.Size(104, 24);
            this.button_Read.TabIndex = 11;
            this.button_Read.Text = "実行";
            this.button_Read.UseVisualStyleBackColor = true;
            this.button_Read.Click += new System.EventHandler(this.button_Read_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(8, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 24);
            this.label1.TabIndex = 10;
            this.label1.Text = "パス：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button_Ref
            // 
            this.button_Ref.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Ref.Location = new System.Drawing.Point(392, 21);
            this.button_Ref.Name = "button_Ref";
            this.button_Ref.Size = new System.Drawing.Size(48, 24);
            this.button_Ref.TabIndex = 9;
            this.button_Ref.Text = "参照";
            this.button_Ref.UseVisualStyleBackColor = true;
            this.button_Ref.Click += new System.EventHandler(this.button_Ref_Click);
            // 
            // PlcRecv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 113);
            this.Controls.Add(this.textBox_Path);
            this.Controls.Add(this.button_Read);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_Ref);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PlcRecv";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ファイル保存選択";
            this.Load += new System.EventHandler(this.PlcRecv_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Path;
        private System.Windows.Forms.Button button_Read;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_Ref;
    }
}