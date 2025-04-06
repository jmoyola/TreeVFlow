namespace TreeVFlowWFormTest
{
    partial class Form1
    {

        #region Windows Form Designer generated code
        private TreeVFlowControl.Imp.TreeVFlow _treeVFlowNode1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._treeVFlowNode1 = new TreeVFlowControl.Imp.TreeVFlow();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.lblContentNode = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTreeNode = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._treeVFlowNode1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button6);
            this.splitContainer1.Panel2.Controls.Add(this.button5);
            this.splitContainer1.Panel2.Controls.Add(this.lblContentNode);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.lblTreeNode);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.button4);
            this.splitContainer1.Panel2.Controls.Add(this.button3);
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Size = new System.Drawing.Size(1040, 672);
            this.splitContainer1.SplitterDistance = 341;
            this.splitContainer1.TabIndex = 1;
            // 
            // _treeVFlowNode1
            // 
            this._treeVFlowNode1.AutoScroll = true;
            this._treeVFlowNode1.AutoSize = true;
            this._treeVFlowNode1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._treeVFlowNode1.BackColor = System.Drawing.Color.Azure;
            this._treeVFlowNode1.ColumnCount = 1;
            this._treeVFlowNode1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 341F));
            this._treeVFlowNode1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._treeVFlowNode1.Footer = null;
            this._treeVFlowNode1.Header = null;
            this._treeVFlowNode1.LevelIndent = 5;
            this._treeVFlowNode1.Location = new System.Drawing.Point(0, 0);
            this._treeVFlowNode1.Name = "_treeVFlowNode1";
            this._treeVFlowNode1.Size = new System.Drawing.Size(341, 672);
            this._treeVFlowNode1.TabIndex = 0;
            this._treeVFlowNode1.Text = "Root";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(254, 123);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(194, 44);
            this.button6.TabIndex = 9;
            this.button6.Text = "Show Active Content Node";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(254, 23);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(194, 44);
            this.button5.TabIndex = 8;
            this.button5.Text = "Show ActiveTreeNode";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // lblContentNode
            // 
            this.lblContentNode.Location = new System.Drawing.Point(173, 278);
            this.lblContentNode.Name = "lblContentNode";
            this.lblContentNode.Size = new System.Drawing.Size(100, 23);
            this.lblContentNode.TabIndex = 7;
            this.lblContentNode.Text = "label3";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(22, 278);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "Active Content Node:";
            // 
            // lblTreeNode
            // 
            this.lblTreeNode.Location = new System.Drawing.Point(173, 255);
            this.lblTreeNode.Name = "lblTreeNode";
            this.lblTreeNode.Size = new System.Drawing.Size(100, 23);
            this.lblTreeNode.TabIndex = 5;
            this.lblTreeNode.Text = "label2";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(45, 255);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Active TreeNode:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(61, 173);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(177, 44);
            this.button4.TabIndex = 3;
            this.button4.Text = "RemoveContent";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(61, 123);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(177, 44);
            this.button3.TabIndex = 2;
            this.button3.Text = "AddContent";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(61, 73);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(177, 44);
            this.button2.TabIndex = 1;
            this.button2.Text = "RemoveTreeNode";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(61, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "AddTreeNode";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 672);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTreeNode;
        private System.Windows.Forms.Label lblContentNode;
        private System.Windows.Forms.Label label4;

        #endregion


    }
}