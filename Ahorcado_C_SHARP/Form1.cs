using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ahorcado_C_SHARP
{
    public partial class Form1 : Form
    {
   
        String palabraOculta = "";//Palabra que hay que adivinar
        int nFallos = 0;//Cuenta el número de fallos
        int partidasGanadas = 0;//Contador partidas ganadas
        int partidasPerdidas = 0;//Contador partidas perdidas
        bool partidaTerminada = false;//Indica si la partida ha terminado

        List<Button> listaBotones = new List<Button>();//Guardamos los botones que han sido pulsados
        public Form1()
        {
            InitializeComponent();
            guionesIniciales();
           
        }

        //Pone tantos guiones como letras tenga la palabra
        private void guionesIniciales()
        {
            palabraOculta = eligePalabra();
            String barras = "";
            for (int i = 0; i < palabraOculta.Length; i++)
            {
                barras += "_ ";
            }
            label1.Text = barras;
        }

        //Recoge la letra ppulsada
        private void button1_Click(object sender, EventArgs e)
        {
            if (!partidaTerminada)//Solo funciona si el usuario no ha perdidodo ni ganado
            {
                Button b = (Button)sender;
                b.Enabled = false;
                String letra = b.Text;
                letra = letra.ToUpper();
                chequeaLetra(letra);
                listaBotones.Add(b);
            }


        }

        //Comprueba si la letra pulsada está en la palabra oculta y la pone si es el caso
        //Contabiliza los fallos y cambia la imagen
        private void chequeaLetra(String letra)
        {
            if (palabraOculta.Contains(letra))//Si la letra esta
            {


                for (int i = 0; i < palabraOculta.Length; i++)//Ponemos la letra en los huecos correspondientes
                {
                    if (palabraOculta[i] == letra[0])
                    {//Si esta la palabra la ponemos donde los guiones
                     //quita el guión bajo de la letra correspondiente
                        label1.Text = label1.Text.Substring(0, 2 * i)
                                + letra
                                + label1.Text.Substring(2 * i + 1);
                    }
                }
                if (!label1.Text.Contains('_'))//Si la etiqueta no tiene guiones es que la partida no está terminada
                {
                    nFallos = -100;//Para el switch case
                    partidaTerminada = true;
                
                    //Añadimos la partida ganada al marcador
                    partidasGanadas++;
                   
                }

            }
            else
            {
                nFallos++;
                if (nFallos >= 6)
                {//Si hay  6 fallos o más la partida ha terminado
                    partidaTerminada = true;
                   
                    partidasPerdidas++;
                   
                    label1.Text = "";//Dejamos en blanco el label
                    for (int i = 0; i < palabraOculta.Length; i++)//Escribimos la palabra con un espacio en cada letra
                    {
                        label1.Text += palabraOculta[i] + " ";
                    }
                }

            }

            //Elegimos la foto correspondiente
            switch (nFallos)
            {
                case 0: pictureBox1.Image = Properties.Resources.img0; break;
                case 1: pictureBox1.Image = Properties.Resources.img1; break;
                case 2: pictureBox1.Image = Properties.Resources.img2; break;
                case 3: pictureBox1.Image = Properties.Resources.img3; break;
                case 4: pictureBox1.Image = Properties.Resources.img4; break;
                case 5: pictureBox1.Image = Properties.Resources.img5; break;
                case 6: pictureBox1.Image = Properties.Resources.img6; break;
                case -100: pictureBox1.Image = Properties.Resources.win; break;
                default: pictureBox1.Image = Properties.Resources.img6; break;
            }
        }

        //Va a seleccionar al azar una palabra de un array de palabras
        private String eligePalabra()
        {
            String[] listaPalabras = { "CUARENTENA", "CORONAVIRUS", "MULTA", "PAPEL HIGIENICO", "ABURRIDO", "CONVIVENCIA", "PATO"};

            Random aleatorio = new Random(); //Variable aleatoria para elegir palabra

            int posicion = aleatorio.Next(listaPalabras.Length);

            return listaPalabras[posicion].ToUpper();
        }

        //Al pulsar se reinicia
        private void reiniciar(object sender, EventArgs e)
        {

            guionesIniciales();
            partidaTerminada = false;
            nFallos = 0;
            pictureBox1.Image = Properties.Resources.img0;


            foreach (Button item in listaBotones) // Volvemos a habilitar los botones pulsados en la partida anterior
            {
                item.Enabled = true;
            }

            //Vaciamos la lista de botones
            listaBotones.Clear();
            //Ponemos el botón de reiniciar invisible de nuevo
            
        }


    }
}
