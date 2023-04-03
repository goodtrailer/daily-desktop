// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DailyDesktop.Core.Util
{
    /// <summary>
    /// An asynchronous version of <see cref="EventHandler"/>.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">An object that contains no event data.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    public delegate Task AsyncEventHandler(object sender, EventArgs args, CancellationToken cancellationToken);

    /// <summary>
    /// An asynchronous version of <see cref="EventHandler{TEventArgs}"/>.
    /// </summary>
    /// <typeparam name="TEventArgs"></typeparam>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">An object that contains the event data.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    public delegate Task AsyncEventHandler<TEventArgs>(object sender, TEventArgs args, CancellationToken cancellationToken);

    /// <summary>
    /// Extension methods for <see cref="AsyncEventHandler"/> and <see cref="AsyncEventHandler{TEventArgs}"/>.
    /// </summary>
    public static class AsyncEventHandlerExtensions
    {
        /// <summary>
        /// Asynchronous version of <see cref="EventHandler.Invoke(object?, EventArgs)"/>. Asynchronously
        /// but sequentially invokes the subscribed delegates. Again, note that the subscribed delegates
        /// are invoked in sequence, not in parallel.
        /// </summary>
        /// <param name="handler">The <see cref="AsyncEventHandler"/>.</param>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">An object that contains no event data.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        public static async Task InvokeAsync(this AsyncEventHandler? handler, object sender, EventArgs args, CancellationToken cancellationToken)
        {
            if (handler == null)
                return;

            var delegates = handler.GetInvocationList().Cast<AsyncEventHandler>();

            if (delegates.Count() == 0)
                return;

            foreach (var d in delegates)
                await d.Invoke(sender, args, cancellationToken);
        }

        /// <summary>
        /// Asynchronous version of <see cref="EventHandler{TEventArgs}.Invoke(object?, TEventArgs)"/>.
        /// Asynchronously but sequentially invokes the subscribed delegates. Again, note that the subscribed
        /// delegates are invoked in sequence, not in parallel.
        /// </summary>
        /// <typeparam name="TEventArgs"></typeparam>
        /// <param name="handler">The <see cref="AsyncEventHandler{TEventArgs}"/>.</param>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">An object that contains the event data.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        public static async Task InvokeAsync<TEventArgs>(this AsyncEventHandler<TEventArgs>? handler, object sender, TEventArgs args, CancellationToken cancellationToken)
        {
            if (handler == null)
                return;

            var delegates = handler.GetInvocationList().Cast<AsyncEventHandler<TEventArgs>>();

            if (delegates.Count() == 0)
                return;

            foreach (var d in delegates)
                await d.Invoke(sender, args, cancellationToken);
        }
    }
}
