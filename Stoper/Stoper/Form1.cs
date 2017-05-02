using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Threading;
using System.Media;

namespace Stoper
{
    public partial class Form1 : Form
    {
        private SoundPlayer soundPlayer;
        string wybranaGodzina = "";
        string lokacjaPiosenki = "";
        Thread a;
        System.Timers.Timer timer;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void circularProgressBar1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            soundPlayer = new SoundPlayer();
            this.dateTimePicker1.CustomFormat = "HH:mm";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.ShowUpDown = true;
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
          //  dateTimePicker1.TextChanged += ZmianaGodziny;
            timer.Elapsed += AktualizujProgressBar;
            timer.Start();
        }

       

        //private void ZmianaGodziny(object sender, EventArgs e)
        //{
        //    string minuty = dateTimePicker1.Value.Minute < 10 ? (0.ToString() + dateTimePicker1.Value.Minute).ToString() : dateTimePicker1.Value.Minute.ToString();
        //    wybranaGodzina = dateTimePicker1.Value.Hour.ToString() +":" + minuty;
        //    MessageBox.Show("Wybrana godzina: " + wybranaGodzina);
        //}

        private void AktualizujProgressBar(object sender, ElapsedEventArgs e)
        {
            
            circularProgressBar1.Invoke(
             (MethodInvoker)delegate
                            {
                            circularProgressBar1.Text = DateTime.Now.ToString("HH:mm:ss");
                            //circularProgressBar1.SuperscriptText = DateTime.Now.ToString("tt");
                                if (wybranaGodzina == DateTime.Now.ToString("HH:mm"))
                                {
                                  //  MessageBox.Show("TA");
                                    a = new Thread(WlaczBudzik);
                                    a.Start();
                                   wybranaGodzina = "";//resetuje godzine aby event ciagle nie wywolywal metody WlaczBudzik

                                }
                            });

        }

        private void WlaczBudzik()
        {
            soundPlayer.PlayLooping();

        }

  

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(lokacjaPiosenki))
            {
                MessageBox.Show("Musisz wybrać dzwięk budzika! Dostępne tylko pliki .wav");
                return;
            }
            string minuty = dateTimePicker1.Value.Minute < 10 ? (0.ToString() + dateTimePicker1.Value.Minute).ToString() : dateTimePicker1.Value.Minute.ToString();
            wybranaGodzina = dateTimePicker1.Value.Hour.ToString() +":" + minuty;
            MessageBox.Show("Budzik włączy się o:  " + wybranaGodzina);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;

            ofd.Filter = "WAV files (*.wav)|*.wav";
            ofd.DefaultExt = ".wav";

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                lokacjaPiosenki = ofd.FileName;

                soundPlayer.SoundLocation = lokacjaPiosenki;
            }
        }
    }
}
