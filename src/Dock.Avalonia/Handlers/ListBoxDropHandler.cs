﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Avalonia.Input;
using Dock.Model;

namespace Dock.Avalonia
{
    /// <summary>
    /// List box drop handler.
    /// </summary>
    public class ListBoxDropHandler : DefaultDropHandler
    {
        private bool Validate(IDock sourceDock, IDock targetDock, object sender, DragEventArgs e, bool bExecute)
        {
            var point = DropHelper.GetPosition(sender, e);

            if (sourceDock != targetDock)
            {
                if (sourceDock.Owner is IDock sourceOwner && targetDock.Owner is IDock targetOwner)
                {
                    if (sourceOwner == targetOwner)
                    {
                        if (e.DragEffects == DragDropEffects.Copy)
                        {
                            if (bExecute)
                            {
                                // TODO: Clone layout and insert into Visible collection.
                            }
                            return true;
                        }
                        else if (e.DragEffects == DragDropEffects.Move)
                        {
                            if (bExecute)
                            {
                                if (sourceOwner.Factory is IFactory factory)
                                {
                                    factory.MoveDockable(sourceOwner, sourceDock, targetDock);
                                }
                            }
                            return true;
                        }
                        else if (e.DragEffects == DragDropEffects.Link)
                        {
                            if (bExecute)
                            {
                                if (sourceOwner.Factory is IFactory factory)
                                {
                                    factory.SwapDockable(sourceOwner, sourceDock, targetDock);
                                }
                            }
                            return true;
                        }
                    }
                    else
                    {
                        if (e.DragEffects == DragDropEffects.Copy)
                        {
                            if (bExecute)
                            {
                                // TODO: Clone layout and insert into Visible collection.
                            }
                            return true;
                        }
                        else if (e.DragEffects == DragDropEffects.Move)
                        {
                            if (bExecute)
                            {
                                if (sourceOwner.Factory is IFactory factory)
                                {
                                    factory.MoveDockable(sourceOwner, targetOwner, sourceDock, targetDock);
                                }
                            }
                            return true;
                        }
                        else if (e.DragEffects == DragDropEffects.Link)
                        {
                            if (bExecute)
                            {
                                if (sourceOwner.Factory is IFactory factory)
                                {
                                    factory.SwapDockable(sourceOwner, targetOwner, sourceDock, targetDock);
                                }
                            }
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <inheritdoc/>
        public override bool Validate(object sender, DragEventArgs e, object sourceContext, object targetContext, object state)
        {
            if (sourceContext is IDock sourceDock && targetContext is IDock targetDock)
            {
                return Validate(sourceDock, targetDock, sender, e, false);
            }
            return false;
        }

        /// <inheritdoc/>
        public override bool Execute(object sender, DragEventArgs e, object sourceContext, object targetContext, object state)
        {
            if (sourceContext is IDock sourceDock && targetContext is IDock targetDock)
            {
                return Validate(sourceDock, targetDock, sender, e, true);
            }
            return false;
        }
    }
}
