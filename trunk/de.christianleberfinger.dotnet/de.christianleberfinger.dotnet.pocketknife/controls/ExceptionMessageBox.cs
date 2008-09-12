using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace de.christianleberfinger.dotnet.pocketknife.controls
{
    /// <summary>
    /// A messagebox showing exceptions in a tree view (includes InnerExceptions)
    /// </summary>
    partial class ExceptionMessageBox : Form
    {
        private ExceptionMessageBox()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExceptionMessageBox_Load(object sender, EventArgs e)
        {
            if(_exception!=null)
                showExceptionInternal(_exception);
        }

        private Exception _exception = null;

        /// <summary>
        /// Shows the dialog with the given exception.
        /// </summary>
        /// <param name="ex"></param>
        public static void showException(Exception ex)
        {
            ExceptionMessageBox e = new ExceptionMessageBox();
            e._exception = ex;
            e.Show();
        }

        private void showExceptionInternal(Exception ex)
        {
            TreeNode rootNode = getNodeFromException(ex);
            TreeNode currentNode = rootNode;
            ex = ex.InnerException;

            while (ex != null)
            {
                TreeNode nextNode = getNodeFromException(ex);
                currentNode.Nodes.Add(nextNode);
                currentNode = nextNode;
                ex = ex.InnerException;
            }

            treeView1.Nodes.Add(rootNode);
        }

        private TreeNode getNodeFromException(Exception ex)
        {
            TreeNode node = new TreeNode(ex.Message);
            node.Text = ex.GetType().Name;
            node.Nodes.Add(ex.Message);

            if(ex.StackTrace!=null && ex.StackTrace!=string.Empty)
                node.Nodes.Add(ex.StackTrace);

            return node;
        }
    }
}
