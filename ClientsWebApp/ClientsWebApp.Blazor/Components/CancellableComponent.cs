﻿using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Components
{
    public class CancellableComponent : ComponentBase, IDisposable
    {
        protected CancellationTokenSource _cts = new();
        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}
