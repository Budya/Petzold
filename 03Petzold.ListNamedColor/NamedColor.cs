using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _03Petzold.ListNamedColor
{
    class NamedColor
    {
        private static NamedColor[] nclrs;
        private Color clr;
        private string str;

        // Static constructor
        static NamedColor()
        {
            PropertyInfo[] props = typeof (Colors).GetProperties();
            nclrs = new NamedColor[props.Length];
            for (int i = 0; i < props.Length; i++)
            {
                nclrs[i] = new NamedColor(props[i].Name, 
                    (Color)props[i].GetValue(null, null));
            }
        }
        // Private constructor
        private NamedColor(string str, Color clr)
        {
            this.str = str;
            this.clr = clr;
        }

        // Static property, only readable
        public static NamedColor[] All
        {
            get { return nclrs; }
        }
        // Prperties read only
        public Color Color
        {
            get { return clr; }
        }

        public string Name
        {
            get 
            { 
                string strSpaced = str[0].ToString();
                for (int i = 0; i < str.Length; i++)
                {
                    strSpaced += (char.IsUpper(str[i]) ? " " : "") +
                                  str[i].ToString();
                }
                return strSpaced;
            }
        }
        // Overridding ToString
        public override string ToString()
        {
            return str;
        }
    }
}
