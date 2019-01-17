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

            tree.Add(4);

            tree.Add(6);
            tree.Add(2);

            tree.Add(7);
            tree.Add(3);
            tree.Add(1);
            tree.Add(5);

            bool valueFound = tree.Exists(3);
            valueFound = tree.Exists(4);
            valueFound = tree.Exists(8);
            

            bool valueDeleted = tree.Delete(2);
            valueDeleted = tree.Delete(5);
        }
    }
}
