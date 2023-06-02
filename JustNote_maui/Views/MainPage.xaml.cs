using JustNote_maui.Models;
using JustNote_maui.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustNote_maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            //notesList.ItemsSource = GetListFromDB();
            ((MainViewModel)this.BindingContext).NoteModels = new ObservableCollection<INoteModel>( GetListFromDB());
            base.OnAppearing();
        }

        private IEnumerable<INoteModel> GetListFromDB()
        {
            var tabl1 = App.RequestSimpleNote.GetItems();
            var tabl2 = App.RequestListNote.GetItems();
            var allTables = new List<INoteModel>();

            allTables.AddRange(tabl1);
            allTables.AddRange(tabl2);

            return allTables;
        }

        #region Animation

        private void ButtonCreatedFilter_Clicked(object sender, EventArgs e)
        {
            var selectedColor = (Microsoft.Maui.Graphics.Color)(Resources.MergedDictionaries.First())["FirstMainGradientStop"];  //(Application.Current.RequestedTheme == Microsoft.Maui.ApplicationModel.AppTheme.Light ? (Resources.First()).["SecondMainGradientStop"] : Resources["ThirdMainGradientStop"]);
            var unselectedColor = (Microsoft.Maui.Graphics.Color)(Resources.MergedDictionaries.First())["AddButtonDark"];


            BackgroundAnimation(sortCreateButton, sortCreateButton.BackgroundColor, selectedColor, 500);
            BackgroundAnimation(sortEditButton, sortEditButton.BackgroundColor, unselectedColor, 500);
            BackgroundAnimation(sortABCButton, sortABCButton.BackgroundColor, unselectedColor, 500);
        }

        private void ButtonEditedFilter_Clicked(object sender, EventArgs e)
        {
            var selectedColor = (Microsoft.Maui.Graphics.Color)(Resources.MergedDictionaries.First())["FirstMainGradientStop"];  //(Application.Current.RequestedTheme == Microsoft.Maui.ApplicationModel.AppTheme.Light ? (Resources.First()).["SecondMainGradientStop"] : Resources["ThirdMainGradientStop"]);
            var unselectedColor = (Microsoft.Maui.Graphics.Color)(Resources.MergedDictionaries.First())["AddButtonDark"];

            BackgroundAnimation(sortEditButton, sortEditButton.BackgroundColor, selectedColor, 500);
            BackgroundAnimation(sortCreateButton, sortCreateButton.BackgroundColor, unselectedColor, 500);
            BackgroundAnimation(sortABCButton, sortABCButton.BackgroundColor, unselectedColor, 500);
        }

        private void ButtonAlphabetFilter_Clicked(object sender, EventArgs e)
        {
            var selectedColor = (Microsoft.Maui.Graphics.Color)(Resources.MergedDictionaries.First())["FirstMainGradientStop"];  //(Application.Current.RequestedTheme == Microsoft.Maui.ApplicationModel.AppTheme.Light ? (Resources.First()).["SecondMainGradientStop"] : Resources["ThirdMainGradientStop"]);
            var unselectedColor = (Microsoft.Maui.Graphics.Color)(Resources.MergedDictionaries.First())["AddButtonDark"];

            BackgroundAnimation(sortABCButton, sortABCButton.BackgroundColor, selectedColor, 500);
            BackgroundAnimation(sortCreateButton, sortCreateButton.BackgroundColor, unselectedColor, 500);
            BackgroundAnimation(sortEditButton, sortEditButton.BackgroundColor, unselectedColor, 500);
        }

        private void autoToggleBehavior_OnChecked(object sender, bool e)
        {
            if(e)
            {
                AnimateExpand_Height();
            }
            else
            {
                AnimateCollapsed_Height();
            }
        }



        private void AnimateExpand_Height(int fromHeight = 0, int toHeight = 70, int constWidht = 70, int formAngle = 90, int toAngle = 0)
        {
            Animation animationExpandGridForAddingNote = new Animation()
                    {
                        {0, 1, new Animation(callback: v => {addGrid.IsVisible = true; addGrid.WidthRequest=constWidht; addGrid.HeightRequest = v; },
                                    start: fromHeight,
                                    end: toHeight) },
                        {0, 1, new Animation(callback: v => addFrame.Rotation = v,
                                    start: formAngle,
                                    end: toAngle) }
                    };

            animationExpandGridForAddingNote.Commit(this, "ExpandGridAnimation", rate: 15, length: 250);
        }

        private void AnimateCollapsed_Height(int fromHeight = 70, int toHeight = 0, int formAngle = 0, int toAngle = 90)
        {
            Animation animationCollapseGridForAddingNote = new Animation()
                    {
                        {0, 1, new Animation(callback: v =>  addGrid.HeightRequest = v,
                                   start: fromHeight,
                                   end: toHeight) },
                        {0, 1, new Animation(callback: v => addFrame.Rotation = v,
                                   start: formAngle,
                                   end: toAngle,
                                   finished: () => addGrid.IsVisible = false)
                        }
                
                    };
            animationCollapseGridForAddingNote.Commit(this, "CollapsedGridAnimation", rate: 15, length: 250);
        }


        private void BackgroundAnimation(VisualElement self, Color fromColor, Color toColor, uint length = 500)
        {
            CreateBackgroundAnimation(self,fromColor,toColor).Commit(this, "BackgroundAnimation" + self.Id, 32, length, Easing.Linear);
        }

        private Animation CreateBackgroundAnimation(VisualElement self, Color fromColor, Color toColor)
        {
            return new Animation(callback: v => self.BackgroundColor = Color.FromRgba(fromColor.Red + (v/100) * (toColor.Red - fromColor.Red),
                           fromColor.Green + (v / 100) * (toColor.Green - fromColor.Green),
                           fromColor.Blue + (v / 100) * (toColor.Blue - fromColor.Blue),
                           fromColor.Alpha + (v / 100) * (toColor.Alpha - fromColor.Alpha)),
                  start: 0,
                  end: 100);
        }

        private void reverseSortFrameToggleBehavior_OnChecked(object sender, bool e)
        {

            if (e)
                BackgroundAnimation(reverseSortFrame, reverseSortFrame.BackgroundColor, (Microsoft.Maui.Graphics.Color)(Resources.MergedDictionaries.First())["FirstMainGradientStop"], 500);
            else
                BackgroundAnimation(reverseSortFrame, reverseSortFrame.BackgroundColor, (Microsoft.Maui.Graphics.Color)(Resources.MergedDictionaries.First())["AddButtonDark"], 500);
        }

        #endregion
    }
}
