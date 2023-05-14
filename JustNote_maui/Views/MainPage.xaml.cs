using JustNote_maui.Models;
using JustNote_maui.ViewModels;
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
            BackgroundAnimation(sortCreateButton, sortCreateButton.BackgroundColor, Color.FromHex("#2B79E1"), 250);
            BackgroundAnimation(sortEditButton, sortEditButton.BackgroundColor, Color.FromRgb(255, 255, 255) , 250);
            BackgroundAnimation(sortABCButton, sortABCButton.BackgroundColor, Color.FromRgb(255, 255, 255), 250);
        }

        private void ButtonEditedFilter_Clicked(object sender, EventArgs e)
        {
            BackgroundAnimation(sortEditButton, sortEditButton.BackgroundColor, Color.FromHex("#2B79E1"), 500);
            BackgroundAnimation(sortCreateButton, sortCreateButton.BackgroundColor, Color.FromRgb(255, 255, 255), 500);
            BackgroundAnimation(sortABCButton, sortABCButton.BackgroundColor, Color.FromRgb(255, 255, 255), 500);
        }

        private void ButtonAlphabetFilter_Clicked(object sender, EventArgs e)
        {


            BackgroundAnimation(sortABCButton, sortABCButton.BackgroundColor, Color.FromHex("#2B79E1"), 500);
            BackgroundAnimation(sortCreateButton, sortCreateButton.BackgroundColor, Color.FromRgb(255, 255, 255), 500);
            BackgroundAnimation(sortEditButton, sortEditButton.BackgroundColor, Color.FromRgb(255, 255, 255), 500);
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
                                   finished: () => addGrid.IsVisible = true)
                        }
                    };
            animationCollapseGridForAddingNote.Commit(this, "CollapsedGridAnimation", rate: 15, length: 250);
        }


        private void BackgroundAnimation(VisualElement self, Color fromColor, Color toColor, uint length = 250)
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
                BackgroundAnimation(reverseSortFrame, reverseSortFrame.BackgroundColor, Color.FromHex("#2B79E1"), 500);
            else
                BackgroundAnimation(reverseSortFrame, reverseSortFrame.BackgroundColor, Color.FromRgb(255, 255, 255), 500);
        }

        #endregion
    }
}
