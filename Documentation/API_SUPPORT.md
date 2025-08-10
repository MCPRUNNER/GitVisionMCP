# API Support

| Feature         | REST                   | SOAP                        | JSON-RPC                   |
| --------------- | ---------------------- | --------------------------- | -------------------------- |
| **Protocol**    | HTTP                   | HTTP, SMTP                  | HTTP, WebSocket            |
| **Data Format** | JSON, XML              | XML                         | JSON                       |
| **Style**       | Resource-based (CRUD)  | Operation-based (methods)   | Method-based (RPC)         |
| **Complexity**  | Low                    | High                        | Low                        |
| **Security**    | HTTPS, OAuth, API keys | WS-Security, SSL            | HTTPS                      |
| **Performance** | Lightweight, fast      | Heavy due to XML            | Lightweight, fast          |
| **Tooling**     | Widely supported       | Requires WSDL and libraries | Less common, but supported |
| **Use Case**    | Web/mobile APIs        | Enterprise systems          | Lightweight internal APIs  |
