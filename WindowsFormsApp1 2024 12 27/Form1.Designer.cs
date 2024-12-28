    namespace WindowsFormsApp1_2024_12_27
    {
        partial class Form1
        {
            /// <summary>
            /// 設計工具所需的變數。
            /// </summary>
            private System.ComponentModel.IContainer components = null;

            /// <summary>
            /// 清除任何使用中的資源。
            /// </summary>
            /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }

            #region Windows Form 設計工具產生的程式碼

            /// <summary>
            /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
            /// 這個方法的內容。
            /// </summary>
            private void InitializeComponent()
            {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.DialogueLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.choice1 = new System.Windows.Forms.Button();
            this.choice2 = new System.Windows.Forms.Button();
            this.Speaker = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Character = new System.Windows.Forms.PictureBox();
            this.Scene = new System.Windows.Forms.PictureBox();
            this.dialoguePanel = new System.Windows.Forms.Panel();
            this.nextbutton = new System.Windows.Forms.Button();
            this.choice3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Character)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Scene)).BeginInit();
            this.dialoguePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // DialogueLabel
            // 
            this.DialogueLabel.AutoSize = true;
            this.DialogueLabel.Location = new System.Drawing.Point(16, 218);
            this.DialogueLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DialogueLabel.Name = "DialogueLabel";
            this.DialogueLabel.Size = new System.Drawing.Size(33, 12);
            this.DialogueLabel.TabIndex = 0;
            this.DialogueLabel.Text = "label1";
            this.DialogueLabel.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(173, 148);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(68, 22);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(268, 141);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 31);
            this.button1.TabIndex = 3;
            this.button1.Text = "確認";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // choice1
            // 
            this.choice1.Location = new System.Drawing.Point(211, 52);
            this.choice1.Margin = new System.Windows.Forms.Padding(2);
            this.choice1.Name = "choice1";
            this.choice1.Size = new System.Drawing.Size(95, 31);
            this.choice1.TabIndex = 4;
            this.choice1.Text = "button2";
            this.choice1.UseVisualStyleBackColor = true;
            this.choice1.Visible = false;
            this.choice1.Click += new System.EventHandler(this.choice_Click);
            // 
            // choice2
            // 
            this.choice2.Location = new System.Drawing.Point(211, 98);
            this.choice2.Margin = new System.Windows.Forms.Padding(2);
            this.choice2.Name = "choice2";
            this.choice2.Size = new System.Drawing.Size(95, 31);
            this.choice2.TabIndex = 5;
            this.choice2.Text = "button3";
            this.choice2.UseVisualStyleBackColor = true;
            this.choice2.Visible = false;
            this.choice2.Click += new System.EventHandler(this.choice_Click);
            // 
            // Speaker
            // 
            this.Speaker.AutoSize = true;
            this.Speaker.Location = new System.Drawing.Point(16, 184);
            this.Speaker.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Speaker.Name = "Speaker";
            this.Speaker.Size = new System.Drawing.Size(33, 12);
            this.Speaker.TabIndex = 8;
            this.Speaker.Text = "label2";
            this.Speaker.Visible = false;
            this.Speaker.Click += new System.EventHandler(this.Speaker_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(181, 121);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "玩家名稱";
            // 
            // Character
            // 
            this.Character.Image = ((System.Drawing.Image)(resources.GetObject("Character.Image")));
            this.Character.Location = new System.Drawing.Point(422, 95);
            this.Character.Margin = new System.Windows.Forms.Padding(2);
            this.Character.Name = "Character";
            this.Character.Size = new System.Drawing.Size(113, 119);
            this.Character.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Character.TabIndex = 7;
            this.Character.TabStop = false;
            this.Character.Visible = false;
            this.Character.Click += new System.EventHandler(this.Character_Click);
            // 
            // Scene
            // 
            this.Scene.Image = global::WindowsFormsApp1_2024_12_27.Properties.Resources.螢幕擷取畫面_2023_07_28_223624;
            this.Scene.Location = new System.Drawing.Point(-1, 0);
            this.Scene.Margin = new System.Windows.Forms.Padding(2);
            this.Scene.Name = "Scene";
            this.Scene.Size = new System.Drawing.Size(536, 309);
            this.Scene.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Scene.TabIndex = 6;
            this.Scene.TabStop = false;
            // 
            // dialoguePanel
            // 
            this.dialoguePanel.Controls.Add(this.nextbutton);
            this.dialoguePanel.Location = new System.Drawing.Point(-1, 211);
            this.dialoguePanel.Margin = new System.Windows.Forms.Padding(2);
            this.dialoguePanel.Name = "dialoguePanel";
            this.dialoguePanel.Size = new System.Drawing.Size(536, 87);
            this.dialoguePanel.TabIndex = 10;
            // 
            // nextbutton
            // 
            this.nextbutton.Location = new System.Drawing.Point(500, 57);
            this.nextbutton.Margin = new System.Windows.Forms.Padding(2);
            this.nextbutton.Name = "nextbutton";
            this.nextbutton.Size = new System.Drawing.Size(27, 24);
            this.nextbutton.TabIndex = 11;
            this.nextbutton.Text = "▶";
            this.nextbutton.UseVisualStyleBackColor = true;
            this.nextbutton.Visible = false;
            this.nextbutton.Click += new System.EventHandler(this.nextbutton_Click);
            // 
            // choice3
            // 
            this.choice3.Location = new System.Drawing.Point(211, 139);
            this.choice3.Margin = new System.Windows.Forms.Padding(2);
            this.choice3.Name = "choice3";
            this.choice3.Size = new System.Drawing.Size(95, 31);
            this.choice3.TabIndex = 11;
            this.choice3.Text = "button3";
            this.choice3.UseVisualStyleBackColor = true;
            this.choice3.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(533, 300);
            this.Controls.Add(this.choice3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Speaker);
            this.Controls.Add(this.choice2);
            this.Controls.Add(this.choice1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.DialogueLabel);
            this.Controls.Add(this.dialoguePanel);
            this.Controls.Add(this.Character);
            this.Controls.Add(this.Scene);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Character)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Scene)).EndInit();
            this.dialoguePanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

            }

            #endregion

            private System.Windows.Forms.Label DialogueLabel;
            private System.Windows.Forms.TextBox textBox1;
            private System.Windows.Forms.Button button1;
            private System.Windows.Forms.Button choice1;
            private System.Windows.Forms.Button choice2;
            private System.Windows.Forms.PictureBox Scene;
            private System.Windows.Forms.PictureBox Character;
            private System.Windows.Forms.Label Speaker;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.Panel dialoguePanel;
            private System.Windows.Forms.Button nextbutton;
        private System.Windows.Forms.Button choice3;
    }
    }

