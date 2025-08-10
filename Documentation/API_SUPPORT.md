# API Support - Work in progress, not available

## Overview

Intent is to allow for seamless integration with various APIs using a standardized configuration approach. So that API workflows can be documented and processed.

## Supported Features

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

## Configuration File

`.gitvision/apiconnect.json`

```json
{
  "ApiConnect": [
    {
      "Name": "ANSIBLE_DEV",
      "Description": "Ansible Development API",
      "BaseUrl": "http://tower.rsyslab.com/api/v2",
      "Headers": {
        "Authorization": "Bearer ghp_12345",
        "Accept": "application/json"
      }
    },
    {
      "Name": "NewsApi2",
      "Description": "News API 2 Description",
      "BaseUrl": "https://api.news.com",
      "Authentication": {
        "User": "UserName",
        "Password": "news-password"
      },
      "Headers": {
        "Accept": "application/json"
      }
    },
    {
      "Name": "NewsApi3",
      "Description": "News API 3 Description",
      "BaseUrl": "https://api.news.com",
      "Headers": {
        "X-Api-Key": "news-api-key",
        "Accept": "application/json, application/stream+json"
      }
    }
  ]
}
```
