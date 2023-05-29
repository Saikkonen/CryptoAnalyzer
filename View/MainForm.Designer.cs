namespace CryptoAnalyzer
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
            dateTimePickerFrom = new DateTimePicker();
            dateTimePickerTo = new DateTimePicker();
            label_Result = new Label();
            label_From = new Label();
            label_To = new Label();
            button_Fetch = new Button();
            SuspendLayout();
            // 
            // dateTimePickerFrom
            // 
            dateTimePickerFrom.Location = new Point(73, 70);
            dateTimePickerFrom.Name = "dateTimePickerFrom";
            dateTimePickerFrom.Size = new Size(200, 23);
            dateTimePickerFrom.TabIndex = 0;
            // 
            // dateTimePickerTo
            // 
            dateTimePickerTo.Location = new Point(307, 70);
            dateTimePickerTo.Name = "dateTimePickerTo";
            dateTimePickerTo.Size = new Size(200, 23);
            dateTimePickerTo.TabIndex = 1;
            // 
            // label_Result
            // 
            label_Result.AutoSize = true;
            label_Result.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label_Result.Location = new Point(199, 193);
            label_Result.Name = "label_Result";
            label_Result.Size = new Size(0, 19);
            label_Result.TabIndex = 2;
            // 
            // label_From
            // 
            label_From.AutoSize = true;
            label_From.Location = new Point(73, 52);
            label_From.Name = "label_From";
            label_From.Size = new Size(35, 15);
            label_From.TabIndex = 3;
            label_From.Text = "From";
            // 
            // label_To
            // 
            label_To.AutoSize = true;
            label_To.Location = new Point(307, 52);
            label_To.Name = "label_To";
            label_To.Size = new Size(19, 15);
            label_To.TabIndex = 4;
            label_To.Text = "To";
            // 
            // button_Fetch
            // 
            button_Fetch.Location = new Point(539, 70);
            button_Fetch.Name = "button_Fetch";
            button_Fetch.Size = new Size(200, 23);
            button_Fetch.TabIndex = 5;
            button_Fetch.Text = "Fetch";
            button_Fetch.UseVisualStyleBackColor = true;
            button_Fetch.Click += Button_Fetch_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button_Fetch);
            Controls.Add(label_To);
            Controls.Add(label_From);
            Controls.Add(label_Result);
            Controls.Add(dateTimePickerTo);
            Controls.Add(dateTimePickerFrom);
            Name = "MainForm";
            Text = "Crypto Analyzer";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker dateTimePickerFrom;
        private DateTimePicker dateTimePickerTo;
        private Label label_Result;
        private Label label_From;
        private Label label_To;
        private Button button_Fetch;
    }
}