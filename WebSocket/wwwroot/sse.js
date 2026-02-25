window.iniciarSSE = (url, dotNetObjRef) => {
    const evtSource = new EventSource(url);

    evtSource.onmessage = function(event) {
        // envia a string JSON direto
        dotNetObjRef.invokeMethodAsync('ReceberEventoSSE', event.data);
    };

    evtSource.onerror = function() {
        console.log("❌ SSE erro. Tentando reconectar...");
        evtSource.close();
        setTimeout(() => window.iniciarSSE(url, dotNetObjRef), 2000);
    };
};
