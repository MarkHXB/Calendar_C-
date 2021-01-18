using Calendar.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Calendar.Main.GetTask;

namespace Calendar
{
    public partial class MainForm : Form
    {
        static AddSection addSec;
        static int color = 0;
        static Button button;
        static bool[] pressed;
        static List<Panel> dayPanels;
        //static List<Button> dayButtons;
        //static List<Button> addButtons;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            addSec = new AddSection();
            addSec.Opacity = 0;
            addSec.Show();

            #region SET_THE_NAMES

            setPanel_setButton_names();

            dayPanels = this.Controls.OfType<Panel>().Where(x => x.Name.Contains("day")).ToList().OrderBy(x => x.Name).ToList();
            //dayButtons = dayPanels[0].Controls.OfType<Button>().Where(x => x.Name.Contains("dt")).ToList().OrderBy(x => x.Name).ToList();
            //addButtons = this.Controls.OfType<Button>().Where(x => x.Name.Contains("add")).ToList().OrderBy(x => x.Name).ToList();

            #endregion


            pressed = new bool[dayPanels.Count];
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void sampleBtn1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void sampleBtn1_MouseLeave(object sender, EventArgs e)
        {

        }
        private void dayBtn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            panelPressed(button);
        }
        private void addBtn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
   
            Panel parent = (Panel)button.Parent;
            List<Button> dyButtons = parent.Controls.OfType<Button>().ToList();


            for (int i = 0; i < dyButtons.Count; i++)
            {
                if (dyButtons[i].Name.Contains("dt"))
                {
                    GetTask.currentDate = dyButtons[i].Name.Substring(5, 1);
                }
            }

            formFadingTimer.Start();

            //addNewTask(button, (Panel)button.Parent);
        }
        private void formFading()
        {
            if (this.Opacity > 0)
            {
                this.Opacity -= .10;
                addSec.Opacity += .10;
            }
            else
            {
                this.Hide();
                formFadingTimer.Stop();
            }
        }
        private void animationTime_Tick(object sender, EventArgs e)
        {
            color += 6;
            if (color == 252)
            {
                animationTime.Stop();
            }

            button.FlatAppearance.BorderColor = Color.FromArgb(color, 0, 0);
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button = (Button)sender;
            animationTime.Start();
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            animationTime.Stop();
            button.FlatAppearance.BorderColor = Color.Black;
            color = 0;
        }


        #region PANEL_BUTTON_SET_NAMES_ONLOAD

        private void setPanel_setButton_names()
        {
            //needed for days panels
            //PANEL
            List<Panel> panels = this.Controls.OfType<Panel>().ToList();
            panels = panels.OrderBy(x => x.Name).ToList();
            int panelCC = 0;
            for (int i = 0; i < panels.Count; i++)
            {
                if (panels[i].Name.Contains("panel"))
                {
                    panels[i].Name = "dayPanel" + panelCC;

                }
                panelCC++;

            }





            //DAYBUTTON
            int dayBtCC = 1;
            int days = 1;
            for (int i = 0; i < panels.Count; i++)
            {
                if (panels[i].Name.Contains("day"))
                {
                    List<Button> btns = panels[i].Controls.OfType<Button>().ToList();

                    btns = btns.OrderBy(x => x.Name).ToList();

                    for (int j = 0; j < btns.Count; j++)
                    {
                        if (btns[j].Text != "")
                        {
                            btns[j].Text = (days++).ToString();

                            btns[j].Name = "dtBtn" + dayBtCC.ToString();
                            btns[j].Click += dayBtn_Click;

                            dayBtCC++;
                        }

                    }

                }
            }




            //ADDBUTTON
            int addBtCC = 0;

            for (int i = 0; i < panels.Count; i++)
            {
                if (panels[i].Name.Contains("day"))
                {
                    List<Button> btns = panels[i].Controls.OfType<Button>().ToList();
                    for (int b = 0; b < btns.Count; b++)
                    {
                        if (!btns[b].Name.Contains("dt"))
                        {
                            btns[b].Name = "addBtn" + addBtCC.ToString();
                            btns[b].Click += addBtn_Click;
                            btns[b].Visible = false;
                            addBtCC++;

                        }
                    }

                }

            }

            //ADDLABEL
            int aLblCounter = 0;
            for (int i = 0; i < panels.Count; i++)
            {
                List<Label> aLbls = panels[i].Controls.OfType<Label>().ToList();

                for (int j = 0; j < aLbls.Count; j++)
                {
                    aLbls[j].Name = "addLbl" + (aLblCounter).ToString();

                }
                aLblCounter++;
            }
        }

        #endregion


        #region PANEL_HANDLING_ONLOAD

        #endregion


        #region PANEL_HANDLING
        /*********************/
        /*******  TEST  ******/
        /*********************/
        private void panelPressed(Button button)
        {
            for (int i = 0; i < pressed.Length; i++)
            {
                if (!pressed[i]) { pressed[i] = true; }
                else { pressed[i] = false; }

            }
            panelVisible(button);
        }
        private void panelVisible(Button button)
        {
            for (int i = 0; i < pressed.Length; i++)
            {
                if (Convert.ToString(i + 1) == button.Name.Substring(5, 1))
                {
                    if (pressed[i])
                    {
                        for (int j = 0; j < dayPanels.Count; j++)
                        {
                            if (Convert.ToString(j + 1) == button.Name.Substring(5, 1))
                            {
                                List<Panel> sbPanels = dayPanels[j].Controls.OfType<Panel>().ToList();
                                if (sbPanels.Count != 0)
                                {

                                    subPanelsLayout(sbPanels, true);

                                }
                                else
                                {
                                    addDayBtnLayout(j, true);

                                    addLabelLayout(j, true);
                                }
                            }
                        }
                    }
                    else if (!pressed[i])
                    {
                        for (int j = 0; j < dayPanels.Count; j++)
                        {
                            if (Convert.ToString(j + 1) == button.Name.Substring(5, 1))
                            {
                                List<Panel> sbPanels = dayPanels[j].Controls.OfType<Panel>().ToList();
                                if (sbPanels.Count != 0)
                                {

                                    subPanelsLayout(sbPanels, false);

                                }
                                else
                                {
                                    addDayBtnLayout(j, false);

                                    addLabelLayout(j, false);
                                }
                            }
                        }
                    }
                }
            }
        }
        /*********************/
        /*******  TEST  ******/
        /*********************/

        private void subPanelsLayout(List<Panel> sbPanels, bool visible)
        {
            sbPanels = sbPanels.OrderBy(x => x.Name).ToList();

            for (int x = 0; x < sbPanels.Count; x++)
            {
                sbPanels[x].Visible = visible;
            }
        }

        private void addLabelLayout(int index,bool visible)
        {
            List<Label> addLbls = dayPanels[index].Controls.OfType<Label>().ToList();
            for (int y = 0; y < addLbls.Count; y++)
            {
                addLbls[y].Visible = visible;
            }
        }
        private void addLabelLayout(Panel currentPanel,bool visible)
        {
            List<Label> addLbls = currentPanel.Controls.OfType<Label>().ToList();
            for (int y = 0; y < addLbls.Count; y++)
            {
                addLbls[y].Visible = visible;
            }
        }

        private void addDayBtnLayout(int index,bool visible)
        {
            Button addBtn = dayPanels[index].Controls.OfType<Button>().Where(x => x.Name.Contains("add")).First();
            addBtn.Visible = visible;
        }
        private void addDayBtnLayout(Panel panel,bool visible)
        {
            Button addBtn = panel.Controls.OfType<Button>().Where(x => x.Name.Contains("add")).First();
            addBtn.Visible = visible;
        }
        #endregion


        #region BUTTON_ADDNEWTASK

        private List<Button> selectAddButtons()
        {
            List<Button> btn = new List<Button>();

            for (int i = 0; i < dayPanels.Count; i++)
            {
                List<Button> permanentBtn = dayPanels[i].Controls.OfType<Button>().ToList();
                permanentBtn = permanentBtn.OrderBy(x => x.Name).ToList();

                for (int j = 0; j < permanentBtn.Count; j++)
                {
                    if (permanentBtn[j].Name.Contains("add"))
                    {
                        btn.Add(permanentBtn[j]);
                    }
                }
            }

            return btn;
        }

        private void addNewTask(Button currentAddBtn,Panel belongPanel)
        {
            List<Button> buttons = selectAddButtons();

            addLabelLayout(belongPanel, false);

            addDayBtnLayout(belongPanel, false);

            foreach (var item in buttons)
            {
                if(item == currentAddBtn)
                {
                    NewTask task = new NewTask();

                    belongPanel.Controls.Add(task.original_newTask);
                }
            }
        }

        #endregion

        private void sbPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void formFadingTimer_Tick(object sender, EventArgs e)
        {
            formFading();
        }
    }
}
