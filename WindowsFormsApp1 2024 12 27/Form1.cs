using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Drawing;

namespace WindowsFormsApp1_2024_12_27
{
    public partial class Form1 : Form
    {
        private Dictionary<string, Queue<SequenceItem>> Sequences = new Dictionary<string, Queue<SequenceItem>>();
        private bool isDialoguePlaying = false;
        private bool isChoosingOption = false;
        public const int MaxOptionCount = 3;
        private Dictionary<string, Dialogue> dialogues;
        private Dictionary<string, OptionData> options;
        private Dictionary<string, string> characterImages;
        public Queue<SequenceItem> CurrentSequence { get; set; } = new Queue<SequenceItem>();
        public string MainCharactor = "";
        public Form1()
        {
            InitializeComponent();
            InitializeData();
            InitializeForm();
        }

        public void InitializeForm() //初始化視窗
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            DialogueLabel.Click += new EventHandler(DialogueLabel_Click);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.DialogueLabel.Parent = this.dialoguePanel;
            this.Speaker.Parent = this.dialoguePanel;
            this.nextbutton.Parent = this.dialoguePanel;
            this.Character.Parent = this.Scene;
            this.button_Reset.Parent = this.Scene;
            this.button1.Parent = this.Scene;
            SetLabelLoction(DialogueLabel, dialoguePanel);
            SetLabelLoction(Speaker, dialoguePanel);
            Button[] choices = new Button[MaxOptionCount] { choice1, choice2, choice3 };
            foreach (var choice in choices)
            {
                choice.Parent = this.Scene;
            }
        }

        public void InitializeData()
        {
            LoadDialogues();
            LoadOptions();
            LoadSequence();
            LoadCharacterImages();
            DisplayChoices(false);
            ChangeImage("Background1", Scene);
            Speaker.Visible = false;
            label1.Visible = true;
            textBox1.Visible = true;
            button1.Visible = true;
            dialoguePanel.Visible = true;
            isChoosingOption = true;
            isDialoguePlaying = false;
            DialogueLabel.Visible = false;
            nextbutton.Visible = false;
            Character.Visible = false;
        }

        private void SetLabelLoction(Control control, Control panel)
        {
            Point oldPoint = control.Location;
            control.Location = new Point(oldPoint.X - panel.Location.X, oldPoint.Y - panel.Location.Y);
        }
        
        private void LoadDialogues() //載入對話
        {
            string json = File.ReadAllText(@"Resources/dialogues.json");
            dialogues = JsonConvert.DeserializeObject<Dictionary<string, Dialogue>>(json);
        }

        private void LoadOptions() //載入選項
        {
            string json = File.ReadAllText(@"Resources/options.json");
            var optionsList = JsonConvert.DeserializeObject<List<OptionData>>(json);

            options = optionsList.ToDictionary(options => options.Id);
        }

        private void LoadSequence() //載入對話序列
        {
            CurrentSequence = null;
            Sequences.Clear();
            string[] files = Directory.GetFiles(@"Resources/sequence");

            foreach (string file in files)
            {
                if (!file.Contains("sequence"))
                    continue;
                string json = File.ReadAllText(file);
                var sequenceList = JsonConvert.DeserializeObject<List<SequenceItem>>(json);

                string name = Path.GetFileNameWithoutExtension(file);
                Queue<SequenceItem> new_seq = new Queue<SequenceItem>();
                foreach (var item in sequenceList)
                    new_seq.Enqueue(item);
                Sequences.Add(name, new_seq);
            }
            CurrentSequence = Sequences["start_sequence"];
        }

        private void LoadCharacterImages() //載入角色圖片
        {
            try
            {
                string json = File.ReadAllText(@"Resources/Character.json");
                characterImages = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"無法載入角色圖片資訊：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetCharacterImage(string characterName) //取得角色圖片
        {
            if (characterImages.TryGetValue(characterName, out string imageName))
            {
                return imageName;
            }
            return null;
        }


        private void GameStart(object sender, EventArgs e) //遊戲開始按鈕
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("請輸入玩家名稱！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MainCharactor = textBox1.Text;
            Speaker.Visible = true;
            label1.Visible = false;
            textBox1.Visible = false;
            button1.Visible = false;
            dialoguePanel.Visible = true;

            DialogueLabel.Visible = true;
            nextbutton.Visible = true;
            nextbutton.Enabled = true;
            isChoosingOption = false;
            Character.Visible = true;
            ProcessNextSequenceItem();
        }

        private async void ShowDialogue(string dialogueId) //顯示對話
        {

            if (isChoosingOption || isDialoguePlaying)
                return;
            isDialoguePlaying = true;
            nextbutton.Enabled = false;

            if (dialogues.ContainsKey(dialogueId))
            {
                var dialogue = dialogues[dialogueId];
                var dialogueText = dialogue.Text;
                var character = dialogue.Character;
                var expression = dialogue.expression;
                var backgroundImg = dialogue.BackgroundImg;

                if (character == "主角")
                {
                    Speaker.Text = MainCharactor;
                    string imageName = GetCharacterImage(character);
                    if (!string.IsNullOrEmpty(imageName))
                    {
                        ChangeImage(imageName, Character);
                        Character.Visible = true;
                    }
                }
                else
                {
                    Speaker.Text = character;
                    string imageName = GetCharacterImage(character);
                    if (!string.IsNullOrEmpty(imageName))
                    {
                        ChangeImage(imageName, Character);
                        Character.Visible = true;
                    }
                    else
                    {
                        Character.Visible = false;
                    }
                }

                if (!string.IsNullOrEmpty(expression))
                {
                    ChangeImage(expression, Character);
                    Character.Visible = true;
                }

                if (!string.IsNullOrEmpty(backgroundImg))
                {
                    ChangeImage(backgroundImg, Scene);
                    Character.Visible = true;
                }

                DialogueLabel.Text = "";

                foreach (char c in dialogueText) //逐字顯示
                {
                    DialogueLabel.Text += c;
                    await Task.Delay(50);
                }
            }
            else
            {
                MessageBox.Show("無法找到對話 ID: " + dialogueId, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            isDialoguePlaying = false;
            nextbutton.Enabled = true;
        }

        private void ShowOptions(string optionId) //顯示選項
        {
            Button[] Choices = new Button[MaxOptionCount] { choice1, choice2, choice3 };
            DisplayChoices(false);
            var optionData = options[optionId];
            for (int i = 0; i < optionData.Options.Count && i < MaxOptionCount; i++)
            {
                Option option = optionData.Options[i];
                Button btn_choice = Choices[i];
                btn_choice.Text = option.Text;
                btn_choice.Tag = option;
                btn_choice.Visible = true;
                btn_choice.Enabled = true;
                nextbutton.Enabled = false;
            }
        }

        private void ProcessNextSequenceItem() //處理下一個對話或選項
        {
            if (isChoosingOption || CurrentSequence.Count == 0)
            {
                return;
            }
            if (isDialoguePlaying)
            {
                return;
            }
            var item = CurrentSequence.Dequeue();
            if (item.Type == "dialogue")
            {
                ShowDialogue(item.Id);
            }
            else if (item.Type == "option")
            {
                ShowOptions(item.Option);
                isChoosingOption = true;
            }
        }

        public void DisplayChoices(bool IsVisible) //顯示選項
        {
            Button[] Choices = new Button[MaxOptionCount] { choice1, choice2, choice3 };
            foreach (var btn in Choices)
                btn.Visible = IsVisible;
        }

        private void choice_Click(object sender, EventArgs e) //選項按鈕事件
        {
            Button[] Choices = new Button[MaxOptionCount] { choice1, choice2, choice3 };
            Button btn = (Button)sender;
            Option option = (Option)btn.Tag;
            isChoosingOption = false;
            nextbutton.Enabled = true;
            DisplayChoices(false);
            if (!string.IsNullOrEmpty(option.sequenceId))
                CurrentSequence = new Queue<SequenceItem>(Sequences[option.sequenceId]);

            if (!string.IsNullOrEmpty(option.BackgroundImg))
                ChangeImage(option.BackgroundImg, Scene);

            ProcessNextSequenceItem();
        }

        private void ChangeImage(string resourceName, PictureBox targetPictureBox) //更換圖片
        {
            if (string.IsNullOrEmpty(resourceName))
            {
                return;
            }
            try
            {
                if (targetPictureBox.Image != null) //釋放資源
                {
                    targetPictureBox.Image.Dispose();
                    targetPictureBox.Image = null;
                }

                object resource = WindowsFormsApp1_2024_12_27.Properties.Resources.ResourceManager.GetObject(resourceName);

                if (resource == null)
                {
                    throw new InvalidOperationException($"資源 {resourceName} 不存在。");
                }

                if (resource is Bitmap bitmap)
                {
                    targetPictureBox.Image = (Bitmap)bitmap.Clone();
                    targetPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else if (resource is byte[] imageData)
                {
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        targetPictureBox.Image = (Bitmap)Image.FromStream(ms).Clone();
                    }
                    targetPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    throw new InvalidOperationException($"資源 {resourceName} 不是有效的 Bitmap 或 byte[] 資源。");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"無法從資源中載入圖像：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) //按下 Enter 鍵繼續對話
        {
            if (keyData == Keys.Enter)
            {
                ProcessNextSequenceItem();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void DialogueLabel_Click(object sender, EventArgs e) => ProcessNextSequenceItem();

        private void nextbutton_Click(object sender, EventArgs e) => ProcessNextSequenceItem();

        private void button_Reset_Click(object sender, EventArgs e) => InitializeData();
    }

    public class Dialogue
    {
        public string Character { get; set; }
        public string Text { get; set; }
        public string expression { get; set; }
        public string BackgroundImg { get; set; }
    }

    public class OptionData
    {
        public string Id { get; set; }
        public List<Option> Options { get; set; } = new List<Option>();
    }

    public class Option
    {
        public string Text { get; set; }
        public string sequenceId { get; set; }
        public string BackgroundImg { get; set; }
    }


    public class SequenceItem
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public string Option { get; set; }
    }
}
