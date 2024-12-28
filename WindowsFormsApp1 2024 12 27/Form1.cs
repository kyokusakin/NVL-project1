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
        private Dictionary<string, Dialogue> dialogues;
        private Dictionary<string, Option> options;

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
            var optionsList = JsonConvert.DeserializeObject<List<Option>>(json);
            options = optionsList.ToDictionary(option => option.Id, option => option);
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

        private void ShowOptions(List<string> optionIds)
        {
            choice1.Visible = false;
            choice2.Visible = false;
            nextbutton.Enabled = false;

            if (optionIds.Count > 0)
            {
                choice1.Text = options[optionIds[0]].Text;
                choice1.Tag = options[optionIds[0]].NextDialogueId;
                choice1.Visible = true;
                choice1.Enabled = true;
            }

            if (optionIds.Count > 1)
            {
                choice2.Text = options[optionIds[1]].Text;
                choice2.Tag = options[optionIds[1]].NextDialogueId;
                choice2.Visible = true;
                choice2.Enabled = true;
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
                ShowOptions(item.Options);
                isChoosingOption = true;
            }
        }

        private void choice1_Click(object sender, EventArgs e)
        {
            choice1.Enabled = false;
            choice2.Enabled = false;
            choice1.Visible = false;
            choice2.Visible = false;
            nextbutton.Enabled = true;
            isChoosingOption = false;

            string nextDialogueId = (string)choice1.Tag;
            ProcessNextDialogue(nextDialogueId);
        }

        private void choice2_Click(object sender, EventArgs e)
        {
            choice1.Enabled = false;
            choice2.Enabled = false;
            choice1.Visible = false;
            choice2.Visible = false;
            nextbutton.Enabled = true;
            isChoosingOption = false;

            string nextDialogueId = (string)choice2.Tag;
            ProcessNextDialogue(nextDialogueId);


        }

        private void ProcessNextDialogue(string dialogueId)
        {
            ShowDialogue(dialogueId);
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

    public class Option
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string NextDialogueId { get; set; }
    }

    public class SequenceItem
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public List<string> Options { get; set; }
    }
}
