namespace HafizaOyunu
{
    public partial class Form1 : Form
    {
        List<string> icons = new List<string>()
        {
          "a","b","c","d","e","f","g","h","w","i","k","l","m","n","o","q","p","r","s","t",
          "a","b","c","d","e","f","g","h","w","i","k","l","m","n","o","q","p","r","s","t"
        };
        Random rnd = new Random();
        int random_index;
        Button birinci_tiklanan, ikinci_tiklanan;
        List<Button> acik_butonlar = new List<Button>();
        int aktif_oyuncu = 1;
        int oyuncu1_eslesme = 0;
        int oyuncu2_eslesme = 0;
        public Form1()
        {
            InitializeComponent();
            BaslangictaGostermeTimer.Tick += Timer1_Tick;
            HataliEslesmeTimer.Tick += Timer2_Tick;
            IkinciyiBeklemeTimer.Tick += Timer3_Tick;
        }

        private void Timer3_Tick(object? sender, EventArgs e)
        {
            IkinciyiBeklemeTimer.Stop();
            if ((birinci_tiklanan != null && ikinci_tiklanan == null))
            {
                birinci_tiklanan.ForeColor = birinci_tiklanan.BackColor;
                acik_butonlar.Remove(birinci_tiklanan);
                birinci_tiklanan = null;
                if (aktif_oyuncu == 1)
                {
                    aktif_oyuncu = 2;
                    OyuncuSirasi.Text = "Oyuncu2";
                }
                else
                {
                    aktif_oyuncu = 1;
                    OyuncuSirasi.Text = "Oyuncu1";
                }
            }

        }

        private void Timer2_Tick(object? sender, EventArgs e)
        {
            HataliEslesmeTimer.Stop();
            birinci_tiklanan.ForeColor = birinci_tiklanan.BackColor;
            ikinci_tiklanan.ForeColor = ikinci_tiklanan.BackColor;
            acik_butonlar.Clear();//Açýk buton sayýsýný sýfýrladýk.
            birinci_tiklanan = null;
            ikinci_tiklanan = null;
        }

        private void Timer1_Tick(object? sender, EventArgs e)
        {
            BaslangictaGostermeTimer.Stop();
            foreach (Button item in Controls.OfType<Button>())
            {
                item.ForeColor = item.BackColor;// 5 sn dolunca bütün ikonlar ayný renk olup görünmez olacaktýr.
                item.Enabled = true;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Button btn;
            foreach (Button btn in Controls.OfType<Button>()) // Sadece buton olanlar alýnýyor.
            {
                // btn = item as Button;
                random_index = rnd.Next(icons.Count);//Her bir labela ikonlarýn sayýsýna göre random ikon atamasý yapýlýyor.
                btn.Text = icons[random_index];//Random gelen ikonlar butonun içerisine yazýlýyor.
                btn.ForeColor = Color.Black;// Yazý rengini siyah yapýldý.
                btn.Enabled = false;
                icons.RemoveAt(random_index);//Random atadýklarýný listeden kaldýrýyor ,tekrardan index atamasýn diye.O yüzden ikonlar sayýlarak indexleniyor.
            }
            BaslangictaGostermeTimer.Interval = 5000;// Bütün ikonlarýn görünme süresi 5000 ms = 5 sn
            BaslangictaGostermeTimer.Start();
        }

        private void Buttons_clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;// O an hangi butona basýlý ise anlamýdýr.
            if (acik_butonlar.Count >= 2) { return; }//Böylelikle 2 butona basýldýðýnda 1 saniyelik bekleme sürecinde 3. ikona bassak da açýlmaz.
            if (birinci_tiklanan != null && btn == birinci_tiklanan) { return; }
            if (birinci_tiklanan == null)
            {
                birinci_tiklanan = btn;
                birinci_tiklanan.ForeColor = Color.Black;
                IkinciyiBeklemeTimer.Interval = 5000;
                IkinciyiBeklemeTimer.Start();//1.týklamadan sonra 5 saniye bekliyor. Eðer 5 saniye baþka butona týklamazsa 2.oyuncuya geçiyor.
                acik_butonlar.Add(btn);//Açýk butona ekleme yaptý.
                return;//direkt clickten çýkarýyor yani ikinci buton týklanana kadar kod çalýþmaz.
            }

            ikinci_tiklanan = btn;
            ikinci_tiklanan.ForeColor = Color.Black;
            IkinciyiBeklemeTimer.Stop();
            acik_butonlar.Add(btn);

            if (birinci_tiklanan.Text == ikinci_tiklanan.Text)
            {
                birinci_tiklanan.Enabled = false;
                ikinci_tiklanan.Enabled = false;
                birinci_tiklanan.ForeColor = Color.Black;
                ikinci_tiklanan.ForeColor = Color.Black;
                acik_butonlar.Clear();
                birinci_tiklanan = null;
                ikinci_tiklanan = null;
                if (aktif_oyuncu == 1)
                {

                    oyuncu1_eslesme++;
                    Oyuncu1Lbl.Text = oyuncu1_eslesme.ToString();
                    if (oyuncu1_eslesme == 10)
                    {
                        foreach (Button buton in Controls.OfType<Button>())
                        {
                            if (buton.Enabled == true) // Hala eþleþmemiþ olan butonlardýr.
                            {
                                buton.ForeColor = Color.Black; // Ýkonu görünür yapar.
                            }
                        }
                        MessageBox.Show("Tebrikler! Oyunu Oyuncu1 Kazandý.");
                        Close();
                    }
                }
                else
                {
                    oyuncu2_eslesme++;
                    Oyuncu2Lbl.Text = oyuncu2_eslesme.ToString();
                    if (oyuncu2_eslesme == 10)
                    {
                        foreach (Button buton in Controls.OfType<Button>())
                        {
                            if (buton.Enabled == true) // Hala eþleþmemiþ olan butonlardýr.
                            {
                                buton.ForeColor = Color.Black; // Ýkonu görünür yapar.
                            }
                        }
                        MessageBox.Show("Tebrikler! Oyunu Oyuncu2 Kazandý.");
                        Close();
                    }

                }

            }

            else
            {
                if (aktif_oyuncu == 1)
                {
                    aktif_oyuncu = 2;
                    OyuncuSirasi.Text = "OYUNCU2";
                }
                else
                {
                    aktif_oyuncu = 1;
                    OyuncuSirasi.Text = "OYUNCU1";
                }
                HataliEslesmeTimer.Interval = 1000;//Seçilen iki kartý da 1 saniyeliðine gösteriyor.
                HataliEslesmeTimer.Start();

            }

        }
    }
}
