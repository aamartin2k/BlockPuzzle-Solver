using BPSolver;
using ControlTreeView;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFProt
{
    public partial class Form1 : Form
    {
        // Asignar mismo color a todos los hijos
       
        //private List<Color> CreateColorList()
        //{
        //    return typeof(Color).GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public)
        //                                          .Select(c => (Color)c.GetValue(null, null))
        //                                          .ToList();
        //} 

        private void UpdateTreeView(GameSimpleNode root)
        {
            List<Color> KnownColorList;
            KnownColorList = typeof(Color).GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public)
                                                  .Select(c => (Color)c.GetValue(null, null))
                                                  .ToList();
            cTreeStatus.Nodes.Clear();

            cTreeStatus.BeginUpdate();
            TreeTrav(root, null, 1);
            cTreeStatus.EndUpdate();


            void TreeTrav(GameSimpleNode dataNode, CTreeNode controlNode, int colorIdx)
            {
               
                // Nueva implementacion con BPNode
                BPNode node;
                node = new BPNode(dataNode.Nombre);
                node.Index = dataNode.Id;
                node.BackColor = KnownColorList[colorIdx];
                node.MakeCurrent = ActionCurrent;
                node.Rename = ActionRename;

                // Nueva implementacion con BPNode

                CTreeNode ctlNodo;
                //ctlNodo = new CTreeNode(dataNode.Id.ToString(), button);
                ctlNodo = new CTreeNode(dataNode.Id.ToString(), node);

                if (null == controlNode)
                    cTreeStatus.Nodes.Add(ctlNodo);
                else
                    controlNode.Nodes.Add(ctlNodo);

                // Si tiene hijos
                if (dataNode.Children.Count() > 0)
                {
                    // Increment indice de color
                    colorIdx = colorIdx + 1;
                    // ignorar Black = 8
                    if (colorIdx == 8)
                        colorIdx = 9;

                    if (colorIdx > KnownColorList.Count - 1)
                        colorIdx = 1;

                    // Para cada hijo recurse Preorder
                    foreach (var item in dataNode.Children)
                    {
                        TreeTrav(item, ctlNodo, colorIdx);
                    }
                }

               

            }
        }

        private int count = 0;

        // Para ejecutar con delegates
        private void ActionRename(int index, string name)
        {
            Console.WriteLine(string.Format(" Renombrar id: {0} a: {1}", index, name));
            Console.WriteLine(string.Format(" Exec Count: {0}", count));
            count++;
            Out_Rename(index, name);
        }

        private void ActionCurrent(int index)
        {
            Console.WriteLine(" Hacer actual id: " + index.ToString());
            Out_MoveToChild(index);
        }


    }
}
