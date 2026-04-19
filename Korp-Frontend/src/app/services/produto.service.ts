import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Produto, ProdutoCreate } from '../models/produto.model';

@Injectable({
  providedIn: 'root'
})

export class ProdutoService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7249/api/produtos'; 

  obterTodos(): Observable<Produto[]> {
    return this.http.get<Produto[]>(this.apiUrl);
  }

  criar(produto: ProdutoCreate): Observable<Produto> {
    return this.http.post<Produto>(this.apiUrl, produto);
  }
}
