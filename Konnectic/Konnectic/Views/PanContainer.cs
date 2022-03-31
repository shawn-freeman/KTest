﻿using Konnectic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Konnectic.Views
{
    public class PanContainer : ContentView
    {
        double currentScale = 1;
        double startScale = 1;
        double xOffset = 0;
        double yOffset = 0;

        int _debugUpdateSpeed = 500;
        DateTime _lastDebug;

        public PanContainer()
        {
            PinchGestureRecognizer pinchGesture = new PinchGestureRecognizer();
            pinchGesture.PinchUpdated += PinchGesture_PinchUpdated;
            GestureRecognizers.Add(pinchGesture);

            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            GestureRecognizers.Add(panGesture);

        }

        private void PinchGesture_PinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            if (e.Status == GestureStatus.Started)
            {
                // Store the current scale factor applied to the wrapped user interface element,
                // and zero the components for the center point of the translate transform.
                startScale = Content.Scale;
                Content.AnchorX = 0;
                Content.AnchorY = 0;
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
                    var output = $"----- BEGIN PINCH OUTPUT -----" +
                                    $"xOffset: {xOffset}\r\n" +
                                    $"yOffset: {yOffset}\r\n" +
                                    $"renderedX: {renderedX}\r\n" +
                                    $"deltaX: {deltaX}\r\n" +
                                    $"deltaWidth: {deltaWidth}\r\n" +
                                    $"originX: {originX}\r\n" +

                                    $"renderedY: {renderedY}\r\n" +
                                    $"deltaY: {deltaY}\r\n" +
                                    $"deltaHeight: {deltaHeight}\r\n" +
                                    $"originY: {originY}\r\n" +

                                    $"targetX: {targetX}\r\n" +
                                    $"targetY: {targetY}\r\n" +

                                    $"Content.TranslationX: {Content.TranslationX}\r\n" +
                                    $"Content.TranslationY: {Content.TranslationY}\r\n" +

                                    $"Content.Scale: {Content.Scale}\r\n" +
                                    $"----- END PINCH OUTPUT -----";
                    Console.WriteLine(output);

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
                        var output = $"----- BEGIN PAN OUTPUT -----" +
                                        $"xOffset: {xOffset}\r\n" +
                                        $"yOffset: {yOffset}\r\n" +
                                        $"newX: {newX}\r\n" +
                                        $"newY: {newY}\r\n" +
                                        $"width: {width}\r\n" +
                                        $"height: {height}\r\n" +

                                        $"canMoveX: {canMoveX}\r\n" +
                                        $"canMoveY: {canMoveY}\r\n" +

                                        $"Content.TranslationX: {Content.TranslationX}\r\n" +
                                        $"Content.TranslationY: {Content.TranslationY}\r\n" +
                                        $"----- END PAN OUTPUT -----";
                        Console.WriteLine(output);

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