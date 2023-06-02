﻿using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace JustNote_maui.Behaviors
{
    public class ToggleBehavior : Behavior<View>
    {
        readonly TapGestureRecognizer tapRecognizer;

        public ToggleBehavior()
        {
            tapRecognizer = new TapGestureRecognizer
            {
                Command = new Command(() => this.IsToggled = !this.IsToggled)
            };
        }

        public static readonly BindableProperty IsToggledProperty = BindableProperty.Create("IsToggled", typeof(bool), typeof(ToggleBehavior), false, propertyChanged: OnIsToggledChanged);

        public event EventHandler<bool> OnChecked;
        private static void OnIsToggledChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ToggleBehavior toggleBehavior = bindable as ToggleBehavior;
            if( toggleBehavior != null) {
                toggleBehavior.OnChecked?.Invoke(null, (bool)newValue);
            }
        }

        public bool IsToggled
        {
            set { SetValue(IsToggledProperty, value); }
            get 
            
            { return (bool)GetValue(IsToggledProperty); }
        }

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.GestureRecognizers.Add(this.tapRecognizer);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.GestureRecognizers.Remove(this.tapRecognizer);
        }

        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);
            this.BindingContext = bindable.BindingContext;
            bindable.BindingContextChanged += Bindable_BindingContextChanged;
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            base.OnDetachingFrom(bindable);
            this.BindingContext = null;
            bindable.BindingContextChanged -= Bindable_BindingContextChanged;
        }

        void Bindable_BindingContextChanged(object sender, EventArgs e)
        {
            var bobject = sender as BindableObject;

            this.BindingContext = bobject?.BindingContext;
        }
    }
}
