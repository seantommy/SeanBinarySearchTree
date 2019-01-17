using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeanBST
{
    class SeanBinaryTree<T> where T : IComparable
    {

        private SeanNode<T> head;

        public SeanBinaryTree() { }

        public SeanBinaryTree(T firstEntry)
        {
            Add(firstEntry);
        }

        /// <summary>
        /// Adds a T value to the tree
        /// </summary>
        /// <param name="newEntry"></param>
        public void Add(T newEntry)
        {
            if (head == null) //if there are no entries
            {
                head = new SeanNode<T>(newEntry);
            }
            else
            {
                AddRecurse(newEntry, head);
            }
        }

        private void AddRecurse(T newEntry, SeanNode<T> node)
        {
            if (newEntry.CompareTo(node.Value) <= 0)
            {
                if(node.Left == null)
                {
                    SeanNode<T> newNode = new SeanNode<T>(newEntry);
                    node.Left = newNode;
                }
                else
                {
                    AddRecurse(newEntry, node.Left);
                }
            }
            else
            {
                if(node.Right == null)
                {
                    SeanNode<T> newNode = new SeanNode<T>(newEntry);
                    node.Right = newNode;
                }
                else
                {
                    AddRecurse(newEntry, node.Right);
                }
            }
        }

        /// <summary>
        /// Looks for the given value in the tree and returns true if the value is found.
        /// </summary>
        /// <param name="value">The value you're trying to find.</param>
        /// <returns>Returns true if the value is in the tree.</returns>
        public bool Exists(T valueToFind)
        {
            bool found = false;
            found = ExistsRecurse(valueToFind, head);
            return found;
        }

        private bool ExistsRecurse(T valueToFind, SeanNode<T> node)
        {
            bool found = false;

            if (valueToFind.Equals(node.Value))
            {
                found = true;
            }
            else if (valueToFind.CompareTo(node.Value) < 0)
            {
                if (node.Left == null)
                {
                    found = false;
                }
                else
                {
                    found = ExistsRecurse(valueToFind, node.Left);
                }
            }
            else
            {
                if (node.Right == null)
                {
                    found = false;
                }
                else
                {
                    found = ExistsRecurse(valueToFind, node.Right);
                }
            }
            return found;
        }

        /*public void Delete(T value)
        {
            if (tTree == null || lastEntry == -1)
            {
                return;
            }
            else if (lastEntry == 0)
            {
                lastEntry--;
                tTree = null;
            }
            else
            {

            }

        }
        */
        private void Sort()
        {
            //if()

        }

    }
}
