import {} from "./AuthToken";

export class Api {
  private async request<T>(url: string, options: RequestInit = {}): Promise<T> {
    const res = await fetch(
      url, //CORS (en "vite.config.ts") esta configurado para manejar la ruta o ip de la api backend
      {
        ...options,
        credentials: "include",
        headers: {
          "Content-Type": "application/json",

          ...(options.headers || {}),
        },
      },
    );

    if (!res.ok) {
      throw new Error(`Error: ${res.status}`);
    }

    return res.json() as Promise<T>;
  }

  get<T>(url: string) {
    return this.request<T>(url, { method: "GET" });
  }

  post<T>(url: string, body: unknown) {
    return this.request<T>(url, { method: "POST", body: JSON.stringify(body) });
  }

  put<T>(url: string, body: unknown) {
    return this.request<T>(url, { method: "PUT", body: JSON.stringify(body) });
  }

  delete<T>(url: string) {
    return this.request<T>(url, { method: "DELETE" });
  }
}
