using Konnectic.Common;
using Konnectic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Konnectic.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PanPinchContainer : ContentView
	{
        public FlowerOfLifeViewModel ViewModel => (FlowerOfLifeViewModel)BindingContext;

        double currentScale = 1;
        double startScale = 1;
        double xOffset = 0;
        double yOffset = 0;

        int _debugUpdateSpeed = 33;
        DateTime _lastDebug;
        private string classStr;
        private string panStr;
        private string pinchStr;

        public PanPinchContainer()
        {
            PinchGestureRecognizer pinchGesture = new PinchGestureRecognizer();
            pinchGesture.PinchUpdated += PinchGesture_PinchUpdated;
            GestureRecognizers.Add(pinchGesture);

            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            GestureRecognizers.Add(panGesture);
        }

        //protected override void OnChildAdded(Element child)
        //{
        //    base.OnChildAdded(child);

        //    if(debugPanel != null) base.OnChildAdded(debugPanel);
        //}

        private void PinchGesture_PinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            if (e.Status == GestureStatus.Started)
            {
                // Store the current scale factor applied to the wrapped user interface element,
                // and zero the components for the center point of the translate transform.
                startScale = Content.Scale;
                //Content.AnchorX = e.ScaleOrigin.X;
                //Content.AnchorY = e.ScaleOrigin.Y;
            }

            if (e.Status == GestureStatus.Running)
            {
                // Calculate the scale factor to be applied.
                currentScale += (e.Scale - 1) * startScale;
                currentScale = Math.Max(1, currentScale);

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the X pixel coordinate.
                double renderedX = Content.X + xOffset;
                double deltaX = renderedX / Width;
                double deltaWidth = Width / (Content.Width * startScale);
                double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the Y pixel coordinate.
                double renderedY = Content.Y + yOffset;
                double deltaY = renderedY / Height;
                double deltaHeight = Height / (Content.Height * startScale);
                double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                // Calculate the transformed element pixel coordinates.
                double targetX = xOffset - (originX * Content.Width) * (currentScale - startScale);
                double targetY = yOffset - (originY * Content.Height) * (currentScale - startScale);



                // Apply translation based on the change in origin.
                Content.TranslationX = targetX.Clamp(-Content.Width * (currentScale - 1), 0);
                Content.TranslationY = targetY.Clamp(-Content.Height * (currentScale - 1), 0);

                // Apply scale factor.
                Content.Scale = currentScale;

                if (DateTime.Now >= _lastDebug.AddMilliseconds(_debugUpdateSpeed))
                {
                    classStr = $"xOffset: {Math.Round(xOffset, 2)}\r\n" +
                                $"yOffset: {Math.Round(yOffset, 2)}\r\n" +
                                $"currentScale: {Math.Round(currentScale, 2)}\r\n" +
                                $"startScale: {Math.Round(startScale, 2)}\r\n" +
                                $"Content.X: {Math.Round(Content.X, 2)}\r\n" +
                                $"Content.Y: {Math.Round(Content.Y, 2)}\r\n" +
                                $"Content.Height: {Math.Round(Content.Height, 2)}\r\n" +
                                $"Content.Width: {Math.Round(Content.Width, 2)}\r\n" +
                                $"Content.Scale: {Math.Round(Content.Scale, 2)}\r\n" +
                                $"Content.AnchorX: {Math.Round(Content.AnchorX, 2)}\r\n" +
                                $"Content.AnchorY: {Math.Round(Content.AnchorY, 2)}\r\n" +
                                $"Content.TranslationX: {Math.Round(Content.TranslationX, 2)}\r\n" +
                                $"Content.TranslationY: {Math.Round(Content.TranslationY, 2)}\r\n";
                    pinchStr = $"----- BEGIN PINCH OUTPUT -----\r\n" +
                                    $"e.ScaleOrigin.X: {Math.Round(e.ScaleOrigin.X, 2)}\r\n" +
                                    $"e.ScaleOrigin.Y: {Math.Round(e.ScaleOrigin.Y, 2)}\r\n" +
                                    $"e.Scale: {Math.Round(e.Scale, 2)}\r\n" +

                                    $"renderedX: {Math.Round(renderedX, 2)}\r\n" +
                                    $"deltaX: {Math.Round(deltaX, 2)}\r\n" +
                                    $"deltaWidth: {Math.Round(deltaWidth, 2)}\r\n" +
                                    $"originX: {Math.Round(originX, 2)}\r\n" +

                                    $"renderedY: {Math.Round(renderedY, 2)}\r\n" +
                                    $"deltaY: {Math.Round(deltaY, 2)}\r\n" +
                                    $"deltaHeight: {Math.Round(deltaHeight, 2)}\r\n" +
                                    $"originY: {Math.Round(originY, 2)}\r\n" +

                                    $"targetX: {Math.Round(targetX, 2)}\r\n" +
                                    $"targetY: {Math.Round(targetY, 2)}\r\n" +

                                    

                                    $"Content.Scale: {Math.Round(Content.Scale, 2)}\r\n" +
                                    $"----- END PINCH OUTPUT -----";
                    ViewModel.DebugMessage = $"{classStr}{panStr}\r\n{pinchStr}";

                    _lastDebug = DateTime.Now;
                }

            }

            if (e.Status == GestureStatus.Completed)
            {
                // Store the translation delta's of the wrapped user interface element.
                xOffset = Content.TranslationX;
                yOffset = Content.TranslationY;
            }
        }

        public void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            //if (Content.Scale == 1)
            //{
            //    return;
            //}

            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
                    double newX = (e.TotalX * Scale) + xOffset;
                    double newY = (e.TotalY * Scale) + yOffset;

                    double width = (Content.Width * Content.Scale);
                    double height = (Content.Height * Content.Scale);

                    bool canMoveX = width > App.ScreenWidth;
                    bool canMoveY = height > App.ScreenHeight;

                    if (canMoveX)
                    {
                        double minX = (width - (App.ScreenWidth / 2)) * -1;
                        double maxX = Math.Min(App.ScreenWidth / 2, width / 2);

                        if (newX < minX)
                        {
                            newX = minX;
                        }

                        if (newX > maxX)
                        {
                            newX = maxX;
                        }
                    }
                    else
                    {
                        newX = 0;
                    }

                    if (canMoveY)
                    {
                        double minY = (height - (App.ScreenHeight / 2)) * -1;
                        double maxY = Math.Min(App.ScreenHeight / 2, height / 2);

                        if (newY < minY)
                        {
                            newY = minY;
                        }

                        if (newY > maxY)
                        {
                            newY = maxY;
                        }
                    }
                    else
                    {
                        newY = 0;
                    }

                    Content.TranslationX = newX;
                    Content.TranslationY = newY;

                    if (DateTime.Now >= _lastDebug.AddMilliseconds(_debugUpdateSpeed))
                    {
                        classStr = $"xOffset: {Math.Round(xOffset, 2)}\r\n" +
                                    $"yOffset: {Math.Round(yOffset, 2)}\r\n" +
                                    $"currentScale: {Math.Round(currentScale, 2)}\r\n" +
                                    $"startScale: {Math.Round(startScale, 2)}\r\n" +
                                    $"Content.X: {Math.Round(Content.X, 2)}\r\n" +
                                    $"Content.Y: {Math.Round(Content.Y, 2)}\r\n" +
                                    $"Content.Height: {Math.Round(Content.Height, 2)}\r\n" +
                                    $"Content.Width: {Math.Round(Content.Width, 2)}\r\n" +
                                    $"Content.Scale: {Math.Round(Content.Scale, 2)}\r\n" +
                                    $"Content.AnchorX: {Math.Round(Content.AnchorX, 2)}\r\n" +
                                    $"Content.AnchorY: {Math.Round(Content.AnchorY, 2)}\r\n" +
                                    $"Content.TranslationX: {Math.Round(Content.TranslationX, 2)}\r\n" +
                                    $"Content.TranslationY: {Math.Round(Content.TranslationY, 2)}\r\n";
                        panStr = $"----- BEGIN PAN OUTPUT -----\r\n" +
                                        $"newX: {Math.Round(newX, 2)}\r\n" +
                                        $"newY: {Math.Round(newY, 2)}\r\n" +
                                        $"width: {Math.Round(width, 2)}\r\n" +
                                        $"height: {Math.Round(height, 2)}\r\n" +

                                        $"canMoveX: {canMoveX}\r\n" +
                                        $"canMoveY: {canMoveY}\r\n" +
                                        $"----- END PAN OUTPUT -----";
                        ViewModel.DebugMessage = $"{classStr}{panStr}\r\n{pinchStr}";

                        _lastDebug = DateTime.Now;
                    }
                    break;

                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    xOffset = Content.TranslationX;
                    yOffset = Content.TranslationY;
                    break;
            }
        }
    }
}