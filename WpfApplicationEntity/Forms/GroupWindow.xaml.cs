using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplicationEntity.Forms
{
    /// <summary>
    /// Логика взаимодействия для GroupWindow.xaml
    /// </summary>
    public partial class GroupWindow : Window
    {
        private bool add_edit;
        private int id;
        public GroupWindow()
        {
            InitializeComponent();
        }
        public GroupWindow(bool add_edit, int id = 0)
        {
            InitializeComponent();
            this.add_edit = add_edit;
            this.id = id;
            if (this.add_edit == false)
            {
                using (WFAEntity.API.MyDBContext objectMyDBContext =
                            new WFAEntity.API.MyDBContext())
                {
                    WFAEntity.API.Group group = WFAEntity.API.DatabaseRequest.GetGroupById(objectMyDBContext, this.id);
                    textBlockAddEditGroup.Text = group.Name;
                }
                ButtonAddEditGroup.Content = "Изменить";
            }
        }

        private bool IsDataCorrcet()
        {
            if (textBlockAddEditGroup.Text != string.Empty)
                return true;
            return false;
        }
        private void ButtonAddEditGroup_Click(object sender, RoutedEventArgs e)
        {
            WFAEntity.API.Group objectGroup = new WFAEntity.API.Group();
            if (this.add_edit == true)
            {
                objectGroup.Name = textBlockAddEditGroup.Text;
                try
                {
                    if (this.IsDataCorrcet() == true)
                    {
                        using (WFAEntity.API.MyDBContext objectMyDBContext =
                            new WFAEntity.API.MyDBContext())
                        {
                            objectMyDBContext.Groups.Add(objectGroup);
                            objectMyDBContext.SaveChanges();
                        }
                        MessageBox.Show("Группа добавлена");
                        this.DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Ввод данных", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    if (this.IsDataCorrcet())
                    {
                        using (WFAEntity.API.MyDBContext objectMyDBContext =
                            new WFAEntity.API.MyDBContext())
                        {
                            WFAEntity.API.Group group = new WFAEntity.API.Group();
                            group = WFAEntity.API.DatabaseRequest.GetGroupById(objectMyDBContext, this.id);
                            group.Name = textBlockAddEditGroup.Text;
                            objectMyDBContext.Entry(group).State = System.Data.Entity.EntityState.Modified;
                            objectMyDBContext.SaveChanges();
                        }
                        MessageBox.Show("Группа изменена");
                        this.DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Ввод данных", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
