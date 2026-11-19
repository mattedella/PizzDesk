using System;
using System.Collections.Generic;
using System.Text;

namespace Prenotazione_PIzze
{
    public static class Tema
    {
        // Regola 60%
        public static readonly Color Sfondo =
            ColorTranslator.FromHtml("#F5F7F2");

        // Regola 30%
        public static readonly Color Verde =
            ColorTranslator.FromHtml("#527A46");

        public static readonly Color VerdeScuro =
            ColorTranslator.FromHtml("#385536");

        // Regola 10%
        public static readonly Color Giallo =
            ColorTranslator.FromHtml("#E9BD45");

        public static readonly Color GialloScuro =
            ColorTranslator.FromHtml("#CFA23A");

        // Colori neutri
        public static readonly Color Bianco =
            Color.White;

        public static readonly Color Testo =
            ColorTranslator.FromHtml("#29332A");

        public static readonly Color Bordo =
            ColorTranslator.FromHtml("#DFE5DA");
    }
}
