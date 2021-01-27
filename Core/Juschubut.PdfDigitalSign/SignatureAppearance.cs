using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Juschubut.PdfDigitalSign
{
    public class SignatureAppearance
    {
        public SignatureAppearance()
        {
          //  this.X = 50;
            //this.Y = 50;
            //this.Width = 550;
            this.Height = 150;

            //this.AltoFirma = 150;
            this.MargenDerecho = 40;
            this.MargenIzquierdo = 100;
            this.MargenInferior = 40;
        }


        public int? Page { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }


        public int MargenIzquierdo { get; set; }
        public int MargenDerecho { get; set; }
        public int MargenInferior { get; set; }

        public bool FirmaOlografa { get; set; } = true;
        public string SignatureImage { get; set; }
        public string SignatureImageDefault { get; set; }

        public IList<string> PreFirma { get; set; }

        public IList<string> PostFirma { get; set; }

        /// <summary>
        /// Layout de firmas 1, 2, 3, 4, 5, 6, -1: Personalizado
        /// </summary>
        public int Layout { get; set; }

        /// <summary>
        /// Indica el numero de firma dentro del layout
        /// </summary>
        public int NumeroFirma { get; set; }

        public Rectangle GetRectangle(int anchoPagina)
        {
            if (this.Layout > 0)
            {
                int x1 = this.MargenIzquierdo;
                int y1 = this.MargenInferior;

                int w = anchoPagina - this.MargenDerecho - this.MargenInferior;

                Box box = GetBox(w);

                x1 = this.MargenInferior + box.Ancho * box.Columna;
                y1 = this.MargenInferior + box.Alto * box.Fila;

                return new Rectangle(x1, y1, box.Ancho, box.Alto);
            }
            else
            {
                return new Rectangle(this.X, this.Y, this.Width, this.Height);
            }
        }

        private Box GetBox(int anchoMaximo)
        {
            int fila = 0;
            int columna = 0;
            int alto = this.Height;
            int ancho = anchoMaximo;
           
            if (this.Layout <= 3)
            {
                columna = this.NumeroFirma - 1;
                ancho = anchoMaximo / this.Layout;
            }
            if (this.Layout == 4)
            {
                ancho = ancho / 2;

                if (this.NumeroFirma <= 2)
                {
                    columna = this.NumeroFirma-1;
                    fila = 1;
                }
                else 
                {
                    columna = this.NumeroFirma-3;
                }
            }
            if (this.Layout == 5)
            {

                if (this.NumeroFirma <= 2)
                {
                    ancho = anchoMaximo / 2;
                    columna = this.NumeroFirma - 1;
                    fila = 1;
                }
                else
                {
                    ancho = anchoMaximo / 3;
                    columna = this.NumeroFirma - 3;
                }
            }
            if (this.Layout == 6)
            {
                ancho = anchoMaximo / 3;

                if (this.NumeroFirma <= 2)
                {
                    columna = this.NumeroFirma - 1;
                    fila = 1;
                }
                else
                {
                    columna = this.NumeroFirma - 4;
                }
            }

            return new Box
            {
                Fila = fila,
                Columna = columna, 
                Ancho = ancho,
                Alto = alto
            };
        }

        private class Box
        {
            public int Fila { get; set; }
            public int Columna { get; set; }
            public int Ancho { get; set; }
            public int Alto { get; set; }
        }
    }
}
