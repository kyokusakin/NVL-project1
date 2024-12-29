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
            DialogueLabel.Click += new EventHandler(DialogueLabel_Click);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Scene.SendToBack();
            this.Character.BackColor = Color.Transparent;
            this.Character.Parent = this.Scene;
            this.Character.BringToFront();
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
        private void LoadDialogues()
        {
            string json = File.ReadAllText(@"Resources/dialogues.json");
            dialogues = JsonConvert.DeserializeObject<Dictionary<string, Dialogue>>(json);
        }

        private void LoadOptions()
        {
            string json = File.ReadAllText(@"Resources/options.json");
            var optionsList = JsonConvert.DeserializeObject<List<OptionData>>(json);

            options = optionsList.ToDictionary(options => options.Id);
        }

        private void LoadSequence()
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

        private void LoadCharacterImages()
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

        private void GameStart(object sender, EventArgs e)
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

        private async void ShowDialogue(string dialogueId)
        {

            if (isChoosingOption || isDialoguePlaying)
                return;
            isDialoguePlaying = true;

            // 禁用 nextbutton
            nextbutton.Enabled = false;

            // 檢查對話 ID 是否存在於字典中
            if (dialogues.ContainsKey(dialogueId))
            {
                var dialogue = dialogues[dialogueId]; // 直接通過 dialogueId 獲取對話
                var character = dialogue.Character;
                var dialogueText = dialogue.Text;

                if (character == "主角")
                {
                    Speaker.Text = MainCharactor;
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
                DialogueLabel.Text = "";

                foreach (char c in dialogueText)
                {
                    DialogueLabel.Text += c;
                    await Task.Delay(50); // 顯示每個字的間隔
                }
            }
            else
            {
                MessageBox.Show("無法找到對話 ID: " + dialogueId, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            isDialoguePlaying = false;
            // 啟用 nextbutton
            nextbutton.Enabled = true;
        }

        private string GetCharacterImage(string characterName)
        {
            if (characterImages.TryGetValue(characterName, out string imageName))
            {
                return imageName;
            }
            return null;
        }


        private void ShowOptions(string optionId)
        {
            Button[] Choices = new Button[MaxOptionCount] { choice1, choice2, choice3 };
            DisplayChoices(false);
            //讀取選項清單
            var optionData = options[optionId];
            //設定選項資料
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

        private void ProcessNextSequenceItem()
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
        public void DisplayChoices(bool IsVisible)
        {
            Button[] Choices = new Button[MaxOptionCount] { choice1, choice2, choice3 };
            foreach (var btn in Choices)
                btn.Visible = IsVisible;
        }

        private void choice_Click(object sender, EventArgs e)
        {
            Button[] Choices = new Button[MaxOptionCount] { choice1, choice2, choice3 };
            Button btn = (Button)sender;
            Option option = (Option)btn.Tag;
            isChoosingOption = false;
            nextbutton.Enabled = true;
            DisplayChoices(false);
            if (!string.IsNullOrEmpty(option.sequenceId))
            {
                if (option.sequenceId == option.sequenceId)
                {
                    CurrentSequence = new Queue<SequenceItem>(Sequences[option.sequenceId]);
                }
                else
                {
                    CurrentSequence = Sequences[option.sequenceId];
                }
            }

            if (!string.IsNullOrEmpty(option.BackgroundImg))
            {
                ChangeImage(option.BackgroundImg, Scene);
            }
            ProcessNextSequenceItem();
        }

        private void Speaker_Click(object sender, EventArgs e) { }

        private void ChangeImage(string resourceName, PictureBox targetPictureBox)
        {
            if (!string.IsNullOrEmpty(resourceName))
            {
                try
                {
                    object resource = WindowsFormsApp1_2024_12_27.Properties.Resources.ResourceManager.GetObject(resourceName);

                    if (resource is Bitmap bitmap)
                    {
                        targetPictureBox.Image = bitmap;
                        targetPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else if (resource is byte[] imageData)
                    {
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            targetPictureBox.Image = Image.FromStream(ms);
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
                    MessageBox.Show($"無法從資源中加載圖像：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                ProcessNextSequenceItem();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void DialogueLabel_Click(object sender, EventArgs e) => ProcessNextSequenceItem();

        private void nextbutton_Click(object sender, EventArgs e) => ProcessNextSequenceItem();

        private void Character_Click(object sender, EventArgs e)
        {
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            InitializeData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

    public class Dialogue
    {
        public string Character { get; set; }
        public string Text { get; set; }
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
