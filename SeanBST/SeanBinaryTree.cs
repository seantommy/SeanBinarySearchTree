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
                    node.Left.Parent = node;
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
                    node.Right.Parent = node;
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

        /// <summary>
        /// If the provided value exists in the tree, deletes it.
        /// Returns true if the value was found and deleted.
        /// If multiple instances of the value exist, only the first value found will be deleted.
        /// </summary>
        /// <param name="valueToDelete">The value you want to delete from the tree.</param>
        /// <returns>Whether the value was found and deleted or not.</returns>
        public bool Delete(T valueToDelete)
        {
            bool valueDeleted = false;
            if (head == null)
            {
                return valueDeleted;
            }else if(head.Left == null && head.Right == null)
            {
                head = null;
                valueDeleted = true;
            }
            else valueDeleted = DeleteRecurse(valueToDelete, head);

            return valueDeleted;
        }

        private bool DeleteRecurse(T valueToDelete, SeanNode<T> node)
        {
            bool valueDeleted = false;

            if (valueToDelete.Equals(node.Value))
            { 
                //if the node has no children, we can just delete it.
                if (node.Left == null && node.Right == null) 
                {
                    if (node.Value.CompareTo(node.Parent.Value) <= 0)
                    {
                        node.Parent.Left = null;
                    }
                    else
                    {
                        node.Parent.Right = null;
                    }
                }else if(node.Left == null) //if the node has only one child, we can just pass its child to its parent.
                {
                    if (node.Value.CompareTo(node.Parent.Value) <= 0)
                    {
                        node.Parent.Left = node.Right;
                    }
                    else
                    {
                        node.Parent.Right = node.Right;
                    }
                    node.Right.Parent = node.Parent;
                }
                else if(node.Right == null) //see above comment
                {
                    if(node.Value.CompareTo(node.Parent.Value) <= 0)
                    {
                        node.Parent.Left = node.Left;
                    }
                    else
                    {
                        node.Parent.Right = node.Left;
                    }
                    node.Left.Parent = node.Parent;
                }
                else //if the node has two children, we must poll for a replacement.
                {
                    node.Value = PollReplacementValue(node);
                }
                valueDeleted = true;
            }
            else if(valueToDelete.CompareTo(node.Value) < 0)
            {
                if(node.Left != null)
                {
                    valueDeleted = DeleteRecurse(valueToDelete, node.Left);
                }
                
            }
            else
            {
                if (node.Right != null)
                {
                    valueDeleted = DeleteRecurse(valueToDelete, node.Right);
                }
            }

            return valueDeleted;
        }

        /// <summary>
        /// Finds the needed value in the tree, deletes the node where value is found, and returns the found value.
        /// Recurses if found node has children.
        /// </summary>
        /// <param name="node">The node that needs a new value</param>
        /// <returns>The value to be placed in your node</returns>
        private T PollReplacementValue(SeanNode<T> node)
        {
            T returnVal = default(T);

            SeanNode<T> currentNode = new SeanNode<T>();
            currentNode = node.Left;
            while(currentNode.Right != null)
            {
                currentNode = currentNode.Right;
            }

            returnVal = currentNode.Value;

            if(currentNode.Left != null)
            {
                currentNode.Value = PollReplacementValue(currentNode);
            }

            if(currentNode.Value.CompareTo(currentNode.Parent.Value) <= 0)
            {
                if (currentNode.Left == null)
                {
                    currentNode.Parent.Left = null;
                }
                else
                {
                    currentNode.Parent.Left = currentNode.Left;
                    currentNode.Left.Parent = currentNode.Parent;
                }
            }
            else
            {
                if(currentNode.Left == null)
                {
                    currentNode.Parent.Right = null;
                }
                else
                {
                    currentNode.Parent.Right = currentNode.Left;
                    currentNode.Left.Parent = currentNode.Parent;
                }
            }

            return returnVal;
        }
        
        private void Sort()
        {
            //if()

        }

    }
}
