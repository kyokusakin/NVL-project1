using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Windows.Forms.Integration;
using Viewbox = System.Windows.Controls.Viewbox;
using WindowsFormsApp1_2024_12_27.Properties;
namespace WindowsFormsApp1_2024_12_27
{
    public partial class Form1 : Form
    {
        private Dictionary<string, Queue<SequenceItem>> Sequences =new Dictionary<string, Queue<SequenceItem>>();
        private bool isDialoguePlaying = false;
        private bool isChoosingOption = false;
        public const int MaxOptionCount = 3;
        private Dictionary<string, Dialogue> dialogues;
        private Dictionary<string, OptionData> options;
        public Queue<SequenceItem> CurrentSequence { get; set; } = new Queue<SequenceItem>();
        public string MainCharactor = "";
        public Form1()
        {
            InitializeComponent();
            InitializeData();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            DialogueLabel.Click += new EventHandler(DialogueLabel_Click);
            //自動放大縮小匹配介面
            ElementHost host = new ElementHost {
                Dock = DockStyle.Fill
            };
            WindowsFormsHost windowsFormsHost = new WindowsFormsHost {
                Child = panel_Main
            };

            Viewbox viewbox = new Viewbox {
                Child = windowsFormsHost
            };
            host.Child = viewbox;
            this.Controls.Add(host);
        }
        public void InitializeData()
        {
            LoadDialogues();
            LoadOptions();
            LoadSequence();
            DisplayChoices(false);
            Speaker.Visible = false;
            label1.Visible = true;
            textBox1.Visible = true;
            button1.Visible = true;
            dialoguePanel.Visible = true;
            isChoosingOption = false;
            isDialoguePlaying = false;
            DialogueLabel.Visible = false;
            nextbutton.Visible = false;
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
            CurrentSequence = null;
            Sequences.Clear();
            string[] files = Directory.GetFiles(@"./Resources");

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

                Speaker.Text = (character=="主角")? MainCharactor : character; // 顯示角色名稱
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
                btn_choice.Tag = option;
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

            MainCharactor = textBox1.Text; 
            Speaker.Visible = true;
            label1.Visible = false;
            textBox1.Visible = false;
            button1.Visible = false;
            dialoguePanel.Visible = true;

            DialogueLabel.Visible = true;
            nextbutton.Visible = true;
            nextbutton.Enabled = true;
            ProcessNextSequenceItem();
        }

        private void ProcessNextSequenceItem()
        {
            if (CurrentSequence.Count == 0)
            {
                //MessageBox.Show("對話結束", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (isChoosingOption)
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
            Option option  = (Option)btn.Tag;
            isChoosingOption = false;
            nextbutton.Enabled = true;
            DisplayChoices(false);
            if(!string.IsNullOrEmpty(option.sequenceId))
                CurrentSequence = Sequences[option.sequenceId];
            ProcessNextSequenceItem();
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

        private void DialogueLabel_Click(object sender, EventArgs e) => ProcessNextSequenceItem();

        private void nextbutton_Click(object sender, EventArgs e) => ProcessNextSequenceItem();

        private void Character_Click(object sender, EventArgs e)
        {
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            InitializeData();
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
    }


    public class SequenceItem
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public string Option { get; set; }
    }
}
