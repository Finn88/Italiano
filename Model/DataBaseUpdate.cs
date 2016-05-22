﻿using System;
using System.Linq;
using Model.Tables;

namespace Model
{

    public partial class DataBase
    {
        [AttributeUsage(AttributeTargets.Property)]
        public class CoulmnId : Attribute
        {
            public string Text;

            public CoulmnId(string text)
            {
                Text = text;
            }
        }

        public static class GridsIds
        {
            public static int StrudentsGridId
            {
                get { return 1; }
            }
            public static int TeachersGridId
            {
                get { return 10; }
            }
            public static int ContactsGridId
            {
                get { return 20; }
            }
            public static int PaymentsGridId
            {
                get { return 30; }
            }
            public static int GroupsGridId
            {
                get { return 40; }
            }
            public static int PrivateGridId
            {
                get { return 50; }
            }
            public static int EventsGridId
            {
                get { return 60; }
            }
            public static int MasterGridId
            {
                get { return 70; }
            }
        }

        public void UpdateDB()
        {
            if (GridsSettings.Any(c => c.GridId == 1)) return;

            GridsSettings.Clear();
            var flag = 0;

            foreach (var propertyInfo in typeof(Persons).GetProperties().Where(c => Attribute.IsDefined(c, typeof(CoulmnId))))
            {
                var attribute = (CoulmnId)propertyInfo.GetCustomAttributes(typeof(CoulmnId), false).First();
                GridsSettings.Add(new GridsSettings
                {
                    GridId = GridsIds.StrudentsGridId,
                    ColumnHeader = attribute.Text,
                    ColumnName = propertyInfo.Name,
                    IsVisible = false,
                    OrderNr = flag,
                    Width = 100
                });
                GridsSettings.Add(new GridsSettings
                {
                    GridId = GridsIds.TeachersGridId,
                    ColumnHeader = attribute.Text,
                    ColumnName = propertyInfo.Name,
                    IsVisible = false,
                    OrderNr = flag,
                    Width = 100
                });
                GridsSettings.Add(new GridsSettings
                {
                    GridId = GridsIds.ContactsGridId,
                    ColumnHeader = attribute.Text,
                    ColumnName = propertyInfo.Name,
                    IsVisible = false,
                    OrderNr = flag,
                    Width = 100
                });
                flag++;
            }

            GridsSettings.Add(new GridsSettings
            {
                GridId = GridsIds.StrudentsGridId,
                ColumnHeader = "Группа",
                ColumnName = "GroupId",
                IsVisible = false,
                OrderNr = flag + 1,
                Width = 100
            });

            GridsSettings.Add(new GridsSettings
            {
                GridId = GridsIds.StrudentsGridId,
                ColumnHeader = "Обучается с",
                ColumnName = "SecondaryDateString",
                IsVisible = false,
                OrderNr = flag + 2,
                Width = 100
            });

            GridsSettings.Add(new GridsSettings
            {
                GridId = GridsIds.TeachersGridId,
                ColumnHeader = "Работает с",
                ColumnName = "SecondaryDateString",
                IsVisible = false,
                OrderNr = flag + 1,
                Width = 100
            });

            flag = 0;
            foreach (var propertyInfo in typeof(Payments).GetProperties().Where(c => Attribute.IsDefined(c, typeof(CoulmnId))))
            {
                var attribute = (CoulmnId) propertyInfo.GetCustomAttributes(typeof (CoulmnId), false).First();
                GridsSettings.Add(new GridsSettings
                                      {
                                          GridId = GridsIds.PaymentsGridId,
                                          ColumnHeader = attribute.Text,
                                          ColumnName = propertyInfo.Name,
                                          IsVisible = false,
                                          OrderNr = flag,
                                          Width = 100
                                      });
                flag++;
            }
            GridsSettings.Add(new GridsSettings
            {
                GridId = GridsIds.PaymentsGridId,
                ColumnHeader = "Студент",
                ColumnName = "CustomerName",
                IsVisible = false,
                OrderNr = flag+1,
                Width = 100
            });
            
            flag = 0;
            foreach (var propertyInfo in typeof(Groups).GetProperties().Where(c => Attribute.IsDefined(c, typeof(CoulmnId))))
            {
                var attribute = (CoulmnId)propertyInfo.GetCustomAttributes(typeof(CoulmnId), false).First();
                GridsSettings.Add(new GridsSettings
                {
                    GridId = GridsIds.GroupsGridId,
                    ColumnHeader = attribute.Text,
                    ColumnName = propertyInfo.Name,
                    IsVisible = false,
                    OrderNr = flag,
                    Width = 100
                });
                GridsSettings.Add(new GridsSettings
                {
                    GridId = GridsIds.PrivateGridId,
                    ColumnHeader = attribute.Text,
                    ColumnName = propertyInfo.Name,
                    IsVisible = false,
                    OrderNr = flag,
                    Width = 100
                });
                flag++;
            }

            GridsSettings.Add(new GridsSettings
            {
                GridId = GridsIds.GroupsGridId,
                ColumnName = "LessonsDays",
                ColumnHeader = "Дни занятий",
                IsVisible = false,
                OrderNr = flag + 1,
                Width = 100
            });

            GridsSettings.Add(new GridsSettings
            {
                GridId = GridsIds.PrivateGridId,
                ColumnName = "LessonsDays",
                ColumnHeader = "Дни занятий",
                IsVisible = false,
                OrderNr = flag + 1,
                Width = 100
            });

            flag = 0;

            foreach (var propertyInfo in typeof(Events).GetProperties().Where(c => Attribute.IsDefined(c, typeof(CoulmnId))))
            {
                var attribute = (CoulmnId)propertyInfo.GetCustomAttributes(typeof(CoulmnId), false).First();
                GridsSettings.Add(new GridsSettings
                {
                    GridId = GridsIds.EventsGridId,
                    ColumnHeader = attribute.Text,
                    ColumnName = propertyInfo.Name,
                    IsVisible = false,
                    OrderNr = flag,
                    Width = 100
                });
                GridsSettings.Add(new GridsSettings
                {
                    GridId = GridsIds.MasterGridId,
                    ColumnHeader = attribute.Text,
                    ColumnName = propertyInfo.Name,
                    IsVisible = false,
                    OrderNr = flag,
                    Width = 100
                });
                flag++;
            }

            SubmitChanges();
        }

    }
}