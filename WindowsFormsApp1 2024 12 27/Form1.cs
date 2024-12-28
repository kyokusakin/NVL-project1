using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WindowsFormsApp1_2024_12_27
{
    public partial class Form1 : Form
    {
        private Queue<SequenceItem> sequence = new Queue<SequenceItem>();
        private bool isDialoguePlaying = false;
        private bool isChoosingOption = false;
        public const int MaxOptionCount = 3;
        private Dictionary<string, Dialogue> dialogues;
        private Dictionary<string, OptionData> options;

        public Form1()
        {
            InitializeComponent();
            LoadDialogues();
            LoadOptions();
            LoadSequence();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            DialogueLabel.Click += new EventHandler(DialogueLabel_Click);
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

            options = optionsList.ToDictionary(options=>options.Id);
        }

        private void LoadSequence()
        {
            string json = File.ReadAllText(@"Resources/sequence.json");
            var sequenceList = JsonConvert.DeserializeObject<List<SequenceItem>>(json);
            foreach (var item in sequenceList)
            {
                sequence.Enqueue(item);
            }
        }

        private async void ShowDialogue(string dialogueId)
        {
            if (isDialoguePlaying) return;
            isDialoguePlaying = true;

            if (isChoosingOption)
            {
                return;
            }
            // 禁用 nextbutton
            nextbutton.Enabled = false;

            // 檢查對話 ID 是否存在於字典中
            if (dialogues.ContainsKey(dialogueId))
            {
                var dialogue = dialogues[dialogueId]; // 直接通過 dialogueId 獲取對話
                var character = dialogue.Character;
                var dialogueText = dialogue.Text;

                Speaker.Text = character; // 顯示角色名稱
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

        private void ShowOptions(string optionId)
        {
            Button[] Choices = new Button[MaxOptionCount] { choice1, choice2, choice3 };
            DisplayChoices(false);
            //讀取選項清單
            var optionData = options[optionId];
            //設定選項資料
            for(int i =0; i< optionData.Options.Count && i< MaxOptionCount; i++)
            {
                Option option = optionData.Options[i];
                Button btn_choice = Choices[i];
                btn_choice.Text = option.Text;
                btn_choice.Tag = option.NextDialogId;
                btn_choice.Visible = true;
                btn_choice.Enabled = true;
                nextbutton.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("請輸入玩家名稱！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Speaker.Text = textBox1.Text;
            Speaker.Visible = true;
            label1.Visible = false;
            textBox1.Visible = false;
            button1.Visible = false;
            dialoguePanel.Visible = true;

            DialogueLabel.Visible = true;
            nextbutton.Visible = true;

            ProcessNextSequenceItem();
        }

        private void ProcessNextSequenceItem()
        {
            if (sequence.Count == 0)
            {
                MessageBox.Show("對話結束", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var item = sequence.Dequeue();
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
            string nextDialogueId = btn.Tag.ToString();
            isChoosingOption = false;
            nextbutton.Enabled = true;
            DisplayChoices(false);
            ShowDialogue(nextDialogueId);
        }

        private void Speaker_Click(object sender, EventArgs e)
        {
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessNextSequenceItem();
            }
        }

        private void DialogueLabel_Click(object sender, EventArgs e)
        {
            ProcessNextSequenceItem();
        }

        private void nextbutton_Click(object sender, EventArgs e)
        {
            ProcessNextSequenceItem();
        }

        private void Character_Click(object sender, EventArgs e)
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
        public string NextDialogId { get; set; }
    }


    public class SequenceItem
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public string Option { get; set; }
    }
}
