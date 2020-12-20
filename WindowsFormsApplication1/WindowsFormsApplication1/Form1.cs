using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Bitmap bmap;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label1.Text = openFileDialog1.FileName;
                label1.Visible = false;
                pictureBox1.Image = new Bitmap(label1.Text);
                bmap = new Bitmap(label1.Text);
            }
        }

        // CONVERTIR EN NOIR & BLANC
        private void button2_Click(object sender, EventArgs e)
        {
            Color c;
            for (int i = 0; i < bmap.Width; i++)
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    byte gray = (byte)(Math.Min(.299 * c.R + .587 * c.G + .114 * c.B, 255));
                    bmap.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            pictureBox2.Image = bmap;
        }

        //Récupération des fréquences

        private void button3_Click(object sender, EventArgs e)
        {
            int[] t = new int[256];
            Color c;
            for (int i = 0; i < 256; i++)
            {
                t[i] = 0;
            }

            Bitmap b2 = new Bitmap(this.pictureBox2.Image);
            for (int i = 0; i < b2.Width; i++)
            {
                for (int j = 0; j < b2.Height; j++)
                {
                    c = b2.GetPixel(i, j);
                    t[(byte)c.R]++;
                }
            }
            //Histogramme
            for (int i = 0; i < 256; i++)
            {
                //Console.WriteLine(t[i]);
                chart1.Series["Series1"].Points.AddXY(i, t[i]);
            }
            chart1.Visible = true;

        }
            //Histogramme cumulé
        private void button5_Click(object sender, EventArgs e)
        {
            Color c;
            int[] T = new int[256];
            bmap = new Bitmap((Bitmap)pictureBox2.Image);
            for (int j = 0; j < 256; j++)
            {
                T[j] = 0;
            }

            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    T[(byte)c.R]++;

                }
            }
            for (int i = 1; i < 256; i++)
            {
                T[i] = T[i - 1] + T[i];
            }

            for (int j = 0; j < 256; j++)
            {
                chart2.Series["Series1"].Points.AddXY(j, T[j]);
            }
            chart2.Visible = true;
        }
        //Histogramme normalisé
        private void button6_Click(object sender, EventArgs e)
        {
            Color c;
            float nbpixel = 0;
            int[] T = new int[256];
            float[] T1 = new float[256];
            bmap = new Bitmap((Bitmap)pictureBox2.Image);
            for (int j = 0; j < 256; j++)
            {
                T[j] = 0;
            }

            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    T[(byte)c.R]++;

                }
            }

            for (int i = 0; i < 256; i++)
            {
                nbpixel = T[i] + nbpixel;
            }

            for (int i = 0; i < 256; i++)
            {
                T1[i] = T[i] / nbpixel;
            }
            for (int j = 0; j < 256; j++)
            {
                chart3.Series["Series1"].Points.AddXY(j, T1[j]);
            }
            chart3.Visible = true;
        
            
        }
        //Expension
        private void button4_Click(object sender, EventArgs e)
        {
            int min = 255;
            int max = 0;
            int[] t = new int[256];
            int[] t1 = new int[256];
            int nbpixel = 0;
            Color c;
            Bitmap b2 = new Bitmap(this.pictureBox2.Image);
            for (int i = 0; i < b2.Width; i++)
            {
                for (int j = 0; j < b2.Height; j++)
                {
                    c = b2.GetPixel(i, j);
                    if ((byte)c.R <= min)
                    {
                        min = (byte)c.R;
                    }

                    if ((byte)c.R >= max)
                    {
                        max = (byte)c.R;
                    }
                }
            }

            for (int i = 0; i < b2.Width; i++)
                for (int j = 0; j < b2.Height; j++)
                {
                    c = b2.GetPixel(i, j);
                    byte NG=(byte)(255*(c.B - min)/max-min);
                    b2.SetPixel(i, j, Color.FromArgb(NG,NG ,NG ));
                   
                }
            pictureBox3.Image = b2;

                         //Mise à jour des Histogrammes
            for (int i = 0; i < b2.Width; i++)
            {
                for (int j = 0; j < b2.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    t[(byte)c.R]++;
                }
            }//
    for (int i=0;i<256;i++)
    {
    	//textBoxI.Text+="/"+(HistogrammeGris[i]);
        chart4.Series["Series1"].Points.AddXY(i, t[i]);
        chart4.Visible = true;
    }
            //Histogramme normalisé
    for (int i = 0; i < 256; i++)
    {
        nbpixel = t[i] + nbpixel;
    }

    for (int i = 0; i < 256; i++)
    {
        t1[i] = t[i] / nbpixel;
    }
    for (int j = 0; j < 256; j++)
    {
        chart6.Series["Series1"].Points.AddXY(j, t1[j]);
    }
    chart6.Visible = true;
            //Histogramme cumulé
    for (int i = 1; i < 256; i++)
    {
        t[i] = t[i - 1] + t[i];
    }

    for (int j = 0; j < 256; j++)
    {
        chart5.Series["Series1"].Points.AddXY(j, t[j]);
    }
    chart5.Visible = true;

               
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
        
        //Egalisation
        private void Egalisation_Click(object sender, EventArgs e)
        {
            //Mise à jour de l'histogramme
            Color c;
            float nbpixel = 0;
            float[] T = new float[256];
            float[] T4 = new float[256];
            float[] T2 = new float[256];
            float[] T3 = new float[256];
            float max = 1;
            bmap = new Bitmap((Bitmap)pictureBox2.Image);
            for (int j = 0; j < 256; j++)
            {
                T[j] = 0;
                T4[j] = 0;
                T2[j] = 0;
                T3[j] = 0;
            }

            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    T[(byte)c.R]++;

                }
            }
            //Calcul d'intensité maximale
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    if ((byte)c.R >= max)
                    {
                        max = (byte)c.R;
                    }
                }
            }
            //Calcul de l'histogramme cumulé
            
            for (int i = 1; i < 256; i++)
            {
                T[i] = T[i - 1] + T[i];

            }
            //Normalisation de l'histogramme cumulé
            for (int i = 0; i < 256; i++)
            {
                nbpixel = T[i] + nbpixel;

            }

            for (int i = 0; i < 256; i++)
            {
                T2[i] = T[i] / nbpixel;

            }
            //Histogramme égalisé
            for (int i = 0; i < 256; i++)
            {
                T3[i] = T2[i]*max;
              
                
            }
           

            //Mise à jour des intensités de l'image
                for (int i = 0; i < bmap.Width; i++)
                    for (int j = 0; j < bmap.Height; j++)
                    {
                       c = bmap.GetPixel(i, j);
                       byte NG = (byte)(T3[c.R]);
                      //bmap.SetPixel(i, j, Color.FromArgb(NG, NG, NG));

                    }
                pictureBox4.Image = bmap;
            //Mise à jour de l'histogramme
               
                for (int i = 0; i < 256; i++)
                {
                    //textBoxI.Text+="/"+(HistogrammeGris[i]);
                    chart4.Series["Series1"].Points.AddXY(i, T3[i]);
                }
                chart4.Visible = true;
            
            //nouvel hisogramme normalisé
                for (int i = 0; i < 256; i++)
                {
                    nbpixel = T3[i] + nbpixel;
                }

                for (int i = 0; i < 256; i++)
                {
                    T4[i] = T3[i] / nbpixel;
                }
                for (int j = 0; j < 256; j++)
                {
                    chart6.Series["Series1"].Points.AddXY(j, T4[j]);
                }
                chart6.Visible = true;
            //nouvel histogramme cumulé
                for (int i = 1; i < 256; i++)
                {
                    T3[i] = T3[i - 1] + T3[i];
                }

                for (int j = 0; j < 256; j++)
                {
                    chart5.Series["Series1"].Points.AddXY(j, T3[j]);
                }
                chart5.Visible = true;
            }

        //Convolution
        private int[,] Convolution(int N, Bitmap b2, int[,] Filter)
        {

            Color c;

            int pix, sum, pixel, l, k;
            int[,] Resultat = new int[b2.Width, b2.Height];
            Array.Clear(Resultat, 0, Resultat.Length);



            for (int i = (N / 2); i < (b2.Width - N / 2); i++)
            {
                for (int j = (N / 2); j < (b2.Height - N / 2); j++)
                {
                    pixel = 0;

                    for (l = -(N / 2); l <= (N / 2); l++)
                    {
                        for (k = -(N / 2); k <= (N / 2); k++)
                        {
                            c = b2.GetPixel(i + k, j + l);

                            pix = Filter[k + (N / 2), l + (N / 2)];
                            sum = (c.R * pix);
                            pixel = pixel + sum;


                        }
                    }

                    Resultat[i, j] = pixel / (N * N);
                    if (Resultat[i, j] < 0)
                    {
                        Resultat[i, j] = 0;
                    }
                    if (Resultat[i, j] > 255)
                    {
                        Resultat[i, j] = 255;
                    }
                }
            }
            return Resultat;
        }

        //Moyenneur

         private void button13_Click(object sender, EventArgs e)
        {
            Bitmap b2 = new Bitmap(pictureBox2.Image);

            int[,] Filter = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            int N = 3;
            int[,] Res;
            int p = 0;
            Res = Convolution(N, b2, Filter);

            for (int i = 0; i < b2.Width; i++)
            {
                for (int j = 0; j < b2.Height; j++)
                {
                    p = Res[i, j];
                    b2.SetPixel(i, j, Color.FromArgb(p, p, p));
                }

            }

            pictureBox5.Image = b2;

        }
        //Augmentation
         private void button14_Click(object sender, EventArgs e)
         {
             Bitmap b2 = new Bitmap(pictureBox2.Image);
             int[,] Filter = { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
             int N = 3;
             int[,] Res;
             int p = 0;
             Res = Convolution(N, b2, Filter);

             for (int i = 0; i < b2.Width; i++)
             {
                 for (int j = 0; j < b2.Height; j++)
                 {
                     p = Res[i, j];
                     b2.SetPixel(i, j, Color.FromArgb(p, p, p));
                 }

             }

             pictureBox5.Image = b2;

         }
        //Mediane
         private int mediane(int[] voisins)
         {  
            int med;
            Array.Sort(voisins);
            Array.Reverse(voisins);
              if(voisins.Length % 2==0)
              {
                  med= voisins[(voisins.Length/2) +1];
              }
              else {
                  med = voisins[voisins.Length / 2];
              }
             return med;
         }
        //Filtre Mediane
         private void button15_Click(object sender, EventArgs e)
         {
             Bitmap b2 = new Bitmap(pictureBox2.Image);
             int N = 3;
             int p = 0;
             Color c;
             
             int pix,tmp, l, k;
             int m = 0;
             int[,] Med = new int[b2.Width, b2.Height];
             Array.Clear(Med, 0, Med.Length);
             int[,] Filter = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };

             for (int i = (N / 2); i < (b2.Width - N / 2); i++)
             {
                 for (int j = (N / 2); j < (b2.Height - N / 2); j++)
                 {
                     int[] pixel = new int[9];
                     m = 0;
                     for (l = -(N / 2); l <= (N / 2); l++)
                     {
                         for (k = -(N / 2); k <= (N / 2); k++)
                         {
                             c = b2.GetPixel(i + k, j + l);

                             tmp = Filter[k + (N / 2), l + (N / 2)];
                             pix = (c.R * tmp);
                             pixel[m] = pix;
                             m = m + 1;
                         }
                     }
                     Med[i, j] = mediane(pixel);
                    
                 }
             }
             for (int i = 0; i < b2.Width; i++)
             {
                 for (int j = 0; j < b2.Height; j++)
                 {
                     p = Med[i, j];
                     b2.SetPixel(i, j, Color.FromArgb(p, p, p));
                 }

             }

             pictureBox5.Image = b2;
         }
        //Gaussien
         private double[,] gaussien(int N,double sigma)
         {   

             double[,] filtre = new double[N,N];
             double pi = Math.PI;
             for (int i = 0; i < N; i++)
             {
                 for (int j = 0; j < N; j++)
                 {
                     filtre[i, j] = 1 / (pi * 2 * sigma * sigma) * Math.Exp(-(i * i + j * j) / (2 * sigma * sigma));
                     Console.WriteLine("Gaussien");
                     Console.WriteLine(filtre[i, j]);
                 }
             }
             return filtre;
         }
         private double[,] Conv_gauss (int N, Bitmap b2, double[,] Filter)
         {

             Color c;

             double pix, sum, pixel;
             int l, k;
             double[,] Resultat = new double[b2.Width, b2.Height];
             Array.Clear(Resultat, 0, Resultat.Length);

             for (int i = (N / 2); i < (b2.Width - N / 2); i++)
             {
                 for (int j = (N / 2); j < (b2.Height - N / 2); j++)
                 {
                     pixel = 0;

                     for (l = -(N / 2); l <= (N / 2); l++)
                     {
                         for (k = -(N / 2); k <= (N / 2); k++)
                         {
                             c = b2.GetPixel(i + k, j + l);

                             pix = Filter[k + (N / 2), l + (N / 2)];
                             sum = (c.R * pix);
                             pixel = pixel + sum;


                         }
                     }

                     Resultat[i, j] = pixel / (N * N);
                     if (Resultat[i, j] < 0)
                     {
                         Resultat[i, j] = 0;
                     }
                     if (Resultat[i, j] > 255)
                     {
                         Resultat[i, j] = 255;
                     }
                 }
             }
             return Resultat;
         }
           
         //Filtre Gaussien
         private void button7_Click(object sender, EventArgs e)
         {
            Bitmap b2 = new Bitmap(pictureBox2.Image);
            int N = 3;
            int sigma=1;
            double[,] Res;
            int p = 0;
            Res = Conv_gauss(N, b2, gaussien(N,sigma) );

            for (int i = 0; i < b2.Width; i++)
            {
                for (int j = 0; j < b2.Height; j++)
                {
                    p =(int) Res[i, j];
                    b2.SetPixel(i, j, Color.FromArgb(p, p, p));
                }

            }

            pictureBox5.Image = b2;

           
         }


         // segmentation en region 

        //Seuillage binaire

         private void button16_Click(object sender, EventArgs e)
         {
             Bitmap img = new Bitmap(pictureBox2.Image);
             Color c;
             int seuil = 150;
             for (int i = 0; i < img.Width; i++)
             {
                 for (int j = 0; j < img.Height; j++)
                 {
                     c = img.GetPixel(i, j);

                     if (c.R < seuil) img.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                     else img.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                 }
             }
             pictureBox9.Image = img;
         }

         // contour en gradient

         private void button17_Click_1(object sender, EventArgs e)
         {

             Bitmap GX = new Bitmap(pictureBox2.Image);
             Bitmap GY = new Bitmap(pictureBox2.Image);
             Bitmap contour = new Bitmap(pictureBox2.Image) ;
             int[,] masqueX = { {1,0,-1 }, {1,0,-1 }, {1,0,-1 }};
             int[,] masqueY = {{1,1,1 },{0,0,0 }, {-1,-1,-1 }};
             //Couleurr milieu;
             int rX, rY, pix, sueil;
             double result;
             int[,] matriceX;
             int[,] matriceY;
             int[,] G = new int[GX.Width, GX.Height];
             Color cX, cY;
             // calculer la matrice GX
             matriceX = Convolution(3, GX, masqueX);
             // calculer GY
             matriceY = Convolution(3, GY, masqueY);
             //calculer la norme

             for (int i = 0; i < GY.Width; i++)
             {
                 for (int j = 0; j < GY.Height; j++)
                 {
                     rX = matriceX[i, j];
                     rY = matriceY[i, j];

                     result = Math.Round(Math.Sqrt((rX * rX) + (rY * rY)));
                     pix = (int)result;
                     if (result > 255) result = 255;

                     if (result < 0) result = 0;

                     G[i, j] = pix;
                 }

             }

             sueil = 100;

             for (int i = 0; i < GX.Width; i++)
             {
                 for (int j = 0; j < GX.Height; j++)
                 {
                     if (G[i, j] >= sueil) G[i, j] = 255;
                     if (G[i, j] < sueil) G[i, j] = 0;

                     contour.SetPixel(i, j, Color.FromArgb(G[i, j], G[i, j], G[i, j]));
                 }
             }

             pictureBox10.Image = contour;
         }


        //Opérations sur les images

        //Image1
        private void button11_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label4.Text = openFileDialog1.FileName;
                label4.Visible = false;
                pictureBox6.Image = new Bitmap(label4.Text);
                bmap = new Bitmap(label4.Text);

                Color c;
                for (int i = 0; i < bmap.Width; i++)
                    for (int j = 0; j < bmap.Height; j++)
                    {
                        c = bmap.GetPixel(i, j);
                        byte gray = (byte)(Math.Min(.299 * c.R + .587 * c.G + .114 * c.B, 255));
                        bmap.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                    }
                pictureBox6.Image = bmap;
            }
        }
        //Image2
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label5.Text = openFileDialog1.FileName;
                label5.Visible = false;
                pictureBox7.Image = new Bitmap(label4.Text);
                bmap = new Bitmap(label5.Text);
            }

            Color c;
            for (int i = 0; i < bmap.Width; i++)
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    byte gray = (byte)(Math.Min(.299 * c.R + .587 * c.G + .114 * c.B, 255));
                    bmap.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            pictureBox7.Image = bmap;
        }
        //soustraction
        private void button9_Click(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap((Bitmap)pictureBox6.Image);
            Bitmap b2 = new Bitmap((Bitmap)pictureBox7.Image);
            Color c1 = b.GetPixel(0, 0);
            Color c2 = b2.GetPixel(0, 0);
            for (int i = 0; i < b.Height; i++)
            {
                for (int j = 0; j < b.Width; j++)
                {
                    if (i < b.Height && j < b.Height)
                    {
                        c1 = b.GetPixel(i, j);
                    }
                    if (i < b2.Height && j < b2.Height)
                    {
                        c2 = b2.GetPixel(i, j);
                        byte c = (byte)(Math.Max(c1.R - c2.R,0));
                        b2.SetPixel(i, j, Color.FromArgb(c, c, c));
                    }

                }
            }
            pictureBox8.Image = b2;
        }
        //Addition
        private void button8_Click(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap((Bitmap)pictureBox6.Image);
            Bitmap b2 = new Bitmap((Bitmap)pictureBox7.Image);
            Color c1 = b.GetPixel(0, 0);
            Color c2 = b2.GetPixel(0,0);
            for(int i=0;i<b.Height;i++)
            {
                for(int j=0;j<b.Width;j++)
                {   
                    if(i<b.Height && j<b.Height)
                     { c1 = b.GetPixel(i, j);
                                 }
                if (i < b2.Height && j < b2.Height)
                { c2 = b2.GetPixel(i, j); 
                    byte c = (byte)(Math.Min(c1.R+c2.R,255));
                    b2.SetPixel(i, j, Color.FromArgb(c,c,c));
                }
                  
                }
            }
            pictureBox8.Image = b2;
        }
        //Zoom
        private void button10_Click(object sender, EventArgs e)
        {
            
                bmap = new Bitmap(pictureBox8.Image);
                Color c;
                for (int i = 0; i < bmap.Width; i++)
                    for (int j = 0; j < bmap.Height; j++)
                    {
                        c = bmap.GetPixel(i, j);
                        byte z = (byte)(c.R*10);
                        bmap.SetPixel(i, j, Color.FromArgb(z, z, z));
                    }
                pictureBox8.Image = bmap;
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }



        

       

     

      


       

       }

      
        
    
}
