namespace IRL_Image_Creator.Windows.Helpers
{
    public static class ListViewHelper
    {
        public static void AutoResizeColumns(ListView listView)
        {
            int[] maxColumnWitdths = new int[listView.Columns.Count];

            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            foreach (ColumnHeader column in listView.Columns)
            {
                maxColumnWitdths[column.Index] = column.Width;
            }

            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            foreach (ColumnHeader column in listView.Columns)
            {
                if (column.Width > maxColumnWitdths[column.Index])
                {
                    maxColumnWitdths[column.Index] = column.Width;
                }
            }

            for (int i = 0; i < listView.Columns.Count; i++)
            {
                listView.Columns[i].Width = maxColumnWitdths[i];
            }
        }
    }
}
