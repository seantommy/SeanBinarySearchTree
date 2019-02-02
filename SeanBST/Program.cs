using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeanBST
{
    class Program
    {
        static void Main(string[] args)
        {
            SeanBinaryTree<int> tree = new SeanBinaryTree<int>();

            tree.Add(100);
            tree.Add(99);
            tree.Add(92);
            tree.Add(22);
            tree.Add(21);
            tree.Add(20);

            tree.Add(4);

            tree.Add(6);
            tree.Add(2);

            tree.Add(7);
            tree.Add(3);
            tree.Add(1);
            tree.Add(5);
            
            tree.Add(15);
            tree.Add(11);
            tree.Add(13);
            tree.Add(12);
            tree.Add(10);
            tree.Add(9);
            tree.Add(17);
            tree.Add(18);
            
            bool valueFound = tree.Exists(3);
            valueFound = tree.Exists(4);
            valueFound = tree.Exists(8);

            
            tree.Delete(15);
            tree.Delete(5);
            tree.Delete(9);
            tree.Delete(4);
            

            List<int> treeValues = tree.Traverse();
            
        }
    }
}
