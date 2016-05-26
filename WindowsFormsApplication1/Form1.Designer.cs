namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.CreateButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.VerticInsertsNum = new System.Windows.Forms.NumericUpDown();
            this.TableOfAdjacency = new System.Windows.Forms.DataGridView();
            this.GraphCanvas = new System.Windows.Forms.PictureBox();
            this.UnderSetFounder = new System.Windows.Forms.Button();
            this.resTB = new System.Windows.Forms.TextBox();
            this.simmplFuncTB = new System.Windows.Forms.TextBox();
            this.simpleFunc = new System.Windows.Forms.Button();
            this.FoundUndersetsLabel = new System.Windows.Forms.Label();
            this.mnojestvoLB = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.VerticInsertsNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableOfAdjacency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GraphCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(628, 19);
            this.CreateButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(136, 50);
            this.CreateButton.TabIndex = 0;
            this.CreateButton.Text = "Составь!";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(398, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "Сколько вершин будет в графе:";
            // 
            // VerticInsertsNum
            // 
            this.VerticInsertsNum.Location = new System.Drawing.Point(446, 29);
            this.VerticInsertsNum.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.VerticInsertsNum.Name = "VerticInsertsNum";
            this.VerticInsertsNum.Size = new System.Drawing.Size(128, 31);
            this.VerticInsertsNum.TabIndex = 2;
            // 
            // TableOfAdjacency
            // 
            this.TableOfAdjacency.AllowUserToAddRows = false;
            this.TableOfAdjacency.AllowUserToDeleteRows = false;
            this.TableOfAdjacency.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TableOfAdjacency.Location = new System.Drawing.Point(24, 110);
            this.TableOfAdjacency.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.TableOfAdjacency.Name = "TableOfAdjacency";
            this.TableOfAdjacency.RowHeadersWidth = 50;
            this.TableOfAdjacency.Size = new System.Drawing.Size(562, 429);
            this.TableOfAdjacency.TabIndex = 3;
            this.TableOfAdjacency.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TableOfAdjacency_CellContentClick);
            this.TableOfAdjacency.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.TableOfAdjacency_CellValueChanged);
            // 
            // GraphCanvas
            // 
            this.GraphCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GraphCanvas.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.GraphCanvas.Location = new System.Drawing.Point(628, 110);
            this.GraphCanvas.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.GraphCanvas.Name = "GraphCanvas";
            this.GraphCanvas.Size = new System.Drawing.Size(774, 1027);
            this.GraphCanvas.TabIndex = 4;
            this.GraphCanvas.TabStop = false;
            this.GraphCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.GraphCanvas_Paint);
            this.GraphCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GraphCanvas_MouseDown);
            this.GraphCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GraphCanvas_MouseMove);
            this.GraphCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GraphCanvas_MouseUp);
            // 
            // UnderSetFounder
            // 
            this.UnderSetFounder.Location = new System.Drawing.Point(24, 579);
            this.UnderSetFounder.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.UnderSetFounder.Name = "UnderSetFounder";
            this.UnderSetFounder.Size = new System.Drawing.Size(562, 98);
            this.UnderSetFounder.TabIndex = 5;
            this.UnderSetFounder.Text = "Найди мне минимальные внешне устойчивые подмножества графа!";
            this.UnderSetFounder.UseVisualStyleBackColor = true;
            this.UnderSetFounder.Click += new System.EventHandler(this.UnderSetFounder_Click);
            // 
            // resTB
            // 
            this.resTB.Location = new System.Drawing.Point(26, 690);
            this.resTB.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.resTB.Name = "resTB";
            this.resTB.Size = new System.Drawing.Size(556, 31);
            this.resTB.TabIndex = 6;
            // 
            // simmplFuncTB
            // 
            this.simmplFuncTB.Location = new System.Drawing.Point(26, 802);
            this.simmplFuncTB.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.simmplFuncTB.Name = "simmplFuncTB";
            this.simmplFuncTB.Size = new System.Drawing.Size(556, 31);
            this.simmplFuncTB.TabIndex = 7;
            // 
            // simpleFunc
            // 
            this.simpleFunc.Location = new System.Drawing.Point(446, 740);
            this.simpleFunc.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.simpleFunc.Name = "simpleFunc";
            this.simpleFunc.Size = new System.Drawing.Size(140, 50);
            this.simpleFunc.TabIndex = 8;
            this.simpleFunc.Text = "Упростить";
            this.simpleFunc.UseVisualStyleBackColor = true;
            this.simpleFunc.Click += new System.EventHandler(this.simpleFunc_Click);
            // 
            // FoundUndersetsLabel
            // 
            this.FoundUndersetsLabel.AutoSize = true;
            this.FoundUndersetsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FoundUndersetsLabel.Location = new System.Drawing.Point(134, 846);
            this.FoundUndersetsLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.FoundUndersetsLabel.Name = "FoundUndersetsLabel";
            this.FoundUndersetsLabel.Size = new System.Drawing.Size(0, 30);
            this.FoundUndersetsLabel.TabIndex = 10;
            // 
            // mnojestvoLB
            // 
            this.mnojestvoLB.FormattingEnabled = true;
            this.mnojestvoLB.ItemHeight = 25;
            this.mnojestvoLB.Location = new System.Drawing.Point(26, 913);
            this.mnojestvoLB.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.mnojestvoLB.Name = "mnojestvoLB";
            this.mnojestvoLB.Size = new System.Drawing.Size(544, 204);
            this.mnojestvoLB.TabIndex = 11;
            this.mnojestvoLB.SelectedIndexChanged += new System.EventHandler(this.mnojestvoLB_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1426, 1177);
            this.Controls.Add(this.mnojestvoLB);
            this.Controls.Add(this.FoundUndersetsLabel);
            this.Controls.Add(this.simpleFunc);
            this.Controls.Add(this.simmplFuncTB);
            this.Controls.Add(this.resTB);
            this.Controls.Add(this.UnderSetFounder);
            this.Controls.Add(this.GraphCanvas);
            this.Controls.Add(this.TableOfAdjacency);
            this.Controls.Add(this.VerticInsertsNum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CreateButton);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.VerticInsertsNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableOfAdjacency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GraphCanvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown VerticInsertsNum;
        private System.Windows.Forms.DataGridView TableOfAdjacency;
        private System.Windows.Forms.PictureBox GraphCanvas;
        private System.Windows.Forms.Button UnderSetFounder;
        private System.Windows.Forms.TextBox resTB;
        private System.Windows.Forms.TextBox simmplFuncTB;
        private System.Windows.Forms.Button simpleFunc;
        private System.Windows.Forms.Label FoundUndersetsLabel;
        private System.Windows.Forms.ListBox mnojestvoLB;
    }
}

