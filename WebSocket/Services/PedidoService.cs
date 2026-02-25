using System.Net.Http.Json;
using Microsoft.AspNetCore.SignalR.Client;
using WebSocket.Models;

namespace WebSocket.Services;

public class PedidoService
{
    private readonly HttpClient _http;
    private HubConnection? _hubConnection;

    public PedidoService(HttpClient http)
    {
        _http = http;
    }

    public event Action<PedidoDotNet>? PedidoCriado;
    public event Action<PedidoDotNet>? PedidoAtualizado;
    public event Action<int>? PedidoDeletado;

    public async Task<List<PedidoDotNet>> ObterTodos()
        => await _http.GetFromJsonAsync<List<PedidoDotNet>>("v1/pedidos") ?? new List<PedidoDotNet>();

    public async Task Criar(PedidoDotNet pedido)
        => await _http.PostAsJsonAsync("v1/pedidos", pedido);

    public async Task Atualizar(PedidoDotNet pedido)
        => await _http.PutAsJsonAsync($"v1/pedidos/{pedido.Id}", pedido);

    public async Task Deletar(int id)
        => await _http.DeleteAsync($"v1/pedidos/{id}");

    // ⚡ SignalR
    public async Task IniciarHub()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(_http.BaseAddress!.ToString() + "pedidoHub")
            .WithAutomaticReconnect()
            .Build();

        _hubConnection.On<PedidoDotNet>("PedidoCriado", p => PedidoCriado?.Invoke(p));
        _hubConnection.On<PedidoDotNet>("PedidoAtualizado", p => PedidoAtualizado?.Invoke(p));
        _hubConnection.On<int>("PedidoDeletado", id => PedidoDeletado?.Invoke(id));

        await _hubConnection.StartAsync();
    }
}