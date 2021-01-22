using Calendar.Main;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Calendar
{
    public partial class MainForm : Form
    { 
        private static int currentDBindex=0;
        private static bool restoreData=true;
        public static Form form = null;
        private static int lastItemPosi_Y = 0;
        public MainForm()
        {
            restoreData = true;
            InitializeComponent();     
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void mainP_Move(object sender, MouseEventArgs e)
        {
            
        }
        private void mainForm_Activated(object sender, EventArgs e)
        {
            if (restoreData)
            {
                form = this;

                MainPanelConfig();

                menuPanel_2_config_activated();

                restoreData = false;
            }
        }

        #region Main

        private void MainPanelConfig()
        {
            if (!OccursWhenHaveNoTask())
            {
                GenerateMainElements();
            }
        }

        //HAVE TASKS PANEL
        private void GenerateMainElements()
        {
            CollectCurrentDayTasks();

            int posY_label = 17;

            int task_number = CountTasks();

            for (int i = 0; i < task_number; i++)
            {
                Panel panel = new Panel()
                {
                    //Style
                    Size=new Size(950,73),
                    Location=new Point(17, posY_label),

                    //Data
                    Name=$"taskPanel{i+1}"

                };
                Button button = new Button()
                {
                    //Style
                    Location = new Point(3, 17),
                    Size = new Size(40, 39),
                    BackColor = Color.Transparent,
                    FlatStyle = FlatStyle.Flat,
                    BackgroundImageLayout = ImageLayout.Zoom,

                    //Data
                    BackgroundImage = SelectAlarmImage(),
                    Name=$"taskButton{i+1}"
                };
                Label titleLabel = new Label()
                {
                    //Style
                    Location=new Point(49,13),
                    Font=new Font(new FontFamily("Microsoft Sans Serif"),14f),

                    //Data
                    Name=$"taskTitleLabel{i+1}",
                    Text= SelectTitleText()
                };
                Label dateLabel = new Label()
                {
                    //Style
                    Location = new Point(54,45),
                    Font = new Font(new FontFamily("Microsoft Sans Serif"), 12f),

                    //Data
                    Name = $"taskDateLabel{i + 1}",
                    Text = SelectAlarmDate()
                };
                //Style
                button.FlatAppearance.BorderSize = 0;

                //Data
                Control[] panel_content =
                {
                    button,titleLabel,dateLabel
                };

                panel.Controls.AddRange(panel_content);

                mainPanel.Controls.Add(panel);

                posY_label += 88;
                lastItemPosi_Y = posY_label;
            }

            InsertNewTask();
        }
         
        //SORRY PANEL
        private void InsertNewTask()
        {
            Panel panel = new Panel()
            {
                Size=new Size(612,81),
                Location=new Point(170,lastItemPosi_Y),
                Name="Insert_Panel_O"
            };
            Button button = new Button()
            {
                Location=new Point(108,24),
                Size=new Size(85,54),
                BackgroundImage=Image.FromFile(@"C:\Users\bakon\OneDrive\Asztali gép\Infó\C#\Calendar\Assets\add_icon.png"),
                BackgroundImageLayout=ImageLayout.Zoom,
                BackColor=Color.Transparent,
                FlatStyle=FlatStyle.Flat,
                Name="Insert_Panel_Label_O"
            };
            Label label = new Label()
            {
                Location=new Point(211,34),
                Font=new Font(new FontFamily("Microsoft Tai Le"),18f),
                Name="Insert_Panel_Label_O",
                Text="Adj hozzá új feladatot.",
                Size=new Size(265,31)
            };

            button.FlatAppearance.BorderSize = 0;
            button.Click += insertButton_Click;

            Control[] control_content =
            {
                button,label
            };
            panel.Controls.AddRange(control_content);
            mainPanel.Controls.Add(panel);

            lastItemPosi_Y = panel.Location.Y + panel.Size.Height;
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            //Method 1
            /*
            AddSection addSec = new AddSection();

            addSec.Show();
            
            this.Hide();
            */

            //Method 2

            ShowInsertPanel();
        }

        private void ShowInsertPanel()
        {
            Panel panel = new Panel()
            {
                Name = "Insert_Panel_Show",
                Location = new Point(257, lastItemPosi_Y+20),
                Size = new Size(436, 269)
                
            };
            Label title = new Label()
            {
                Text="Fontosság:",
                Location=new Point(156,10),
                Font=new Font(new FontFamily("Microsoft YaHei"),14f),
                Size=new Size(114,26)
            };
            Button bt1 = new Button()
            {
                BackgroundImage = Image.FromFile(@"C:\Users\bakon\OneDrive\Asztali gép\Infó\C#\Calendar\Assets\level1_flag.png"),
                Location=new Point(53,48),
                BackgroundImageLayout=ImageLayout.Zoom,
                Size=new Size(78,38)
            };
            Button bt2 = new Button()
            {
                BackgroundImage = Image.FromFile(@"C:\Users\bakon\OneDrive\Asztali gép\Infó\C#\Calendar\Assets\level2_flag.png"),
                Location = new Point(173, 48),
                BackgroundImageLayout = ImageLayout.Zoom,
                Size = new Size(78, 38)
            };
            Button bt3 = new Button()
            {
                BackgroundImage = Image.FromFile(@"C:\Users\bakon\OneDrive\Asztali gép\Infó\C#\Calendar\Assets\level3_flag.png"),
                Location = new Point(300, 48),
                BackgroundImageLayout = ImageLayout.Zoom,
                Size = new Size(78, 38)
            };
            RadioButton rd1 = new RadioButton()
            {
                Text="Ráér",
                Name="rdButton1",
                Location=new Point(72,91)
            };
            RadioButton rd2 = new RadioButton()
            {
                Text = "Fontos",
                Name = "rdButton2",
                Location = new Point(187, 91)
            };
            RadioButton rd3 = new RadioButton()
            {
                Text = "Nagyon fontos",
                Name = "rdButton3",
                Location = new Point(291, 91)
            };
            Label date_Title = new Label()
            {
                Font=new Font(new FontFamily("Microsoft Tai Le"),12f),
                Text="Dátum",
                Location=new Point(131,112)
            };
            TextBox txtBox_Date = new TextBox()
            {
                Size=new Size(152,29),
                Name="txtBoxDate",
                Location=new Point(126,136),
                Font = new Font(new FontFamily("Microsoft Tai Le"), 14f),

            };
            Label content_Title = new Label()
            {
                Font = new Font(new FontFamily("Microsoft Tai Le"), 12f),
                Text = "Tartalom",
                Location=new Point(61,170),
                
            };
            TextBox txtBox_Content = new TextBox()
            {
                Size = new Size(308, 36),
                Name = "txtBoxContent",
                Location = new Point(57, 194),
                Font = new Font(new FontFamily("Microsoft Tai Le"), 10f),
                Multiline=true
            };
            Button insert_Btn = new Button()
            {
                Location=new Point(173,237),
                Text="Hozzáad",
                Font = new Font(new FontFamily("Microsoft Tai Le"), 12f),
                Size=new Size(97,30)
            };

            insert_Btn.Click += insertNewTask_Click;

            Control[]control =
            {
                title,bt1,bt2,bt3,rd1,rd2,rd3,date_Title,txtBox_Date,content_Title,
                txtBox_Content,insert_Btn
            };

            panel.Controls.AddRange(control);

            mainPanel.Controls.Add(panel);    
            
        }

        private void insertNewTask_Click(object sender, EventArgs e)
        {
            Control ctl = sender as Control;
            Control pctl = ctl.Parent;

            Task task = new Task();
            int _level = 1;
            DateTime _alarm=new DateTime();
            string _content = "";

            RadioButton checkedButton = pctl.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked);

            

            for (int x = 0; x < pctl.Controls.Count; x++)
            {
                if (pctl.Controls[x].Name == "txtBoxDate")
                {
                    _alarm = Convert.ToDateTime(pctl.Controls[x].Text);
                }
                if (pctl.Controls[x].Name == "txtBoxContent")
                {
                    _content = Convert.ToString(pctl.Controls[x].Text);
                }
            }

            if (CheckInput(_level, _alarm, _content))
            {
                task.Insert(_level,_alarm,_content);

                Console.WriteLine("Task insertation complete..");
            } 
        }

        private bool CheckInput(int level,DateTime alarm,string content)
        {
            bool fine = false;

            bool part_1 = false;
            bool part_2 = false;
            bool part_3 = false;

            if (level < 4 && level!=0)
            {
                part_1 = true;
            }
            if(alarm != null)
            {
                part_2 = true;
            }

            if (content.Length > 3)
            {
                part_3 = true;
            }

            if (part_1 && part_2 && part_3) { fine = true; }

            //ERROR NOTIFICATION

            if (!part_1) { MessageBox.Show($"Szint: {level}\nlegyen 1 és 3 között!"); }
            if(!part_2) { MessageBox.Show($"Dátum: {alarm}\nNormális dátum legyen. pl.:( 2021.07.24. )"); }
            if (!part_3) { MessageBox.Show($"Tartalom: {content}\nLegalább 3 karakter legyen."); }

            return fine;
        }

        private bool OccursWhenHaveNoTask()
        {
            bool result = false;

            if (CountTasks() == 0)
            {
                result = true;

                PictureBox image = new PictureBox()
                {
                    Location = new Point(mainPanel.Size.Width/4, -25),
                    Size = new Size(476,455),
                    Image = Image.FromFile(@"C:\Users\bakon\OneDrive\Asztali gép\Infó\C#\Calendar\Assets\sorry_image_jak_edited.png"),
                    SizeMode=PictureBoxSizeMode.Zoom,
                    Name="sorryImage"
                };

                lastItemPosi_Y = image.Location.Y+image.Size.Height;

                mainPanel.Controls.Add(image);

                InsertNewTask();
                
            }

            return result;
        }

        #endregion

        #region menuPanel_1



        #endregion

        #region menuPanel_2

        private void menuPanel_2_config_activated()
        {
            menuP2_Content_Activated();

        }
        private void menuP2_Content_Activated()
        {
            currentDayName.Text = Calendar.choosedDay_Name;

            int task_number = CountTasks();

            currentTasksLbl.Text = task_number.ToString();
        }

        #endregion

        #region Functions
        private void CollectCurrentDayTasks()
        {
            foreach (var item in Task.DbContent)
            {
                Console.WriteLine(item.Date_Id);
                if (item.Date_Id == Calendar.choosedDay)
                {   
                    Calendar.selectedTask.Add(new Task
                    {
                        Date_Id = item.Date_Id,
                        TaskLevel = item.TaskLevel,
                        Alarm = item.Alarm,
                        TaskContent = item.TaskContent
                    });
                }
            }
        }
        private int CountTasks()
        {
            int tasks = 0;

            foreach (var item in Task.DbContent)
            {
                if (item.Date_Id == Calendar.choosedDay)
                {
                    tasks++;
                }
            }

            return tasks;
        }

        private Image SelectAlarmImage()
        {
            Image alarmImage = null;

            if (Calendar.selectedTask[currentDBindex].TaskLevel == 1)
            {
                alarmImage= Image.FromFile(@"C:\Users\bakon\OneDrive\Asztali gép\Infó\C#\Calendar\Assets\level1_circle_edited.png");
            }
            else if (Calendar.selectedTask[currentDBindex].TaskLevel == 2)
            {
                alarmImage = Image.FromFile(@"C:\Users\bakon\OneDrive\Asztali gép\Infó\C#\Calendar\Assets\level2_circle_edited.png");
            }
            else
            {
                alarmImage = Image.FromFile(@"C:\Users\bakon\OneDrive\Asztali gép\Infó\C#\Calendar\Assets\level3_circle_edited.png");
            }

            
            return alarmImage;
        }

        private string SelectTitleText()
        {
            string content = "";

            content = Calendar.selectedTask[currentDBindex].TaskContent;

            return content;
        }

        private string SelectAlarmDate()
        {
            DateTime date;
            string content = "";

            date = Calendar.selectedTask[currentDBindex].Alarm;
            content = date.ToString();

            currentDBindex++;

            return content;
        }

        #endregion

        private void btnBackForm_Click(object sender, EventArgs e)
        { 
            Calendar.form.Show();

            this.Hide();
        }
    }
}
